# NcGroup

#### Namespace
`ASA.NetCDF4`

### enum `GroupLocation`
        AllChildrenGrps
        AllGrps
        ChildrenGrps
        ChildrenOfChildrenGrps
        ParentsAndCurrentGrps
        ParentsGrps
### enum `Location`
        All
        Children
        ChildrenAndCurrent
        Current
        Parents
        ParentsAndCurrent

### Class `NcGroup`

#### Description
Type to manage a netCDF group or file instance.

#### Fields
        #myId
        #nullObject
#### Methods
        +AddCompoundType(string name, long size)
        +AddDim(string name)
        +AddDim(string name, Int32 dimSize)
        +AddDim(NcDim dim)
        +AddEnumType(string name, NcEnumType.Types baseType)
        +AddGroup(string name)
        +AddOpaqueType(string name, Int32 size)
        +AddVar(string name, NcType ncType, NcDim ncDim)
        +AddVar(string name, string typeName)
        +AddVar(string name, string typeName, List<string> dimNames)
        +AddVar(string name, string typeName, string dimName)
        +AddVar(string name, NcType type)
        +AddVar(string name, NcType ncType, List<NcDim> ncDimVector)
        +AddVlenType(string name, NcType baseType)
        +CheckData()
        +CheckDefine()
        #CheckNull()
        +GetAtt(string name, Location location=Location.Current)
        +GetAttCount(Location location=Location.Current)
        +GetAtts(Location location=Location.Current)
        +GetAtts(string name, Location location=Location.Current)
        +GetCoordVar(string coordVarName, ref NcDim ncDim, ref NcVar ncVar, Location location = Location.Current)
        +GetCoordVars(Location location=Location.Current)
        +GetDim(string name, Location location=Location.Current)
        +GetDimCount(Location location=Location.Current)
        +GetDims(string name, Location location=Location.Current)
        +GetDims(Location location=Location.Current)
        +GetGroup(string name, GroupLocation location=GroupLocation.ChildrenGrps)
        +GetGroupCount(GroupLocation location=GroupLocation.ChildrenGrps)
        +GetGroups(GroupLocation location=GroupLocation.ChildrenGrps)
        +GetGroups(string name, GroupLocation location=GroupLocation.ChildrenGrps)
        +GetId()
        +GetName(bool fullName=false)
        +GetParentGroup()
        +GetType(string name, Location location=Location.Current)
        +GetTypeCount(NcTypeEnum enumType, Location location=Location.Current)
        +GetTypeCount(Location location=Location.Current)
        +GetTypes(Location location=Location.Current)
        +GetTypes(NcTypeEnum enumType, Location location=Location.Current)
        +GetTypes(string name, NcTypeEnum enumType, Location location=Location.Current)
        +GetTypes(string name, Location location=Location.Current)
        +GetVar(string name, Location location=Location.Current)
        +GetVarCount(Location location=Location.Current)
        +GetVars(Location location=Location.Current)
        +GetVars(string name, Location location=Location.Current)
        +IsNull()
        +IsRootGroup()
        -LocationIsChildGroup(Location location)
        -LocationIsCurrentGroup(Location location)
        -LocationIsParentGroup(Location location)
        +NcGroup()
        +NcGroup(int groupId)
        +NcGroup(NcGroup rhs)
        -~NcGroup()
        +PutAtt(NcAtt attr)
        +PutAtt(string name, NcType type, Int32[] dataValues)
        +PutAtt(string name, NcType type, Int64 datumValue)
        +PutAtt(string name, NcType type, Int64[] dataValues)
        +PutAtt(string name, NcType type, UInt16 datumValue)
        +PutAtt(string name, NcType type, UInt16[] dataValues)
        +PutAtt(string name, NcType type, UInt32 datumValue)
        +PutAtt(string name, NcType type, UInt32[] dataValues)
        +PutAtt(string name, NcType type, UInt64 datumValue)
        +PutAtt(string name, NcType type, UInt64[] dataValues)
        +PutAtt(string name, NcType type, byte datumValue)
        +PutAtt(string name, NcType type, Int32 datumValue)
        +PutAtt(string name, NcType type, double datumValue)
        +PutAtt(string name, NcType type, double[] dataValues)
        +PutAtt(string name, NcType type, float datumValue)
        +PutAtt(string name, NcType type, float[] dataValues)
        +PutAtt(string name, NcType type, sbyte datumValue)
        +PutAtt(string name, NcType type, sbyte[] dataValues)
        +PutAtt(string name, string dataValues)
        +PutAtt(string name, NcType type, Int16[] dataValues)
        +PutAtt(string name, NcType type, Int16 datumValue)
        +PutAtt(string name, NcType type, byte[] dataValues)

