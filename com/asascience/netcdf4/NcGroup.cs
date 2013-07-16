
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
                throw new exceptions.NcNullGrp("Attempt to invoke NcGroup method on a Null group");
            }
        }
    
        public int GetGroupCount(GroupLocation location=GroupLocation.ChildrenGrps) {
            CheckNull();
            int ngroups=0;
            // record this group
            if(location == GroupLocation.ParentsAndCurrentGrps || location == GroupLocation.AllGrps) {
                ngroups++;
            }

            // search in current group
            if(location == GroupLocation.ChildrenGrps || location == GroupLocation.AllChildrenGrps 
                                                            || location == GroupLocation.AllGrps)  {
                int numgrps=0;
                int[] ncids=null;
                NcCheck.Check(NetCDF.nc_inq_grps(myId, ref numgrps, ncids));
                ngroups += numgrps;
            }
            // search in parent groups
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

                NcCheck.Check(NetCDF.nc_inq_grps(myId, ref numgrps, ncids));
                for(int i=0; i<groupCount; i++) {
                    NcGroup tmpGroup = new NcGroup(ncids[i]);
                    ncGroups.Add(tmpGroup.GetName(), tmpGroup);
                }
            }
            // search in parent groups.
            if(location == GroupLocation.ParentsGrps || location == GroupLocation.ParentsAndCurrentGrps
                                                            || location == GroupLocation.AllGrps) {
                NcGroup tmpGroup = GetParentGroup();
                while(tmpGroup != null && !tmpGroup.IsNull()) {
                    ncGroups.Add(tmpGroup.GetName(), tmpGroup);
                    tmpGroup = tmpGroup.GetParentGroup();
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
            int nvars=0;
            if(LocationIsCurrentGroup(location)) {
                NcCheck.Check(NetCDF.nc_inq_nvars(myId, ref nvars));
            }

            if(LocationIsParentGroup(location)) {
                foreach(KeyValuePair<string,NcGroup> g in GetGroups(GroupLocation.ParentsGrps)) {
                    nvars += g.Value.GetVarCount();
                }
            }

            if(LocationIsChildGroup(location)) {
                foreach(KeyValuePair<string, NcGroup> g in GetGroups(GroupLocation.AllChildrenGrps)) {
                    nvars += g.Value.GetVarCount();
                }
            }
            return nvars;
        }

        public Dictionary<string, NcVar> GetVars(Location location=Location.Current) {
            Dictionary<string, NcVar> vars = new Dictionary<string, NcVar>();
            if(LocationIsCurrentGroup(location)) {
                int varCount = GetVarCount();
                Int32[] varIds = new Int32[varCount];
                NcCheck.Check(NetCDF.nc_inq_varids(myId, ref varCount, varIds));
                foreach(Int32 varId in varIds) {
                    NcVar tmpVar = new NcVar(this, varId);
                    vars.Add(tmpVar.GetName(), tmpVar);
                }
            }
            if(LocationIsParentGroup(location)) {
                foreach(KeyValuePair<string, NcGroup> g in GetGroups(GroupLocation.ParentsGrps)) {
                    g.Value.GetVars().ToList().ForEach(x => vars[x.Key] = x.Value);
                }
            }
            if(LocationIsChildGroup(location)) {
                foreach(KeyValuePair<string, NcGroup> g in GetGroups(GroupLocation.AllChildrenGrps)) {
                    g.Value.GetVars().ToList().ForEach(x => vars[x.Key] = x.Value);
                }
            }
            return vars;
        }

        public HashSet<NcVar> GetVars(string name, Location location=Location.Current) {
            HashSet<NcVar> ncVars = new HashSet<NcVar>();
            foreach(KeyValuePair<string, NcVar> k in GetVars(location)) {
                if(k.Key == name)
                    ncVars.Add(k.Value);
            }
            return ncVars;
        }

        public NcVar GetVar(string name, Location location=Location.Current) {
            foreach(KeyValuePair<string, NcVar> k in GetVars(location)) {
                if(k.Key == name)
                    return k.Value;
            }
            return new NcVar(); // return null
        }

        // Add a new netCDF variable
        public NcVar AddVar(string name, string typeName, string dimName) {
            CheckNull();
            NcType tmpType = GetType(typeName, Location.ParentsAndCurrent);
            if(tmpType.IsNull())
                throw new exceptions.NcNullType("Attempt to invoke NcGroup.AddVar failed: typeName must be defined in either the current group or a parent group");

            NcDim tmpDim = GetDim(dimName, Location.ParentsAndCurrent);
            if(tmpDim.IsNull())
                throw new exceptions.NcNullDim("Attempt to invoke NcGroup.AddVar failed: dimName must be defined in either the current group or a parent group");

            Int32 varId=0;
            Int32[] dimId = {tmpDim.GetId()};
            NcCheck.Check(NetCDF.nc_def_var(myId, name, (NetCDF.nc_type)tmpType.GetId(), 1, dimId, ref varId));
            return new NcVar(this, varId);
        }

        public NcVar AddVar(string name, NcType ncType, NcDim ncDim) {
            CheckNull();
            if(ncType.IsNull())
                throw new exceptions.NcNullType("Attempt to invoke NcGroup.AddVar failed: NcType must be defined in either the current group or a parent group");
            NcType tmpType = GetType(ncType.GetName(), Location.ParentsAndCurrent);
            if(tmpType.IsNull())
                throw new exceptions.NcNullType("Attempt to invoke NcGroup.AddVar failed: NcType must be defined in either the current group or a parent group");

            if(ncDim.IsNull())
                throw new exceptions.NcNullDim("Attempt to invoke NcGroup.AddVar failed: NcDim must be defined in either the current group or a parent group");
            NcDim tmpDim = GetDim(ncDim.GetName(), Location.ParentsAndCurrent);
            if(tmpDim.IsNull())
                throw new exceptions.NcNullDim("Attempt to invoke NcGroup.AddVar failed: NcDim must be defined in either the current group or a parent group");

            Int32 varId=0;
            Int32[] dimId = {tmpDim.GetId()};
            NcCheck.Check(NetCDF.nc_def_var(myId, name, (NetCDF.nc_type)tmpType.GetId(), 1, dimId, ref varId));
            return new NcVar(this, varId);
        }

        public NcVar AddVar(string name, string typeName, List<string> dimNames) {
            CheckNull();
            NcType tmpType = GetType(typeName, Location.ParentsAndCurrent);
            if(tmpType.IsNull())
                throw new exceptions.NcNullType("Attempt to invoke NcGroup.AddVar failed: NcType must be defined in either the current group or a parent group");

            Int32[] dimIds = new Int32[dimNames.Count];
            for(int i=0;i<dimNames.Count;i++) {
                NcDim tmpDim = GetDim(dimNames[i], Location.ParentsAndCurrent);
                if(tmpDim.IsNull())
                    throw new exceptions.NcNullDim("Attempt to invoke NcGroup.AddVar failed: NcDim must be defined in either the current group or a parent group");
                dimIds[i] = tmpDim.GetId();
            }

            Int32 varId=0;
            NcCheck.Check(NetCDF.nc_def_var(myId, name, (NetCDF.nc_type)tmpType.GetId(), dimIds.Length, dimIds, ref varId));
            return new NcVar(this, varId);
        }

        public NcVar AddVar(string name, NcType ncType, List<NcDim> ncDimVector) {
            CheckNull();
            NcType tmpType=null;
            Int32 varId=0;
            Int32[] dimIds = new Int32[ncDimVector.Count];
            if(ncType.IsNull())
                throw new exceptions.NcNullType("Attempt to invoke NcGroup.AddVar with a Null NcType object");
            tmpType = GetType(ncType.GetName(), Location.ParentsAndCurrent);
            if(tmpType.IsNull())
                throw new exceptions.NcNullType("Attempt to invoke NcGroup.AddVar failed: NcType must be defined in either the current group or a parent group");
            for(int i=0;i<ncDimVector.Count;i++) {
                if(ncDimVector[i].IsNull())
                    throw new exceptions.NcNullDim("Attempt to invoke NcGroup.AddVar failed: NcType must be defined in either the current group or a parent group");
                NcDim tmpDim = GetDim(ncDimVector[i].GetName(), Location.ParentsAndCurrent);
                if(tmpDim.IsNull())
                    throw new exceptions.NcNullDim("Attempt to invoke NcGroup::addVar failed: NcDim must be defined in either the current group or a parent group");
                dimIds[i] = ncDimVector[i].GetId();
            }
            NcCheck.Check(NetCDF.nc_def_var(myId, name, (NetCDF.nc_type)tmpType.GetId(), ncDimVector.Count, dimIds, ref varId));

            return new NcVar(this, varId);
        }

        public int GetAttCount(Location location=Location.Current) {
            CheckNull();
            Int32 ngatts = 0;
            if(LocationIsCurrentGroup(location)){ 
                NcCheck.Check(NetCDF.nc_inq_natts(myId, ref ngatts));
            }

            if(LocationIsParentGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
                foreach(KeyValuePair<string, NcGroup> g in groups) {
                    ngatts += g.Value.GetAttCount();
                }
            }
            

            if(LocationIsChildGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> g in groups) {
                    ngatts += g.Value.GetAttCount();
                }
            }
            return ngatts;
        }

        public Dictionary<string, NcGroupAtt> GetAtts(Location location=Location.Current) {
            CheckNull();
            Dictionary<string, NcGroupAtt> ncAtts = new Dictionary<string, NcGroupAtt>();

            if(LocationIsCurrentGroup(location)) {
                int attCount = GetAttCount();
                for(int i=0;i<attCount;i++) {
                    NcGroupAtt tmpGroupAtt = new NcGroupAtt(this, i);
                    ncAtts.Add(tmpGroupAtt.GetName(), tmpGroupAtt);
                }
            }

            if(LocationIsParentGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
                foreach(KeyValuePair<string, NcGroup> g in groups) {
                    g.Value.GetAtts().ToList().ForEach(x => ncAtts[x.Key] = x.Value);
                }
            }
            if(LocationIsChildGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> g in groups) {
                    g.Value.GetAtts().ToList().ForEach(x => ncAtts[x.Key] = x.Value);
                }
            }
            return ncAtts;
        }

        public HashSet<NcGroupAtt> GetAtts(string name, Location location=Location.Current) {
            CheckNull();
            HashSet<NcGroupAtt> attSet = new HashSet<NcGroupAtt>();
            Dictionary<string, NcGroupAtt> ncAtts = GetAtts(location);
            foreach(KeyValuePair<string, NcGroupAtt> k in ncAtts) {
                if(k.Key == name)
                    attSet.Add(k.Value);
            }
            return attSet;
        }

        public NcGroupAtt GetAtt(string name, Location location=Location.Current) {
            CheckNull();
            Dictionary<string, NcGroupAtt> ncAtts = GetAtts(location);
            foreach(KeyValuePair<string, NcGroupAtt> k in ncAtts) {
                if(k.Key == name) {
                    return k.Value;
                }
            }
            return new NcGroupAtt(); // null
        }

        public NcGroupAtt PutAtt(string name, string dataValues) {
            CheckNull();
            NcCheck.Check(NetCDF.nc_put_att_text(myId, NcAtt.NC_GLOBAL, name, dataValues.Length, dataValues));
            return GetAtt(name);
        }
        public NcGroupAtt PutAtt(string name, NcType type, sbyte datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_schar(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type) type.GetId(), 1, new sbyte[] { datumValue }));
            return GetAtt(name);
        }

        public NcGroupAtt PutAtt(string name, NcType type, byte datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_uchar(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type) type.GetId(), 1, new byte[] { datumValue }));
            return GetAtt(name);
        }

        
        public NcGroupAtt PutAtt(string name, NcType type, Int16 datumValue) {
            CheckNull();
            //TODO: Support for VLEN | OPAQUE | ENUM | COMPOUND
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_short(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), 1, new Int16[] { datumValue }));
            return GetAtt(name);
        }

        public NcGroupAtt PutAtt(string name, NcType type, UInt16 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_ushort(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), 1, new UInt16[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcGroupAtt PutAtt(string name, NcType type, Int32 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_int(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), 1, new Int32[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcGroupAtt PutAtt(string name, NcType type, UInt32 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_uint(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), 1, new UInt32[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcGroupAtt PutAtt(string name, NcType type, Int64 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_longlong(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), 1, new Int64[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcGroupAtt PutAtt(string name, NcType type, UInt64 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_ulonglong(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), 1, new UInt64[] { datumValue }));
            return GetAtt(name);
        }

        public NcGroupAtt PutAtt(string name, NcType type, float datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_float(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), 1, new float[] { datumValue }));
            return GetAtt(name);
        }

        public NcGroupAtt PutAtt(string name, NcType type, double datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_double(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), 1, new double[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcGroupAtt PutAtt(string name, NcType type, sbyte[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_schar(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type) type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }

        public NcGroupAtt PutAtt(string name, NcType type, byte[] dataValues) {
            CheckNull();
            if(!type.IsFixedType()) {
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            }
            NcCheck.Check(NetCDF.nc_put_att_uchar(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type) type.GetId(), dataValues.Length, dataValues ));
            return GetAtt(name);
        }

        
        public NcGroupAtt PutAtt(string name, NcType type, Int16[] dataValues) {
            CheckNull();
            //TODO: Support for VLEN | OPAQUE | ENUM | COMPOUND
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_short(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), dataValues.Length, dataValues ));
            return GetAtt(name);
        }

        public NcGroupAtt PutAtt(string name, NcType type, UInt16[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_ushort(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), dataValues.Length, dataValues ));
            return GetAtt(name);
        }
        
        public NcGroupAtt PutAtt(string name, NcType type, Int32[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_int(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), dataValues.Length, dataValues ));
            return GetAtt(name);
        }
        
        public NcGroupAtt PutAtt(string name, NcType type, UInt32[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_uint(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), dataValues.Length, dataValues ));
            return GetAtt(name);
        }
        
        public NcGroupAtt PutAtt(string name, NcType type, Int64[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_longlong(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), dataValues.Length, dataValues ));
            return GetAtt(name);
        }
        
        public NcGroupAtt PutAtt(string name, NcType type, UInt64[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_ulonglong(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), dataValues.Length, dataValues ));
            return GetAtt(name);
        }

        public NcGroupAtt PutAtt(string name, NcType type, float[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_float(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), dataValues.Length, dataValues ));
            return GetAtt(name);
        }

        public NcGroupAtt PutAtt(string name, NcType type, double[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_double(myId, NcAtt.NC_GLOBAL, name, (NetCDF.nc_type)type.GetId(), dataValues.Length, dataValues ));
            return GetAtt(name);
        }

        public int GetDimCount(Location location=Location.Current) {
            CheckNull();
            int ndims =0;
            // Search current group
            if(LocationIsCurrentGroup(location)) {
                int ndimsp=0;
                NcCheck.Check(NetCDF.nc_inq_ndims(myId, ref ndimsp));
                ndims += ndimsp;
            }
            // Search in parent group
            if(LocationIsParentGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    ndims += k.Value.GetDimCount();
                }
            }

            if(LocationIsChildGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    ndims += k.Value.GetDimCount();
                }
            }
            return ndims;
        }

        private bool LocationIsCurrentGroup(Location location) {
            return (location == Location.Current || location == Location.ParentsAndCurrent 
                                                            || location == Location.ChildrenAndCurrent 
                                                            || location == Location.All);
        }

        private bool LocationIsParentGroup(Location location) {
            return (location == Location.Parents || location == Location.ParentsAndCurrent 
                                                            || location == Location.All);
        }

        private bool LocationIsChildGroup(Location location) {
            return (location == Location.Children || location == Location.ChildrenAndCurrent 
                                                            || location == Location.All);
        }

        // Get the dictionary (map) of NcDim objects
        public Dictionary<string, NcDim> GetDims(Location location=Location.Current) {
            CheckNull();
            Dictionary<string, NcDim> ncDims = new Dictionary<string, NcDim>();

            // search current group
            if(LocationIsCurrentGroup(location)) {
                Int32 dimCount = GetDimCount();
                Int32[] dimIds = new Int32[dimCount];
                NcCheck.Check(NetCDF.nc_inq_dimids(myId, ref dimCount, dimIds, 0));
                for(int i=0;i<dimCount;i++) {
                    NcDim tmpDim = new NcDim(this, dimIds[i]);
                    ncDims.Add(tmpDim.GetName(), tmpDim);
                }
            }

            if(LocationIsParentGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    k.Value.GetDims().ToList().ForEach(x => ncDims[x.Key] = x.Value);
                }
            }

            if(LocationIsChildGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    k.Value.GetDims().ToList().ForEach(x => ncDims[x.Key] = x.Value);
                }
            }
            return ncDims;
        }

        // Get all NcDim objects with a given name
        public HashSet<NcDim> GetDims(string name, Location location=Location.Current) {
            CheckNull();
            HashSet<NcDim> dimSet = new HashSet<NcDim>();
            Dictionary<string, NcDim> ncDims = GetDims(location);
            foreach(KeyValuePair<string, NcDim> k in ncDims) {
                if(k.Key == name)
                    dimSet.Add(k.Value);
            }
            return dimSet;
        }

        public NcDim GetDim(string name, Location location=Location.Current) {
            CheckNull();
            Dictionary<string, NcDim> ncDims = GetDims(location);
            foreach(KeyValuePair<string, NcDim> k in ncDims) {
                if(k.Key == name)
                    return k.Value;
            }
            return new NcDim(); // null dim
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
                    k.Value.GetTypes().ToList().ForEach(x => ncTypes[x.Key] = x.Value);
                }
            }

            // search in child groups
            if(location == Location.Children || location == Location.ChildrenAndCurrent
                                                        || location == Location.All) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string, NcGroup> k in groups) {
                    k.Value.GetTypes().ToList().ForEach(x => ncTypes[x.Key] = x.Value);
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

        public NcEnumType AddEnumType(string name, NcEnumType.Types baseType) {
            CheckNull();
            Int32 typeId=0;
            NcCheck.Check(NetCDF.nc_def_enum(myId, (int)baseType, name, ref typeId));
            NcEnumType tmp = new NcEnumType(this, name);
            return tmp;
        }

        public NcVlenType AddVlenType(string name, NcType baseType) {
            CheckNull();
            Int32 typeId=0;
            NcCheck.Check(NetCDF.nc_def_vlen(myId, name, (NetCDF.nc_type)baseType.GetId(), ref typeId));
            NcVlenType ncTypeTmp = new NcVlenType(this, name);
            return ncTypeTmp;
        }
        
        public NcOpaqueType AddOpaqueType(string name, Int32 size) {
            CheckNull();
            Int32 typeid=0;
            NcCheck.Check(NetCDF.nc_def_opaque(myId, size, name, ref typeid));
            return new NcOpaqueType(this, name);
        }

        public NcCompoundType AddCompoundType(string name, long size) {
            throw new NotImplementedException("AddCompoundType() not implemented");
            return null;
        }

        public Dictionary<string, NcGroup> GetCoordVars(Location location=Location.Current) {
            Dictionary<string, NcGroup> coordVars = new Dictionary<string, NcGroup>();
            if(LocationIsCurrentGroup(location)) {
                foreach(KeyValuePair<string, NcDim> dim in GetDims()) {
                    if(GetVars().ContainsKey(dim.Key))
                        coordVars.Add(dim.Key, this);
                }
            }

            if(LocationIsParentGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.ParentsGrps);
                foreach(KeyValuePair<string,NcGroup> k in groups) {
                    k.Value.GetCoordVars().ToList().ForEach(x => coordVars[x.Key] = x.Value);
                }
            }

            if(LocationIsChildGroup(location)) {
                Dictionary<string, NcGroup> groups = GetGroups(GroupLocation.AllChildrenGrps);
                foreach(KeyValuePair<string,NcGroup> k in groups) {
                    k.Value.GetCoordVars().ToList().ForEach(x => coordVars[x.Key] = x.Value);
                }
            }
            return coordVars;
        }

        public void GetCoordVar(string coordVarName, ref NcDim ncDim, ref NcVar ncVar, Location location = Location.Current) {
            throw new NotImplementedException("GetCoordVar() not implemented");
        }


    }
}
