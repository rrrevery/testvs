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
using BF.CrmProc.MZKGL;
using BF.CrmProc.GTPT;


namespace BF.CrmProc.HYKGL
{
    public class HYKGL_CXKXX : BASECRMClass
    {
        public int iHF = 0;
        public int iZT = 0;
        public int iBJ_CZK = 0;
        public int iBJ_TS = 0;
        public int iBJ_QKGX = 0;
        public string dialogName = string.Empty;
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            switch (dialogName)
            {
                case "ListKCK":
                    CondDict.Add("sBGDDDM", "H.BGDDDM");
                    CondDict.Add("iHYKTYPE", "H.HYKTYPE");
                    CondDict.Add("sKSKH", "H.CZKHM");
                    CondDict.Add("sJSKH", "H.CZKHM");
                    CondDict.Add("iSL", "ROWNUM");

                    if (iHF == 0)
                    {
                        query.SQL.Text = "select H.*,D.HYKNAME,B.BGDDMC from HYKCARD H,HYKDEF D,HYK_BGDD B ";
                        query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM");
                        if (iBJ_CZK == 1)
                            query.SQL.Text = query.SQL.Text.Replace("HYKCARD", "MZKCARD");
                    }
                    else
                    {
                        query.SQL.Text = " select H.*,D.HYKNAME,B.BGDDMC from HYKCARD_BAK H,HYKDEF D, HYK_BGDD B ";
                        query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM");
                        if (iBJ_CZK == 1)
                            query.SQL.Text = query.SQL.Text.Replace("HYKCARD_BAK", "MZKCARD_BAK");
                    }
                    break;
                case "ListHYK":
                    CondDict.Add("sKSKH", "H.HYK_NO");
                    CondDict.Add("sJSKH", "H.HYK_NO");
                    CondDict.Add("iHYKTYPE", "H.HYKTYPE");
                    CondDict.Add("iSL", "ROWNUM");
                    CondDict.Add("dDJSJ", "H.DJSJ");
                    CondDict.Add("iSEX", "G.SEX");

                    query.SQL.Text = "select HYID,HYK_NO,HY_NAME,HYKTYPE,HYKNAME,STATUS,BJ_CHILD,YXQ,LJXFJE,WCLJF,QCYE,YE,FXDW,MDID,MDMC,FXDWMC,DJSJ,SEX,SJHM";
                    query.SQL.Add(" from (");
                    query.SQL.Add(" select H.HYID,H.HYK_NO,H.HY_NAME,H.HYKTYPE,D.HYKNAME,H.STATUS,0 as BJ_CHILD,H.YXQ,H.DJSJ,");
                    query.SQL.Add(" nvl(F.LJXFJE,0) LJXFJE,nvl(F.WCLJF,0) WCLJF,E.QCYE,E.YE,H.FXDW,H.MDID,M.MDMC,B.FXDWMC,G.SEX,G.SJHM");
                    query.SQL.Add(" from HYK_HYXX H,HYKDEF D,HYK_JFZH F,HYK_JEZH E,MDDY M,FXDWDEF B,HYK_GKDA G");
                    query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE and  H.HYID=F.HYID(+) and H.HYID=E.HYID(+) and H.MDID=M.MDID(+)  and H.FXDW=B.FXDWID and H.GKID=G.GKID");
                    query.SQL.Add(" union");
                    query.SQL.Add(" select C.HYID,C.HYK_NO,C.HY_NAME,H.HYKTYPE,D.HYKNAME,C.STATUS,1 as BJ_CHILD,H.YXQ,H.DJSJ,");
                    query.SQL.Add(" nvl(F.LJXFJE,0) LJXFJE,nvl(F.WCLJF,0) WCLJF,E.QCYE,E.YE,C.FXDW,C.MDID,M.MDMC,B.FXDWMC,G.SEX,G.SJHM");
                    query.SQL.Add(" from HYK_HYXX H,HYKDEF D,HYK_CHILD_JL C,HYK_JFZH F,HYK_JEZH E,MDDY M,FXDWDEF B,HYK_GKDA G");
                    query.SQL.Add(" where H.HYID = C.HYID and H.HYKTYPE = D.HYKTYPE and H.HYID=F.HYID(+) and H.HYID=E.HYID(+) and C.MDID=M.MDID(+)  and C.FXDW=B.FXDWID and H.GKID=G.GKID");
                    query.SQL.Add(" ) H where 1 = 1");
                    if (iZT == 1)
                        query.SQL.Add("and STATUS not in (-1,-2,-3)");
                    if (iZT == 2)
                        query.SQL.Add("and STATUS >=0");
                    break;

                case "ListWXHYK":
                    CondDict.Add("iHYID", "H.HYID");
                    CondDict.Add("sKSKH", "H.HYK_NO");
                    CondDict.Add("sJSKH", "H.HYK_NO");
                    CondDict.Add("iHYKTYPE", "H.HYKTYPE");
                    CondDict.Add("iSL", "ROWNUM");
                    CondDict.Add("dDJSJ", "H.DJSJ");
                    CondDict.Add("iSEX", "G.SEX");
                    CondDict.Add("sHY_NAME", "H.HY_NAME");
                    CondDict.Add("iZYID", "G.ZYID");
                    CondDict.Add("iPUBLICID", "U.PUBLICID");

                    query.SQL.Text = " select H.HYID,H.HYK_NO,H.HY_NAME,H.HYKTYPE,H.STATUS,H.YXQ,H.DJSJ,U.OPENID,G.SEX,G.SJHM,D.HYKNAME from HYK_HYXX H,WX_UNION U,WX_HYKHYXX Y,HYK_GKDA G,HYKDEF D";
                    query.SQL.Add("  where H.HYID=Y.HYID and Y.UNIONID=U.UNIONID and H.GKID=G.GKID and H.HYKTYPE=D.HYKTYPE ");
                    if (iZT == 2)
                        query.SQL.Add("and H.STATUS >=0");
                    break;
                case "ListYHQZH":
                    CondDict.Add("iYHQID", "YHQID");
                    CondDict.Add("iCXID", "CXID");
                    CondDict.Add("dJSRQ", "JSRQ");
                    CondDict.Add("iHYKTYPE", "HYKTYPE");
                    CondDict.Add("sKSKH", "HYK_NO");
                    CondDict.Add("sJSKH", "HYK_NO");
                    CondDict.Add("iHYID", "HYID");
                    query.SQL.Text = "  select * from (";
                    query.SQL.Add("  select H.HYKTYPE, Z.HYID,Z.YHQID,Z.CXID,Z.MDFWDM,'集团' MDFWMC,Z.JE,Z.JSRQ,Q.YHQMC,H.HYK_NO,H.HY_NAME,C.CXZT");
                    query.SQL.Add("  from HYK_YHQZH Z ,YHQDEF Q,HYK_HYXX H ,CXHDDEF C");
                    query.SQL.Add("  where Z.YHQID=Q.YHQID  and Z.HYID=H.HYID and Z.CXID=C.CXID and Q.FS_YQMDFW=1");
                    query.SQL.Add("  union");
                    query.SQL.Add("  select  H.HYKTYPE,Z.HYID,Z.YHQID,Z.CXID,Z.MDFWDM,S.SHMC MDFWMC,Z.JE,Z.JSRQ,Q.YHQMC,H.HYK_NO,H.HY_NAME,C.CXZT");
                    query.SQL.Add("  from HYK_YHQZH Z ,YHQDEF Q,HYK_HYXX H,CXHDDEF C,SHDY S ");
                    query.SQL.Add("  where Z.YHQID=Q.YHQID  and Z.HYID=H.HYID and Z.CXID=C.CXID and Z.MDFWDM=S.SHDM and Q.FS_YQMDFW=2");
                    query.SQL.Add("  union");
                    query.SQL.Add("  select  H.HYKTYPE, Z.HYID,Z.YHQID,Z.CXID,Z.MDFWDM,S.MDMC MDFWMC,Z.JE,Z.JSRQ,Q.YHQMC,H.HYK_NO,H.HY_NAME,C.CXZT");
                    query.SQL.Add("  from HYK_YHQZH Z ,YHQDEF Q,HYK_HYXX H,CXHDDEF C,MDDY S");
                    query.SQL.Add("  where Z.YHQID=Q.YHQID  and Z.HYID=H.HYID and Z.CXID=C.CXID and Z.MDFWDM=S.MDDM  and Q.FS_YQMDFW=3");
                    query.SQL.Add(" ) where 1=1 and JE>0");
                    break;
                //此文件添加查询卡相关的功能
                case "ListCZTHYK":
                    CondDict.Add("sSFZBH", "G.SFZBH");
                    CondDict.Add("sHYKNO", "H.HYK_NO");
                    CondDict.Add("sHYKNO_OLD", "H.OLD_HYKNO");
                    CondDict.Add("sHY_NAME", "H.HY_NAME");
                    CondDict.Add("sSJHM", "G.SJHM");
                    query.SQL.Text = " select H.HYID,H.HYK_NO,H.HYKTYPE,H.HY_NAME,G.SJHM,G.SFZBH,F.HYKNAME,D.FXDWMC,H.STATUS ";
                    query.SQL.Add("  from HYK_HYXX H,HYK_GKDA G,HYKDEF F,FXDWDEF D   where H.GKID=G.GKID and H.HYKTYPE=F.HYKTYPE and H.FXDW=D.FXDWID");
                    break;
                case "ListWXUSER":

                    CondDict.Add("dDJSJ", "U.DJSJ");
                    CondDict.Add("sNICKNAME", ",U.sNICKNAME");
                    string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
                    query.SQL.Text = " select U.*,H.HYKTYPE,H.HYKNAME,W.HYK_NO from  HYK_HYXX  W,HYKDEF H,WX_USER U";

                    if (DbSystemName == "ORACLE")
                    {
                        query.SQL.Add(" where  U.OPENID= W.OPENID(+) ");
                        query.SQL.Add("  and W.HYKTYPE=H.HYKTYPE(+) ");
                    }
                    else if (DbSystemName == "SYBASE")
                    {
                        query.SQL.Add(" where  U.OPENID*=W.OPENID ");
                        query.SQL.Add("  and W.HYKTYPE*=H.HYKTYPE ");
                    }
                    query.SQL.Add("  and   U.OPENID IS NOT NULL ");
                    query.SQL.Add("  and (W.STATUS>=0 or W.STATUS is null) ");
                    break;
                case "ListZKXX":
                    CondDict.Add("iJLBH", "W.JLBH");
                    CondDict.Add("iBJ_XTZK", "D.BJ_XTZK");
                    if (iBJ_CZK == 1)
                    {
                        query.SQL.Text = "select W.* from MZK_JKJLITEM W,MZK_JKJL L,HYKDEF D where W.JLBH=L.JLBH and L.HYKTYPE=D.HYKTYPE and  W.BJ_ZK=0";
                    }
                    else
                    {
                        query.SQL.Text = "select W.* from HYKJKJLITEM W,HYKJKJL L,HYKDEF D where W.JLBH=L.JLBH and  L.HYKTYPE=D.HYKTYPE and W.BJ_ZK=0";
                    }
                    break;
                case "ListMZK":
                    CondDict.Add("sKSKH", "H.HYK_NO");
                    CondDict.Add("sJSKH", "H.HYK_NO");
                    CondDict.Add("iHYKTYPE", "H.HYKTYPE");
                    CondDict.Add("iSL", "ROWNUM");
                    CondDict.Add("sHYK_NO", "H.HYK_NO");
                    query.SQL.Text = "select H.*,D.HYKNAME,J.YE,J.QCYE from MZKXX H,HYKDEF D,MZK_KHDA K,MZK_JEZH J where H.HYKTYPE=D.HYKTYPE and D.BJ_CZK=1 and H.KHID=K.KHID(+)";
                    query.SQL.Add(" and H.HYID = J.HYID");
                    if (iZT == 1)
                        query.SQL.Add(" and H.STATUS>=0");
                    break;
                case "ListMZKSKD":
                    CondDict.Add("iSTATUS", "L.STATUS");
                    CondDict.Add("iKHID", "L.KHID");
                    if (iBJ_TS == 1)
                    {
                        query.SQL.Text = " select L.JLBH,L.SKSL,L.SSJE,L.YSZE,L.ZY,L.DJRMC,L.DJSJ,L.ZXRMC,L.ZXRQ,A.KHMC,A.KHID,I.YXQTS, I.YHQLX,Y.YHQMC,L.SJZSJF,L.SJZSJE";
                        query.SQL.Add("  from  MZK_SKJL L,MZK_KHDA A, MZK_SKJLZKITEM I,YHQDEF Y");
                        query.SQL.Add("  where L.KHID=A.KHID(+) and L.JLBH=I.JLBH(+) and I.YHQLX=Y.YHQID(+) AND L.FS=1");
                    }
                    else
                    {
                        query.SQL.Text = "select L.JLBH,L.SKSL,L.SSJE,L.ZY,L.DJRMC,L.DJSJ,L.ZXRMC,L.ZXRQ,A.KHMC,A.KHID ,L.YSZE";
                        if (iBJ_QKGX != 0)
                        {
                            query.SQL.Add(" ,(SELECT sum(C.JE) FROM  MZK_SKJLSKMX  C ,ZFFS Z where C.JLBH=L.JLBH  and  C.ZFFSID=Z.ZFFSID and Z.TYPE=10) DHJE ");
                        }
                        query.SQL.Add(" from MZK_SKJL L,MZK_KHDA A");
                        query.SQL.Add(" where  A.KHID=L.KHID(+) and L.FS=1 ");

                    }
                    if (iBJ_QKGX != 0)
                    {
                        query.SQL.Add("  and L.JLBH in (select JLBH from MZK_SKJLSKMX C,ZFFS Z where C.ZFFSID=Z.ZFFSID and Z.TYPE=10)  ");
                    }
                    break;
                case "ListMZKSKMX":
                    CondDict.Add("iSKJLBH", "I.JLBH");
                    CondDict.Add("sHYK_NO", "H.HYK_NO");
                    query.SQL.Text = "select I.JLBH,I.PDJE, H.HYID,H.HYKTYPE,H.HYK_NO,F.HYKNAME,I.QCYE,H.YXQ from MZK_SKJLITEM I, MZKXX H,HYKDEF F  ";
                    query.SQL.Add("  where H.HYID=I.HYID and F.HYKTYPE=I.HYKTYPE");
                    break;
            }

            SetSearchQuery(query, lst);
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            switch (dialogName)
            {
                case "ListKCK":
                    KCKXX item_KCK = new KCKXX();
                    item_KCK.sCZKHM = query.FieldByName("CZKHM").AsString;
                    item_KCK.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_KCK.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_KCK.iSTATUS = query.FieldByName("STATUS").AsInteger;
                    item_KCK.fQCYE = query.FieldByName("QCYE").AsFloat;
                    item_KCK.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                    item_KCK.fPDJE = query.FieldByName("PDJE").AsFloat;
                    item_KCK.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                    item_KCK.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                    item_KCK.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                    item_KCK.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
                    return item_KCK;

                case "ListHYK":
                    HYXX_Detail item_hyk = new HYXX_Detail();
                    item_hyk.iHYID = query.FieldByName("HYID").AsInteger;
                    item_hyk.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item_hyk.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_hyk.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_hyk.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item_hyk.iSTATUS = query.FieldByName("STATUS").AsInteger;
                    item_hyk.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
                    item_hyk.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    item_hyk.fLJXFJE = query.FieldByName("LJXFJE").AsFloat;
                    item_hyk.fQCYE = query.FieldByName("QCYE").AsFloat;
                    item_hyk.fCZJE = query.FieldByName("YE").AsFloat;
                    item_hyk.sMDMC = query.FieldByName("MDMC").AsString;
                    item_hyk.iMDID = query.FieldByName("MDID").AsInteger;
                    item_hyk.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                    item_hyk.iFXDW = query.FieldByName("FXDW").AsInteger;
                    item_hyk.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                    item_hyk.iSEX = query.FieldByName("SEX").AsInteger;
                    item_hyk.sSJHM = query.FieldByName("SJHM").AsString;
                    return item_hyk;

                case "ListWXHYK":
                    HYXX_Detail item_WXhyk = new HYXX_Detail();
                    item_WXhyk.iHYID = query.FieldByName("HYID").AsInteger;
                    item_WXhyk.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item_WXhyk.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_WXhyk.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_WXhyk.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item_WXhyk.iSTATUS = query.FieldByName("STATUS").AsInteger;
                    //item_WXhyk.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
                    //item_WXhyk.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    //item_WXhyk.fLJXFJE = query.FieldByName("LJXFJE").AsFloat;
                    //item_WXhyk.fQCYE = query.FieldByName("QCYE").AsFloat;
                    //item_WXhyk.fCZJE = query.FieldByName("YE").AsFloat;
                    //item_WXhyk.sMDMC = query.FieldByName("MDMC").AsString;
                    //item_WXhyk.iMDID = query.FieldByName("MDID").AsInteger;
                    //item_WXhyk.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                    //item_WXhyk.iFXDW = query.FieldByName("FXDW").AsInteger;
                    item_WXhyk.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                    item_WXhyk.iSEX = query.FieldByName("SEX").AsInteger;
                    item_WXhyk.sSJHM = query.FieldByName("SJHM").AsString;
                    item_WXhyk.sOPENID = query.FieldByName("OPENID").AsString;

                    return item_WXhyk;
                case "ListYHQZH":
                    YHQZH item_yqhzh = new YHQZH();
                    item_yqhzh.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item_yqhzh.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item_yqhzh.iHYID = query.FieldByName("HYID").AsInteger;
                    item_yqhzh.iYHQID = query.FieldByName("YHQID").AsInteger;
                    item_yqhzh.sYHQMC = query.FieldByName("YHQMC").AsString;
                    item_yqhzh.iCXID = query.FieldByName("CXID").AsInteger;
                    item_yqhzh.sCXZT = query.FieldByName("CXZT").AsString;
                    item_yqhzh.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                    item_yqhzh.sMDFWMC = query.FieldByName("MDFWMC").AsString;
                    item_yqhzh.fJE = query.FieldByName("JE").AsFloat;
                    item_yqhzh.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_yqhzh.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                    return item_yqhzh;

                case "ListCZTHYK":
                    HYXX_Detail item_czthyxx = new HYXX_Detail();
                    item_czthyxx.iHYID = query.FieldByName("HYID").AsInteger;
                    item_czthyxx.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item_czthyxx.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item_czthyxx.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_czthyxx.sSJHM = query.FieldByName("SJHM").AsString;
                    item_czthyxx.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_czthyxx.sSFZBH = query.FieldByName("SFZBH").AsString;
                    item_czthyxx.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                    item_czthyxx.iSTATUS = query.FieldByName("STATUS").AsInteger;
                    return item_czthyxx;

                case "ListWXUSER":
                    GTPT_WXUser_Proc item_wxuser = new GTPT_WXUser_Proc();
                    item_wxuser.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                    item_wxuser.sOPENID = query.FieldByName("OPENID").AsString;
                    //item_wxuser.sHEADIMGURL = query.FieldByName("HEADIMGURL").AsString;
                    item_wxuser.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_wxuser.sHYKNO = query.FieldByName("HYK_NO").AsString;
                    //item_wxuser.sNICKNAME = query.FieldByName("NICKNAME").AsString;
                    item_wxuser.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_wxuser.sWX_NO = query.FieldByName("WX_NO").AsString;
                    return item_wxuser;
                case "ListZKXX":
                    HYKGL_HYKJK.HYKGL_HYKJKItem item_zkxx = new HYKGL_HYKJK.HYKGL_HYKJKItem();
                    item_zkxx.sCZKHM = query.FieldByName("CZKHM").AsString;
                    item_zkxx.sCDNR = query.FieldByName("CDNR").AsString;
                    item_zkxx.sCDNR = CrmLibProc.DecCDNR(item_zkxx.sCDNR);
                    return item_zkxx;
                case "ListMZK":
                    HYXX_Detail item_mzk = new HYXX_Detail();
                    item_mzk.iHYID = query.FieldByName("HYID").AsInteger;
                    item_mzk.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item_mzk.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                    item_mzk.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_mzk.fYE = query.FieldByName("YE").AsFloat;
                    item_mzk.iSTATUS = query.FieldByName("STATUS").AsInteger;
                    item_mzk.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_mzk.fQCYE = query.FieldByName("QCYE").AsFloat;
                    return item_mzk;
                case "ListMZKSKD":
                    MZKSKD_Detail item_mzkskd = new MZKSKD_Detail();
                    item_mzkskd.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item_mzkskd.iSKSL = query.FieldByName("SKSL").AsInteger;
                    item_mzkskd.cSSJE = query.FieldByName("YSZE").AsCurrency;
                    item_mzkskd.sZY = query.FieldByName("ZY").AsString;
                    item_mzkskd.sDJRMC = query.FieldByName("DJRMC").AsString;
                    item_mzkskd.sZXRMC = query.FieldByName("ZXRMC").AsString;
                    item_mzkskd.sKHMC = query.FieldByName("KHMC").AsString;
                    item_mzkskd.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                    item_mzkskd.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                    item_mzkskd.iKHID = query.FieldByName("KHID").AsInteger;
                    if (iBJ_TS == 1)
                    {
                        item_mzkskd.iYHQID = query.FieldByName("YHQLX").AsInteger;
                        item_mzkskd.sYHQMC = query.FieldByName("YHQMC").AsString;
                        item_mzkskd.fSJZSJE = query.FieldByName("SJZSJE").AsFloat;
                        item_mzkskd.fSJZSJF = query.FieldByName("SJZSJF").AsFloat;
                        item_mzkskd.iYXQTS = query.FieldByName("YXQTS").AsInteger;

                    }
                    if (iBJ_QKGX != 0)
                    {
                        item_mzkskd.fDHJE = query.FieldByName("DHJE").AsFloat;
                    }
                    return item_mzkskd;
                case "ListMZKSKMX":
                    MZKGL.MZKGL_MZKTS.MZKGL_MZKTSKMXITEM item_mzkskmx = new MZKGL.MZKGL_MZKTS.MZKGL_MZKTSKMXITEM();
                    item_mzkskmx.iSKDJLBH = query.FieldByName("JLBH").AsInteger;
                    item_mzkskmx.iHYID = query.FieldByName("HYID").AsInteger;
                    item_mzkskmx.fPDJE = query.FieldByName("PDJE").AsFloat;
                    item_mzkskmx.sCZKHM = query.FieldByName("HYK_NO").AsString;
                    item_mzkskmx.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_mzkskmx.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_mzkskmx.fQCYE = query.FieldByName("QCYE").AsFloat;
                    item_mzkskmx.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                    return item_mzkskmx;
                default:
                    return null;

            }

        }

    }
}
