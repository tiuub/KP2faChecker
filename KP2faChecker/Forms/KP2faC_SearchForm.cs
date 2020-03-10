using KeePass.Plugins;
using KeePass.UI;
using KeePassLib;
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

        public KP2faC_SearchForm(IPluginHost m_host)
        {
            InitializeComponent();
            this.m_host = m_host;
        }

        public void InitEx(Dictionary<string, List<KP2faC_Website>> dictKP2faC_WebsiteByUrl)
        {
            foreach (List<KP2faC_Website> tmpListKP2faC_Website in dictKP2faC_WebsiteByUrl.Values)
            {
                listKP2faC_Website = listKP2faC_Website.Union(tmpListKP2faC_Website).ToList();
            }
            buildListView();
        }

        private void buildListView()
        {
            lv_Websites.Clear();
            lv_Websites.Columns.Add("Name", 120);
            lv_Websites.Columns.Add("Is 2fa supported?", 330);
            ListSorter lsListSorter = new ListSorter(1, SortOrder.Ascending, true, false);
            lv_Websites.ListViewItemSorter = lsListSorter;
            if(m_host != null)
            {
                lv_Websites.SmallImageList = m_host.MainWindow.ClientIcons;
            }
        }

        private void tb_SearchQuery_TextChanged(object sender, EventArgs e)
        {
            doSearch();
        }

        private void doSearch()
        {
            if (listKP2faC_Website.Count > 0)
            {
                lv_Websites.Items.Clear();
                string sSearchQuery = tb_SearchQuery.Text;

                foreach (KP2faC_Website website in listKP2faC_Website)
                {
                    if (compareText(website.name, sSearchQuery) || compareText(website.url, sSearchQuery))
                        addItemToListView(website);
                    else if (website.alternatives != null)
                    {
                        foreach (string domain in website.alternatives)
                        {
                            if (compareText(domain, sSearchQuery))
                            {
                                addItemToListView(website);
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                ListViewItem lvi = lv_Websites.Items.Add("API Error!");
            }
        }

        private bool compareText(string text1, string text2)
        {
            if (text1.IndexOf(text2, StringComparison.OrdinalIgnoreCase) >= 0) return true;
            return false;
        }

        private void addItemToListView(KP2faC_Website website)
        {
            ListViewItem lvi = lv_Websites.Items.Add(website.name);
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
        }

        private void KP2faC_SearchForm_Load(object sender, EventArgs e)
        {
            if (m_host != null)
            {
                this.Left = m_host.MainWindow.Left + 20;
                this.Top = m_host.MainWindow.Top + 20;
                pictureBox1.Image = m_host.MainWindow.ClientIcons.Images[(int)PwIcon.Info];
            }
            doSearch();
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
            KP2faCheckerExt.init(true);
            InitEx(KP2faCheckerExt.dictKP2faC_WebsiteByUrl);
            doSearch();
        }
    }
}
