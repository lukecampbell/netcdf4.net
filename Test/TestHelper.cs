/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcGroup
 */

using System;
using System.Collections.Generic;
using ASA.NetCDF4;

namespace ASA.NetCDF4.Test {
    class TestHelper {
        public static NcFile NewFile(string filePath) {
            NcFile file;
            CheckDelete(filePath);
            file = new NcFile(filePath, NcFileMode.replace, NcFileFormat.nc4);
            return file;
        }
        public static void CheckDelete(string filePath) {
            if(System.IO.File.Exists(filePath)) {
                System.IO.File.Delete(filePath);
            }
        }
    }
}

