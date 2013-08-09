/*
 * Author Luke Campbell <LCampbell@ASAScience.Com>
 * ASA.NetCDF4.NcArray
 */

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace ASA.NetCDF4 {
    
    // NcArray
    ///<summary>
    ///     A class that wraps in-memory buffers and treats them as n-dimensional arrays. 
    ///     Also provides simple manipulation of the values using arithmetic operators
    ///</summary>
    public class NcArray {
        private NcType type;
        private int ndim;
        private int[] shape;

        private Array mArray;

        public NcArray() {
            isNull = true;
        }

        //********************************************************************************
        // NcArray(NcType, int[])
        ///<summary>
        ///  Constructs an empty array of the specified type and shape
        ///</summary>
        ///<param name="type">The data type for the array</param>
        ///<param name="shape">The shape fo the array</param>
        //********************************************************************************
        public NcArray(NcType type, int[] shape) {
            int spaceRequired = 1;
            this.shape = shape;
            ndim = shape.Length;
            CheckShape(shape);
            foreach(int dimLen in shape) {
                spaceRequired *= dimLen;
            }
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    mArray = new %(t)s[spaceRequired];
                    break;

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            this.type = type;
            isNull = false;
        }
        
        //********************************************************************************
        // NcArray(NcType, List<Int32>)
        ///<summary>
        ///  Constructs an empty array of the specified type and shape
        ///</summary>
        ///<param name="type">The data type for the array</param>
        ///<param name="shape">The shape fo the array</param>
        //********************************************************************************
        public NcArray(NcType type, List<Int32> shape) {
            int spaceRequired = 1;
            this.shape = shape.ToArray();
            ndim = shape.Count;
            CheckShape(shape.ToArray());
            foreach(int dimLen in shape) {
                spaceRequired *= dimLen;
            }
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    mArray = new %(t)s[spaceRequired];
                    break;

/%%

                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            this.type = type;
            isNull = false;
        }

        //********************************************************************************
        // NcArray(Array, NcType, int[])
        ///<summary>
        ///  Generic Array wrapper constructor
        ///  This creates an NcArray that wraps the source array, all manipulations on 
        ///  this object will affect the source array instance.
        ///</summary>
        ///<param name="array">Source array</param>
        ///<param name="type">The data type for the array</param>
        ///<param name="shape">The shape fo the array</param>
        //********************************************************************************
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
        
        //********************************************************************************
        // NcArray(Array, NcType, List<int>)
        ///<summary>
        ///  Generic Array wrapper constructor
        ///  This creates an NcArray that wraps the source array, all manipulations on 
        ///  this object will affect the source array instance.
        ///</summary>
        ///<param name="array">Source array</param>
        ///<param name="type">The data type for the array</param>
        ///<param name="shape">The shape fo the array</param>
        //********************************************************************************
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
%%
        //********************************************************************************
        // NcArray(%(t)s[], List<int>)
        ///<summary>
        ///  %(t)s[] wrapper constructor
        ///  This creates an NcArray that wraps the source array, all manipulations on 
        ///  this object will affect the source array instance.
        ///</summary>
        ///<param name="array">Source array</param>
        ///<param name="shape">The shape fo the array</param>
        //********************************************************************************
        public NcArray(%(t)s[] array, List<Int32> shape=null) {
            type = %(i)s.Instance;
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

/%%

%%
        //********************************************************************************
        // NcArray(%(t)s[], int[])
        ///<summary>
        ///  %(t)s[] wrapper constructor
        ///  This creates an NcArray that wraps the source array, all manipulations on 
        ///  this object will affect the source array instance.
        ///</summary>
        ///<param name="array">Source array</param>
        ///<param name="shape">The data type for the array</param>
        //********************************************************************************
        public NcArray(%(t)s[] array, int[] shape=null) {
            type = %(i)s.Instance;
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

/%%

        //********************************************************************************
        // CheckIndex(int[])
        ///<summary>
        ///     Validates that the index input is valid and does not exceed any dimension.
        ///</summary>
        ///<param name="index">Index</param>
        //********************************************************************************
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

        //********************************************************************************
        // FillSlice(Object, int[], int[], int[])
        ///<summary>
        ///     Fills an array with the specified value
        ///</summary>
        ///<param name="fillValue">Fill Value</param>
        ///<param name="start">Start index</param>
        ///<param name="stop">Stop index</param>
        ///<param name="stride">Stride</param>
        ///<returns>The instance of the same array</returns>
        //********************************************************************************
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

        //********************************************************************************
        // Slice(int[], int[], int[])
        ///<summary>
        ///     Returns a copy of a slice of data in the array. This is a deep-copy
        ///     operation.
        ///</summary>
        ///<param name="start">Start Index</param>
        ///<param name="stop">Stop Index</param>
        ///<param name="stride">Stride</param>
        ///<returns>A deep-copy of a slice of the instance</returns>
        //********************************************************************************
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
%%
                case NcTypeEnum.%(c)s:
                    array = new %(t)s[totalSize];
                    break;

/%%
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


        //********************************************************************************
        // ValueAt<T>(param int[])
        ///<summary>
        ///     Retrieves a value at a specific index. Useful for known-types.  If the 
        ///     type doesn't match the casted type use Get(Typename)At methods.
        ///</summary>
        ///<param name="index">Index</param>
        ///<returns>Type-casted value at the specified index</returns>
        //********************************************************************************
        public T ValueAt<T>(params int[] index) {
            T result = (T) mArray.GetValue( offset(index) );
            return result;
        }

        //********************************************************************************
        // Value<T>(param int[])
        ///<summary>
        ///     Retrieves a value at a specific index. Useful for known-types.  If the 
        ///     type doesn't match the casted type use Get(Typename)At methods.
        ///</summary>
        ///<param name="index">Index</param>
        ///<returns>Type-casted value at the specified index</returns>
        //********************************************************************************
        public T Value<T>(int[] index) {
            T result = (T) mArray.GetValue( offset(index) );
            return result;
        }

        //********************************************************************************
        // SetValue(Object, index[])
        ///<summary>
        ///     Sets a value at the specified index.
        ///</summary>
        ///<param name="val"></param>
        ///<param name="index"></param>
        //********************************************************************************
        public void SetValue(Object val, int[] index) {
            mArray.SetValue(val, offset(index));
        }

        //********************************************************************************
        // GetValue(int[])
        ///<summary>
        ///     Retrieves a generic value at a sepcific index. (Not type-casted)
        ///</summary>
        ///<param name="index"></param>
        //********************************************************************************
        public Object GetValue(int[] index) {
            return mArray.GetValue(offset(index));
        }

        //********************************************************************************
        // SetValueAt(Object, params int[])
        ///<summary>
        ///     Sets a value at the specified index
        ///</summary>
        ///<param name="val">Value</param>
        ///<param name="index"></param>
        //********************************************************************************
        public void SetValueAt(Object val, params int[] index) {
            mArray.SetValue(val, offset(index));
        }

        //********************************************************************************
        // GetValueAt(params int[])
        ///<summary>
        ///     Retrieves a generic object value at the specified index.
        ///</summary>
        ///<param name="index"></param>
        //********************************************************************************
        public Object GetValueAt(params int[] index) {
            return mArray.GetValue(offset(index));
        }

%%
        //********************************************************************************
        // Get%(u)sAt(params int[])
        ///<summary>
        ///     Retrieves an %(t)s at the specified index.
        ///</summary>
        ///<param name="index"></param>
        //********************************************************************************
        public %(t)s Get%(u)sAt(params int[] index) {
            %(t)s retval;
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    retval = (%%(t)s) ((%(t)s) mArray.GetValue(offset(index)));
                    break;

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

/%%
%%
        //********************************************************************************
        // Get%(u)sAt(int[])
        ///<summary>
        ///     Retrieves an %(t)s at the specified index.
        ///</summary>
        ///<param name="index"></param>
        //********************************************************************************
        public %(t)s Get%(u)s(int[] index) {
            %(t)s retval;
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    retval = (%%(t)s) ((%(t)s) mArray.GetValue(offset(index)));
                    break;

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return retval;
        }

/%%

        //********************************************************************************
        // Array
        ///<returns>The in-memory buffer</returns>
        //********************************************************************************
        public Array Array {
            get {
                return mArray;
            }
        }

        //********************************************************************************
        // Equals(NcArray)
        ///<summary>
        ///     Compares an array
        ///</summary>
        ///<param name="rhs">Array to compare with</param>
        ///<returns>true if the arrays are equal</returns>
        //********************************************************************************
        public bool Equals(NcArray rhs) {
            if(!CompatibleArray(rhs))
                return false;
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s: {
                    for(int i=0;i<mArray.Length;i++) {
                        if((%(t)s) mArray.GetValue(i) != (%(t)s) rhs.mArray.GetValue(i)) {
                            return false;
                        }
                    }
                    break;
                }
/%%
                default:
                    throw new exceptions.NcBadType("NcArray unsupported type detected in comparison");
            }
            return true;
        }


        //********************************************************************************
        // Fill(Object)
        ///<summary>
        ///     Fills the entire array with the specified value
        ///</summary>
        ///<param name="val"></param>
        ///<returns>this</returns>
        //********************************************************************************
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

        //********************************************************************************
        // GetNcType()
        ///<returns>The NcType for this Array instance</returns>
        //********************************************************************************
        public NcType GetNcType() {
            return type;
        }

        //********************************************************************************
        // Shape
        ///<returns>The shape of this instance<returns>
        //********************************************************************************
        public int[] Shape {
            get { 
                return shape;
            }
        }

        //********************************************************************************
        // Length
        ///<summary>
        ///     In-memory buffer length
        ///</summary>
        ///<returns>In-memory buffer 
        //********************************************************************************
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
        
        //********************************************************************************
        // IsNull()
        ///<returns>True if the object is a null instance</returns>
        //********************************************************************************
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

        //********************************************************************************
        // Reshape(int[])
        ///<summary>
        ///     Reshapes the array instance
        ///</summary>
        ///<param name="shape"></param>
        ///<returns>this</returns>
        //********************************************************************************
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

        //********************************************************************************
        // Arange(NcType, int, int, int, int[])
        ///<summary>
        ///     Creates an NcArray with a preallocated buffer of elements that are set
        ///     based on the specified type, starting value, ending value and stride.
        ///</summary>
        ///<param name="type"></param>
        ///<param name="r1">Start Value</param>
        ///<param name="r2">Stop Value</param>
        ///<param name="r3">Stride Value</param>
        ///<param name="shape">Shape of the output</param>
        ///<returns>New instance of an NcArray</returns>
        //********************************************************************************
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
%%
                case NcTypeEnum.%(c)s:
                        array = new %(t)s[spaceRequired];
                        for(int i=0;i<array.Length;i++)
                            array.SetValue((%(t)s)i,i);
                        break; 

/%%
                default:
                        throw new exceptions.NcBadType("Unsupported NcType for NcArray");
            }

            return new NcArray(array, type, shape);
        }

        //********************************************************************************
        // CompatibleType(NcType)
        ///<returns>Returns true if the type is compatible</returns>
        //********************************************************************************
        public bool CompatibleType(NcType type) {
            if(!type.Equals(type)) {
                return false;
            }
            return true;
        }


        //********************************************************************************
        // CompatibleShape(int[])
        ///<returns>Returns true if the shape matches the instance shape</returns>
        //********************************************************************************
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

        //********************************************************************************
        // CompatibleArray(NcArray)
        ///<summary>
        ///     Determines compatibility with another array
        ///</summary>
        ///<returns>Returns true if the array is of similar type and shape</returns>
        //********************************************************************************
        public bool CompatibleArray(NcArray array) {
            if(!CompatibleType(array.type))
                return false;
            if(!CompatibleShape(array.shape))
                return false;
            return true;
        }

        //********************************************************************************
        // Copy()
        ///<summary>
        ///     Deep Copy
        ///</summary>
        ///<returns>Deep-copy array</returns>
        //********************************************************************************
        public NcArray Copy() {
            Array array=null;
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    array = new %(t)s[mArray.Length];
                    break; 

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            Array.Copy(mArray, array, mArray.Length);
            return new NcArray(array, type, shape);
        }
        
        //********************************************************************************
        // Copy(NcArray)
        ///<summary>
        ///     Deep Copy
        ///</summary>
        ///<param name="rhs">Source Array</param>
        ///<returns>Instance of copied array</param>
        //********************************************************************************
        public static NcArray Copy(NcArray rhs) {
            return rhs.Copy();
        }

        //********************************************************************************
        // Add(NcArray,NcArray)
        ///<summary>
        ///     Adds two arrays
        ///</summary>
        //********************************************************************************
        public static NcArray Add(NcArray a, NcArray b) {
            NcArray result = a.Copy();
            result.Add(b);
            return result;
        }

        public static NcArray operator+(NcArray a, NcArray b) {
            return Add(a,b);
        }

        //********************************************************************************
        // Sub(NcArray,NcArray)
        ///<summary>
        ///     Subtracts two arrays
        ///</summary>
        //********************************************************************************
        public static NcArray Sub(NcArray a, NcArray b) {
            NcArray result = a.Copy();
            result.Sub(b);
            return result;
        }

        public static NcArray operator-(NcArray a, NcArray b) {
            return Sub(a,b);
        }

        //********************************************************************************
        // Mult(NcArray,NcArray)
        ///<summary>
        ///     Multiplies two arrays
        ///</summary>
        //********************************************************************************
        public static NcArray Mult(NcArray a, NcArray b) {
            NcArray result = a.Copy();
            result.Mult(b);
            return result;
        }

        public static NcArray operator*(NcArray a, NcArray b) {
            return Mult(a,b);
        }

        //********************************************************************************
        // Div(NcArray,NcArray)
        ///<summary>
        ///     Divides two arrays
        ///</summary>
        //********************************************************************************
        public static NcArray Div(NcArray a, NcArray b) {
            NcArray result = a.Copy();
            result.Div(b);
            return result;
        }

        public static NcArray operator/(NcArray a, NcArray b) {
            return Div(a,b);
        }

        //********************************************************************************
        // Sub(NcArray)
        ///<summary>
        ///     In-place subtraction
        ///</summary>
        //********************************************************************************
        public NcArray Sub(NcArray array) {
            if(!CompatibleShape(array.shape)) {
                throw new exceptions.NcInvalidArg("Array shape mismatch");
            }
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (%(t)s)mArray.GetValue(i) - (%(t)s)array.mArray.GetValue(i), i);
                    break; 

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        
        //********************************************************************************
        // Add(NcArray)
        ///<summary>
        ///     In-place addition
        ///</summary>
        //********************************************************************************
        public NcArray Add(NcArray array) {
            if(!CompatibleShape(array.shape)) {
                throw new exceptions.NcInvalidArg("Array shape mismatch");
            }
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (%(t)s)mArray.GetValue(i) + (%(t)s)array.mArray.GetValue(i), i);
                    break; 

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        
        //********************************************************************************
        // Mult(NcArray)
        ///<summary>
        ///     In-place multiplication
        ///</summary>
        //********************************************************************************
        public NcArray Mult(NcArray array) {
            if(!CompatibleShape(array.shape)) {
                throw new exceptions.NcInvalidArg("Array shape mismatch");
            }
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (%(t)s)mArray.GetValue(i) * (%(t)s)array.mArray.GetValue(i), i);
                    break; 

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        
        //********************************************************************************
        // Div(NcArray)
        ///<summary>
        ///     In-place division
        ///</summary>
        //********************************************************************************
        public NcArray Div(NcArray array) {
            if(!CompatibleShape(array.shape)) {
                throw new exceptions.NcInvalidArg("Array shape mismatch");
            }
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (%(t)s)mArray.GetValue(i) / (%(t)s)array.mArray.GetValue(i), i);
                    break; 

/%%
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
%%
                case NcTypeEnum.%(c)s:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (%(t)s) mArray.GetValue(i) + (%(t)s) val, i);
                    break;

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        public NcArray Sub(Object val) {
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (%(t)s) mArray.GetValue(i) - (%(t)s) val, i);
                    break;

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        public NcArray Mult(Object val) {
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (%(t)s) mArray.GetValue(i) * (%(t)s) val, i);
                    break;

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
        public NcArray Div(Object val) {
            switch(type.GetTypeClass()) {
%%
                case NcTypeEnum.%(c)s:
                    for(int i=0;i<mArray.Length;i++)
                        mArray.SetValue( (%(t)s) mArray.GetValue(i) / (%(t)s) val, i);
                    break;

/%%
                default:
                    throw new exceptions.NcBadType("NcArray does not support type: " + type.GetTypeClassName());
            }
            return this;
        }
    }
}
