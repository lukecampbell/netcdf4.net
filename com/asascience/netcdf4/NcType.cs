/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcType
 */
namespace netcdf4 {
    /* From netcdf.h:27 */
    public enum nctype {
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
    class NcType {

        // Constructor generates a null object
        public NcType() {
            nullObject = true;
        }

        // Constructor
        public NcType(NcGroup grp, string name) {
            nullObject = false;
            groupId = grp.getId();
            NcType typTmp = grp.getType(name, NcGroup.ParentsAndCurrent);
            myId = typTmp.getId();
        }

        // Constructor for a non-global type
        public NcType(NcGroup grp, int id) {
            nullObject = false;
            myId = id;
            groupId = grp.getId();
        }

        // Constructor for a global type
        public NcType(int id) {
            nullObject = false;
            myId = id;
            groupId = 0;
        }


        public NcType(NcType rhs) {
            nullObject = rhs.nullObject;
            myId = rhs.myId;
            groupId = rhs.groupId;
        }

        ~NcType() {
        }

        public int getId() {
            return myId;
        }

        public NcGroup getParentGroup() {
            if(group_id) {
                return new NcGroup(groupId);
            }
            return new NcGroup();
        }

        public string getName() {
            byte[] charName = new byte[NC_MAX_NAME+1];
            long sizep = 0L;
            string retval;
            // ncCheck(nc_inq_type(groupId, myId, charName, sizep));
            retval = System.Text.Encoding.UTF8.GetString(charName);
        }

        public long getSize() {
            return 0;
        }

        public nctype getTypeClass() {
            return nctype.NC_NAT;
        }

        public string getTypeClassName() {
            return null;
        }

        public bool isNull() {
            return nullObject; 
        }

        protected bool nullObject;
        protected int myId;
        protected int groupId;
    }
}
