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
    public class HYKGL_HYKTK : DJLR_ZX_CLass
    {
        public string sHYKNAME = string.Empty;
        public int iHYKTYPE = 0;
        public double fKFJE = 0;
        public double fCZJE = 0;
        public int iYYTS = 0;
        public string sHYK_NO = string.Empty;
        public List<HYKGL_HYKTKItem> itemTable = new List<HYKGL_HYKTKItem>();

        public class HYKGL_HYKTKItem
        {

            public string sHYKNAME = string.Empty;
            public int iHYID = 0;
            public string dYYXQ = string.Empty;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public string sSFZBH = string.Empty;
            public double fKFJE = 0;
            public int iYYTS = 0, iBJ_CHILD = 0;
            public double fQCYE = 0, fCZJE = 0, fWCLJF = 0;
            public int iHYKTYPE = 0;
            public string sBJ_CHILD = string.Empty;

        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_TK;HYK_TK_ITEM;", "JLBH", iJLBH);
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
            query.SQL.Text = "insert into HYK_TK(JLBH,HYKTYPE,ZY,KFJE,DJR,DJRMC,DJSJ,YYTS,TKSL,TKFS,TKJE,BGDDDM,JE)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:ZY,:KFJE,:DJR,:DJRMC,:DJSJ,:YYTS,:TKSL,3,:TKJE,:BGDDDM,:JE)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("KFJE").AsFloat = fKFJE;
            query.ParamByName("YYTS").AsInteger = iYYTS;
            query.ParamByName("TKSL").AsInteger = itemTable.Count;
            query.ParamByName("TKJE").AsFloat = fKFJE;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("JE").AsFloat = fCZJE;
            query.ExecSQL();
            foreach (HYKGL_HYKTKItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_TK_ITEM(JLBH,HYID,HYKTYPE,HYK_NO,KFJE,YYTS,BJ_CHILD,HY_NAME,JE)";
                query.SQL.Add(" values(:JLBH,:HYID,:HYKTYPE,:HYK_NO,:KFJE,:YYTS,:BJ_CHILD,:HY_NAME,:JE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ParamByName("KFJE").AsFloat = one.fKFJE;
                query.ParamByName("YYTS").AsInteger = one.iYYTS;
                query.ParamByName("BJ_CHILD").AsInteger = one.iBJ_CHILD;
                query.ParamByName("HY_NAME").AsString = one.sHY_NAME;
                query.ParamByName("JE").AsFloat = one.fCZJE;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_TK", serverTime, "JLBH");
            int pMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            foreach (HYKGL_HYKTKItem item in itemTable)
            {
                query.SQL.Text = " Update HYK_HYXX set STATUS=:STATUS where HYID=:HYID";
                query.ParamByName("STATUS").AsInteger = -1;  //退卡暂定-1
                query.ParamByName("HYID").AsInteger = item.iHYID;
                query.ExecSQL();
                if (item.fCZJE > 0)
                {
                    CrmLibProc.UpdateJEZH(out msg, query, item.iHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, pMDID, -item.fCZJE, "会员卡退卡");
                }

            }
        }



        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);

            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDDM", "W.BGDDDM");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");

            query.SQL.Text = "select W.*,K.HYKNAME,B.BGDDMC from  HYK_TK W,HYK_BGDD B,HYKDEF K where W.HYKTYPE=K.HYKTYPE and W.BGDDDM=B.BGDDDM";
            if (sHYK_NO != "")
            {
                query.SQL.Add(" and exists(select 1 from HYK_TK_ITEM I where I.JLBH=W.JLBH and  I.HYK_NO='" + sHYK_NO + "')");
            }
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,D.HYKNAME from  HYK_TK_ITEM I,HYKDEF D where I.HYKTYPE=D.HYKTYPE and  JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYKTKItem item = new HYKGL_HYKTKItem();
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.fKFJE = query.FieldByName("KFJE").AsFloat;
                    item.iYYTS = query.FieldByName("YYTS").AsInteger;
                    item.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
                    item.sBJ_CHILD = item.iBJ_CHILD == 0 ? "主卡" : "从卡";
                    item.fCZJE = query.FieldByName("JE").AsFloat;
                    ((HYKGL_HYKTK)lst[0]).itemTable.Add(item);
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKTK obj = new HYKGL_HYKTK();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.fKFJE = query.FieldByName("KFJE").AsFloat;
            obj.iYYTS = query.FieldByName("YYTS").AsInteger;
            obj.fCZJE = query.FieldByName("JE").AsFloat;
            return obj;
        }


    }
}
