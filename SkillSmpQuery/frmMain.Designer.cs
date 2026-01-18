namespace SkillSmpQuery
{
    partial class frmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblApiKey = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.lblQuery = new System.Windows.Forms.Label();
            this.cboQuery = new System.Windows.Forms.ComboBox();
            this.rbKeyword = new System.Windows.Forms.RadioButton();
            this.rbAi = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBoxMode = new System.Windows.Forms.GroupBox();
            this.groupBoxMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(12, 15);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(51, 12);
            this.lblApiKey.TabIndex = 0;
            this.lblApiKey.Text = "API Key:";
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(69, 12);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(303, 22);
            this.txtApiKey.TabIndex = 1;
            // 
            // lblQuery
            // 
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(12, 43);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(37, 12);
            this.lblQuery.TabIndex = 2;
            this.lblQuery.Text = "Query:";
            // 
            // cboQuery
            // 
            this.cboQuery.FormattingEnabled = true;
            this.cboQuery.Location = new System.Drawing.Point(69, 40);
            this.cboQuery.Name = "cboQuery";
            this.cboQuery.Size = new System.Drawing.Size(303, 20);
            this.cboQuery.TabIndex = 3;
            // 
            // rbKeyword
            // 
            this.rbKeyword.AutoSize = true;
            this.rbKeyword.Checked = true;
            this.rbKeyword.Location = new System.Drawing.Point(6, 21);
            this.rbKeyword.Name = "rbKeyword";
            this.rbKeyword.Size = new System.Drawing.Size(71, 16);
            this.rbKeyword.TabIndex = 4;
            this.rbKeyword.TabStop = true;
            this.rbKeyword.Text = "Keyword";
            this.rbKeyword.UseVisualStyleBackColor = true;
            // 
            // rbAi
            // 
            this.rbAi.AutoSize = true;
            this.rbAi.Location = new System.Drawing.Point(83, 21);
            this.rbAi.Name = "rbAi";
            this.rbAi.Size = new System.Drawing.Size(36, 16);
            this.rbAi.TabIndex = 5;
            this.rbAi.Text = "AI";
            this.rbAi.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(297, 68);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 45);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtResult
            // 
            this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResult.Location = new System.Drawing.Point(12, 119);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.DetectUrls = true;
            this.txtResult.Size = new System.Drawing.Size(360, 222);
            this.txtResult.TabIndex = 7;
            this.txtResult.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtResult_LinkClicked);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 344);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 12);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Ready";
            // 
            // groupBoxMode
            // 
            this.groupBoxMode.Controls.Add(this.rbKeyword);
            this.groupBoxMode.Controls.Add(this.rbAi);
            this.groupBoxMode.Location = new System.Drawing.Point(69, 68);
            this.groupBoxMode.Name = "groupBoxMode";
            this.groupBoxMode.Size = new System.Drawing.Size(222, 45);
            this.groupBoxMode.TabIndex = 9;
            this.groupBoxMode.TabStop = false;
            this.groupBoxMode.Text = "Search Mode";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 365);
            this.Controls.Add(this.groupBoxMode);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cboQuery);
            this.Controls.Add(this.lblQuery);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.lblApiKey);
            this.Name = "frmMain";
            this.Text = "SkillSMP Query v1.0";
            this.groupBoxMode.ResumeLayout(false);
            this.groupBoxMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.ComboBox cboQuery;
        private System.Windows.Forms.RadioButton rbKeyword;
        private System.Windows.Forms.RadioButton rbAi;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RichTextBox txtResult;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox groupBoxMode;
    }
}
