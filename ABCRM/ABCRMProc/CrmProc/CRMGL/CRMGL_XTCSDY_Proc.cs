using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace BF.CrmProc.CRMGL
{
    public class CRMGL_XTCSDY : BASECRMClass
    {

        public string sLX = string.Empty;
        public string sDATATYPESTR = string.Empty;
        public string sYY = string.Empty;
        public string iDEF_VAL;
        public string iCUR_VAL;
        public string iMAX_VAL;
        public string iMIN_VAL;
        public int iSYSID;


        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iMIN_VAL;iMAX_VAL;iDEF_VAL;iCUR_VAL;";
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "B.JLBH");
            CondDict.Add("iSYSID", "B.SYSID");
            query.SQL.Text = "select B.* from BFCONFIG B where 1=1 ";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_XTCSDY obj = new CRMGL_XTCSDY();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sLX = query.FieldByName("LX").AsString;
            obj.iDEF_VAL = query.FieldByName("DEF_VAL").AsString;
            obj.iMAX_VAL = query.FieldByName("MAX_VAL").AsString;
            obj.iMIN_VAL = query.FieldByName("MIN_VAL").AsString;
            obj.sDATATYPESTR = query.FieldByName("DATATYPE").AsInteger == 1 ? "数字" : "";
            obj.iCUR_VAL = query.FieldByName("CUR_VAL").AsString;
            obj.sYY = query.FieldByName("YY").AsString;
            return obj;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != -1)
            {
                query.SQL.Text = "update BFCONFIG set CUR_VAL=:CUR_VAL where JLBH=:JLBH";
                query.ParamByName("CUR_VAL").AsString = iCUR_VAL;
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ExecSQL();
            }


        }
    }
}
