
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcGroup
 */


using System.Collections.Generic;

namespace netcdf4 {
    public enum GroupLocation {
        ChildrenGrps, 
        ParentGrps,
        ChildrenOfChildrenGrps,
        AllChildrenGrps,
        ParentsAndCurrentGrps,
        AllGrps
    }

    public enum Location {
        Current,
        Parents,
        Children,
        ParentsAndCurrent,
        ChildrenAndCurrent,
        All
    }


    class NcGroup {
        public NcGroup() {
        }

        public NcGroup(int groupId) {

        }

        ~NcGroup() {

        }

        public string getName(bool fullName=false) {
            return null;
        }

        public NcGroup getParentGroup() {
            return null;
        }

        public int getId() {
            return groupId;
        }
    
        public int getGroupCount(GroupLocation location=GroupLocation.ChildrenGrps) {
            return 0;

        }

        public Dictionary<string, NcGroup> getGroups(GroupLocation location=GroupLocation.ChildrenGrps) {
            return null;
        }


        protected int groupId;
        

    }
}
