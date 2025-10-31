using Lowest_Checker.stage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Timers;
using System.Windows.Forms;

namespace Lowest_Checker.Microsoft
{
    internal class LoadProxies
    { 
    
        private static HashSet<string> Microsoft_apis = new HashSet<string>();
        public static HashSet<string> Microsoft_Proxies = new HashSet<string>();
        private static HashSet<string> Microsoft_Proxies2 = new HashSet<string>();
        private static System.Timers.Timer timer;

        public static void readProxies()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string[] fileContent = File.ReadAllLines(filePath);
                    foreach (string line in fileContent)
                    {
                        if (line.Contains(':'))
                        {
                            string[] l = line.Split(':');
                            if (l.Length == 2)
                            {
                                Microsoft_Proxies.Add(line);
                            }
                        }

                    }
                    Checker.proxies = new ConcurrentQueue<string>(Microsoft_Proxies);
                }
                
                catch (Exception ex)
                {
                    Console.WriteLine("读取文件时发生错误：" + ex.Message);
                }
            }
        }
        public static void readApis()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string[] fileContent = File.ReadAllLines(filePath);
                    foreach(string line in fileContent)
                    {
                        Microsoft_apis.Add(line);
                    }
                    readProxiesFromApis();
                    Microsoft_Proxies = Microsoft_Proxies2;
                    Checker.proxies = new ConcurrentQueue<string>(Microsoft_Proxies);
                    timer = new System.Timers.Timer(Checker.Microsoft_UpdateProxiesTime*1000);
                    timer.Elapsed += OnTimedEvent;
                    timer.AutoReset = true;
                    timer.Enabled = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("读取文件时发生错误：" + ex.Message);
                }
            }
        }
        private  static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            readProxiesFromApis();
            while(Microsoft_Proxies2.Count == 0)
            {
            }
            Microsoft_Proxies = Microsoft_Proxies2;
            Checker.proxies = new ConcurrentQueue<string>(Microsoft_Proxies);
        }
        private static void readProxiesFromApis()
        {
            Microsoft_Proxies2.Clear();
            foreach (string line in Microsoft_apis)
            {
                try
                {
                    using (WebClient web = new WebClient())
                    {
                        string s = web.DownloadString(new Uri(line));
                        string[] strings = s.Split('\n');
                        foreach (string line1 in strings)
                        {
                            Microsoft_Proxies2.Add(line1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
        }


    }
}
