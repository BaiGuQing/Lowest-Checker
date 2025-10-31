using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lowest_Checker.CheckVersion
{
    internal class CheckUpdate
    {
        public static bool check()
        {
            string data = GetPageSource();
            if (data.Contains("LowestChecker_V" + Program.version))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static string GetPageSource()
        {
            try
            {
                using (WebClient client = new WebClient())
                {

                    string pageSource = client.DownloadString("https://pastebin.com/raw/g9AngUhw");
                    return pageSource;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Contact Mader");
                return null;
            }
        }
    }
}
