namespace SkillSmpQuery.Web.Models;

/// <summary>
/// 代表 API 搜尋回應的結果。
/// </summary>
public sealed class SearchResponse
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public IReadOnlyList<SkillSearchResult> Results { get; init; } = [];
}
