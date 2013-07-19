
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcVar
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ASA.NetCDF4 {
    public enum ChunkMode {
        NC_CHUNKED    = 0,
        NC_CONTIGUOUS = 1
    }

    public enum EndianMode {
        NC_ENDIAN_NATIVE = 0,
        NC_ENDIAN_LITTLE = 1,
        NC_ENDIAN_BIG    = 2
    }
    public enum ChecksumMode {
        NC_NOCHECKSUM = 0,
        NC_FLETCHER32 = 1
    }
    public class NcVar {
        // Null Constructor
        public NcVar() {
            nullObject = true;
        }

        public NcVar(Int32 grpId, Int32 varId) {
            nullObject = false;
            myId = varId;
            groupId = grpId;
        }
        public NcVar(NcGroup grp, Int32 varId) {
            nullObject = false;
            myId = varId;
            groupId = grp.GetId();
        }

        public NcVar(NcVar rhs) {
            nullObject = rhs.nullObject;
            myId = rhs.myId;
            groupId = rhs.groupId;
        }

        public string GetName() {
            CheckNull();
            StringBuilder sb = new StringBuilder((int)NetCDF.netCDF_limits.NC_MAX_NAME);
            NcCheck.Check(NetCDF.nc_inq_varname(groupId, myId, sb));
            return sb.ToString();
        }

        public NcGroup GetParentGroup() {
            CheckNull();
            return new NcGroup(groupId);
        }

        public NcType GetNcType() {
            if(nullObject)
                return new NcType(); // Return null-type
            int xtypep=0;

            NcCheck.Check(NetCDF.nc_inq_vartype(groupId, myId, ref xtypep));
            switch(xtypep) {
                case (int)NcTypeEnum.NC_BYTE:
                    return NcByte.Instance;
                case (int)NcTypeEnum.NC_UBYTE:
                    return NcUbyte.Instance;
                case (int)NcTypeEnum.NC_CHAR:
                    return NcChar.Instance;
                case (int)NcTypeEnum.NC_SHORT:
                    return NcShort.Instance;
                case (int)NcTypeEnum.NC_USHORT:
                    return NcUshort.Instance;
                case (int)NcTypeEnum.NC_INT:
                    return NcInt.Instance;
                case (int)NcTypeEnum.NC_UINT:
                    return NcUint.Instance;
                case (int)NcTypeEnum.NC_INT64:
                    return NcInt64.Instance;
                case (int)NcTypeEnum.NC_UINT64:
                    return NcUint64.Instance;
                case (int)NcTypeEnum.NC_FLOAT:
                    return NcFloat.Instance;
                case(int)NcTypeEnum.NC_DOUBLE:
                    return NcDouble.Instance;
                default:
                    break;
            }
            NcGroup group = new NcGroup(groupId);
            Dictionary<string, NcType> types = group.GetTypes(Location.ParentsAndCurrent);
            foreach(KeyValuePair<string, NcType> k in types) {
                if(k.Value.GetId() == xtypep)
                    return k.Value;
            }
            // Should never get to here
            throw new exceptions.NcException("Type could not be identified");
            return null;
        }

        public void Rename(string newName) {
            CheckNull();
            NcCheck.Check(NetCDF.nc_rename_var(groupId, myId, newName));
        }

        public Int32 GetId() {
            CheckNull();
            return myId;
        }

        public bool IsNull() {
            return nullObject;
        }

        protected void CheckNull() {
            if(nullObject) {
                throw new exceptions.NcNullVar("Attempt to invoke NcVar method on a Null NcVar");
            }
        }

        public Int32 GetDimCount() {
            CheckNull();
            Int32 dimCount=0;
            NcCheck.Check(NetCDF.nc_inq_varndims(groupId, myId, ref dimCount));
            return dimCount;
        }

        public List<int> GetShape() {
            List<int> shape = new List<int>();
            foreach(NcDim d in GetDims())
                shape.Add(d.GetSize());
            return shape;
        }

        public int[] Shape {
            get {
                return GetShape().ToArray();
            }
        }

        public NcDim GetDim(Int32 i) {
            CheckNull();
            List<NcDim> ncDims = GetDims();
            if(i >= ncDims.Count || i < 0) 
                throw new exceptions.NcException("Index out of range");
            return ncDims[i];
        }
        
        public List<NcDim> GetDims() {
            CheckNull();
            Int32 dimCount = GetDimCount();

            List<NcDim> ncDims = new List<NcDim>(dimCount);
            Int32[] dimids = new Int32[dimCount];
            NcGroup parent = GetParentGroup();
            NcCheck.Check(NetCDF.nc_inq_vardimid(groupId, myId, dimids));

            for(int i=0;i<dimCount;i++) {
                ncDims.Add(new NcDim(parent, dimids[i]));
            }
            return ncDims;
        }
        
        public Int32 GetAttCount() {
            CheckNull();
            Int32 attCount=0;
            NcCheck.Check(NetCDF.nc_inq_varnatts(groupId, myId, ref attCount));
            return attCount;
        }

        public NcVarAtt GetAtt(string name) {
            foreach(KeyValuePair<string, NcVarAtt> k in GetAtts()) {
                if(k.Key == name)  {
                    return k.Value;
                } 
            }
            return new NcVarAtt(); // return null;
        }

        public Dictionary<string, NcVarAtt> GetAtts() {
            CheckNull();
            int attCount = GetAttCount();
            Dictionary<string, NcVarAtt> ncAtts = new Dictionary<string, NcVarAtt>();
            NcVarAtt tmpAtt;
            for(int i=0;i<attCount;i++) {
                tmpAtt = new NcVarAtt(groupId, myId, i);
                ncAtts.Add(tmpAtt.GetName(), tmpAtt);
            }
            return ncAtts;
        }

        public NcVarAtt PutAtt(string name, string dataValues) {
            CheckNull();
            NcCheck.Check(NetCDF.nc_put_att_text(groupId, myId, name, dataValues.Length, dataValues));
            return GetAtt(name);
        }

        public NcVarAtt PutAtt(string name, NcType type, sbyte datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_schar(groupId, myId, name, type.GetId(), 1, new sbyte[] {datumValue}));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, byte datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_uchar(groupId, myId, name, type.GetId(), 1, new byte[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, Int16 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_short(groupId, myId, name, type.GetId(), 1, new Int16[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, UInt16 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_ushort(groupId, myId, name, type.GetId(), 1, new UInt16[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, Int32 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_int(groupId, myId, name, type.GetId(), 1, new Int32[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, UInt32 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_uint(groupId, myId, name, type.GetId(), 1, new UInt32[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, Int64 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_longlong(groupId, myId, name, type.GetId(), 1, new Int64[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, UInt64 datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_ulonglong(groupId, myId, name, type.GetId(), 1, new UInt64[]{ datumValue }));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, float datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_float(groupId, myId, name, type.GetId(), 1, new float[] { datumValue }));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, double datumValue) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_double(groupId, myId, name, type.GetId(), 1, new double[]{ datumValue }));
            return GetAtt(name);
        }

        public NcVarAtt PutAtt(string name, NcType type, sbyte[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_schar(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, byte[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_uchar(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, Int16[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_short(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, UInt16[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_ushort(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, Int32[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_int(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, UInt32[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_uint(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, Int64[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_longlong(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, UInt64[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_ulonglong(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, float[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_float(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }
        
        public NcVarAtt PutAtt(string name, NcType type, double[] dataValues) {
            CheckNull();
            if(!type.IsFixedType())
                throw new NotImplementedException("PutAtt() not implemented for non-fixed types");
            NcCheck.Check(NetCDF.nc_put_att_double(groupId, myId, name, type.GetId(), dataValues.Length, dataValues));
            return GetAtt(name);
        }

        public void SetChunking(ChunkMode chunkMode, Int32[] chunkSizes) {
            CheckNull();
            NcCheck.Check(NetCDF.nc_def_var_chunking(groupId, myId, (Int32) chunkMode, chunkSizes));
        }

        public void GetChunkingParameters(ref ChunkMode chunkMode, ref Int32[] chunkSizes) {
            Int32 chunkModeInt = 0;
            chunkSizes = new Int32[GetDimCount()];
            NcCheck.Check(NetCDF.nc_inq_var_chunking(groupId, myId, ref chunkModeInt, chunkSizes));
            chunkMode = (ChunkMode) chunkModeInt;
        }

        /* Sets the compression parameters
          param enableShuffleFilter Set to true to turn on shuffle filter.
          param enableDeflateFilter Set to true to turn on deflate filter.
          param deflateLevel        The deflate level, must be 0 and 9.
        */
        public void SetCompression(bool enableShuffleFilter, bool enableDeflateFilter, Int32 deflateLevel) {
            CheckNull();
            if(enableDeflateFilter && (deflateLevel < 0 || deflateLevel > 9))
                throw new exceptions.NcException("The deflateLevel must be set between 0 and 9");
            NcCheck.Check(NetCDF.nc_def_var_deflate(groupId, myId, enableShuffleFilter ? 1 : 0,enableDeflateFilter ? 1 : 0, deflateLevel));

        }

        public void GetCompressionParameters(ref bool shuffleFilterEnabled, ref bool deflateFilterEnabled, ref Int32 deflateLevel) {
            CheckNull();
            Int32 enableShuffleFilterInt=0;
            Int32 enableDeflateFilterInt=0;
            NcCheck.Check(NetCDF.nc_inq_var_deflate(groupId, myId, ref enableShuffleFilterInt, ref enableDeflateFilterInt, ref deflateLevel));
            shuffleFilterEnabled = Convert.ToBoolean(enableShuffleFilterInt);
            deflateFilterEnabled = Convert.ToBoolean(enableDeflateFilterInt);
            
        }

        public void SetEndianness(EndianMode endianMode) {
            CheckNull();
            NcCheck.Check(NetCDF.nc_def_var_endian(groupId, myId, (int)endianMode));
        }

        public EndianMode GetEndianness() {
            CheckNull();
            Int32 modep=0;
            NcCheck.Check(NetCDF.nc_inq_var_endian(groupId, myId, ref modep));
            return (EndianMode)modep;
        }

        public void SetChecksum(ChecksumMode checksumMode) {
            CheckNull();
            NcCheck.Check(NetCDF.nc_def_var_fletcher32(groupId, myId, (int) checksumMode));
        }

        public ChecksumMode GetChecksum() {
            CheckNull();
            Int32 checkp=0;
            NcCheck.Check(NetCDF.nc_inq_var_fletcher32(groupId, myId, ref checkp));
            return (ChecksumMode) checkp;
        }

        protected void BufferCheck(int len) {
            Int32 spaceRequired = 1;
            List<int> shape = GetShape();
            foreach(int dimLen in shape) {
                spaceRequired *= dimLen;
            }
            if( len < spaceRequired )
                throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
        }

        protected void DimCheck(int len) {
            int dimCount = GetDimCount();
            if(len != dimCount)
                throw new exceptions.NcBufferOverflow("Index array must be the same length as the number of dimensions");
        }
        
        protected void NDimCheck(int indexLength, Int32[] counts, int arrayLen) {
            int sum = 1;
            if(indexLength != GetDimCount())
                throw new exceptions.NcBufferOverflow("Index array must be the same length as the number of dimensions");
            if(counts.Length != GetDimCount())
                throw new exceptions.NcBufferOverflow("Count array must be the same length as the number of dimensions");
            foreach(Int32 c in counts ) sum *= c;
            if(arrayLen < sum)
                throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
        }

        protected void NDimStrideCheck(int indexLength, Int32[] counts, Int32[] strides, int arrayLen) {
            int sum = 1;
            if(indexLength != GetDimCount())
                throw new exceptions.NcBufferOverflow("Index array must be the same length as the number of dimensions");
            if(counts.Length != GetDimCount())
                throw new exceptions.NcBufferOverflow("Count array must be the same length as the number of dimensions");
            if(counts.Length != strides.Length)
                throw new exceptions.NcBufferOverflow("Strides array must be the same length as the number of dimensions");
            for(int i=0;i<counts.Length;i++) {
                sum *= (int) Math.Ceiling((double) counts[i] / (double) strides[i]);
            }
            if(arrayLen < sum)
                throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
        }

        // Used to check if any of the dimensions are unlimited, if they are then clients should specify the indexes and lengths to use
        protected void DimUnlimitedCheck() {
            List<NcDim> dims = GetDims();
            foreach(NcDim dim in dims) {
                if(dim.IsUnlimited())
                    throw new exceptions.NcDimUnlimited("Can't write array to a dimension with NC_UNLIMITED without index and count");
            }
        }

        public NcArray GetVar() {
            CheckNull();
            int spaceRequired = 1;
            List<int> shape = GetShape();
            foreach(int dimLen in shape) {
                spaceRequired *= dimLen;
            }

            switch(GetNcType().GetTypeClass()) {

                case NcTypeEnum.NC_BYTE: {
                    sbyte[] buffer = new sbyte[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

                case NcTypeEnum.NC_UBYTE: {
                    byte[] buffer = new byte[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

                case NcTypeEnum.NC_SHORT: {
                    Int16[] buffer = new Int16[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

                case NcTypeEnum.NC_USHORT: {
                    UInt16[] buffer = new UInt16[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

                case NcTypeEnum.NC_INT: {
                    Int32[] buffer = new Int32[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

                case NcTypeEnum.NC_UINT: {
                    UInt32[] buffer = new UInt32[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

                case NcTypeEnum.NC_INT64: {
                    Int64[] buffer = new Int64[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

                case NcTypeEnum.NC_UINT64: {
                    UInt64[] buffer = new UInt64[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

                case NcTypeEnum.NC_FLOAT: {
                    float[] buffer = new float[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

                case NcTypeEnum.NC_DOUBLE: {
                    double[] buffer = new double[spaceRequired];
                    GetVar(buffer);
                    return new NcArray(buffer, shape);
                }

            }
            return new NcArray();
        }



        public void GetVar(sbyte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                BufferCheck(dataValues.Length);
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_get_var(groupId, myId, dataValues));
                return;
            }
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_get_var_text(groupId, myId, dataValues));
            else
                NcCheck.Check(NetCDF.nc_get_var_schar(groupId, myId, dataValues));
        }
        public void GetVar(byte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                BufferCheck(dataValues.Length);
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_get_var(groupId, myId, dataValues));
                return;
            }
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_get_var_text(groupId, myId, dataValues));
            else
                NcCheck.Check(NetCDF.nc_get_var_uchar(groupId, myId, dataValues));
        }

        public void GetVar(Int16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_get_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_get_var_short(groupId, myId, dataValues));
        }
        
        public void GetVar(UInt16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_get_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_get_var_ushort(groupId, myId, dataValues));
        }

        public void GetVar(Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_get_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_get_var_int(groupId, myId, dataValues));
        }
        
        public void GetVar(UInt32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_get_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_get_var_uint(groupId, myId, dataValues));
        }
        public void GetVar(Int64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_get_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_get_var_longlong(groupId, myId, dataValues));
        }
        
        public void GetVar(UInt64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_get_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_get_var_ulonglong(groupId, myId, dataValues));
        }

        public void GetVar(float[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var_float(groupId, myId, dataValues));
        }

        public void GetVar(double[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                BufferCheck(dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_var_double(groupId, myId, dataValues));
        }

        public int GetVar(Int32[] index, out string dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
            }
            if(GetNcType().GetTypeClass() != NcTypeEnum.NC_VLEN) {
                throw new exceptions.NcBadType("netCDF data type must be VLEN for strings at one index position");
            }
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer;
            NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
            dataValues = encoder.GetString(buffer);
            return buffer.Length;
        }

        public int GetVar(Int32[] index, sbyte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                sbyte[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var1_schar(groupId, myId, index, dataValues));
            return 1;
        }

        public int GetVar(Int32[] index, byte[] dataValues, bool strictChecking=true) {
            CheckNull();
            NcType type = GetNcType();
            NcTypeEnum typeClass = type.GetTypeClass();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(typeClass == NcTypeEnum.NC_VLEN) {
                byte[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            if(typeClass == NcTypeEnum.NC_OPAQUE) {
                NcOpaqueType tmp = new NcOpaqueType(type);
                if(strictChecking && dataValues.Length < tmp.GetTypeSize())
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
                NcCheck.Check(NetCDF.nc_get_var1(groupId, myId, index, dataValues));
                return tmp.GetTypeSize();
            }
            NcCheck.Check(NetCDF.nc_get_var1_uchar(groupId, myId, index, dataValues));
            return 1;
        }

        public int GetVar(Int32[] index, Int16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                Int16[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var1_short(groupId, myId, index, dataValues));
            return 1;
        }
        
        public int GetVar(Int32[] index, UInt16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                UInt16[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var1_ushort(groupId, myId, index, dataValues));
            return 1;
        }

        public int GetVar(Int32[] index, Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                Int32[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var1_int(groupId, myId, index, dataValues));
            return 1;
        }
        
        public int GetVar(Int32[] index, UInt32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                UInt32[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var1_uint(groupId, myId, index, dataValues));
            return 1;
        }
        
        public int GetVar(Int32[] index, Int64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                Int64[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var1_longlong(groupId, myId, index, dataValues));
            return 1;
        }
        
        public int GetVar(Int32[] index, UInt64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                UInt64[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var1_ulonglong(groupId, myId, index, dataValues));
            return 1;
        }

        public int GetVar(Int32[] index, float[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                float[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var1_float(groupId, myId, index, dataValues));
            return 1;
        }
        public int GetVar(Int32[] index, double[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1) 
                    throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                double[] buffer;
                NcCheck.Check(NetCDF.nc_get_var1_vlen(groupId, myId, index, out buffer));
                Array.Copy(buffer, dataValues, Math.Min(dataValues.Length, buffer.Length));
                return Math.Min(dataValues.Length, buffer.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var1_double(groupId, myId, index, dataValues));
            return 1;
        }

        public void GetVar(Int32[] index, Int32[] count, sbyte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_get_vara_text(groupId, myId, index, count, dataValues));
            else
                NcCheck.Check(NetCDF.nc_get_vara_schar(groupId, myId, index, count, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, byte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_get_vara_text(groupId, myId, index, count, dataValues));
            else
                NcCheck.Check(NetCDF.nc_get_vara_uchar(groupId, myId, index, count, dataValues));
        }

        public void GetVar(Int32[] index, Int32[] count, Int16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vara_short(groupId, myId, index, count, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, UInt16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vara_ushort(groupId, myId, index, count, dataValues));
        }

        public void GetVar(Int32[] index, Int32[] count, Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vara_int(groupId, myId, index, count, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, UInt32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vara_uint(groupId, myId, index, count, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, Int64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vara_longlong(groupId, myId, index, count, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, UInt64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vara_ulonglong(groupId, myId, index, count, dataValues));
        }

        public void GetVar(Int32[] index, Int32[] count, float[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vara_float(groupId, myId, index, count, dataValues));
        }

        public void GetVar(Int32[] index, Int32[] count, double[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vara_double(groupId, myId, index, count, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, sbyte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_get_vars_text(groupId, myId, index, count, stride, dataValues));
            else
                NcCheck.Check(NetCDF.nc_get_vars_schar(groupId, myId, index, count, stride, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, byte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_get_vars_text(groupId, myId, index, count, stride, dataValues));
            else
                NcCheck.Check(NetCDF.nc_get_vars_uchar(groupId, myId, index, count, stride, dataValues));
        }

        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, Int16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vars_short(groupId, myId, index, count, stride, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, UInt16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vars_ushort(groupId, myId, index, count, stride, dataValues));
        }

        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vars_int(groupId, myId, index, count, stride, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, UInt32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vars_uint(groupId, myId, index, count, stride, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, Int64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vars_longlong(groupId, myId, index, count, stride, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, UInt64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vars_ulonglong(groupId, myId, index, count, stride, dataValues));
        }

        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, float[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vars_float(groupId, myId, index, count, stride, dataValues));
        }
        
        public void GetVar(Int32[] index, Int32[] count, Int32[] stride, double[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_get_vars_double(groupId, myId, index, count, stride, dataValues));
        }

        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, StringBuilder dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }
        
        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, byte[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }
        
        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, Int16[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }
        
        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, Int32[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }
        
        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, float[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }
        
        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, double[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void PutVar(NcArray array, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(array.Length);
                DimUnlimitedCheck();
            }
            switch(array.GetNcType().GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    PutVar((sbyte[]) array.Array);
                    return;
                case NcTypeEnum.NC_UBYTE:
                    PutVar((byte[]) array.Array);
                    return;
                case NcTypeEnum.NC_SHORT:
                    PutVar((Int16[]) array.Array);
                    return;
                case NcTypeEnum.NC_USHORT:
                    PutVar((UInt16[]) array.Array);
                    return;
                case NcTypeEnum.NC_INT:
                    PutVar((Int32[]) array.Array);
                    return;
                case NcTypeEnum.NC_UINT:
                    PutVar((UInt32[]) array.Array);
                    return;
                case NcTypeEnum.NC_INT64:
                    PutVar((Int64[]) array.Array);
                    return;
                case NcTypeEnum.NC_UINT64:
                    PutVar((UInt64[]) array.Array);
                    return;
                case NcTypeEnum.NC_FLOAT:
                    PutVar((float[]) array.Array);
                    return;
                case NcTypeEnum.NC_DOUBLE:
                    PutVar((double[]) array.Array);
                    return;
            }
        }


        public void PutVar(sbyte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_put_var(groupId, myId, dataValues));
                return;
            }
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_put_var_text(groupId,myId,dataValues));
            else
                NcCheck.Check(NetCDF.nc_put_var_schar(groupId, myId, dataValues));
        }
        public void PutVar(byte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_put_var(groupId, myId, dataValues));
                return;
            }
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_put_var_text(groupId,myId,dataValues));
            else
                NcCheck.Check(NetCDF.nc_put_var_uchar(groupId, myId, dataValues));
        }

        public void PutVar(Int16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_put_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var_short(groupId, myId, dataValues));
        }
        
        public void PutVar(UInt16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_put_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var_ushort(groupId, myId, dataValues));
        }

        public void PutVar(Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_put_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var_int(groupId, myId, dataValues));
        }
        
        public void PutVar(UInt32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_put_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var_uint(groupId, myId, dataValues));
        }
        
        public void PutVar(Int64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_put_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var_longlong(groupId, myId, dataValues));
        }
        
        public void PutVar(UInt64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_ENUM) {
                NcCheck.Check(NetCDF.nc_put_var(groupId, myId, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var_ulonglong(groupId, myId, dataValues));
        }

        public void PutVar(float[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            NcCheck.Check(NetCDF.nc_put_var_float(groupId, myId, dataValues));
        }

        public void PutVar(double[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
                DimUnlimitedCheck();
            }
            NcCheck.Check(NetCDF.nc_put_var_double(groupId, myId, dataValues));
        }

        public void PutVar(Int32[] index, NcArray array, bool strictChecking=true) {
        }
        public void PutVar(Int32[] index, string dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() != NcTypeEnum.NC_VLEN)
                throw new exceptions.NcBadType("netCDF data type must be VLEN for strings at one index position");
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] byteBuffer = encoder.GetBytes(dataValues);
            NcCheck.Check(NetCDF.nc_put_var1_vlen<byte>(groupId, myId, index, byteBuffer));
        }

        public void PutVar(Int32[] index, sbyte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<sbyte>(groupId, myId, index, dataValues));
                return;
            }
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_put_var1_text(groupId, myId, index, dataValues));
            else
                NcCheck.Check(NetCDF.nc_put_var1_schar(groupId, myId, index, dataValues));
        }
        
        public void PutVar(Int32[] index, byte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<byte>(groupId, myId, index, dataValues));
                return;
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_OPAQUE) {
                NcOpaqueType tmp = new NcOpaqueType(GetNcType());
                if(strictChecking && dataValues.Length < tmp.GetTypeSize())
                    throw new exceptions.NcBufferOverflow("Value buffer be at least as large as the opaque type size");
                NcCheck.Check(NetCDF.nc_put_var1(groupId, myId, index, dataValues));
                return;
            }
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_put_var1_text(groupId, myId, index, dataValues));
            else
                NcCheck.Check(NetCDF.nc_put_var1_uchar(groupId, myId, index, dataValues));
        }

        public void PutVar(Int32[] index, Int16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<Int16>(groupId, myId, index, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var1_short(groupId, myId, index, dataValues));
        }
        
        public void PutVar(Int32[] index, UInt16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<UInt16>(groupId, myId, index, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var1_ushort(groupId, myId, index, dataValues));
        }

        public void PutVar(Int32[] index, Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<Int32>(groupId, myId, index, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var1_int(groupId, myId, index, dataValues));
        }
        
        public void PutVar(Int32[] index, UInt32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<UInt32>(groupId, myId, index, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var1_uint(groupId, myId, index, dataValues));
        }
        
        public void PutVar(Int32[] index, Int64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<Int64>(groupId, myId, index, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var1_longlong(groupId, myId, index, dataValues));
        }
        
        public void PutVar(Int32[] index, UInt64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<UInt64>(groupId, myId, index, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var1_ulonglong(groupId, myId, index, dataValues));
        }

        public void PutVar(Int32[] index, float[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<float>(groupId, myId, index, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var1_float(groupId, myId, index, dataValues));
        }
        public void PutVar(Int32[] index, double[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking){
                DimCheck(index.Length);
                if(dataValues.Length < 1)
                    throw new exceptions.NcBufferOverflow("Value buffer must have at least 1 value");
            }
            if(GetNcType().GetTypeClass() == NcTypeEnum.NC_VLEN) {
                NcCheck.Check(NetCDF.nc_put_var1_vlen<double>(groupId, myId, index, dataValues));
                return;
            }
            NcCheck.Check(NetCDF.nc_put_var1_double(groupId, myId, index, dataValues));
        }

        public void PutVar(Int32[] index, Int32[] count, sbyte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_put_vara_text(groupId, myId, index, count, dataValues));
            else
                NcCheck.Check(NetCDF.nc_put_vara_schar(groupId, myId, index, count, dataValues));

        }
        public void PutVar(Int32[] index, Int32[] count, byte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_put_vara_text(groupId, myId, index, count, dataValues));
            else
                NcCheck.Check(NetCDF.nc_put_vara_uchar(groupId, myId, index, count, dataValues));

        }

        public void PutVar(Int32[] index, Int32[] count, Int16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vara_short(groupId, myId, index, count, dataValues));
        }
        
        public void PutVar(Int32[] index, Int32[] count, UInt16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vara_ushort(groupId, myId, index, count, dataValues));
        }

        public void PutVar(Int32[] index, Int32[] count, Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vara_int(groupId, myId, index, count, dataValues));
        }
        public void PutVar(Int32[] index, Int32[] count, UInt32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vara_uint(groupId, myId, index, count, dataValues));
        }
        
        public void PutVar(Int32[] index, Int32[] count, Int64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vara_longlong(groupId, myId, index, count, dataValues));
        }
        
        public void PutVar(Int32[] index, Int32[] count, UInt64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vara_ulonglong(groupId, myId, index, count, dataValues));
        }

        public void PutVar(Int32[] index, Int32[] count, float[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vara_float(groupId, myId, index, count, dataValues));
        }

        public void PutVar(Int32[] index, Int32[] count, double[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) 
                NDimCheck(index.Length, count, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vara_double(groupId, myId, index, count, dataValues));
        }

        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, sbyte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_put_vars_text(groupId, myId, index, count, stride, dataValues));
            else
                NcCheck.Check(NetCDF.nc_put_vars_schar(groupId, myId, index, count, stride, dataValues));
        }
        
        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, byte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            if(NcChar.Instance.Equals(GetNcType()))
                NcCheck.Check(NetCDF.nc_put_vars_text(groupId, myId, index, count, stride, dataValues));
            else
                NcCheck.Check(NetCDF.nc_put_vars_uchar(groupId, myId, index, count, stride, dataValues));
        }

        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, Int16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vars_short(groupId, myId, index, count, stride, dataValues));
        }
        
        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, UInt16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vars_ushort(groupId, myId, index, count, stride, dataValues));
        }

        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vars_int(groupId, myId, index, count, stride, dataValues));
        }
        
        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, UInt32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vars_uint(groupId, myId, index, count, stride, dataValues));
        }
        
        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, Int64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vars_longlong(groupId, myId, index, count, stride, dataValues));
        }
        
        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, UInt64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vars_ulonglong(groupId, myId, index, count, stride, dataValues));
        }

        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, float[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vars_float(groupId, myId, index, count, stride, dataValues));
        }
        
        public void PutVar(Int32[] index, Int32[] count, Int32[] stride, double[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                NDimStrideCheck(index.Length, count, stride, dataValues.Length);
            NcCheck.Check(NetCDF.nc_put_vars_double(groupId, myId, index, count, stride, dataValues));
        }

        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, string dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }
        
        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, byte[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }
        
        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, Int16[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }
        
        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, Int32[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }
        
        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, float[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }
        
        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] imapp, double[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }
        private bool nullObject;
        private Int32 myId;
        private Int32 groupId;

    }
}
