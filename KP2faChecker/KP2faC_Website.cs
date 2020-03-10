using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP2faChecker
{
    public class KP2faC_Website
    {
        public string name { get; set; }
        public string url { get; set; }
        public string doc { get; set; }
        public string twitter { get; set; }
        public string facebook { get; set; }
        public string email_address { get; set; }
        public string exception { get; set; }
        public List<string> tfa { get; set; }
        public List<string> alternatives { get; set; }

        public string getTfa()
        {
            string r = "";
            if (tfa.Contains("proprietary") || tfa.Contains("totp"))
                r += "Software Token (TOTP), ";
            if (tfa.Contains("sms"))
                r += "SMS, ";
            if (tfa.Contains("phone"))
                r += "Phone Call, ";
            if (tfa.Contains("email"))
                r += "Email, ";
            if (tfa.Contains("u2f") || tfa.Contains("hardware"))
                r += "Hardware Token";
            return r;
        }

        public bool is2faPosssible()
        {
            if (this.tfa != null) return true;
            return false;
        }
    }
}
