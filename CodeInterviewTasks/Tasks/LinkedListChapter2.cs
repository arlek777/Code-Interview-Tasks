using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks
{
    public class Node
    {
        public int Data { get; set; }
        public Node Next { get; set; }

        public Node(int d)
        {
            Data = d;
        }
    }

    public class SingleLinkedList
    {
        public Node Head { get; private set; }
        public uint Count { get; private set; }

        public void AddToTail(int data)
        {
            if (Head == null)
            {
                Head = new Node(data);
            }
            else
            {
                var node = FindLast();
                node.Next = new Node(data);
            }

            Count++;
        }

        public void AddToHead(int data)
        {
            if (Head == null)
            {
                Head = new Node(data);
            }
            else
            {
                var oldHead = Head;
                Head = new Node(data) {Next = oldHead};
            }

            Count++;
        }

        public void RemoveDuplicates()
        {
            if(Count == 0 || Count == 1) { return; }
            if (Count == 2 && Head.Data == Head.Next.Data)
            {
                Head.Next = null;
                return;
            }

            var curr = Head;
            while (curr != null)
            {
                var innerNode = curr.Next;
                var prevNode = curr;
                while (innerNode != null) 
                {
                    if (curr.Data == innerNode.Data)
                    {
                        prevNode.Next = innerNode.Next;
                        innerNode = innerNode.Next;
                        Count--;
                    }

                    if(innerNode == null)
                        break;
                    prevNode = innerNode;
                    innerNode = innerNode.Next;
                }
                curr = curr.Next;
            }
        }

        public void RemoveTheMiddleNode(ref Node node)
        {
            if (node == null) return;

            node.Data = node.Next.Data;
            node.Next = node.Next.Next;
        }

        public Node FindNode(int data)
        {
            return FindNode(Head, data);
        }

        public List<int> ToList()
        {
            List<int> list = new List<int>();
            var curr = Head;
            while (curr != null)
            {
                list.Add(curr.Data);
                curr = curr.Next;
            }

            return list;
        }

        public SingleLinkedList SumList(SingleLinkedList list)
        {
            var resultList = new SingleLinkedList();

            int sum = 0;
            int carry = 0;

            var list1 = list.Head;
            var list2 = this.Head;

            while (true)
            {
                if (list1 == null && list2 == null)
                {
                    if (carry == 1) resultList.AddToHead(1);
                    break;
                }

                if (list1 != null)
                {
                    sum += list1.Data;
                    list1 = list1.Next;
                }
                if (list2 != null)
                {
                    sum += list2.Data;
                    list2 = list2.Next;
                }

                resultList.AddToHead(sum % 10);
                sum = sum >= 10 ? 1 : 0;
                carry = sum;
            }

            return resultList;
        }

        public SingleLinkedList SumListV2(SingleLinkedList list)
        {
            var resultList = new SingleLinkedList();

            int sum = 0;
            int thens = 1;

            var list1 = list.Head;
            var list2 = this.Head;
            while (true)
            {
                if (list1 == null && list2 == null)
                    break;

                if (list1 != null)
                {
                    sum += (list1.Data * thens);
                    list1 = list1.Next;
                }
                if (list2 != null)
                {
                    sum += (list2.Data * thens);
                    list2 = list2.Next;
                }

                thens *= 10;
            }

            while (sum != 0)
            {
                resultList.AddToHead(sum % 10);
                sum = sum / 10;
            }

            return resultList;
        }

        private Node FindNode(Node node, int data)
        {
            if (node == null) return null;
            if (node.Data == data) return node;

            return FindNode(node.Next, data);
        }

        private Node FindLast()
        {
            var curr = Head;
            while (curr.Next != null)
            {
                curr = curr.Next;
            }

            return curr;
        }
    }


    [TestClass]
    public class LinkedListChapter2
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

            linkedList.RemoveTheMiddleNode(ref node);
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
    }
}
