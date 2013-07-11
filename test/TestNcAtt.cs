/*
 * Author Luke Campbell <LCampbell@ASAScience.com>
 * netcdf3.test.TestNcGroup
 */

using System;
using netcdf4;

namespace netcdf4.test {
    class TestNcAtt : UnitTest {
        protected const string filePath = "nc_clobber.nc";

        public TestNcAtt() {
            // Test addition goes here
            AddTest(TestAttributes, "TestAttributes");
        }
        public bool TestAttributes() {
            NcFile file = null;
            NcAtt att = null;
            sbyte[] sbyteBuf = new sbyte[2];
            byte[] byteBuf = new byte[2];
            short[] shortBuf = new short[2];
            ushort[] ushortBuf = new ushort[2];
            int[] intBuf = new int[2];
            uint[] uintBuf = new uint[2];
            long[] longBuf = new long[2];
            ulong[] ulongBuf = new ulong[2];
            float[] floatBuf = new float[2];
            double[] doubleBuf = new double[2];
            try { 
                file = TestHelper.NewFile(filePath);
                // Test global attributes
                att = file.PutAtt("string", "test");
                Assert.Equals(att.GetValues(), "test");

                att = file.PutAtt("sbyte", NcByte.Instance, (sbyte) -1);
                att.GetValues(sbyteBuf);
                Assert.Equals(sbyteBuf[0], (sbyte)(-1));
                att = file.PutAtt("sbyteA", NcByte.Instance, new sbyte[]{-1,1});
                att.GetValues(sbyteBuf);
                Assert.Equals(sbyteBuf[1], (sbyte)1);
                
                att = file.PutAtt("byte", NcByte.Instance, (byte) 2);
                att.GetValues(byteBuf);
                Assert.Equals(byteBuf[0], (byte)2);
                att = file.PutAtt("byteA", NcByte.Instance, new byte[]{2,1});
                att.GetValues(byteBuf);
                Assert.Equals(byteBuf[1], (byte)1);
                
                att = file.PutAtt("short", NcShort.Instance, (short) -1);
                att.GetValues(shortBuf);
                Assert.Equals(shortBuf[0], (short)(-1));
                att = file.PutAtt("shortA", NcShort.Instance, new short[]{-1,1});
                att.GetValues(shortBuf);
                Assert.Equals(shortBuf[1], (short)1);
                
                att = file.PutAtt("ushort", NcUshort.Instance, (ushort) 2);
                att.GetValues(ushortBuf);
                Assert.Equals(ushortBuf[0], (ushort)2);
                att = file.PutAtt("ushortA", NcUshort.Instance, new ushort[]{2,1});
                att.GetValues(ushortBuf);
                Assert.Equals(ushortBuf[1], (ushort)1);
                
                att = file.PutAtt("int", NcInt.Instance, (int) -1);
                att.GetValues(intBuf);
                Assert.Equals(intBuf[0], (int)(-1));
                att = file.PutAtt("intA", NcInt.Instance, new int[]{-1,1});
                att.GetValues(intBuf);
                Assert.Equals(intBuf[1], (int)1);
                
                att = file.PutAtt("uint", NcUint.Instance, (uint) 2);
                att.GetValues(uintBuf);
                Assert.Equals(uintBuf[0], (uint)2);
                att = file.PutAtt("uintA", NcUint.Instance, new uint[]{2,1});
                att.GetValues(uintBuf);
                Assert.Equals(uintBuf[1], (uint)1);
                
                att = file.PutAtt("long", NcInt64.Instance, (long) -1);
                att.GetValues(longBuf);
                Assert.Equals(longBuf[0], (long)(-1));
                att = file.PutAtt("longA", NcInt64.Instance, new long[]{-1,1});
                att.GetValues(longBuf);
                Assert.Equals(longBuf[1], (long)1);
                
                att = file.PutAtt("ulong", NcUint64.Instance, (ulong) 2);
                att.GetValues(ulongBuf);
                Assert.Equals(ulongBuf[0], (ulong)2);
                att = file.PutAtt("ulongA", NcUint64.Instance, new ulong[]{2,1});
                att.GetValues(ulongBuf);
                Assert.Equals(ulongBuf[1], (ulong)1);
                
                att = file.PutAtt("float", NcFloat.Instance, (float) -1);
                att.GetValues(floatBuf);
                Assert.Equals(floatBuf[0], (float)(-1));
                att = file.PutAtt("floatA", NcFloat.Instance, new float[]{-1,1});
                att.GetValues(floatBuf);
                Assert.Equals(floatBuf[1], (float)1);
                
                att = file.PutAtt("double", NcDouble.Instance, (double) 2);
                att.GetValues(doubleBuf);
                Assert.Equals(doubleBuf[0], (double)2);
                att = file.PutAtt("doubleA", NcDouble.Instance, new double[]{2,1});
                att.GetValues(doubleBuf);
                Assert.Equals(doubleBuf[1], (double)1);


            } finally {
                file.Close();
            }
            CheckDelete(filePath);
            return true;
        }

    }
}
