using Night.Characters;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rpg_Dungeon.Systems
{
    /// <summary>
    /// Message types for network communication
    /// </summary>
    public enum NetworkMessageType
    {
        // Connection
        PlayerJoin,
        PlayerLeave,
        LobbyUpdate,
        GameStart,
        
        // Game State
        TurnStart,
        TurnEnd,
        CombatAction,
        CombatResult,
        GameStateSync,
        
        // Chat
        ChatMessage,
        
        // System
        Ping,
        Pong,
        Disconnect
    }

    /// <summary>
    /// Network message for client-server communication
    /// </summary>
    public class NetworkMessage
    {
        public NetworkMessageType Type { get; set; }
        public string SenderId { get; set; }
        public string Data { get; set; }
        public DateTime Timestamp { get; set; }

        public NetworkMessage()
        {
            SenderId = string.Empty;
            Data = string.Empty;
            Timestamp = DateTime.UtcNow;
        }

        public NetworkMessage(NetworkMessageType type, string senderId, string data)
        {
            Type = type;
            SenderId = senderId;
            Data = data;
            Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Serialize message to JSON
        /// </summary>
        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions 
            { 
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }

        /// <summary>
        /// Deserialize message from JSON
        /// </summary>
        public static NetworkMessage? FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<NetworkMessage>(json);
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Player join data
    /// </summary>
    public class PlayerJoinData
    {
        public string PlayerName { get; set; } = string.Empty;
        public string CharacterName { get; set; } = string.Empty;
        public string CharacterClass { get; set; } = string.Empty;
    }

    /// <summary>
    /// Lobby state data
    /// </summary>
    public class LobbyStateData
    {
        public List<PlayerJoinData> Players { get; set; } = new();
        public int MaxPlayers { get; set; } = 4;
        public bool GameStarted { get; set; } = false;
    }

    /// <summary>
    /// Combat action data
    /// </summary>
    public class CombatActionData
    {
        public string CharacterName { get; set; } = string.Empty;
        public string ActionType { get; set; } = string.Empty; // "Attack", "Ability", "Item", "Stance", "Pass"
        public string TargetName { get; set; } = string.Empty;
        public int AbilityIndex { get; set; } = -1;
        public int ItemSlotIndex { get; set; } = -1;
        public string StanceName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Combat result data
    /// </summary>
    public class CombatResultData
    {
        public string Message { get; set; } = string.Empty;
        public int Damage { get; set; } = 0;
        public int Healing { get; set; } = 0;
        public int MobHp { get; set; } = 0;
        public Dictionary<string, int> CharacterHp { get; set; } = new();
    }

    /// <summary>
    /// Turn state data
    /// </summary>
    public class TurnStateData
    {
        public string CurrentPlayer { get; set; } = string.Empty;
        public int TurnNumber { get; set; } = 0;
        public int MobHp { get; set; } = 0;
        public int MobMaxHp { get; set; } = 0;
    }
}
