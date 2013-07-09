/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcType
 */

using System;
using netcdf4;

namespace netcdf4.test {
    public class TestNcDim : UnitTest {
        private string filePath = "nc_clobber.nc";
        public TestNcDim() {
            AddTest(test_add_dim, "test_add_dim");
        }

        public bool test_add_dim() {
            NcFile file = null;
            try {
                file = TestHelper.NewFile(filePath);
                NcDim dim1 = file.AddDim("time", 20);
                Assert.Equals(dim1.GetName(), "time");
                Assert.Equals(dim1.GetSize(), 20);
                Assert.Equals(file.GetDimCount(), 1);

            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }


    }
}


