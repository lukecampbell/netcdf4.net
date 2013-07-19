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
                        break;
                case NcTypeEnum.NC_UBYTE:
                        array = new byte[spaceRequired];
                        break;
                case NcTypeEnum.NC_SHORT:
                        array = new Int16[spaceRequired];
                        break;
                case NcTypeEnum.NC_USHORT:
                        array = new UInt16[spaceRequired];
                        break;
                case NcTypeEnum.NC_INT:
                        array = new Int32[spaceRequired];
                        break;
                case NcTypeEnum.NC_UINT:
                        array = new UInt32[spaceRequired];
                        break;
                case NcTypeEnum.NC_INT64:
                        array = new Int64[spaceRequired];
                        break;
                case NcTypeEnum.NC_UINT64:
                        array = new UInt64[spaceRequired];
                        break;
                case NcTypeEnum.NC_FLOAT:
                        array = new float[spaceRequired];
                        break;
                case NcTypeEnum.NC_DOUBLE:
                        array = new double[spaceRequired];
                        break;
                default:
                        throw new exceptions.NcBadType("Unsupported NcType for NcArray");
            }
            for(int i=0;i<array.Length;i++)
                array.SetValue(i,i);

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
    }
}
