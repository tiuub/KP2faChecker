using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KP2faChecker
{
    public class KP2faC_Fileloader
    {
        private KP2faC_Config m_config = null;

        public KP2faC_Fileloader() { }
        public KP2faC_Fileloader(KP2faC_Config m_config) { this.m_config = m_config; }


        public enum getJsonMode
        {
            None,
            Offline,
            Online
        }

        public string GetJson(getJsonMode mode = getJsonMode.None)
        {
            if (mode == getJsonMode.Offline || (mode != getJsonMode.Online && TestOfflineFile()))
            {
                return GetOfflineJsonFile();
            }
            else
            {
                string path = Path.Combine(Path.GetTempPath(), "KP2faCheckerWebsiteList.json");
                string json = GetOnlineJsonFile();
                if (m_config != null && !json.Equals("KP2faC API error"))
                    try
                    {
                        File.WriteAllText(path, json);
                        m_config.tempFilePath = path;
                        m_config.tempFileChecksum = CalculateFileChecksum(path);
                    }
                    catch { }
                return json;
            }
        }

        public string GetOfflineJsonFile()
        {
            try { return File.ReadAllText(m_config.tempFilePath); }
            catch { return "KP2faC API error"; }
        }

        public string GetOnlineJsonFile()
        {
            var url = "https://toasted.top/kp2fac/api/v1/get/all.php";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UserAgent = "KeePass-KP2faChecker-plugin/1.0.0";
            request.ContentType = "application/json; charset=utf-8";
            request.Timeout = 2000;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        if (TestOfflineFile(-30))
                            return GetJson(getJsonMode.Offline);
                        return "KP2faC API error";
                    }
                    var dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    return responseFromServer;
                }
            }
            catch
            {
                if (TestOfflineFile(-30))
                    return GetJson(getJsonMode.Offline);
                return "KP2faC API error";
            }
        }

        public bool TestOfflineFile(int age = -2)
        {
            if (m_config == null)
                return false;
            return TestOfflineFile(m_config.tempFilePath, age);
        }

        public bool TestOfflineFile(string path, int age = -2)
        {
            if (m_config == null)
                return false;
            if (FileLastModified(path) > DateTime.Now.AddDays(age) && m_config.tempFileChecksum != null && CalculateFileChecksum(path) == m_config.tempFileChecksum)
                return true;
            return false;
        }

        private DateTime FileLastModified(string path)
        {
            try { return File.GetLastWriteTime(path); }
            catch { return DateTime.MinValue; }
        }

        private string CalculateFileChecksum(string path) // MD5 Checksum
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(path))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch { return null; }
        }
    }
}
