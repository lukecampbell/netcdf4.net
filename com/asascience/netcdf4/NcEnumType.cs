
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcEnumType
 */
using System;
using System.Text;
namespace netcdf4 {
    public class NcEnumType : NcType {
        public enum Types {
            NC_BYTE   = (int) NcTypeEnum.NC_BYTE,
            NC_SHORT  = (int) NcTypeEnum.NC_SHORT,
            NC_INT    = (int) NcTypeEnum.NC_INT,
            NC_UBYTE  = (int) NcTypeEnum.NC_UBYTE,
            NC_USHORT = (int) NcTypeEnum.NC_USHORT,
            NC_UINT   = (int) NcTypeEnum.NC_UINT,
            NC_INT64  = (int) NcTypeEnum.NC_INT64,
            NC_UINT64 = (int) NcTypeEnum.NC_UINT64
        };

        public NcEnumType() : base() {
        }

        public NcEnumType(NcEnumType rhs) : base(rhs) {
        }

        public NcEnumType(NcGroup grp, string name) : base(grp,name) {
        }

        public NcEnumType(NcType ncType ) : base(ncType) {
            if(GetTypeClass() != NcTypeEnum.NC_ENUM) 
                throw new exceptions.NcBadType("The NcType object must be of type NC_ENUM");
        }



        public NcType GetBaseType() {
            byte[] buffer = null;
            Int32 base_type = 0;
            Int32 base_sizep=0;
            Int32 num_membersp=0;

            NcCheck.Check(NetCDF.nc_inq_enum(groupId, myId, buffer, ref base_type, ref base_sizep, ref num_membersp));
            switch((NcTypeEnum)base_type) {
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
                    return new NcType(GetParentGroup(), (int) base_type);
            }

            return new NcType(); // Null
        }
        public Int32 GetMemberCount() {
            byte[] buffer = null;
            Int32 base_nc_typep = 0;
            Int32 base_sizep=0;
            Int32 num_membersp=0;
            NcCheck.Check(NetCDF.nc_inq_enum(groupId, myId, buffer, ref base_nc_typep, ref base_sizep, ref num_membersp));
            return num_membersp;
        }

        public string GetMemberNameFromValue(sbyte memberValue) {
            byte[] nameBuffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME+1];
            ASCIIEncoding encoder = new ASCIIEncoding();
            NcCheck.Check(NetCDF.nc_inq_enum_ident(groupId, myId, (Int64) memberValue, nameBuffer));
            string buf = encoder.GetString(nameBuffer);
            return buf.Substring(0, buf.IndexOf('\0'));
        }

        public string GetMemberNameFromValue(byte memberValue) {
            byte[] nameBuffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME+1];
            ASCIIEncoding encoder = new ASCIIEncoding();
            NcCheck.Check(NetCDF.nc_inq_enum_ident(groupId, myId, (Int64) memberValue, nameBuffer));
            string buf = encoder.GetString(nameBuffer);
            return buf.Substring(0, buf.IndexOf('\0'));
        }

        public string GetMemberNameFromValue(Int16 memberValue) {
            byte[] nameBuffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME+1];
            ASCIIEncoding encoder = new ASCIIEncoding();
            NcCheck.Check(NetCDF.nc_inq_enum_ident(groupId, myId, (Int64) memberValue, nameBuffer));
            string buf = encoder.GetString(nameBuffer);
            return buf.Substring(0, buf.IndexOf('\0'));
        }

        public string GetMemberNameFromValue(UInt16 memberValue) {
            byte[] nameBuffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME+1];
            ASCIIEncoding encoder = new ASCIIEncoding();
            NcCheck.Check(NetCDF.nc_inq_enum_ident(groupId, myId, (Int64) memberValue, nameBuffer));
            string buf = encoder.GetString(nameBuffer);
            return buf.Substring(0, buf.IndexOf('\0'));
        }

        public string GetMemberNameFromValue(Int32 memberValue) {
            byte[] nameBuffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME+1];
            ASCIIEncoding encoder = new ASCIIEncoding();
            NcCheck.Check(NetCDF.nc_inq_enum_ident(groupId, myId, (Int64) memberValue, nameBuffer));
            string buf = encoder.GetString(nameBuffer);
            return buf.Substring(0, buf.IndexOf('\0'));
        }

        public string GetMemberNameFromValue(UInt32 memberValue) {
            byte[] nameBuffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME+1];
            ASCIIEncoding encoder = new ASCIIEncoding();
            NcCheck.Check(NetCDF.nc_inq_enum_ident(groupId, myId, (Int64) memberValue, nameBuffer));
            string buf = encoder.GetString(nameBuffer);
            return buf.Substring(0, buf.IndexOf('\0'));
        }

        public string GetMemberNameFromValue(Int64 memberValue) {
            byte[] nameBuffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME+1];
            ASCIIEncoding encoder = new ASCIIEncoding();
            NcCheck.Check(NetCDF.nc_inq_enum_ident(groupId, myId, (Int64) memberValue, nameBuffer));
            string buf = encoder.GetString(nameBuffer);
            return buf.Substring(0, buf.IndexOf('\0'));
        }

        public string GetMemberNameFromValue(UInt64 memberValue) {
            byte[] nameBuffer = new byte[(int)NetCDF.netCDF_limits.NC_MAX_NAME+1];
            ASCIIEncoding encoder = new ASCIIEncoding();
            NcCheck.Check(NetCDF.nc_inq_enum_ident(groupId, myId, (Int64) memberValue, nameBuffer));
            string buf = encoder.GetString(nameBuffer);
            return buf.Substring(0, buf.IndexOf('\0'));
        }


        public void AddMember(string name, sbyte memberValue) {
            NcCheck.Check(NetCDF.nc_insert_enum(groupId, myId, name, ref memberValue));
        }

        public void AddMember(string name, byte memberValue) {
            NcCheck.Check(NetCDF.nc_insert_enum(groupId, myId, name, ref memberValue));
        }

        public void AddMember(string name, Int16 memberValue) {
            NcCheck.Check(NetCDF.nc_insert_enum(groupId, myId, name, ref memberValue));
        }

        public void AddMember(string name, UInt16 memberValue) {
            NcCheck.Check(NetCDF.nc_insert_enum(groupId, myId, name, ref memberValue));
        }

        public void AddMember(string name, Int32 memberValue) {
            NcCheck.Check(NetCDF.nc_insert_enum(groupId, myId, name, ref memberValue));
        }

        public void AddMember(string name, UInt32 memberValue) {
            NcCheck.Check(NetCDF.nc_insert_enum(groupId, myId, name, ref memberValue));
        }

        public void AddMember(string name, Int64 memberValue) {
            NcCheck.Check(NetCDF.nc_insert_enum(groupId, myId, name, ref memberValue));
        }

        public void AddMember(string name, UInt64 memberValue) {
            NcCheck.Check(NetCDF.nc_insert_enum(groupId, myId, name, ref memberValue));
        }

    }
}
