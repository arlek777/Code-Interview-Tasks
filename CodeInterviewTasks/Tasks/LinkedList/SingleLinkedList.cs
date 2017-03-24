using System.Collections.Generic;

namespace Tasks.LinkedList
{
    public class SingleLinkedList
    {
        public Node Head { get;  set; }
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

        public SingleLinkedList Reverse()
        {
            if (Head == null || Head.Next == null) return this;

            var first = Head;
            var reversed = new SingleLinkedList();

            if (this.Count == 2)
            {
                var second = first.Next;
                first.Next = reversed.Head;
                second.Next = first;
                reversed.Head = second;
                return reversed;
            }

            while (first != null)
            {
                Node next = first.Next;

                first.Next = reversed.Head;
                reversed.Head = first;

                first = next;
            }

            return reversed;
        }

        public Node RecursiveReverse(Node node, Node reversed)
        {
            if (node == null) return reversed;

            var next = node.Next;
            node.Next = reversed;
            reversed = node;

            var result = RecursiveReverse(next, reversed);

            return result;
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

        public void RemoveTheMiddleNode(Node node)
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
}