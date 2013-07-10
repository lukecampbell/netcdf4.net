/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcType
 */

using System;
using netcdf4;
using netcdf4.exceptions;

namespace netcdf4.test {
    public class TestNcVar : UnitTest {
        private string filePath = "nc_clobber.nc";

        public TestNcVar() {
            // Add tests
            AddTest(TestVarPutGet, "TestVarPutGet");
            AddTest(TestStrictChecking, "TestStrictChecking");
        }

        public bool TestVarPutGet() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            Int32[] vals = new Int32[20];
            Int32[] recvVals = new Int32[20];
            for(int i=0;i<20;i++) 
                vals[i] = i;
            try {
                FileSetup(ref file, ref dim1, ref var1);
                var1.PutVar(vals);
            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }

        private void FileSetup(ref NcFile file, ref NcDim dim1, ref NcVar var1) {
            file = TestHelper.NewFile(filePath);
            dim1 = file.AddDim("time", 20);
            var1 = file.AddVar("temp","int","time");
        }

        public bool TestStrictChecking() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            Int32[] buffer = new Int32[20];
            for(int i=0;i<20;i++) buffer[i] = i;
            try {
                FileSetup(ref file, ref dim1, ref var1);
                var1.PutVar(buffer);
                buffer = new Int32[15]; // Squeeze it
                try {
                    var1.GetVar(buffer);
                    // If it gets to here the program will crash hard anyway
                    throw new AssertFailedException("BufferOverflow exception not raised");
                } catch(NcBufferOverflow e) {
                    // yay it worked
                }


            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }
    }
}

