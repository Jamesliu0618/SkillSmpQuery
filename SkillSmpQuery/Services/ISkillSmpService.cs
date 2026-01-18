using SkillSmpQuery.Models;

namespace SkillSmpQuery.Services;

/// <summary>
/// 定義 SkillSMP API 服務的介面。
/// </summary>
public interface ISkillSmpService
{
    /// <summary>
    /// 執行關鍵字搜尋。
    /// </summary>
    /// <param name="query">搜尋關鍵字。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>搜尋回應結果。</returns>
    Task<SearchResponse> SearchAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// 執行 AI 語義搜尋。
    /// </summary>
    /// <param name="query">搜尋關鍵字。</param>
    /// <param name="cancellationToken">取消令牌。</param>
    /// <returns>搜尋回應結果。</returns>
    Task<SearchResponse> AiSearchAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// 設定 API Key。
    /// </summary>
    /// <param name="apiKey">API 金鑰。</param>
    void SetApiKey(string apiKey);

    /// <summary>
    /// 檢查是否已設定 API Key。
    /// </summary>
    bool HasApiKey { get; }
}
