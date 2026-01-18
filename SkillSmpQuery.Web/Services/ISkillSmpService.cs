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
    /// <param name="query">要搜尋的關鍵字字串。</param>
    /// <param name="cancellationToken">用於取消非同步操作的 Token。</param>
    /// <returns>包含搜尋結果的回應物件。</returns>
    Task<SearchResponse> SearchAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// 執行 AI 語義搜尋。
    /// </summary>
    /// <param name="query">要搜尋的語句或描述。</param>
    /// <param name="cancellationToken">用於取消非同步操作的 Token。</param>
    /// <returns>包含搜尋結果的回應物件。</returns>
    Task<SearchResponse> AiSearchAsync(string query, CancellationToken cancellationToken = default);

    /// <summary>
    /// 設定用於 API 驗證的金鑰。
    /// </summary>
    /// <param name="apiKey">API 金鑰字串。</param>
    void SetApiKey(string apiKey);

    /// <summary>
    /// 取得是否已設定有效的 API Key。
    /// </summary>
    bool HasApiKey { get; }
}
