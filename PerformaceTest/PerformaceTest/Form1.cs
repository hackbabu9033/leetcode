using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            long workSetBefore;
            long workSetAfter;

            PerformanceCounter cpu = new PerformanceCounter(
            "Processor", "% Processor Time", "_Total");
            PerformanceCounter memory = new PerformanceCounter(
                "Memory", "% Committed Bytes in Use");

            string[] txts = File.ReadAllLines(@"..\..\io\Txts.txt");

            a = Environment.WorkingSet;
            start = DateTime.Now;
            Debug.WriteLine("memoryCounter:" + memory.NextValue().ToString());

            for (int i = 0; i < txts.Length; i++)
            {
                txt = txts[i];
                maxDifferCharCount = LengthOfLongestSubstring(txt);
            }

            b = Environment.WorkingSet;
            Debug.WriteLine("memoryCounter:" + memory.NextValue().ToString());
            workSetBefore = (a >= 0) ? a : ((long)int.MaxValue * 2) + a;
            workSetAfter = (b >= 0) ? b : ((long)int.MaxValue * 2) + b;
            memUse = (b - a);

            Debug.WriteLine(" time : " + DateTime.Now.Subtract(start).TotalSeconds.ToString("0.000000") + " sec");
            Debug.WriteLine(" memory : " + memUse / 1024 + " KB");
            MessageBox.Show("ok");
        }

        #region memory allocate/usage optimize
        //public int LengthOfLongestSubstring(string s)
        //{
        //    int maxStringLength = 0;
        //    int curMaxStringLength;
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        //maxStringLength = 1;
        //        //curMaxStringLength = 1;
        //        curMaxStringLength = checkHasRepeatChar(s, i) - i;
        //        maxStringLength = (maxStringLength > curMaxStringLength) ? maxStringLength : curMaxStringLength;
        //    }
        //    return maxStringLength;
        //    //return 0;
        //}

        //private int checkHasRepeatChar(string s, int index)
        //{
        //    for (int j = index + 1; j < s.Length; j++)
        //    {
        //        for (int k = index; k < j; k++)
        //        {
        //            if (s[j] == s[k])
        //            {
        //                return j;
        //            }
        //        }
        //    }
        //    return s.Length;
        //}
        #endregion


        #region version1

        //public int LengthOfLongestSubstring(string s)
        //{
        //    string curMaxString = "";
        //    string result = "";
        //    int curIndex;
        //    char indexChar;
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        result = "";
        //        curIndex = i;
        //        while (curIndex < s.Length)
        //        {
        //            indexChar = s[curIndex];
        //            if (result.IndexOf(indexChar) >= 0)
        //            {
        //                break;
        //            }
        //            result += indexChar;
        //            curIndex++;
        //        }
        //        curMaxString = (curMaxString.Length >= result.Length) ? curMaxString : result;
        //    }
        //    return curMaxString.Length;
        //}

        #endregion

        #region slide window
        public int LengthOfLongestSubstring(string s)
        {
            int maxLength = 0;
            int startIndex = 0;
            int endIndex = 0;
            int nextIndex;
            int windowLength;
            while (endIndex < s.Length)
            {
                nextIndex = endIndex + 1;
                if (nextIndex < s.Length)
                {
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (s[nextIndex] == s[i])
                        {
                            windowLength = nextIndex - startIndex;
                            maxLength = windowLength > maxLength ? windowLength : maxLength;
                            startIndex = i + 1;
                            break;
                        }
                    }
                }
                endIndex++;
            }
            windowLength = endIndex - startIndex;
            maxLength = windowLength > maxLength ? windowLength : maxLength;
            return maxLength;
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            long workSetBefore;
            long workSetAfter;
            long memUse;

            var before = GC.GetTotalMemory(false);

            workSetBefore = Environment.WorkingSet;

            int a = 1;
            int b = 1;
            int c = 1;
            int d = 1;
            a = a + 2;

            workSetAfter = Environment.WorkingSet;
            var after = GC.GetTotalMemory(false);
            memUse = (workSetAfter - workSetBefore);

            Debug.WriteLine(" memory : " + memUse / 1024 + " KB");
        }
    }
}
