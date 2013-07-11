/*
 * Author Luke Campbell <LCampbell@asascience.com>
 */

using System;
using System.Collections.Generic;

namespace netcdf4 {

    public class NcAtt {
        public const Int32 NC_GLOBAL = -1;
        ~NcAtt() {
        }

        public NcAtt() {
            nullObject = true;
        }

        public NcAtt(bool nullObject) {
            nullObject = nullObject;
        }

        public NcAtt(NcAtt rhs) {
            nullObject = rhs.nullObject;
            myName = String.Copy(rhs.myName);
            groupId = rhs.groupId;
            varId = rhs.varId;
        }

        public string GetName() {
            return myName;
        }
    
        public Int32 GetAttLength() {
            Int32 lenp=0;
            NcCheck.Check(NetCDF.nc_inq_attlen(groupId, varId, myName, ref lenp));
            return lenp;
        }

        public NcType GetType()  {
            NetCDF.nc_type xtypep=0;
            NcCheck.Check(NetCDF.nc_inq_atttype(groupId, varId, myName, ref xtypep));

            if((int)xtypep <= 12) {
                // This is an atomic type
                return new NcType((int)xtypep);
            } else {
                Dictionary<string, NcType> typeMap = GetParentGroup().GetTypes(Location.ParentsAndCurrent);
                foreach(KeyValuePair<string, NcType> k in typeMap) {
                    if(k.Value.GetId() == (int)xtypep)
                        return k.Value;
                }
            }
            return new NcType();
        }

        public NcGroup GetParentGroup() {
            return new NcGroup(groupId);
        }

        public string GetValues() {
            return null;
        }


        /* getValues methods */

        public bool IsNull() { return nullObject; }

        protected bool nullObject;

        protected string myName;
        protected int groupId;
        protected int varId;
    }
}

