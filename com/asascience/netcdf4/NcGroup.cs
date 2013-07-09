
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcGroup
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace netcdf4 {
    public enum GroupLocation {
        ChildrenGrps, 
        ParentsGrps,
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


    public class NcGroup {
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
            if(location == GroupLocation.ParentsGrps || location == GroupLocation.ParentsAndCurrentGrps 
                                                            || location == GroupLocation.AllGrps) {
                Dictionary<string,NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
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
            if(location == GroupLocation.ParentsGrps || location == GroupLocation.ParentsAndCurrentGrps
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
            CheckNull();
            HashSet<NcGroup> retval = new HashSet<NcGroup>();
            foreach(KeyValuePair<string, NcGroup> k in GetGroups(location)) {
                if(name.Equals(k.Key))
                    retval.Add(k.Value);
            }
            return retval;
        }

        public NcGroup GetGroup(string name, GroupLocation location=GroupLocation.ChildrenGrps) {
            CheckNull();
            Dictionary<string, NcGroup> groups = GetGroups(location);
            foreach(KeyValuePair<string, NcGroup> k in groups) {
                if(k.Key.Equals(name))
                    return k.Value;
            }
            return new NcGroup(); // Null 
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
            CheckNull();
            int ndims =0;
            // Search current group
            if(location == Location.Current || location == Location.ParentsAndCurrent 
                                                            || location == Location.ChildrenAndCurrent 
                                                            || location == Location.All) {
                int ndimsp=0;
                NcCheck.Check(NetCDF.nc_inq_ndims(myId, ref ndimsp));
                ndims += ndimsp;
            }
            // Search in parent group
            if(location == Location.Parents || location == Location.ParentsAndCurrent 
                                                            || location == Location.All) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    ndims += k.Value.GetDimCount();
                }
            }

            if(location == Location.Children || location == Location.ChildrenAndCurrent 
                                                            || location == Location.All) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    ndims += k.Value.GetDimCount();
                }
            }
            return ndims;
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

        // Adds a dimension of limited size
        public NcDim AddDim(string name, Int32 dimSize) {
            CheckNull();
            Int32 dimId=0;
            NcCheck.Check(NetCDF.nc_def_dim(myId, name, dimSize, ref dimId));
            return new NcDim(this, dimId);
        }

        // Adds a dimension of unlimited size
        public NcDim AddDim(string name) {
            CheckNull();
            Int32 dimId = 0;
            NcCheck.Check(NetCDF.nc_def_dim(myId, name, NetCDF.NC_UNLIMITED, ref dimId));
            return new NcDim(this, dimId);
        }

        public int GetTypeCount(Location location=Location.Current) {
            CheckNull();
            Int32 ntypes=0;
            // search in current group
            if(location == Location.Current || location == Location.ParentsAndCurrent 
                                                        || location == Location.ChildrenAndCurrent
                                                        || location == Location.All) {
                Int32 ntypesp=0;
                Int32[] typeidsp = null;
                NcCheck.Check(NetCDF.nc_inq_typeids(myId, ref ntypesp, typeidsp));
                ntypes += ntypesp;
            }

            // search in parent groups.
            if(location == Location.Parents || location == Location.ParentsAndCurrent 
                                                        || location == Location.All) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    ntypes += k.Value.GetTypeCount();
                }
            }

            // search in child groups
            if(location == Location.Children || location == Location.ChildrenAndCurrent
                                                        || location == Location.All) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    ntypes += k.Value.GetTypeCount();
                }
            }
            return ntypes;
        }

        public int GetTypeCount(NcTypeEnum enumType, Location location=Location.Current) {
            CheckNull();
            Int32 ntypes=0;
            // search in current group
            if(location == Location.Current || location == Location.ParentsAndCurrent 
                                                        || location == Location.ChildrenAndCurrent
                                                        || location == Location.All) {
                Int32 ntypesp=0;
                Int32[] typeidsp = null;
                NcCheck.Check(NetCDF.nc_inq_typeids(myId, ref ntypesp, typeidsp));
                typeidsp = new Int32[ntypesp];
                NcCheck.Check(NetCDF.nc_inq_typeids(myId, ref ntypesp, typeidsp));

                foreach(Int32 i in typeidsp) {
                    NcType typeTmp = new NcType(this,i);
                    if(typeTmp.GetTypeClass() == enumType)
                        ntypes++;
                }
            }

            // search in parent groups
            if(location == Location.Parents || location == Location.ParentsAndCurrent 
                                                        || location == Location.All) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    ntypes += k.Value.GetTypeCount(enumType);
                }
            }

            // search in children groups
            if(location == Location.Children || location == Location.ChildrenAndCurrent
                                                        || location == Location.All) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    ntypes += k.Value.GetTypeCount(enumType);
                }
            }
            return ntypes;
        }

        public Dictionary<string, NcType> GetTypes(Location location=Location.Current) {
            CheckNull();
            Dictionary<string, NcType> ncTypes = new Dictionary<string, NcType>();

            // search in current group
            if(location == Location.Current || location == Location.ParentsAndCurrent 
                                                        || location == Location.ChildrenAndCurrent
                                                        || location == Location.All) {
                Int32 typeCount = GetTypeCount();
                Int32[] typeIds = new Int32[typeCount];
                NcCheck.Check(NetCDF.nc_inq_typeids(myId, ref typeCount, typeIds));

                foreach(Int32 i in typeIds) {
                    NcType tmpType = new NcType(this, i);
                    ncTypes.Add(tmpType.GetName(), tmpType);
                }
            }

            // search in parent groups
            if(location == Location.Parents || location == Location.ParentsAndCurrent 
                                                        || location == Location.All) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    Dictionary<string, NcType> tmpTypes = k.Value.GetTypes();
                    ncTypes.Union(tmpTypes);
                }
            }

            // search in child groups
            if(location == Location.Children || location == Location.ChildrenAndCurrent
                                                        || location == Location.All) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    Dictionary<string, NcType> tmpTypes = k.Value.GetTypes();
                    ncTypes.Union(tmpTypes);
                }
            }
            return ncTypes;
        }

        public HashSet<NcType> GetTypes(string name, Location location=Location.Current) {
            CheckNull();
            Dictionary<string, NcType> types = GetTypes(location);
            HashSet<NcType> ncTypes = new HashSet<NcType>();
            foreach(KeyValuePair<string, NcType> k in types) {
                if(k.Key == name)
                    ncTypes.Add(k.Value);
            }
            return ncTypes;
        }

        public HashSet<NcType> GetTypes(NcTypeEnum enumType, Location location=Location.Current) {
            CheckNull();
            Dictionary<string, NcType> types = GetTypes(location);
            HashSet<NcType> ncTypes = new HashSet<NcType>();
            foreach(KeyValuePair<string, NcType> k in types) {
                if(k.Value.GetTypeClass() == enumType)
                    ncTypes.Add(k.Value);
            }
            return ncTypes;
        }

        public HashSet<NcType> GetTypes(string name, NcTypeEnum enumType, Location location=Location.Current) {
            CheckNull();
            Dictionary<string, NcType> types = GetTypes(location);
            HashSet<NcType> ncTypes = new HashSet<NcType>();
            foreach(KeyValuePair<string, NcType> k in types) {
                if(k.Value.GetTypeClass() == enumType && k.Key == name)
                    ncTypes.Add(k.Value);
            }
            return ncTypes;
        }

        public NcType GetType(string name, Location location=Location.Current) {
            CheckNull();
            if(name == "byte") return NcByte.Instance;
            if(name == "ubyte") return NcUbyte.Instance;
            if(name == "char") return NcChar.Instance;
            if(name == "short") return NcShort.Instance;
            if(name == "ushort") return NcUshort.Instance;
            if(name == "int") return NcInt.Instance;
            if(name == "uint") return NcUint.Instance;
            if(name == "int64") return NcInt64.Instance;
            if(name == "uint64") return NcUint64.Instance;
            if(name == "float") return NcFloat.Instance;
            if(name == "double") return NcDouble.Instance;
            if(name == "string") return NcString.Instance;
            Dictionary<string, NcType> types = GetTypes(location);
            foreach(KeyValuePair<string, NcType> k in types) {
                if(k.Key == name)
                    return k.Value;
            }
            return new NcType(); // null
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
