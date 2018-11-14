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
    public class GTPT_MEDIADY_Proc : DJLR_ZX_CLass
    {
        public int iTYPE = 0;
        public string sTITLE = string.Empty;
        public string sDESCRIPTION = string.Empty;
        public string sMEDIA_ID = string.Empty;
        public string sURL = string.Empty;
        public int iPUBLICID = 0;
        public string sTYPE
        {
            get {
                if (iTYPE == 1)
                    return "文字";
                else if (iTYPE == 2)
                    return "图片";
                else if (iTYPE == 3)
                    return "语音";
                else if (iTYPE == 4)
                    return "视频";
                else if (iTYPE == 5)
                    return "音乐";
                else if (iTYPE == 6)
                    return "图文";
                else if (iTYPE == 7)
                    return "卡包";
                else if (iTYPE == 8)
                    return "缩略图";
                else
                    return "";
            }
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "WX_MEDIADY", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_MEDIADY");
            query.SQL.Text = "insert into WX_MEDIADY(JLBH,TYPE,TITLE,DESCRIPTION,MEDIA_ID,URL,DJR,DJRMC,DJSJ,PUBLICID) ";
            query.SQL.Add(" values(:JLBH,:TYPE,:TITLE,:DESCRIPTION,:MEDIA_ID,:URL,:DJR,:DJRMC,:DJSJ,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("TYPE").AsInteger = iTYPE;
            query.ParamByName("TITLE").AsString = sTITLE;
            query.ParamByName("DESCRIPTION").AsString = sDESCRIPTION;
            query.ParamByName("MEDIA_ID").AsString = sMEDIA_ID;
            query.ParamByName("URL").AsString = sURL;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "F.JLBH");
            CondDict.Add("iDJR", "F.DJR");
            CondDict.Add("sDJRMC", "F.DJRMC");
            CondDict.Add("dDJSJ", "F.DJSJ");
            CondDict.Add("iTYPE", "F.TYPE");
            CondDict.Add("iPUBLICID", "F.PUBLICID");
            query.SQL.Text = "select * from WX_MEDIADY F where 1=1"; //F.TYPE in(2,3,4,5,8)
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_MEDIADY_Proc obj = new GTPT_MEDIADY_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iTYPE = query.FieldByName("TYPE").AsInteger;
            obj.sTITLE = query.FieldByName("TITLE").AsString;
            obj.sDESCRIPTION = query.FieldByName("DESCRIPTION").AsString;
            obj.sMEDIA_ID = query.FieldByName("MEDIA_ID").AsString;
            obj.sURL = query.FieldByName("URL").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            return obj;
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            ExecTable(query, "WX_MEDIADY", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ");

        }
    }
}
