/*
 * Author: Luke Campbell <LCampbell@ASAScience.com>
 * ASA.NetCDF4.Test.TestNcArray
 */

using System;
using System.Collections.Generic;
using ASA.NetCDF4;

namespace ASA.NetCDF4.Test {
    public class TestNcArray : UnitTest {
        private string filePath = "test.nc";
        public TestNcArray() {
            AddTest(TestSlicing, "TestSlicing");
            AddTest(TestReshape, "TestReshape");
            AddTest(TestAddSub, "TestAddSub");
            AddTest(TestMultDiv, "TestMultDiv");
            AddTest(TestGet, "TestGet");
            AddTest(TestPut, "TestPut");
            AddTest(TestCasting,"TestCasting");
        }

        public bool TestSlicing() {

            int[] basicArray = new int[64];
            for(int i=0;i<64;i++) basicArray[i] = i;

            NcArray ncArray = new NcArray(basicArray, new int[] { 4,4,4});
            NcArray outArray = ncArray.Slice(new int[] { 2, 2, 2}, new int[] {4, 4, 4});
            Assert.Equals(outArray.Array, new int[] { 42, 43, 46, 47, 58, 59, 62, 63 });

            return true;
        }

        public bool TestReshape() {

            int[] shape = new int[] { 2,2,2 };
            NcArray array = NcArray.Arange(NcDouble.Instance, 2*2*2).Reshape(shape);
            Assert.Equals(array.Shape, shape);
            array.Reshape(new int[] { 8 });
            Assert.Equals(array.Shape, new int[]  { 8 } );
            try {
                array.GetValueAt(1,1,1);
                throw new AssertFailedException("Failed to throw index bounds exception");
            } catch (exceptions.NcInvalidArg) {
            }

            return true;

        }

        public bool TestAddSub() {
            NcArray ones = new NcArray(NcInt.Instance, new int[] { 10 }).Fill(1);
            NcArray twos = ones + 1;
            NcArray threes = ones + 2;
            NcArray fours = twos + 2;

            Assert.True(threes.Equals(ones + twos));
            Assert.False(fours.Equals(ones + twos));
            Assert.True(fours.Equals(twos + twos));
            Assert.True(ones.Equals(threes - twos));
            Assert.True(twos.Equals(fours - twos));
            Assert.False(ones.Equals(fours - ones));
            return true;
        }

        public bool TestMultDiv() {
            NcArray ones = new NcArray(NcInt.Instance, new int[] { 10 }).Fill(1);
            NcArray twos = ones * 2;
            NcArray threes = twos + 1;
            NcArray fours = twos * 2;
            NcArray zeros = ones * 0;


            Assert.True(threes.Equals(ones * threes));
            Assert.True(twos.Equals(fours / twos));
            Assert.True(twos.Equals(fours / 2));
            try {
                fours.Div(0);
                throw new AssertFailedException("Failed to throw DivideByZeroException");
            } catch (DivideByZeroException) {
            }

            NcArray buf = NcArray.Arange(NcDouble.Instance, 5);
            buf = buf * 10.0;
            int[] shape = new int[] { 1,3,5,7};
            NcArray uVector = NcArray.Arange(NcShort.Instance, 1 * 3 * 5 * 7).Reshape(shape);
            return true;
        }




        public bool TestGet() {
            NcFile file = null;
            try {
                file = TestHelper.NewFile(filePath);
                NcDim time = file.AddDim("time", 10);
                NcDim x = file.AddDim("x", 20);
                NcDim y = file.AddDim("y", 20);
                NcVar u = file.AddVar("u", NcFloat.Instance, new List<NcDim>() { time, x, y });
                NcVar v = file.AddVar("v", NcFloat.Instance, new List<NcDim>() { time, x, y });

                float[] uBuf = new float[10 * 20 * 20];
                for(int i=0;i<uBuf.Length;i++) uBuf[i] = (float)i;
                float[] vBuf = new float[10 * 20 * 20];
                for(int i=0;i<vBuf.Length;i++) vBuf[i] = (float)i;

                u.PutVar(uBuf);
                v.PutVar(vBuf);

                NcArray uArray = u.GetVar();
                Assert.Equals(uArray.Array, uBuf);

            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool TestPut() {
            NcFile file = null;
            try {
                file = TestHelper.NewFile(filePath);
                NcDim time = file.AddDim("time", 2);
                NcDim x = file.AddDim("x", 2);
                NcDim y = file.AddDim("y", 2);
                NcDim z = file.AddDim("z", 4);
                NcVar u = file.AddVar("u", NcFloat.Instance, new List<NcDim>() { time, x, y, z });
                NcVar v = file.AddVar("v", NcFloat.Instance, new List<NcDim>() { time, x, y, z });

                NcArray uArray = new NcArray(NcFloat.Instance, u.Shape);
                uArray.Fill(1);
                uArray.FillSlice(20, new int[] { 0, 0, 0, 3 }, new int[] { 2, 2, 2, 4});
                NcArray vArray = new NcArray(NcFloat.Instance, v.Shape);
                vArray.Fill(100);

                u.PutVar(uArray);

                v.PutVar(vArray);

                NcArray outArray = u.GetVar();
                Assert.True(uArray.Equals(outArray));
                
                outArray = v.GetVar();
                Assert.True(vArray.Equals(outArray));

                    

            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool TestCasting() {
            NcArray a = NcArray.Arange(NcInt.Instance, 2);
            double b = a.GetDoubleAt(0);
            Assert.Equals(b, 0.0);
            return true;
        }

    }
}


