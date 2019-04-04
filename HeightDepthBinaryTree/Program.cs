using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeightDepthBinaryTree
{
    public class BinaryTree
    {

        public static int GetMaxTreeDepth(BinaryTreeNode root)
        {
            if (root == null)
            {
                throw new ArgumentNullException();
            }
            int depth = 0;
            return GetMaxHelper(root, depth);
        }

        private static int GetMaxHelper(BinaryTreeNode node, int depth)
        {
            if (node == null)
            {
                return depth;
            }

            return Math.Max(GetMaxHelper(node.Right, depth + 1), GetMaxHelper(node.Left, depth + 1));
        }
    }

    public class BinaryTreeNode
    {
        public BinaryTreeNode Left { get; set; }
        public BinaryTreeNode Right { get; set; }
        public int Value { get; set; }
    }

    [TestClass]
    public class MaxTreeDepth
    {
        //     0 
        //   1    2
        //  3
        [TestMethod]
        public void GetMaxTreeDepthTest()
        {
            BinaryTreeNode root = new BinaryTreeNode
            {
                Value = 0,
                Left = new BinaryTreeNode { Value = 1 },
                Right = new BinaryTreeNode { Value = 2 }
            };
            root.Left.Right = new BinaryTreeNode { Value = 3 };

            int result = BinaryTree.GetMaxTreeDepth(root);
            Assert.AreEqual(3, result);
        }

        //     0 

        [TestMethod]
        public void GetMaxTreeDepthTestOneNode()
        {
            BinaryTreeNode root = new BinaryTreeNode
            {
                Value = 0,
            };

            int result = BinaryTree.GetMaxTreeDepth(root);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetMaxTreeDepthTestNull()
        {
            BinaryTreeNode root = null;
            int result = BinaryTree.GetMaxTreeDepth(root);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinaryTreeNode root = new BinaryTreeNode
            {
                Value = 0,
                Left = new BinaryTreeNode { Value = 1 },
                Right = new BinaryTreeNode { Value = 2 }
            };
            root.Left.Right = new BinaryTreeNode { Value = 3 };
            root.Left.Left = new BinaryTreeNode { Value = 4 };
            root.Left.Right.Left = new BinaryTreeNode { Value = 5 };
            root.Left.Right.Right = new BinaryTreeNode { Value = 6 };

            //          0 
            //        1   2
            //      4    3
            //         5    6
            Console.WriteLine(BinaryTree.GetMaxTreeDepth(root));
        }
    }
}
