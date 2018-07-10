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



namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKZF : DJLR_ZX_CLass
    {
        public int iHYKTYPE = 0;
        public double fJE = 0;
        public string sHYKNAME = string.Empty;
        public int iMDID = -1;
        public string sMDMC = string.Empty;
        public string sHYKNO = string.Empty;
        public int iBHKS;
        public List<MZKGL_MZKZFItem> itemTable = new List<MZKGL_MZKZFItem>();

        public class MZKGL_MZKZFItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public string sSFZBH = string.Empty;
            public double fLJXFJE = 0, fLJJF = 0, fWCLJF = 0, fYE = 0;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_ZFJL;MZK_ZFJLITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_ZFJL");
            iMDID = CrmLibProc.BGDDToMDID(query,sBGDDDM);
            query.SQL.Text = "insert into MZK_ZFJL(JLBH,HYKTYPE,ZY,DJSJ,DJR,DJRMC,MDID,JE,BGDDDM)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:ZY,:DJSJ,:DJR,:DJRMC,:MDID,:JE,:BGDDDM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ExecSQL();
            foreach (MZKGL_MZKZFItem one in itemTable)
            {
                query.SQL.Text = "insert into MZK_ZFJLITEM(JLBH,HYID,LJXFJE,LJJF,WCLJF,YE,HYKNO)";
                query.SQL.Add(" values(:JLBH,:HYID,:LJXFJE,:LJJF,:WCLJF,:YE,:HYKNO)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("LJXFJE").AsFloat = one.fLJXFJE;
                query.ParamByName("LJJF").AsFloat = one.fLJJF;
                query.ParamByName("WCLJF").AsFloat = one.fWCLJF;
                query.ParamByName("YE").AsFloat = one.fYE;
                query.ParamByName("HYKNO").AsString = one.sHYK_NO;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_ZFJL", serverTime);
            foreach (MZKGL_MZKZFItem one in itemTable)
            {
                query.SQL.Text = "update MZKXX set STATUS=:STATUS where HYID=:HYID";
                query.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.HYXXStatus.HYXX_STATUS_ZF;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ExecSQL();
                CrmLibProc.SaveCardTrack(query, one.sHYK_NO, one.iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_ZF, iJLBH, iLoginRYID, sLoginRYMC);
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","W.JLBH");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sBGDDDM", "W.BGDDDM");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");

            query.SQL.Text = "  select W.*,B.BGDDMC,D.HYKNAME ,(SELECT COUNT(JLBH) BHKS FROM MZK_ZFJLITEM I WHERE i.jlbh=W.JLBH  GROUP BY JLBH) BHKS";
            query.SQL.Add("  from MZK_ZFJL W,HYK_BGDD B,HYKDEF D where W.BGDDDM=B.BGDDDM(+) and W.HYKTYPE=D.HYKTYPE");
            if (sHYKNO != "")
            {
                query.SQL.Add(" and exists(select 1 from MZK_ZFJLITEM I where I.JLBH=W.JLBH and  I.HYKNO='" + sHYKNO + "'  )");
            }
            SetSearchQuery(query,lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,H.HY_NAME from MZK_ZFJLITEM I,MZKXX H where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.HYID=H.HYID");
                query.Open();
                while (!query.Eof)
                {
                    MZKGL_MZKZFItem item = new MZKGL_MZKZFItem();
                    ((MZKGL_MZKZF)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYKNO").AsString;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    //item.sSFZBH = hyxx.sSFZBH;
                    //item.fQCYE = hyxx.fQCYE;
                    item.fYE = query.FieldByName("YE").AsFloat;
                    //item.fWCLJF = hyxx.fWCLJF;
                    query.Next();
                }
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKZF item = new MZKGL_MZKZF();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.iMDID = query.FieldByName("MDID").AsInteger;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.sZY = query.FieldByName("ZY").AsString;
            item.iBHKS = query.FieldByName("BHKS").AsInteger;
            item.fJE = query.FieldByName("JE").AsFloat;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            return item;
        }
    }
}
