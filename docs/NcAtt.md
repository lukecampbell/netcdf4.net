# NcAtt

#### Namespace 
`ASA.NetCDF4` 

### Class `NcAtt`

#### Description
NetCDF Attribute Class 
#### Fields
        +NC_GLOBAL
        #groupId
        #myName
        #nullObject
        #varId

#### Methods
        #CheckAttLen(int arrayLength)
        -CheckFixedType()
        #CheckNull()
        +GetAttLength()
        +GetName()
        +GetNcType()
        +GetParentGroup()
        +GetValues(Int64[] dataValues, bool strictChecking=true)
        +GetValues(UInt32[] dataValues, bool strictChecking=true)
        +GetValues(UInt64[] dataValues, bool strictChecking=true)
        +GetValues(Int16[] dataValues, bool strictChecking=true)
        +GetValues(UInt16[] dataValues, bool strictChecking=true)
        +GetValues(float[] dataValues, bool strictChecking=true)
        +GetValues(sbyte[] dataValues, bool strictChecking=true)
        +GetValues()
        +GetValues(double[] dataValues, bool strictChecking=true)
        +GetValues(Int32[] dataValues, bool strictChecking=true)
        +GetValues(byte[] dataValues, bool strictChecking=true)
        +IsNull()
        +NcAtt(bool nullObject)
        +NcAtt(NcAtt rhs)
        +NcAtt()
        -~NcAtt()
