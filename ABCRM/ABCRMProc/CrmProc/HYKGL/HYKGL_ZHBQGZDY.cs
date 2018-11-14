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
    public class HYKGL_ZHBQGZDY : DJLR_CLass
    {
        public int iLABELID
        {
            get { return iJLBH; }
            set { iJLBH = value; }
        }
        public string sLABEL_VALUE = string.Empty;
        public int iYXYF = 0, iNEW_LABELID = 0, iTJSL = 0, iTJYF = 0;
        public string sNEW_LABELVALUE = string.Empty;

        public class HYKGL_ZHBQGZDYItem
        {
            public int iLABELID = 0;
            public int iSJNR = 0;
            public int iXH = 0;
            public string sXH = string.Empty;
            public string sLABELVALUE = string.Empty;

        }
        public List<HYKGL_ZHBQGZDYItem> itemTable = new List<HYKGL_ZHBQGZDYItem>();

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "LABEL_GZITEM;LABEL_GZ;", "LABELID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("LABEL_GZ");
            query.SQL.Text = "insert into LABEL_GZ(LABELID,DJR,DJSJ,DJRMC,STATUS,YXYF,NEW_LABELID,TJSL,TJYF)";
            query.SQL.Add(" values(:JLBH,:DJR,:DJSJ,:DJRMC,:STATUS,:YXYF,:NEW_LABELID,:TJSL,:TJYF)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("YXYF").AsInteger = iYXYF;
            query.ParamByName("NEW_LABELID").AsInteger = iNEW_LABELID;
            query.ParamByName("TJSL").AsInteger = iTJSL;
            query.ParamByName("TJYF").AsInteger = iTJYF;

            query.ExecSQL();
            foreach (HYKGL_ZHBQGZDYItem one in itemTable)
            {
                query.SQL.Text = "insert into LABEL_GZITEM(LABELID,XH,SJNR)";
                query.SQL.Add(" values(:LABELID,:XH,:SJNR)");
                query.ParamByName("LABELID").AsInteger = iJLBH;
                query.ParamByName("XH").AsInteger = one.iXH;
                query.ParamByName("SJNR").AsInteger = one.iSJNR;
                query.ExecSQL();
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.LABELID");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iTJSL", "W.TJSL");
            CondDict.Add("iTJYF", "W.TJYF");
            CondDict.Add("sLABEL_VALUE", "L.LABEL_VALUE");
            query.SQL.Text = "  select W.*,L.LABEL_VALUE,X.LABEL_VALUE NEW_LABELVALUE from LABEL_GZ W,LABEL_XMITEM L,LABEL_XMITEM X";
            query.SQL.Add("  where   W.LABELID=L.LABELID(+) and W.NEW_LABELID=X.LABELID(+) ");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "   SELECT I.*,M.LABEL_VALUE FROM LABEL_GZITEM I,LABEL_XMITEM M where I.SJNR=M.LABELID ";
                query.SQL.Add("  and I.LABELID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_ZHBQGZDYItem obj = new HYKGL_ZHBQGZDYItem();
                    ((HYKGL_ZHBQGZDY)lst[0]).itemTable.Add(obj);
                    obj.iXH = query.FieldByName("XH").AsInteger;
                    obj.sXH = "第" + (obj.iXH + 1) + "组";
                    obj.iSJNR = query.FieldByName("SJNR").AsInteger;
                    obj.sLABELVALUE = query.FieldByName("LABEL_VALUE").AsString;
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_ZHBQGZDY obj = new HYKGL_ZHBQGZDY();
            obj.iJLBH = query.FieldByName("LABELID").AsInteger;
            obj.iLABELID = query.FieldByName("LABELID").AsInteger;
            obj.sLABEL_VALUE = query.FieldByName("LABEL_VALUE").AsString;
            obj.iYXYF = query.FieldByName("YXYF").AsInteger;
            obj.iNEW_LABELID = query.FieldByName("NEW_LABELID").AsInteger;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.iNEW_LABELID = query.FieldByName("NEW_LABELID").AsInteger;
            obj.sNEW_LABELVALUE = query.FieldByName("NEW_LABELVALUE").AsString;
            obj.iTJSL = query.FieldByName("TJSL").AsInteger;
            obj.iTJYF = query.FieldByName("TJYF").AsInteger;
            return obj;
        }

    }
}
