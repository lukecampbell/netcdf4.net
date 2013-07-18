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
            AddTest(TestGet, "TestGet");
            AddTest(TestPut, "TestPut");
        }

        public bool TestSlicing() {

            int[] basicArray = new int[64];
            for(int i=0;i<64;i++) basicArray[i] = i;

            NcArray ncArray = new NcArray(basicArray, new int[] { 4,4,4});
            int[] outArray;

            ncArray.Slice(out outArray, new int[] { 2, 2, 2 }, new int[] { 4, 4, 4 });
            Assert.Equals(outArray, new int[] { 42, 43, 46, 47, 58, 59, 62, 63 });

            int val;
            ncArray.At(out val, new int[] { 2,2,2 } );
            Assert.Equals(val, 42);
            ncArray.ValueAt(out val, 2,2,2);
            Assert.Equals(val, 42);


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
                NcDim time = file.AddDim("time", 4);
                NcDim x = file.AddDim("x", 4);
                NcDim y = file.AddDim("y", 4);
                NcVar u = file.AddVar("u", NcFloat.Instance, new List<NcDim>() { time, x, y });
                NcVar v = file.AddVar("v", NcFloat.Instance, new List<NcDim>() { time, x, y });


            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
    }
}


