using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SkillSmpQuery;

/// <summary>
/// 管理應用程式設定，包含 API Key 加密儲存與搜尋歷史。
/// </summary>
public class AppSettings
{
    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "SkillSmpQuery",
        "settings.json"
    );

    private const int MaxSearchHistory = 20;

    public string EncryptedApiKey { get; set; } = string.Empty;
    public List<string> SearchHistory { get; set; } = new();

    /// <summary>
    /// 載入設定檔。
    /// </summary>
    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var json = File.ReadAllText(SettingsPath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
        }
        catch
        {
            // 設定檔損壞時回傳預設值
        }
        return new AppSettings();
    }

    /// <summary>
    /// 儲存設定檔。
    /// </summary>
    public void Save()
    {
        try
        {
            var directory = Path.GetDirectoryName(SettingsPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsPath, json);
        }
        catch
        {
            // 儲存失敗時靜默處理
        }
    }

    /// <summary>
    /// 取得解密後的 API Key。
    /// </summary>
    public string GetApiKey()
    {
        if (string.IsNullOrEmpty(EncryptedApiKey))
            return string.Empty;

        try
        {
            var encryptedBytes = Convert.FromBase64String(EncryptedApiKey);
            var decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 設定並加密 API Key。
    /// </summary>
    public void SetApiKey(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            EncryptedApiKey = string.Empty;
            return;
        }

        try
        {
            var plainBytes = Encoding.UTF8.GetBytes(apiKey);
            var encryptedBytes = ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);
            EncryptedApiKey = Convert.ToBase64String(encryptedBytes);
        }
        catch
        {
            EncryptedApiKey = string.Empty;
        }
    }

    /// <summary>
    /// 新增搜尋記錄。
    /// </summary>
    public void AddSearchHistory(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return;

        // 移除重複項目
        SearchHistory.Remove(query);
        
        // 插入到最前面
        SearchHistory.Insert(0, query);

        // 限制數量
        if (SearchHistory.Count > MaxSearchHistory)
        {
            SearchHistory.RemoveRange(MaxSearchHistory, SearchHistory.Count - MaxSearchHistory);
        }
    }
}
