using System;
using netcdf4;

namespace netcdf4.test {
    class TestNetCDF4Bridge : UnitTest {
		public TestNetCDF4Bridge() {
			addTest(test_netcdf_bridge);
            addTest(test_open);
		}

        public bool test_netcdf_bridge() {
            Console.WriteLine("test_netcdf_bridge...");
            string retval = NetCDF.nc_inq_libvers();
            if(retval != null) {
                Console.WriteLine(retval);
                return true;
            }
            return false;
        }
        public bool test_open() {
            Console.WriteLine("test_open...");
            string filePath = "nc_test.nc";
            if(!System.IO.File.Exists(filePath)) {
                Console.WriteLine("Test file: " + filePath + " was not found");
                return false;
            }
            NcFile ncFile = new NcFile(filePath, FileMode.read);
            return true;
        }
    }
}


