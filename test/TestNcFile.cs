/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcFile
 */
using System;
using netcdf4;

namespace netcdf4.test {
    class TestNcFile : UnitTest {
        public TestNcFile() {
            AddTest(TestOpenCreate, "TestOpenCreate");
        }

        public bool TestOpenCreate() {
            string filePath = "nc_clobber.nc";
            CheckDelete(filePath);
            NcFile file = null;
            try {
                file = new NcFile(filePath, FileMode.replace, FileFormat.nc4);
            } finally { 
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
    }
}

