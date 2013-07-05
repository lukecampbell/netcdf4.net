using System;
using netcdf4;

namespace netcdf4.test {
    class TestNetCDF4Bridge : UnitTest {
		public TestNetCDF4Bridge() {
			addTest(test_netcdf_bridge);
		}

        public bool test_netcdf_bridge() {
            Console.WriteLine("Test works");
            string retval = NetCDF.nc_inq_libvers();
            if(retval != null) {
                Console.WriteLine(NetCDF.nc_inq_libvers());
                return true;
            }
            return false;
        }
    }
}


