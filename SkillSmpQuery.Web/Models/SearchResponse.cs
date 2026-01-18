namespace SkillSmpQuery.Web.Models;

/// <summary>
/// 代表 API 搜尋回應的結果封裝。
/// </summary>
public sealed class SearchResponse
{
    /// <summary>
    /// 搜尋請求是否成功。
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// 若請求失敗，此處包含錯誤訊息；否則為 null。
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// 搜尋到的技能結果列表。
    /// </summary>
    public IReadOnlyList<SkillSearchResult> Results { get; init; } = [];
}
