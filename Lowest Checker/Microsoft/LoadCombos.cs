using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lowest_Checker.Microsoft
{
    internal class LoadCombos
    {
        public static HashSet<string> Microsoft_Combos = new HashSet<string>();
        public static bool CheckRequirements(string input)
        {
            int numRequirementsMet = 0;

            if (Regex.IsMatch(input, ".*[A-Z].*"))
            {
                numRequirementsMet++;
            }
            if (Regex.IsMatch(input, ".*[a-z].*"))
            {
                numRequirementsMet++;
            }
            if (Regex.IsMatch(input, ".*\\d.*"))
            {
                numRequirementsMet++;
            }
            if (Regex.IsMatch(input, ".*[!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>/?].*"))
            {
                numRequirementsMet++;
            }

            return numRequirementsMet >= 2;
        }
        public static void readCombo()
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
                            if (l.Length == 2 && l[1].Length >= 8 && CheckRequirements(l[1]))
                            {
                                Microsoft_Combos.Add(line);
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("读取文件时发生错误：" + ex.Message);
                }
            }

            Checker.combos = new ConcurrentQueue<string>(Microsoft_Combos);

        }
    }
}
