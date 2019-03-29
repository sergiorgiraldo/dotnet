using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectingCycleInLinkedList
{
    class LinkedList
    {
        Node Head;

        public class Node
        {
            public int value;
            public string key;
            public Node NextNode;

            public Node(int value)
            {
                key = Guid.NewGuid().ToString();
                this.value = value;
            }
        }

        public LinkedList(Node head)
        {
            Head = head;
        }

        public Boolean hasLoop() //brute force
        {
            List<string> lstKeys = new List<string>
            {
                Head.key
            };

            Node tempNode = Head.NextNode;
            while (tempNode != null)
            {
                if (lstKeys.IndexOf(tempNode.key) != -1) return true;
                lstKeys.Add(tempNode.key);
                tempNode = tempNode.NextNode;
            }
            return false;
        }

        public Boolean hasLoop2() //aha
        {
            Node tempNode = Head;
            Node tempNode1 = Head.NextNode;
            while (tempNode != null && tempNode1 != null)
            {
                if (tempNode.Equals(tempNode1))
                {
                    return true;
                }

                if ((tempNode1.NextNode != null) && (tempNode.NextNode != null))
                {
                    tempNode1 = tempNode1.NextNode.NextNode;
                    tempNode = tempNode.NextNode;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public static void Main()
        {
            Node head = new Node(1);
            LinkedList ll = new LinkedList(head);

            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);
            Node node6 = new Node(6);
            Node node7 = new Node(7);

            head.NextNode = node2;
            node2.NextNode = node3;
            node3.NextNode = node4;
            node4.NextNode = node5;
            node5.NextNode = node6;
            node6.NextNode = node7;
            node7.NextNode = node4;

            Console.WriteLine(ll.hasLoop());
            Console.Read();
        }
    }
}
