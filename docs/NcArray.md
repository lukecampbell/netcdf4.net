# NcArray

#### Namespace 
`ASA.NetCDF4` 

### Class `NcArray`

#### Description
A class that wraps in-memory buffers and treats them as n-dimensional arrays. 
Also provides simple manipulation of the values using arithmetic operators.

#### Fields
```
 -isNull
 -mArray
 -ndim
 -shape
 -type
```

#### Methods
```
 +Add(NcArray array)
 +Add(NcArray a, Object val)
 +Add(NcArray a, NcArray b)
 +Add(Object val)
 +Arange(NcType type, int r1, int r2=0, int stride=1, int[] shape=null)
 +CheckIndex(int[] index)
 #CheckNull()
 #CheckShape(List<int> shape)
 #CheckShape(int[] shape)
 +CompatibleArray(NcArray array)
 +CompatibleShape(int[] shape)
 +CompatibleType(NcType type)
 +Copy(NcArray rhs)
 +Copy()
 +Div(NcArray array)
 +Div(Object val)
 +Div(NcArray a, NcArray b)
 +Div(NcArray a, Object val)
 +Equals(NcArray rhs)
 +Fill(Object val)
 +FillSlice(Object fillValue, int[] start=null, int[] stop=null, int[] stride=null)
 +GetByte(int[] index)
 +GetByteAt(params int[] index)
 +GetDouble(int[] index)
 +GetDoubleAt(params int[] index)
 +GetFloat(int[] index)
 +GetFloatAt(params int[] index)
 +GetInt16(int[] index)
 +GetInt16At(params int[] index)
 +GetInt32(int[] index)
 +GetInt32At(params int[] index)
 +GetInt64(int[] index)
 +GetInt64At(params int[] index)
 +GetNcType()
 +GetSbyte(int[] index)
 +GetSbyteAt(params int[] index)
 +GetUInt16(int[] index)
 +GetUInt16At(params int[] index)
 +GetUInt32(int[] index)
 +GetUInt32At(params int[] index)
 +GetUInt64(int[] index)
 +GetUInt64At(params int[] index)
 +GetValue(int[] index)
 +GetValueAt(params int[] index)
 +IsNull()
 +Mult(NcArray array)
 +Mult(NcArray a, NcArray b)
 +Mult(NcArray a, Object val)
 +Mult(Object val)
 +NcArray()
 +NcArray(Int16[] array, int[] shape=null)
 +NcArray(Int32[] array, List<Int32> shape=null)
 +NcArray(Int32[] array, int[] shape=null)
 +NcArray(Int64[] array, List<Int32> shape=null)
 +NcArray(Int64[] array, int[] shape=null)
 +NcArray(NcType type, List<Int32> shape)
 +NcArray(Array array, NcType type, int[] shape=null)
 +NcArray(UInt16[] array, List<Int32> shape=null)
 +NcArray(UInt16[] array, int[] shape=null)
 +NcArray(UInt32[] array, List<Int32> shape=null)
 +NcArray(UInt32[] array, int[] shape=null)
 +NcArray(Int16[] array, List<Int32> shape=null)
 +NcArray(UInt64[] array, int[] shape=null)
 +NcArray(byte[] array, List<Int32> shape=null)
 +NcArray(byte[] array, int[] shape=null)
 +NcArray(double[] array, List<Int32> shape=null)
 +NcArray(double[] array, int[] shape=null)
 +NcArray(float[] array, List<Int32> shape=null)
 +NcArray(float[] array, int[] shape=null)
 +NcArray(sbyte[] array, List<Int32> shape=null)
 +NcArray(sbyte[] array, int[] shape=null)
 +NcArray(Array array, NcType type, List<int> shape=null)
 +NcArray(UInt64[] array, List<Int32> shape=null)
 +NcArray(NcType type, int[] shape)
 +Reshape(int[] shape)
 +SetValue(Object val, int[] index)
 +SetValueAt(Object val, params int[] index)
 +Slice(int[] start=null, int[] stop=null, int[] stride=null)
 +Sub(NcArray array)
 +Sub(Object val)
 +Sub(NcArray a, NcArray b)
 +Sub(NcArray a, Object val)
 +ToString()
 +Value(int[] index)
 +ValueAt(params int[] index)
 -offset(int[] index)
 +operator *(NcArray a, NcArray b)
 +operator *(NcArray a, Object val)
 +operator +(NcArray a, NcArray b)
 +operator +(NcArray a, Object val)
 +operator -(NcArray a, NcArray b)
 +operator -(NcArray a, Object val)
 +operator /(NcArray a, NcArray b)
 +operator /(NcArray a, Object val)
 -rslice_offset(int[] start, int[] stop, int[] stride, int dim, List<int>index, List<int> results)
 -stringify(StringBuilder buf, int dim=0, List<int> index=null)
```

#### Properties
```
 +Array
 +Length
 +Shape
```

