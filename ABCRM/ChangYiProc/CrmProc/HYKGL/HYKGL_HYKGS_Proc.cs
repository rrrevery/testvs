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
    public class HYKGL_HYKGS : DZHYK_DJLR_CLass
    {
        public int iLX = 0;//1挂失恢复0挂失
        public double fJF = 0;
        public double fJE = 0;
        //public string sFXDWDM = string.Empty;
        public string sFXDWMC = string.Empty;
        //public int iMDID = 0;
        public int iBJ_CHILD = 0;
        public int iJLBHA = 0;

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
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_CZK_KX", "CZJPJ_JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_CZK_KX");
            query.SQL.Text = "insert into HYK_CZK_KX(CZJPJ_JLBH,HYKTYPE,VIPID,HYKNO,ZY,JF,JE,JLJE,LX,DJSJ,DJR,DJRMC,CZDD,BJ_CHILD)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:HYID,:HYK_NO,:ZY,:JF,:JE,0,:LX,:DJSJ,:DJR,:DJRMC,:CZDD,:BJ_CHILD)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("HYK_NO").AsString = sHYKNO;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("JF").AsFloat = fJF;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("LX").AsInteger = iLX;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("BJ_CHILD").AsInteger = iBJ_CHILD;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_CZK_KX", serverTime, "CZJPJ_JLBH");
            if (iLX == 0)
            {
                if (iBJ_CHILD == 0)//挂式的为主卡
                {
                    CrmLibProc.UpdateHYKSTATUS(query, iHYID, (int)BASECRMDefine.HYXXStatus.HYXX_STATUS_KX);

                    //if (sHYK_NOC != "")
                    //{
                    //    //sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    //    query.SQL.Text = "insert into HYK_CHILD_JL(HYID,HY_NAME,HYK_NO,CDNR,JKRQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,YZM,FXDW,BJ_BD,BINDCARDTIME,ORDERID,WX_CARD,OPENID)";
                    //    query.SQL.Add(" select HYID,HY_NAME,HYK_NO,CDNR,JKRQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,-4,YBGDD,KHID,MDID,YZM,FXDW,BJ_BD,BINDCARDTIME,ORDERID,WX_CARD,OPENID");
                    //    query.SQL.Add(" from HYK_HYXX where HYK_NO='" + sHYKNO + "'");
                    //    query.ExecSQL();

                    //    query.SQL.Text = "update HYK_HYXX set (HY_NAME,HYK_NO,CDNR,JKRQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,YZM,FXDW,BJ_BD,BINDCARDTIME,ORDERID,WX_CARD,OPENID)=";
                    //    query.SQL.Add(" (select HY_NAME,HYK_NO,CDNR,JKRQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,YZM,FXDW,BJ_BD,BINDCARDTIME,ORDERID,WX_CARD,OPENID from HYK_CHILD_JL where HYK_NO='" + sHYK_NOC + "')");
                    //    query.SQL.Add(" where HYID=:HYID");
                    //    query.ParamByName("HYID").AsInteger = iHYID; ;
                    //    query.ExecSQL();

                    //    query.SQL.Clear();
                    //    query.SQL.Text = "delete from HYK_CHILD_JL  where HYID=:HYID and HYK_NO=:HYK_NO";
                    //    query.ParamByName("HYID").AsInteger = iHYID;
                    //    query.ParamByName("HYK_NO").AsString = sHYK_NOC;
                    //    query.ExecSQL();


                    //    iJLBHA = SeqGenerator.GetSeq("HYK_ZZKBGJL");

                    //    query.SQL.Text = "insert into HYK_ZZKBGJL(JLBH,HYKNO,ZY,DJSJ,DJR,DJRMC,CZDD,HYK_NOC,LX,DJJLBH)";
                    //    query.SQL.Add(" values(:JLBH,:HYK_NO,:ZY,:DJSJ,:DJR,:DJRMC,:CZDD,:HYK_NOC,:LX,:DJJLBH)");
                    //    query.ParamByName("JLBH").AsInteger = iJLBHA;
                    //    query.ParamByName("HYK_NO").AsString = sHYKNO;
                    //    query.ParamByName("HYK_NOC").AsString = sHYK_NOC;
                    //    query.ParamByName("ZY").AsString = "会员卡挂失";//挂失记录为0
                    //    query.ParamByName("LX").AsInteger = 0;
                    //    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    //    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    //    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    //    query.ParamByName("CZDD").AsString = sBGDDDM;
                    //    query.ParamByName("DJJLBH").AsInteger = iJLBH;

                    //    query.ExecSQL();
                    //}

                }
                else//挂失的为子卡
                {
                    query.Close();
                    query.SQL.Text = "update HYK_CHILD_JL set STATUS=:STATUS where HYK_NO=:HYK_NO";
                    query.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.HYXXStatus.HYXX_STATUS_KX;
                    query.ParamByName("HYK_NO").AsString = sHYKNO;
                    query.ExecSQL();
                }

            }
            else if (iLX == 1)
            {
                if (iBJ_CHILD == 0)
                {
                    CrmLibProc.UpdateHYKSTATUS(query, iHYID, (int)BASECRMDefine.HYXXStatus.HYXX_STATUS_XF);
                }
                else
                {
                    query.Close();
                    query.SQL.Text = "update HYK_CHILD_JL set STATUS=:STATUS where HYK_NO=:HYK_NO";
                    query.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.HYXXStatus.HYXX_STATUS_XF;
                    query.ParamByName("HYK_NO").AsString = sHYKNO;
                    query.ExecSQL();

                }
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iLX", "W.LX");
            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("sBGDDDM", "W.CZDD");
            CondDict.Add("sFXDWDM", "W.BGDDDM");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("sHY_NAME", "H.HY_NAME");
            CondDict.Add("iHYID", "H.HYID");
            CondDict.Add("sHYKNO", "W.HYKNO");
            CondDict.Add("fJF", "W.JF");
            CondDict.Add("fJE", "W.JE");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("sZY", "W.ZY");

            query.SQL.Text = "select W.*,H.HY_NAME,D.HYKNAME,B.BGDDMC,F.FXDWMC";
            query.SQL.Add(" from HYK_CZK_KX W,HYK_HYXX H,HYKDEF D,HYK_BGDD B,FXDWDEF F");
            query.SQL.Add(" where W.VIPID=H.HYID and H.HYKTYPE=D.HYKTYPE and W.CZDD=B.BGDDDM(+) and H.FXDW=F.FXDWID(+)");
            SetSearchQuery(query, lst);
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKGS obj = new HYKGL_HYKGS();
            obj.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            obj.iLX = query.FieldByName("LX").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.iHYID = query.FieldByName("VIPID").AsInteger;
            obj.sHYKNO = query.FieldByName("HYKNO").AsString;
            //obj.sHYK_NOC = query.FieldByName("HYK_NOC").AsString;
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.fJF = query.FieldByName("JF").AsFloat;
            obj.fJE = query.FieldByName("JE").AsFloat;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.sFXDWMC = query.FieldByName("FXDWMC").AsString;
            obj.sBGDDDM = query.FieldByName("CZDD").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
            return obj;
        }
    }
}


