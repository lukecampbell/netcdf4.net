using System;
using netcdf4;

namespace netcdf4.test {
    class TestNetCDF4Bridge : UnitTest {
		public TestNetCDF4Bridge() {
			addTest(test_netcdf_bridge);
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
    }
}


