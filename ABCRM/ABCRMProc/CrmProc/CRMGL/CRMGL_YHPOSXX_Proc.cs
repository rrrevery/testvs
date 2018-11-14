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
    public class CRMGL_YHPOSXX : BASECRMClass
    {
        public string sSKTNO = string.Empty;
        public string sOldSKTNO = string.Empty;
        public string sXLH = string.Empty;
        public string sIPADDRESS = string.Empty;
        public string sMAINKEY = string.Empty;
        public int iJM = 0;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (sOldSKTNO != "")
            {
                query.SQL.Text = "update YHPOSXX set MACHINE=:MACHINE,XLH=:XLH,IPADDRESS=:IPADDRESS,MAINKEY=:MAINKEY,BJ_JM=:BJ_JM";
                query.SQL.Add(" where MACHINE=:OMACHINE");
                query.ParamByName("MACHINE").AsString = sSKTNO;
                query.ParamByName("OMACHINE").AsString = sOldSKTNO;
                query.ParamByName("XLH").AsString = sXLH;
                query.ParamByName("IPADDRESS").AsString = sIPADDRESS;
                query.ParamByName("MAINKEY").AsString = sMAINKEY;
                query.ParamByName("BJ_JM").AsInteger = iJM;
                query.ExecSQL();
            }
            else
            {
                query.SQL.Text = "insert into YHPOSXX(MACHINE,XLH,IPADDRESS,MAINKEY,BJ_JM)";
                query.SQL.Add(" values(:MACHINE,:XLH,:IPADDRESS,:MAINKEY,:BJ_JM)");
                query.ParamByName("MACHINE").AsString = sSKTNO;
                query.ParamByName("XLH").AsString = sXLH;
                query.ParamByName("IPADDRESS").AsString = sIPADDRESS;
                query.ParamByName("MAINKEY").AsString = sMAINKEY;
                query.ParamByName("BJ_JM").AsInteger = iJM;
                query.ExecSQL();
            }
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from YHPOSXX where MACHINE='" + sSKTNO + "' ");
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("sSKTNO", "MACHINE");
            query.SQL.Text = "select * from YHPOSXX where 1=1";
            //if (sSKTNO != string.Empty)
            //    query.SQL.AddLine("  and MACHINE='" + sSKTNO + "'");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            CRMGL_YHPOSXX obj = new CRMGL_YHPOSXX();
            obj.sSKTNO = query.FieldByName("MACHINE").AsString;
            obj.sXLH = query.FieldByName("XLH").AsString;
            obj.sIPADDRESS = query.FieldByName("IPADDRESS").AsString;
            obj.sMAINKEY = query.FieldByName("MAINKEY").AsString;
            obj.iJM = query.FieldByName("BJ_JM").AsInteger;
            return obj;
        }
    }
}
