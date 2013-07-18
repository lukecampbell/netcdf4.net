
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcDouble
 */
namespace ASA.NetCDF4 {
    public class NcDouble : NcType {
        private static NcDouble instance;
        public static NcDouble Instance {
            get { 
                if(instance==null)
                    instance = new NcDouble();
                return instance;
            }
        }
        public NcDouble() : base((int) NcTypeEnum.NC_DOUBLE) {
        }

    }
}
