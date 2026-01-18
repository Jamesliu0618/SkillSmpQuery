namespace SkillSmpQuery.Wasm.Models;

/// <summary>
/// 代表搜尋結果中的單一項目，包含技能與相關性分數。
/// </summary>
public sealed record SkillSearchResult
{
    /// <summary>
    /// 技能詳細資訊。
    /// </summary>
    public required SkillInfo Skill { get; init; }

    /// <summary>
    /// 搜尋結果的相關性分數 (越高代表越相關)。
    /// </summary>
    public double Score { get; init; }
}
