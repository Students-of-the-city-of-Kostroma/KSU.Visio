using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSU.Visio.Lib;
using System.Drawing;



namespace KSU.Visio.UT
{
    [TestClass]
    public class UTReturn_mess
    {
        [TestMethod]
        public void UTPos_hit()
        {
            Return_mess RO = new Return_mess();
            UTDraw(RO);
            Point p = new Point(5, 3);
            bool actual = RO.Hit_testing(RO, p);
            bool expected = true;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void UTNeg_hit()
        {
            Return_mess RO = new Return_mess();
            UTDraw(RO);
            Point p = new Point(20, 20);
            bool actual = RO.Hit_testing(RO, p);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        public void UTDraw(Return_mess RO)
        {
            RO.Basic_points[0] = new Point(7, 8);
            RO.Basic_points[1] = new Point(5, 2);
            //Graphics gr = new Graphics();

        }
        [TestMethod]
        public void UTShift()
        {
            Return_mess RO = new Return_mess();
            UTDraw(RO);
            Point p = new Point(10, 10);
            Point p1 = new Point(5, 8);
            RO.Hit_testing(RO, p);
            RO.Shift(p1);
            if (p1.Equals(RO.Basic_points[0]))
                Assert.AreEqual(p1, RO.Basic_points[0]);
            else
                Assert.AreEqual(p1, RO.Basic_points[1]);


        }

        [TestMethod]
        public void UTShift_Hit_pos()
        {
            Return_mess RO = new Return_mess();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);
            Point p1 = new Point(11, 11);
            bool actual = RO.Hit_testing(RO, p1);
            bool expected = true;
            Assert.AreEqual(expected, actual);


        }
        [TestMethod]
        public void UTShift_Hit_neg()
        {
            Return_mess RO = new Return_mess();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Hit_testing(RO, p);
            RO.Shift(p);
            Point p1 = new Point(30, 357);
            bool actual = RO.Hit_testing(RO, p1);
            bool expected = false;
            Assert.AreEqual(expected, actual);

        }


    }
}
