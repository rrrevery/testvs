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
using System.Net;
using System.IO;

namespace BF.CrmProc.GTPT
{
    public class GTPT_WXJB_Proc : DZHYK_DJLR_CLass
    {
        public string sOPENID = string.Empty;
        public string sPUBLICNAME = string.Empty;
        public int iLX = 0;
        public int iPUBLICID = 0;
        public string dBDSJ = string.Empty;
        public string sBINDCODE = string.Empty;
        public int iBJ_WXKBJB = 0;
        public class WXKBJB {
            public string card_id = string.Empty;
            public string code = string.Empty;
        }
        public class Token1
        {
            public string errCode = string.Empty;
            public string errmsg = string.Empty;
            public string result = string.Empty;

        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYID == 0)
            {
                msg = CrmLibStrings.msgNeedHYKNO;
                return false;
            }

            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_CANCELHYXX;", "JLBH", iJLBH);
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("iHYID", "W.HYID");
            CondDict.Add("sHYK_NO", "W.HYK_NO");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("sOPENID", "W.OPENID");
            CondDict.Add("sHY_NAME", "H.HY_NAME");
            CondDict.Add("sHYKNAME", "B.HYKNAME");

            query.SQL.Text = "select W.JLBH, W.HYK_NO,W.HYID,W.OPENID,W.HYKTYPE,W.PUBLICID,B.HYKNAME,C.PUBLICNAME,D.LX,B.HYKNAME,C.PUBLICNAME,D.LX,MAX(D.DJSJ) as BDSJ,W.DJRMC,W.DJSJ";
            query.SQL.Add(" from WX_CANCELHYXX W,HYKDEF B,WX_PUBLIC C,WX_BINDCARDJL D ");
            query.SQL.Add(" where W.HYKTYPE=B.HYKTYPE  and W.PUBLICID=C.PUBLICID and W.HYID=D.HYID(+) and W.PUBLICID=" + iLoginPUBLICID);
            SetSearchQuery(query, lst, true, "GROUP BY W.JLBH, W.HYK_NO,W.HYID,W.OPENID,W.HYKTYPE,B.HYKNAME,C.PUBLICNAME,D.LX,W.PUBLICID，w.djrmc,w.djsj");
            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_CANCELHYXX");
            query.SQL.Text = "delete  from  WX_HYKHYXX  where HYID=" + iHYID + "and UNIONID='" + sUNIONID + "'";
            query.ExecSQL();
            query.SQL.Text = "insert into WX_CANCELHYXX(JLBH,HYID,HYK_NO,HYKTYPE,OPENID,DJSJ,DJR,DJRMC,UNIONID,PUBLICID)";
            query.SQL.Add(" values(:JLBH,:HYID,:HYK_NO,:HYKTYPE,:OPENID,:DJSJ,:DJR,:DJRMC,:UNIONID,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("HYK_NO").AsString = sHYK_NO;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("OPENID").AsString = sOPENID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("PUBLICID").AsInteger = iPUBLICID;
            query.ParamByName("UNIONID").AsString = sUNIONID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            //解绑同时反激活微信卡包会员卡
            if (iBJ_WXKBJB == 1 && sBINDCODE !="")
            {
                WXKBJB obj = new WXKBJB();
                string pPUBLICIF = string.Empty;
                query.SQL.Text = "select * from WX_PUBLIC where PUBLICID=" + iPUBLICID;
                query.Open();
                if (!query.IsEmpty)
                {
                    pPUBLICIF = query.FieldByName("PUBLICIF").AsString;

                }
                obj.card_id = System.Configuration.ConfigurationManager.AppSettings["card_id"];
                obj.code = sBINDCODE;
                string content = JsonConvert.SerializeObject(obj);

                string token = CrmLibProc.getToken(pPUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(token);
                string outpost = string.Empty;
                string sPostUrl = String.Format("https://api.weixin.qq.com/card/membercard/unactivate?access_token={0}", oToken.result);

                bool isok = CrmLibProc.SendHttpPostRequest(out msg, sPostUrl, content, out outpost);
                CrmLibProc.WriteToLog("url:https://api.weixin.qq.com/card/membercard/unactivate?access_token={" + oToken.result + "}\r\nresult:" + outpost);
                Token1 oCard = new Token1();
                oCard = JsonConvert.DeserializeObject<Token1>(outpost);
                if (oCard.errCode != "0" || oCard.errmsg != "ok")
                    msg = oCard.errmsg;

            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_CANCELHYXX", serverTime, "JLBH");
            //query.SQL.Text = "Begin";
            //query.SQL.Text += "  update HYK_HYXX set OPENID=NULL";
            //query.SQL.Add(" where HYK_NO=:HYK_NO;");
            //query.SQL.Add("   update HYK_CHILD_JL set OPENID=NULL  where HYK_NO=:HYK_NO;");
            //query.SQL.Add("   end;");
            //query.ParamByName("HYK_NO").AsString = sHYK_NO;
            //query.ExecSQL();
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXJB_Proc obj = new GTPT_WXJB_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.iLX = query.FieldByName("LX").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sOPENID = query.FieldByName("OPENID").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            //obj.sUNIONID = query.FieldByName("UNIONID").AsString;
            //obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            //obj.iZXR = query.FieldByName("ZXR").AsInteger;
            //obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.dBDSJ = FormatUtils.DateToString(query.FieldByName("BDSJ").AsDateTime);
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            //obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.sPUBLICNAME = query.FieldByName("PUBLICNAME").AsString;
            return obj;
        }

    }
}
