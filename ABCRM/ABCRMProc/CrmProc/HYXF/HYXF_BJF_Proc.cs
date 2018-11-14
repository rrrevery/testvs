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

namespace BF.CrmProc.HYXF
{
    public class HYXF_BJF_Proc : DJLR_ZX_CLass
    {
        public int iID;
        public string sMC = string.Empty;
        public double fJE = 0, fJF = 0;
        public int iHYID;
        public int iHYKTYPE;
        //public string sBGDDDM = string.Empty;
        public string sHYKNAME = string.Empty;
        public string sHYK_NO = string.Empty;
        //public string sBGDDMC = string.Empty;
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "BJF", "JLBH", iJLBH);
        }


        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("BJF");
            query.SQL.Text = "insert into BJF(JLBH,ID,JE,JF,HYID,HYKTYPE,BGDDDM,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:JLBH,:ID,:JE,:JF,:HYID,:HYKTYPE,:BGDDDM,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("ID").AsInteger = iID;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("JF").AsFloat = fJF;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "B.JLBH");
            CondDict.Add("iHYKTYPE", "B.HYKTYPE");
            CondDict.Add("iID", "B.ID");
            CondDict.Add("sHYK_NO", "X.HYK_NO");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("iDJR", "B.DJR");
            CondDict.Add("sDJRMC", "B.DJRMC");
            CondDict.Add("dDJSJ", "B.DJSJ");
            CondDict.Add("iZXR", "B.ZXR");
            CondDict.Add("sZXRMC", "B.ZXRMC");
            CondDict.Add("dZXRQ", "B.ZXRQ");

            query.SQL.Text = "select B.*,Z.MC,X.HYK_NO ,E.HYKNAME,D.BGDDMC from BJF B,SHJFGZ Z,HYK_HYXX X ,HYKDEF E,HYK_BGDD D";
            query.SQL.Add("    where B.ID=Z.ID AND B.HYID=X.HYID AND E.HYKTYPE=B.HYKTYPE AND D.BGDDDM=B.BGDDDM");
            SetSearchQuery(query, lst);
            return lst;

        }

        public override object SetSearchData(CyQuery query)
        {
            HYXF_BJF_Proc obj = new HYXF_BJF_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iID = query.FieldByName("ID").AsInteger;
            obj.sMC = query.FieldByName("MC").AsString;
            obj.fJE = query.FieldByName("JE").AsFloat;
            obj.fJF = query.FieldByName("JF").AsFloat;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return obj;
        }


        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "BJF", serverTime);
            int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            query.Close();
            CrmLibProc.UpdateJFZH(out msg, query, 0, iHYID, BASECRMDefine.HYK_JFBDCLLX_JFBDD, iJLBH, iMDID, fJF, iLoginRYID, sLoginRYMC, "补积分", 0, 0, 0, 0);

        }
    }
}