using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks.LinkedList
{
    [TestClass]
    public class LinkedListChapterTests
    {
        [TestMethod]
        public void RemoveDuplicatesWithoutBuffer()
        {
            var linkedList = new SingleLinkedList();

            int[] arr = new[] { 2, 3, 4, 3, 2 };
            foreach (var i in arr)
            {
                linkedList.AddToTail(i);
            }

            linkedList.RemoveDuplicates();

            HashSet<int> test = new HashSet<int>();
            var node = linkedList.Head;
            while (node != null)
            {
                if(test.Contains(node.Data)) { Assert.Fail("Duplicated linked list"); }
                test.Add(node.Data);

                node = node.Next;
            }
        }

        [TestMethod]
        public void RemoveNodeInTheMiddle()
        {
            var linkedList = new SingleLinkedList();

            int[] arr = new[] { 2, 3, 4, 3, 2 };
            foreach (var i in arr)
            {
                linkedList.AddToTail(i);
            }

            var node = linkedList.FindNode(4);

            linkedList.RemoveTheMiddleNode(node);
            var result = linkedList.ToList();
            var test = new[] {2, 3, 3, 2};

            for (int i = 0; i < test.Length; i++)
            {
                if (test[i] != result[i])
                {
                    Assert.Fail("Not removed from the middle.");
                }
            }
        }

        [TestMethod]
        public void SumLinkedListDigitsV1()
        {
            var linkedList1 = new SingleLinkedList();
            linkedList1.AddToHead(5);
            linkedList1.AddToHead(1);
            linkedList1.AddToHead(3);

            var linkedList2 = new SingleLinkedList();
            linkedList2.AddToHead(2);
            linkedList2.AddToHead(9);
            linkedList2.AddToHead(5);

            var resultList = linkedList1.SumList(linkedList2);

            var expected = new[] { 8, 0, 8 };
            var result = resultList.ToList();

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] != expected[i])
                {
                    Assert.Fail("Failed.");
                }
            }
        }

        [TestMethod]
        public void SumLinkedListDigitsV2()
        {
            var linkedList1 = new SingleLinkedList();
            linkedList1.AddToHead(5);
            linkedList1.AddToHead(1);
            linkedList1.AddToHead(3);

            var linkedList2 = new SingleLinkedList();
            linkedList2.AddToHead(2);
            linkedList2.AddToHead(9);
            linkedList2.AddToHead(5);

            var resultList = linkedList1.SumListV2(linkedList2);

            var expected = new[] { 8, 0, 8 };
            var result = resultList.ToList();

            for (int i = 0; i < result.Count; i++)
            {
                if (result[i] != expected[i])
                {
                    Assert.Fail("Failed.");
                }
            }
        }

        [TestMethod]
        public void LoopReverseList()
        {
            var linkedList1 = new SingleLinkedList();
            linkedList1.AddToTail(1);
            linkedList1.AddToTail(2);
            linkedList1.AddToTail(3);
            linkedList1.AddToTail(4);

            var reversed = linkedList1.Reverse().ToList();

            int[] expected = new[] {4, 3, 2, 1};
            for (int i = 0; i < expected.Length; i++)
            {
                if (reversed[i] != expected[i])
                {
                    Assert.Fail("Failed.");
                }
            }
        }

        [TestMethod]
        public void LoopReverseListWith2Elements()
        {
            var linkedList1 = new SingleLinkedList();
            linkedList1.AddToTail(1);
            linkedList1.AddToTail(2);

            var reversed = linkedList1.Reverse().ToList();

            int[] expected = new[] { 2, 1 };
            for (int i = 0; i < expected.Length; i++)
            {
                if (reversed[i] != expected[i])
                {
                    Assert.Fail("Failed.");
                }
            }
        }
    }
}
