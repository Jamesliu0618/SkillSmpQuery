namespace SkillSmpQuery.Wasm.Models;

/// <summary>
/// 代表單一技能的詳細資訊。
/// </summary>
public sealed record SkillInfo
{
    /// <summary>
    /// 技能名稱。
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// 技能作者名稱。
    /// </summary>
    public string Author { get; init; } = string.Empty;

    /// <summary>
    /// 技能描述內容。
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// 技能的官方連結 URL。
    /// </summary>
    public string SkillUrl { get; init; } = string.Empty;

    /// <summary>
    /// 技能獲得的星星數量。
    /// </summary>
    public int Stars { get; init; }
}
