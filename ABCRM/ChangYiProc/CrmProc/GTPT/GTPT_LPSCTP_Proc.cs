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
using BF.CrmProc;
using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_LPSCTP_Proc : BASECRMClass
    {
        public int iLPID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sLPDM = string.Empty;
        public string sIMG = string.Empty;
        public string sLPMC = string.Empty;
        public string sPIC_URL = string.Empty;
        public string sLPJS = string.Empty;
        public string sLPQC = string.Empty;
        public int iDHJF = 0;
        public int iBJ_NORMAL = 0;

        public override bool IsValidData(out string msg, BF.Pub.CyQuery query, System.DateTime serverTime)
        {
            msg = "";
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CrmLibProc.DeleteDataBase(query, out msg, "update HYK_JFFHLPXX set IMG=null,PIC_URL=null,LPJS=null where  LPID=" + iLPID);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "  update HYK_JFFHLPXX set IMG=:IMG,PIC_URL=:PIC_URL,LPJS=:LPJS,LPQC=:LPQC,DHJF=:DHJF,BJ_NORMAL=:BJ_NORMAL where  LPID=:LPID ";
            query.ParamByName("LPID").AsInteger = iLPID;
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("PIC_URL").AsString = sPIC_URL;
            query.ParamByName("DHJF").AsInteger = iDHJF;
            query.ParamByName("BJ_NORMAL").AsInteger = iBJ_NORMAL;
            query.ParamByName("LPJS").AsString = sLPJS;
            query.ParamByName("LPQC").AsString = sLPQC;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH","LPID");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select * from HYK_JFFHLPXX where 1=1 ";
            //if (iLPID !=0)
            //    query.SQL.AddLine(" where LPID=" + iLPID);
            SetSearchQuery(query, lst);
            return lst;

        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_LPSCTP_Proc obj = new GTPT_LPSCTP_Proc();
            obj.iJLBH = query.FieldByName("LPID").AsInteger;
            obj.sLPDM = query.FieldByName("LPDM").AsString;
            obj.sLPMC = query.FieldByName("LPMC").AsString;
            obj.iDHJF = query.FieldByName("DHJF").AsInteger;
            obj.iBJ_NORMAL = query.FieldByName("BJ_NORMAL").AsInteger;
            obj.sLPJS = LibProc.CyUrlEncode(query.FieldByName("LPJS").AsString);
            obj.sLPQC = query.FieldByName("LPQC").AsString;
            obj.sIMG = query.FieldByName("IMG").AsString;
            obj.sPIC_URL = query.FieldByName("PIC_URL").AsString;
            return obj;
        }
    }
}
