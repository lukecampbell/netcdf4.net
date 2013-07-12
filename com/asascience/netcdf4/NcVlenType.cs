/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcVlenType
 */
using System;
using System.Text;
namespace netcdf4 {
    public class NcVlenType : NcType {
        private static NcVlenType instance;
        public static NcVlenType Instance {
            get {
                if(instance==null)
                    instance = new NcVlenType();
                return instance;
            }
        }

        public NcVlenType() : base() {
        }


        public NcVlenType(NcGroup grp, string name) : base(grp,name) {
        }

        public NcVlenType(NcVlenType rhs) : base(rhs) {
        }

        public NcVlenType(NcType ncType) : base(ncType) {
            if(GetTypeClass() != NcTypeEnum.NC_VLEN)
                throw new exceptions.NcException("The NcType object must be the base of a Vlen type");
        }

        public NcType GetBaseType() {
            byte[] buffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME+1];
            NetCDF.nc_type base_nc_typep=(NetCDF.nc_type)0;
            Int32 datum_sizep=0;
            NcCheck.Check(NetCDF.nc_inq_vlen(groupId,  myId, buffer, ref datum_sizep, ref base_nc_typep));
            switch((int)base_nc_typep) {
                case (int)NcTypeEnum.NC_BYTE:
                    return NcByte.Instance;
                case (int)NcTypeEnum.NC_UBYTE:
                    return NcUbyte.Instance;
                case (int)NcTypeEnum.NC_CHAR:
                    return NcChar.Instance;
                case (int)NcTypeEnum.NC_SHORT:
                    return NcShort.Instance;
                case (int)NcTypeEnum.NC_USHORT:
                    return NcUshort.Instance;
                case (int)NcTypeEnum.NC_INT:
                    return NcInt.Instance;
                case (int)NcTypeEnum.NC_UINT:
                    return NcUint.Instance;
                case (int)NcTypeEnum.NC_INT64:
                    return NcInt64.Instance;
                case (int)NcTypeEnum.NC_UINT64:
                    return NcUint64.Instance;
                case (int)NcTypeEnum.NC_FLOAT:
                    return NcFloat.Instance;
                case (int)NcTypeEnum.NC_DOUBLE:
                    return NcDouble.Instance;
                case (int)NcTypeEnum.NC_STRING:
                    return NcString.Instance;
                default:
                    return new NcType(GetParentGroup(), (int)base_nc_typep);
            }
        }
    }
}
