using System.Net.Http.Headers;
using System.Text.Json;
using SkillSmpQuery.Models;

namespace SkillSmpQuery.Services;

/// <summary>
/// SkillSMP API 服務的 HTTP 實作。
/// </summary>
public sealed class SkillSmpService : ISkillSmpService, IDisposable
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://skillsmp.com/api/v1";
    private string _apiKey = string.Empty;

    public SkillSmpService()
    {
        _httpClient = new HttpClient();
    }

    public SkillSmpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public bool HasApiKey => !string.IsNullOrEmpty(_apiKey);

    public void SetApiKey(string apiKey)
    {
        _apiKey = apiKey ?? string.Empty;
    }

    public Task<SearchResponse> SearchAsync(string query, CancellationToken cancellationToken = default)
    {
        return SendSearchRequestAsync($"/skills/search?q={Uri.EscapeDataString(query)}", cancellationToken);
    }

    public Task<SearchResponse> AiSearchAsync(string query, CancellationToken cancellationToken = default)
    {
        return SendSearchRequestAsync($"/skills/ai-search?q={Uri.EscapeDataString(query)}", cancellationToken);
    }

    private async Task<SearchResponse> SendSearchRequestAsync(string endpoint, CancellationToken cancellationToken)
    {
        if (!HasApiKey)
        {
            return new SearchResponse
            {
                Success = false,
                ErrorMessage = "API Key 尚未設定。"
            };
        }

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BaseUrl + endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new SearchResponse
                {
                    Success = false,
                    ErrorMessage = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {content}"
                };
            }

            return ParseResponse(content);
        }
        catch (OperationCanceledException)
        {
            return new SearchResponse
            {
                Success = false,
                ErrorMessage = "搜尋已取消。"
            };
        }
        catch (HttpRequestException ex)
        {
            return new SearchResponse
            {
                Success = false,
                ErrorMessage = $"網路錯誤: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            return new SearchResponse
            {
                Success = false,
                ErrorMessage = $"未預期的錯誤: {ex.Message}"
            };
        }
    }

    private static SearchResponse ParseResponse(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("success", out var successProp) || !successProp.GetBoolean())
            {
                return new SearchResponse
                {
                    Success = false,
                    ErrorMessage = $"API 回應失敗: {json}"
                };
            }

            if (!root.TryGetProperty("data", out var dataProp) ||
                !dataProp.TryGetProperty("data", out var resultsProp))
            {
                return new SearchResponse { Success = true, Results = [] };
            }

            var results = new List<SkillSearchResult>();

            foreach (var item in resultsProp.EnumerateArray())
            {
                if (!item.TryGetProperty("skill", out var skill)) continue;

                var skillInfo = new SkillInfo
                {
                    Name = skill.TryGetProperty("name", out var n) ? n.GetString() ?? "" : "",
                    Author = skill.TryGetProperty("author", out var a) ? a.GetString() ?? "" : "",
                    Description = skill.TryGetProperty("description", out var d) ? d.GetString() ?? "" : "",
                    SkillUrl = skill.TryGetProperty("skillUrl", out var u) ? u.GetString() ?? "" : "",
                    Stars = skill.TryGetProperty("stars", out var s) ? s.GetInt32() : 0
                };

                var searchResult = new SkillSearchResult
                {
                    Skill = skillInfo,
                    Score = item.TryGetProperty("score", out var sc) ? sc.GetDouble() : 0
                };

                results.Add(searchResult);
            }

            return new SearchResponse { Success = true, Results = results };
        }
        catch (JsonException)
        {
            return new SearchResponse
            {
                Success = false,
                ErrorMessage = $"無法解析 API 回應: {json}"
            };
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
