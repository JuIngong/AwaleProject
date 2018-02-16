using Microsoft.VisualStudio.TestTools.UnitTesting;
using AwaleProject.modele;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AwaleProject.modele.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        public void ChosePlayTest_EmptyHole()
        {
            Board b = new Board();
            b.ChosePlay(1);
            Assert.AreEqual(0, b.Circular[1]);
        }
        [TestMethod()]
        public void ChosePlayTest_PreviousAdd()
        {
            Board b = new Board();
            b.ChosePlay(1);
            Assert.AreEqual(5, b.Circular[0]);
        }

        [TestMethod()]
        public void ChosePlayTest_LastPreviousAdd()
        {
            Board b = new Board();
            b.ChosePlay(1);

            Assert.AreEqual(5, b.Circular[9]);
        }

        [TestMethod()]
        public void ChosePlayTest_LastPreviousNotChanged()
        {
            Board b = new Board();
            b.ChosePlay(1);
            Assert.AreEqual(4, b.Circular[8]);
        }

        [TestMethod()]
        public void CanChoseTest()
        {
            Board b = new Board();
            b.ChosePlay(1);
            Assert.IsTrue(b.CanChose(2));
        }

        [TestMethod()]
        public void CanChoseTest_False()
        {
            Board b = new Board();
            b.ChosePlay(1);
            Assert.IsFalse(b.CanChose(1));
        }

        [TestMethod()]
        public void GetBeanTest()
        {
            Board b = new Board();
            b.ChosePlay(1);
            b.Circular[1] = 2;
            Assert.AreEqual(2, b.GetBean(1));
        }

        [TestMethod()]
        public void GetBeanTest_Hole()
        {
            Board b = new Board();
            b.ChosePlay(1);
            b.Circular[1] = 2;
            b.GetBean(1);
            Assert.AreEqual(0, b.Circular[1]);
        }

        [TestMethod()]
        public void GetBeanTest_0()
        {
            Board b = new Board();
            b.ChosePlay(1);
            b.Circular[1] = 4;
            Assert.AreEqual(0, b.GetBean(1));
        }

        [TestMethod()]
        public void GetBeanTest_0Hole()
        {
            Board b = new Board();
            b.ChosePlay(1);
            b.Circular[1] = 4;
            b.GetBean(1);
            Assert.AreEqual(4, b.Circular[1]);
        }
    }
}