/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.test.TestData
 */

using netcdf4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netcdf4.test {
    class TestData : UnitTest {
        private const string filePath = "nc_clobber.nc";
        private const string dataFilePath = "test-data/201223618.nc";
        public TestData() {
            AddTest(TestVars, "TestVars");
        }

        public bool TestVars() {
            if(!System.IO.File.Exists(dataFilePath)) {
                Console.WriteLine("Unable to find sample data file.");
                return false;
            }
            NcFile dataFile = null;
            NcFile file = null;
            try {
                dataFile = new NcFile(dataFilePath, FileMode.read);
                Assert.Equals(dataFile.Format, FileFormat.classic64);
                file = TestHelper.NewFile(filePath);
                // Ensure there is only a flat group structure
                Assert.False(true);

                // Start with the attrs
                var attrs = dataFile.GetAtts(Location.Current);
                Assert.True(attrs.ContainsKey("Conventions") && attrs["Conventions"].GetValues() == "CF-1.0");
                Assert.True(attrs.ContainsKey("CoordinateProjection") && attrs["CoordinateProjection"].GetValues() == "none");
                Assert.True(attrs.ContainsKey("GroundWater_Forcing") && attrs["GroundWater_Forcing"].GetValues() == "GROUND WATER FORCING IS OFF!");
                foreach(var k in attrs) {
                    file.PutAtt(k.Value);
                }

                var fileAttrs = file.GetAtts(Location.Current);
                Assert.True(fileAttrs.ContainsKey("Conventions") && fileAttrs["Conventions"].GetValues() == "CF-1.0");
                Assert.True(fileAttrs.ContainsKey("CoordinateProjection") && fileAttrs["CoordinateProjection"].GetValues() == "none");
                Assert.True(fileAttrs.ContainsKey("GroundWater_Forcing") && fileAttrs["GroundWater_Forcing"].GetValues() == "GROUND WATER FORCING IS OFF!");


            } finally {
                dataFile.Close();
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
    }
}

