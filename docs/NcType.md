# NcType

#### Namespace
`ASA.NetCDF4`

### enum `NcTypeEnum`
        NC_BYTE
        NC_CHAR
        NC_COMPOUND
        NC_DOUBLE
        NC_ENUM
        NC_FLOAT
        NC_INT
        NC_INT64
        NC_LONG
        NC_NAT
        NC_OPAQUE
        NC_SHORT
        NC_STRING
        NC_UBYTE
        NC_UINT
        NC_UINT64
        NC_USHORT
        NC_VLEN
### Class `NcType`
#### Description
A type that represents a netCDF data-type.
#### Fields
        #groupId
        #myId
        #nullObject
#### Methods
        +Equals(NcType other)
        +GetId()
        +GetName()
        +GetParentGroup()
        +GetSize()
        +GetTypeClass()
        +GetTypeClassName()
        +IsFixedType()
        +IsNull()
        +IsUserDefined()
        +NcType(NcGroup grp, string name)
        +NcType(NcType rhs)
        +NcType(NcGroup grp, int id)
        +NcType(Int32 id)
        +NcType()
        -~NcType()

