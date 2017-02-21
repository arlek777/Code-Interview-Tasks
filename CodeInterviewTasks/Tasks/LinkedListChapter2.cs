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

        private Node FindLast()
        {
            var curr = Head;
            while (curr != null)
            {
                if (curr.Next == null) return curr;
                curr = curr.Next;
            }

            return curr;
        }
    }


    [TestClass]
    public class LinkedListChapter2
    {
        private readonly SingleLinkedList _linkedList = new SingleLinkedList();

        [TestMethod]
        public void RemoveDuplicatesWithoutBuffer()
        {
            int[] arr = new[] { 3, 5, 3, 5, 3, 2, 4, 5 };
            foreach (var i in arr)
            {
                _linkedList.AddToTail(i);
            }

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
    }
}
