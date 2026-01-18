using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SkillSmpQuery.Infrastructure;

/// <summary>
/// 使用 Windows DPAPI 加密的設定儲存提供者。
/// </summary>
public sealed class SettingsProvider : ISettingsProvider
{
    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "SkillSmpQuery",
        "settings.json"
    );

    private const int MaxSearchHistory = 20;

    private SettingsData _data = new();

    public void Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var json = File.ReadAllText(SettingsPath);
                _data = JsonSerializer.Deserialize<SettingsData>(json) ?? new SettingsData();
            }
        }
        catch
        {
            _data = new SettingsData();
        }
    }

    public void Save()
    {
        try
        {
            var directory = Path.GetDirectoryName(SettingsPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsPath, json);
        }
        catch
        {
            // 儲存失敗時靜默處理
        }
    }

    public string GetApiKey()
    {
        if (string.IsNullOrEmpty(_data.EncryptedApiKey))
            return string.Empty;

        try
        {
            var encryptedBytes = Convert.FromBase64String(_data.EncryptedApiKey);
            var decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        catch
        {
            return string.Empty;
        }
    }

    public void SetApiKey(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            _data.EncryptedApiKey = string.Empty;
            return;
        }

        try
        {
            var plainBytes = Encoding.UTF8.GetBytes(apiKey);
            var encryptedBytes = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
            _data.EncryptedApiKey = Convert.ToBase64String(encryptedBytes);
        }
        catch
        {
            _data.EncryptedApiKey = string.Empty;
        }
    }

    public IReadOnlyList<string> GetSearchHistory() => _data.SearchHistory.AsReadOnly();

    public void AddSearchHistory(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return;

        _data.SearchHistory.Remove(query);
        _data.SearchHistory.Insert(0, query);

        if (_data.SearchHistory.Count > MaxSearchHistory)
        {
            _data.SearchHistory.RemoveRange(MaxSearchHistory, _data.SearchHistory.Count - MaxSearchHistory);
        }
    }

    /// <summary>
    /// 內部資料結構，用於 JSON 序列化。
    /// </summary>
    private sealed class SettingsData
    {
        public string EncryptedApiKey { get; set; } = string.Empty;
        public List<string> SearchHistory { get; set; } = new();
    }
}
