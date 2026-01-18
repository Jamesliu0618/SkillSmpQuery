namespace SkillSmpQuery.Wasm.Infrastructure;

/// <summary>
/// 定義應用程式設定儲存的介面。
/// </summary>
public interface ISettingsProvider
{
    /// <summary>
    /// 取得 API Key。
    /// </summary>
    Task<string> GetApiKeyAsync();

    /// <summary>
    /// 設定 API Key。
    /// </summary>
    Task SetApiKeyAsync(string apiKey);

    /// <summary>
    /// 取得搜尋歷史記錄。
    /// </summary>
    Task<IReadOnlyList<string>> GetSearchHistoryAsync();

    /// <summary>
    /// 新增搜尋記錄。
    /// </summary>
    Task AddSearchHistoryAsync(string query);
}
