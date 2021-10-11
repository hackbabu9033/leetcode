using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Problem3_add_two0_number
{
    public static class DataProcessor
    {
        private static string AccessFilePath = @"..\..\Txts.txt";
        public static void Create(int count,int averageLength)
        {
            var result = new List<ListNode>();
            var rand = new Random();
            int nodeLength;
            int maxLenght = averageLength * 2;
            for (int i = 0; i < count; i++)
            {
                var listNode = new ListNode(rand.Next(0,9));
                var curListNode = listNode;
                nodeLength = rand.Next(0, maxLenght);
                for (int j = 0; j < nodeLength; j++)
                {
                    curListNode.next = new ListNode(rand.Next(0, 9));
                    curListNode = curListNode.next;
                } 
                result.Add(listNode);
            }
            Save(result);
        }

        private static void Save(List<ListNode> data)
        {
            using (var stream = File.Open(AccessFilePath, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, data);
                stream.Flush();
                stream.Position = 0;
            }
        }

        public static List<ListNode> Read()
        {
            using (Stream stream = File.Open(AccessFilePath, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                return (List<ListNode>)binaryFormatter.Deserialize(stream);
            }
        }
    }
}
