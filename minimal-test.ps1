try {
    $isNewer = $true
    if ($isNewer) {
        Write-Host "test"
    } elseif ($false) {
        Write-Host "test2"
    } else {
        Write-Host "test3"
    }
} catch {
    Write-Host "error"
}
