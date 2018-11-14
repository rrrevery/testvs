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

using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_WXPPSBDY : BASECRMClass
    {

        public int iSBID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sSBMC = string.Empty;
        public string sIMG = string.Empty;
        public string sLOGO = string.Empty;
        public string sCONTENT = string.Empty;
        public string sFLMC = string.Empty;
        public int iFLID = 0;
        public string sPHONE = string.Empty;
        public string sIP = string.Empty;
        public string sDZ = string.Empty;
        public int iBJ_TJ = 0;
        public int iBJ_SHOW = 0;
        public string sYWM = string.Empty;
        public string sSBJJ = string.Empty;
        public int iINX = 0; public int A = 0, R;
        public int iPUBLICID = 0;

        public override bool IsValidData(out string msg, BF.Pub.CyQuery query, System.DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "WX_SB", "SBID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_SB");
            query.SQL.Text = "insert into WX_SB(SBID,SBMC,IMG,LOGO,INX,DZ,PHONE,IP,CONTENT,FLID,YWM,PUBLICID)";
            query.SQL.Add(" values(:SBID,:SBMC,:IMG,:LOGO,:INX,:DZ,:PHONE,:IP,:CONTENT,:FLID,:YWM,:PUBLICID)");
            query.ParamByName("SBID").AsInteger = iJLBH;
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("LOGO").AsString = sLOGO;
            query.ParamByName("FLID").AsInteger = iFLID;
            query.ParamByName("INX").AsInteger = iINX;
            query.ParamByName("YWM").AsString = sYWM;
            query.ParamByName("SBMC").AsString = sSBMC;
            query.ParamByName("DZ").AsString = sDZ;
            query.ParamByName("PHONE").AsString = sPHONE;
            query.ParamByName("IP").AsString = sIP;
            query.ParamByName("CONTENT").AsString = sCONTENT;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ExecSQL();

        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "B.SBID");
            CondDict.Add("iINX", "B.INX");
            CondDict.Add("sPHONE", "B.PHONE");
            CondDict.Add("sIP", "B.IP");
            CondDict.Add("sCONTENT", "B.CONTENT");
            CondDict.Add("iSBID", "B.SBID");
            CondDict.Add("iFLID", "B.FLID");
            CondDict.Add("iBJ_TJ", "B.BJ_TJ");
            CondDict.Add("iBJ_SHOW", "B.BJ_SHOW");
            CondDict.Add("sYWM", "B.YWM");

            query.SQL.Text = "select B.*,F.FLMC,F.FLID from WX_SB B,WX_FL F where B.FLID=F.FLID";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXPPSBDY obj = new GTPT_WXPPSBDY();
            obj.iJLBH = query.FieldByName("SBID").AsInteger;
            obj.sIMG = query.FieldByName("IMG").AsString;
            obj.sLOGO = query.FieldByName("LOGO").AsString;
            obj.iINX = query.FieldByName("INX").AsInteger;
            obj.iFLID = query.FieldByName("FLID").AsInteger;
            obj.sPHONE = query.FieldByName("PHONE").AsString;
            obj.sYWM = query.FieldByName("YWM").AsString;
            obj.sSBMC = query.FieldByName("SBMC").AsString;
            obj.sFLMC = query.FieldByName("FLMC").AsString;
            obj.sDZ = query.FieldByName("DZ").AsString;
            obj.sIP = query.FieldByName("IP").AsString;
            obj.sCONTENT = LibProc.CyUrlEncode(query.FieldByName("CONTENT").AsString);
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            return obj;
        }
    }
}
