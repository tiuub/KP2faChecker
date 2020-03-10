namespace KP2faChecker.Forms
{
    partial class KP2faC_EntrieViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KP2faC_EntrieViewForm));
            this.gB_Entry = new System.Windows.Forms.GroupBox();
            this.rtb_Entry = new KeePass.UI.CustomRichTextBoxEx();
            this.gB_2faOptions = new System.Windows.Forms.GroupBox();
            this.rtb_2FA = new KeePass.UI.CustomRichTextBoxEx();
            this.pb_trafficlight = new System.Windows.Forms.PictureBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.llbl_Donate = new System.Windows.Forms.LinkLabel();
            this.gB_isPossible = new System.Windows.Forms.GroupBox();
            this.btn_Reload = new System.Windows.Forms.Button();
            this.gB_Entry.SuspendLayout();
            this.gB_2faOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_trafficlight)).BeginInit();
            this.gB_isPossible.SuspendLayout();
            this.SuspendLayout();
            // 
            // gB_Entry
            // 
            this.gB_Entry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gB_Entry.Controls.Add(this.rtb_Entry);
            this.gB_Entry.Location = new System.Drawing.Point(10, 10);
            this.gB_Entry.Name = "gB_Entry";
            this.gB_Entry.Size = new System.Drawing.Size(304, 98);
            this.gB_Entry.TabIndex = 0;
            this.gB_Entry.TabStop = false;
            this.gB_Entry.Text = "Entry";
            // 
            // rtb_Entry
            // 
            this.rtb_Entry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Entry.Location = new System.Drawing.Point(8, 19);
            this.rtb_Entry.Name = "rtb_Entry";
            this.rtb_Entry.ReadOnly = true;
            this.rtb_Entry.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtb_Entry.Size = new System.Drawing.Size(291, 73);
            this.rtb_Entry.TabIndex = 1;
            this.rtb_Entry.Text = "";
            // 
            // gB_2faOptions
            // 
            this.gB_2faOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gB_2faOptions.Controls.Add(this.rtb_2FA);
            this.gB_2faOptions.Location = new System.Drawing.Point(10, 114);
            this.gB_2faOptions.Name = "gB_2faOptions";
            this.gB_2faOptions.Size = new System.Drawing.Size(425, 125);
            this.gB_2faOptions.TabIndex = 1;
            this.gB_2faOptions.TabStop = false;
            this.gB_2faOptions.Text = "2FA Options";
            // 
            // rtb_2FA
            // 
            this.rtb_2FA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_2FA.Location = new System.Drawing.Point(8, 20);
            this.rtb_2FA.Name = "rtb_2FA";
            this.rtb_2FA.ReadOnly = true;
            this.rtb_2FA.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtb_2FA.Size = new System.Drawing.Size(411, 99);
            this.rtb_2FA.TabIndex = 0;
            this.rtb_2FA.Text = "";
            // 
            // pb_trafficlight
            // 
            this.pb_trafficlight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pb_trafficlight.Location = new System.Drawing.Point(46, 19);
            this.pb_trafficlight.Name = "pb_trafficlight";
            this.pb_trafficlight.Size = new System.Drawing.Size(25, 73);
            this.pb_trafficlight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_trafficlight.TabIndex = 2;
            this.pb_trafficlight.TabStop = false;
            this.pb_trafficlight.Click += new System.EventHandler(this.pb_trafficlight_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.Location = new System.Drawing.Point(360, 245);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // llbl_Donate
            // 
            this.llbl_Donate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.llbl_Donate.AutoSize = true;
            this.llbl_Donate.Location = new System.Drawing.Point(312, 250);
            this.llbl_Donate.Name = "llbl_Donate";
            this.llbl_Donate.Size = new System.Drawing.Size(42, 13);
            this.llbl_Donate.TabIndex = 4;
            this.llbl_Donate.TabStop = true;
            this.llbl_Donate.Text = "Donate";
            this.llbl_Donate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbl_Donate_LinkClicked);
            // 
            // gB_isPossible
            // 
            this.gB_isPossible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gB_isPossible.Controls.Add(this.pb_trafficlight);
            this.gB_isPossible.Location = new System.Drawing.Point(320, 10);
            this.gB_isPossible.Name = "gB_isPossible";
            this.gB_isPossible.Size = new System.Drawing.Size(115, 98);
            this.gB_isPossible.TabIndex = 5;
            this.gB_isPossible.TabStop = false;
            this.gB_isPossible.Text = "Is 2fa supported?";
            // 
            // btn_Reload
            // 
            this.btn_Reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Reload.Location = new System.Drawing.Point(10, 245);
            this.btn_Reload.Name = "btn_Reload";
            this.btn_Reload.Size = new System.Drawing.Size(75, 23);
            this.btn_Reload.TabIndex = 6;
            this.btn_Reload.Text = "Reload";
            this.btn_Reload.UseVisualStyleBackColor = true;
            this.btn_Reload.Click += new System.EventHandler(this.btn_Reload_Click);
            // 
            // KP2faC_EntrieViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 280);
            this.Controls.Add(this.btn_Reload);
            this.Controls.Add(this.llbl_Donate);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.gB_2faOptions);
            this.Controls.Add(this.gB_Entry);
            this.Controls.Add(this.gB_isPossible);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KP2faC_EntrieViewForm";
            this.Text = "KP2faChecker";
            this.Load += new System.EventHandler(this.KP2faC_EntrieViewForm_Load);
            this.gB_Entry.ResumeLayout(false);
            this.gB_2faOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_trafficlight)).EndInit();
            this.gB_isPossible.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gB_Entry;
        private System.Windows.Forms.GroupBox gB_2faOptions;
        private KeePass.UI.CustomRichTextBoxEx rtb_2FA;
        private KeePass.UI.CustomRichTextBoxEx rtb_Entry;
        private System.Windows.Forms.PictureBox pb_trafficlight;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.LinkLabel llbl_Donate;
        private System.Windows.Forms.GroupBox gB_isPossible;
        private System.Windows.Forms.Button btn_Reload;
    }
}