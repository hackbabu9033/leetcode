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
        private static int[] test123 = new int[10000000];
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

            start = DateTime.Now;
            a = Environment.WorkingSet;

            for (int i = 0; i < txts.Length; i++)
            {
                txt = txts[i];
                maxDifferCharCount = LengthOfLongestSubstring(txt);
            }

            b = Environment.WorkingSet;
            memUse = (b - a);

            Debug.WriteLine(" time : " + DateTime.Now.Subtract(start).TotalSeconds.ToString("0.000000") + " sec");
            Debug.WriteLine(" memory : " + memUse / 1024 + " KB");
            MessageBox.Show("ok");
        }

        #region brute force

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

        #region brute force - optimize memory
        //public int LengthOfLongestSubstring(string s)
        //{
        //    int maxStringLength = 0;
        //    int curMaxStringLength;
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        curMaxStringLength = checkHasRepeatChar(s, i) - i;
        //        maxStringLength = (maxStringLength > curMaxStringLength) ? maxStringLength : curMaxStringLength;
        //    }
        //    return maxStringLength;
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

        #region slide window
        //public int LengthOfLongestSubstring(string s)
        //{
        //    int maxLength = 0;
        //    int startIndex = 0;
        //    int endIndex = 0;
        //    int nextIndex;
        //    int windowLength;
        //    while (endIndex < s.Length)
        //    {
        //        nextIndex = endIndex + 1;
        //        if (nextIndex < s.Length)
        //        {
        //            for (int i = startIndex; i < endIndex; i++)
        //            {
        //                if (s[nextIndex] == s[i])
        //                {
        //                    windowLength = nextIndex - startIndex;
        //                    maxLength = windowLength > maxLength ? windowLength : maxLength;
        //                    startIndex = i + 1;
        //                    break;
        //                }
        //            }
        //        }
        //        endIndex++;
        //    }
        //    windowLength = endIndex - startIndex;
        //    maxLength = windowLength > maxLength ? windowLength : maxLength;
        //    return maxLength;
        //}

        #endregion

        #region slide window optimize
        public int LengthOfLongestSubstring(string s)
        {
            int[] charShowUpCount = new int[128]; // cache slide window char showup count
            int maxLength = 0;
            int startIndex = 0;
            int endIndex = 0;
            int nextIndex;
            int windowLength;
            char end;
            char start;
            while (endIndex < s.Length)
            {
                end = s[endIndex];
                charShowUpCount[end]++;
                if (charShowUpCount[end] > 1)
                {
                    start = s[startIndex];
                    charShowUpCount[start]--;
                    startIndex++;
                }
                windowLength = endIndex - startIndex;
                maxLength = windowLength > maxLength ? windowLength : maxLength;
                endIndex++;
            }
            return maxLength;
        }

        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            int iterationCount = 100000000;
            char[] array = new char[iterationCount];
            string s = "";
            List<char> list = new List<char>();

            long before;
            long after;

            before = Environment.WorkingSet;
            stopWatch.Start();
            for (int i = 0; i < iterationCount; i++)
            {
                array[i] = 'a';
            }
            stopWatch.Stop();
            Debug.WriteLine("array result:" + stopWatch.ElapsedMilliseconds.ToString());
            stopWatch.Reset();

            stopWatch.Start();
            for (int i = 0; i < iterationCount; i++)
            {
                list.Add('a');
            }
            stopWatch.Stop();
            Debug.WriteLine("list result:" + stopWatch.ElapsedMilliseconds.ToString());
            stopWatch.Reset();
            //after = Environment.WorkingSet;
            //stopWatch.Start();
            //for (int i = 0; i < iterationCount; i++)
            //{
            //    s += 'a';
            //}
            //stopWatch.Stop();
            //Debug.WriteLine("string result:" + stopWatch.ElapsedMilliseconds.ToString());
            //stopWatch.Reset();
        }

        // leetcode Problem5：find Palindromic string
        private void Problem5_Click(object sender, EventArgs e)
        {
            string[] txts = File.ReadAllLines(@"..\..\io\Txts.txt");


        }

        private string LongestPalindrome(string s)
        {
            var length = s.Length;
            var usedEndPalindromeIndex = new int[length];
            for (int i = 0; i < length && s.Length; i++)
            {
                s.Substring
            }
        }
    }
}
