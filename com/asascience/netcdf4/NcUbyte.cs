
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcUbyte
 */
namespace netcdf4 {
    public class NcUbyte : NcType {
        private static NcUbyte instance;
        public static NcUbyte Instance {
            get {
                if(instance==null)
                    instance = new NcUbyte();
                return instance;
            }
        }

        public NcUbyte() : base((int)NcTypeEnum.NC_UBYTE) {
        }
    }
}
