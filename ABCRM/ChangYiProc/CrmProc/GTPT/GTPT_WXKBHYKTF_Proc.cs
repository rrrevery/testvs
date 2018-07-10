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

namespace BF.CrmProc.GTPT
{
    public class GTPT_WXKBHYKTF_Proc : BASECRMClass
    {
        public string sCARDID = string.Empty;
        public string sQRCODEURL = string.Empty;
        public string sCONTENT = string.Empty;
        public string sCARDMC = string.Empty;
        public string sOLDCARDID = string.Empty;
        public int iPUBLICID = 0;
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from WX_KBHYKTF where CARDID='" + sCARDID + "'");
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            DeleteDataQuery(out msg, query, serverTime);
            query.SQL.Text = "insert into WX_KBHYKTF(CARDID,QRCODEURL,CONTENT,PUBLICID)";
            query.SQL.Add(" values(:CARDID,:QRCODEURL,:CONTENT,:PUBLICID)");
            query.ParamByName("QRCODEURL").AsString = sQRCODEURL;
            query.ParamByName("CONTENT").AsString = sCONTENT;
            query.ParamByName("CARDID").AsString = sCARDID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ExecSQL();

        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("sCARDID", "S.CARDID");
            CondDict.Add("sCARDMC", "B.TITLE");
            query.SQL.Text = "select S.*,B.TITLE from WX_KLXDEF B,WX_KBHYKTF S";
            query.SQL.Add("where B.CARDID=S.CARDID");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXKBHYKTF_Proc obj = new GTPT_WXKBHYKTF_Proc();
            obj.sQRCODEURL = query.FieldByName("QRCODEURL").AsString;
            obj.sCONTENT = query.FieldByName("CONTENT").AsString;
            obj.sCARDID = query.FieldByName("CARDID").AsString;
            obj.sCARDMC = query.FieldByName("TITLE").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            return obj;
        }
    }
}
