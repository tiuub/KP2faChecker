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
    public partial class KP2faC_EntrieViewForm : Form
    {
        private IPluginHost m_host = null;
        public KP2faC_EntrieViewForm(IPluginHost m_host)
        {
            InitializeComponent();
            this.m_host = m_host;
        }
        public void InitEx(PwEntry pe)
        {
            btn_Reload.Enabled = false;
            btn_Reload.Visible = false;

            RichTextBuilder rb = new RichTextBuilder();

            string strItemSeperator = Environment.NewLine;

            EvAppendEntryFieldProtectedString(rb, strItemSeperator, "Title", PwDefs.TitleField, pe);
            EvAppendEntryFieldProtectedString(rb, strItemSeperator, "Username", PwDefs.UserNameField, pe);
            EvAppendEntryFieldProtectedString(rb, strItemSeperator, "URL", PwDefs.UrlField, pe);

            this.Text = this.Text + " - " + pe.Strings.ReadSafe(PwDefs.TitleField);


            string r = "";
            string totpAlready = "No";
            KP2faC_Website curWebsite = null;
            bool bPossible = false;
            if ((pe.Strings.Exists("otp") && pe.Strings.Get("otp") != null) ||
                (pe.Strings.Exists("TOTP Seed") && pe.Strings.Get("TOTP Seed") != null))
                totpAlready = "Yes";
            string url = pe.Strings.ReadSafe(PwDefs.UrlField);
            pb_trafficlight.Image = Properties.Resources.trafficlight_orange;
            if (String.IsNullOrEmpty(url))
                r = "No Url set. A Url is needed to check for 2fa support";
            else
            {
                if (KP2faCheckerExt.dictKp2fac_WebsiteByDomain.Count > 0)
                {
                    string prettifiedUrl = KP2faCheckerExt.prettifyUrl(url, KP2faCheckerExt.prettifyMode.AllWithoutTld);
                    string[] domainArray = prettifiedUrl.Split('.').Reverse().ToArray();

                    var curDict = KP2faCheckerExt.dictKp2fac_WebsiteByDomain;
                    for (int i = 0; i < domainArray.Length; i++)
                    {
                        if (curDict.ContainsKey(domainArray[i]))
                        {
                            if (curDict[domainArray[i]] is KP2faC_Website)
                            {
                                if (((KP2faC_Website)curDict[domainArray[i]]).is2faPosssible())
                                {
                                    if (i == domainArray.Length - 1)
                                    {
                                        r = "Yes" + r;
                                        bPossible = true;
                                        pb_trafficlight.Image = Properties.Resources.trafficlight_green;
                                    }
                                }
                                else
                                {
                                    r = "No" + r;
                                    pb_trafficlight.Image = Properties.Resources.trafficlight_red;
                                }
                                curWebsite = (KP2faC_Website)curDict[domainArray[i]];
                            }
                            else
                            {
                                curDict = (Dictionary<string, object>)curDict[domainArray[i]];
                                if (i == domainArray.Length - 1)
                                {
                                    if (curDict.ContainsKey("*") && curDict["*"] is KP2faC_Website)
                                    {
                                        if (((KP2faC_Website)curDict["*"]).is2faPosssible())
                                        {
                                            r = "Yes" + r;
                                            bPossible = true;
                                            pb_trafficlight.Image = Properties.Resources.trafficlight_green;
                                        }
                                        else
                                        {
                                            r = "No" + r;
                                            pb_trafficlight.Image = Properties.Resources.trafficlight_red;
                                        }
                                        curWebsite = (KP2faC_Website)curDict["*"];
                                    }
                                }
                            }
                        }
                        else if (curDict.ContainsKey("*"))
                        {
                            if (curDict["*"] is KP2faC_Website)
                            {
                                if (((KP2faC_Website)curDict["*"]).is2faPosssible())
                                {
                                    r = "Yes" + r;
                                    bPossible = true;
                                    pb_trafficlight.Image = Properties.Resources.trafficlight_green;
                                }
                                else
                                {
                                    r = "No" + r;
                                    pb_trafficlight.Image = Properties.Resources.trafficlight_red;
                                }
                                curWebsite = (KP2faC_Website)curDict["*"];
                            }
                            break;
                        }
                    }
                }
                else
                {
                    r = "Unknown. API Error!";
                    btn_Reload.Enabled = true;
                    btn_Reload.Visible = true;
                    btn_Reload.Tag = pe;
                }
                if (string.IsNullOrEmpty(r))
                    r = "Dont know!";
            }
            EvAppendEntryFieldString(rb, strItemSeperator, "2FA already configured", totpAlready);
            rb.Build(rtb_Entry);

            rb = new RichTextBuilder();
            EvAppendEntryFieldString(rb, strItemSeperator, "2FA supported", r);
            if (bPossible && curWebsite != null)
            {
                rb.AppendLine();
                EvAppendEntryFieldString(rb, strItemSeperator, "Websitename", curWebsite.name);
                EvAppendEntryFieldString(rb, strItemSeperator, "2FA Options", curWebsite.getTfa());
                EvAppendEntryFieldString(rb, strItemSeperator, "Documentation", curWebsite.doc);
                if (!String.IsNullOrEmpty(curWebsite.exception))
                    EvAppendEntryFieldString(rb, strItemSeperator, "Exception", curWebsite.exception);
            }
            else if (!bPossible && curWebsite != null)
            {
                EvAppendEntryFieldString(rb, strItemSeperator, "This is wrong?", "Give a hint! https://toasted.top/kp2fac/");
                rb.AppendLine();
                EvAppendEntryFieldString(rb, strItemSeperator, "Tell them to support 2FA!", " ");
                if (!String.IsNullOrEmpty(curWebsite.email_address))
                    EvAppendEntryFieldString(rb, strItemSeperator, "Email address", curWebsite.email_address);
                if (!String.IsNullOrEmpty(curWebsite.facebook))
                    EvAppendEntryFieldString(rb, strItemSeperator, "Facebook", curWebsite.facebook);
                if (!String.IsNullOrEmpty(curWebsite.twitter))
                    EvAppendEntryFieldString(rb, strItemSeperator, "Twitter", curWebsite.twitter);
            }
            else
            {
                rb.AppendLine();
                EvAppendEntryFieldString(rb, strItemSeperator, "Is 2fa supported?", "Give a hint! https://toasted.top/kp2fac/");
            }

            rb.Build(rtb_2FA);
        }


        private static void EvAppendEntryFieldProtectedString(RichTextBuilder rb,
                string strItemSeparator, string strName, string strKeyProtectedString, PwEntry pe)
        {

            if (pe.Strings.Get(strKeyProtectedString).IsProtected)
            {
                EvAppendEntryFieldString(rb, strItemSeparator, strName, PwDefs.HiddenPassword);
            }
            else
            {
                EvAppendEntryFieldString(
                   rb, strItemSeparator, strName,
                   pe.Strings.Get(strKeyProtectedString).ReadString());
            }
        }

        private static void EvAppendEntryFieldString(RichTextBuilder rb,
            string strItemSeparator, string strName, string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return;
            rb.Append(strName, System.Drawing.FontStyle.Bold, null, null, ":", " ");
            rb.Append(strValue);
            rb.Append(strItemSeparator);
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llbl_Donate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=5F5QB7744AD5G&source=url");
            llbl_Donate.LinkVisited = true;
        }

        private void pb_trafficlight_Click(object sender, EventArgs e)
        {

        }

        private void KP2faC_EntrieViewForm_Load(object sender, EventArgs e)
        {
            if (m_host != null)
            {
                this.Left = m_host.MainWindow.Left + 20;
                this.Top = m_host.MainWindow.Top + 20;
            }
        }

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            KP2faCheckerExt.init(true);
            if(btn_Reload.Tag is PwEntry)
            {
                InitEx((PwEntry)btn_Reload.Tag);
            }
        }
    }
}
