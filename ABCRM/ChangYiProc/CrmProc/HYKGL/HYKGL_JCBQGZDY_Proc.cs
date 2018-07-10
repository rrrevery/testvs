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
    public class HYKGL_JCBQGZDY : DJLR_CLass
    {
        public int iLABELID
        {
            get { return iJLBH; }
            set { iJLBH = value; }
        }
        public string sLABELVALUE = string.Empty;
        public int iYXYF = 0, iNEW_LABELID = 0;
        public string sNEW_LABELVALUE = string.Empty;

        public class HYKGL_JCBQGZDYItem
        {
            public int iSJLX = 0;
            public string sSJMC = string.Empty;
            public int iSJNR = 0;
            public string sSJDM = string.Empty;
        }
        public List<HYKGL_JCBQGZDYItem> itemTable = new List<HYKGL_JCBQGZDYItem>();

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "LABEL_BASE_GZITEM;LABEL_BASE_GZ", "LABELID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                 iJLBH = SeqGenerator.GetSeq("LABEL_BASE_GZ");
            query.SQL.Text = "insert into LABEL_BASE_GZ(LABELID,DJR,DJSJ,DJRMC,STATUS,YXYF,NEW_LABELID)";
            query.SQL.Add(" values(:JLBH,:DJR,:DJSJ,:DJRMC,:STATUS,:YXYF,:NEW_LABELID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("STATUS").AsInteger =iSTATUS;
            query.ParamByName("YXYF").AsInteger = iYXYF;
            query.ParamByName("NEW_LABELID").AsInteger = iNEW_LABELID;
            query.ExecSQL();
            foreach (HYKGL_JCBQGZDYItem one in itemTable)
            {
                query.SQL.Text = "insert into LABEL_BASE_GZITEM(LABELID,SJLX,SJNR)";
                query.SQL.Add(" values(:LABELID,:SJLX,:SJNR)");
                query.ParamByName("LABELID").AsInteger = iJLBH;
                query.ParamByName("SJLX").AsInteger = one.iSJLX;
                query.ParamByName("SJNR").AsInteger = one.iSJNR;
                query.ExecSQL();
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.LABELID");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iSTATUS", "W.STATUS");
            CondDict.Add("sLABELVALUE", "L.LABEL_VALUE");
            CondDict.Add("sNEW_LABELVALUE", "X.LABEL_VALUE");
            query.SQL.Text = "  select W.*,L.LABEL_VALUE,X.LABEL_VALUE NEW_LABELVALUE ";
            query.SQL.Add(" from LABEL_BASE_GZ W,LABEL_XMITEM L,LABEL_XMITEM X ");
            query.SQL.Add("  where W.LABELID=L.LABELID and  W.NEW_LABELID=X.LABELID(+)");
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "   SELECT I.*,S.SPMC SJMC,S.SPDM SJDM  FROM LABEL_BASE_GZITEM I,SHSPXX S WHERE I.SJNR=S.SHSPID AND I.SJLX=0";
                query.SQL.Add("  and I.LABELID=" + iJLBH);
                query.SQL.Add("  UNION");
                query.SQL.Add("   SELECT I.*,S.SPFLMC SJMC,S.SPFLDM SJDM FROM LABEL_BASE_GZITEM I,SHSPFL S WHERE I.SJNR=S.SHSPFLID AND I.SJLX=1");
                query.SQL.Add("  and I.LABELID=" + iJLBH);
                query.SQL.Add("  UNION");
                query.SQL.Add("   SELECT I.*,S.SBMC SJMC,S.SBDM SJDM FROM LABEL_BASE_GZITEM I,SHSPSB S WHERE I.SJNR=S.SHSBID AND I.SJLX=2");
                query.SQL.Add("  and I.LABELID=" + iJLBH);

                query.Open();
                while (!query.Eof)
                {
                    HYKGL_JCBQGZDYItem obj = new HYKGL_JCBQGZDYItem();
                    ((HYKGL_JCBQGZDY)lst[0]).itemTable.Add(obj);
                    obj.iSJLX = query.FieldByName("SJLX").AsInteger;
                    obj.iSJNR = query.FieldByName("SJNR").AsInteger;
                    obj.sSJMC = query.FieldByName("SJMC").AsString;
                    obj.sSJDM = query.FieldByName("SJDM").AsString;
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_JCBQGZDY obj = new HYKGL_JCBQGZDY();
            obj.iJLBH = query.FieldByName("LABELID").AsInteger;
            obj.iLABELID = query.FieldByName("LABELID").AsInteger;
            obj.sLABELVALUE = query.FieldByName("LABEL_VALUE").AsString;
            obj.iYXYF = query.FieldByName("YXYF").AsInteger;
            obj.iNEW_LABELID = query.FieldByName("NEW_LABELID").AsInteger;
            obj.sNEW_LABELVALUE = query.FieldByName("NEW_LABELVALUE").AsString;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            return obj;
        }

    }
}
