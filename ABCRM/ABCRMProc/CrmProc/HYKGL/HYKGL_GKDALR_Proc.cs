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

namespace BF.CrmProc.HYKGL
{
    public class HYKGL_GKDALR : HYXX_Detail
    {
        public class _STATUS
        {
            public int iJLBH = -1;
            public int iHYKTYPE = -1;
            public int iFXDWID = -1;
            public int iFXQDID = -1;
            public int iBJ_NAME = -1;
            public int iBJ_SJHM = -1;
            public int iBJ_QQ = -1;
            public int iBJ_WX = -1;
            public int iBJ_TXDZ = -1;
            public int iBJ_GZDW = -1;
            public int iBJ_ZY = -1;
            public int iBJ_JTGJ = -1;
        }
        public class _JTXX
        {
            public string JTXM = string.Empty;
            public string JTGX = string.Empty;
            public string JTXB = string.Empty;
            public string sJTNL = string.Empty;
            public int JTNL = 0;
            public string JTSR = string.Empty;
        }
        public HYXX_Detail HYKXX;
        public List<HYXX_Detail> HYKXXLIST;//所有卡号信息
        public string CBL_HYBJ;
        public List<_JTXX> JTXX;
        public int YZ_SJHM = 0;


        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_GKDA", "GKID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            int tp_gkid = iJLBH;

            if (HYKXX.sSJHM == "")
            {
                msg = "失败！请输入手机号码！";
                return;
            }

            if (tp_gkid != 0)
            {
                HYKGL_DALR tp1 = new HYKGL_DALR();
                tp1 = SearchDataZY(tp_gkid.ToString());
                Set_GKDA_CHANGGE(query, tp_gkid, tp1.HYKXX.sSJHM, HYKXX.sSJHM, "手机号码");
                Set_GKDA_CHANGGE(query, tp_gkid, tp1.HYKXX.sSFZBH, HYKXX.sSFZBH, "身份证");
                Set_GKDA_CHANGGE(query, tp_gkid, tp1.HYKXX.sTXDZ, HYKXX.sTXDZ, "通讯地址");
                Set_GKDA_CHANGGE(query, tp_gkid, tp1.HYKXX.iTJRYID.ToString(), HYKXX.iTJRYID.ToString(), "推荐人");
                //if (tp1.HYKXX.sWX != HYKXX.sWX || tp1.HYKXX.sSJHM != HYKXX.sSJHM)
                //{
                //    int tp_jlbh = SeqGenerator.GetSeq("HYK_ZYDAXGJL");
                //    query.SQL.Text = "insert into HYK_ZYDAXGJL(JLBH,GKID,OLD_SJHM,NEW_SJHM,OLD_WX,NEW_WX,DJSJ,DJR,DJRMC)";
                //    query.SQL.Add(" values(:JLBH,:GKID,:OLD_SJHM,:NEW_SJHM,:OLD_WX,:NEW_WX,:DJSJ,:DJR,:DJRMC)");
                //    query.ParamByName("JLBH").AsInteger = tp_jlbh;
                //    query.ParamByName("GKID").AsInteger = tp_gkid;
                //    query.ParamByName("OLD_SJHM").AsString = tp1.HYKXX.sSJHM;
                //    query.ParamByName("NEW_SJHM").AsString = HYKXX.sSJHM;
                //    query.ParamByName("OLD_WX").AsString = tp1.HYKXX.sWX;
                //    query.ParamByName("NEW_WX").AsString = HYKXX.sWX;
                //    query.ParamByName("DJR").AsInteger = iLoginRYID;
                //    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                //    query.ParamByName("DJSJ").AsDateTime = serverTime;
                //    query.ExecSQL();

                //}
                //query.SQL.Text = "  update HYK_GKDA set sjhm='' where SJHM='" + HYKXX.sSJHM.Trim() + "'";
                //query.ExecSQL();

            }
            else
            {
                tp_gkid = SeqGenerator.GetSeq("HYK_GKDA");
                iJLBH = tp_gkid;
            }
            HYKXX.sHYK_NO = sHYK_NO;
            HYKXX.iGKID = tp_gkid;
            CrmLibProc.SaveGRXX(query, HYKXX, iLoginRYID, sLoginRYMC);


            #region JTXX 家庭信息
            if (JTXX.Count > 0)
            {
                query.SQL.Clear();
                query.Params.Clear();
                query.SQL.Text = "Delete from  HYK_GKDA_JTXX where GKID=" + tp_gkid + "";
                query.ExecSQL();
                for (int i = 0; i < JTXX.Count; i++)
                {
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = "insert into HYK_GKDA_JTXX(GKID,JTXM,JTGX,JTXB,JTNL,JTSR) values(:GKID,:JTXM,:JTGX,:JTXB,:JTNL,:JTSR)";
                    query.ParamByName("GKID").AsInteger = tp_gkid;
                    query.ParamByName("JTXM").AsString = JTXX[i].JTXM.ToString();
                    query.ParamByName("JTGX").AsString = JTXX[i].JTGX.ToString();
                    query.ParamByName("JTXB").AsString = JTXX[i].JTXB.ToString();
                    query.ParamByName("JTNL").AsInteger = JTXX[i].JTNL;
                    query.ParamByName("JTSR").AsDateTime = FormatUtils.ParseDateString(JTXX[i].JTSR);
                    query.ExecSQL();
                }
            }
            #endregion
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "G.GKID");
            CondDict.Add("sHY_NAME", "G.GK_NAME");
            CondDict.Add("iSEX", "G.SEX");
            CondDict.Add("sSJHM", "G.SJHM");
            CondDict.Add("iZJLXID", "G.ZJLXID");
            CondDict.Add("sSFZBH", "G.SFZBH");
            CondDict.Add("iZYID", "G.ZYID");
            CondDict.Add("iXLID", "G.XLID");
            CondDict.Add("iJTSRID", "G.JTSRID");
            CondDict.Add("iJTGJID", "G.JTGJID");
            CondDict.Add("sDJRMC", "G.DJRMC");
            CondDict.Add("sGXRMC", "G.GXRMC");
            CondDict.Add("dDJSJ", "G.DJSJ");
            CondDict.Add("dGXSJ", "G.GXSJ");
            CondDict.Add("iGKID", "G.GKID");
            CondDict.Add("sHYK_NO", "H.HYK_NO");
            CondDict.Add("iSQID", "S.SQID");
            CondDict.Add("iXQID", "G.XQID");
            query.SQL.Text = "select distinct G.*,B.GK_NAME TJRYMC,PERSON_NAME KHJLMC ,X.XQMC,Q.QYMC QQYMC ";
            query.SQL.AddLine("from HYK_GKDA G,HYK_GKDA B,RYXX C ,XQDY X,XQDYITEM S,HYK_HYQYDY Q ");
            query.SQL.AddLine(" where G.TJRYID=B.GKID(+) and G.KHJLRYID=C.PERSON_ID(+) ");
            query.SQL.AddLine("  and G.XQID=X.XQID(+) and G.QYID=Q.QYID(+) and G.XQID = S.XQID(+) ");
            query.SQL.AddLine(" and rownum<10000 ");
            if (!sHYK_NO.Equals(""))
            {
                query.SQL.AddLine("  and G.GKID=(select unique H.GKID from HYK_HYXX H,HYK_CHILD_JL J where H.HYID=J.HYID(+) and (  H.HYK_NO='" + sHYK_NO + "' or J.HYK_NO='" + sHYK_NO + "' ))");
            }
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Clear();
                query.Params.Clear();
                List<_JTXX> tp_jtxxs = new List<_JTXX>();
                query.SQL.Text = "SELECT * FROM HYK_GKDA_JTXX WHERE GKID=" + iJLBH + "";
                query.Open();
                while (!query.Eof)
                {
                    _JTXX tp_jtxx = new _JTXX();
                    tp_jtxx.JTXM = query.FieldByName("JTXM").AsString;
                    tp_jtxx.JTGX = query.FieldByName("JTGX").AsString;
                    tp_jtxx.JTXB = query.FieldByName("JTXB").AsString;
                    tp_jtxx.JTNL = query.FieldByName("JTNL").AsInteger;
                    tp_jtxx.JTSR = FormatUtils.DateToString(query.FieldByName("JTSR").AsDateTime);
                    tp_jtxxs.Add(tp_jtxx);
                    query.Next();
                }
                query.Close();
                ((HYKGL_GKDALR)lst[0]).JTXX = tp_jtxxs;
                List<HYXX_Detail> hyklist = new List<HYXX_Detail>();
                query.Close();
                query.SQL.Clear();
                query.SQL.Text = "   select H.FXDW,  H.HYID ,H.Hyk_No,H.Gkid,H.Hyktype,H.Status,H.HY_NAME,H.JKRQ,H.BJ_PSW,2 Bj_Parent,'附属卡' BJ_PARENTSTR, h.mainhyid,H.YXQ,";
                query.SQL.Add("  D.HYKNAME,D.CDJZ  ,nvl(F.LJXFJE,0) LJXFJE  ,nvl(F.WCLJF,0) WCLJF ,E.QCYE,E.YE  ,A.MDMC,B.FXDWMC   ");
                query.SQL.Add("    from HYK_HYXX X ,HYKDEF D,HYK_JFZH F,HYK_JEZH E ,MDDY A,FXDWDEF B ,HYK_HYXX H");
                query.SQL.Add("    where  X.HYKTYPE=D.HYKTYPE and X.HYID=F.HYID(+) and X.HYID=E.HYID(+) ");
                query.SQL.Add("    and X.MDID=A.MDID(+)  and X.FXDW=B.FXDWID(+) and X.GKID=" + iJLBH + "");
                query.SQL.Add("     and X.MAINHYID=H.HYID");
                query.SQL.Add("  union");
                query.SQL.Add("  select H.FXDW, H.HYID ,H.Hyk_No,H.Gkid,H.Hyktype,H.Status,H.HY_NAME,H.JKRQ,H.BJ_PSW,2 Bj_Parent,'主卡' BJ_PARENTSTR,H.MAINHYID, H.YXQ,");
                query.SQL.Add("   D.HYKNAME,D.CDJZ  ,nvl(F.LJXFJE,0) LJXFJE  ,nvl(F.WCLJF,0) WCLJF ,E.QCYE,E.YE  ,A.MDMC,B.FXDWMC ");
                query.SQL.Add("    from HYK_HYXX X ,HYKDEF D,HYK_JFZH F,HYK_JEZH E ,MDDY A,FXDWDEF B ,HYK_HYXX H");
                query.SQL.Add("    where  X.HYKTYPE=D.HYKTYPE and X.HYID=F.HYID(+) and X.HYID=E.HYID(+) ");
                query.SQL.Add("   and X.MDID=A.MDID(+)  and X.FXDW=B.FXDWID(+) and X.GKID=" + iJLBH + "");
                query.SQL.Add("   and X.Hyid=H.MAINHYID");
                query.SQL.Add("  union");
                query.SQL.Add("   select H.FXDW,  H.HYID ,H.Hyk_No,H.Gkid,H.Hyktype,H.Status,H.HY_NAME,H.JKRQ,H.BJ_PSW,H.Bj_Parent,'主卡' BJ_PARENTSTR,H.MAINHYID,H.YXQ,");
                query.SQL.Add("   D.HYKNAME,D.CDJZ  ,nvl(F.LJXFJE,0) LJXFJE  ,nvl(F.WCLJF,0) WCLJF ,E.QCYE,E.YE  ,A.MDMC,B.FXDWMC ");
                query.SQL.Add("    from HYK_HYXX H ,HYKDEF D,HYK_JFZH F,HYK_JEZH E ,MDDY A,FXDWDEF B ");
                query.SQL.Add("    where  H.HYKTYPE=D.HYKTYPE and H.HYID=F.HYID(+) and H.HYID=E.HYID(+)");
                query.SQL.Add("    and H.MDID=A.MDID(+)  and H.FXDW=B.FXDWID(+) and H.GKID=" + iJLBH + "");
                query.SQL.Add("    union");
                query.SQL.Add("  select  H.FXDW,  H.HYID ,J.Hyk_No,H.Gkid,H.Hyktype,J.Status,J.HY_NAME,J.JKRQ,J.BJ_PSW,0 Bj_Parent,'子卡' BJ_PARENTSTR, MAINHYID,H.YXQ, ");
                query.SQL.Add("   D.HYKNAME,D.CDJZ  ,nvl(F.LJXFJE,0) LJXFJE  ,nvl(F.WCLJF,0) WCLJF ,E.QCYE,E.YE  ,A.MDMC,B.FXDWMC");
                query.SQL.Add("  from HYK_HYXX H ,HYKDEF D,HYK_JFZH F,HYK_JEZH E ,MDDY A,FXDWDEF B ,HYK_CHILD_JL J ");
                query.SQL.Add("    where H.HYID=J.HYID  and  H.HYKTYPE=D.HYKTYPE and H.HYID=F.HYID(+) and H.HYID=E.HYID(+)");
                query.SQL.Add("    and J.MDID=A.MDID(+)  and J.FXDW=B.FXDWID(+) and H.GKID=" + iJLBH + "");
                query.SQL.Add("   order by  status desc");
                query.Open();
                while (!query.Eof)
                {
                    HYXX_Detail item = new HYXX_Detail();
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.iGKID = query.FieldByName("GKID").AsInteger;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    item.fLJXFJE = query.FieldByName("LJXFJE").AsFloat;
                    item.fQCYE = query.FieldByName("QCYE").AsFloat;
                    item.fCZJE = query.FieldByName("YE").AsFloat;
                    item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                    item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.fYE = query.FieldByName("YE").AsFloat;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                    item.dJKRQ = query.FieldByName("JKRQ").AsDateTime.ToString();
                    item.iBJ_PSW = query.FieldByName("BJ_PSW").AsInteger;
                    item.iCDJZ = query.FieldByName("CDJZ").AsInteger;//??
                    item.iFXDW = query.FieldByName("FXDW").AsInteger;
                    if (query.FieldByName("BJ_PARENT").AsInteger == 0)
                    {
                        item.iBJ_PARENT = 1;
                    }
                    else if (query.FieldByName("BJ_PARENT").AsInteger == 1)
                    {
                        item.iBJ_PARENT = 0;
                    }
                    else
                    {
                        item.iBJ_PARENT = query.FieldByName("BJ_PARENT").AsInteger;
                    }
                    item.iGKID = query.FieldByName("GKID").AsInteger;
                    hyklist.Add(item);
                    query.Next();
                }
                query.Close();
                ((HYKGL_GKDALR)lst[0]).HYKXXLIST = hyklist;
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            iJLBH = query.FieldByName("GKID").AsInteger;
            HYKGL_GKDALR item = new HYKGL_GKDALR();
            item.iJLBH = query.FieldByName("GKID").AsInteger;

            item.HYKXX = new HYXX_Detail();
            item.HYKXX.iGKID = query.FieldByName("GKID").AsInteger;
            item.HYKXX.iZJLXID = query.FieldByName("ZJLXID").AsInteger;
            item.HYKXX.iBJ_YZZJLX = query.FieldByName("BJ_YZZJLX").AsInteger;//验证 身份证标记
            item.HYKXX.sSFZBH = query.FieldByName("SFZBH").AsString;
            item.HYKXX.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
            item.HYKXX.sHY_NAME = query.FieldByName("GK_NAME").AsString;
            item.HYKXX.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
            item.HYKXX.iTJRYID = query.FieldByName("TJRYID").AsInteger;//推荐人
            item.HYKXX.sTJRYMC = query.FieldByName("TJRYMC").AsString;//推荐人名称
            item.HYKXX.sSJHM = query.FieldByName("SJHM").AsString;
            item.HYKXX.iBJ_YZSJHM = query.FieldByName("BJ_YZSJHM").AsInteger;//验证 标记
            item.HYKXX.sPHONE = query.FieldByName("PHONE").AsString;
            item.HYKXX.sQQ = query.FieldByName("QQ").AsString;//QQ
            item.HYKXX.sWX = query.FieldByName("WX").AsString;//WX
            item.HYKXX.sWB = query.FieldByName("WB").AsString;//WB
            item.HYKXX.sEMAIL = query.FieldByName("E_MAIL").AsString;
            item.HYKXX.sYZBM = query.FieldByName("YZBM").AsString;
            item.HYKXX.sTXDZ = query.FieldByName("TXDZ").AsString;
            item.HYKXX.sGZDW = query.FieldByName("GZDW").AsString;
            //JSXX
            //Other--------------------------------------------------------
            item.HYKXX.iZYID = query.FieldByName("ZYID").AsInteger;//zhiye
            item.HYKXX.iXLID = query.FieldByName("XLID").AsInteger;
            item.HYKXX.iJTSRID = query.FieldByName("JTSRID").AsInteger;
            item.HYKXX.iJTGJID = query.FieldByName("JTGJID").AsInteger;
            item.HYKXX.iJTCYID = query.FieldByName("JTCYID").AsInteger;

            item.HYKXX.iQCPPID = query.FieldByName("QCPPID").AsInteger;
            item.HYKXX.sCPH = query.FieldByName("CPH").AsString;
            item.HYKXX.iJHBJ = query.FieldByName("JHBJ").AsInteger;
            item.HYKXX.dJHJNR = FormatUtils.DateToString(query.FieldByName("JHJNR").AsDateTime);
            item.HYKXX.sPPHY = query.FieldByName("PPHY").AsString;
            item.HYKXX.sBZ = query.FieldByName("BZ").AsString;

            item.HYKXX.sPPXQ = query.FieldByName("PPXQ").AsString;
            item.HYKXX.iKHJLRYID = query.FieldByName("KHJLRYID").AsInteger;
            item.HYKXX.sKHJLMC = query.FieldByName("KHJLMC").AsString;

            //not true
            item.HYKXX.sZW = query.FieldByName("ZW").AsString;

            item.HYKXX.iZSCSJID = query.FieldByName("ZSCSJID").AsInteger;
            item.HYKXX.sFAX = query.FieldByName("FAX").AsString;
            item.HYKXX.sBGDH = query.FieldByName("BGDH").AsString;
            item.HYKXX.iQYID = query.FieldByName("QYID").AsInteger;

            item.HYKXX.iBJ_CLD = query.FieldByName("BJ_CLD").AsInteger;
            item.HYKXX.sQIYEMC = query.FieldByName("QYMC").AsString;
            item.HYKXX.sQIYEXZ = query.FieldByName("QYXZ").AsString;
            item.HYKXX.sGKNC = query.FieldByName("GKNC").AsString;
            item.HYKXX.iMZID = query.FieldByName("MZID").AsInteger;


            item.HYKXX.iDJR = query.FieldByName("DJR").AsInteger;
            item.HYKXX.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.HYKXX.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.HYKXX.iGXR = query.FieldByName("GXR").AsInteger.ToString();
            item.HYKXX.sGXRMC = query.FieldByName("GXRMC").AsString;
            item.HYKXX.dGXSJ = FormatUtils.DatetimeToString(query.FieldByName("GXSJ").AsDateTime);
            item.HYKXX.sCanvas = query.FieldByName("Canvas").AsString;
            //
            item.HYKXX.sXXFS = query.FieldByName("XXFS").AsString;
            item.HYKXX.sYYAH = query.FieldByName("YYAH").AsString;
            item.HYKXX.sCXXX = query.FieldByName("CXXX").AsString;
            item.HYKXX.iCANSMS = query.FieldByName("CANSMS").AsInteger;
            item.HYKXX.iXQID = query.FieldByName("XQID").AsInteger;
            item.HYKXX.sXQMC = query.FieldByName("XQMC").AsString;
            item.HYKXX.sSW = query.FieldByName("SW").AsString;//三围
            item.HYKXX.sXZ = query.FieldByName("XZ").AsString;//星座
            item.HYKXX.sSX = query.FieldByName("SX").AsString;//属相
            item.HYKXX.sCM = query.FieldByName("CM").AsString;//尺码
            item.HYKXX.sROAD = query.FieldByName("ROAD").AsString;
            item.HYKXX.sHOUSENUM = query.FieldByName("HOUSENUM").AsString;
            item.HYKXX.sIMGURL = query.FieldByName("IMAGEURL").AsString;

            //mease infomation
            item.iZJLXID = query.FieldByName("ZJLXID").AsInteger;
            item.iBJ_YZZJLX = query.FieldByName("BJ_YZZJLX").AsInteger;//验证 身份证标记
            item.sSFZBH = query.FieldByName("SFZBH").AsString;
            item.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
            item.sHY_NAME = query.FieldByName("GK_NAME").AsString;
            item.iTJRYID = query.FieldByName("TJRYID").AsInteger;//推荐人
            item.sTJRYMC = query.FieldByName("TJRYMC").AsString;//推荐人名称
            //meommunication infomation
            item.sSJHM = query.FieldByName("SJHM").AsString;
            item.iBJ_YZSJHM = query.FieldByName("BJ_YZSJHM").AsInteger;//验证 标记
            item.sPHONE = query.FieldByName("PHONE").AsString;
            item.sQQ = query.FieldByName("QQ").AsString;//QQ
            item.sWX = query.FieldByName("WX").AsString;//WX
            item.sWB = query.FieldByName("WB").AsString;//WB
            item.sEMAIL = query.FieldByName("E_MAIL").AsString;
            item.sYZBM = query.FieldByName("YZBM").AsString;
            item.iQYID = query.FieldByName("QYID").AsInteger;
            item.sTXDZ = query.FieldByName("TXDZ").AsString;
            //PP
            item.sGZDW = query.FieldByName("GZDW").AsString;
            //JS
            //Ot-----------------------------------------------------
            item.iZYID = query.FieldByName("ZYID").AsInteger;//zhiye
            item.iXLID = query.FieldByName("XLID").AsInteger;
            item.iJTSRID = query.FieldByName("JTSRID").AsInteger;
            item.iJTGJID = query.FieldByName("JTGJID").AsInteger;
            item.iJTCYID = query.FieldByName("JTCYID").AsInteger;

            item.iQCPPID = query.FieldByName("QCPPID").AsInteger;
            item.sCPH = query.FieldByName("CPH").AsString;
            item.iJHBJ = query.FieldByName("JHBJ").AsInteger;
            item.dJHJNR = FormatUtils.DateToString(query.FieldByName("JHJNR").AsDateTime);
            item.sPPHY = query.FieldByName("PPHY").AsString;
            item.sBZ = query.FieldByName("BZ").AsString;

            item.sPPXQ = query.FieldByName("PPXQ").AsString;
            item.iKHJLRYID = query.FieldByName("KHJLRYID").AsInteger;
            item.sKHJLMC = query.FieldByName("KHJLMC").AsString;

            //no
            item.sZW = query.FieldByName("ZW").AsString;

            item.iZSCSJID = query.FieldByName("ZSCSJID").AsInteger;
            item.sFAX = query.FieldByName("FAX").AsString;
            item.sBGDH = query.FieldByName("BGDH").AsString;
            item.iQYID = query.FieldByName("QYID").AsInteger;

            item.iBJ_CLD = query.FieldByName("BJ_CLD").AsInteger;
            item.sQIYEMC = query.FieldByName("QYMC").AsString;
            item.sQIYEXZ = query.FieldByName("QYXZ").AsString;
            item.sGKNC = query.FieldByName("GKNC").AsString;
            item.iMZID = query.FieldByName("MZID").AsInteger;


            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iGXR = query.FieldByName("GXR").AsInteger.ToString();
            item.sGXRMC = query.FieldByName("GXRMC").AsString;
            item.dGXSJ = FormatUtils.DatetimeToString(query.FieldByName("GXSJ").AsDateTime);
            item.sCanvas = query.FieldByName("Canvas").AsString;
            item.sXXFS = query.FieldByName("XXFS").AsString;
            item.sYYAH = query.FieldByName("YYAH").AsString;
            item.sCXXX = query.FieldByName("CXXX").AsString;
            item.iCANSMS = query.FieldByName("CANSMS").AsInteger;
            item.iXQID = query.FieldByName("XQID").AsInteger;
            item.sXQMC = query.FieldByName("XQMC").AsString;
            item.sSW = query.FieldByName("SW").AsString;//三围
            item.sXZ = query.FieldByName("XZ").AsString;//星座
            item.sSX = query.FieldByName("SX").AsString;//属相
            item.sCM = query.FieldByName("CM").AsString;//尺码
            item.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
            return item;
        }

        private HYKGL_DALR SearchDataZY(string vgkid)
        {
            HYKGL_DALR item = new HYKGL_DALR();
            string msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select H.HYK_NO, G.*,D.PERSON_NAME AS TJRYMC,(select NR from HYXXXMDEF where XMID=G.ZJLXID) ZJLXMC ";
                    query.SQL.Add("     from HYK_GKDA G,HYK_HYXX H,RYXX D where G.GKID=H.GKID (+) AND G.TJRYID=D.PERSON_ID(+) ");

                    if (vgkid != "")
                        query.SQL.AddLine("  and G.GKID=" + vgkid);

                    query.Open();
                    if (!query.IsEmpty)
                    {


                        item.iJLBH = query.FieldByName("GKID").AsInteger;
                        item.HYKXX = new HYXX_Detail();
                        item.HYKXX.iHYID = query.FieldByName("GKID").AsInteger;
                        item.HYKXX.iZJLXID = query.FieldByName("ZJLXID").AsInteger;
                        item.HYKXX.iBJ_YZZJLX = query.FieldByName("BJ_YZZJLX").AsInteger;//验证 身份证标记
                        item.HYKXX.sSFZBH = query.FieldByName("SFZBH").AsString;
                        item.HYKXX.sHY_NAME = query.FieldByName("GK_NAME").AsString;
                        item.HYKXX.iTJRYID = query.FieldByName("TJRYID").AsInteger;//推荐人
                        item.HYKXX.sSJHM = query.FieldByName("SJHM").AsString;
                        item.HYKXX.sTXDZ = query.FieldByName("TXDZ").AsString;
                        item.HYKXX.sWX = query.FieldByName("WX").AsString;

                    }
                    query.Close();

                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }

            return item;

        }

        public void Set_GKDA_CHANGGE(CyQuery query, Int32 gkid, string oldvalue, string newvalue, string valname)
        {
            if (oldvalue == newvalue) { return; }
            else
            {
                query.SQL.Clear();
                query.Params.Clear();
                string tp_sql = "insert into HYK_GKDA_CHANGE";
                tp_sql += "(GKID,JLBH,OLDVAL,NEWVAL,VALNAME,DJSJ,DJR,DJRMC)";
                tp_sql += "values";
                tp_sql += "(:GKID,:JLBH,:OLDVAL,:NEWVAL,:VALNAME,sysdate,:DJR,:DJRMC)";

                query.SQL.Text = tp_sql;
                query.ParamByName("GKID").AsInteger = gkid;
                int tp_change = SeqGenerator.GetSeq("HYK_GKDA_CHANGE");
                query.ParamByName("JLBH").AsInteger = tp_change;
                query.ParamByName("OLDVAL").AsString = oldvalue;
                query.ParamByName("NEWVAL").AsString = newvalue;
                query.ParamByName("VALNAME").AsString = valname;
                query.ParamByName("DJR").AsInteger = iLoginRYID;
                query.ParamByName("DJRMC").AsString = sLoginRYMC;
                query.ExecSQL();
            }
        }

    }
}