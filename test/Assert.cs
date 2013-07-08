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
        public static void Equals(IComparable a, IComparable b) {
            if(a == null)
                throw new AssertFailedException("a is null");
            if(b==null)
                throw new AssertFailedException("b is null");
            if(a.CompareTo(b) != 0)
                throw new AssertFailedException(a.ToString() + " != " + b.ToString());
        }
        public static void NotEquals(IComparable a, IComparable b) {
            if(a == null)
                throw new AssertFailedException("a is null");
            if(b==null)
                throw new AssertFailedException("b is null");
            if(a.CompareTo(b) == 0)
                throw new AssertFailedException(a.ToString() + " == " + b.ToString());
        }
        public static void True(bool ret) {
            if(!ret) 
                throw new AssertFailedException("value is not true");
        }
        public static void False(bool ret) {
            if(ret)
                throw new AssertFailedException("value is not false");
        }
        public static void NotNull(Object ret) {
            if(ret == null) 
                throw new AssertFailedException("value is null");
        }
        public static void Null(Object ret) {
            if(ret == null)
                throw new AssertFailedException("value is not null");
        }
    }
}
