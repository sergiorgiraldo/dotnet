using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HeightDepthBinaryTree
{
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
					Left = new BinaryTreeNode {Value = 1},
					Right = new BinaryTreeNode {Value = 2}
				};
				root.Left.Right = new BinaryTreeNode {Value = 3};

				int result = GetMaxTreeDepth(root);
				Assert.AreEqual(3,result);
			}

			//     0 

			[TestMethod]
			public void GetMaxTreeDepthTestOneNode()
			{
				BinaryTreeNode root = new BinaryTreeNode
				{
					Value = 0,
				};

				int result = GetMaxTreeDepth(root);
				Assert.AreEqual(1, result);
			}

			[TestMethod]
			[ExpectedException(typeof(ArgumentNullException))]
			public void GetMaxTreeDepthTestNull()
			{
				BinaryTreeNode root = null;
				int result = GetMaxTreeDepth(root);
			}

			private int GetMaxTreeDepth(BinaryTreeNode root)
			{
				if (root == null)
				{
					throw new ArgumentNullException();
				}
				int depth = 0;
				return GetMaxHelper(root, depth);
			}

			private int GetMaxHelper(BinaryTreeNode root, int depth)
			{
				if (root == null)
				{
					return depth;
				}

				return Math.Max( GetMaxHelper(root.Right, depth+1),  GetMaxHelper(root.Left, depth+1));
			}
		}
		
       class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
