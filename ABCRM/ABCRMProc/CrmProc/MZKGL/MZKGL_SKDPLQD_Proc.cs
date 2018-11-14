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
using BF.CrmProc;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_SKDPLQD_Proc : DJLR_ZX_CLass
    {
        public List<MZKGL_SKDPLQDItem> itemTable = new List<MZKGL_SKDPLQDItem>();

        public class MZKGL_SKDPLQDItem
        {
            public int iSKDBH = 0;
            public int iSKSL = 0;
            public decimal cSSJE = 0;
            public string sZY = "", sDJRMC = "", sZXRMC = "", sKHMC = "";
            public string dDJSJ = "";
            public string dZXRQ = "";
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "CZK_SKDPLQD;CZK_SKDPLQD_ITEM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("CZK_SKDPLQD");
            query.SQL.Text = "insert into CZK_SKDPLQD(JLBH,DJSJ,DJR,DJRMC,CZDD,ZY)";
            query.SQL.Add(" values(:JLBH,:DJSJ,:DJR,:DJRMC,:BGDDDM,:ZY)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("ZY").AsString = sZY;

            query.ExecSQL();
            foreach (MZKGL_SKDPLQDItem one in itemTable)
            {
                query.SQL.Text = "insert into CZK_SKDPLQD_ITEM(JLBH,SKDBH)";
                query.SQL.Add(" values(:JLBH,:SKDBH)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("SKDBH").AsInteger = one.iSKDBH;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string sHYK_NO = string.Empty;
            ExecTable(query, "CZK_SKDPLQD", serverTime, "JLBH");
            DbConnection connexec = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery execquery = new CyQuery(connexec);
                foreach (MZKGL_SKDPLQDItem one in itemTable)
                {
                    query.SQL.Text = "update MZK_SKJL set STATUS=2,QDSJ=sysdate where JLBH=" + one.iSKDBH;
                    query.ExecSQL();

                    query.SQL.Text = "delete from MZKCARD A where exists (select 1 from MZK_SKJLITEM B where A.CZKHM=B.CZKHM and B.JLBH = " + one.iSKDBH + ")";
                    query.ExecSQL();

                    query.SQL.Text = "select HYID from MZK_SKJLITEM where JLBH=" + one.iSKDBH;
                    query.Open();
                    while (!query.Eof)
                    {
                        MZKGL_SKDPLQD_Proc item = new MZKGL_SKDPLQD_Proc();
                        CrmLibProc.UpdateMZKSTATUS(execquery, query.FieldByName("HYID").AsInteger, 0);
                        query.Next();
                    }
                    query.Close();
                }
            }
            finally
            {
                connexec.Close();
            }



        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("sBGDDDM", "A.CZDD");
            query.SQL.Text = "select A.*,BGDDMC";
            query.SQL.Add("     from CZK_SKDPLQD A,HYK_BGDD C");
            query.SQL.Add("    where A.CZDD=C.BGDDDM ");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,L.SKSL,L.SSJE,L.ZY,L.DJRMC,L.DJSJ,L.ZXRMC,L.ZXRQ,A.KHMC ";
                query.SQL.Add(" from CZK_SKDPLQD_ITEM I,MZK_SKJL L,MZK_KHDA A");
                query.SQL.Add(" where I.JLBH=" + iJLBH + " and I.SKDBH=L.JLBH and A.KHID(+)=L.KHID ");
                query.Open();
                while (!query.Eof)
                {
                    MZKGL_SKDPLQDItem item = new MZKGL_SKDPLQDItem();
                    ((MZKGL_SKDPLQD_Proc)lst[0]).itemTable.Add(item);
                    item.iSKDBH = query.FieldByName("SKDBH").AsInteger;
                    item.iSKSL = query.FieldByName("SKSL").AsInteger;
                    item.cSSJE = query.FieldByName("SSJE").AsCurrency;
                    item.sZY = query.FieldByName("ZY").AsString;
                    item.sDJRMC = query.FieldByName("DJRMC").AsString;
                    item.sZXRMC = query.FieldByName("ZXRMC").AsString;
                    item.sKHMC = query.FieldByName("KHMC").AsString;
                    item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                    item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_SKDPLQD_Proc item = new MZKGL_SKDPLQD_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return item;
        }



    }
}
