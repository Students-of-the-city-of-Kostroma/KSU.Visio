using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KSU.Visio.Lib;
using System.Drawing;

namespace KSU.Visio.UT
{
    [TestClass]
    public class UTRectangle_object
    {
        [TestMethod]
        public void UTPos_hit()
        {
            Rectangle_object RO = new Rectangle_object();
            UTDraw(RO);
            Point p = new Point(3, 2);
            bool actual = RO.Hit_testing( p);
            bool expected = true;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void UTNeg_hit()
        {
            Rectangle_object RO = new Rectangle_object();
            UTDraw(RO);
            Point p = new Point(7, 9);
            bool actual = RO.Hit_testing(p);
            bool expected = false;
            Assert.AreEqual(expected, actual);
        }
        public void UTDraw(Rectangle_object RO)
        {
            ////RO.LeftTop = new Point(2, 4);
            ////RO.RightBottom = new Point(4, 1);
            //Graphics gr = new Graphics();

        }
        [TestMethod]
        public void UTShift()
        {
            Rectangle_object RO = new Rectangle_object();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);           
            Assert.AreEqual(p, RO.LeftTop);

        }

        [TestMethod]
        public void UTShift_Hit_pos()
        {
            Rectangle_object RO = new Rectangle_object();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);
            Point p1 = new Point(11, 8);
            bool actual = RO.Hit_testing( p1);
            bool expected = true;
            Assert.AreEqual(expected, actual);


        }
        [TestMethod]
        public void UTShift_Hit_neg()
        {
            Rectangle_object RO = new Rectangle_object();
            UTDraw(RO);
            Point p = new Point(10, 10);
            RO.Shift(p);
            Point p1 = new Point(7, 9);
            bool actual = RO.Hit_testing( p1);
            bool expected = false;
            Assert.AreEqual(expected, actual);

        }


    }
}
