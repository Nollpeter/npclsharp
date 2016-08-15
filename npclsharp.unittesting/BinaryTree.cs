using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using npclsharp.Containers;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace npclsharp.unittesting
{
    [TestClass]
    public class BinaryTreeTest
    {
        private IBinaryTree<Int32> Tree { get; set; }
        [TestInitialize]
        public void Init()
        {
            Tree = new BinaryTree<Int32>(10);
            Tree.Root.LeftNode = new BinaryNode<Int32>(3,Tree.Root);
            Tree.Root.RightNode = new BinaryNode<Int32>(12,Tree.Root);


        }
        [TestMethod]
        public void TestInOrder()
        {
            String s = "";
            Tree.InOrder(p=>s += $"{p} ");
            Assert.AreEqual("3 10 12 ",s);
        }
        [TestMethod]
        public void TestPreOrder()
        {
            String s = "";
            Tree.PreOrder(p => s += $"{p} ");
            Assert.AreEqual("10 3 12 ", s);
        }
        [TestMethod]
        public void TestPostOrder()
        {
            String s = "";
            Tree.PostOrder(p => s += $"{p} ");
            Assert.AreEqual("3 12 10 ", s);
        }

        [TestMethod]
        public void TestInOrderSortedNess()
        {
            Random r = new Random();
            Tree = new BinaryTree<Int32>();
            List<Int32> numbers = new List<Int32>() { 92, 14, 74, 17, 5, 43, 40, 85, 91, 18, 48, 91, 98, 60, 80, 76, 12, 65, 90, 8, 31, 71, 16, 83, 96, 60, 96, 61, 89, 98, 63, 35, 67, 62, 30, 75, 11, 35, 33, 24, 75, 77, 83, 7, 43, 53, 50, 75, 42, 59, 77, 10, 19, 48, 48, 43, 51, 3, 48, 2, 22, 45, 63, 46, 68, 32, 34, 13, 15, 37, 33, 89, 4, 43, 30, 88, 6, 13, 66, 34 };
            foreach (Int32 t in numbers)
            {
                Tree.Insert(t);
            }
            List<Int32> treeNumbersInOrder = new List<Int32>();
            Tree.InOrder(p=>treeNumbersInOrder.Add(p));
            numbers.Sort();
            for (int i = 0; i < numbers.Count; i++)
            {
                Assert.AreEqual(numbers[i],treeNumbersInOrder[i]);
            }

        }

        [TestCase(1, ExpectedResult = "1 3 10 12 ")]
        [TestCase(5, ExpectedResult = "3 5 10 12 ")]
        [TestCase(11, ExpectedResult = "3 10 11 12 ")]
        [TestCase(13, ExpectedResult = "3 10 12 13 ")]
        [TestMethod]
        public String TestInsert(Int32 insertValue)
        {
            Tree = new BinaryTree<Int32>(10);
            Tree.Root.LeftNode = new BinaryNode<Int32>(3,Tree.Root);
            Tree.Root.RightNode = new BinaryNode<Int32>(12,Tree.Root);
            Tree.Insert(insertValue);
            String s = "";
            Tree.InOrder(p => s += $"{p} ");
            return s;
        }
    }
}
