using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KP2faChecker
{
    public class KP2faC_FileloaderAsync
    {
        private KP2faC_Config m_config = null;

        public KP2faC_FileloaderAsync(KP2faC_Config m_config) { this.m_config = m_config; }

        private string USER_AGENT = "KeePass-KP2faChecker-plugin/1.1.0";
        private string CONTENTTYPE = "application/json; charset=utf-8";
        private int TIMEOUT_SEC = 10;

        public enum getJsonMode
        {
            None,
            Offline,
            Online
        }

        public async Task<string> GetJsonAsync(getJsonMode mode = getJsonMode.None)
        {
            await Task.Delay(1000);
            if (mode == getJsonMode.Offline || (mode != getJsonMode.Online && await TestOfflineFileAsync()))
            {
                return await GetOfflineJsonFileAsync();
            }
            else
            {
                string path = Path.Combine(Path.GetTempPath(), "KP2faCheckerWebsiteList.json");
                string json = await GetOnlineJsonFileAsync();
                if (m_config != null && !json.Equals("KP2faC API error"))
                    try
                    {
                        using (StreamWriter writer = new StreamWriter(m_config.tempFilePath))
                        {
                            await writer.WriteAsync(json);
                        }
                        m_config.tempFilePath = path;
                        m_config.tempFileChecksum = await CalculateFileChecksumAsync(path);
                    }
                    catch { }
                return json;
            }
        }

        public async Task<string> GetOfflineJsonFileAsync()
        {
            try
            {
                using (var reader = File.OpenText(m_config.tempFilePath))
                {
                    var fileText = await reader.ReadToEndAsync();
                    return fileText;
                }
            }
            catch { return "KP2faC API error"; }
        }

        public async Task<string> GetOnlineJsonFileAsync()
        {
            var url = "https://toasted.top/kp2fac/api/v1/get/all.php";

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };

            var httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("User-Agent", USER_AGENT);
            httpClient.DefaultRequestHeaders.Add("ContentType", CONTENTTYPE);
            httpClient.Timeout = TimeSpan.FromSeconds(TIMEOUT_SEC);

            await Task.Delay(3000);
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (await TestOfflineFileAsync(-30))
                        return await GetJsonAsync(getJsonMode.Offline);
                    return "KP2faC API error";
                }
                return WebUtility.HtmlDecode(await response.Content.ReadAsStringAsync());
            }
            catch { }
            if (await TestOfflineFileAsync(-30))
                return await GetJsonAsync(getJsonMode.Offline);
            return "KP2faC API error";
        }

        public async Task<bool> TestOfflineFileAsync(int age = -2)
        {
            if (m_config == null)
                return false;
            return await TestOfflineFileAsync(m_config.tempFilePath, age);
        }

        public async Task<bool> TestOfflineFileAsync(string path, int age = -2)
        {
            if (m_config == null)
                return false;
            if (FileLastModifiedAsync(path) > DateTime.Now.AddDays(age) && m_config.tempFileChecksum != null && await CalculateFileChecksumAsync(path) == m_config.tempFileChecksum)
                return true;
            return false;
        }

        private DateTime FileLastModifiedAsync(string path)
        {
            try { return File.GetLastWriteTime(path); }
            catch { return DateTime.MinValue; }
        }

        private async Task<string> CalculateFileChecksumAsync(string path) // MD5 Checksum
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true)) // true means use IO async operations
                    {
                        byte[] buffer = new byte[4096];
                        int bytesRead;
                        do
                        {
                            bytesRead = await stream.ReadAsync(buffer, 0, 4096);
                            if (bytesRead > 0)
                            {
                                md5.TransformBlock(buffer, 0, bytesRead, null, 0);
                            }
                        } while (bytesRead > 0);

                        md5.TransformFinalBlock(buffer, 0, 0);
                        return BitConverter.ToString(md5.Hash).Replace("-", "").ToUpperInvariant();
                    }
                }
            }
            catch { return null; }
        }
    }
}
