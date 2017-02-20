using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tasks
{
    public class Node
    {
        public int Data { get; private set; }
        public Node Next { get; set; }

        public Node(int d)
        {
            Data = d;
        }
    }

    public class SingleLinkedList
    {
        public Node FirstNode { get; private set; }
        public uint Count { get; private set; }

        public void AddToTail(int data)
        {
            if (FirstNode == null)
            {
                FirstNode = new Node(data);
            }
            else
            {
                var node = FindLast();
                node.Next = new Node(data);
            }

            Count++;
        }

        private Node FindLast()
        {
            var curr = FirstNode;
            var prev = FirstNode;
            while (curr != null)
            {
                prev = curr;
                curr = curr.Next;
            }

            return prev;
        }

        public void RemoveDuplicates()
        {
            if(Count == 0 || Count == 1) { return; }
            if (Count == 2 && FirstNode.Data == FirstNode.Next.Data)
            {
                FirstNode.Next = null;
                return;
            }

            var curr = FirstNode;
            while (curr != null)
            {
                var innerNode = curr.Next;
                var prevNode = curr;
                while (innerNode != null)
                {
                    if (curr.Data == innerNode.Data)
                    {
                        prevNode.Next = innerNode.Next;
                    }

                    innerNode = innerNode.Next;
                }

                curr = curr.Next;
            }
        }
    }


    [TestClass]
    public class LinkedListChapter2
    {
        private readonly SingleLinkedList _linkedList = new SingleLinkedList();

        [TestMethod]
        public void RemoveDuplicatesWithoutBuffer()
        {
            int[] arr = new[] { 2, 1, 5, 2, 4, 5 };
            foreach (var i in arr)
            {
                _linkedList.AddToTail(i);
            }

            _linkedList.RemoveDuplicates();

            HashSet<int> test = new HashSet<int>();
            var node = _linkedList.FirstNode;
            while (node != null)
            {
                if(test.Contains(node.Data)) { Assert.Fail("Duplicated linked list"); }
                test.Add(node.Data);

                node = node.Next;
            }
        }
    }
}
