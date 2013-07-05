using System;
using netcdf4;

namespace netcdf4.test {
    class TestNetCDF4Bridge : UnitTest {
		public TestNetCDF4Bridge() {
			addTest(test_netcdf_bridge);
            addTest(test_open);
		}

        public bool test_netcdf_bridge() {
            Console.Write("test_netcdf_bridge...");
            string retval = NetCDF.nc_inq_libvers();
            if(retval != null) {
                Console.Write(retval);
                return true;
            }
            return false;
        }
        public bool test_open() {
            Console.Write("test_open...");
            string filePath = "nc_test.nc";
            if(!System.IO.File.Exists(filePath)) {
                Console.Write("Test file: " + filePath + " was not found");
                return false;
            }
            NcFile ncFile = new NcFile(filePath, FileMode.read);
            return true;
        }
    }
}


