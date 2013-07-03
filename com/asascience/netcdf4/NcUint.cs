/*
 * Author Luke Campbell <LCampbell@asascience.com>
 */
namespace netcdf4 {
    class NcUint : NcType{
        public NcUint() {
        }
        private static NcUint ncuint_instance=null;
        public static NcUint NC_UINT {
            get { 
                if(ncuint_instance == null) {
                    ncuint_instance = new NcUint();
                }
                return ncuint_instance;
            }
        }
    }
}
