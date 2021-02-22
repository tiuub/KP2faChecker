using KeePass.Plugins;
using KeePass.UI;
using KeePassLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KP2faChecker
{
    public class KP2faCheckerExt : Plugin
    {
        private KP2faColumnProv m_prov = null;
        private ToolStripMenuItem m_MainMenuItem;
        private ToolStripMenuItem m_EntryContextCheckItem;
        public static IPluginHost m_host = null;
        public static Dictionary<string, object> dictKp2fac_WebsiteByDomain = new Dictionary<string, object>();
        private static KP2faC_Config m_config = null;
        private static bool bRetrievingJson = false;

        public static KP2faC_Config getKP2faC_Config() { return m_config; }

        public static string _PluginName = "KP2faChecker";

        public override bool Initialize(IPluginHost host)
        {
            if (host == null) return false;
            m_host = host;

            m_prov = new KP2faColumnProv();
            m_host.ColumnProviderPool.Add(m_prov);

            m_MainMenuItem = new ToolStripMenuItem(_PluginName + " - Search Websites");
            m_MainMenuItem.Click += MainMenuItem_OnClick;
            m_MainMenuItem.Image = Properties.Resources.KP2faC_Icon;
            m_host.MainWindow.ToolsMenu.DropDownItems.Add(m_MainMenuItem);

            m_EntryContextCheckItem = new ToolStripMenuItem("Check for 2fa support");
            m_EntryContextCheckItem.Click += EntryContextCheckItem_OnClick;
            m_EntryContextCheckItem.Image = Properties.Resources.KP2faC_Icon;
            m_host.MainWindow.EntryContextMenu.Items.Insert(m_host.MainWindow.EntryContextMenu.Items.Count, m_EntryContextCheckItem);
            ToolStripSeparator m_EntryContextCheckSeparator = new ToolStripSeparator();
            m_host.MainWindow.EntryContextMenu.Items.Insert(m_host.MainWindow.EntryContextMenu.Items.IndexOf(m_EntryContextCheckItem), m_EntryContextCheckSeparator);

            m_config = new KP2faC_Config(m_host);

            init(true);

            return true;
        }
        public override void Terminate()
        {
            if (m_host == null) return;

            m_host.ColumnProviderPool.Remove(m_prov);
            m_prov = null;

            m_host = null;
        }

        public static async void init(bool refresh = false)
        {
            bRetrievingJson = true;
            KP2faC_FileloaderAsync fileloader = new KP2faC_FileloaderAsync(m_config);
            string json = await fileloader.GetJsonAsync();

            if (!json.Equals("KP2faC API error") && !json.StartsWith("null"))
            {
                List<KP2faC_Website> websites = JsonConvert.DeserializeObject<List<KP2faC_Website>>(json);
                foreach (KP2faC_Website website in websites)
                {
                    if (website.alternatives != null)
                    {
                        foreach (string domain in website.alternatives)
                        {
                            string domainName = prettifyUrl(website.url, prettifyMode.AllWithoutTld);

                            string[] domainArray = domain.Split('.');
                            domainArray = domainArray.Reverse().ToArray();
                            var curDict = dictKp2fac_WebsiteByDomain;
                            int i = 0;
                            for (i = 0; i < domainArray.Length - 1; i++)
                            {
                                if (!curDict.Keys.Contains(domainArray[i]))
                                {
                                    curDict.Add(domainArray[i], new Dictionary<string, object>());
                                }
                                
                                curDict = (Dictionary<string, object>)curDict[domainArray[i]];
                            }
                            if (!curDict.ContainsKey(domainArray[i]))
                                curDict.Add(domainArray[i], website);
                        }
                    }
                }
            }
            else
            {
                await Task.Run(() =>
                {
                    MessageBox.Show("KP2faChecker cannot connect to the API.", "KP2faChecker - Connection error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                });
            }
            if (refresh)
                m_host.MainWindow.RefreshEntriesList();
            bRetrievingJson = false;
        }

        public sealed class KP2faColumnProv : ColumnProvider
        {
            private const string KP2faColumnName = "2FA Support";

            public override string[] ColumnNames
            {
                get { return new string[] { KP2faColumnName }; }
            }

            public override string GetCellData(string strColumnName, PwEntry pe)
            {
                string r = "";

                if ((pe.Strings.Exists("otp") && pe.Strings.Get("otp") != null) ||
                    (pe.Strings.Exists("TOTP Seed") && pe.Strings.Get("TOTP Seed") != null))
                    r = "Already set";
                else
                {
                    string url = pe.Strings.ReadSafe(PwDefs.UrlField);
                    if (String.IsNullOrEmpty(url))
                        r = "URL empty";
                    else
                    {
                        if (dictKp2fac_WebsiteByDomain.Count > 0)
                        {
                            r = "Unknown";
                            string prettifiedUrl = prettifyUrl(url, prettifyMode.AllWithoutTld);
                            string[] domainArray = prettifiedUrl.Split('.').Reverse().ToArray();

                            var curDict = dictKp2fac_WebsiteByDomain;
                            for (int i = 0; i < domainArray.Length; i++)
                            {
                                if (curDict.ContainsKey(domainArray[i]))
                                {
                                    if (curDict[domainArray[i]] is KP2faC_Website)
                                    {
                                        if (((KP2faC_Website)curDict[domainArray[i]]).is2faPosssible())
                                        {
                                            if (i == domainArray.Length - 1)
                                                r = "Yes";
                                            else
                                                r = "Maybe";
                                        }
                                        else
                                        {
                                                r = "No";
                                        }

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
                                                    r = "Yes";
                                                }
                                                else
                                                {
                                                    r = "No";
                                                }
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
                                            r = "Yes";
                                        }
                                        else
                                        {
                                            r = "No";
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        else if (bRetrievingJson)
                        {
                            r = "Loading";
                        }
                        else
                        {
                            r = "API Error";
                        }
                    }
                }

                return r;
            }

            public override bool SupportsCellAction(string strColumnName)
            {
                return (strColumnName == KP2faColumnName);
            }

            public override void PerformCellAction(string strColumnName, PwEntry pe)
            {
                if ((strColumnName == KP2faColumnName) && (pe != null))
                {
                    Forms.KP2faC_EntrieViewForm e2f = new Forms.KP2faC_EntrieViewForm(m_host);
                    e2f.InitEx(pe);
                    e2f.Show();
                }
            }
        }

        [Flags]
        public enum prettifyMode : short
        {
            none = 0,
            protocol = 1 << 0,
            www = 1 << 1,
            co = 1 << 2,
            tld = 1 << 3 | co,
            All = protocol | www | tld,
            AllWithoutTld = protocol | www,
        }

        public static string prettifyUrl(string url, prettifyMode mode = prettifyMode.protocol)
        {
            if ((mode & prettifyMode.protocol) == prettifyMode.protocol)
            {
                url = url.Replace("https://", "");
                url = url.Replace("http://", "");
                url = url.Split('/')[0];
            }
            if ((mode & prettifyMode.www) == prettifyMode.www)
                url = url.Replace("www.", "");
            if ((mode & prettifyMode.co) == prettifyMode.co)
                url = url.Replace(".co.", ".");
            if ((mode & prettifyMode.tld) == prettifyMode.tld && url.Contains("."))
                url = url.Substring(0, url.LastIndexOf("."));
            return url;
        }

        private void MainMenuItem_OnClick(object sender, EventArgs e)
        {
            Forms.KP2faC_SearchForm sf = new Forms.KP2faC_SearchForm(m_host);
            sf.InitExAsync();
            sf.ShowDialog();
        }

        private void EntryContextCheckItem_OnClick(object sender, EventArgs e)
        {
            if (m_host.MainWindow.GetSelectedEntriesCount() == 1)
            {
                Forms.KP2faC_EntrieViewForm evf = new Forms.KP2faC_EntrieViewForm(m_host);
                evf.InitEx(m_host.MainWindow.GetSelectedEntry(true));
                evf.ShowDialog();
            }
        }

        public override string UpdateUrl
        {
            get
            {
                return "https://raw.githubusercontent.com/tiuub/KP2faChecker/master/VERSION";
            }
        }
    }
}
