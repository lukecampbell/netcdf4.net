/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcType
 */
using System;
using System.Text;

namespace netcdf4 {
    /* From netcdf.h:27 */
    public enum NcTypeEnum {
        NC_NAT      = 0,
        NC_BYTE     = 1,
        NC_CHAR     = 2,
        NC_SHORT    = 3,
        NC_INT      = 4,
        NC_LONG     = 4,
        NC_FLOAT    = 5,
        NC_DOUBLE   = 6,
        NC_UBYTE    = 7,
        NC_USHORT   = 8,
        NC_UINT     = 9,
        NC_INT64    = 10,
        NC_UINT64   = 11,
        NC_STRING   = 12,
        NC_VLEN     = 13,
        NC_OPAQUE   = 14,
        NC_ENUM     = 15,
        NC_COMPOUND = 16
    }
    public class NcType {

        // Constructor generates a null object
        public NcType() {
            nullObject = true;
        }

        // Compares global types
        public virtual bool Equals(NcType other) {
            return myId == other.myId;
        }
        
        // Constructor
        public NcType(NcGroup grp, string name) {
            nullObject = false;
            groupId = grp.GetId();
            NcType typeTmp = grp.GetType(name, Location.ParentsAndCurrent);
            myId = typeTmp.GetId();
        }

        // Constructor for a non-global type
        public NcType(NcGroup grp, int id) {
            nullObject = false;
            myId = id;
            groupId = grp.GetId();
        }

        // Constructor for a global type
        public NcType(Int32 id) {
            nullObject = false;
            myId = id;
            groupId = 0;
        }

        // Copy constructor
        public NcType(NcType rhs) {
            nullObject = rhs.nullObject;
            myId = rhs.myId;
            groupId = rhs.groupId;
        }

        ~NcType() {
        }

        public Int32 GetId() {
            return myId;
        }


        public NcGroup GetParentGroup() {
            if(groupId!=0) {
                return new NcGroup(groupId);
            }
            return new NcGroup(); // Null
        }

        public string GetName() {
            StringBuilder charName = new StringBuilder((int)NetCDF.netCDF_limits.NC_MAX_NAME);
            Int32 sizep = 0;
            NcCheck.Check(NetCDF.nc_inq_type(groupId, (NetCDF.nc_type)myId,charName, ref sizep));
            return charName.ToString();
        }

        public Int32 GetSize() {
            StringBuilder charName = new StringBuilder((int)NetCDF.netCDF_limits.NC_MAX_NAME);
            Int32 sizep = 0;
            NcCheck.Check(NetCDF.nc_inq_type(groupId, (NetCDF.nc_type)myId, charName, ref sizep));
            return sizep;
        }

        public NcTypeEnum GetTypeClass() {
            switch(myId) {
                case (int)NcTypeEnum.NC_BYTE    : return NcTypeEnum.NC_BYTE;
                case (int)NcTypeEnum.NC_UBYTE   : return NcTypeEnum.NC_UBYTE;
                case (int)NcTypeEnum.NC_CHAR    : return NcTypeEnum.NC_CHAR;
                case (int)NcTypeEnum.NC_SHORT   : return NcTypeEnum.NC_SHORT;
                case (int)NcTypeEnum.NC_USHORT  : return NcTypeEnum.NC_USHORT;
                case (int)NcTypeEnum.NC_INT     : return NcTypeEnum.NC_UBYTE;
                case (int)NcTypeEnum.NC_UINT    : return NcTypeEnum.NC_UINT;  
                case (int)NcTypeEnum.NC_INT64   : return NcTypeEnum.NC_INT64; 
                case (int)NcTypeEnum.NC_UINT64  : return NcTypeEnum.NC_UINT64;
                case (int)NcTypeEnum.NC_FLOAT   : return NcTypeEnum.NC_FLOAT;
                case (int)NcTypeEnum.NC_DOUBLE  : return NcTypeEnum.NC_DOUBLE;
                case (int)NcTypeEnum.NC_STRING  : return NcTypeEnum.NC_STRING;
                case (int)NcTypeEnum.NC_VLEN    : return NcTypeEnum.NC_VLEN;
                case (int)NcTypeEnum.NC_OPAQUE  : return NcTypeEnum.NC_OPAQUE;
                case (int)NcTypeEnum.NC_ENUM    : return NcTypeEnum.NC_ENUM;
                case (int)NcTypeEnum.NC_COMPOUND: return NcTypeEnum.NC_COMPOUND;
                default :
                    byte[] readBuffer = null;
                    Int32 sizep=0;
                    Int32 base_nc_typep=0;
                    Int32 nfieldsp=0;
                    Int32 classp=0;
                    NcCheck.Check(NetCDF.nc_inq_user_type(groupId, myId, readBuffer, ref sizep, ref base_nc_typep, ref nfieldsp, ref classp));
                    return (NcTypeEnum)classp;
            }
            return (NcTypeEnum)myId;
        }

        public bool IsFixedType() {
            NcTypeEnum nc_type = GetTypeClass();
            if(nc_type == NcTypeEnum.NC_VLEN || nc_type == NcTypeEnum.NC_OPAQUE || nc_type == NcTypeEnum.NC_ENUM
                                                                        || nc_type == NcTypeEnum.NC_COMPOUND)
                return false;
            return true;
        }

        public bool IsUserDefined() {
            return myId >= 32;
        }

        public string GetTypeClassName() {
            switch(myId) {
                case (int)NcTypeEnum.NC_BYTE    : return "NC_BYTE";
                case (int)NcTypeEnum.NC_UBYTE   : return "NC_UBYTE";
                case (int)NcTypeEnum.NC_CHAR    : return "NC_CHAR";
                case (int)NcTypeEnum.NC_SHORT   : return "NC_SHORT";
                case (int)NcTypeEnum.NC_USHORT  : return "NC_USHORT";
                case (int)NcTypeEnum.NC_INT     : return "NC_INT";
                case (int)NcTypeEnum.NC_UINT    : return "NC_UINT";  
                case (int)NcTypeEnum.NC_INT64   : return "NC_INT64"; 
                case (int)NcTypeEnum.NC_UINT64  : return "NC_UINT64";
                case (int)NcTypeEnum.NC_FLOAT   : return "NC_FLOAT";
                case (int)NcTypeEnum.NC_DOUBLE  : return "NC_DOUBLE";
                case (int)NcTypeEnum.NC_STRING  : return "NC_STRING";
                case (int)NcTypeEnum.NC_VLEN    : return "NC_VLEN";
                case (int)NcTypeEnum.NC_OPAQUE  : return "NC_OPAQUE";
                case (int)NcTypeEnum.NC_ENUM    : return "NC_ENUM";
                case (int)NcTypeEnum.NC_COMPOUND: return "NC_COMPOUND";
                
            }
            return "Used Defined Type: " + (myId-32);
        }

        public bool IsNull() {
            return nullObject; 
        }

        protected bool nullObject;
        protected Int32 myId;
        protected Int32 groupId;
    }
}
