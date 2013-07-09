
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcEnumType
 */
using System;
using System.Text;
namespace netcdf4 {
    public class NcEnumType : NcType {
        public NcEnumType() : base() {
        }

        public NcEnumType(NcEnumType rhs) : base(rhs) {
        }

        public NcEnumType(NcGroup grp, string name) : base(grp,name) {
        }
        public NcType GetBaseType() {
            StringBuilder charName = new StringBuilder((int)NetCDF.netCDF_limits.NC_MAX_NAME);
            Int32 base_nc_typep=0;
            Int32 base_sizep=0;
            Int32 num_membersp=0;


            return null;
        }
    }
}
