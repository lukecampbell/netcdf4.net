
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcInt
 */
namespace ASA.NetCDF4 {
    public class NcInt : NcType {
        private static NcInt instance;
        public static NcInt Instance {
            get {
                if(instance == null)
                    instance = new NcInt();
                return instance;
            }
        }

        public NcInt() : base((int)NcTypeEnum.NC_INT) {
        }

    }
}
