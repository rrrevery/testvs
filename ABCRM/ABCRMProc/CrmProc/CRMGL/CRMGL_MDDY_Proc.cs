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
    public class CRMGL_MDDY : MDDY
    {
        public string sGXSHMC = string.Empty;
        public int iFDBH_JXC = 0;
        public int iBJ_ZB = 0;
        public bool qx = true;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MDDY", "MDID", iMDID);
        }

        public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            CheckChildExist(query, out msg, "HYK_BGDD;HYK_HYXX", "MDID", iJLBH);
            return msg == "";
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                //DeleteDataQuery(out msg, query,serverTime);
                query.SQL.Text = "update MDDY set SHDM=:SHDM,MDDM=:MDDM,MDMC=:MDMC,GXSHDM=:GXSHDM,FDBH_JXC=:FDBH_JXC";
                query.SQL.Add(" where MDID=:MDID");
                query.ParamByName("MDID").AsInteger = iMDID;
                query.ParamByName("SHDM").AsString = sSHDM;
                query.ParamByName("MDDM").AsString = sMDDM;
                query.ParamByName("MDMC").AsString = sMDMC;
                query.ParamByName("GXSHDM").AsString = sSHDM;// sGXSHDM;
                query.ParamByName("FDBH_JXC").AsInteger = iFDBH_JXC;
                query.ExecSQL();
            }
            else
            {
                iJLBH = SeqGenerator.GetSeqNoDBID("MDDY");
                query.SQL.Text = "insert into MDDY(MDID,SHDM,MDDM,MDMC,GXSHDM,FDBH_JXC)";
                query.SQL.Add(" values(:MDID,:SHDM,:MDDM,:MDMC,:GXSHDM,:FDBH_JXC)");
                query.ParamByName("MDID").AsInteger = iMDID;
                query.ParamByName("SHDM").AsString = sSHDM;
                query.ParamByName("MDDM").AsString = sMDDM;
                query.ParamByName("MDMC").AsString = sMDMC;
                query.ParamByName("GXSHDM").AsString = sSHDM;// sGXSHDM;
                query.ParamByName("FDBH_JXC").AsInteger = iFDBH_JXC;
                query.ExecSQL();
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iFDBH_JXC;";
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "B.MDID");
            CondDict.Add("sSHDM", "S.SHDM");
            CondDict.Add("sSHMC", "S.SHMC");
            CondDict.Add("sMDDM", "B.MDDM");
            CondDict.Add("sMDMC", "B.MDMC");
            CondDict.Add("sGXSHDM", "B.GXSHDM");
            query.SQL.Text = "select B.*,S.SHMC,(select SHMC from SHDY where SHDM=B.GXSHDM) GXSHMC from MDDY B,SHDY S";
            query.SQL.Add("where B.SHDM=S.SHDM");
            if (sReqMode != "View" && qx && iLoginRYID != GlobalVariables.SYSInfo.iAdminID)
            {
                query.SQL.Add(" and (exists(select 1 from XTCZY_MDQX X where X.PERSON_ID=" + iLoginRYID + " and X.MDID=B.MDID)");
                query.SQL.Add(" or exists(select 1 from CZYGROUP_MDQX X,XTCZYGRP G where G.PERSON_ID=" + iLoginRYID + " and X.MDID=B.MDID and X.ID=G.GROUPID))");
                //query.ParamByName("RYID").AsInteger = iLoginRYID;
            }
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            CRMGL_MDDY obj = new CRMGL_MDDY();
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sMDDM = query.FieldByName("MDDM").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sGXSHDM = query.FieldByName("GXSHDM").AsString;
            obj.sGXSHMC = query.FieldByName("GXSHMC").AsString;
            obj.iFDBH_JXC = query.FieldByName("FDBH_JXC").AsInteger;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            return obj;
        }
    }
}
