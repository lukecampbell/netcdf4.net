
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcGroup
 */

using System;
using System.Text;
using System.Collections.Generic;

namespace netcdf4 {
    public enum GroupLocation {
        ChildrenGrps, 
        ParentGrps,
        ChildrenOfChildrenGrps,
        AllChildrenGrps,
        ParentsAndCurrentGrps,
        AllGrps
    }

    public enum Location {
        Current,
        Parents,
        Children,
        ParentsAndCurrent,
        ChildrenAndCurrent,
        All
    }


    class NcGroup {
        protected Int32 myId;
        protected bool nullObject;

        public NcGroup() {
            nullObject = true;
        }

        public NcGroup(int groupId) {
            myId = groupId;
        }
        public NcGroup(NcGroup rhs) {
            nullObject = rhs.nullObject;
            myId = rhs.myId;
        }

        ~NcGroup() {

        }

        public string GetName(bool fullName=false) {
            if(IsNull()) {
                throw new exceptions.NcNullGrp("Attempt to invoke NcGroup.GetName on a Null group");
            }
            string groupName;
            if(fullName) {
               Int32 lenp = new Int32();
               NcCheck.Check(NetCDF.nc_inq_grpname_len(myId, ref lenp));
               StringBuilder name = new StringBuilder();
               NcCheck.Check(NetCDF.nc_inq_grpname_full(myId, ref lenp, name));
               groupName = name.ToString();
            }
            else {
				StringBuilder name = new StringBuilder ();
                NcCheck.Check(NetCDF.nc_inq_grpname(myId, name));
                groupName = name.ToString();
            }
            return groupName;
        }

        public NcGroup GetParentGroup() {
            if(IsNull()) throw new exceptions.NcNullGrp("Attempt to invoke NcGroup.GetParentGroup on a Null group");
            try {
                int parentId=0;
                NcCheck.Check(NetCDF.nc_inq_grp_parent(myId, ref parentId));
                NcGroup ncGroupParent = new NcGroup(parentId);
                return ncGroupParent;
            } catch (exceptions.NcEnoGrp e) {
                return new NcGroup();
            }
        }

        public Int32 GetId() {
            return myId;
        }
        protected void CheckNull() {
            if(IsNull()) {
                throw new exceptions.NcNullGrp("Attempt to invoke NcGroup.GetId on a Null group");
            }
        }
    
        public int GetGroupCount(GroupLocation location=GroupLocation.ChildrenGrps) {
            CheckNull();
            int ngroups=0;
            // record this group
            if(location == GroupLocation.ParentsAndCurrentGrps || location == GroupLocation.AllGrps) {
                ngroups++;
            }

            // search in parent groups
            if(location == GroupLocation.ChildrenGrps || location == GroupLocation.AllChildrenGrps 
                                                            || location == GroupLocation.AllGrps)  {
                int numgrps=0;
                int[] ncids=null;
                NcCheck.Check(NetCDF.nc_inq_grps(myId, ref numgrps, ncids));
                ngroups += numgrps;
            }
            // search is parent groups
            if(location == GroupLocation.ParentGrps || location == GroupLocation.ParentsAndCurrentGrps 
                                                            || location == GroupLocation.AllGrps) {
                Dictionary<string,NcGroup> groups = GetGroups(GroupLocation.ParentGrps);
                ngroups += groups.Count;
            }

            // get the number of all children that are childreof children
            if(location == GroupLocation.ChildrenOfChildrenGrps || location == GroupLocation.AllChildrenGrps
                                                            || location == GroupLocation.AllGrps) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ChildrenOfChildrenGrps);
                ngroups += groups.Count;
            }
            return ngroups;
        }

        public Dictionary<string, NcGroup> GetGroups(GroupLocation location=GroupLocation.ChildrenGrps) {
            CheckNull();
            Dictionary<string, NcGroup> ncGroups = new Dictionary<string, NcGroup>();

            // Record this group
            if(location == GroupLocation.ParentsAndCurrentGrps || location == GroupLocation.AllGrps) {
                ncGroups.Add(GetName(), this);
            }

            // the child groups of the current group
            if(location == GroupLocation.ChildrenGrps || location == GroupLocation.AllChildrenGrps 
                                                            || location == GroupLocation.AllGrps ) {
                int groupCount = GetGroupCount();
                int[] ncids = new int[groupCount];
                int numgrps=0;

                NcCheck.Check(NetCDF.nc_inq_grps(GetId(), ref numgrps, ncids));
                for(int i=0; i<groupCount; i++) {
                    NcGroup tmpGroup = new NcGroup(ncids[i]);
                    ncGroups.Add(tmpGroup.GetName(), tmpGroup);
                }
            }
            // search in parent groups.
            if(location == GroupLocation.ParentGrps || location == GroupLocation.ParentsAndCurrentGrps
                                                            || location == GroupLocation.AllGrps) {
                NcGroup tmpGroup = this;
                if(!tmpGroup.IsRootGroup()) {
                    while(true) {
                        NcGroup parentGroup = tmpGroup.GetParentGroup();
                        if(parentGroup.IsNull())
                            break;
                        ncGroups.Add(parentGroup.GetName(), parentGroup);
                        tmpGroup = parentGroup;
                    }
                }
            }

            // search in child groups of the children
            if(location == GroupLocation.ChildrenOfChildrenGrps || location == GroupLocation.AllChildrenGrps
                                                            || location == GroupLocation.AllGrps) { 
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    foreach(KeyValuePair<string, NcGroup> kchild in k.Value.GetGroups(GroupLocation.AllChildrenGrps)) {
                        ncGroups.Add(kchild.Key, kchild.Value);
                    }
                }
            }
            return ncGroups;
        }

        public HashSet<NcGroup> GetGroups(string name, GroupLocation location=GroupLocation.ChildrenGrps) {
            throw new NotImplementedException("GetGroups() not implemented");
            return null;
        }

        public NcGroup GetGroup(string name, GroupLocation location=GroupLocation.ChildrenGrps) {
            throw new NotImplementedException("GetGroup() not implemented");
            return null;
        }

        public NcGroup AddGroup(string name) {
            CheckNull();
            int new_ncid=0;
            NcCheck.Check(NetCDF.nc_def_grp(myId, name, ref new_ncid));
            return new NcGroup(new_ncid);
        }

        public bool IsNull() {
            return nullObject;
        }

        public bool IsRootGroup() {
			if(GetName().Equals("/")) {
                return true;
            }
            return false;
        }

        public int GetVarCount(Location location=Location.Current) {
            throw new NotImplementedException("GetVarCount() not implemented");
            return 0;
        }

        public Dictionary<string, NcVar> GetVars(Location location=Location.Current) {
            throw new NotImplementedException("GetVars() not implemented");
            return null;
        }

        public HashSet<NcVar> GetVars(string name, Location location=Location.Current) {
            throw new NotImplementedException("GetVars() not implemented");
            return null;
        }

        public NcVar GetVar(string name, Location location=Location.Current) {
            throw new NotImplementedException("GetVar() not implemented");
            return null;
        }

        public NcVar AddVar(string name, string typeName, string dimName) {
            throw new NotImplementedException("AddVar() not implemented");
            return null;
        }

        public NcVar AddVar(string name, NcType ncType, NcDim ncDim) {
            throw new NotImplementedException("AddVar() not implemented");
            return null;
        }

        public NcVar AddVar(string name, string typeName, List<string> dimNames) {
            throw new NotImplementedException("AddVar() not implemented");
            return null;
        }

        public NcVar AddVar(string name, NcType ncType, List<NcDim> ncDimVector) {
            throw new NotImplementedException("AddVar() not implemented");
            return null;
        }

        public int GetAttCount(Location location=Location.Current) {
            throw new NotImplementedException("GetAttCount() not implemented");
            return 0;
        }

        public Dictionary<string, NcGroupAtt> GetAtts(Location location=Location.Current) {
            throw new NotImplementedException("GetAtts() not implemented");
            return null;
        }

        public HashSet<NcGroupAtt> GetAtts(string name, Location location=Location.Current) {
            throw new NotImplementedException("GetAtts() not implemented");
            return null;
        }

        public NcGroupAtt GetAtt(string name, Location location=Location.Current) {
            throw new NotImplementedException("GetAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, List<string> dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, string dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, NcType type, short datumValue) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, NcType type, int datumValue) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, NcType type, long datumValue) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, NcType type, float datumValue) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, NcType type, double datumValue) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, NcType type, ushort datumValue) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, NcType type, uint datumValue) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, NcType type, ulong datumValue) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcGroupAtt PutAtt(string name, NcType type, List<string> dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        /* TODO: Fill in the rest of the PutAtts */

        public int GetDimCount(Location location=Location.Current) {
            throw new NotImplementedException("GetDimCount() not implemented");
            return 0;
        }

        public Dictionary<string, NcDim> GetDims(Location location=Location.Current) {
            throw new NotImplementedException("GetDims() not implemented");
            return null;
        }

        public HashSet<NcDim> GetDims(string name, Location location=Location.Current) {
            throw new NotImplementedException("GetDims() not implemented");
            return null;
        }

        public NcDim GetDim(string name, Location location=Location.Current) {
            throw new NotImplementedException("GetDim() not implemented");
            return null;
        }

        public NcDim AddDim(string name, long dimSize) {
            throw new NotImplementedException("AddDim() not implemented");
            return null;
        }

        public NcDim AddDim(string name) {
            throw new NotImplementedException("AddDim() not implemented");
            return null;
        }

        public int GetTypeCount(Location location=Location.Current) {
            throw new NotImplementedException("GetTypeCount() not implemented");
            return 0;
        }

        public int GetTypeCount(NcTypeEnum enumType, Location location=Location.Current) {
            throw new NotImplementedException("GetTypeCount() not implemented");
            return 0;
        }

        public Dictionary<string, NcType> GetTypes(Location location=Location.Current) {
            throw new NotImplementedException("GetTypes() not implemented");
            return null;
        }

        public HashSet<NcType> GetTypes(string name, Location location=Location.Current) {
            throw new NotImplementedException("GetTypes() not implemented");
            return null;
        }

        public HashSet<NcType> GetTypes(NcTypeEnum enumType, Location location=Location.Current) {
            throw new NotImplementedException("GetTypes() not implemented");
            return null;
        }

        public HashSet<NcType> GetTypes(string name, NcTypeEnum enumType, Location location=Location.Current) {
            throw new NotImplementedException("GetTypes() not implemented");
            return null;
        }

        public NcType GetType(string name, Location location=Location.Current) {
            throw new NotImplementedException("GetType() not implemented");
            return null;
        }

        /*
        public NcEnumType AddEnumType(string name, NcTypeEnum basetype) {
            throw new NotImplementedException("AddEnumType() not implemented");
            return null;
        }*/

        public NcVlenType AddVlenType(string name, NcType basetype) {
            throw new NotImplementedException("AddVlenType() not implemented");
            return null;
        }
        
        public NcOpaqueType AddOpaqueType(string name, long size) {
            throw new NotImplementedException("AddOpaqueType() not implemented");
            return null;
        }

        public NcCompoundType AddCompoundType(string name, long size) {
            throw new NotImplementedException("AddCompoundType() not implemented");
            return null;
        }

        public Dictionary<string, NcGroup> GetCoordVars(Location location=Location.Current) {
            throw new NotImplementedException("GetCoordVars() not implemented");
            return null;
        }

        public void GetCoordVar(string coordVarName, ref NcDim ncDim, ref NcVar ncVar, Location location = Location.Current) {
            throw new NotImplementedException("GetCoordVar() not implemented");
        }


    }
}
