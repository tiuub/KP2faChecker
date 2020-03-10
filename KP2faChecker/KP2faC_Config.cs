using KeePass.App.Configuration;
using KeePass.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP2faChecker
{
    public class KP2faC_Config
    {
        private AceCustomConfig m_config = null;
        public KP2faC_Config(IPluginHost host)
        {
            m_config = host.CustomConfig;
        }

        private const String XMLPATH_PLUGINNAME = "KP2faChecker";
        private const String XMLPATH_TEMP_FILE_PATH = XMLPATH_PLUGINNAME + ".TempFilePath";
        private const String XMLPATH_TEMP_FILE_CHECKSUM = XMLPATH_PLUGINNAME + ".TempFileChecksum";

        public string tempFilePath
        {
            get
            {
                return m_config.GetString(XMLPATH_TEMP_FILE_PATH, "");
            }
            set
            {
                m_config.SetString(XMLPATH_TEMP_FILE_PATH, value);
            }
        }
        public string tempFileChecksum
        {
            get
            {
                return m_config.GetString(XMLPATH_TEMP_FILE_CHECKSUM, "");
            }
            set
            {
                m_config.SetString(XMLPATH_TEMP_FILE_CHECKSUM, value);
            }
        }
    }
}
