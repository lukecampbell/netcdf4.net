/*
 * Author Luke Campbell <LCampbell@asascience.com>
 * com.asascience.netcdf4.NcVlenStruct
 */
using System;
using System.Text;
using System.Runtime.InteropServices;
namespace netcdf4 {
    [StructLayout(LayoutKind.Sequential)]
    public struct VlenStruct {
        public Int32 len;
        public IntPtr p; // Data 
    }
    public class Vlen {
        private GCHandle handle;
        private VlenStruct vlen_t;

        public Vlen() {
            isNull = true;
        }

        public Vlen(Array t) {
            isNull = false;
            handle = GCHandle.Alloc(t, GCHandleType.Pinned);
            vlen_t.len = t.Length;
            vlen_t.p = Marshal.UnsafeAddrOfPinnedArrayElement(t, 0);
        }
        ~Vlen() {
            if(!isNull) {
                handle.Free();
            }
        }

        public VlenStruct ToStruct() {
            VlenStruct retval = new VlenStruct();
            retval.len = vlen_t.len;
            retval.p = vlen_t.p;
            return retval;
        }

        private bool isNull;
    }
}

