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
    public class GTPT_WXSPTJ : BASECRMClass
    {
        public int iSPID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iWX_MDID = 0;
        public string sSPMC = string.Empty;
        public string sSPDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sSHDM = string.Empty;
        public string sMDMC = string.Empty;
        public string sSPJC = string.Empty;
        public string sIMG = string.Empty;
        public string sLOGO = string.Empty;
        public string sCONTENT = string.Empty;
        public string sFLMC = string.Empty;
        public int iFLID = 0;
        public string sPHONE = string.Empty;
        public string sIP = string.Empty;
        public string sDZ = string.Empty;
        public string sSPJG = string.Empty;
        public int iINX = 0;
        public int iCS = 0;
        public int iBJ_SHOW = 0;
        public int iSBID = 0;
        public string sSBMC = string.Empty;
        public string sSPCS = string.Empty;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "WX_SP", "SPID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("WX_SP");
            }
            query.SQL.Text = "insert into WX_SP(SPID,WX_MDID,SHDM,SPDM,SPMC,SPJG,IMG,LOGO,INX,DZ,PHONE,IP,CONTENT,CS,SPJC,BJ_SHOW,SBID,SPCS)";
            query.SQL.Add(" values(:SPID,:WX_MDID,:SHDM,:SPDM,:SPMC,:SPJG,:IMG,:LOGO,:INX,:DZ,:PHONE,:IP,:CONTENT,:CS,:SPJC,:BJ_SHOW,:SBID,:SPCS)");
            query.ParamByName("SPID").AsInteger = iJLBH;
            query.ParamByName("SPDM").AsString = sSPDM;
            query.ParamByName("WX_MDID").AsInteger = iWX_MDID;
            query.ParamByName("SHDM").AsString = sSHDM;
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("LOGO").AsString = sLOGO;
            query.ParamByName("IP").AsString = sIP;
            query.ParamByName("CS").AsInteger = iCS;
            query.ParamByName("INX").AsInteger = iINX;
            query.ParamByName("BJ_SHOW").AsInteger = iBJ_SHOW;
            query.ParamByName("SBID").AsInteger = iSBID;
            query.ParamByName("SPMC").AsString = sSPMC;
            query.ParamByName("SPJG").AsString = sSPJG;
            query.ParamByName("DZ").AsString = sDZ;
            query.ParamByName("PHONE").AsString = sPHONE;
            query.ParamByName("CONTENT").AsString = sCONTENT;
            query.ParamByName("SPJC").AsString = sSPJC;
            query.ParamByName("SPCS").AsString = sSPCS;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "B.SPID");
            CondDict.Add("iWX_MDID", "B.WX_MDID");
            CondDict.Add("sSPDM", "B.SPDM");
            CondDict.Add("sPHONE", "B.PHONE");
            CondDict.Add("sIP", "B.IP");
            CondDict.Add("sSHDM", "B.SHDM");

            List<Object> lst = new List<Object>();
            query.SQL.Text = "select B.*,S.SHMC,M.MDMC,T.SBMC from WX_SP B,SHDY S,WX_MDDY  M,WX_SB T ";
            query.SQL.Add(" where B.SHDM=S.SHDM and B.WX_MDID=M.MDID  and B.SBID=T.SBID");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXSPTJ obj = new GTPT_WXSPTJ();
            obj.iJLBH = query.FieldByName("SPID").AsInteger;
            obj.iWX_MDID = query.FieldByName("WX_MDID").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSPDM = query.FieldByName("SPDM").AsString;
            obj.sIMG = query.FieldByName("IMG").AsString;
            obj.sLOGO = query.FieldByName("LOGO").AsString;
            obj.iINX = query.FieldByName("INX").AsInteger;
            obj.sIP = query.FieldByName("IP").AsString;
            obj.iBJ_SHOW = query.FieldByName("BJ_SHOW").AsInteger;
            obj.iSBID = query.FieldByName("SBID").AsInteger;
            obj.sSBMC = query.FieldByName("SBMC").AsString;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.sSPMC = query.FieldByName("SPMC").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sCONTENT = LibProc.CyUrlEncode(query.FieldByName("CONTENT").AsString);
            obj.sPHONE = query.FieldByName("PHONE").AsString;
            obj.sDZ = query.FieldByName("DZ").AsString;
            obj.sSPJG = query.FieldByName("SPJG").AsString;
            obj.sSPJC = query.FieldByName("SPJC").AsString;
            obj.sSPCS = LibProc.CyUrlEncode(query.FieldByName("SPCS").AsString);
            return obj;
        }
    }
}
