namespace SkillSmpQuery.Wasm.Infrastructure;

/// <summary> 提供應用程式設定的存取介面。 </summary>
public interface ISettingsProvider
{
    /// <summary> 取得儲存的 API Key。 </summary>
    Task<string> GetApiKeyAsync();

    /// <summary> 儲存 API Key。 </summary>
    Task SetApiKeyAsync(string apiKey);

    /// <summary> 取得搜尋歷史紀錄。 </summary>
    Task<IReadOnlyList<string>> GetSearchHistoryAsync();

    /// <summary> 新增一筆搜尋紀錄。 </summary>
    Task AddSearchHistoryAsync(string query);
}
