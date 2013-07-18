/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcString
 */
namespace ASA.NetCDF4 {
    public class NcString : NcType {
        private static NcString instance;
        public static NcString Instance {
            get {
                if(instance==null)
                    instance = new NcString();
                return instance;
            }
        }

        public NcString() : base((int)NcTypeEnum.NC_STRING) {
        }
    }
}
