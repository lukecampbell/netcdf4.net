using netcdf4.exceptions;
namespace netcdf4 {
    class NcCheck {
        public const int NC_EBADID =-33;
        public const int NC_ENFILE =-34;
        public const int NC_EEXIST =-35;
        public const int NC_EINVAL =-36;
        public const int NC_EPERM =-37;
        public const int NC_ENOTINDEFINE = -38;
        public const int NC_EINDEFINE = -39;
        public const int NC_EINVALCOORDS = -40;
        public const int NC_EMAXDIMS = -41;
        public const int NC_ENAMEINUSE = -42;
        public const int NC_ENOTATT = -43;
        public const int NC_EMAXATTS = -44;
        public const int NC_EBADTYPE = -45;
        public const int NC_EBADDIM = -46;
        public const int NC_EUNLIMPOS = -47;
        public const int NC_EMAXVARS = -48;
        public const int NC_ENOTVAR = -49;
        public const int NC_EGLOBAL = -50;
        public const int NC_ENOTNC = -51;
        public const int NC_ESTS = -52;
        public const int NC_EMAXNAME = -53;
        public const int NC_EUNLIMIT = -54;
        public const int NC_ENORECVARS = -55;
        public const int NC_ECHAR = -56;
        public const int NC_EEDGE = -57;
        public const int NC_ESTRIDE = -58;
        public const int NC_EBADNAME = -59;
        public const int NC_ERANGE = -60;
        public const int NC_ENOMEM = -61;
        public const int NC_EVARSIZE = -62;
        public const int NC_EDIMSIZE = -63;
        public const int NC_ETRUNC = -64;
        public const int NC_EAXISTYPE = -65;
        public const int NC_EDAP = -66;
        public const int NC_ECURL = -67;
        public const int NC_EIO = -68;
        public const int NC_ENODATA = -69;
        public const int NC_EDAPSVC = -70;
        public const int NC_EDAS = -71;
        public const int NC_EDDS = -72;
        public const int NC_EDATADDS = -73;
        public const int NC_EDAPURL = -74;
        public const int NC_EDAPCONSTRAINT = -75;
        public const int NC_ETRANSLATION = -76;
        public const int NC4_FIRST_ERROR = -100;
        public const int NC_EHDFERR = -101;
        public const int NC_ECANTREAD = -102;
        public const int NC_ECANTWRITE = -103;
        public const int NC_ECANTCREATE = -104;
        public const int NC_EFILEMETA = -105;
        public const int NC_EDIMMETA = -106;
        public const int NC_EATTMETA = -107;
        public const int NC_EVARMETA = -108;
        public const int NC_ENOCOMPOUND = -109;
        public const int NC_EATTEXISTS = -110;
        public const int NC_ENOTNC4 = -111;
        public const int NC_ESTRICTNC3 = -112;
        public const int NC_ENOTNC3 = -113;
        public const int NC_ENOPAR = -114;
        public const int NC_EPARINIT = -115;
        public const int NC_EBADGRPID = -116;
        public const int NC_EBADTYPID = -117;
        public const int NC_ETYPDEFINED = -118;
        public const int NC_EBADFIELD = -119;
        public const int NC_EBADCLASS = -120;
        public const int NC_EMAPTYPE = -121;
        public const int NC_ELATEFILL = -122;
        public const int NC_ELATEDEF = -123;
        public const int NC_EDIMSCALE = -124;
        public const int NC_ENOGRP = -125;
        public const int NC_ESTORAGE = -126;
        public const int NC_EBADCHUNK = -127;
        public const int NC_ENOTBUILT = -128;
        public const int NC_EDISKLESS = -129;
        public const int NC4_LAST_ERROR = -129;

        public static void check(int retCode) {
            
            switch(retCode) {
                case NC_NOERR           : return; /* No Error */
                  
                case NC_EBADID          : throw NcBadId("Not a netcdf id");
                case NC_ENFILE          : throw NcNFile("Too many netcdfs open");
                case NC_EEXIST          : throw NcExist("netcdf file exists && NC_NOCLOBBER");
                case NC_EINVAL          : throw NcInvalidArg("Invalid Argument");
                case NC_EPERM           : throw NcInvalidWrite("Write to read only");
                case NC_ENOTINDEFINE    : throw NcNotInDefineMode("Operation not allowed in data mode");
                case NC_EINDEFINE       : throw NcInDefineMode("Operation not allowed in define mode");
                case NC_EINVALCOORDS    : throw NcInvalidCoords("Index exceeds dimension bound");
                case NC_EMAXDIMS        : throw NcMaxDims("NC_MAX_DIMS is exceeded");
                case NC_ENAMEINUSE      : throw NcNameInUse("String match to name in use");
                case NC_ENOTATT         : throw NcNotAtt("Attribute not found");
                case NC_EMAXATTS        : throw NcMaxAtts("NC_MAX_ATTRS exceeded");
                case NC_EBADTYPE        : throw NcBadType("Not a netcdf data type");
                case NC_EBADDIM         : throw NcBadDim("Invalid dimension id or name");
                case NC_EUNLIMPOS       : throw NcUnlimPos("NC_UNLIMITED is in the wrong index");
                case NC_EMAXVARS        : throw NcMaxVars("NC_MAX_VARS is exceeded");
                case NC_ENOTVAR         : throw NcNotVar("Variable is not found");
                case NC_EGLOBAL         : throw NcGlobal("Action prohibited on NC_GLOBAL varid");
                case NC_ENOTNC          : throw NcNotNCF("Not a netcdf file");
                case NC_ESTS            : throw NcSts("In Fortran, string too short");
                case NC_EMAXNAME        : throw NcMaxName("NC_MAX_NAME exceeded");
                case NC_EUNLIMIT        : throw NcUnlimit("NC_UNLIMITED size already in use");
                case NC_ENORECVARS      : throw NcNoRecVars("nc_rec op when there are no record vars");
                case NC_ECHAR           : throw NcChar("Attempt to convert between text & numbers");
                case NC_EEDGE           : throw NcEdge("Edge+start exceeds dimension bound");
                case NC_ESTRIDE         : throw NcStride("Illegal stride");
                case NC_EBADNAME        : throw NcBadName("Attribute or variable name contains illegal characters");
                case NC_ERANGE          : throw NcRange("Math result not representable");
                case NC_ENOMEM          : throw NcNoMem("Memory allocation (malloc) failure");
                case NC_EVARSIZE        : throw NcVarSize("One or more variable sizes violate format constraints");
                case NC_EDIMSIZE        : throw NcDimSize("Invalid dimension size");
                case NC_ETRUNC          : throw NcTrunc("File likely truncated or possibly corrupted");

                  // The following are specific netCDF4 errors.
                case NC_EHDFERR         : throw NcHdfErr("An error was reported by the HDF5 layer.");
                case NC_ECANTREAD       : throw NcCantRead("Cannot Read");
                case NC_ECANTWRITE      : throw NcCantWrite("Cannott write");
                case NC_ECANTCREATE     : throw NcCantCreate("Cannot create");
                case NC_EFILEMETA       : throw NcFileMeta("File  meta");
                case NC_EDIMMETA        : throw NcDimMeta("dim meta");
                case NC_EATTMETA        : throw NcAttMeta("att meta");
                case NC_EVARMETA        : throw NcVarMeta("var meta");
                case NC_ENOCOMPOUND     : throw NcNoCompound("No compound");
                case NC_EATTEXISTS      : throw NcAttExists("Attribute exists");
                case NC_ENOTNC4         : throw NcNotNc4("Attempting netcdf-4 operation on netcdf-3 file.");
                case NC_ESTRICTNC3      : throw NcStrictNc3("Attempting netcdf-4 operation on strict nc3 netcdf-4 file.");
                case NC_EBADGRPID       : throw NcBadGroupId("Bad group id.");
                case NC_EBADTYPID       : throw NcBadTypeId("Bad type id.");                       // netcdf.h file inconsistent with documentation!!
                case NC_EBADFIELD       : throw NcBadFieldId("Bad field id.");                     // netcdf.h file inconsistent with documentation!!
                  //  case NC_EUNKNAME        : throw NcUnkownName("Cannot find the field id.");   // netcdf.h file inconsistent with documentation!!

                case NC_ENOGRP          : throw NcEnoGrp("No netCDF group found");
                case NC_ELATEDEF        : throw NcElateDef("Operation to set the deflation, chunking, endianness, fill, compression, or checksum of a NcVar object has been made after a call to getVar or putVar."
                                       );

                default:
                  throw NcException("NcException","Unknown error");
                }
        }

    }
}

