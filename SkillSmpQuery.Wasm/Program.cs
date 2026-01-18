using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SkillSmpQuery.Wasm;
using SkillSmpQuery.Wasm.Infrastructure;
using SkillSmpQuery.Wasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient 設定：這裡的 BaseAddress 是網站自己的 URL (用於載入靜態資源)
// 但 SkillSmpService 內部有寫死 API URL，所以這裡只需要一個基本的 HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register application services
builder.Services.AddScoped<ISkillSmpService, SkillSmpService>();
builder.Services.AddScoped<ISettingsProvider, LocalStorageSettingsProvider>();

await builder.Build().RunAsync();
