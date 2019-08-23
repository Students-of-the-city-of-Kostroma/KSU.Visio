using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSU.Visio.Lib;
using System.Drawing;

namespace KSU.Visio.UT
{
    [TestClass]
    public class UTInstance_specification
    {
        [TestMethod]
        public void UTPos_hit()
        {
            Instance_specification RO = new Instance_specification();
            UTDraw(RO);
            Point p = new Point(3, 2);
            bool actual = RO.Hit_testing(RO, p);
            bool expected = true;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void UTNeg_hit()
        {
            Instance_specification RO = new Instance_specification();
            UTDraw(RO);
            Point p = new Point(7, 9);
            bool actual = RO.Hit_testing(RO, p);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        public void UTDraw(Instance_specification RO)
        {
            RO.Basic_points[0] = new Point(2, 4);
            RO.Basic_points[1] = new Point(4, 1);
            //Graphics gr = new Graphics();

        }
        [TestMethod]
        public void UTShift()
        {
            Instance_specification RO = new Instance_specification();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);
            Assert.AreEqual(p, RO.Basic_points[0]);

        }

        [TestMethod]
        public void UTShift_Hit_pos()
        {
            Instance_specification RO = new Instance_specification();
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
            Instance_specification RO = new Instance_specification();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);
            Point p1 = new Point(7, 9);
            bool actual = RO.Hit_testing(RO, p1);
            bool expected = false;
            Assert.AreEqual(expected, actual);

        }
    }
}
