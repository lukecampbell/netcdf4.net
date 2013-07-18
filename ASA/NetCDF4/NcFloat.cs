
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcFloat
 */
namespace ASA.NetCDF4 {
    public class NcFloat : NcType {
        private static NcFloat instance;
        public static NcFloat Instance {
            get {
                if(instance==null)
                    instance = new NcFloat();
                return instance;
            }
        }

        public NcFloat() : base((int)NcTypeEnum.NC_FLOAT) {
        }

    }
}
