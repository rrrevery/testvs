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
    public class HYKGL_BQHYCX : HYXX_Detail
    {
        //公开显示信息
        public bool bShowPublic = false;
        public int iBJ_TRANS = 0;
        public string sBJ_TRANSSTR = string.Empty;
        public double fQZ = 0;
        public string sLABELXMMC = string.Empty, sLABELVALUE = string.Empty;

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "A.MDID");
            CondDict.Add("sMDMC", "C.MDMC");
            CondDict.Add("iHYID", "A.HYID");
            CondDict.Add("sHYKNO", "A.HYK_NO");
            CondDict.Add("sHY_NAME", "A.HY_NAME");
            CondDict.Add("iSEX", "B.SEX");
            CondDict.Add("dCSRQ", "B.CSRQ");
            CondDict.Add("iHYKTYPE", "A.HYKTYPE");
            CondDict.Add("sHYKNAME", "D.HYKNAME");
            CondDict.Add("sSJHM", "B.SJHM");
            CondDict.Add("iZJLXID", "B.ZJLXID");
            CondDict.Add("sSFZBH", "B.SFZBH");
            CondDict.Add("SR", "TO_CHAR(B.CSRQ,'MM-dd')");
            CondDict.Add("iAGE", "to_number(to_char(sysdate,'yyyy'))-to_number(to_char(B.CSRQ,'yyyy'))");
            CondDict.Add("dJKRQ", "A.JKRQ");
            CondDict.Add("dYXQ", "A.YXQ");
            CondDict.Add("iSTATUS", "A.STATUS");
            CondDict.Add("fWCLJF", "J.WCLJF");
            CondDict.Add("fLJJF", "J.LJJF");
            CondDict.Add("fLJXFJE", "J.LJXFJE");
            CondDict.Add("fBQJF", "J.BQJF");
            CondDict.Add("sCANVAS", "B.CANVAS");
            CondDict.Add("sSHDM", "M.SHDM");
            CondDict.Add("iXH", "X.LABELID");
            CondDict.Add("iBJ_TRANS", "W.BJ_TRANS");
            CondDict.Add("fQZ", "W.QZ");

            query.SQL.Text = " select  W.QZ,W.BJ_TRANS, W.HYID,A.MDID,M.MDMC ,A.HYK_NO,A.HY_NAME,B.SEX, ";
            query.SQL.Add("   B.CSRQ,A.HYKTYPE,F.HYKNAME,J.WCLJF,J.BQJF,J.LJJF,B.ZJLXID,B.SFZBH,B.SJHM,");
            query.SQL.Add("  A.JKRQ,A.YXQ,A.STATUS,B.PHONE,B.QQ,B.WX,B.WB,B.E_MAIL,B.YZBM,B.TXDZ,X.LABEL_VALUE,L.LABELXMMC");
            query.SQL.Add("   from  HYK_HYBQ W,HYK_HYXX A,HYK_GKDA B,HYK_JFZH J,MDDY M,LABEL_XMITEM X,LABEL_XM L,HYKDEF F");
            query.SQL.Add("  where W.HYID=A.HYID and A.GKID=B.GKID and W.HYID=J.HYID(+) and A.MDID=M.MDID");
            query.SQL.Add("  and W.LABELXMID=X.LABELXMID and W.LABEL_VALUEID=X.LABEL_VALUEID ");
            query.SQL.Add("  and X.LABELXMID=L.LABELXMID and A.HYKTYPE=F.HYKTYPE");
            SetSearchQuery(query, lst);
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_BQHYCX entity = new HYKGL_BQHYCX();
            entity.iMDID = query.FieldByName("MDID").AsInteger;
            entity.sMDMC = query.FieldByName("MDMC").AsString;
            entity.iHYID = query.FieldByName("HYID").AsInteger;
            entity.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            entity.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            entity.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
            entity.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
            entity.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            entity.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            if (bShowPublic == false)
            {
                entity.sSJHM = CrmLibProc.MakePrivateNumber(query.FieldByName("SJHM").AsString);
                entity.sSFZBH = CrmLibProc.MakePrivateNumber(query.FieldByName("SFZBH").AsString);
            }
            else
            {
                entity.sSJHM = query.FieldByName("SJHM").AsString;
                entity.sSFZBH = query.FieldByName("SFZBH").AsString;
            }
            entity.iZJLXID = query.FieldByName("ZJLXID").AsInteger;
            entity.dJKRQ = FormatUtils.DateToString(query.FieldByName("JKRQ").AsDateTime);
            entity.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            entity.iSTATUS = query.FieldByName("STATUS").AsInteger;
            entity.fWCLJF = query.FieldByName("WCLJF").AsFloat;
            entity.fBQJF = query.FieldByName("BQJF").AsFloat;
            entity.fLJJF = query.FieldByName("LJJF").AsFloat;
            entity.sQQ = query.FieldByName("QQ").AsString;
            entity.sWB = query.FieldByName("WB").AsString;
            entity.sEMAIL = query.FieldByName("E_MAIL").AsString;
            entity.sYZBM = query.FieldByName("YZBM").AsString;
            entity.sTXDZ = query.FieldByName("TXDZ").AsString;
            entity.sLABELVALUE = query.FieldByName("LABEL_VALUE").AsString;
            entity.sLABELXMMC = query.FieldByName("LABELXMMC").AsString;
            entity.iBJ_TRANS = query.FieldByName("BJ_TRANS").AsInteger;
            switch (entity.iBJ_TRANS)
            {
                case 0:
                    entity.sBJ_TRANSSTR = "消费标签";
                    break;
                case 1:
                    entity.sBJ_TRANSSTR = "手工标签";
                    break;
                case 2:
                    entity.sBJ_TRANSSTR = "导入标签";
                    break;
            }

            //if (bShowPublic == false)
            //{
            //    if (CrmLibProc.CheckListCount(lst))
            //        break;
            //}
            return entity;
        }
    }
}
