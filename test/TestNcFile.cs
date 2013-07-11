/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcFile
 */
using System;
using netcdf4;

namespace netcdf4.test {
    class TestNcFile : UnitTest {
        public TestNcFile() {
            AddTest(test_open_create, "test_open_create");
            AddTest(test_open_nocreate, "test_open_nocreate");
        }

        public bool test_open_create() {
            string filePath = "nc_clobber.nc";
            int id;
            CheckDelete(filePath);
            NcFile file = new NcFile(filePath, FileMode.replace, FileFormat.nc4);
            id = file.GetId();
            file.Close();
            if(System.IO.File.Exists(filePath)) {
                System.IO.File.Delete(filePath);
                return true;
            }
            Console.Write("NetCDF4 File Failed to be created");
            return false;
        }

        public bool test_open_nocreate() {
            // Verify that the file does exist, if not skip
            string filePath = "nc_test.nc";
            if(!System.IO.File.Exists(filePath)) {
                Console.WriteLine("\n\tTest file is missing. Skipping");
                return true;
            }

            NcFile file = new NcFile(filePath, FileMode.read);
            Int32 id = file.GetId();
            file.Close();
            file = null;
            return true;
        }
    }
}

