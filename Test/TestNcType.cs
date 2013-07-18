/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcType
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASA.NetCDF4;

namespace ASA.NetCDF4.Test {
    public class TestNcType : UnitTest {
        public TestNcType() {
            // Add tests
            AddTest(TestTypes, "TestTypes");
        }
        public bool TestTypes() {
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
    }
}


