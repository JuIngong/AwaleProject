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
    public class GameTests
    {
        [TestMethod()]
        public void ChoseHoleTest_0()
        {
            Game game = new Game(new Player(""), new Player(""));
            game.ChoseHole(1);
            Assert.AreEqual(0, game.Me.Score);
        }

        [TestMethod()]
        public void ChoseHoleTest_GetBean()
        {
            Game game = new Game(new Player(""), new Player(""));
            game.Board.Circular[5] = 2;
            game.Board.Circular[6] = 1;
            game.ChoseHole(6);
            Assert.AreEqual(3, game.Me.Score );
        }

        [TestMethod()]
        public void ChoseHoleTest_GetBeanMultiple()
        {
            Game game = new Game(new Player(""), new Player(""));
            game.Board.Circular[5] = 2;
            game.Board.Circular[4] = 2;
            game.Board.Circular[6] = 1;
            game.Board.Circular[7] = 3;
            game.ChoseHole(7);
            Assert.AreEqual(6, game.Me.Score);
        }
    }
}