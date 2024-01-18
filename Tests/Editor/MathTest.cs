using System.Collections;
using System.Collections.Generic;
using MiskCore.Math;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MiskCore
{
    public class MathTest
    {
        #region InnerEulerAngle2Vector

        [Test]
        public void InnerEulerAngle2Vector_Angle0()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.forward, Vector3.forward), 0);
        }

        [Test]
        public void InnerEulerAngle2Vector_Angle90()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.forward, Vector3.up), 90);
        }

        [Test]
        public void InnerEulerAngle2Vector_AngleNav90()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.forward, Vector3.down), 90);
        }

        [Test]
        public void InnerEulerAngle2Vector_Angle180()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.forward, Vector3.back), 180);
        }

        [Test]
        public void InnerEulerAngle2Vector_Angle45()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.forward, Vector3.forward + Vector3.up), 45);
        }

        [Test]
        public void InnerEulerAngle2Vector_Angle135()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.forward, Vector3.back + Vector3.up), 135);
        }

        [Test]
        public void InnerEulerAngle2Vector_Replace_Angle90()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.up, Vector3.forward), 90);
        }

        [Test]
        public void InnerEulerAngle2Vector_Replace_AngleNav90()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.down, Vector3.forward), 90);
        }

        [Test]
        public void InnerEulerAngle2Vector_Replace_Angle180()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.back, Vector3.forward), 180);
        }

        [Test]
        public void InnerEulerAngle2Vector_Replace_Angle45()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.forward + Vector3.up, Vector3.forward), 45);
        }

        [Test]
        public void InnerEulerAngle2Vector_Replace_Angle135()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.back + Vector3.up, Vector3.forward), 135);
        }

        [Test]
        public void InnerEulerAngle2Vector_Angle225ToBe135()
        {
            Assert.AreEqual(MathExtension.InnerEulerAngle2Vector(Vector3.forward, Vector3.back + Vector3.down), 135);
        }
        #endregion


        #region PositiveEulerAngle

        [Test]
        public void PositiveEulerAngle_0()
        {
            Assert.AreEqual(MathExtension.PositiveEulerAngle(0), 0);
        }

        [Test]
        public void PositiveEulerAngle_60()
        {
            Assert.AreEqual(MathExtension.PositiveEulerAngle(60), 60);
        }

        [Test]
        public void PositiveEulerAngle_181()
        {
            Assert.AreEqual(MathExtension.PositiveEulerAngle(181), 181);
        }

        [Test]
        public void PositiveEulerAngle_359()
        {
            Assert.AreEqual(MathExtension.PositiveEulerAngle(359), 359);
        }

        [Test]
        public void PositiveEulerAngle_360()
        {
            Assert.AreEqual(MathExtension.PositiveEulerAngle(360), 0);
        }

        [Test]
        public void PositiveEulerAngle_Nav60()
        {
            Assert.AreEqual(MathExtension.PositiveEulerAngle(-60), 300);
        }

        [Test]
        public void PositiveEulerAngle_Nav181()
        {
            Assert.AreEqual(MathExtension.PositiveEulerAngle(-181), 179);
        }

        [Test]
        public void PositiveEulerAngle_361()
        {
            Assert.AreEqual(MathExtension.PositiveEulerAngle(361), 1);
        }

        [Test]
        public void PositiveEulerAngle_720()
        {
            Assert.AreEqual(MathExtension.PositiveEulerAngle(720), 0);
        }

        #endregion


        #region GetDirectionBy2EularAngle

        [Test]
        public void GetDirectionBy2EularAngle_0_1()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, 1), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_1_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(1, 0), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_179()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, 179), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_179_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(179, 0), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_181()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, 181), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_181_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(181, 0), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_359()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, 359), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_359_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(359, 0), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_30_60()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(30, 60), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_60_30()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(60, 30), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_Nav1()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, -1), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_Nav1_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(-1, 0), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_Nav179()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, -179), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_Nav179_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(-179, 0), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_Nav181()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, -181), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_Nav181_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(-181, 0), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_Nav359()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, -359), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_Nav359_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(-359, 0), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_Nav30_Nav60()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(-30, -60), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_Nav60_Nav30()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(-60, -30), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_361()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, 361), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_361_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(361, 0), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_539()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, 539), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_539_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(539, 0), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_541()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, 541), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_541_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(541, 0), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_0_719()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(0, 719), RotationDirection.CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_719_0()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(719, 0), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_390_420()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(390, 420), RotationDirection.COUNTER_CLOCKWISE);
        }

        [Test]
        public void GetDirectionBy2EularAngle_420_390()
        {
            Assert.AreEqual(MathExtension.GetDirectionBy2EularAngle(420, 390), RotationDirection.CLOCKWISE);
        }



        #endregion


        #region GetNearRotationDifference

        [Test]
        public void GetNearRotationDifference_0_1()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, 1), 1);
        }

        [Test]
        public void GetNearRotationDifference_1_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(1, 0), 1);
        }

        [Test]
        public void GetNearRotationDifference_0_179()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, 179), 179);
        }

        [Test]
        public void GetNearRotationDifference_179_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(179, 0), 179);
        }

        [Test]
        public void GetNearRotationDifference_0_181()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, 181), 179);
        }

        [Test]
        public void GetNearRotationDifference_181_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(181, 0), 179);
        }

        [Test]
        public void GetNearRotationDifference_0_359()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, 359), 1);
        }

        [Test]
        public void GetNearRotationDifference_359_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(359, 0), 1);
        }

        [Test]
        public void GetNearRotationDifference_30_60()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(30, 60), 30);
        }

        [Test]
        public void GetNearRotationDifference_60_30()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(60, 30), 30);
        }

        [Test]
        public void GetNearRotationDifference_0_Nav1()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, -1), 1);
        }

        [Test]
        public void GetNearRotationDifference_Nav1_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(-1, 0), 1);
        }

        [Test]
        public void GetNearRotationDifference_0_Nav179()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, -179), 179);
        }

        [Test]
        public void GetNearRotationDifference_Nav179_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(-179, 0), 179);
        }

        [Test]
        public void GetNearRotationDifference_0_Nav181()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, -181), 179);
        }

        [Test]
        public void GetNearRotationDifference_Nav181_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(-181, 0), 179);
        }

        [Test]
        public void GetNearRotationDifference_0_Nav359()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, -359), 1);
        }

        [Test]
        public void GetNearRotationDifference_Nav359_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(-359, 0), 1);
        }

        [Test]
        public void GetNearRotationDifference_Nav30_Nav60()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(-30, -60), 30);
        }

        [Test]
        public void GetNearRotationDifference_Nav60_Nav30()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(-60, -30), 30);
        }

        [Test]
        public void GetNearRotationDifference_0_361()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, 361), 1);
        }

        [Test]
        public void GetNearRotationDifference_361_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(361, 0), 1);
        }

        [Test]
        public void GetNearRotationDifference_0_539()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, 539), 179);
        }

        [Test]
        public void GetNearRotationDifference_539_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(539, 0), 179);
        }

        [Test]
        public void GetNearRotationDifference_0_541()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, 541), 179);
        }

        [Test]
        public void GetNearRotationDifference_541_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(541, 0), 179);
        }

        [Test]
        public void GetNearRotationDifference_0_719()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(0, 719), 1);
        }

        [Test]
        public void GetNearRotationDifference_719_0()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(719, 0), 1);
        }

        [Test]
        public void GetNearRotationDifference_390_420()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(390, 420), 30);
        }

        [Test]
        public void GetNearRotationDifference_420_390()
        {
            Assert.AreEqual(MathExtension.GetNearRotationDifference(420, 390), 30);
        }



        #endregion
    }
}

