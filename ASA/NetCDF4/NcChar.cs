/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * com.asascience.netcdf4.NcChar
 */
using System;

namespace ASA.NetCDF4 {
    public class NcChar : NcType {
        private static NcChar instance;
        public static NcChar Instance {
            get {
                if(instance==null)
                    instance = new NcChar();
                return instance;
            }
        }
        public NcChar() : base((int)NcTypeEnum.NC_CHAR) {
        }
    }
}

