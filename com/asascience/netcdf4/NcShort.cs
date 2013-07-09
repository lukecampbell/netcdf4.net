
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcShort
 */
namespace netcdf4 {
    public class NcShort : NcType {
        private static NcShort instance;
        public static NcShort Instance {
            get {
                if(instance==null)
                    instance = new NcShort();
                return instance;
            }
        }
        public NcShort() : base((int)NcTypeEnum.NC_SHORT) {
        }

    }
}
