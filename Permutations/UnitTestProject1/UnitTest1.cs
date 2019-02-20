using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Permutations;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public Permutation p;


        [TestInitialize]
        public void TestInitialize()
        {
            p = new Permutation();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var result = p.Do(new List<string> { "a" });
            Assert.IsTrue(result.Count == 1); //a
        }
        [TestMethod]
        public void TestMethod2()
        {
            var result = p.Do(new List<string> { "a" });
            Assert.IsTrue(result[0] == "a");
        }
        [TestMethod]
        public void TestMethod3()
        {
            var result = p.Do(new List<string> { "a", "b" });
            Assert.IsTrue(result.Count == 3);//a,b,ab
        }
        [TestMethod]
        public void TestMethod4()
        {
            var result = p.Do(new List<string> { "a", "b" });
            Assert.IsTrue(result.IndexOf("a") != -1);
            Assert.IsTrue(result.IndexOf("b") != -1);
            Assert.IsTrue(result.IndexOf("ab") != -1);
        }
        [TestMethod]
        public void TestMethod5()
        {
            var result = p.Do(new List<string> { "a", "b", "c" });
            Assert.IsTrue(result.Count == 7);//a,b,c,ab,ac,bc,abc
        }
        [TestMethod]
        public void TestMethod6()
        {
            var result = p.Do(new List<string> { "a", "b", "c" });
            Assert.IsTrue(result.IndexOf("a") != -1);
            Assert.IsTrue(result.IndexOf("b") != -1);
            Assert.IsTrue(result.IndexOf("c") != -1);
            Assert.IsTrue(result.IndexOf("ab") != -1);
            Assert.IsTrue(result.IndexOf("ac") != -1);
            Assert.IsTrue(result.IndexOf("bc") != -1);
            Assert.IsTrue(result.IndexOf("abc") != -1);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var result = p.Do(new List<string> { "a", "b", "c", "d" });
            Assert.IsTrue(result.IndexOf("a") != -1);
            Assert.IsTrue(result.IndexOf("b") != -1);
            Assert.IsTrue(result.IndexOf("c") != -1);
            Assert.IsTrue(result.IndexOf("d") != -1);
            Assert.IsTrue(result.IndexOf("ab") != -1);
            Assert.IsTrue(result.IndexOf("ac") != -1);
            Assert.IsTrue(result.IndexOf("ad") != -1);
            Assert.IsTrue(result.IndexOf("bc") != -1);
            Assert.IsTrue(result.IndexOf("bd") != -1);
            Assert.IsTrue(result.IndexOf("cd") != -1);
            Assert.IsTrue(result.IndexOf("abc") != -1);
            Assert.IsTrue(result.IndexOf("abd") != -1);
            Assert.IsTrue(result.IndexOf("acd") != -1);
            Assert.IsTrue(result.IndexOf("bcd") != -1);
            Assert.IsTrue(result.IndexOf("abcd") != -1);
        }
    }
}
