# SkillSmpQuery 查詢工具

SkillSmpQuery 是一個專為 skillsmp.com API 開發的桌面端客戶端，旨在提供流暢且高效的技能資料查詢體驗。

## 專案簡介
本工具透過 .NET 9.0 構建，支援多種搜尋模式並整合 Windows 系統安全儲存，讓使用者能快速檢索 skillsmp.com 上的技能資訊。

## 主要功能
- 支援關鍵字搜尋與 AI 語意搜尋模式
- 安全的 API Key 儲存（採用 Windows DPAPI 加密）
- 搜尋歷史紀錄與下拉式選單快選
- 支援取消進行中的請求
- 搜尋結果連結可直接點擊導覽

## 系統需求
- .NET 9.0 執行階段或 SDK

## 編譯與執行

### 編譯專案
請在專案根目錄執行以下指令：
```bash
dotnet build
```

### 執行程式
```bash
dotnet run
```

## 使用指南
1. 啟動程式後，於 API Key 欄位輸入您的 skillsmp.com API 金鑰。
2. 在搜尋框輸入查詢內容。
3. 選擇所需的搜尋模式（關鍵字或 AI 語意）。
4. 點擊「Search」按鈕開始查詢。
5. 點擊搜尋結果中的連結可開啟瀏覽器查看詳細資訊。

## 組態配置
程式設定檔案（包含加密後的 API Key 及歷史紀錄）存放於以下路徑：
`%APPDATA%\SkillSmpQuery\settings.json`

## 授權條款
[License Placeholder]
