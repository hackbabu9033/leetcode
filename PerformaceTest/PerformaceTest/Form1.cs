using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PerformaceTest
{
    public partial class Form1 : Form
    {
        //public static int maxStringLength = 0;
        //public static int curMaxStringLength;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int txtCount = 30000;
            int txtMaxLength = 1000;
            Random random = new Random();
            Random random2 = new Random();
            int txtLength;
            int charIndex;
            List<char> charList = new List<char>();
            for (char c = 'A'; c <= 'Z'; ++c)
                charList.Add(c);

            char curChar;
            char[] chars;
            string txt;
            string[] txts;

            txts = new string[txtCount];
            for (int i = 0; i < txtCount; i++)
            {
                txtLength = random.Next(3, txtMaxLength);

                chars = new char[txtLength];
                for (int v = 0; v < txtLength; v++)
                {
                    charIndex = random2.Next(0, charList.Count - 1);
                    curChar = charList[charIndex];
                    chars[v] = curChar;
                }

                txt = new string(chars);
                txts[i] = txt;
            }

            File.WriteAllLines(@"..\..\io\Txts.txt", txts);

            MessageBox.Show("ok");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime start;
            long a;
            long b;
            long memUse;
            string txt;
            int maxDifferCharCount;

            string[] txts = File.ReadAllLines(@"..\..\io\Txts.txt");
            System.Diagnostics.Debug.WriteLine("txts length:" + txts.Length);

            a = Environment.WorkingSet;
            start = DateTime.Now;

            for (int i = 0; i < txts.Length; i++)
            {
                txt = txts[i];
                maxDifferCharCount = LengthOfLongestSubstring(txt);
            }

            b = Environment.WorkingSet;
            memUse = (b - a);

            System.Diagnostics.Debug.WriteLine(" time : " + DateTime.Now.Subtract(start).TotalSeconds.ToString("0.000000") + " sec");
            System.Diagnostics.Debug.WriteLine(" memory : " + memUse/1024 + " KB");

            MessageBox.Show("ok");
        }

        public int LengthOfLongestSubstring(string s)
        {
            //int maxStringLength = 0;
            //int curMaxStringLength;
            for (int i = 0; i < s.Length; i++)
            {
                //maxStringLength = 1;
                //curMaxStringLength = 1;
                //curMaxStringLength = checkHasRepeatChar(s, i) - i;
                //maxStringLength = (maxStringLength > curMaxStringLength) ? maxStringLength : curMaxStringLength;
            }
            //return maxStringLength;
            return 0;
        }

        private int checkHasRepeatChar(string s, int index)
        {
            for (int j = index + 1; j < s.Length; j++)
            {
                for (int k = index; k < j; k++)
                {
                    if (s[j] == s[k])
                    {
                        return j;
                    }
                }
            }
            return s.Length;
        }
    }
}
