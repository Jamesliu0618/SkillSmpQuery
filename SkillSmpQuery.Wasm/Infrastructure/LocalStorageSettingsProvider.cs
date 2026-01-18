using Microsoft.JSInterop;
using System.Text.Json;

namespace SkillSmpQuery.Wasm.Infrastructure;

/// <summary> 使用 Browser LocalStorage 的設定儲存提供者。 </summary>
public sealed class LocalStorageSettingsProvider : ISettingsProvider
{
    private readonly IJSRuntime _jsRuntime;
    private const string ApiKeyKey = "skillsmp_apikey";
    private const string HistoryKey = "skillsmp_history";
    private const int MaxSearchHistory = 20;

    public LocalStorageSettingsProvider(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> GetApiKeyAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", ApiKeyKey) ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public async Task SetApiKeyAsync(string apiKey)
    {
        try
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", ApiKeyKey);
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", ApiKeyKey, apiKey);
            }
        }
        catch
        {
            // 儲存失敗時靜默處理
        }
    }

    public async Task<IReadOnlyList<string>> GetSearchHistoryAsync()
    {
        try
        {
            var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", HistoryKey);
            if (string.IsNullOrEmpty(json))
            {
                return [];
            }

            return JsonSerializer.Deserialize<List<string>>(json) ?? [];
        }
        catch
        {
            return [];
        }
    }

    public async Task AddSearchHistoryAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return;

        try
        {
            var history = (await GetSearchHistoryAsync()).ToList();
            
            history.Remove(query);
            history.Insert(0, query);

            if (history.Count > MaxSearchHistory)
            {
                history = history.Take(MaxSearchHistory).ToList();
            }

            var json = JsonSerializer.Serialize(history);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", HistoryKey, json);
        }
        catch
        {
            // 儲存失敗時靜默處理
        }
    }
}
