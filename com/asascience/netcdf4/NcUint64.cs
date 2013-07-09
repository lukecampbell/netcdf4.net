
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcUint64
 */
namespace netcdf4 {
    public class NcUint64 : NcType {
        private static NcUint64 instance;
        public static NcUint64 Instance {
            get {
                if(instance==null)
                    instance = new NcUint64();
                return instance;
            }
        }

        public NcUint64() : base((int)NcTypeEnum.NC_UINT64) {
        }
    }
}
