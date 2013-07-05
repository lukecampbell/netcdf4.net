
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcFile
 */
using System;
namespace netcdf4 {
    public enum FileMode {
        read,
        write,
        replace,
        newFile
    }
    public enum FileFormat {
        classic,
        classic64,
        nc4,
        nc4classic
    }

    class NcFile : NcGroup {
        public const int NC_NOWRITE       = 0x00;
        public const int NC_WRITE         = 0x01;
        public const int NC_CLOBBER       = 0x00;
        public const int NC_NOCLOBBER     = 0x04;
        public const int NC_DISKLESS      = 0x08;
        public const int NC_MMAP          = 0x0010;
        public const int NC_CLASSIC_MODEL = 0x0100;
        public const int NC_LOCK          = 0x0400;
        public const int NC_SHARE         = 0x0800;
        public const int NC_NETCDF4       = 0x1000;
        public const int NC_MPIIO         = 0x2000;
        public const int NC_MPIPOSIX      = 0x4000;
        public const int NC_PNETCDF       = 0x8000;

        public const int NC_FORMAT_CLASSIC         = 1;
        public const int NC_FORMAT_64BIT           = 2;
        public const int NC_FORMAT_NETCDF4         = 3;
        public const int NC_FORMAT_NETCDF4_CLASSIC = 4;

        public NcFile(NcGroup rhs) {
            throw new NotImplementedException("NcFile constructor not implemented");
        }

        public NcFile() : base() {
        }

        public NcFile(string filePath, FileMode fMode) {
            switch(fMode) {
                case FileMode.write:
                    NcCheck.check(NetCDF.nc_open(filePath, NC_WRITE, ref myId));
                    break;
                case FileMode.read:
                    NcCheck.check(NetCDF.nc_open(filePath, NC_NOWRITE, ref myId));
                    break;
                case FileMode.newFile:
                    NcCheck.check(NetCDF.nc_open(filePath, NC_NETCDF4 | NC_NOCLOBBER, ref myId));
                    break;
                case FileMode.replace:
                    NcCheck.check(NetCDF.nc_open(filePath, NC_NETCDF4 | NC_CLOBBER, ref myId));
                    break;
            }
            nullObject = false;
        }

        public NcFile(string filePath, FileMode fMode, FileFormat fFormat) {
            throw new NotImplementedException("NcFile constructor not implemented");
        }

        ~NcFile() {
            NcCheck.check(NetCDF.nc_close(myId));
        }
    }
}
