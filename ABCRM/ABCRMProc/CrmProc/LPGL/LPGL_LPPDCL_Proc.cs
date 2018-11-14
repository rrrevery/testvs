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
    public class LPGL_LPPDCL : LPXX
    {
        public int iZXR;
        public string sZXRMC;
        public string dZXRQ;
        public string dPDRQ;
        public int iRQYZ;
        public string yzmsg;
        public class LPGL_LPPDITEM : LPXX
        {
            public double fPDKC;
            public double fPKSL;
            public double fLPKC;
        }

        public List<LPGL_LPPDITEM> itemTable = new List<LPGL_LPPDITEM>();
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sBGDDMC", "D.BGDDMC");
            CondDict.Add("sZY", "X.ZY");
            CondDict.Add("sDJRMC", "X.DJRMC");
            CondDict.Add("sZXRMC", "X.ZXRMC");
            CondDict.Add("dDJSJ", "X.DJSJ");
            CondDict.Add("dZXRQ", "X.ZXRQ");
            CondDict.Add("iZXR", "X.ZXR");
            CondDict.Add("iDJR", "X.DJR");


            query.SQL.Text = " select  X.*,D.BGDDMC from   HYK_LPPDJL X,HYK_BGDD D";
            query.SQL.Add("  where  X.BGDDDM=D.BGDDDM");
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from HYK_LPPDJLITEM I where I.JLBH=" + iJLBH;
                query.SQL.Add("  order by LPID");
                query.Open();
                while (!query.Eof)
                {
                    LPGL_LPPDITEM item = new LPGL_LPPDITEM();
                    ((LPGL_LPPDCL)lst[0]).itemTable.Add(item);
                    item.sLPDM = query.FieldByName("LPDM").AsString;
                    item.sLPMC = query.FieldByName("LPMC").AsString;
                    item.iLPID = query.FieldByName("LPID").AsInteger;
                    item.fLPDJ = query.FieldByName("LPDJ").AsFloat;
                    item.fLPJF = query.FieldByName("LPJF").AsFloat;
                    item.fKCSL = query.FieldByName("LPKC").AsFloat;
                    item.fPDKC = query.FieldByName("PDKC").AsFloat;
                    item.fPKSL = query.FieldByName("PKSL").AsFloat;
                    query.Next();
                }
            }

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_LPPDCL item = new LPGL_LPPDCL();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.dPDRQ = FormatUtils.DateToString(query.FieldByName("PDRQ").AsDateTime);
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.sZY = query.FieldByName("BZ").AsString;
            return item;
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_JFFHLPJHD");
            query.SQL.Text = "insert into HYK_LPPDJL(JLBH,BGDDDM,DJR,DJRMC,DJSJ,PDRQ,BZ)";
            query.SQL.Add(" values(:JLBH,:BGDDDM,:DJR,:DJRMC,:DJSJ,:PDRQ,:ZY)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("PDRQ").AsDateTime = FormatUtils.ParseDateString(dPDRQ);
            query.ParamByName("ZY").AsString = sZY;
            query.ExecSQL();
            foreach (LPGL_LPPDITEM one in itemTable)
            {
                query.SQL.Text = "insert into  HYK_LPPDJLITEM(JLBH,LPID,LPDM,LPMC,LPDJ,LPJF,PDKC,PKSL,LPKC)";
                query.SQL.Add(" values(:JLBH,:LPID,:LPDM,:LPMC,:LPDJ,:LPJF,:PDKC,:PKSL,:LPKC)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("LPDM").AsString = one.sLPDM;
                query.ParamByName("LPMC").AsString = one.sLPMC;
                query.ParamByName("LPDJ").AsFloat = one.fLPDJ;
                query.ParamByName("LPJF").AsFloat = one.fLPJF;
                query.ParamByName("PDKC").AsFloat = one.fPDKC;
                query.ParamByName("LPKC").AsFloat = one.fKCSL;
                if (one.fKCSL - one.fPDKC == 0)
                {
                    query.ParamByName("PKSL").AsFloat = 0;
                }
                else
                {
                    query.ParamByName("PKSL").AsFloat = -(one.fKCSL - one.fPDKC);
                }

                query.ExecSQL();
            }
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_LPPDJL;HYK_LPPDJLITEM;", "JLBH", iJLBH);
        }


        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_LPPDJL", serverTime);
            foreach (LPGL_LPPDITEM one in itemTable)
            {
                CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, (int)BASECRMDefine.CLLX_LPBD.LPBD_SY, iJLBH, sBGDDDM, one.fPKSL, iLoginRYID.ToString(), sLoginRYMC);
            }
        }

    }
}
