/*
 * Author Luke Campbell <LCampbell@ASAScience.Com>
 * ASA.NetCDF4.NcArray
 */

using System;
using System.Collections.Generic;

namespace ASA.NetCDF4 {
    public class NcArray {
        private sbyte[] sbyteArray;
        private byte[] byteArray;
        private Int16[] int16Array;
        private UInt16[] uint16Array;
        private Int32[] int32Array;
        private UInt32[] uint32Array;
        private Int64[] int64Array;
        private UInt64[] uint64Array;
        private float[] floatArray;
        private double[] doubleArray;
        private NcType type;
        private int ndim;
        private int[] shape;

        private Array selected;

        public NcArray() {
            isNull = true;
        }

        public NcArray(NcType type, List<Int32> shape) {
            int spaceRequired = 1;
            this.shape = shape.ToArray();
            foreach(int dimLen in shape) {
                spaceRequired *= dimLen;
            }
            switch(type.GetTypeClass()) {
                case NcTypeEnum.NC_BYTE:
                    sbyteArray = new sbyte[spaceRequired];
                    selected = sbyteArray;
                    break;
                case NcTypeEnum.NC_UBYTE:
                    byteArray = new byte[spaceRequired];
                    selected = byteArray;
                    break;
                case NcTypeEnum.NC_SHORT:
                    int16Array = new Int16[spaceRequired];
                    selected = int16Array;
                    break;
                case NcTypeEnum.NC_USHORT:
                    uint16Array = new UInt16[spaceRequired];
                    selected = uint16Array;
                    break;
                case NcTypeEnum.NC_INT:
                    int32Array = new Int32[spaceRequired];
                    selected = int32Array;
                    break;
                case NcTypeEnum.NC_UINT:
                    uint32Array = new UInt32[spaceRequired];
                    selected = uint32Array;
                    break;
                case NcTypeEnum.NC_INT64:
                    int64Array = new Int64[spaceRequired];
                    selected = int64Array;
                    break;
                case NcTypeEnum.NC_UINT64:
                    uint64Array = new UInt64[spaceRequired];
                    selected = uint64Array;
                    break;
                case NcTypeEnum.NC_FLOAT:
                    floatArray = new float[spaceRequired];
                    selected = floatArray;
                    break;
                case NcTypeEnum.NC_DOUBLE:
                    doubleArray = new double[spaceRequired];
                    selected = doubleArray;
                    break;

                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            this.type = type;
            isNull = false;
        }


        public NcArray(sbyte[] array, List<Int32> shape=null) {
            type = NcByte.Instance;
            sbyteArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = sbyteArray;
            isNull = false;
        }
        public NcArray(sbyte[] array, int[] shape=null) {
            type = NcByte.Instance;
            sbyteArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = sbyteArray;
            isNull = false;
        }

        public NcArray(byte[] array, List<Int32> shape=null) {
            type = NcUbyte.Instance;
            byteArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = byteArray;
            isNull = false;
        }
        public NcArray(byte[] array, int[] shape=null) {
            type = NcUbyte.Instance;
            byteArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = byteArray;
            isNull = false;
        }

        public NcArray(Int16[] array, List<Int32> shape=null) {
            type = NcShort.Instance;
            int16Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = int16Array;
            isNull = false;
        }
        public NcArray(Int16[] array, int[] shape=null) {
            type = NcShort.Instance;
            int16Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = int16Array;
            isNull = false;
        }

        public NcArray(UInt16[] array, List<Int32> shape=null) {
            type = NcUshort.Instance;
            uint16Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = uint16Array;
            isNull = false;
        }
        public NcArray(UInt16[] array, int[] shape=null) {
            type = NcUshort.Instance;
            uint16Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = uint16Array;
            isNull = false;
        }

        public NcArray(Int32[] array, List<Int32> shape=null) {
            type = NcInt.Instance;
            int32Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = int32Array;
            isNull = false;
        }
        public NcArray(Int32[] array, int[] shape=null) {
            type = NcInt.Instance;
            int32Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = int32Array;
            isNull = false;
        }

        public NcArray(UInt32[] array, List<Int32> shape=null) {
            type = NcUint.Instance;
            uint32Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = uint32Array;
            isNull = false;
        }
        public NcArray(UInt32[] array, int[] shape=null) {
            type = NcUint.Instance;
            uint32Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = uint32Array;
            isNull = false;
        }

        public NcArray(Int64[] array, List<Int32> shape=null) {
            type = NcInt64.Instance;
            int64Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = int64Array;
            isNull = false;
        }
        public NcArray(Int64[] array, int[] shape=null) {
            type = NcInt64.Instance;
            int64Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = int64Array;
            isNull = false;
        }

        public NcArray(UInt64[] array, List<Int32> shape=null) {
            type = NcUint64.Instance;
            uint64Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = uint64Array;
            isNull = false;
        }
        public NcArray(UInt64[] array, int[] shape=null) {
            type = NcUint64.Instance;
            uint64Array = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = uint64Array;
            isNull = false;
        }

        public NcArray(float[] array, List<Int32> shape=null) {
            type = NcFloat.Instance;
            floatArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = floatArray;
            isNull = false;
        }
        public NcArray(float[] array, int[] shape=null) {
            type = NcFloat.Instance;
            floatArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = floatArray;
            isNull = false;
        }

        public NcArray(double[] array, List<Int32> shape=null) {
            type = NcDouble.Instance;
            doubleArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Count;
                this.shape = shape.ToArray();
            }
            selected = doubleArray;
            isNull = false;
        }
        public NcArray(double[] array, int[] shape=null) {
            type = NcDouble.Instance;
            doubleArray = array;
            if(shape == null) {
                ndim = 1;
                this.shape = new int[] { array.Length };
            } else {
                ndim = shape.Length;
                this.shape = shape;
            }
            selected = doubleArray;
            isNull = false;
        }


        private int offset(int[] index) {
            int a = 1;
            int offset = 0;

            for(int i=0;i<ndim;i++) {
                a=1;
                for(int j=i;j<(ndim-1);j++) a *= shape[j];
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




        public void Slice(out sbyte[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            array = new sbyte[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = sbyteArray[offset];
            }

        }
        

        public void Slice(out byte[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            array = new byte[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = byteArray[offset];
            }

        }
        

        public void Slice(out Int16[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            array = new Int16[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = int16Array[offset];
            }

        }
        

        public void Slice(out UInt16[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            array = new UInt16[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = uint16Array[offset];
            }

        }
        

        public void Slice(out Int32[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            array = new Int32[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = int32Array[offset];
            }

        }
        

        public void Slice(out UInt32[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            array = new UInt32[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = uint32Array[offset];
            }

        }
        

        public void Slice(out Int64[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            array = new Int64[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = int64Array[offset];
            }

        }
        

        public void Slice(out UInt64[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            array = new UInt64[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = uint64Array[offset];
            }

        }
        

        public void Slice(out float[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            
            array = new float[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = floatArray[offset];
            }

        }
        

        public void Slice(out double[] array, int[] start=null, int[] stop=null, int[] stride=null) {
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
                if(start[i] > stop[i])
                    throw new exceptions.NcInvalidArg("The stop argument must be greater than the start argument");
            }



            int[] dimLens = new int[ndim];
            int totalSize = 1;
            for(int i=0;i<ndim;i++) {
                dimLens[i] = (int) Math.Ceiling((double)(stop[i] - start[i]) / (double)stride[i]);
                totalSize *= dimLens[i];

            }

            array = new double[totalSize];

            int arrayOffset=0;
            List<int> index = new List<int>();
            List<int> offsets = new List<int>();
            rslice_offset(start, stop, stride, 0, index, offsets);
            foreach(int offset in offsets) {
                array[arrayOffset++] = doubleArray[offset];
            }

        }
        

        public void At(out sbyte result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = sbyteArray[offset(index)];
        }

        public void At(out byte result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = byteArray[offset(index)];
        }

        public void At(out Int16 result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = int16Array[offset(index)];
        }

        public void At(out UInt16 result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = uint16Array[offset(index)];
        }

        public void At(out Int32 result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = int32Array[offset(index)];
        }

        public void At(out UInt32 result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = uint32Array[offset(index)];
        }

        public void At(out Int64 result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = int64Array[offset(index)];
        }

        public void At(out UInt64 result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = uint64Array[offset(index)];
        }

        public void At(out float result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = floatArray[offset(index)];
        }

        public void At(out double result, int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = doubleArray[offset(index)];
        }

        public void ValueAt(out sbyte result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = sbyteArray[offset(index)];
        }

        public void ValueAt(out byte result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = byteArray[offset(index)];
        }

        public void ValueAt(out Int16 result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = int16Array[offset(index)];
        }

        public void ValueAt(out UInt16 result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = uint16Array[offset(index)];
        }

        public void ValueAt(out Int32 result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = int32Array[offset(index)];
        }

        public void ValueAt(out UInt32 result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = uint32Array[offset(index)];
        }

        public void ValueAt(out Int64 result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = int64Array[offset(index)];
        }

        public void ValueAt(out UInt64 result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = uint64Array[offset(index)];
        }

        public void ValueAt(out float result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = floatArray[offset(index)];
        }

        public void ValueAt(out double result, params int[] index) {
            if(index.Length != ndim) 
                throw new exceptions.NcInvalidArg("The index must contain the same number of elements as the number of dimensions.");

            result = doubleArray[offset(index)];
        }
        
        
        public Array Array {
            get {
                switch(type.GetTypeClass()) {
                    case NcTypeEnum.NC_BYTE:
                        return sbyteArray;

                    case NcTypeEnum.NC_UBYTE:
                        return byteArray;

                    case NcTypeEnum.NC_SHORT:
                        return int16Array;

                    case NcTypeEnum.NC_USHORT:
                        return uint16Array;

                    case NcTypeEnum.NC_INT:
                        return int32Array;

                    case NcTypeEnum.NC_UINT:
                        return uint32Array;

                    case NcTypeEnum.NC_INT64:
                        return int64Array;

                    case NcTypeEnum.NC_UINT64:
                        return uint64Array;

                    case NcTypeEnum.NC_FLOAT:
                        return floatArray;

                    case NcTypeEnum.NC_DOUBLE:
                        return doubleArray;
                }
                return null;
            }
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
                for(int i=0;i<ndim;i++)
                    l *= shape[i];
                return l;
            }
        }

        private bool isNull;
        
        public bool IsNull() {
            return isNull;
        }

    }
}
