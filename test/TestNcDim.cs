/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcDim
 */

using System;
using netcdf4;

namespace netcdf4.test {
    public class TestNcDim : UnitTest {
        private string filePath = "nc_clobber.nc";
        public TestNcDim() {
            AddTest(TestAddDim, "TestAddDim");
            AddTest(TestUnlimitedDim, "TestUnlimitedDim");
        }

        public bool TestAddDim() {
            NcFile file = null;
            try {
                file = TestHelper.NewFile(filePath);
                NcDim dim1 = file.AddDim("time", 20);
                Assert.Equals(dim1.GetName(), "time");
                Assert.Equals(dim1.GetSize(), 20);
                Assert.Equals(file.GetDimCount(), 1);

            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }

        public bool TestUnlimitedDim() {
            NcFile file = null;
            NcDim time = null;
            NcVar timeVar = null;
            Int32[] readBuffer = new Int32[11];
            Int32[] writeBuffer = new Int32[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            try {
                file = TestHelper.NewFile(filePath);
                time = file.AddDim("time");
                timeVar = file.AddVar("t", NcInt64.Instance, time);
                timeVar.PutVar(new Int32[] { 0 }, new Int32[] { 11 }, writeBuffer);
                timeVar.GetVar(readBuffer);
                for(int i=0;i<11;i++)
                    Assert.Equals(readBuffer[i], (Int32)i);

            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
    }
}


