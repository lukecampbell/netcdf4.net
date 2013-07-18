/*
 * Author Luke Campbell <LCampbell@asascience.com>
 */

using System;
using System.Text;
using System.Collections.Generic;

namespace ASA.NetCDF4 {

    public class NcAtt {
        public const Int32 NC_GLOBAL = -1;
        ~NcAtt() {
        }

        public NcAtt() {
            nullObject = true;
        }

        public NcAtt(bool nullObject) {
            this.nullObject = nullObject;
        }

        public NcAtt(NcAtt rhs) {
            nullObject = rhs.nullObject;
            myName = rhs.myName;
            groupId = rhs.groupId;
            varId = rhs.varId;
        }

        public virtual string GetName() {
            return myName;
        }
    
        public Int32 GetAttLength() {
            Int32 lenp=0;
            NcCheck.Check(NetCDF.nc_inq_attlen(groupId, varId, myName, ref lenp));
            return lenp;
        }

        public NcType GetNcType()  {
            int xtypep=0;
            NcCheck.Check(NetCDF.nc_inq_atttype(groupId, varId, myName, ref xtypep));

            if(xtypep <= 12) {
                // This is an atomic type
                return new NcType(xtypep);
            } else {
                Dictionary<string, NcType> typeMap = GetParentGroup().GetTypes(Location.ParentsAndCurrent);
                foreach(KeyValuePair<string, NcType> k in typeMap) {
                    if(k.Value.GetId() == xtypep)
                        return k.Value;
                }
            }
            return new NcType();
        }

        public NcGroup GetParentGroup() {
            return new NcGroup(groupId);
        }
        protected void CheckAttLen(int arrayLength) {
            Int32 att_len = GetAttLength();
            if(arrayLength < att_len)
                throw new exceptions.NcBufferOverflow("Array is not large enough to represent variable");
        }
        protected void CheckNull() {
            if(nullObject) {
                throw new exceptions.NcNullAtt("Attempt to invoke NcAtt method on a Null group");
            }
        }
        private void CheckFixedType() {
            if(!GetNcType().IsFixedType())
                throw new NotImplementedException("GetValues() for non-fixed types is not yet implemented");
        }

        public string GetValues() {
            CheckNull();
            ASCIIEncoding encoder = new ASCIIEncoding();
            int attLen = GetAttLength();
            byte[] buffer = new byte[attLen];
            NcCheck.Check(NetCDF.nc_get_att_text(groupId, varId, myName, buffer));
            return encoder.GetString(buffer);
        }

        public void GetValues(sbyte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_schar(groupId, varId, myName, dataValues));
        }

        public void GetValues(byte[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_uchar(groupId, varId, myName, dataValues));
        }

        public void GetValues(Int16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_short(groupId, varId, myName, dataValues));
        }

        public void GetValues(UInt16[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_ushort(groupId, varId, myName, dataValues));
        }

        public void GetValues(Int32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_int(groupId, varId, myName, dataValues));
        }

        public void GetValues(UInt32[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_uint(groupId, varId, myName, dataValues));
        }
        
        public void GetValues(Int64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_longlong(groupId, varId, myName, dataValues));
        }

        public void GetValues(UInt64[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_ulonglong(groupId, varId, myName, dataValues));
        }

        public void GetValues(float[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_float(groupId, varId, myName, dataValues));
        }

        public void GetValues(double[] dataValues, bool strictChecking=true) {
            CheckNull();
            if(strictChecking)
                CheckAttLen(dataValues.Length);
            CheckFixedType();
            NcCheck.Check(NetCDF.nc_get_att_double(groupId, varId, myName, dataValues));
        }



        /* getValues methods */

        public bool IsNull() { return nullObject; }

        protected bool nullObject;

        protected string myName;
        protected int groupId;
        protected int varId;
    }
}

