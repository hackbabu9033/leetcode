using System;

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

        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length <= 0)
            {
                return GetMedianForArray(nums2);
            }
            if (nums2.Length <= 0)
            {
                return GetMedianForArray(nums1);
            }
            var middleIndex = (nums1.Length + nums2.Length) / 2;
            int bottomIndex;
            int topLeft = 0;
            int topRight = nums1.Length - 1;
            int topLength;
            int bottomLeft = 0;
            int bottomRight = nums2.Length - 1;
            int bottomLength;
            int left = 0;
            int right = nums1.Length + nums2.Length - 1;
            int curLength;
            int mergeMiddleIndex;
            while (right >= left)
            {
                // 取從兩個array各取中間的數以左的分一群，以右的分一群
                // EX: top:[1,2,7],bottom=[3,8,19,21]
                // =>t:[1,2,-<|->,7]
                // b:[3,8,19-<|->,21]
                curLength = (right - left + 1);
                mergeMiddleIndex = curLength / 2 + left;
                topLength = topRight - topLeft + 1;
                bottomLength = bottomRight - bottomLeft + 1;
                if (mergeMiddleIndex >= middleIndex)
                {
                    topRight = topLength / 2 + topLeft;
                    bottomRight = bottomLength / 2 + bottomLeft;
                    right = mergeMiddleIndex;
                }
                else
                {
                    topLeft = (topLength / 2) + 1 + topLeft;
                    bottomLeft = (bottomLength / 2) + 1 + bottomLeft;
                    left = mergeMiddleIndex + 1;
                }
            }
            
            return 0;
        }

        private static int[] Slice(int[] array,int start,int end)
        {
            var sliceLength = start + end + 1;
            var result = new int[sliceLength];
            for (int i = 0; i < sliceLength; i++)
            {
                result[i] = array[start + i];
            }
            return result;
        }


        private static double GetMedianForArray(int[] array)
        {
            if (array.Length <= 0)
            {
                return 0;
            }
            int middle = array.Length / 2;
            return array.Length % 2 == 0 ? (array[middle] + array[middle + 1]) / 2 : array[middle];
        }

        public static int FindInsertIndexForSortedList(int[] sortedList, double insertValue)
        {
            int left, right, middle, value;
            left = 0;
            middle = 0;
            right = sortedList.Length - 1;
            while (right >= left)
            {
                middle = (left + right) / 2;
                value = sortedList[middle];
                if (insertValue > value)
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }
            return (left + right - 1) / 2;
        }
    }
}
