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
    public class HYKGL_YHQYXQ : DJLR_ZX_CLass
    {
        public string dXYXQ = string.Empty;
        public string sMDMC = string.Empty;
        public int iMDID;
        public int iYHQID;
        public string sYHQMC = string.Empty;
        public string sHYKNO = string.Empty;
        public string dJSSJ1 = string.Empty;
        public string dJSSJ2 = string.Empty;
        public int iCXID = -1;
        public string sCXZT = string.Empty;
        public List<HYKGL_YHQYXQItem> itemTable = new List<HYKGL_YHQYXQItem>();

        public class HYKGL_YHQYXQItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public int iYHQID = 0;
            public string sYHQMC = string.Empty;
            public string sMDFWDM = string.Empty;
            public int iCXID = 0;
            public string sCXZT = string.Empty;
            public string dYYXQ = string.Empty;
            public string dJSRQ = string.Empty;
            public double fJE = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "YHQ_YXQBDJL;YHQ_YXQBDJLITEM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("YHQ_YXQBDJL");
            query.SQL.Text = "insert into YHQ_YXQBDJL(JLBH,ZY,XYXQ,MDID,DJR,DJRMC,DJSJ,CXID)";//,CZDD
            query.SQL.Add(" values(:JLBH,:ZY,:XYXQ,:MDID,:DJR,:DJRMC,:DJSJ,:CXID)");//,:CZDD
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("XYXQ").AsDateTime = FormatUtils.ParseDateString(dXYXQ);
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            //query.ParamByName("JSSJ1").AsDateTime = Convert.ToDateTime(dJSSJ1);
            //query.ParamByName("JSSJ2").AsDateTime = Convert.ToDateTime(dJSSJ2);
            query.ParamByName("CXID").AsInteger = iCXID;
            query.ExecSQL();
            foreach (HYKGL_YHQYXQItem one in itemTable)
            {
                query.SQL.Text = "insert into YHQ_YXQBDJLITEM(JLBH,HYID,YHQID,MDFWDM,YYXQ,JE)";
                query.SQL.Add(" values(:JLBH,:HYID,:YHQID,:MDFWDM,:YYXQ,:JE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("YHQID").AsInteger = one.iYHQID;
                query.ParamByName("MDFWDM").AsString = one.sMDFWDM; 
                query.ParamByName("YYXQ").AsDateTime = FormatUtils.ParseDateString(one.dJSRQ);
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "YHQ_YXQBDJL", serverTime, "JLBH");
            foreach (HYKGL_YHQYXQItem one in itemTable)
            {
                //CrmLibProc.UpdateYHQZH(out msg, query, one.iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iYHQID, iMDID, iCXID, one.fCKJE, dJSRQ, "优惠券批量存款");
                // query.Close();
                query.SQL.Text = "update HYK_YHQZH set JSRQ=:XYXQ where HYID=:HYID and YHQID=:YHQID and JSRQ=:YYXQ and MDFWDM=:MDFWDM ";
                if (iCXID >= 0)
                {
                    query.SQL.Add("  AND CXID=:CXID");
                    query.ParamByName("CXID").AsInteger = iCXID;
                }

                query.ParamByName("XYXQ").AsDateTime = FormatUtils.ParseDateString(dXYXQ);
                query.ParamByName("YYXQ").AsDateTime = FormatUtils.ParseDateString(one.dJSRQ);
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("YHQID").AsInteger = one.iYHQID;
                query.ParamByName("MDFWDM").AsString = one.sMDFWDM;
                query.ExecSQL();

                int tJYBH = SeqGenerator.GetSeq("HYK_YHQCLJL");
                query.SQL.Text = "insert into HYK_YHQCLJL(JYBH,HYID,CLSJ,CLLX,JLBH,YHQID,CXID,JSRQ,MDFWDM,MDID,ZY,JFJE,DFJE,YE)";
                query.SQL.Add(" select :JYBH,HYID," + CyDbSystem.GetDbServerTimeFuncSql(query.Connection) + ",:CLLX,:JLBH,YHQID,CXID,JSRQ,MDFWDM,:MDID,:ZY,round(:JFJE,2),round(:DFJE,2),JE");
                query.SQL.Add(" from HYK_YHQZH where HYID=:HYID and YHQID=:YHQID and JSRQ=:JSRQ and MDFWDM=:MDFWDM and CXID=:CXID");
                query.ParamByName("JYBH").AsInteger = tJYBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("YHQID").AsInteger = one.iYHQID;
                query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(one.dYYXQ);
                query.ParamByName("MDFWDM").AsString = one.sMDFWDM;
                query.ParamByName("CXID").AsInteger = one.iCXID;
                query.ParamByName("CLLX").AsInteger = BASECRMDefine.CZK_CLLX_BD;
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("MDID").AsInteger = CrmLibProc.GetMDIDByRY(iLoginRYID);
                query.ParamByName("ZY").AsString = "有效期变动";
                query.ParamByName("JFJE").AsFloat = 0;
                query.ParamByName("DFJE").AsFloat = 0;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("iDJR", "A.DJR");
            CondDict.Add("sDJRMC", "A.DJRMC");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("iZXR", "A.ZXR");
            CondDict.Add("sZXRMC", "A.ZXRMC");
            CondDict.Add("dZXRQ", "A.ZXRQ");
            CondDict.Add("sZY", "A.ZY");
            CondDict.Add("iMDID", "A.MDID");

            query.SQL.Text = "select A.*,M.MDMC,F.CXZT";
            query.SQL.Add(" from YHQ_YXQBDJL A,MDDY M,CXHDDEF F where A.MDID=M.MDID and A.CXID=F.CXID(+)");
            if (sHYKNO != "" && sHYKNO != null)
            {
                query.SQL.Add("  and A.JLBH in (select JLBH from YHQ_YXQBDJLITEM I,HYK_HYXX H where H.HYID=I.HYID AND  H.HYK_NO='" + sHYKNO + "' )");
            }
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select A.*,B.HYK_NO,C.YHQMC ";
                query.SQL.AddLine("from YHQ_YXQBDJLITEM A,HYK_HYXX B,YHQDEF C ");
                query.SQL.AddLine("where A.HYID=B.HYID and A.YHQID=C.YHQID and A.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_YHQYXQItem item = new HYKGL_YHQYXQItem();
                    ((HYKGL_YHQYXQ)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.iYHQID = query.FieldByName("YHQID").AsInteger;
                    item.sYHQMC = query.FieldByName("YHQMC").AsString;
                    item.dJSRQ = FormatUtils.DateToString(query.FieldByName("YYXQ").AsDateTime);
                    item.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                    item.fJE = query.FieldByName("JE").AsFloat;
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQYXQ item = new HYKGL_YHQYXQ();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.dXYXQ = FormatUtils.DateToString(query.FieldByName("XYXQ").AsDateTime);
            item.sZY = query.FieldByName("ZY").AsString;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.iMDID = query.FieldByName("MDID").AsInteger;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            //item.dJSSJ1 = FormatUtils.DateToString(query.FieldByName("JSSJ1").AsDateTime);
            //item.dJSSJ2 = FormatUtils.DateToString(query.FieldByName("JSSJ2").AsDateTime);
            item.sCXZT = query.FieldByName("CXZT").AsString;
            item.iCXID = query.FieldByName("CXID").AsInteger;
            return item;
        }
    }
}
