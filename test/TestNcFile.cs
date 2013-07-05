using System;
using netcdf4;

namespace netcdf4.test {
    class TestNcFile : UnitTest {
        public TestNcFile() {
            addTest(test_open_create);
            addTest(test_open_nocreate);
        }

        public bool test_open_create() {
            Console.Write("test_open_create...");
            string filePath = "nc_clobber.nc";
            int id;
            if(System.IO.File.Exists(filePath)) {
                System.IO.File.Delete(filePath);
            }
            NcFile file = new NcFile(filePath, FileMode.replace, FileFormat.nc4);
            id = file.GetId();
            file = null;
            if(System.IO.File.Exists(filePath))
                return true;
            Console.Write("NetCDF4 File Failed to be created");
            return false;
        }

        public bool test_open_nocreate() {
            Console.Write("test_open_nocreate...SKIP");
            return true;
        }
    }
}
