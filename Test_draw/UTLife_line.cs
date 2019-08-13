using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OOP_drow;
using System.Drawing;

namespace Test_draw
{
    [TestClass]
    public class UTLife_line
    {
        [TestMethod]
        public void UTPos_hit()
        {
            Life_line RO = new Life_line();
            UTDraw(RO);
            Point p = new Point(3, 2);
            bool actual = RO.Hit_testing(RO, p);
            bool expected = true;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void UTNeg_hit()
        {
            Life_line RO = new Life_line();
            UTDraw(RO);
            Point p = new Point(7, 9);
            bool actual = RO.Hit_testing(RO, p);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        public void UTDraw(Life_line RO)
        {
            RO.Basic_points[0] = new Point(2, 4);
            RO.Basic_points[1] = new Point(4, 1);
            //Graphics gr = new Graphics();

        }
        [TestMethod]
        public void UTShift()
        {
            Life_line RO = new Life_line();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);
            Assert.AreEqual(p, RO.Basic_points[0]);

        }

        [TestMethod]
        public void UTShift_Hit_pos()
        {
            Life_line RO = new Life_line();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);
            Point p1 = new Point(11, 8);
            bool actual = RO.Hit_testing(RO, p1);
            bool expected = true;
            Assert.AreEqual(expected, actual);


        }
        [TestMethod]
        public void UTShift_Hit_neg()
        {
            Life_line RO = new Life_line();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);
            Point p1 = new Point(7, 9);
            bool actual = RO.Hit_testing(RO, p1);
            bool expected = false;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void UTHit_testing_line_pos()
        {
            Life_line RO = new Life_line();
            UTDraw(RO);
            Point p = new Point(3, 54);
            bool actual = RO.Hit_testing_line(RO, p);
            bool expected = true;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void UTHit_testing_line_neg()
        {
            Life_line RO = new Life_line();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);
            Point p1 = new Point(7, 9);
            bool actual = RO.Hit_testing_line(RO, p1);
            bool expected = false;
            Assert.AreEqual(expected, actual);

        }


        [TestMethod]
        public void UTShift_line_pos()
        {
            Life_line RO = new Life_line();
            UTDraw(RO);
            Point p = new Point(3, 54);
            RO.Hit_testing_line(RO,p);
            Point p1 = new Point(7, 15);
            RO.Shift_line(p1);
            bool actual = RO.Hit_testing_line(RO, p1);
            bool expected = true;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void UTShift_line_neg()
        {
            Life_line RO = new Life_line();
            UTDraw(RO);
            Point p = new Point(10, 10);
            Point p1 = new Point(7, 4);
            RO.Shift_line(p1);
            bool actual = RO.Hit_testing_line(RO, p1);
            bool expected = false;
            Assert.AreEqual(expected, actual);

        }

    }
}
