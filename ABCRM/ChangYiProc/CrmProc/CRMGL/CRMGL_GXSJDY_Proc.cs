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

namespace BF.CrmProc.CRMGL
{
    public class CRMGL_GXSJDY : GXSJDY
    {
        public List<GXSJDY_MD_ITEM> itemTable = new List<GXSJDY_MD_ITEM>();
        public class GXSJDY_MD_ITEM
        {
            public string sMDMC = string.Empty;
            public int iQYID = 0;
            public int iMDID = 0;
            public int iTM = 0;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "QYDEF;QYDEF_MD;", "QYID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("QYDEF");
            query.SQL.Text = "insert into QYDEF(QYID,QYMC,QYLX,FTPBJ,STATUS)";
            query.SQL.Add(" values(:QYID,:QYMC,:QYLX,:FTPBJ,:STATUS)");
            query.ParamByName("QYID").AsInteger = iJLBH;
            query.ParamByName("QYMC").AsString = sQYMC;
            query.ParamByName("QYLX").AsInteger = iQYLX;
            query.ParamByName("FTPBJ").AsInteger = iFTPBJ;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ExecSQL();
            foreach (GXSJDY_MD_ITEM one in itemTable)
            {
                query.SQL.Text = "insert into QYDEF_MD(QYID,MDID)";
                query.SQL.Add(" values(:QYID,:MDID)");
                query.ParamByName("QYID").AsInteger = iJLBH;
                query.ParamByName("MDID").AsInteger = one.iMDID;
                query.ExecSQL();
            }
        }

        //审核时使用
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //ExecTable(query, "QYDEF", serverTime, "QYID");
            //foreach (GXSJDY_MD_ITEM one in itemTable)
            //{
            //    query.SQL.Text = "update HYK_HYXX set YXQ=:YXQ";
            //    if (iGGZT != 0)
            //    {
            //        query.SQL.Add(",STATUS=" + (FormatUtils.ParseDateString(dXYXQ) > serverTime.Date ? 1 : -4).ToString());
            //    }
            //    query.SQL.Add(" where HYID=:HYID");
            //    query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dXYXQ);
            //    query.ParamByName("HYID").AsInteger = one.iHYID;
            //    query.ExecSQL();
            //}
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_GXSJDY item = new CRMGL_GXSJDY();
            item.iJLBH = query.FieldByName("QYID").AsInteger;
            item.sQYMC = query.FieldByName("QYMC").AsString;
            item.iQYLX = query.FieldByName("QYLX").AsInteger;
            item.iFTPBJ = query.FieldByName("FTPBJ").AsInteger;
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
            return item;
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "W.QYID");
            CondDict.Add("sQYMC", "W.QYMC");
            CondDict.Add("iQYLX", "W.QYLX");
            CondDict.Add("iFTPBJ", "W.FTPBJ");
            CondDict.Add("iSTATUS", "W.STATUS");
            CondDict.Add("iTM", "W.TM");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "SELECT * FROM QYDEF W";
            query.SQL.Text += " WHERE 1=1 ";
            if (iJLBH != 0)
                query.SQL.AddLine("  and QYID=" + iJLBH);
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = " SELECT W.QYID,W.MDID,D.MDMC,D.MDDM";
                query.SQL.Text += " FROM QYDEF_MD W,MDDY D";
                query.SQL.Text += " WHERE W.MDID=D.MDID";
                query.SQL.Text += " AND W.QYID=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    GXSJDY_MD_ITEM item = new GXSJDY_MD_ITEM();
                    ((CRMGL_GXSJDY)lst[0]).itemTable.Add(item);
                    item.iQYID = query.FieldByName("QYID").AsInteger;
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    query.Next();
                }

            }
            return lst;
        }
    }
}
