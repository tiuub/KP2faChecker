using KeePass.Plugins;
using KeePass.UI;
using KeePassLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KP2faChecker.Forms
{
    public partial class KP2faC_SearchForm : Form
    {
        private List<KP2faC_Website> listKP2faC_Website = new List<KP2faC_Website>();
        private IPluginHost m_host = null;
        private bool bRetrievingJson = false;
        private bool bSearching = false;

        public KP2faC_SearchForm(IPluginHost m_host)
        {
            InitializeComponent();
            this.m_host = m_host;
        }

        public async void InitExAsync()
        {
            buildListView();
            bRetrievingJson = true;
            doSearchAsync();

            startAnimation("Loading");
            btn_Search.Enabled = false;
            btn_Reload.Enabled = false;

            KP2faC_FileloaderAsync fileloader = new KP2faC_FileloaderAsync(KP2faCheckerExt.getKP2faC_Config());
            string json = await fileloader.GetJsonAsync();
            stopAnimation();

            if (!json.Equals("KP2faC API error"))
            {
                listKP2faC_Website = JsonConvert.DeserializeObject<List<KP2faC_Website>>(json);
                doSearchAsync();
            }
            else
            {
                btn_Search.Enabled = true;
                btn_Reload.Enabled = true;
            }
            bRetrievingJson = false;
        }

        private void buildListView()
        {
            lv_Websites.Clear();
            lv_Websites.Columns.Add("Name", 120);
            lv_Websites.Columns.Add("Is 2fa supported?", 330);
            ListSorter lsListSorter = new ListSorter(0, SortOrder.Ascending, true, false);
            lv_Websites.ListViewItemSorter = lsListSorter;
            if(m_host != null)
            {
                lv_Websites.SmallImageList = m_host.MainWindow.ClientIcons;
            }
        }

        private async void doSearchAsync()
        {
            int c = 0;
            startAnimation("Searching");
            bSearching = true;
            btn_Search.Enabled = false;
            btn_Reload.Enabled = false;

            lv_Websites.BeginUpdate();
            ListSorter ls = (ListSorter)lv_Websites.ListViewItemSorter;
            lv_Websites.ListViewItemSorter = null;
            lv_Websites.Items.Clear();
            if (listKP2faC_Website.Count > 0)
            {
                string sSearchQuery = tb_SearchQuery.Text;
                await Task.Run(() =>
                {
                    foreach (KP2faC_Website website in listKP2faC_Website)
                    {
                        if (string.IsNullOrWhiteSpace(sSearchQuery) || compareText(website.name, sSearchQuery) || compareText(website.url, sSearchQuery))
                        {
                            addWebsiteToListView(website);
                            c++;
                        }
                        else if (website.alternatives != null)
                        {
                            foreach (string domain in website.alternatives)
                            {
                                if (compareText(domain, sSearchQuery))
                                {
                                    addWebsiteToListView(website);
                                    c++;
                                    break;
                                }
                            }
                        }
                    }
                });
            }
            else if (bRetrievingJson)
            {
                ListViewItem lvi = lv_Websites.Items.Add("Loading websites...");
                lvi.ImageIndex = (int)PwIcon.NetworkServer;
                lvi.ToolTipText = "The plugin is currently downloading the json file from the server or reading it from your pc!";
                lvi.Font = new Font(lvi.Font, FontStyle.Italic);
            }
            else
            {
                ListViewItem lvi = lv_Websites.Items.Add("API Error!");
                lvi.ImageIndex = (int)PwIcon.Warning;
                lvi.ToolTipText = "There happend an error! Try to connect your pc to the internet and press Reload!";
            }

            btn_Reload.Enabled = true;
            btn_Search.Enabled = true;
            bSearching = false;
            lv_Websites.ListViewItemSorter = ls;
            lv_Websites.Sort();
            lv_Websites.EndUpdate();
            stopAnimation();
            setStateText("Found " + c + " entrie(s)!");
        }

        private bool compareText(string text1, string text2)
        {
            if (text1.IndexOf(text2, StringComparison.OrdinalIgnoreCase) >= 0) return true;
            return false;
        }

        private void addWebsiteToListView(KP2faC_Website website)
        {
            ListViewItem lvi = new ListViewItem(website.name);
            if (website.is2faPosssible())
            {
                lvi.BackColor = Color.LightGreen;
                lvi.SubItems.Add("Yes - " + website.getTfa());
                lvi.ImageIndex = (int)PwIcon.Checked;
                lvi.ToolTipText = "URL: " + website.url;

                if (!string.IsNullOrEmpty(website.exception))
                {
                    lvi.ImageIndex = (int)PwIcon.Warning;
                    lvi.ToolTipText = lvi.ToolTipText + "\nException: " + website.exception;
                }
            }
            else
            {
                lvi.BackColor = Color.LightSalmon;
                lvi.SubItems.Add("No");
                lvi.ImageIndex = (int)PwIcon.Expired;
                lvi.ToolTipText = "Tell them to support 2fa:\n (Double click)";
            }
            lvi.Tag = website;
            lv_Websites.Items.Add(lvi);
        }

        private void KP2faC_SearchForm_Load(object sender, EventArgs e)
        {
            if (m_host != null)
            {
                this.Left = m_host.MainWindow.Left + 20;
                this.Top = m_host.MainWindow.Top + 20;
                pictureBox1.Image = m_host.MainWindow.ClientIcons.Images[(int)PwIcon.Info];
            }
        }

        private void lv_Websites_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var senderList = (ListView)sender;
            var item = senderList.HitTest(e.Location).Item;
            if (item != null)
            {
                KP2faC_Website website = ((KP2faC_Website)item.Tag);
                if (website.is2faPosssible())
                {
                    if (website.doc != null) System.Diagnostics.Process.Start(website.doc);
                    else MessageBox.Show("Sorry, I cant help you. No Documentation found!", "KP2faChecker - " + website.name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (!string.IsNullOrEmpty(website.email_address))
                    {
                        DialogResult dialogResult = MessageBox.Show(website.name + " is not supporting 2fa.\n\nTell them to support 2fa on:\n    Twitter: " + website.twitter + "\n    Facebook: " + website.facebook + "\n    Email: " + website.email_address + "\n\nDo you want to send an email to " + website.name + "?", "KP2faChecker - " + website.name, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if(dialogResult == DialogResult.Yes)
                            System.Diagnostics.Process.Start(Uri.EscapeUriString(string.Format("mailto:{0}?Subject={1}&Body={2}",website.email_address, "Support Two Factor Authentication", "Security is important. We'd like it if you supported two factor auth.\r\n")));
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show(website.name + " is not supporting 2fa.\n\nTell them to support 2fa on:\n    Twitter: " + website.twitter + "\n    Facebook: " + website.facebook, "KP2faChecker - " + website.name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    
                }
                
            }
        }

        private void lv_Websites_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // arrows for showing sort
            const string ascArrow = "▲ ";
            const string descArrow = "▼ ";

            CustomListViewEx lv = (CustomListViewEx)sender;
            ListSorter sorter = (ListSorter)lv.ListViewItemSorter;
            ColumnHeader head = lv.Columns[sorter.Column];

            // remove arrow
            if (head.Text.StartsWith(ascArrow) || head.Text.StartsWith(descArrow))
                head.Text = head.Text.Substring(2, head.Text.Length - 2);

            head = lv.Columns[e.Column];
            if (sorter.Column == e.Column)
            {
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                    head.Text = descArrow + head.Text;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                    head.Text = ascArrow + head.Text;
                }
            }
            else
            {
                sorter.Order = SortOrder.Ascending;
                sorter.Column = e.Column;
                head.Text = ascArrow + head.Text;
            }
            lv.ListViewItemSorter = sorter;
            lv.Sort();
        }

        private void llbl_Donate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5F5QB7744AD5G&source=url");
            llbl_Donate.LinkVisited = true;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            if (!bSearching && !bRetrievingJson)
                InitExAsync();
        }

        private void setStateText(string text)
        {
            lbl_State.Visible = true;
            lbl_State.Text = text;
        }

        private string sStateText;

        private void startAnimation(string text)
        {
            lbl_State.Visible = true;
            lbl_State.Text = text;
            sStateText = text;
            t_Animation.Start();
        }

        private void stopAnimation()
        {
            lbl_State.Visible = false;
            lbl_State.Text = "";
            sStateText = null;
            t_Animation.Stop();
        }

        private void t_Animation_Tick(object sender, EventArgs e)
        {
            string s = lbl_State.Text.Replace(sStateText, "");
            if (s.Length == 0 || s.Length >= 3)
                lbl_State.Text = sStateText + ".";
            else if (s.Length == 1)
                lbl_State.Text = sStateText + "..";
            else
                lbl_State.Text = sStateText + "...";
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (!bSearching && !bRetrievingJson)
                doSearchAsync();
        }

        private void tb_SearchQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (!bSearching && !bRetrievingJson)
                    doSearchAsync();
        }
    }
}
