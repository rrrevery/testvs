using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_GXHFHDY : BASECRMClass
    {
        public int iID = 0;
        public string sZD = string.Empty;
        public string sZDMC = string.Empty;
        public string sFH = string.Empty;
        public string[] asFieldNames = { 
                                            "iID;ID",
                                            "sZD;ZD",
                                            "sZDMC;ZDMC",
                                            "sFH;FH",                                                                                 
                                       };
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "begin";
            query.SQL.Add("update WX_GXHFHDY set FH=:FH where ID=:ID;");
            query.SQL.Add("if sql%notfound then");
            query.SQL.Add("insert into WX_GXHFHDY(ID,ZD,ZDMC,FH)");
            query.SQL.Add("values(:ID,:ZD,:ZDMC,:FH);");
            query.SQL.Add("end if;end;");
            query.ParamByName("ID").AsInteger = iID;
            query.ParamByName("ZD").AsString = sZD;
            query.ParamByName("ZDMC").AsString = sZDMC;
            query.ParamByName("FH").AsString = sFH;
            query.ExecSQL();
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select *";
            query.SQL.Add(" from WX_GXHFHDY");
            query.SQL.Add(" where 1=1");
            query.Open();
            while (!query.Eof)
            {
                GTPT_GXHFHDY obj = new GTPT_GXHFHDY();
                lst.Add(obj);
                obj.iID = query.FieldByName("ID").AsInteger;
                obj.sZD = query.FieldByName("ZD").AsString;
                obj.sZDMC = query.FieldByName("ZDMC").AsString;
                obj.sFH = query.FieldByName("FH").AsString;
                query.Next();
            }
            query.Close();       

            
                
                return lst;
        } 
    }
}
