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
    public class HYKGL_HYKZF_Proc : DJLR_ZX_CLass
    {
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public int iMDID = -1;
        public string sMDMC = string.Empty;
        public string sHYKNO = string.Empty;
        public int iBHKS;
        public List<HYKGL_HYKZFItem> itemTable = new List<HYKGL_HYKZFItem>();

        public class HYKGL_HYKZFItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public string sSFZBH = string.Empty;
            public double fLJXFJE = 0, fLJJF = 0, fWCLJF = 0, fYE = 0;
            public int iBJ_CHILD = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_ZFJL;HYK_ZFJLITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_ZFJL");
            query.SQL.Text = "insert into HYK_ZFJL(JLBH,HYKTYPE,ZY,DJSJ,DJR,DJRMC,BGDDDM)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:ZY,:DJSJ,:DJR,:DJRMC,:BGDDDM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ExecSQL();
            foreach (HYKGL_HYKZFItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_ZFJLITEM(JLBH,HYID,LJXFJE,LJJF,WCLJF,YE,HYKNO,BJ_CHILD)";
                query.SQL.Add(" values(:JLBH,:HYID,:LJXFJE,:LJJF,:WCLJF,:YE,:HYKNO,:BJ_CHILD)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("LJXFJE").AsFloat = one.fLJXFJE;
                query.ParamByName("LJJF").AsFloat = one.fLJJF;
                query.ParamByName("WCLJF").AsFloat = one.fWCLJF;
                query.ParamByName("YE").AsFloat = one.fYE;
                query.ParamByName("HYKNO").AsString = one.sHYK_NO;
                query.ParamByName("BJ_CHILD").AsInteger = one.iBJ_CHILD;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_ZFJL", serverTime);
            foreach (HYKGL_HYKZFItem one in itemTable)
            {
                //if (one.iBJ_CHILD == 0)//作废的为主卡
                // {
                query.SQL.Text = "  Begin";
                query.SQL.Text += "  update HYK_HYXX set STATUS=:STATUS where HYID=:HYID;";
                query.SQL.AddLine("  update HYK_CHILD_JL set STATUS=:STATUS where HYID=:HYID;");
                query.SQL.AddLine(" End;");
                query.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.HYXXStatus.HYXX_STATUS_ZF;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDDM", "W.BGDDDM");
            CondDict.Add("sBGDDMC", "BGDDMC");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sHYKNAME", "HYKNAME");
            CondDict.Add("sZY", "ZY");
            CondDict.Add("sDJRMC", "DJRMC");

            CondDict.Add("dDJSJ", "DJSJ");
            CondDict.Add("sZXRMC", "ZXRMC");
            CondDict.Add("dZXRQ", "ZXRQ");
            CondDict.Add("iMDID", "W.MDID");
            query.SQL.Text = "  select W.*,D.HYKNAME ,W.BGDDDM,B.BGDDMC,(SELECT COUNT(JLBH) BHKS FROM HYK_ZFJLITEM I WHERE i.jlbh=W.JLBH  GROUP BY JLBH) BHKS";
            query.SQL.Add("  from HYK_ZFJL W,HYKDEF D,HYK_BGDD B where W.HYKTYPE=D.HYKTYPE  and W.BGDDDM=B.BGDDDM(+)");// MDDY B, W.MDID=B.MDID(+) and
            if (sHYKNO != "")
            {
                query.SQL.Add(" and exists(select 1 from HYK_ZFJLITEM I where I.JLBH=W.JLBH and  I.HYKNO='" + sHYKNO + "'  )");
            }

            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {

                query.SQL.Text = "select I.*,H.HY_NAME from HYK_ZFJLITEM I, HYK_HYXX H where I.JLBH="+iJLBH;
                query.SQL.Add(" AND  I.HYID=H.HYID");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYKZFItem item = new HYKGL_HYKZFItem();
                    ((HYKGL_HYKZF_Proc)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYKNO").AsString;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
                    //item.sSFZBH = hyxx.sSFZBH;
                    item.fYE = query.FieldByName("YE").AsFloat;
                    item.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    item.fLJXFJE = query.FieldByName("LJXFJE").AsFloat;
                    query.Next();
                }

            }
            query.Close();
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKZF_Proc obj = new HYKGL_HYKZF_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            //item.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.iBHKS = query.FieldByName("BHKS").AsInteger;
            return obj;
        }
    }
}
