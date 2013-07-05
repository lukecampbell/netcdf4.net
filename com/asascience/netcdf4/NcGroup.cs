
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
        protected int myId;
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
               NcCheck.check(NetCDF.nc_inq_grpname_len(myId, ref lenp));
               StringBuilder name = new StringBuilder();
               NcCheck.check(NetCDF.nc_inq_grpname_full(myId, ref lenp, name));
               groupName = name.ToString();
            }
            else {
				StringBuilder name = new StringBuilder ();
                NcCheck.check(NetCDF.nc_inq_grpname(myId, name));
                groupName = name.ToString();
            }
            return groupName;
        }

        public NcGroup GetParentGroup() {
            throw new NotImplementedException("GetParentGroup() not implemented");
            return null;
        }

        public int GetId() {
            return myId;
        }
    
        public int getGroupCount(GroupLocation location=GroupLocation.ChildrenGrps) {
            throw new NotImplementedException("GetGroupCount() not implemented");
            return 0;

        }

        public Dictionary<string, NcGroup> GetGroups(GroupLocation location=GroupLocation.ChildrenGrps) {
            throw new NotImplementedException("GetGroups() not implemented");
            return null;
        }

        public HashSet<NcGroup> GetGroups(string name, GroupLocation location=GroupLocation.ChildrenGrps) {
            throw new NotImplementedException("GetGroups() not implemented");
            return null;
        }

        public NcGroup GetGroup(string name, GroupLocation location=GroupLocation.ChildrenGrps) {
            throw new NotImplementedException("GetGroup() not implemented");
            return null;
        }

        public NcGroup addGroup(string name) {
            throw new NotImplementedException("AddGroup() not implemented");
            return null;
        }

        public bool IsNull() {
            return nullObject;
        }

        public bool isRootGroup() {
			throw new NotImplementedException("IsRootGroup() not impNotImplementedExceptionlemented");
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
