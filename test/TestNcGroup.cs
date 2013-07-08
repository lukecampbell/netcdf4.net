/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcGroup
 */

using System;
using netcdf4;

namespace netcdf4.test {
    class TestNcGroup : UnitTest {
        public TestNcGroup() {
            AddTest(test_get_name, "test_get_name");
            AddTest(test_get_parent_group, "test_get_parent_group");
        }

        public NcFile newFile(string filePath) {
            NcFile file;
            CheckDelete(filePath);
            file = new NcFile(filePath, FileMode.replace, FileFormat.nc4);
            return file;
        }


        public bool test_get_name() {
            string filePath = "nc_clobber.nc";
            string groupName;
            int id;
            CheckDelete(filePath);
            NcFile file;
            NcGroup group;
            try {
                file = newFile(filePath);
                id = file.GetId();
                Assert.NotEquals(id,0);
                group = file;
                groupName = group.GetName();
                Assert.Equals(groupName, "/");
                Assert.True(group.IsRootGroup());
            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool test_get_parent_group() {
            NcGroup group;
            NcFile file;
            string filePath = "nc_clobber.nc";

            group = new NcGroup();
            try {
                NcGroup grpTest = group.GetParentGroup();
                throw new AssertFailedException("NcNullGrp not thrown");
            } catch (exceptions.NcNullGrp e) {
            } catch (Exception e) {
                throw new AssertFailedException("NcNullGrp not thrown");
            }

            try {
                file = newFile(filePath);
                group = file.GetParentGroup();
                Assert.True(group.IsNull());
            } finally {
                file.close();
            }
            return true;
        }
    }
}

