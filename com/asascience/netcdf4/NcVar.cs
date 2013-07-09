
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
        public NcVar() {
            throw new NotImplementedException("NcVar not implemented");
        }

        public NcVar(NcGroup grp, Int32 varId) {
            throw new NotImplementedException("NcVar not implemented");
        }

        public NcVar(NcVar rhs) {
            throw new NotImplementedException("NcVar not implemented");
        }

        public string GetName() {
            throw new NotImplementedException("GetName() not implemented");
            return null;
        }

        public NcGroup GetParentGroup() {
            throw new NotImplementedException("GetParentGroup() not implemented");
            return null;
        }

        public NcType GetType() {
            throw new NotImplementedException("GetType() not implemented");
            return null;
        }

        public void Rename(string newName) {
            throw new NotImplementedException("Rename() not implemented");
        }

        public Int32 GetId() {
            throw new NotImplementedException("GetId() not implemented");
        }

        public bool IsNull() {
            return nullObject;
        }

        public Int32 GetDimCount() {
            throw new NotImplementedException("GetDimCount() not implemented");
            return 0;
        }

        public NcDim GetDim(Int32 i) {
            throw new NotImplementedException("GetDim() not implemented");
            return null;
        }
        
        public List<NcDim> GetDims() {
            throw new NotImplementedException("GetDims() not implemented");
            return null;
        }
        
        public Int32 GetAttCount() {
            throw new NotImplementedException("GetAttCount() not implemented");
            return 0;
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

        public void SetChunking(ChunkMode chunkMode, List<Int32> chunkSizes) {
            throw new NotImplementedException("SetChunking() not implemented");
        }

        public void GetChunkingParameters(ChunkMode chunkMode, List<Int32> chunkSizes) {
            throw new NotImplementedException("GetChunkingParameters() not implemented");
        }

        /* Sets the compression parameters
          param enableShuffleFilter Set to true to turn on shuffle filter.
          param enableDeflateFilter Set to true to turn on deflate filter.
          param deflateLevel        The deflate level, must be 0 and 9.
        */
        public void SetCompression(bool enableShuffleFilter, bool enableDeflateFilter, ref Int32 deflateLevel) {
            throw new NotImplementedException("SetCompression() not implemented");
        }

        public void GetCompressionParameters(ref bool shuffleFilterEnabled, ref bool deflateFilterEnabled, ref Int32 deflateLevel) {
            throw new NotImplementedException("GetCompressionParameters() not implemented");
        }

        public void SetEndianness(EndianMode endianMode) {
            throw new NotImplementedException("SetEndinanness() not implemented");
        }

        public EndianMode GetEndianness() {
            throw new NotImplementedException("GetEndianness() not implemented");
            return 0;
        }

        public void SetChecksum(ChecksumMode checksumMode) {
            throw new NotImplementedException("SetChecksum() not implemented");
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
            throw new NotImplementedException("GetVar() not implemented");
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

        public void GetVar(Int32[] startp, Int32[] countp, Int32[] stridep, StringBuider dataValues, bool strictChecking=true) {
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
            throw new NotImplementedException("PutVar() not implemented");
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

        public void PutVar(Int32[] startp, Int32[] countp, Int32[] stridep, StringBuider dataValues) {
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
