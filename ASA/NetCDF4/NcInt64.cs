
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcInt64
 */
namespace ASA.NetCDF4 {
    public class NcInt64 : NcType {
        private static NcInt64 instance;
        public static NcInt64 Instance {
            get {
                if(instance==null)
                    instance = new NcInt64();
                return instance;
            }
        }

        public NcInt64() : base((int)NcTypeEnum.NC_INT64) {
        }

    }
}
