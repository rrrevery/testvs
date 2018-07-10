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


namespace BF.CrmProc.LPGL
{
    public class LPGL_LPZFD : LPXX
    {
        public int iZXR;
        public string sZXRMC;
        public string dZXRQ;
        public double fBFSL = 0;
        public class LPGL_LPZFItem : LPXX
        {
            public double fBFSL;
        }
        public List<LPGL_LPZFItem> itemTable = new List<LPGL_LPZFItem>();

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "X.JLBH");
            CondDict.Add("sBGDDMC", "D.BGDDMC");
            CondDict.Add("sZY", "X.ZY");
            CondDict.Add("iDJR", "X.DJR");
            CondDict.Add("iZXR", "X.ZXR");
            CondDict.Add("dDJSJ", "X.DJSJ");
            CondDict.Add("dZXRQ", "X.ZXRQ");
            CondDict.Add("sBGDDDM", " X.BGDDDM");

            query.SQL.Text = "select X.*,D.BGDDMC  from HYK_JFFHLPBFJL X,HYK_BGDD D";
            query.SQL.Add("  where  X.BGDDDM=D.BGDDDM");
            SetSearchQuery(query, lst);

            if(lst.Count==1)
            {
                query.SQL.Text = "select * from HYK_JFFHLPBFJLITEM I where I.JLBH=" + iJLBH;
                query.SQL.Add("  order by LPID");
                query.Open();
                while (!query.Eof)
                {
                    LPGL_LPZFItem item = new LPGL_LPZFItem();
                    ((LPGL_LPZFD)lst[0]).itemTable.Add(item);
                    item.sLPDM = query.FieldByName("LPDM").AsString;
                    item.sLPMC = query.FieldByName("LPMC").AsString;
                    item.iLPID = query.FieldByName("LPID").AsInteger;
                    item.fLPDJ = query.FieldByName("LPDJ").AsFloat;
                    item.fLPJF = query.FieldByName("LPJF").AsFloat;
                    item.fBFSL = query.FieldByName("BFSL").AsFloat;
                    item.fKCSL = GetLPKC(item.iLPID, ((LPGL_LPZFD)lst[0]).sBGDDDM);
                    query.Next();
                }
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_LPZFD item = new LPGL_LPZFD();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.sZY = query.FieldByName("BZ").AsString;
            return item;
        }

        public static double GetLPKC(int iLPID1, string sBGDDDM1)
        {
            double KCSL = 0;

            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            conn.Open();
            CyQuery query1 = new CyQuery(conn);
            query1.SQL.Text = "  select P.KCSL   from HYK_JFFHLPXX L,HYK_JFFHLPKC P ";
            query1.SQL.Add("   where L.LPID=P.LPID and P.KCSL>0");
            query1.SQL.Add("  AND P.BGDDDM=:BGDDDM");
            query1.SQL.Add("  AND P.LPID=:LPID");
            query1.ParamByName("BGDDDM").AsString = sBGDDDM1;
            query1.ParamByName("LPID").AsInteger = iLPID1;
            query1.Open();
            while (!query1.Eof)
            {
                KCSL = query1.FieldByName("KCSL").AsFloat;
                query1.Next();
            }
            return KCSL;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_JFFHLPBFJL");
            query.SQL.Text = "insert into HYK_JFFHLPBFJL(JLBH,BGDDDM,DJR,DJRMC,DJSJ,BZ)";
            query.SQL.Add(" values(:JLBH,:BGDDDM,:DJR,:DJRMC,:DJSJ,:ZY)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("ZY").AsString = sZY;
            query.ExecSQL();
            foreach (LPGL_LPZFItem one in itemTable)
            {
                query.SQL.Text = "insert into  HYK_JFFHLPBFJLITEM(JLBH,LPID,LPDM,LPMC,LPDJ,LPJF,BFSL)";
                query.SQL.Add(" values(:JLBH,:LPID,:LPDM,:LPMC,:LPDJ,:LPJF,:BFSL)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("LPDM").AsString = one.sLPDM;
                query.ParamByName("LPMC").AsString = one.sLPMC;
                query.ParamByName("LPDJ").AsFloat = one.fLPDJ;
                query.ParamByName("LPJF").AsFloat = one.fLPJF;
                query.ParamByName("BFSL").AsFloat = one.fBFSL;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_JFFHLPBFJL", serverTime);
            foreach (LPGL_LPZFItem one in itemTable)
            {
                CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, (int)BASECRMDefine.CLLX_LPBD.LPBD_ZF, iJLBH, sBGDDDM, -one.fBFSL, iLoginRYID.ToString(), sLoginRYMC);
            }
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_JFFHLPBFJL;HYK_JFFHLPBFJLITEM;", "JLBH", iJLBH);
        }
    }
}
