using SkillSmpQuery.Web.Models;

namespace SkillSmpQuery.Web.Services;

/// <summary>
/// 定義 SkillSMP API 服務的介面。
/// </summary>
public interface ISkillSmpService
{
    /// <summary>
    /// 執行關鍵字搜尋。
    /// </summary>
    Task<SearchResponse> SearchAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// 執行 AI 語義搜尋。
    /// </summary>
    Task<SearchResponse> AiSearchAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// 設定 API Key。
    /// </summary>
    void SetApiKey(string apiKey);

    /// <summary>
    /// 檢查是否已設定 API Key。
    /// </summary>
    bool HasApiKey { get; }
}
