using System;

namespace Problem3_add_two0_number
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataProcessor.Create(100,10);
            var testData = DataProcessor.Read();
        }
    }

    public static class Solution
    {
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var result = new ListNode();
            addNodeValue(l1, l2, false, result);
            return result;
        }

        public static void addNodeValue(ListNode l1, ListNode l2, bool isPreOverflow, ListNode result)
        {
            if (l1 == null && l2 == null && !isPreOverflow)
            {
                return;
            }
            var l1NodeValue = (l1 == null) ? 0 : l1.val;
            var l2NodeValue = (l2 == null) ? 0 : l2.val;
            var l1Next = (l1 == null) ? null : l1.next;
            var l2Next = (l2 == null) ? null : l2.next;

            var addValue = l1NodeValue + l2NodeValue;
            if (isPreOverflow)
            {
                addValue++;
            }
            var nodeValue = addValue % 10;
            var isoverFlow = (addValue >= 10);
            result.val = nodeValue;
            if (l1Next != null || l2Next != null || isoverFlow)
            {
                result.next = new ListNode();
            }
            addNodeValue(l1Next, l2Next, isoverFlow, result.next);
        }

    }
}
