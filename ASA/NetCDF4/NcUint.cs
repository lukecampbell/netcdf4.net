/*
 * Author Luke Campbell <LCampbell@asascience.com>
 */
namespace ASA.NetCDF4 {
    public class NcUint : NcType{
        private static NcUint instance;
        public static NcUint Instance {
            get {
                if(instance==null)
                    instance = new NcUint();
                return instance;
            }
        }
        public NcUint() : base((int)NcTypeEnum.NC_UINT) {
        }
    }
}
