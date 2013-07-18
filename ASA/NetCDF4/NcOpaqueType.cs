
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcOpaqueType
 */
using System;
namespace ASA.NetCDF4 {
    public class NcOpaqueType : NcType {

        public NcOpaqueType() : base() {
        }

        public NcOpaqueType(NcGroup grp, string name) : base(grp, name) {
        }

        public NcOpaqueType(NcType ncType) : base(ncType) {
            if(GetTypeClass() != NcTypeEnum.NC_OPAQUE)
                throw new exceptions.NcBadType("The NcType object must have a base type of NC_OPAQUE");
        }

        public NcOpaqueType(NcOpaqueType rhs) : base(rhs) {
        }

        public Int32 GetTypeSize() {
            byte[] buffer = null;
            Int32 sizep=0;
            NcCheck.Check(NetCDF.nc_inq_opaque(groupId, myId, buffer, ref sizep));
            return sizep;
        }
    }
}
