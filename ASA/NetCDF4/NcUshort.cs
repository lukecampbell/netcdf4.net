
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcUshort
 */
namespace ASA.NetCDF4 {
    public class NcUshort : NcType {
        private static NcUshort instance;
        public static NcUshort Instance {
            get {
                if(instance==null)
                    instance = new NcUshort();
                return instance;
            }
        }
        public NcUshort() : base((int)NcTypeEnum.NC_USHORT) {
        }
    }
}
