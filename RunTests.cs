using System;
using netcdf4.test;

namespace netcdf4
{
    class RunTests
    {
        public static void Main (string[] args)
        {
            UnitTest test_netcdf4_bridge = new TestNetCDF4Bridge ();
            UnitTest test_file = new TestNcFile();
            test_netcdf4_bridge.Run ();
            test_file.Run();

        }
    }
}