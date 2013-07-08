using System;
using netcdf4.test;

namespace netcdf4
{
    class RunTests
    {
        public static void Main (string[] args)
        {
            //UnitTest test_netcdf4_bridge = new TestNetCDF4Bridge ();
            UnitTest test_file = new TestNcFile();
            UnitTest test_group = new TestNcGroup();
            //test_netcdf4_bridge.Run ();
            test_file.Run();
            test_group.Run();
        }
    }
}
