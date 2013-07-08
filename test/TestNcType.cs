/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcType
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using netcdf4;

namespace netcdf4.test {
    public class TestNcType : UnitTest {
        public TestNcType() {
            // Add tests
            AddTest(test_types, "test_types");
            AddTest(test_string_bridge, "test_string_bridge");
        }
        public bool test_types() {
            for(int i=1;i<13;i++) {
                NcType t = new NcType(i);
                Assert.Equals(t.GetId(), i);
                if(i==1) { // byte
                    Assert.Equals(t.GetName(), "byte");
                    Assert.Equals(t.GetSize(), 1); // A byte should be just one byte right?
                } else {
                    Assert.NotNull(t.GetName());
                    Assert.True(t.GetSize() > 0);
                }
            }

            return true;
        }
        public bool test_string_bridge() {
            StringBuilder charName = new StringBuilder(256);
            Int32 sizep = 0;
            NcCheck.Check(NetCDF.nc_inq_type(0, (NetCDF.nc_type)1, charName, ref sizep));
            return true;
        }
    }
}


