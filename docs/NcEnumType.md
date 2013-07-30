#NcEnumType

#### Namespace
`ASA.NetCDF4`

### Class `NcEnumType : NcType`

#### Description
NetCDF type implementation for an enumerated type.

### enum `Types`
        NC_BYTE
        NC_INT
        NC_INT64
        NC_SHORT
        NC_UBYTE
        NC_UINT
        NC_UINT64
        NC_USHORT

#### Methods
        +AddMember(string name, byte memberValue)
        +AddMember(string name, sbyte memberValue)
        +AddMember(string name, Int32 memberValue)
        +AddMember(string name, UInt16 memberValue)
        +AddMember(string name, UInt64 memberValue)
        +AddMember(string name, Int64 memberValue)
        +AddMember(string name, UInt32 memberValue)
        +AddMember(string name, Int16 memberValue)
        +GetBaseType()
        +GetMemberCount()
        +GetMemberNameFromValue(Int32 memberValue)
        +GetMemberNameFromValue(Int64 memberValue)
        +GetMemberNameFromValue(UInt16 memberValue)
        +GetMemberNameFromValue(Int16 memberValue)
        +GetMemberNameFromValue(UInt64 memberValue)
        +GetMemberNameFromValue(byte memberValue)
        +GetMemberNameFromValue(UInt32 memberValue)
        +GetMemberNameFromValue(sbyte memberValue)
        +NcEnumType(NcEnumType rhs)
        +NcEnumType(NcGroup grp, string name)
        +NcEnumType(NcType ncType )
        +NcEnumType()

