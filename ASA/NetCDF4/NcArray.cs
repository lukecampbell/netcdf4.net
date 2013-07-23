/*
 * Author Luke Campbell <LCampbell@ASAScience.Com>
 * ASA.NetCDF4.NcArray
 */

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace ASA.NetCDF4 {
    public class NcArray {
        private NcType type;
        private int ndim;
        private int[] shape;

        private Array mArray;

        public NcArray() {
            isNull = true;
        }

        public NcArray(NcType type, int[] shape) {
            int spaceRequired = 1;
            this.shape = shape;
            ndim = shape.Length;
            CheckShape(shape);
            foreach(int dimLen in shape) {
                spaceRequired *= dimLen;
            }
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    mArray = new sbyte[spaceRequired];
                    break;
                case NcTypeEnum.NC_UBYTE:
                    mArray = new byte[spaceRequired];
                    break;
                case NcTypeEnum.NC_SHORT:
                    mArray = new Int16[spaceRequired];
                    break;
                case NcTypeEnum.NC_USHORT:
                    mArray = new UInt16[spaceRequired];
                    break;
                case NcTypeEnum.NC_INT:
                    mArray = new Int32[spaceRequired];
                    break;
                case NcTypeEnum.NC_UINT:
                    mArray = new UInt32[spaceRequired];
                    break;
                case NcTypeEnum.NC_INT64:
                    mArray = new Int64[spaceRequired];
                    break;
                case NcTypeEnum.NC_UINT64:
                    mArray = new UInt64[spaceRequired];
                    break;
                case NcTypeEnum.NC_FLOAT:
                    mArray = new float[spaceRequired];
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    mArray = new double[spaceRequired];
                    break;

                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            this.type = type;
            isNull = false;
        }
        public NcArray(NcType type, List<Int32> shape) {
            int spaceRequired = 1;
            this.shape = shape.ToArray();
            ndim = shape.Count;
            CheckShape(shape.ToArray());
            foreach(int dimLen in shape) {
                spaceRequired *= dimLen;
            }
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    mArray = new sbyte[spaceRequired];
                    break;
                case NcTypeEnum.NC_UBYTE:
                    mArray = new byte[spaceRequired];
                    break;
                case NcTypeEnum.NC_SHORT:
                    mArray = new Int16[spaceRequired];
                    break;
                case NcTypeEnum.NC_USHORT:
                    mArray = new UInt16[spaceRequired];
                    break;
                case NcTypeEnum.NC_INT:
                    mArray = new Int32[spaceRequired];
                    break;
                case NcTypeEnum.NC_UINT:
                    mArray = new UInt32[spaceRequired];
                    break;
                case NcTypeEnum.NC_INT64:
                    mArray = new Int64[spaceRequired];
                    break;
                case NcTypeEnum.NC_UINT64:
                    mArray = new UInt64[spaceRequired];
                    break;
                case NcTypeEnum.NC_FLOAT:
                    mArray = new float[spaceRequired];
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    mArray = new double[spaceRequired];
                    break;

                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            this.type = type;
            isNull = false;
        }

        public NcArray(Array array, NcType type, int[] shape=null) {
            if(type == null || type.IsNull()) 
                throw new exceptions.NcNullType("Can't make an array with null type");
            this.type = type;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }
        
        public NcArray(Array array, NcType type, List<int> shape=null) {
            if(type == null || type.IsNull()) 
                throw new exceptions.NcNullType("Can't make an array with null type");
            this.type = type;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(sbyte[] array, List<Int32> shape=null) {
            type = NcByte.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(byte[] array, List<Int32> shape=null) {
            type = NcUbyte.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(Int16[] array, List<Int32> shape=null) {
            type = NcShort.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(UInt16[] array, List<Int32> shape=null) {
            type = NcUshort.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(Int32[] array, List<Int32> shape=null) {
            type = NcInt.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(UInt32[] array, List<Int32> shape=null) {
            type = NcUint.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(Int64[] array, List<Int32> shape=null) {
            type = NcInt64.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(UInt64[] array, List<Int32> shape=null) {
            type = NcUint64.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(float[] array, List<Int32> shape=null) {
            type = NcFloat.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(double[] array, List<Int32> shape=null) {
            type = NcDouble.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            isNull = false;
            CheckShape(shape);
        }


        public NcArray(sbyte[] array, int[] shape=null) {
            type = NcByte.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(byte[] array, int[] shape=null) {
            type = NcUbyte.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(Int16[] array, int[] shape=null) {
            type = NcShort.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(UInt16[] array, int[] shape=null) {
            type = NcUshort.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(Int32[] array, int[] shape=null) {
            type = NcInt.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(UInt32[] array, int[] shape=null) {
            type = NcUint.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(Int64[] array, int[] shape=null) {
            type = NcInt64.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(UInt64[] array, int[] shape=null) {
            type = NcUint64.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(float[] array, int[] shape=null) {
            type = NcFloat.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public NcArray(double[] array, int[] shape=null) {
            type = NcDouble.Instance;
            mArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            isNull = false;
            CheckShape(shape);
        }

        public void CheckIndex(int[] index) {
            if (index.Length != ndim) {
                throw new exceptions.NcInvalidArg("Index must have the same number of elements as there are dimensions in the array.");
            }
            for(int i=0;i<index.Length;i++) {
                if(index[i] >= shape[i]) {
                    throw new exceptions.NcInvalidArg("Index must not exceed any dimension.");
                }
            }
        }



        private int offset(int[] index) {
            int a = 1;
            int offset = 0;
            CheckIndex(index);

            for(int i=0;i<ndim;i++) {
                a=1;
                for(int j=i+1;j<(ndim);j++) a *= shape[j];
                offset += a * index[i];
            }
            return offset;
        }
        
        // recursive alg for slicing
        private int rslice_offset(int[] start, int[] stop, int[] stride, int dim, List<int>index, List<int> results) {
            if(dim == ndim) {
                results.Add(offset(index.ToArray()));
                return 0;
            }


            for(int i=start[dim]; i < stop[dim]; i+= stride[dim]) {
                index.Add(i);
                rslice_offset(start, stop, stride, dim+1,index, results);
                index.RemoveAt(index.Count-1);
            }
            return 0;
        }

        public NcArray FillSlice(Object fillValue, int[] start=null, int[] stop=null, int[] stride=null) {
            CheckNull();
            if(start == null) {
                start = new int[ndim];
                for(int i=0;i<ndim;i++) start[i] = 0;
            }

            if(stop == null) {
                stop = new int[ndim];
                for(int i=0;i<ndim;i++) stop[i] = shape[i];
            }

            if(stride == null) {
                stride = new int[ndim];
                for(int i=0;i<ndim;i++) stride[i] = 1;
            }

            if(start.Length != ndim || stop.Length != ndim || stride.Length != ndim) {
                throw new exceptions.NcInvalidArg("The slice arguments must have enough elements for the number of dimensions in the array");
            }

            for(int i=0;i<ndim;i++) {
                if(start[i] >= stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }


            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                mArray.SetValue(fillValue, offset);
            }
            return this;
        }

        public NcArray Slice(int[] start=null, int[] stop=null, int[] stride=null)  {
            CheckNull();
            if(start == null) {
                start = new int[ndim];
                for(int i=0;i<ndim;i++) start[i] = 0;
            }

            if(stop == null) {
                stop = new int[ndim];
                for(int i=0;i<ndim;i++) stop[i] = shape[i];
            }

            if(stride == null) {
                stride = new int[ndim];
                for(int i=0;i<ndim;i++) stride[i] = 1;
            }

            if(start.Length != ndim || stop.Length != ndim || stride.Length != ndim) {
                throw new exceptions.NcInvalidArg("The slice arguments must have enough elements for the number of dimensions in the array");
            }

            for(int i=0;i<ndim;i++) {
                if(start[i] >= stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }
            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            Array array;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    array = new sbyte[totalSize];
                    break;

                case NcTypeEnum.NC_UBYTE:
                    array = new byte[totalSize];
                    break;

                case NcTypeEnum.NC_SHORT:
                    array = new Int16[totalSize];
                    break;

                case NcTypeEnum.NC_USHORT:
                    array = new UInt16[totalSize];
                    break;

                case NcTypeEnum.NC_INT:
                    array = new Int32[totalSize];
                    break;

                case NcTypeEnum.NC_UINT:
                    array = new UInt32[totalSize];
                    break;

                case NcTypeEnum.NC_INT64:
                    array = new Int64[totalSize];
                    break;

                case NcTypeEnum.NC_UINT64:
                    array = new UInt64[totalSize];
                    break;

                case NcTypeEnum.NC_FLOAT:
                    array = new float[totalSize];
                    break;

                case NcTypeEnum.NC_DOUBLE:
                    array = new double[totalSize];
                    break;
                default:
                    throw new exceptions.NcBadType("Unsupported type encountered during slicing");
            }


            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array.SetValue(mArray.GetValue(offset), arrayOffset++);
            }
            return new NcArray(array, type, dimLens);
        }


        public T ValueAt<T>(params int[] index) {
            T result = (T) mArray.GetValue( offset(index) );
            return result;
        }

        public T Value<T>(int[] index) {
            T result = (T) mArray.GetValue( offset(index) );
            return result;
        }

        public void SetValue(Object val, int[] index) {
            mArray.SetValue(val, offset(index));
        }

        public Object GetValue(int[] index) {
            return mArray.GetValue(offset(index));
        }

        public void SetValueAt(Object val, params int[] index) {
            mArray.SetValue(val, offset(index));
        }

        public Object GetValueAt(params int[] index) {
            return mArray.GetValue(offset(index));
        }

        public sbyte GetSbyteAt(params int[] index) {
            sbyte retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (sbyte) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (sbyte) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (sbyte) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (sbyte) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (sbyte) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (sbyte) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (sbyte) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (sbyte) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (sbyte) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (sbyte) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public byte GetByteAt(params int[] index) {
            byte retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (byte) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (byte) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (byte) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (byte) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (byte) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (byte) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (byte) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (byte) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (byte) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (byte) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public Int16 GetInt16At(params int[] index) {
            Int16 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (Int16) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (Int16) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (Int16) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (Int16) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (Int16) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (Int16) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (Int16) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (Int16) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (Int16) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (Int16) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public UInt16 GetUInt16At(params int[] index) {
            UInt16 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (UInt16) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (UInt16) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (UInt16) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (UInt16) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (UInt16) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (UInt16) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (UInt16) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (UInt16) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (UInt16) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (UInt16) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public Int32 GetInt32At(params int[] index) {
            Int32 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (Int32) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (Int32) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (Int32) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (Int32) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (Int32) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (Int32) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (Int32) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (Int32) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (Int32) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (Int32) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public UInt32 GetUInt32At(params int[] index) {
            UInt32 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (UInt32) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (UInt32) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (UInt32) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (UInt32) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (UInt32) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (UInt32) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (UInt32) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (UInt32) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (UInt32) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (UInt32) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public Int64 GetInt64At(params int[] index) {
            Int64 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (Int64) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (Int64) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (Int64) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (Int64) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (Int64) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (Int64) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (Int64) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (Int64) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (Int64) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (Int64) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public UInt64 GetUInt64At(params int[] index) {
            UInt64 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (UInt64) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (UInt64) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (UInt64) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (UInt64) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (UInt64) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (UInt64) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (UInt64) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (UInt64) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (UInt64) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (UInt64) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public float GetFloatAt(params int[] index) {
            float retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (float) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (float) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (float) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (float) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (float) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (float) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (float) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (float) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (float) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (float) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public double GetDoubleAt(params int[] index) {
            double retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (double) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (double) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (double) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (double) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (double) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (double) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (double) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (double) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (double) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (double) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public sbyte GetSbyte(int[] index) {
            sbyte retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (sbyte) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (sbyte) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (sbyte) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (sbyte) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (sbyte) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (sbyte) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (sbyte) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (sbyte) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (sbyte) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (sbyte) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public byte GetByte(int[] index) {
            byte retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (byte) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (byte) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (byte) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (byte) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (byte) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (byte) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (byte) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (byte) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (byte) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (byte) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public Int16 GetInt16(int[] index) {
            Int16 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (Int16) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (Int16) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (Int16) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (Int16) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (Int16) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (Int16) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (Int16) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (Int16) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (Int16) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (Int16) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public UInt16 GetUInt16(int[] index) {
            UInt16 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (UInt16) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (UInt16) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (UInt16) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (UInt16) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (UInt16) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (UInt16) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (UInt16) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (UInt16) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (UInt16) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (UInt16) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public Int32 GetInt32(int[] index) {
            Int32 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (Int32) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (Int32) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (Int32) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (Int32) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (Int32) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (Int32) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (Int32) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (Int32) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (Int32) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (Int32) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public UInt32 GetUInt32(int[] index) {
            UInt32 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (UInt32) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (UInt32) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (UInt32) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (UInt32) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (UInt32) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (UInt32) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (UInt32) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (UInt32) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (UInt32) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (UInt32) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public Int64 GetInt64(int[] index) {
            Int64 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (Int64) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (Int64) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (Int64) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (Int64) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (Int64) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (Int64) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (Int64) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (Int64) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (Int64) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (Int64) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public UInt64 GetUInt64(int[] index) {
            UInt64 retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (UInt64) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (UInt64) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (UInt64) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (UInt64) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (UInt64) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (UInt64) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (UInt64) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (UInt64) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (UInt64) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (UInt64) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public float GetFloat(int[] index) {
            float retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (float) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (float) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (float) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (float) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (float) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (float) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (float) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (float) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (float) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (float) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

        public double GetDouble(int[] index) {
            double retval;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    retval = (double) ((sbyte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UBYTE:
                    retval = (double) ((byte) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_SHORT:
                    retval = (double) ((Int16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_USHORT:
                    retval = (double) ((UInt16) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT:
                    retval = (double) ((Int32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT:
                    retval = (double) ((UInt32) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_INT64:
                    retval = (double) ((Int64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_UINT64:
                    retval = (double) ((UInt64) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_FLOAT:
                    retval = (double) ((float) mArray.GetValue(offset(index)));
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    retval = (double) ((double) mArray.GetValue(offset(index)));
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }




        public Array Array {
            get {
                return mArray;
            }
        }

        public bool Equals(NcArray rhs) {
            if(!CompatibleArray(rhs))
                return false;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((sbyte) mArray.GetValue(i) != (sbyte) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                case NcTypeEnum.NC_UBYTE: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((byte) mArray.GetValue(i) != (byte) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                case NcTypeEnum.NC_SHORT: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((Int16) mArray.GetValue(i) != (Int16) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                case NcTypeEnum.NC_USHORT: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((UInt16) mArray.GetValue(i) != (UInt16) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                case NcTypeEnum.NC_INT: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((Int32) mArray.GetValue(i) != (Int32) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                case NcTypeEnum.NC_UINT: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((UInt32) mArray.GetValue(i) != (UInt32) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                case NcTypeEnum.NC_INT64: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((Int64) mArray.GetValue(i) != (Int64) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                case NcTypeEnum.NC_UINT64: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((UInt64) mArray.GetValue(i) != (UInt64) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                case NcTypeEnum.NC_FLOAT: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((float) mArray.GetValue(i) != (float) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                case NcTypeEnum.NC_DOUBLE: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((double) mArray.GetValue(i) != (double) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
                default:
                    throw new exceptions.NcBadType("NcArray unsupported type detected in comparison");
            }
            return true;
        }


        public NcArray Fill(Object val) {
            for(int i=0;i<mArray.Length;i++) {
                mArray.SetValue(val, i);
            }
            return this;
        }
        
        protected void CheckShape(int[] shape) {
            for(int i=0;i<shape.Length;i++)
                if(shape[i] < 1)
                    throw new exceptions.NcInvalidArg("A dimension must have at least one element");
        }

        protected void CheckShape(List<int> shape) {
            CheckShape(shape.ToArray());
        }


        protected void CheckNull() {
            if(isNull) {
                throw new exceptions.NcNullVar("Attempt to invoke NcArray method on a Null NcVar");
            }
        }

        public NcType GetNcType() {
            return type;
        }

        public int[] Shape {
            get { 
                return shape;
            }
        }

        public int Length {
            get {
                int l=1;
                for(int i=0;i<ndim;i++) {
                    l *= shape[i];
                }
                return l;
            }
        }

        private bool isNull;
        
        public bool IsNull() {
            return isNull;
        }

        private void stringify(StringBuilder buf, int dim=0, List<int> index=null) {
            if(index==null) {
                index = new List<int>();
            }
            int ndim = shape.Length;
            if(dim == ndim) {
                buf.Append(mArray.GetValue(offset(index.ToArray())));
                buf.Append(" ");
                return;
            }
            for(int i=0;i<shape[dim];i++) {
                if(dim < (ndim-1)) 
                    buf.Append('[');
                index.Add(i);
                stringify(buf, dim+1, index);
                index.RemoveAt(index.Count-1);
                if(dim < (ndim-1))  {
                    buf.Append(']');
                    if(i < (shape[dim]-1)) {
                        buf.Append('\n');
                        for(int j=0;j<dim;j++)
                            buf.Append(' ');
                    }
                }

            }
        }

        public NcArray Reshape(int[] shape) {
            int spaceRequired = 1;
            for(int i=0;i<shape.Length;i++)
                spaceRequired *= shape[i];
            if(mArray.Length != spaceRequired) 
                throw new exceptions.NcInvalidArg("Total size of new array must be unchanged"); 
            this.shape = shape;
            this.ndim = shape.Length;
            return this;
        }

        public override string ToString() { 
            StringBuilder sb = new StringBuilder();
            stringify(sb);
            return sb.ToString();
        }

        public static NcArray Arange(NcType type, int r1, int r2=0, int stride=1, int[] shape=null) {
            int start=0, stop=0;
            if(r2 != 0) {
                start = r1;
                stop = r2;
            } else {
                start = 0;
                stop = r1;
            }

            int spaceRequired = (int) Math.Ceiling((double) (stop - start) / (double) (stride));

            if(shape == null) {
                shape = new int[] { spaceRequired };
            }

            int tmp=1;
            for(int i=0;i<shape.Length;i++)
                tmp *= shape[i];
            if(tmp != spaceRequired)
                throw new exceptions.NcInvalidArg("Total size of the array must match the overall shape");

            Array array;

            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                        array = new sbyte[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((sbyte)i,i);
                        break; 
                case NcTypeEnum.NC_UBYTE:
                        array = new byte[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((byte)i,i);
                        break; 
                case NcTypeEnum.NC_SHORT:
                        array = new Int16[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((Int16)i,i);
                        break; 
                case NcTypeEnum.NC_USHORT:
                        array = new UInt16[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((UInt16)i,i);
                        break; 
                case NcTypeEnum.NC_INT:
                        array = new Int32[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((Int32)i,i);
                        break; 
                case NcTypeEnum.NC_UINT:
                        array = new UInt32[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((UInt32)i,i);
                        break; 
                case NcTypeEnum.NC_INT64:
                        array = new Int64[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((Int64)i,i);
                        break; 
                case NcTypeEnum.NC_UINT64:
                        array = new UInt64[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((UInt64)i,i);
                        break; 
                case NcTypeEnum.NC_FLOAT:
                        array = new float[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((float)i,i);
                        break; 
                case NcTypeEnum.NC_DOUBLE:
                        array = new double[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((double)i,i);
                        break; 
                default:
                        throw new exceptions.NcBadType("Unsupported NcType for NcArray");
            }

            return new NcArray(array, type, shape);
        }

        public bool CompatibleType(NcType type) {
            if(!type.Equals(type)) {
                return false;
            }
            return true;
        }


        public bool CompatibleShape(int[] shape) {
            if(this.shape.Length !=shape.Length) {
                return false;
            }
            for(int i=0;i<shape.Length;i++) {
                if(this.shape[i] != shape[i]) {
                    return false;
                }
            }
            return true;
        }

        public bool CompatibleArray(NcArray array) {
            if(!CompatibleType(array.type))
                return false;
            if(!CompatibleShape(array.shape))
                return false;
            return true;
        }

        public NcArray Copy() {
            Array array=null;
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    array = new sbyte[mArray.Length];
                    break; 
                case NcTypeEnum.NC_UBYTE:
                    array = new byte[mArray.Length];
                    break; 
                case NcTypeEnum.NC_SHORT:
                    array = new Int16[mArray.Length];
                    break; 
                case NcTypeEnum.NC_USHORT:
                    array = new UInt16[mArray.Length];
                    break; 
                case NcTypeEnum.NC_INT:
                    array = new Int32[mArray.Length];
                    break; 
                case NcTypeEnum.NC_UINT:
                    array = new UInt32[mArray.Length];
                    break; 
                case NcTypeEnum.NC_INT64:
                    array = new Int64[mArray.Length];
                    break; 
                case NcTypeEnum.NC_UINT64:
                    array = new UInt64[mArray.Length];
                    break; 
                case NcTypeEnum.NC_FLOAT:
                    array = new float[mArray.Length];
                    break; 
                case NcTypeEnum.NC_DOUBLE:
                    array = new double[mArray.Length];
                    break; 
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            Array.Copy(mArray, array, mArray.Length);
            return new NcArray(array, type, shape);
        }
        public static NcArray Copy(NcArray rhs) {
            return rhs.Copy();
        }

        public static NcArray Add(NcArray a, NcArray b) {
            NcArray result = a.Copy();
            result.Add(b);
            return result;
        }

        public static NcArray operator+(NcArray a, NcArray b) {
            return Add(a,b);
        }

        public static NcArray Sub(NcArray a, NcArray b) {
            NcArray result = a.Copy();
            result.Sub(b);
            return result;
        }

        public static NcArray operator-(NcArray a, NcArray b) {
            return Sub(a,b);
        }

        public static NcArray Mult(NcArray a, NcArray b) {
            NcArray result = a.Copy();
            result.Mult(b);
            return result;
        }

        public static NcArray operator*(NcArray a, NcArray b) {
            return Mult(a,b);
        }

        public static NcArray Div(NcArray a, NcArray b) {
            NcArray result = a.Copy();
            result.Div(b);
            return result;
        }

        public static NcArray operator/(NcArray a, NcArray b) {
            return Div(a,b);
        }

        public NcArray Sub(NcArray array) {
            if(!CompatibleShape(array.shape)) {
                throw new exceptions.NcInvalidArg("Array shape mismatch");
            }
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (sbyte)mArray.GetValue(i) - (sbyte)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UBYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (byte)mArray.GetValue(i) - (byte)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_SHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int16)mArray.GetValue(i) - (Int16)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_USHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt16)mArray.GetValue(i) - (UInt16)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_INT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int32)mArray.GetValue(i) - (Int32)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UINT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt32)mArray.GetValue(i) - (UInt32)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_INT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int64)mArray.GetValue(i) - (Int64)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UINT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt64)mArray.GetValue(i) - (UInt64)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_FLOAT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (float)mArray.GetValue(i) - (float)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_DOUBLE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (double)mArray.GetValue(i) - (double)array.mArray.GetValue(i), i);
                    break; 
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        public NcArray Add(NcArray array) {
            if(!CompatibleShape(array.shape)) {
                throw new exceptions.NcInvalidArg("Array shape mismatch");
            }
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (sbyte)mArray.GetValue(i) + (sbyte)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UBYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (byte)mArray.GetValue(i) + (byte)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_SHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int16)mArray.GetValue(i) + (Int16)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_USHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt16)mArray.GetValue(i) + (UInt16)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_INT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int32)mArray.GetValue(i) + (Int32)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UINT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt32)mArray.GetValue(i) + (UInt32)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_INT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int64)mArray.GetValue(i) + (Int64)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UINT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt64)mArray.GetValue(i) + (UInt64)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_FLOAT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (float)mArray.GetValue(i) + (float)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_DOUBLE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (double)mArray.GetValue(i) + (double)array.mArray.GetValue(i), i);
                    break; 
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        public NcArray Mult(NcArray array) {
            if(!CompatibleShape(array.shape)) {
                throw new exceptions.NcInvalidArg("Array shape mismatch");
            }
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (sbyte)mArray.GetValue(i) * (sbyte)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UBYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (byte)mArray.GetValue(i) * (byte)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_SHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int16)mArray.GetValue(i) * (Int16)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_USHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt16)mArray.GetValue(i) * (UInt16)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_INT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int32)mArray.GetValue(i) * (Int32)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UINT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt32)mArray.GetValue(i) * (UInt32)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_INT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int64)mArray.GetValue(i) * (Int64)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UINT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt64)mArray.GetValue(i) * (UInt64)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_FLOAT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (float)mArray.GetValue(i) * (float)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_DOUBLE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (double)mArray.GetValue(i) * (double)array.mArray.GetValue(i), i);
                    break; 
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        public NcArray Div(NcArray array) {
            if(!CompatibleShape(array.shape)) {
                throw new exceptions.NcInvalidArg("Array shape mismatch");
            }
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (sbyte)mArray.GetValue(i) / (sbyte)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UBYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (byte)mArray.GetValue(i) / (byte)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_SHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int16)mArray.GetValue(i) / (Int16)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_USHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt16)mArray.GetValue(i) / (UInt16)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_INT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int32)mArray.GetValue(i) / (Int32)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UINT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt32)mArray.GetValue(i) / (UInt32)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_INT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int64)mArray.GetValue(i) / (Int64)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_UINT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt64)mArray.GetValue(i) / (UInt64)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_FLOAT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (float)mArray.GetValue(i) / (float)array.mArray.GetValue(i), i);
                    break; 
                case NcTypeEnum.NC_DOUBLE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (double)mArray.GetValue(i) / (double)array.mArray.GetValue(i), i);
                    break; 
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }

        public static NcArray Add(NcArray a, Object val) {
            NcArray result = a.Copy();
            result.Add(val);
            return result;
        }

        public static NcArray operator+(NcArray a, Object val) {
            return Add(a,val);
        }
        public static NcArray Sub(NcArray a, Object val) {
            NcArray result = a.Copy();
            result.Sub(val);
            return result;
        }

        public static NcArray operator-(NcArray a, Object val) {
            return Sub(a,val);
        }
        public static NcArray Mult(NcArray a, Object val) {
            NcArray result = a.Copy();
            result.Mult(val);
            return result;
        }

        public static NcArray operator*(NcArray a, Object val) {
            return Mult(a,val);
        }
        public static NcArray Div(NcArray a, Object val) {
            NcArray result = a.Copy();
            result.Div(val);
            return result;
        }

        public static NcArray operator/(NcArray a, Object val) {
            return Div(a,val);
        }

        public NcArray Add(Object val) {
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (sbyte) mArray.GetValue(i) + (sbyte) val, i);
                    break;
                case NcTypeEnum.NC_UBYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (byte) mArray.GetValue(i) + (byte) val, i);
                    break;
                case NcTypeEnum.NC_SHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int16) mArray.GetValue(i) + (Int16) val, i);
                    break;
                case NcTypeEnum.NC_USHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt16) mArray.GetValue(i) + (UInt16) val, i);
                    break;
                case NcTypeEnum.NC_INT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int32) mArray.GetValue(i) + (Int32) val, i);
                    break;
                case NcTypeEnum.NC_UINT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt32) mArray.GetValue(i) + (UInt32) val, i);
                    break;
                case NcTypeEnum.NC_INT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int64) mArray.GetValue(i) + (Int64) val, i);
                    break;
                case NcTypeEnum.NC_UINT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt64) mArray.GetValue(i) + (UInt64) val, i);
                    break;
                case NcTypeEnum.NC_FLOAT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (float) mArray.GetValue(i) + (float) val, i);
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (double) mArray.GetValue(i) + (double) val, i);
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        public NcArray Sub(Object val) {
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (sbyte) mArray.GetValue(i) - (sbyte) val, i);
                    break;
                case NcTypeEnum.NC_UBYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (byte) mArray.GetValue(i) - (byte) val, i);
                    break;
                case NcTypeEnum.NC_SHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int16) mArray.GetValue(i) - (Int16) val, i);
                    break;
                case NcTypeEnum.NC_USHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt16) mArray.GetValue(i) - (UInt16) val, i);
                    break;
                case NcTypeEnum.NC_INT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int32) mArray.GetValue(i) - (Int32) val, i);
                    break;
                case NcTypeEnum.NC_UINT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt32) mArray.GetValue(i) - (UInt32) val, i);
                    break;
                case NcTypeEnum.NC_INT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int64) mArray.GetValue(i) - (Int64) val, i);
                    break;
                case NcTypeEnum.NC_UINT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt64) mArray.GetValue(i) - (UInt64) val, i);
                    break;
                case NcTypeEnum.NC_FLOAT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (float) mArray.GetValue(i) - (float) val, i);
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (double) mArray.GetValue(i) - (double) val, i);
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        public NcArray Mult(Object val) {
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (sbyte) mArray.GetValue(i) * (sbyte) val, i);
                    break;
                case NcTypeEnum.NC_UBYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (byte) mArray.GetValue(i) * (byte) val, i);
                    break;
                case NcTypeEnum.NC_SHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int16) mArray.GetValue(i) * (Int16) val, i);
                    break;
                case NcTypeEnum.NC_USHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt16) mArray.GetValue(i) * (UInt16) val, i);
                    break;
                case NcTypeEnum.NC_INT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int32) mArray.GetValue(i) * (Int32) val, i);
                    break;
                case NcTypeEnum.NC_UINT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt32) mArray.GetValue(i) * (UInt32) val, i);
                    break;
                case NcTypeEnum.NC_INT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int64) mArray.GetValue(i) * (Int64) val, i);
                    break;
                case NcTypeEnum.NC_UINT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt64) mArray.GetValue(i) * (UInt64) val, i);
                    break;
                case NcTypeEnum.NC_FLOAT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (float) mArray.GetValue(i) * (float) val, i);
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (double) mArray.GetValue(i) * (double) val, i);
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        public NcArray Div(Object val) {
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (sbyte) mArray.GetValue(i) / (sbyte) val, i);
                    break;
                case NcTypeEnum.NC_UBYTE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (byte) mArray.GetValue(i) / (byte) val, i);
                    break;
                case NcTypeEnum.NC_SHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int16) mArray.GetValue(i) / (Int16) val, i);
                    break;
                case NcTypeEnum.NC_USHORT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt16) mArray.GetValue(i) / (UInt16) val, i);
                    break;
                case NcTypeEnum.NC_INT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int32) mArray.GetValue(i) / (Int32) val, i);
                    break;
                case NcTypeEnum.NC_UINT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt32) mArray.GetValue(i) / (UInt32) val, i);
                    break;
                case NcTypeEnum.NC_INT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (Int64) mArray.GetValue(i) / (Int64) val, i);
                    break;
                case NcTypeEnum.NC_UINT64:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (UInt64) mArray.GetValue(i) / (UInt64) val, i);
                    break;
                case NcTypeEnum.NC_FLOAT:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (float) mArray.GetValue(i) / (float) val, i);
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (double) mArray.GetValue(i) / (double) val, i);
                    break;
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
    }
}
