using System;
using System.Threading;
using System.Windows.Forms;

namespace SkillSmpQuery
{
    /// <summary>
    /// SkillSMP 查詢軟體的主要介面。
    /// </summary>
    public partial class frmMain : Form
    {
        private readonly SkillSmpClient _client = new SkillSmpClient();
        private readonly AppSettings _settings;
        private CancellationTokenSource? _cts;

        /// <summary>
        /// 初始化 frmMain 類別的新執行個體。
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
            _settings = AppSettings.Load();
            LoadSettings();
        }

        /// <summary>
        /// 從設定檔載入已儲存的資料。
        /// </summary>
        private void LoadSettings()
        {
            txtApiKey.Text = _settings.GetApiKey();
            
            // 載入搜尋歷史到 ComboBox
            cboQuery.Items.Clear();
            foreach (var query in _settings.SearchHistory)
            {
                cboQuery.Items.Add(query);
            }
        }

        /// <summary>
        /// 處理搜尋按鈕點擊事件。
        /// </summary>
        private async void btnSearch_Click(object? sender, EventArgs e)
        {
            string apiKey = txtApiKey.Text.Trim();
            string query = cboQuery.Text.Trim();

            if (string.IsNullOrEmpty(apiKey))
            {
                MessageBox.Show("請輸入 API Key。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("請輸入查詢關鍵字。", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 儲存 API Key
            _settings.SetApiKey(apiKey);
            
            // 儲存搜尋歷史
            _settings.AddSearchHistory(query);
            _settings.Save();
            
            // 更新下拉選單
            if (!cboQuery.Items.Contains(query))
            {
                cboQuery.Items.Insert(0, query);
            }

            SetUIEnabled(false);
            btnSearch.Text = "Cancel";
            btnSearch.Enabled = true;
            lblStatus.Text = "Searching...";
            txtResult.Text = "";

            _cts = new CancellationTokenSource();

            try
            {
                string result;
                if (rbKeyword.Checked)
                {
                    result = await _client.SearchAsync(apiKey, query, _cts.Token);
                }
                else
                {
                    result = await _client.AiSearchAsync(apiKey, query, _cts.Token);
                }

                txtResult.Text = result;
                lblStatus.Text = _cts.Token.IsCancellationRequested ? "Cancelled." : "Done.";
            }
            catch (OperationCanceledException)
            {
                txtResult.Text = "搜尋已取消。";
                lblStatus.Text = "Cancelled.";
            }
            catch (Exception ex)
            {
                txtResult.Text = $"Error: {ex.Message}";
                lblStatus.Text = "Failed.";
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
                SetUIEnabled(true);
                btnSearch.Text = "Search";
            }
        }

        /// <summary>
        /// 處理取消按鈕點擊事件 (搜尋中時)。
        /// </summary>
        private void btnCancel_Click()
        {
            _cts?.Cancel();
        }

        /// <summary>
        /// 設定介面控制項的啟用狀態。
        /// </summary>
        private void SetUIEnabled(bool enabled)
        {
            txtApiKey.Enabled = enabled;
            cboQuery.Enabled = enabled;
            rbKeyword.Enabled = enabled;
            rbAi.Enabled = enabled;
            
            // 搜尋中時，按鈕變成取消功能
            if (!enabled)
            {
                btnSearch.Click -= btnSearch_Click;
                btnSearch.Click += (s, e) => btnCancel_Click();
            }
            else
            {
                // 移除所有事件處理器並重新綁定
                btnSearch.Click -= btnSearch_Click;
                btnSearch.Click += btnSearch_Click;
            }
        }

        /// <summary>
        /// 處理結果區域中的超連結點擊事件。
        /// </summary>
        private void txtResult_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.LinkText))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = e.LinkText,
                    UseShellExecute = true
                });
            }
        }

        /// <summary>
        /// 表單關閉時儲存設定。
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cts?.Cancel();
            _settings.Save();
            base.OnFormClosing(e);
        }
    }
}
