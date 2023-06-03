using System;
using System.ComponentModel;

namespace problem_4_get_merge_array_median
{
    class Program
    {
        static void Main(string[] args)
        {
            var num1 = new int[] { 3, 17, 35, 49 };
            var nums2 = new int[] { 3, 8, 19, 21, 27, 35 };
            FindMedianSortedArrays(num1, nums2);
        }

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var m = nums1.Length;
            var n = nums2.Length;

            var leftIndex = 0;
            var rightIndex = m + n - 1;
            var leftMax = 0;
            var rightMin = 0;
            var num1left = 0;
            var num1right = n;
            var num2left = 0;
            var num2right = m;
            

            double nums1Median;
            double nums2Median;
            while (rightIndex - leftIndex <= 1)
            {

                if (num1left == num1right)
                {
                    
                }
                if (num2left == num2right)
                {

                }
                nums1Median = ComputeMedian(nums1, num1left, num1right);
                nums2Median = ComputeMedian(nums2, num2left, num2right);
                if (nums1Median > nums2Median)
                {
                    num1right = n / 2;
                    rightIndex = rightIndex - n / 2 + 1;
                    rightMin = nums1[n / 2 + 1];
                }
                else if (nums1Median < nums2Median)
                {
                    
                }
                else
                {
                    // 兩邊中位數一樣 == 答案
                    return nums1Median;
                }
            }
        }
        public double ComputeMedian(int[] array, int left, int right)
        {
            var middle = (left + right) / 2;
            if (middle % 2 == 0)
            {
                return array[middle];
            }
            return (array[middle] + array[middle + 1]) / 2;
        }

        public void Get(int[] array)
        {

        }
    }


}

