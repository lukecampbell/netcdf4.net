/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcGroup
 */

using System;
using System.Collections.Generic;
using System.Linq;
using netcdf4;

namespace netcdf4.test {
    class TestNcGroup : UnitTest {
        public TestNcGroup() {
            AddTest(test_get_name, "test_get_name");
            AddTest(test_get_parent_group, "test_get_parent_group");
            AddTest(test_get_group_count, "test_get_group_count");
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

        public bool test_get_group_count() {
            NcGroup group;
            NcFile file;
            string filePath = "nc_clobber.nc";
            try {
                file = newFile(filePath);
                group = file;
                int groupCount;
                int oGroupCount=0;
                groupCount = group.GetGroupCount(GroupLocation.AllGrps);
                Assert.Equals(groupCount, 1); // Only the root group/file so no groups are defined
                NcGroup childGroup = group.AddGroup("child1");
                Assert.Equals(group.GetGroupCount(GroupLocation.AllGrps), 2);
                Dictionary<string, NcGroup> groups = group.GetGroups(GroupLocation.AllGrps);
                for(int i=0;i<groups.Count;i++) {
                    KeyValuePair<string, NcGroup> k = groups.ElementAt(i);
                    if(i==0)
                        Assert.Equals(k.Key, "/");
                    else if(i==1)
                        Assert.Equals(k.Key, "child1");
                }

            } finally {
                file.close();
            }

            CheckDelete(filePath);
            return true;
        }
    }
}

