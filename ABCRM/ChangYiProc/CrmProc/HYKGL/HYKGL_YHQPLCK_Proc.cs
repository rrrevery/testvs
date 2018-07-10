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
    public class HYKGL_YHQPLCK : HYKYHQ_DJLR_CLass
    {
        public int iKHID = 0;
        //public int iMDID = -1;
        public string sHYKNO = string.Empty;
        public double fTYCKJE = 0;
        //  public string sMDMC = string.Empty;      
        public int iFS_YQMDFW;
        public int iBHKS = 0;
        public double fZCKJE = 0;
        public string sMDFWMC = string.Empty;
        public List<HYKGL_YHQPLCKItem> itemTable = new List<HYKGL_YHQPLCKItem>();

        public class HYKGL_YHQPLCKItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public double fCKJE = 0;
            public int iINX = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (HYKGL_YHQPLCKItem one in itemTable)
            {
                if (one.fCKJE <= 0)
                {
                    msg = CrmLibStrings.msgWrongCKJE;
                    return false;
                }
            }
            return true;
        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_YHQ_PLCKJL;HYK_YHQ_PLCKJLITEM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_YHQ_PLCKJL");
            query.SQL.Text = "insert into HYK_YHQ_PLCKJL(JLBH,KHID,YHQID,JSRQ,CXID,ZY,DJR,DJRMC,DJSJ,CZDD,MDFWDM,DJLX)";//,TYCKJE
            query.SQL.Add(" values(:JLBH,:KHID,:YHQID,:JSRQ,:CXID,:ZY,:DJR,:DJRMC,:DJSJ,:CZDD,:MDFWDM,:DJLX)");//,:TYCKJE
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KHID").AsInteger = iKHID;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("CXID").AsInteger = iCXID;
            query.ParamByName("MDFWDM").AsString = sMDFWDM;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            //query.ParamByName("TYCKJE").AsFloat = fTYCKJE;
            query.ExecSQL();
            foreach (HYKGL_YHQPLCKItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_YHQ_PLCKJLITEM(JLBH,HYID,CKJE,BZ,BJ_CHILD,HYKNO,INX,HYKTYPE)";
                query.SQL.Add(" values(:JLBH,:HYID,:CKJE,:BZ,:BJ_CHILD,:HYKNO,:INX,:HYKTYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("CKJE").AsFloat = one.fCKJE;
                query.ParamByName("HYKNO").AsString = one.sHYK_NO;
                query.ParamByName("BZ").AsString = sZY;
                query.ParamByName("BJ_CHILD").AsInteger = 0;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            int iMDID = CrmLibProc.MDDMToMDID(query, sMDFWDM);
            ExecTable(query, "HYK_YHQ_PLCKJL", serverTime, "JLBH");
            foreach (HYKGL_YHQPLCKItem one in itemTable)
            {
                CrmLibProc.UpdateYHQZH(out msg, query, one.iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iYHQID, iMDID, iCXID, one.fCKJE, dJSRQ, "优惠券批量存款", sMDFWDM);
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("sBGDDDM", "A.CZDD");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("iYHQID", "A.YHQID");
            CondDict.Add("dJSRQ", "A.JSRQ");
            CondDict.Add("iCXID", "A.CXID");
            CondDict.Add("sZY", "A.ZY");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("dZXRQ", "A.ZXRQ");
            CondDict.Add("sZXRMC", "A.ZXRMC");
            CondDict.Add("sDJRMC", "A.DJRMC");
            CondDict.Add("iMDID", "B.MDID");
            query.SQL.Text = "select M.JLBH,M.ZY,M.CZDD,M.YHQID,M.JSRQ,M.CXID,M.DJR,M.DJRMC,M.DJSJ,M.ZXR,M.ZXRMC,M.ZXRQ,M.BHKS,M.ZCKJE,M.FS_YQMDFW,M.YHQMC,M.CXZT,M.MDMC,M.BGDDMC,M.MDFWMC from(";
            query.SQL.Add("select A.*,D.YHQMC,E.CXZT,M.MDMC,B.BGDDMC,F.MDMC MDFWMC");
            query.SQL.Add("  ,(select COUNT(JLBH) BHKS FROM HYK_YHQ_PLCKJLITEM I where I.JLBH=A.JLBH) BHKS,");
            query.SQL.Add(" (select sum(CKJE) CKJE FROM HYK_YHQ_PLCKJLITEM I where I.JLBH=A.JLBH) ZCKJE ,D.FS_YQMDFW ");
            query.SQL.Add(" from HYK_YHQ_PLCKJL A,YHQDEF D,CXHDDEF E,HYK_BGDD B,MDDY M,MDDY F");
            query.SQL.Add(" where  A.YHQID=D.YHQID and A.CXID=E.CXID(+) ");
            query.SQL.Add("  and A.CZDD=B.BGDDDM and B.MDID=M.MDID and A.MDFWDM=F.MDDM(+) and D.FS_YQMDFW=3");
            if (sHYKNO != "" && sHYKNO != null)
            {
                query.SQL.Add("  and A.JLBH in (select JLBH from HYK_YHQ_PLCKJLITEM where HYKNO='" + sHYKNO + "' )");
            }

            MakeSrchCondition(query, "", false);
            query.SQL.Add(" union");
            query.SQL.Text += "  select A.*,D.YHQMC,E.CXZT,M.MDMC,B.BGDDMC,F.SHMC MDFWMC";
            query.SQL.Add("  ,(select COUNT(JLBH) BHKS FROM HYK_YHQ_PLCKJLITEM I where I.JLBH=A.JLBH) BHKS,");
            query.SQL.Add(" (select sum(CKJE) CKJE FROM HYK_YHQ_PLCKJLITEM I where I.JLBH=A.JLBH) ZCKJE ,D.FS_YQMDFW ");
            query.SQL.Add(" from HYK_YHQ_PLCKJL A,YHQDEF D,CXHDDEF E,HYK_BGDD B,MDDY M,SHDY F");
            query.SQL.Add(" where  A.YHQID=D.YHQID and A.CXID=E.CXID(+) ");
            query.SQL.Add("  and A.CZDD=B.BGDDDM and B.MDID=M.MDID and A.MDFWDM=F.SHDM(+) and D.FS_YQMDFW in(1,2)");
            if (sHYKNO != "" && sHYKNO != null)
            {
                query.SQL.Add("  and A.JLBH in (select JLBH from HYK_YHQ_PLCKJLITEM where HYKNO='" + sHYKNO + "' )");
            }
            MakeSrchCondition(query, "", false);
            query.SQL.Add(" )M");
            SetSearchQuery(query, lst, false);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,D.HYKNAME,H.HY_NAME from HYK_YHQ_PLCKJLITEM I,HYK_HYXX H,HYKDEF D where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.HYID=H.HYID and I.HYKTYPE=D.HYKTYPE");
                query.SQL.Add(" order by I.INX");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_YHQPLCKItem item = new HYKGL_YHQPLCKItem();
                    ((HYKGL_YHQPLCK)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYKNO").AsString;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.fCKJE = query.FieldByName("CKJE").AsFloat;
                    item.iINX = query.FieldByName("INX").AsInteger;
                    query.Next();
                }
            }
            query.Close();


            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQPLCK item = new HYKGL_YHQPLCK();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sZY = query.FieldByName("ZY").AsString;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iYHQID = query.FieldByName("YHQID").AsInteger;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.iCXID = query.FieldByName("CXID").AsInteger;
            item.sCXZT = query.FieldByName("CXZT").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            item.iBHKS = query.FieldByName("BHKS").AsInteger;
            item.fZCKJE = query.FieldByName("ZCKJE").AsFloat;
            item.iFS_YQMDFW = query.FieldByName("FS_YQMDFW").AsInteger;
            if (item.iFS_YQMDFW != 1)
            {
                item.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                item.sMDFWMC = query.FieldByName("MDFWMC").AsString;
            }
            else
            {
                item.sMDFWDM = " ";
                item.sMDFWMC = "集团";
            }
            return item;
        }
    }
}
