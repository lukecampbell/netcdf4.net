/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcType
 */

using System;
using System.Text;
using netcdf4;
using netcdf4.exceptions;

namespace netcdf4.test {
    public class TestNcVar : UnitTest {
        private string filePath = "nc_clobber.nc";

        public TestNcVar() {
            // Add tests
            AddTest(TestVarPutGet, "TestVarPutGet");
            AddTest(TestStrictChecking, "TestStrictChecking");
            AddTest(TestStringVar, "TestStringVar");
            AddTest(TestByteVar, "TestByteVar");
            AddTest(TestInt16Var, "TestInt16Var");
            AddTest(TestInt32Var, "TestInt32Var");
            AddTest(TestFloatVar, "TestFloatVar");
            AddTest(TestDoubleVar, "TestDoubleVar");
            AddTest(TestStringIndexVar, "TestStringIndexVar");
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

        private void FileSetup(ref NcFile file, ref NcDim dim1, ref NcVar var1, string type="int") {
            file = TestHelper.NewFile(filePath);
            dim1 = file.AddDim("time", 20);
            var1 = file.AddVar("temp",type,"time");
        }
        private void FileSetup(ref NcFile file, ref NcDim dim1, ref NcVar var1, NcType type) {
            file = TestHelper.NewFile(filePath);
            dim1 = file.AddDim("time", 20);
            var1 = file.AddVar("temp", type, dim1);
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
        
        
        public bool TestByteVar() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            byte[] buffer = new byte[20];
            byte[] readBuffer = new byte[20];
            for(int i=0;i<20;i++) buffer[i] = (byte)i;
            try {
                FileSetup(ref file, ref dim1, ref var1, NcShort.Instance);
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                var1.PutVar(new Int32[]{5}, new byte[]{30});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], (byte)30);
            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }
        
        public bool TestInt16Var() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            Int16[] buffer = new Int16[20];
            Int16[] readBuffer = new Int16[20];
            for(int i=0;i<20;i++) buffer[i] = (Int16)i;
            try {
                FileSetup(ref file, ref dim1, ref var1, NcShort.Instance);
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }

        public bool TestInt32Var() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            Int32[] buffer = new Int32[20];
            Int32[] readBuffer = new Int32[20];
            for(int i=0;i<20;i++) buffer[i] = i;
            try {
                FileSetup(ref file, ref dim1, ref var1, NcInt.Instance);
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool TestFloatVar() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            float[] buffer = new float[20];
            float[] readBuffer = new float[20];
            for(int i=0;i<20;i++) buffer[i] = (float)i;
            try {
                FileSetup(ref file, ref dim1, ref var1, NcFloat.Instance);
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }
        
        public bool TestDoubleVar() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            double[] buffer = new double[20];
            double[] readBuffer = new double[20];
            for(int i=0;i<20;i++) buffer[i] = (double)i;
            try {
                FileSetup(ref file, ref dim1, ref var1, NcDouble.Instance);
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }
        
        public bool TestStringVar() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            ASCIIEncoding encoder = new ASCIIEncoding();

            string buffer = String.Format("{0,20}", "hi there"); // need exactly 20 chars
            byte[] readBuffer = new byte[20];
            try {
                FileSetup(ref file, ref dim1, ref var1, NcChar.Instance);
                var1.PutVar(encoder.GetBytes(buffer));
                var1.GetVar(readBuffer);
                Assert.Equals(encoder.GetString(readBuffer), buffer);
            } finally {
                file.close();
            }
            CheckDelete(filePath);
            return true;
        }

    }
}

