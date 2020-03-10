namespace KP2faChecker.Forms
{
    partial class KP2faC_SearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KP2faC_SearchForm));
            this.tb_SearchQuery = new System.Windows.Forms.TextBox();
            this.lv_Websites = new KeePass.UI.CustomListViewEx();
            this.lbl_SearchQuery = new System.Windows.Forms.Label();
            this.llbl_Donate = new System.Windows.Forms.LinkLabel();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Reload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_SearchQuery
            // 
            this.tb_SearchQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_SearchQuery.Location = new System.Drawing.Point(173, 13);
            this.tb_SearchQuery.Name = "tb_SearchQuery";
            this.tb_SearchQuery.Size = new System.Drawing.Size(304, 20);
            this.tb_SearchQuery.TabIndex = 0;
            this.tb_SearchQuery.TextChanged += new System.EventHandler(this.tb_SearchQuery_TextChanged);
            // 
            // lv_Websites
            // 
            this.lv_Websites.AllowColumnReorder = true;
            this.lv_Websites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_Websites.FullRowSelect = true;
            this.lv_Websites.HideSelection = false;
            this.lv_Websites.Location = new System.Drawing.Point(6, 39);
            this.lv_Websites.MultiSelect = false;
            this.lv_Websites.Name = "lv_Websites";
            this.lv_Websites.ShowItemToolTips = true;
            this.lv_Websites.Size = new System.Drawing.Size(471, 123);
            this.lv_Websites.TabIndex = 1;
            this.lv_Websites.UseCompatibleStateImageBehavior = false;
            this.lv_Websites.View = System.Windows.Forms.View.Details;
            this.lv_Websites.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lv_Websites_ColumnClick);
            this.lv_Websites.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_Websites_MouseDoubleClick);
            // 
            // lbl_SearchQuery
            // 
            this.lbl_SearchQuery.AutoSize = true;
            this.lbl_SearchQuery.Location = new System.Drawing.Point(6, 16);
            this.lbl_SearchQuery.Name = "lbl_SearchQuery";
            this.lbl_SearchQuery.Size = new System.Drawing.Size(161, 13);
            this.lbl_SearchQuery.TabIndex = 2;
            this.lbl_SearchQuery.Text = "Search query (Domain or Name):";
            // 
            // llbl_Donate
            // 
            this.llbl_Donate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llbl_Donate.AutoSize = true;
            this.llbl_Donate.Location = new System.Drawing.Point(366, 247);
            this.llbl_Donate.Name = "llbl_Donate";
            this.llbl_Donate.Size = new System.Drawing.Size(42, 13);
            this.llbl_Donate.TabIndex = 6;
            this.llbl_Donate.TabStop = true;
            this.llbl_Donate.Text = "Donate";
            this.llbl_Donate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl_Donate_LinkClicked);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Location = new System.Drawing.Point(414, 242);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 5;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(339, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "Double Click the website to open the documentation or get information.\r\nHover the" +
    " website to get additional information and exceptions.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 50);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tb_SearchQuery);
            this.groupBox2.Controls.Add(this.lv_Websites);
            this.groupBox2.Controls.Add(this.lbl_SearchQuery);
            this.groupBox2.Location = new System.Drawing.Point(12, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 168);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Search";
            // 
            // btn_Reload
            // 
            this.btn_Reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Reload.Location = new System.Drawing.Point(12, 242);
            this.btn_Reload.Name = "btn_Reload";
            this.btn_Reload.Size = new System.Drawing.Size(75, 23);
            this.btn_Reload.TabIndex = 11;
            this.btn_Reload.Text = "Reload";
            this.btn_Reload.UseVisualStyleBackColor = true;
            this.btn_Reload.Click += new System.EventHandler(this.btn_Reload_Click);
            // 
            // KP2faC_SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 275);
            this.Controls.Add(this.btn_Reload);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.llbl_Donate);
            this.Controls.Add(this.btn_Close);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(523, 1000);
            this.MinimumSize = new System.Drawing.Size(523, 314);
            this.Name = "KP2faC_SearchForm";
            this.Text = "KP2faChecker - Search Websites";
            this.Load += new System.EventHandler(this.KP2faC_SearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_SearchQuery;
        private KeePass.UI.CustomListViewEx lv_Websites;
        private System.Windows.Forms.Label lbl_SearchQuery;
        private System.Windows.Forms.LinkLabel llbl_Donate;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Reload;
    }
}