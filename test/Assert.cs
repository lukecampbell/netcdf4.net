/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf4.test.Assert
 */

using System;
namespace netcdf4.test {
    [Serializable()]
    public class AssertFailedException : System.Exception {
        public AssertFailedException() : base() {}
        public AssertFailedException(string message) : base(message) {}
        public AssertFailedException(string message, System.Exception inner): base(message, inner) {}
        protected AssertFailedException(System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) {}
    }
    public class Assert {
        public static void Equals(IComparable a, IComparable b, string message=null) {
            if(a == null)
                throw new AssertFailedException("a is null");
            if(b==null)
                throw new AssertFailedException("b is null");
            if(a.CompareTo(b) != 0)
                throw new AssertFailedException(String.IsNullOrEmpty(message) ? a.ToString() + " != " + b.ToString() : message);
        }
        public static void NotEquals(IComparable a, IComparable b, string message=null) {
            if(a == null)
                throw new AssertFailedException("a is null");
            if(b==null)
                throw new AssertFailedException("b is null");
            if(a.CompareTo(b) == 0)
                throw new AssertFailedException(String.IsNullOrEmpty(message) ? a.ToString() + " == " + b.ToString() : message);
        }
        public static void True(bool ret, string message=null) {
            if(!ret) 
                throw new AssertFailedException(String.IsNullOrEmpty(message) ? "value is not true" : message );
        }
        public static void False(bool ret, string message=null) {
            if(ret)
                throw new AssertFailedException(String.IsNullOrEmpty(message) ? "value is not false" : message);
        }
        public static void NotNull(Object ret, string message=null) {
            if(ret == null) 
                throw new AssertFailedException(String.IsNullOrEmpty(message) ? "value is null" : message);
        }
        public static void Null(Object ret, string message = null) {
            if(ret == null)
                throw new AssertFailedException(String.IsNullOrEmpty(message) ? "value is not null" : message);
        }
    }
}
