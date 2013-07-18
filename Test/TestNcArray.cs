/*
 * Author: Luke Campbell <LCampbell@ASAScience.com>
 * ASA.NetCDF4.Test.TestNcArray
 */

using System;
using ASA.NetCDF4;

namespace ASA.NetCDF4.Test {
    public class TestNcArray : UnitTest {
        public TestNcArray() {
            AddTest(TestSlicing, "TestSlicing");
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

            return true;
        }
    }
}


