using System.Net.Http.Headers;
using System.Text.Json;

namespace SkillSmpQuery;

/// <summary>
/// 負責與 SkillSMP API 進行通訊的客戶端。
/// </summary>
public class SkillSmpClient
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private const string BaseUrl = "https://skillsmp.com/api/v1";

    /// <summary>
    /// 執行關鍵字搜尋。
    /// </summary>
    public async Task<string> SearchAsync(string apiKey, string query, CancellationToken cancellationToken = default)
    {
        var json = await SendRequestAsync(apiKey, $"/skills/search?q={Uri.EscapeDataString(query)}", cancellationToken);
        return FormatResponse(json);
    }

    /// <summary>
    /// 執行 AI 語義搜尋。
    /// </summary>
    public async Task<string> AiSearchAsync(string apiKey, string query, CancellationToken cancellationToken = default)
    {
        var json = await SendRequestAsync(apiKey, $"/skills/ai-search?q={Uri.EscapeDataString(query)}", cancellationToken);
        return FormatResponse(json);
    }

    /// <summary>
    /// 發送 HTTP GET 請求並取得回應內容。
    /// </summary>
    private async Task<string> SendRequestAsync(string apiKey, string endpoint, CancellationToken cancellationToken)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BaseUrl + endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return content;
            }
            else
            {
                return $"Error: {(int)response.StatusCode} {response.ReasonPhrase}\r\nDetails: {content}";
            }
        }
        catch (OperationCanceledException)
        {
            return "搜尋已取消。";
        }
        catch (HttpRequestException ex)
        {
            return $"網路錯誤: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"未預期的錯誤: {ex.Message}";
        }
    }

    /// <summary>
    /// 格式化 API 回應為易讀格式。
    /// </summary>
    private string FormatResponse(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("success", out var successProp) || !successProp.GetBoolean())
            {
                return $"API 回應失敗:\r\n{json}";
            }

            if (!root.TryGetProperty("data", out var dataProp) ||
                !dataProp.TryGetProperty("data", out var resultsProp))
            {
                return "無搜尋結果。";
            }

            var sb = new System.Text.StringBuilder();
            int index = 1;

            foreach (var item in resultsProp.EnumerateArray())
            {
                if (!item.TryGetProperty("skill", out var skill)) continue;

                string name = skill.TryGetProperty("name", out var n) ? n.GetString() ?? "" : "";
                string author = skill.TryGetProperty("author", out var a) ? a.GetString() ?? "" : "";
                string desc = skill.TryGetProperty("description", out var d) ? d.GetString() ?? "" : "";
                string url = skill.TryGetProperty("skillUrl", out var u) ? u.GetString() ?? "" : "";
                int stars = skill.TryGetProperty("stars", out var s) ? s.GetInt32() : 0;
                double score = item.TryGetProperty("score", out var sc) ? sc.GetDouble() : 0;

                sb.AppendLine($"[{index}] {name} (by {author})");
                sb.AppendLine($"    ⭐ {stars}  |  Score: {score:F2}");
                sb.AppendLine($"    {desc}");
                sb.AppendLine($"    🔗 {url}");
                sb.AppendLine();
                index++;
            }

            if (sb.Length == 0)
            {
                return "無符合條件的結果。";
            }

            return sb.ToString();
        }
        catch (JsonException)
        {
            return $"無法解析 API 回應格式:\r\n{json}";
        }
    }
}
