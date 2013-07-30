#NcFile

#### Namespace
`ASA.NetCDF4`

### enum `NcFileMode`
        classic
        classic64
        nc4
        nc4classic
### enum `NcFileMode`
        newFile
        read
        replace
        write

### Class `NcFile : NcGroup`

#### Description
Type to manage a netCDF file instance.

#### Fields
        +NC_64BIT_OFFSET
        +NC_CLASSIC_MODEL
        +NC_CLOBBER
        +NC_DISKLESS
        +NC_FORMAT_64BIT
        +NC_FORMAT_CLASSIC
        +NC_FORMAT_NETCDF4
        +NC_FORMAT_NETCDF4_CLASSIC
        +NC_LOCK
        +NC_MMAP
        +NC_MPIIO
        +NC_MPIPOSIX
        +NC_NETCDF4
        +NC_NOCLOBBER
        +NC_NOWRITE
        +NC_PNETCDF
        +NC_SHARE
        +NC_WRITE
        -filePath
#### Methods
        +Close()
        +NcFile(string filePath, NcFileMode fMode, NcFileFormat fFormat)
        +NcFile()
        +NcFile(NcGroup rhs)
        -~NcFile()
        +NcFile(string filePath, NcFileMode fMode)
#### Properties
        +FilePath
        +Format

