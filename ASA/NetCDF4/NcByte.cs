/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * com.asascience.netcdf4.NcChar
 */
using System;

namespace ASA.NetCDF4 {
    public class NcByte : NcType {
        private static NcByte instance;
        public static NcByte Instance {
            get {
                if(instance == null)
                    instance = new NcByte();
                return instance;
            }
        }

        public NcByte() : base((int)NcTypeEnum.NC_BYTE) {
        }
        public Int32 Size() {
            return 1;
        }
    }
}

