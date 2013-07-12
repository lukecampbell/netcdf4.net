
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcVarAtt
 */
using System;
using System.Text;

namespace netcdf4 {
    public class NcVarAtt : NcAtt {
        public NcVarAtt() : base() {
        }

        public NcVarAtt(NcVarAtt rhs) : base(rhs) {
        }

        public NcVarAtt(NcGroup grp, NcVar var, Int32 index) : base(false) {
            ASCIIEncoding encoder = new ASCIIEncoding();
            groupId = grp.GetId();
            varId = var.GetId();
            byte[] buffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME];
            NcCheck.Check(NetCDF.nc_inq_attname(groupId, varId, index, buffer));
            string sbuffer = encoder.GetString(buffer);
            myName = sbuffer.Substring(0, sbuffer.IndexOf('\0')); // A null-terminated C-string
        }

        public NcVarAtt(Int32 groupId, Int32 varId, Int32 index) : base(false) {
            ASCIIEncoding encoder = new ASCIIEncoding();
            this.groupId = groupId;
            this.varId = varId;
            byte[] buffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME];
            NcCheck.Check(NetCDF.nc_inq_attname(groupId, varId, index, buffer));
            string sbuffer = encoder.GetString(buffer);
            myName = sbuffer.Substring(0, sbuffer.IndexOf('\0')); // A null-terminated C-string
        }

        public NcVar GetParentVar() {
            return new NcVar(groupId, varId);
        }
    }
}
