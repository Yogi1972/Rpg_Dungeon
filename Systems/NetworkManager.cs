using Night.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Rpg_Dungeon.Systems
{
    /// <summary>
    /// Manages network connections for LAN multiplayer
    /// </summary>
    public class NetworkManager
    {
        #region Fields

        private TcpListener? _server;
        private TcpClient? _client;
        private NetworkStream? _stream;
        private bool _isHost;
        private bool _isConnected;
        private string _playerId;
        private Thread? _receiveThread;
        private Queue<NetworkMessage> _messageQueue;
        private readonly object _queueLock = new();

        public const int DEFAULT_PORT = 7777;

        #endregion

        #region Properties

        public bool IsHost => _isHost;
        public bool IsConnected => _isConnected;
        public string PlayerId => _playerId;

        #endregion

        #region Events

        public event Action<NetworkMessage>? OnMessageReceived;
        public event Action<string>? OnPlayerConnected;
        public event Action<string>? OnPlayerDisconnected;
        public event Action<string>? OnError;

        #endregion

        #region Constructor

        public NetworkManager()
        {
            _playerId = Guid.NewGuid().ToString();
            _messageQueue = new Queue<NetworkMessage>();
        }

        #endregion

        #region Server Methods (Host)

        /// <summary>
        /// Start hosting a game server
        /// </summary>
        public bool StartHost(int port = DEFAULT_PORT)
        {
            try
            {
                _server = new TcpListener(IPAddress.Any, port);
                _server.Start();
                _isHost = true;
                _isConnected = true;

                Console.WriteLine($"✅ Server started on port {port}");
                Console.WriteLine($"📡 Local IP: {GetLocalIPAddress()}");
                Console.WriteLine("⏳ Waiting for players to connect...");

                // Start accepting clients in background
                Task.Run(() => AcceptClients());

                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Failed to start server: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Accept incoming client connections
        /// </summary>
        private async void AcceptClients()
        {
            while (_isHost && _server != null)
            {
                try
                {
                    var client = await _server.AcceptTcpClientAsync();
                    Console.WriteLine($"🎮 Player connected from {((IPEndPoint)client.Client.RemoteEndPoint!).Address}");
                    
                    // Handle this client in a separate task
                    _ = Task.Run(() => HandleClient(client));
                }
                catch (Exception ex)
                {
                    if (_isHost)
                    {
                        OnError?.Invoke($"Error accepting client: {ex.Message}");
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Handle individual client connection
        /// </summary>
        private void HandleClient(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();
                var buffer = new byte[4096];

                while (_isHost && client.Connected)
                {
                    var bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        var json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        var message = NetworkMessage.FromJson(json);
                        
                        if (message != null)
                        {
                            EnqueueMessage(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client disconnected: {ex.Message}");
            }
        }

        #endregion

        #region Client Methods (Join)

        /// <summary>
        /// Connect to a host server
        /// </summary>
        public bool ConnectToHost(string ipAddress, int port = DEFAULT_PORT)
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(ipAddress, port);
                _stream = _client.GetStream();
                _isHost = false;
                _isConnected = true;

                Console.WriteLine($"✅ Connected to server at {ipAddress}:{port}");

                // Start receiving messages
                _receiveThread = new Thread(ReceiveMessages);
                _receiveThread.IsBackground = true;
                _receiveThread.Start();

                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Failed to connect: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Receive messages from server
        /// </summary>
        private void ReceiveMessages()
        {
            var buffer = new byte[4096];

            while (_isConnected && _stream != null)
            {
                try
                {
                    var bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        var json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        var message = NetworkMessage.FromJson(json);
                        
                        if (message != null)
                        {
                            EnqueueMessage(message);
                        }
                    }
                    else
                    {
                        // Connection closed
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (_isConnected)
                    {
                        OnError?.Invoke($"Error receiving message: {ex.Message}");
                    }
                    break;
                }
            }

            Disconnect();
        }

        #endregion

        #region Message Handling

        /// <summary>
        /// Send a message to the network
        /// </summary>
        public void SendMessage(NetworkMessage message)
        {
            try
            {
                var json = message.ToJson();
                var data = Encoding.UTF8.GetBytes(json);

                if (_isHost && _server != null)
                {
                    // Broadcast to all clients (implement client list tracking)
                    // For now, just log
                    Console.WriteLine($"📤 [HOST] Sent: {message.Type}");
                }
                else if (_stream != null)
                {
                    _stream.Write(data, 0, data.Length);
                    Console.WriteLine($"📤 [CLIENT] Sent: {message.Type}");
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Failed to send message: {ex.Message}");
            }
        }

        /// <summary>
        /// Enqueue received message for processing
        /// </summary>
        private void EnqueueMessage(NetworkMessage message)
        {
            lock (_queueLock)
            {
                _messageQueue.Enqueue(message);
            }

            OnMessageReceived?.Invoke(message);
        }

        /// <summary>
        /// Process all queued messages
        /// </summary>
        public List<NetworkMessage> GetPendingMessages()
        {
            lock (_queueLock)
            {
                var messages = _messageQueue.ToList();
                _messageQueue.Clear();
                return messages;
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Get local IP address
        /// </summary>
        public static string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "127.0.0.1";
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// Disconnect from network
        /// </summary>
        public void Disconnect()
        {
            _isConnected = false;

            try
            {
                _stream?.Close();
                _client?.Close();
                _server?.Stop();

                Console.WriteLine("🔌 Disconnected from network");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during disconnect: {ex.Message}");
            }
        }

        #endregion
    }

    /// <summary>
    /// Network lobby manager for pre-game setup
    /// </summary>
    public class NetworkLobby
    {
        private NetworkManager _networkManager;
        private LobbyStateData _lobbyState;
        private bool _isReady;

        public NetworkLobby(NetworkManager networkManager)
        {
            _networkManager = networkManager;
            _lobbyState = new LobbyStateData();
            _isReady = false;
        }

        public bool IsReady => _isReady;
        public LobbyStateData LobbyState => _lobbyState;

        /// <summary>
        /// Add player to lobby
        /// </summary>
        public void AddPlayer(PlayerJoinData player)
        {
            if (_lobbyState.Players.Count < _lobbyState.MaxPlayers)
            {
                _lobbyState.Players.Add(player);
                BroadcastLobbyUpdate();
            }
        }

        /// <summary>
        /// Remove player from lobby
        /// </summary>
        public void RemovePlayer(string playerName)
        {
            _lobbyState.Players.RemoveAll(p => p.PlayerName == playerName);
            BroadcastLobbyUpdate();
        }

        /// <summary>
        /// Broadcast lobby state to all players
        /// </summary>
        private void BroadcastLobbyUpdate()
        {
            var json = JsonSerializer.Serialize(_lobbyState);
            var message = new NetworkMessage(NetworkMessageType.LobbyUpdate, _networkManager.PlayerId, json);
            _networkManager.SendMessage(message);
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            _lobbyState.GameStarted = true;
            _isReady = true;

            var message = new NetworkMessage(NetworkMessageType.GameStart, _networkManager.PlayerId, "START");
            _networkManager.SendMessage(message);
        }
    }
}
