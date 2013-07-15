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
            Int32 base_nc_typep=0;
            Int32 datum_sizep=0;
            NcCheck.Check(NetCDF.nc_inq_vlen(groupId,  myId, buffer, ref datum_sizep, ref base_nc_typep));
            switch((NcTypeEnum)base_nc_typep) {
                case NcTypeEnum.NC_BYTE:   return NcByte.Instance;
                case NcTypeEnum.NC_UBYTE:  return NcUbyte.Instance;
                case NcTypeEnum.NC_CHAR:   return NcChar.Instance;
                case NcTypeEnum.NC_SHORT:  return NcShort.Instance;
                case NcTypeEnum.NC_USHORT: return NcUshort.Instance;
                case NcTypeEnum.NC_INT:    return NcInt.Instance;
                case NcTypeEnum.NC_UINT:   return NcUint.Instance;
                case NcTypeEnum.NC_INT64:  return NcInt64.Instance;
                case NcTypeEnum.NC_UINT64: return NcUint64.Instance;
                case NcTypeEnum.NC_FLOAT:  return NcFloat.Instance;
                case NcTypeEnum.NC_DOUBLE: return NcDouble.Instance;
                case NcTypeEnum.NC_STRING: return NcString.Instance;
                default:
                    return new NcType(GetParentGroup(), base_nc_typep);
            }
            return new NcType(); // null
        }
    }
}
