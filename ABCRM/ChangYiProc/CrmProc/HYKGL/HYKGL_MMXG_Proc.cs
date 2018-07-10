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


namespace BF.CrmProc.HYKGL
{
    public class HYKGL_MMXG : DZHYK_DJLR_CLass
    {
        public string sPSW_OLD = string.Empty;
        public string sPSW_NEW = string.Empty;
        public int iTYPE = 0;//0密码修改1密码重置          
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_MMXGJL", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CheckExecutedTable(query, "HYK_MMXGJL", "JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_MMXGJL");
            query.Close();
            query.SQL.Text = "insert into HYK_MMXGJL(JLBH,HYID,PSW_OLD,PSW_NEW,DJR,DJRMC,DJSJ,TYPE,BGDDDM)";
            query.SQL.Add(" values(:JLBH,:HYID,:PSW_OLD,:PSW_NEW,:DJR,:DJRMC,:DJSJ,:TYPE,:BGDDDM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("PSW_OLD").AsString = sPSW_OLD;
            query.ParamByName("PSW_NEW").AsString = sPSW_NEW;
            query.ParamByName("TYPE").AsInteger = iTYPE;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            query.SQL.Text = "update HYK_HYXX set PASSWORD=:PASSWORD where HYID=:HYID";
            query.ParamByName("PASSWORD").AsString = sPSW_NEW;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iTYPE", "W.TYPE");
            query.SQL.Text = "select W.*,H.HY_NAME,H.HYK_NO,D.HYKNAME,B.BGDDMC,G.SFZBH";
            query.SQL.Add("     from HYK_MMXGJL W,HYK_HYXX H,HYKDEF D,HYK_BGDD B,HYK_GRXX G");
            query.SQL.Add("    where W.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE and W.BGDDDM=B.BGDDDM and H.HYID=G.HYID(+)");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_MMXG obj = new HYKGL_MMXG();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sPSW_OLD = query.FieldByName("PSW_OLD").AsString;
            obj.sPSW_NEW = query.FieldByName("PSW_NEW").AsString;
            obj.iTYPE = query.FieldByName("TYPE").AsInteger;
            obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return obj;
        }
    }
}
