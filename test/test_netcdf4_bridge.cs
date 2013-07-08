using System;
using netcdf4;

namespace netcdf4.test {
    class TestNetCDF4Bridge : UnitTest {
		public TestNetCDF4Bridge() {
			AddTest(test_netcdf_bridge, "test_netcdf_bridge");
		}

        public bool test_netcdf_bridge() {
            string retval = NetCDF.libvers();
            Assert.NotNull(retval);
            return true;
        }
    }
}


