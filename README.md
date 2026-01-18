# SkillSMP Query

SkillSMP Query 是一個專為 skillsmp.com API 開發的網頁應用程式，提供流暢且高效的技能資料查詢體驗。

## Demo

部署完成後可在以下位置存取：
**https://jamesliu0618.github.io/SkillSmpQuery/**

## 主要功能

- 關鍵字搜尋與 AI 語意搜尋模式
- API Key 安全儲存（瀏覽器 LocalStorage）
- 搜尋歷史紀錄與下拉式選單快選
- 支援取消進行中的請求
- 搜尋結果卡片式呈現，連結可直接點擊

## 技術架構

| 項目 | 說明 |
|------|------|
| **框架** | Blazor WebAssembly (.NET 10) |
| **UI** | Bootstrap 5 |
| **部署** | GitHub Pages (靜態託管) |
| **CI/CD** | GitHub Actions |

## 專案結構

```
SkillSmpQuery.Wasm/
├── Models/           # 資料模型 (SkillInfo, SearchResponse...)
├── Services/         # 業務邏輯與 API 呼叫
├── Infrastructure/   # 基礎設施 (LocalStorage 設定儲存)
├── Pages/            # Blazor 頁面元件
├── Shared/           # 共用 UI 元件
└── wwwroot/          # 靜態資源
```

## 本地開發

### 系統需求
- .NET 10 SDK

### 編譯專案
```bash
cd SkillSmpQuery.Wasm
dotnet build
```

### 執行開發伺服器
```bash
dotnet run
```
開啟瀏覽器前往 `http://localhost:5000`

### 發布專案
```bash
dotnet publish -c Release -o release
```

## 使用指南

1. 開啟網頁應用程式
2. 於 **API Key** 欄位輸入您的 skillsmp.com API 金鑰
3. 在搜尋框輸入查詢內容
4. 選擇搜尋模式（Keyword Search / AI Search）
5. 點擊 **Search** 按鈕開始查詢
6. 點擊結果卡片中的 **View Skill** 按鈕查看詳細資訊

## 部署說明

本專案已設定 GitHub Actions 自動部署：

1. 推送到 `main` 分支會自動觸發建置
2. 建置成功後會自動部署到 `gh-pages` 分支
3. GitHub Pages 會從 `gh-pages` 分支提供靜態網站

### 首次部署設定

1. 到 GitHub Repo → **Settings** → **Actions** → **General**
2. 在 **Workflow permissions** 選擇 **Read and write permissions**
3. 儲存後重新執行 workflow

## 注意事項

### CORS 限制

由於 Blazor WebAssembly 是純前端應用，所有 API 請求都是從瀏覽器發出。如果 `skillsmp.com` 的 API 不支援 CORS（跨來源資源共用），搜尋功能可能無法正常運作。

如遇到 CORS 問題，可考慮：
- 使用 CORS Proxy（開發測試用）
- 建立後端代理服務
- 聯繫 API 提供者啟用 CORS

## 授權條款

MIT License
