using System.Text;
using SkillSmpQuery.Models;

namespace SkillSmpQuery.Formatters;

/// <summary>
/// 負責將搜尋結果格式化為顯示用文字。
/// </summary>
public static class ResultFormatter
{
    /// <summary>
    /// 將搜尋回應格式化為易讀的文字格式。
    /// </summary>
    /// <param name="response">搜尋回應。</param>
    /// <returns>格式化後的字串。</returns>
    public static string Format(SearchResponse response)
    {
        if (!response.Success)
        {
            return response.ErrorMessage ?? "發生未知錯誤。";
        }

        if (response.Results.Count == 0)
        {
            return "無符合條件的結果。";
        }

        var sb = new StringBuilder();
        int index = 1;

        foreach (var result in response.Results)
        {
            var skill = result.Skill;

            sb.AppendLine($"[{index}] {skill.Name} (by {skill.Author})");
            sb.AppendLine($"    Stars: {skill.Stars}  |  Score: {result.Score:F2}");
            sb.AppendLine($"    {skill.Description}");
            sb.AppendLine($"    {skill.SkillUrl}");
            sb.AppendLine();

            index++;
        }

        return sb.ToString();
    }
}
