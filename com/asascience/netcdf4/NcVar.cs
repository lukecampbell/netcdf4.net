
/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcVar
 */
using System;
using System.Text;
using System.Collections.Generic;

namespace netcdf4 {
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

        public NcType GetType() {
            if(nullObject)
                return new NcType(); // Return null-type
            NetCDF.nc_type xtypep=0;
            Int32 xtype=0;

            NcCheck.Check(NetCDF.nc_inq_vartype(groupId, myId, ref xtypep));
            xtype = (int) xtypep;
            switch(xtype) {
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
                if(k.Value.GetId() == xtype)
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
                throw new exceptions.NcNullVar("Attempt to invoke NcGroup.GetId on a Null group");
            }
        }

        public Int32 GetDimCount() {
            CheckNull();
            Int32 dimCount=0;
            NcCheck.Check(NetCDF.nc_inq_varndims(groupId, myId, ref dimCount));
            return dimCount;
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
            throw new NotImplementedException("GetAtt() not implemented");
            return null;
        }

        public Dictionary<string, NcVarAtt> GetAtts() {
            throw new NotImplementedException("GetAtts() not implemented");
            return null;
        }

        public NcVarAtt PutAtt(string name, string dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcVarAtt PutAtt(string name, NcType type, byte[] dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcVarAtt PutAtt(string name, NcType type, short dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcVarAtt PutAtt(string name, NcType type, int dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, long dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcVarAtt PutAtt(string name, NcType type, float dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcVarAtt PutAtt(string name, NcType type, double dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, ushort dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, uint dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, ulong dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcVarAtt PutAtt(string name, NcType type, short[] dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, int[] dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, long[] dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }

        public NcVarAtt PutAtt(string name, NcType type, float[] dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, double[] dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, ushort[] dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, uint[] dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
        }
        
        public NcVarAtt PutAtt(string name, NcType type, ulong[] dataValues) {
            throw new NotImplementedException("PutAtt() not implemented");
            return null;
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

        private void BufferCheck(int len) {
            Int32 spaceRequired = 0;
            List<NcDim> dims = GetDims();
            foreach(NcDim dim in dims) {
                spaceRequired += dim.GetSize();
            }
            if( len < spaceRequired )
                throw new exceptions.NcBufferOverflow("Strict Checking: Not enough space available to store variable");
        }

        public void GetVar(StringBuilder dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(byte[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int16[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking) {
                BufferCheck(dataValues.Length);
            }
            NcCheck.Check(NetCDF.nc_get_var_int(groupId, myId, dataValues));

        }

        public void GetVar(float[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(double[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] index, StringBuilder dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] index, byte[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] index, Int16[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] index, Int32[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] index, float[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }
        public void GetVar(Int32[] index, double[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, StringBuilder dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, byte[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, Int16[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, Int32[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, float[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, double[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, StringBuilder dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, byte[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int16[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }

        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, float[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
        }
        
        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, double[] dataValues, bool strictChecking=true) {
            throw new NotImplementedException("GetVar() not implemented");
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

        public void PutVar(string dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(byte[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int16[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] dataValues) {
            NcCheck.Check(NetCDF.nc_put_var_int(groupId, myId, dataValues));
        }

        public void PutVar(float[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(double[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] index, string dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] index, byte[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] index, Int16[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] index, Int32[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] index, float[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }
        public void PutVar(Int32[] index, double[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, string dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, byte[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, Int16[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, Int32[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, float[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, double[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, StringBuilder dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, byte[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int16[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, Int32[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }

        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, float[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
        }
        
        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, double[] dataValues) {
            throw new NotImplementedException("PutVar() not implemented");
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
