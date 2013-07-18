/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcType
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ASA.NetCDF4;
using ASA.NetCDF4.exceptions;

namespace ASA.NetCDF4.test {
    public class TestNcVar : UnitTest {
        private string filePath = "nc_clobber.nc";

        public TestNcVar() {
            // Add tests
            AddTest(TestVarPutGet, "TestVarPutGet");
            AddTest(TestStrictChecking, "TestStrictChecking");
            AddTest(TestStringVar, "TestStringVar");
            AddTest(TestSByteVar, "TestSByteVar");
            AddTest(TestByteVar, "TestByteVar");
            AddTest(TestInt16Var, "TestInt16Var");
            AddTest(TestUInt16Var, "TestUInt16Var");
            AddTest(TestInt32Var, "TestInt32Var");
            AddTest(TestUInt32Var, "TestInt32Var");
            AddTest(TestInt64Var, "TestInt64Var");
            AddTest(TestUInt64Var, "TestInt64Var");
            AddTest(TestFloatVar, "TestFloatVar");
            AddTest(TestDoubleVar, "TestDoubleVar");
            AddTest(TestExtensive, "TestExtensive");
            AddTest(TestScalar, "TestScalar");
        }

        public bool TestVarPutGet() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            Int32[] vals = new Int32[20];
            for(int i=0;i<20;i++) 
                vals[i] = i;
            try {
                FileSetup(ref file, ref dim1, ref var1);
                var1.PutVar(vals);
            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }

        private void FileSetup(ref NcFile file, ref NcDim dim1, ref NcVar var1, string type="int") {
            file = TestHelper.NewFile(filePath);
            dim1 = file.AddDim("time", 20);
            var1 = file.AddVar("temp",type,"time");
        }
        private void FileSetup(ref NcFile file, ref NcDim dim1, ref NcVar var1, NcType type, int len=20) {
            file = TestHelper.NewFile(filePath);
            dim1 = file.AddDim("time", len);
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
                } catch(NcBufferOverflow) {
                    // yay it worked
                }


            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }

        public bool TestSByteVar() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            sbyte[] buffer = new sbyte[4];
            sbyte[] readBuffer = new sbyte[4];
            for(int i=0;i<4;i++) buffer[i] = (sbyte)(i-1);
            try {
                FileSetup(ref file, ref dim1, ref var1, NcByte.Instance, 4);
                // Test the array get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<4;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);

                // Test get and put for scalars
                var1.PutVar(new Int32[] { 1 }, new sbyte[] { -2 });
                var1.GetVar(new Int32[] { 1 }, readBuffer);
                Assert.Equals(readBuffer[0], (sbyte)-2);

                // Test get and put subsets
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 2 }, new sbyte[] { -2, -2 });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 2 }, readBuffer);
                Assert.Equals(readBuffer[0], (sbyte)-2);
                Assert.Equals(readBuffer[1], (sbyte)-2);
                // Test ranges
                var1.PutVar(new Int32[] { 0 }, new int[] { 127 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new int[] { 128 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
                var1.PutVar(new Int32[] { 0 }, new int[] { -128 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new int[] { -129 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }

            } finally {
                file.Close();
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
                FileSetup(ref file, ref dim1, ref var1, NcByte.Instance);
                // Test the basic get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                // test get and put scalars
                var1.PutVar(new Int32[]{5}, new byte[]{30});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], (byte)30);

                // test get and put 1d arrays
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 4 }, new byte[] { 20, 20, 20, 20 });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 4 }, readBuffer);
                Assert.Equals(readBuffer[0], (byte)20);
                Assert.Equals(readBuffer[3], (byte)20);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[0], (byte)0);
                Assert.Equals(readBuffer[2], (byte)20);
                Assert.Equals(readBuffer[5], (byte)20);
                Assert.Equals(readBuffer[6], (byte)6);

                // test striding
                var1.PutVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, new byte[] { 40, 40, 40, 40});
                var1.GetVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, readBuffer);
                Assert.Equals(readBuffer[0], (byte)40);
                Assert.Equals(readBuffer[2], (byte)40);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[10], (byte)40);
                Assert.Equals(readBuffer[12], (byte)40);
                Assert.Equals(readBuffer[14], (byte)40);
                Assert.Equals(readBuffer[15], (byte)15);
                // Test ranges
                // Note: NC_BYTE is a SIGNED 8-bit integer, no getting around that.
                var1.PutVar(new Int32[] { 0 }, new int[] { 127 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new int[] { 128 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
                var1.PutVar(new Int32[] { 0 }, new int[] { -128 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new int[] { -129 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }

            } finally {
                file.Close();
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
            for(int i=0;i<20;i++) buffer[i] = (Int16)(i-10);
            try {
                FileSetup(ref file, ref dim1, ref var1, NcShort.Instance);
                // Test the basic get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                // test get and put scalars
                var1.PutVar(new Int32[]{5}, new Int16[]{-30});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], (Int16)(-30));
                // test get and put 1d arrays
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 4 }, new Int16[] { -20, -20, -20, -20 });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 4 }, readBuffer);
                Assert.Equals(readBuffer[0], (Int16)(-20));
                Assert.Equals(readBuffer[3], (Int16)(-20));
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[0], (Int16)(-10));
                Assert.Equals(readBuffer[2], (Int16)(-20));
                Assert.Equals(readBuffer[5], (Int16)(-20));
                Assert.Equals(readBuffer[6], (Int16)(-4));
                
                // test striding
                var1.PutVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, new Int16[] { -40, -40, -40});
                var1.GetVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, readBuffer);
                Assert.Equals(readBuffer[0], (Int16)(-40));
                Assert.Equals(readBuffer[2], (Int16)(-40));
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[10], (Int16)(-40));
                Assert.Equals(readBuffer[12], (Int16)(-40));
                Assert.Equals(readBuffer[14], (Int16)(-40));
                Assert.Equals(readBuffer[15], (Int16)5);
                // Test ranges
                var1.PutVar(new Int32[] { 0 }, new int[] { 32767 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new int[] { 32768 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
                var1.PutVar(new Int32[] { 0 }, new int[] { -32768 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new int[] { -32769 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool TestUInt16Var() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            UInt16[] buffer = new UInt16[20];
            UInt16[] readBuffer = new UInt16[20];
            for(int i=0;i<20;i++) buffer[i] = (UInt16)i;
            try {
                FileSetup(ref file, ref dim1, ref var1, NcUshort.Instance);
                // Test the basic get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                // test get and put scalars
                var1.PutVar(new Int32[]{5}, new UInt16[]{30});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], (UInt16)30);
                // test get and put 1d arrays
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 4 }, new UInt16[] { 20, 20, 20, 20 });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 4 }, readBuffer);
                Assert.Equals(readBuffer[0], (UInt16)20);
                Assert.Equals(readBuffer[3], (UInt16)20);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[0], (UInt16)0);
                Assert.Equals(readBuffer[2], (UInt16)20);
                Assert.Equals(readBuffer[5], (UInt16)20);
                Assert.Equals(readBuffer[6], (UInt16)6);
                
                // test striding
                var1.PutVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, new UInt16[] { 40, 40, 40});
                var1.GetVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, readBuffer);
                Assert.Equals(readBuffer[0], (UInt16)40);
                Assert.Equals(readBuffer[2], (UInt16)40);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[10], (UInt16)40);
                Assert.Equals(readBuffer[12], (UInt16)40);
                Assert.Equals(readBuffer[14], (UInt16)40);
                Assert.Equals(readBuffer[15], (UInt16)15);
                // Test ranges
                var1.PutVar(new Int32[] { 0 }, new int[] { 65535 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new int[] { 65536 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
                try {
                    var1.PutVar(new Int32[] { 0 }, new int[] { -1 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
            } finally {
                file.Close();
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
            for(int i=0;i<20;i++) buffer[i] = (Int32)(i-10);
            try {
                FileSetup(ref file, ref dim1, ref var1, NcInt.Instance);
                // Test the basic get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                // test get and put scalars
                var1.PutVar(new Int32[]{5}, new Int32[]{-30});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], (Int32)(-30));
                // test get and put 1d arrays
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 4 }, new Int32[] { -20, -20, -20, -20 });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 4 }, readBuffer);
                Assert.Equals(readBuffer[0], (Int32)(-20));
                Assert.Equals(readBuffer[3], (Int32)(-20));
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[0], (Int32)(-10));
                Assert.Equals(readBuffer[2], (Int32)(-20));
                Assert.Equals(readBuffer[5], (Int32)(-20));
                Assert.Equals(readBuffer[6], (Int32)(-4));
                
                // test striding
                var1.PutVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, new Int32[] { -40, -40, -40});
                var1.GetVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, readBuffer);
                Assert.Equals(readBuffer[0], (Int32)(-40));
                Assert.Equals(readBuffer[2], (Int32)(-40));
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[10], (Int32)(-40));
                Assert.Equals(readBuffer[12], (Int32)(-40));
                Assert.Equals(readBuffer[14], (Int32)(-40));
                Assert.Equals(readBuffer[15], (Int32)5);
                // Test ranges
                var1.PutVar(new Int32[] { 0 }, new long[] { 2147483647 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new long[] { 2147483648 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
                var1.PutVar(new Int32[] { 0 }, new long[] { -2147483648 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new long[] { -2147483649 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool TestUInt32Var() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            UInt32[] buffer = new UInt32[20];
            UInt32[] readBuffer = new UInt32[20];
            for(int i=0;i<20;i++) buffer[i] = (UInt32)i;
            try {
                FileSetup(ref file, ref dim1, ref var1, NcUint.Instance);
                // Test the basic get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                // test get and put scalars
                var1.PutVar(new Int32[]{5}, new UInt32[]{30});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], (UInt32)30);
                // test get and put 1d arrays
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 4 }, new UInt32[] { 20, 20, 20, 20 });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 4 }, readBuffer);
                Assert.Equals(readBuffer[0], (UInt32)20);
                Assert.Equals(readBuffer[3], (UInt32)20);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[0], (UInt32)0);
                Assert.Equals(readBuffer[2], (UInt32)20);
                Assert.Equals(readBuffer[5], (UInt32)20);
                Assert.Equals(readBuffer[6], (UInt32)6);
                
                // test striding
                var1.PutVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, new UInt32[] { 40, 40, 40});
                var1.GetVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, readBuffer);
                Assert.Equals(readBuffer[0], (UInt32)40);
                Assert.Equals(readBuffer[2], (UInt32)40);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[10], (UInt32)40);
                Assert.Equals(readBuffer[12], (UInt32)40);
                Assert.Equals(readBuffer[14], (UInt32)40);
                Assert.Equals(readBuffer[15], (UInt32)15);
                // Test ranges
                var1.PutVar(new Int32[] { 0 }, new ulong[] { 4294967295 });
                try {
                    var1.PutVar(new Int32[] { 0 }, new ulong[] { 4294967296 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
                try {
                    var1.PutVar(new Int32[] { 0 }, new long[] { -1 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool TestInt64Var() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            Int64[] buffer = new Int64[20];
            Int64[] readBuffer = new Int64[20];
            for(int i=0;i<20;i++) buffer[i] = (Int64)(i-10);
            try {
                FileSetup(ref file, ref dim1, ref var1, NcInt64.Instance);
                // Test the basic get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                // test get and put scalars
                var1.PutVar(new Int32[]{5}, new Int64[]{-30});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], (Int64)(-30));
                // test get and put 1d arrays
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 4 }, new Int64[] { -20, -20, -20, -20 });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 4 }, readBuffer);
                Assert.Equals(readBuffer[0], (Int64)(-20));
                Assert.Equals(readBuffer[3], (Int64)(-20));
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[0], (Int64)(-10));
                Assert.Equals(readBuffer[2], (Int64)(-20));
                Assert.Equals(readBuffer[5], (Int64)(-20));
                Assert.Equals(readBuffer[6], (Int64)(-4));
                
                // test striding
                var1.PutVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, new Int64[] { -40, -40, -40});
                var1.GetVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, readBuffer);
                Assert.Equals(readBuffer[0], (Int64)(-40));
                Assert.Equals(readBuffer[2], (Int64)(-40));
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[10], (Int64)(-40));
                Assert.Equals(readBuffer[12], (Int64)(-40));
                Assert.Equals(readBuffer[14], (Int64)(-40));
                Assert.Equals(readBuffer[15], (Int64)5);
                // Test ranges
                var1.PutVar(new Int32[] { 0 }, new ulong[] { 9223372036854775807L });
                try {
                    var1.PutVar(new Int32[] { 0 }, new ulong[] { 9223372036854775808L });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
                var1.PutVar(new Int32[] { 0 }, new long[] { -9223372036854775808L });
                // Can't actually show the negative... how would you represent the overflow?
            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool TestUInt64Var() {
            NcFile file = null;
            NcDim dim1  = null;
            NcVar var1  = null;
            UInt64[] buffer = new UInt64[20];
            UInt64[] readBuffer = new UInt64[20];
            for(int i=0;i<20;i++) buffer[i] = (UInt64)i;
            try {
                FileSetup(ref file, ref dim1, ref var1, NcUint64.Instance);
                // Test the basic get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                // test get and put scalars
                var1.PutVar(new Int32[]{5}, new UInt64[]{30});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], (UInt64)30);
                // test get and put 1d arrays
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 4 }, new UInt64[] { 20, 20, 20, 20 });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 4 }, readBuffer);
                Assert.Equals(readBuffer[0], (UInt64)20);
                Assert.Equals(readBuffer[3], (UInt64)20);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[0], (UInt64)0);
                Assert.Equals(readBuffer[2], (UInt64)20);
                Assert.Equals(readBuffer[5], (UInt64)20);
                Assert.Equals(readBuffer[6], (UInt64)6);
                
                // test striding
                var1.PutVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, new UInt64[] { 40, 40, 40});
                var1.GetVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, readBuffer);
                Assert.Equals(readBuffer[0], (UInt64)40);
                Assert.Equals(readBuffer[2], (UInt64)40);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[10], (UInt64)40);
                Assert.Equals(readBuffer[12], (UInt64)40);
                Assert.Equals(readBuffer[14], (UInt64)40);
                Assert.Equals(readBuffer[15], (UInt64)15);
                // Test ranges
                var1.PutVar(new Int32[] { 0 }, new ulong[] { 18446744073709551615L });
                try {
                    var1.PutVar(new Int32[] { 0 }, new long[] { -1 });
                    throw new AssertFailedException("Failed to raise NcRange exception.");
                } catch (NcRange) {
                }
            } finally {
                file.Close();
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
                // Test the basic get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                // test get and put scalars
                var1.PutVar(new Int32[]{5}, new float[]{30.0f});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], 30.0f);
                // test get and put 1d arrays
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 4 }, new float[] { 20f, 20f, 20f, 20f });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 4 }, readBuffer);
                Assert.Equals(readBuffer[0], (float)20f);
                Assert.Equals(readBuffer[3], (float)20f);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[0], (float)0f);
                Assert.Equals(readBuffer[2], (float)20f);
                Assert.Equals(readBuffer[5], (float)20f);
                Assert.Equals(readBuffer[6], (float)6f);
                // test striding
                var1.PutVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, new float[] { 40, 40, 40});
                var1.GetVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, readBuffer);
                Assert.Equals(readBuffer[0], (float)40);
                Assert.Equals(readBuffer[2], (float)40);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[10], (float)40);
                Assert.Equals(readBuffer[12], (float)40);
                Assert.Equals(readBuffer[14], (float)40);
                Assert.Equals(readBuffer[15], (float)15);
            } finally {
                file.Close();
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
                // Test the basic get/put
                var1.PutVar(buffer);
                var1.GetVar(readBuffer);
                for(int i=0;i<20;i++)
                    Assert.Equals(readBuffer[i], buffer[i]);
                // test get and put scalars
                var1.PutVar(new Int32[]{5}, new double[]{30.0});
                var1.GetVar(new Int32[]{5}, readBuffer);
                Assert.Equals(readBuffer[0], 30.0);
                // test get and put 1d arrays
                var1.PutVar(new Int32[] { 2 }, new Int32[] { 4 }, new double[] { 20.0, 20.0, 20.0, 20.0 });
                var1.GetVar(new Int32[] { 2 }, new Int32[] { 4 }, readBuffer);
                Assert.Equals(readBuffer[0], (double)20.0);
                Assert.Equals(readBuffer[3], (double)20.0);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[0], (double)0.0);
                Assert.Equals(readBuffer[2], (double)20.0);
                Assert.Equals(readBuffer[5], (double)20.0);
                Assert.Equals(readBuffer[6], (double)6.0);
                // test striding
                var1.PutVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, new double[] { 40, 40, 40});
                var1.GetVar(new Int32[] { 10 }, new Int32[] { 5 }, new Int32[] {2}, readBuffer);
                Assert.Equals(readBuffer[0], (double)40);
                Assert.Equals(readBuffer[2], (double)40);
                var1.GetVar(readBuffer);
                Assert.Equals(readBuffer[10], (double)40);
                Assert.Equals(readBuffer[12], (double)40);
                Assert.Equals(readBuffer[14], (double)40);
                Assert.Equals(readBuffer[15], (double)15);
            } finally {
                file.Close();
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
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }

        public bool TestExtensive() {
            NcFile file = null;
            NcGroup east = null;
            NcGroup west = null;
            NcDim timeDim = null;
            NcDim latDim = null;
            NcDim lonDim = null;
            NcVar timeVar = null;
            NcVar latVar = null;
            NcVar lonVar = null;
            NcVar temp = null;
            float[] buffer;

            double[] timeArray = new double[] { 0, 1, 2, 3, 4, 5, 6 };
            double[] latArray  = new double[] { 40.1, 40.2, 40.3, 40.4, 40.5 };
            double[] lonArray  = new double[] { -73.5, -73.4, -73.3, -73.2 };
            float[] sstArray   = new float[140];

            double[] readBuffer = new double[140];
            for(int i=0;i<140;i++)
                sstArray[i] = (float) i;

            try {
                file = TestHelper.NewFile(filePath);
                east = file.AddGroup("east");
                west = file.AddGroup("west");

                // Fill out east first
                timeDim = east.AddDim("time"); // UNLIMITED
                latDim = east.AddDim("lat", 5); // 5 lats
                lonDim = east.AddDim("lon", 4); // 4 lons 
                timeVar = east.AddVar("time", NcDouble.Instance, timeDim);
                latVar = east.AddVar("lat", NcDouble.Instance, latDim);
                lonVar = east.AddVar("lon", NcDouble.Instance, lonDim);
                temp = east.AddVar("sst", NcFloat.Instance,(List<NcDim>)new[]{timeDim, latDim, lonDim}.ToList());

                timeVar.PutVar(new Int32[] { 0 } , new Int32[] { timeArray.Length }, timeArray);
                latVar.PutVar(latArray);
                lonVar.PutVar(lonArray);
                temp.PutVar(new Int32[] { 0,0,0}, new Int32[] { 7, 5, 4 }, sstArray);

                // Now check that the writes were successful and that
                // the read buffer matches the writes

                timeVar.GetVar(readBuffer);
                for(int i=0;i<7;i++) Assert.Equals(readBuffer[i], timeArray[i]);
                latVar.GetVar(readBuffer);
                for(int i=0;i<5;i++) Assert.Equals(readBuffer[i], latArray[i]);
                lonVar.GetVar(readBuffer);
                for(int i=0;i<4;i++) Assert.Equals(readBuffer[i], lonArray[i]);
                temp.GetVar(readBuffer);
                for(int i=0;i<140;i++) Assert.Equals((float)readBuffer[i], sstArray[i]);
            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }
        public bool TestScalar() {
            NcFile file = null;
            float[] buffer = new float[] { 1.0f };
            try {
                file = TestHelper.NewFile("sample.nc");
                NcVar var = file.AddVar("scalar", NcFloat.Instance);

                var.PutAtt("long_name", "A scalar variable");
                var.PutVar(new float[] { 1.0f });
                buffer[0] = 90.0f;
                Assert.Equals(buffer[0], 90.0f);
                var.GetVar(buffer);
                Assert.Equals(buffer[0], 1.0f);
            } finally {
                file.Close();
            }
            //CheckDelete(filePath);
            return true;
        }

    }
}

