namespace SkillSmpQuery.Models;

/// <summary>
/// 代表單一技能的詳細資訊。
/// </summary>
public sealed record SkillInfo
{
    public string Name { get; init; } = string.Empty;
    public string Author { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string SkillUrl { get; init; } = string.Empty;
    public int Stars { get; init; }
}
