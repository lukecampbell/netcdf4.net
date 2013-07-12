/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcGroup
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using netcdf4;

namespace netcdf4.test {
    class TestNcGroup : UnitTest {
        protected const string filePath = "nc_clobber.nc";
        public TestNcGroup() {
            AddTest(test_get_name, "test_get_name");
            AddTest(test_get_parent_group, "test_get_parent_group");
            AddTest(test_get_group_count, "test_get_group_count");
            AddTest(test_get_groups, "test_get_groups");
            AddTest(TestVlen, "TestVlen");
        }

        public NcFile newFile(string filePath) {
            return TestHelper.NewFile(filePath);
        }


        public bool test_get_name() {
            string groupName;
            int id;
            CheckDelete(filePath);
            NcFile file=null;
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
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool test_get_parent_group() {
            NcGroup group;
            NcFile file=null;

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
                file.Close();
            }
            return true;
        }

        public bool test_get_group_count() {
            NcGroup group;
            NcFile file=null;
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
                file.Close();
            }

            CheckDelete(filePath);
            return true;
        }
        public bool test_get_groups() {
            NcGroup group;
            NcFile file=null;

            NcGroup child1_1;
            NcGroup child1_2;

            try {
                file = newFile(filePath);
                group = file;
                /* -- Check differentiability for GetGroup -- */
                child1_1 = file.AddGroup("group1");
                file.AddGroup("group2");

                group = file.GetGroup("group1");
                Assert.Equals(group.GetName(), "group1");
                /* -- Check that sets work for GetGroups -- */
                child1_2 = child1_1.AddGroup("group1"); // Second one named group1
                HashSet<NcGroup> groups = file.GetGroups("group1");
                foreach(NcGroup g in groups) {
                    if(g.GetId() != child1_1.GetId() && g.GetId() != child1_2.GetId()) {
                        throw new AssertFailedException("Incorrect group in set");
                    }
                }
            } finally {
                file.Close();
            }

            CheckDelete(filePath);
            return true;
        }
        public bool TestVlen() {
            NcFile file = null;
            NcVlenType vlen = null;
            NcDim dim = null;
            NcVar var = null;
            double[] vlenBuffer = new double[] { 0, 12, 4 };
            Vlen data = new Vlen(vlenBuffer);
            Vlen readVlen = null;
            VlenStruct vs;
            double[] readBuffer = new double[8];
            try {
                file = TestHelper.NewFile(filePath);
                vlen = file.AddVlenType("vlen", NcDouble.Instance);
                dim = file.AddDim("time", 1);
                var = file.AddVar("time", vlen, dim);
                var.PutVar(new Int32[] { 0 }, data);
                var.GetVar(new Int32[] { 0 }, ref readVlen);
                vs = readVlen.ToStruct();
                Marshal.Copy(vs.p, readBuffer, 0, vs.len);
                for(int i=0;i<vlenBuffer.Length;i++)
                    Assert.Equals(vlenBuffer[i], readBuffer[i]);

            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
    }
}

