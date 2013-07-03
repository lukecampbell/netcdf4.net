/*
 * Author Luke Campbell <LCampbell@asascience.com>
 */

namespace netcdf4 {

    class NcAtt {
        ~NcAtt() {
        }

        public NcAtt() {
        }

        public NcAtt(bool nullObject) {
        }

        public NcAtt(NcAtt rhs) {
        }

        public string getName() {
            return myName;
        }
    
        public long getAttLength() {
            return 0;
        }

        public NcType getType()  {
            return null;
        }

        public NcGroup getParentGroup() {
            return null;

        }

        /* getValues methods */

        public bool isNull() { return nullObject; }

        protected bool nullObject;

        protected string myName;
        protected int groupId;
        protected int varId;
    }
}

