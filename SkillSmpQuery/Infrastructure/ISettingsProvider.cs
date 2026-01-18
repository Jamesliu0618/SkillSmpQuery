namespace SkillSmpQuery.Infrastructure;

/// <summary>
/// 定義應用程式設定儲存的介面。
/// </summary>
public interface ISettingsProvider
{
    /// <summary>
    /// 取得 API Key。
    /// </summary>
    string GetApiKey();

    /// <summary>
    /// 設定 API Key（會加密儲存）。
    /// </summary>
    void SetApiKey(string apiKey);

    /// <summary>
    /// 取得搜尋歷史記錄。
    /// </summary>
    IReadOnlyList<string> GetSearchHistory();

    /// <summary>
    /// 新增搜尋記錄。
    /// </summary>
    void AddSearchHistory(string query);

    /// <summary>
    /// 儲存設定到持久化儲存。
    /// </summary>
    void Save();

    /// <summary>
    /// 從持久化儲存載入設定。
    /// </summary>
    void Load();
}
