
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcFile
 */
using System;
namespace ASA.NetCDF4 {
    public enum NcFileMode {
        read,
        write,
        replace,
        newFile
    }
    public enum NcFileFormat {
        classic,
        classic64,
        nc4,
        nc4classic
    }

    public class NcFile : NcGroup {
        private string filePath = "";

        public const int NC_NOWRITE       = 0x0000;
        public const int NC_WRITE         = 0x0001;
        public const int NC_CLOBBER       = 0x0000;
        public const int NC_NOCLOBBER     = 0x0004;
        public const int NC_DISKLESS      = 0x0008;
        public const int NC_MMAP          = 0x0010;
        public const int NC_CLASSIC_MODEL = 0x0100;
        public const int NC_64BIT_OFFSET  = 0x0200;
        public const int NC_LOCK          = 0x0400;
        public const int NC_SHARE         = 0x0800;
        public const int NC_NETCDF4       = 0x1000;
        public const int NC_MPIIO         = 0x2000;
        public const int NC_MPIPOSIX      = 0x4000;
        public const int NC_PNETCDF       = 0x8000;
        public const int NC_FORMAT_CLASSIC         = 0x01;
        public const int NC_FORMAT_64BIT           = 0x02;
        public const int NC_FORMAT_NETCDF4         = 0x03;
        public const int NC_FORMAT_NETCDF4_CLASSIC = 0x04;


        public NcFile(NcGroup rhs) : base(rhs) {
            
        }

        public NcFile() : base() {
        }

        public NcFile(string filePath, NcFileMode fMode) {
            switch(fMode) {
                case NcFileMode.write:
                    NcCheck.Check(NetCDF.nc_open(filePath, NC_WRITE, ref myId));
                    break;
                case NcFileMode.read:
                    NcCheck.Check(NetCDF.nc_open(filePath, NC_NOWRITE, ref myId));
                    break;
                case NcFileMode.newFile:
                    NcCheck.Check(NetCDF.nc_create(filePath, NC_NETCDF4 | NC_NOCLOBBER, ref myId));
                    break;
                case NcFileMode.replace:
                    NcCheck.Check(NetCDF.nc_create(filePath, NC_NETCDF4 | NC_CLOBBER, ref myId));
                    break;
            }
            this.filePath = filePath;
            nullObject = false;
        }

        public NcFile(string filePath, NcFileMode fMode, NcFileFormat fFormat) {
            Int32 format = 0;
            switch(fFormat) {
                case NcFileFormat.classic:
                    format = 0;
                    break;
                case NcFileFormat.classic64:
                    format = NC_64BIT_OFFSET;
                    break;
                case NcFileFormat.nc4:
                    format = NC_NETCDF4;
                    break;
                case NcFileFormat.nc4classic:
                    format = NC_NETCDF4 | NC_CLASSIC_MODEL;
                    break;
            }
            switch(fMode) {
                case NcFileMode.write:
                    NcCheck.Check(NcCheck.NC_EINVAL);
                    break;
                case NcFileMode.read:
                    NcCheck.Check(NcCheck.NC_EINVAL);
                    break;
                case NcFileMode.newFile:
                    NcCheck.Check(NetCDF.nc_create(filePath, format | NC_NOCLOBBER, ref myId));
                    break;
                case NcFileMode.replace:
                    NcCheck.Check(NetCDF.nc_create(filePath, format | NC_CLOBBER, ref myId));
                    break;
            }
            this.filePath = filePath;
            nullObject = false;
        }
        public void Close() {
            NcCheck.Check(NetCDF.nc_close(myId));
            nullObject = true;
            myId = 0;
        }

        public string FilePath { 
            get {
                return filePath;
            }
        }


        public NcFileFormat Format {
            get {
                Int32 formatp=0;
                NcCheck.Check(NetCDF.nc_inq_format(myId, ref formatp));
                if(formatp == NC_FORMAT_CLASSIC)
                    return NcFileFormat.classic;
                if(formatp == NC_FORMAT_64BIT)
                    return NcFileFormat.classic64;
                if(formatp == NC_FORMAT_NETCDF4)
                    return NcFileFormat.nc4;
                if(formatp == NC_FORMAT_NETCDF4_CLASSIC)
                    return NcFileFormat.nc4classic;

                throw new exceptions.NcInvalidArg("Unknown file format: " + formatp);
            }
        }

        ~NcFile() {
            if(!nullObject) {
                NcCheck.Check(NetCDF.nc_close(myId));
            }
        }
    }
}
