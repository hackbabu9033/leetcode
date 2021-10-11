using System;
using System.Collections.Generic;
using System.Text;

namespace Problem3_add_two0_number
{
    [Serializable]
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}
