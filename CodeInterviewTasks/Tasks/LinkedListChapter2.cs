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

        public void RemoveDuplicates()
        {
            if(Count == 0 || Count == 1) { return; }
            if (Count == 2 && Head.Data == Head.Next.Data)
            {
                Head.Next = null;
                return;
            }

            var curr = Head;
            while (curr != null)// TODO next to null
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

        public void RemoveTheMiddleNode(Node node)
        {
            if (node?.Next == null)
                return;

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
        private readonly SingleLinkedList _linkedList = new SingleLinkedList();

        [TestInitialize]
        public void Init()
        {
            int[] arr = new[] { 2,3,4,3,2 };
            foreach (var i in arr)
            {
                _linkedList.AddToTail(i);
            }
        }

        [TestMethod]
        public void RemoveDuplicatesWithoutBuffer()
        {
            _linkedList.RemoveDuplicates();

            HashSet<int> test = new HashSet<int>();
            var node = _linkedList.Head;
            while (node != null)
            {
                if(test.Contains(node.Data)) { Assert.Fail("Duplicated linked list"); }
                test.Add(node.Data);

                node = node.Next;
            }
        }

        [TestMethod]
        public void RemoveNodeInTheMiddle()
        {//TODO think if it's the last node
            _linkedList.RemoveTheMiddleNode(_linkedList.FindNode(4));
            var result = _linkedList.ToList();
            var test = new[] {2, 3, 3, 2};

            for (int i = 0; i < test.Length; i++)
            {
                if (test[i] != result[i])
                {
                    Assert.Fail("Not removed from the middle.");
                }
            }
        }
    }
}
