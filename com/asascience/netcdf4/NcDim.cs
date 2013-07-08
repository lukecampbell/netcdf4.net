
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcDim
 */

using System;
using System.Text;


namespace netcdf4 {
    public class NcDim {
        public NcDim() {
            nullObject = true;
        }

        ~NcDim() {
        }

        public NcDim(NcGroup grp, Int32 dimId) {
            nullObject = false;
            groupId = grp.GetId();
            myId = dimId;
        }

        public NcDim(NcDim rhs) {
            nullObject = rhs.nullObject;
            myId = rhs.myId;
            groupId = rhs.groupId;
        }

        public string GetName() {
            CheckNull();
            StringBuilder dimName = new StringBuilder((int)NetCDF.netCDF_limits.NC_MAX_NAME);
            NcCheck.Check(NetCDF.nc_inq_dimname(groupId, myId, dimName));
            return dimName.ToString();
        }

        public Int32 GetId() {
            CheckNull();
            return myId;
        }

        public NcGroup GetParentGroup() {
            CheckNull();
            return new NcGroup(groupId);
        }

        public bool IsUnlimited() {
            CheckNull();
            Int32 numlimdims=0;
            Int32[] unlimdimidsp = null;
            NcCheck.Check(NetCDF.nc_inq_unlimdims(groupId, ref numlimdims, unlimdimidsp));
            unlimdimidsp = new Int32[numlimdims];
            NcCheck.Check(NetCDF.nc_inq_unlimdims(groupId, ref numlimdims, unlimdimidsp));
            for(int i=0;i<numlimdims;i++) {
                if(unlimdimidsp[i] == myId)
                    return true;
            }
            return false;
        }

        public Int32 GetSize() {
            CheckNull();
            Int32 dimSize=0;
            NcCheck.Check(NetCDF.nc_inq_dimlen(groupId, myId, ref dimSize));
            return dimSize;
        }

        public void Rename(string newName) {
            CheckNull();
            NcCheck.Check(NetCDF.nc_rename_dim(groupId, myId, newName));
        }

        public bool IsNull() {
            return nullObject;
        }
        protected void CheckNull() {
            if(IsNull()) {
                throw new exceptions.NcNullType("Attempt to invoke NcGroup.GetId on a Null group");
            }
        }

        private bool nullObject;
        private Int32 myId;
        private Int32 groupId;

    }
}
