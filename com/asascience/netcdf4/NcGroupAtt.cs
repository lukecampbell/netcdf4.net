
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcGroupAtt
 */

using System;
using System.Text;

namespace netcdf4 {
    public class NcGroupAtt : NcAtt {
        // null constructor
        public NcGroupAtt() : base() {
        }

        // copy constructor
        public NcGroupAtt(NcGroupAtt rhs) : base(rhs) {
        }

        // Constructor for an existing global attr
        public NcGroupAtt(NcGroup grp, int index) : base(false) {
            groupId = grp.GetId();
            varId = NC_GLOBAL;
            StringBuilder attName = new StringBuilder((int)NetCDF.netCDF_limits.NC_MAX_NAME);
            NcCheck.Check(NetCDF.nc_inq_attname(groupId, varId, index, attName));
            myName = attName.ToString();
        }

    }
}
