namespace SkillSmpQuery.Models;

/// <summary>
/// 代表搜尋結果中的單一項目，包含技能與相關性分數。
/// </summary>
public sealed record SkillSearchResult
{
    public required SkillInfo Skill { get; init; }
    public double Score { get; init; }
}
