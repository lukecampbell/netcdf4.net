
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcException
 */
namespace ASA.NetCDF4.exceptions {
    public class NcException : System.Exception{
        public NcException() : base() {
        }
        public NcException(string message) : base(message) {
        }
        public NcException(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcException(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcBadId : NcException {
        public NcBadId() : base() {
        }
        public NcBadId(string message) : base(message) {
        }
        public NcBadId(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcBadId(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNFile : NcException {
        public NcNFile() : base() {
        }
        public NcNFile(string message) : base(message) {
        }
        public NcNFile(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNFile(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcExist : NcException {
        public NcExist() : base() {
        }
        public NcExist(string message) : base(message) {
        }
        public NcExist(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcExist(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcInvalidArg : NcException {
        public NcInvalidArg() : base() {
        }
        public NcInvalidArg(string message) : base(message) {
        }
        public NcInvalidArg(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcInvalidArg(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcInvalidWrite : NcException {
        public NcInvalidWrite() : base() {
        }
        public NcInvalidWrite(string message) : base(message) {
        }
        public NcInvalidWrite(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcInvalidWrite(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNotInDefineMode : NcException {
        public NcNotInDefineMode() : base() {
        }
        public NcNotInDefineMode(string message) : base(message) {
        }
        public NcNotInDefineMode(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNotInDefineMode(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcInDefineMode : NcException {
        public NcInDefineMode() : base() {
        }
        public NcInDefineMode(string message) : base(message) {
        }
        public NcInDefineMode(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcInDefineMode(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcInvalidCoords : NcException {
        public NcInvalidCoords() : base() {
        }
        public NcInvalidCoords(string message) : base(message) {
        }
        public NcInvalidCoords(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcInvalidCoords(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcMaxDims : NcException {
        public NcMaxDims() : base() {
        }
        public NcMaxDims(string message) : base(message) {
        }
        public NcMaxDims(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcMaxDims(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNameInUse : NcException {
        public NcNameInUse() : base() {
        }
        public NcNameInUse(string message) : base(message) {
        }
        public NcNameInUse(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNameInUse(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNotAtt : NcException {
        public NcNotAtt() : base() {
        }
        public NcNotAtt(string message) : base(message) {
        }
        public NcNotAtt(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNotAtt(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcMaxAtts : NcException {
        public NcMaxAtts() : base() {
        }
        public NcMaxAtts(string message) : base(message) {
        }
        public NcMaxAtts(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcMaxAtts(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcBadType : NcException {
        public NcBadType() : base() {
        }
        public NcBadType(string message) : base(message) {
        }
        public NcBadType(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcBadType(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcBadDim : NcException {
        public NcBadDim() : base() {
        }
        public NcBadDim(string message) : base(message) {
        }
        public NcBadDim(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcBadDim(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcUnlimPos : NcException {
        public NcUnlimPos() : base() {
        }
        public NcUnlimPos(string message) : base(message) {
        }
        public NcUnlimPos(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcUnlimPos(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcMaxVars : NcException {
        public NcMaxVars() : base() {
        }
        public NcMaxVars(string message) : base(message) {
        }
        public NcMaxVars(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcMaxVars(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNotVar : NcException {
        public NcNotVar() : base() {
        }
        public NcNotVar(string message) : base(message) {
        }
        public NcNotVar(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNotVar(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcGlobal : NcException {
        public NcGlobal() : base() {
        }
        public NcGlobal(string message) : base(message) {
        }
        public NcGlobal(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcGlobal(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNotNCF : NcException {
        public NcNotNCF() : base() {
        }
        public NcNotNCF(string message) : base(message) {
        }
        public NcNotNCF(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNotNCF(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcSts : NcException {
        public NcSts() : base() {
        }
        public NcSts(string message) : base(message) {
        }
        public NcSts(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcSts(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcMaxName : NcException {
        public NcMaxName() : base() {
        }
        public NcMaxName(string message) : base(message) {
        }
        public NcMaxName(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcMaxName(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcUnlimit : NcException {
        public NcUnlimit() : base() {
        }
        public NcUnlimit(string message) : base(message) {
        }
        public NcUnlimit(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcUnlimit(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNoRecVars : NcException {
        public NcNoRecVars() : base() {
        }
        public NcNoRecVars(string message) : base(message) {
        }
        public NcNoRecVars(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNoRecVars(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcChar : NcException {
        public NcChar() : base() {
        }
        public NcChar(string message) : base(message) {
        }
        public NcChar(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcChar(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcEdge : NcException {
        public NcEdge() : base() {
        }
        public NcEdge(string message) : base(message) {
        }
        public NcEdge(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcEdge(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcStride : NcException {
        public NcStride() : base() {
        }
        public NcStride(string message) : base(message) {
        }
        public NcStride(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcStride(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcBadName : NcException {
        public NcBadName() : base() {
        }
        public NcBadName(string message) : base(message) {
        }
        public NcBadName(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcBadName(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcRange : NcException {
        public NcRange() : base() {
        }
        public NcRange(string message) : base(message) {
        }
        public NcRange(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcRange(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNoMem : NcException {
        public NcNoMem() : base() {
        }
        public NcNoMem(string message) : base(message) {
        }
        public NcNoMem(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNoMem(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcVarSize : NcException {
        public NcVarSize() : base() {
        }
        public NcVarSize(string message) : base(message) {
        }
        public NcVarSize(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcVarSize(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcDimSize : NcException {
        public NcDimSize() : base() {
        }
        public NcDimSize(string message) : base(message) {
        }
        public NcDimSize(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcDimSize(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcTrunc : NcException {
        public NcTrunc() : base() {
        }
        public NcTrunc(string message) : base(message) {
        }
        public NcTrunc(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcTrunc(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcHdfErr : NcException {
        public NcHdfErr() : base() {
        }
        public NcHdfErr(string message) : base(message) {
        }
        public NcHdfErr(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcHdfErr(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcCantRead : NcException {
        public NcCantRead() : base() {
        }
        public NcCantRead(string message) : base(message) {
        }
        public NcCantRead(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcCantRead(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcCantWrite : NcException {
        public NcCantWrite() : base() {
        }
        public NcCantWrite(string message) : base(message) {
        }
        public NcCantWrite(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcCantWrite(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcCantCreate : NcException {
        public NcCantCreate() : base() {
        }
        public NcCantCreate(string message) : base(message) {
        }
        public NcCantCreate(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcCantCreate(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcFileMeta : NcException {
        public NcFileMeta() : base() {
        }
        public NcFileMeta(string message) : base(message) {
        }
        public NcFileMeta(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcFileMeta(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcDimMeta : NcException {
        public NcDimMeta() : base() {
        }
        public NcDimMeta(string message) : base(message) {
        }
        public NcDimMeta(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcDimMeta(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcAttMeta : NcException {
        public NcAttMeta() : base() {
        }
        public NcAttMeta(string message) : base(message) {
        }
        public NcAttMeta(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcAttMeta(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcVarMeta : NcException {
        public NcVarMeta() : base() {
        }
        public NcVarMeta(string message) : base(message) {
        }
        public NcVarMeta(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcVarMeta(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNoCompound : NcException {
        public NcNoCompound() : base() {
        }
        public NcNoCompound(string message) : base(message) {
        }
        public NcNoCompound(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNoCompound(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcAttExists : NcException {
        public NcAttExists() : base() {
        }
        public NcAttExists(string message) : base(message) {
        }
        public NcAttExists(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcAttExists(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNotNc4 : NcException {
        public NcNotNc4() : base() {
        }
        public NcNotNc4(string message) : base(message) {
        }
        public NcNotNc4(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNotNc4(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcStrictNc3 : NcException {
        public NcStrictNc3() : base() {
        }
        public NcStrictNc3(string message) : base(message) {
        }
        public NcStrictNc3(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcStrictNc3(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcBadGroupId : NcException {
        public NcBadGroupId() : base() {
        }
        public NcBadGroupId(string message) : base(message) {
        }
        public NcBadGroupId(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcBadGroupId(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcBadTypeId : NcException {
        public NcBadTypeId() : base() {
        }
        public NcBadTypeId(string message) : base(message) {
        }
        public NcBadTypeId(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcBadTypeId(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcBadFieldId : NcException {
        public NcBadFieldId() : base() {
        }
        public NcBadFieldId(string message) : base(message) {
        }
        public NcBadFieldId(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcBadFieldId(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcUnknownName : NcException {
        public NcUnknownName() : base() {
        }
        public NcUnknownName(string message) : base(message) {
        }
        public NcUnknownName(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcUnknownName(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcEnoGrp : NcException {
        public NcEnoGrp() : base() {
        }
        public NcEnoGrp(string message) : base(message) {
        }
        public NcEnoGrp(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcEnoGrp(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNullGrp : NcException {
        public NcNullGrp() : base() {
        }
        public NcNullGrp(string message) : base(message) {
        }
        public NcNullGrp(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNullGrp(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNullType : NcException {
        public NcNullType() : base() {
        }
        public NcNullType(string message) : base(message) {
        }
        public NcNullType(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNullType(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNullDim : NcException {
        public NcNullDim() : base() {
        }
        public NcNullDim(string message) : base(message) {
        }
        public NcNullDim(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNullDim(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNullVar : NcException {
        public NcNullVar() : base() {
        }
        public NcNullVar(string message) : base(message) {
        }
        public NcNullVar(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNullVar(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcNullAtt : NcException {
        public NcNullAtt() : base() {
        }
        public NcNullAtt(string message) : base(message) {
        }
        public NcNullAtt(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcNullAtt(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcElateDef : NcException {
        public NcElateDef() : base() {
        }
        public NcElateDef(string message) : base(message) {
        }
        public NcElateDef(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcElateDef(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcBufferOverflow : NcException {
        public NcBufferOverflow() : base() {
        }
        public NcBufferOverflow(string message) : base(message) {
        }
        public NcBufferOverflow(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcBufferOverflow(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
    public class NcDimUnlimited : NcException {
        public NcDimUnlimited() : base() {
        }
        public NcDimUnlimited(string message) : base(message) {
        }
        public NcDimUnlimited(string message, System.Exception inner) : base(message, inner) { 
        }
        protected NcDimUnlimited(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) {
        }
    }
}

