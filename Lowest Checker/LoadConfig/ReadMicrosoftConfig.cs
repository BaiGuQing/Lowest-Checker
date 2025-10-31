using Lowest_Checker.Microsoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lowest_Checker.LoadConfig
{
    internal class ReadMicrosoftConfig
    {
        public static void read()
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Configs"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Configs");
            }
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Configs/MicrosoftConfig.ini"))
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/Configs/MicrosoftConfig.ini", "MinThreads:180\nMaxThreads:180\nProxies Type(http/socks4/socks5):socks4\n" +
                    "Save 2FAs(T/F):T\nSave Blockeds(T/F):T\nSave Lockeds(T/F):T\nUpdate Api's Proxies(s):120\nProxy Connect Timeout(ms):8000");
            }
            else
            {
                string[] s = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "/Configs/MicrosoftConfig.ini");
                foreach (string s2 in s)
                {
                    if (s2.Contains(':'))
                    {
                        string[] strings = s2.Split(':');
                        switch (strings[0])
                        {
                            case "MaxThreads":
                                Checker.Microsoft_MaxThreads = int.Parse(strings[1]);
                                break;
                            case "MinThreads":
                                Checker.Microsoft_MinThreads = int.Parse(strings[1]);
                                break;
                            case "Proxies Type(http/socks4/socks5):":
                                Checker.Microsoft_Proxies_Type = strings[1];
                                break;
                            case "Save 2FAs(T/F)":
                                if (strings[1].Equals("T"))
                                {
                                    Checker.Microsoft_Save2FAs = true;
                                }
                                break;
                            case "Update Api's Proxies(s)":
                                Checker.Microsoft_UpdateProxiesTime = int.Parse(strings[1]);
                                break;
                            case "Save Blockeds(T/F)":
                                if (strings[1].Equals("T"))
                                {
                                    Checker.Microsoft_SaveBlocked = true;
                                }
                                break;
                            case "Save Lockeds(T/F)":
                                if (strings[1].Equals("T"))
                                {
                                    Checker.Microsoft_SaveLockeds = true;
                                }
                                break;
                            case "Proxy Connect Timeout(ms)":
                                Checker.Microsoft_ProxyTimeout = int.Parse(strings[1]);
                                break;
                        }
                    }
                }
            }
        }
    }
}
