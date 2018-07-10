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
    public class HYKGL_YXQYC_Proc : DJLR_ZX_CLass
    {
        public int iHYKTYPE = 0;
        public int iGGZT = 0;
        public string sHYKNAME = string.Empty;
        public string dXYXQ = string.Empty;
        public string sHYKNO = string.Empty;
        public string sMDMC = string.Empty;
        public int iKSL = 0;
        public string sHYK_NO = string.Empty;
        public List<HYKGL_YXQYCItem> itemTable = new List<HYKGL_YXQYCItem>();

        public class HYKGL_YXQYCItem
        {

            public string sHYKNAME = string.Empty;
            public int iHYID = 0;
            public string dYYXQ = string.Empty;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public string sSFZBH = string.Empty;
            public double fQCYE = 0, fYE = 0, fWCLJF = 0;
        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_YXQBDJL;HYK_YXQBDJLITEM;", "CZJPJ_JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_YXQBDJL");
            query.SQL.Text = "insert into HYK_YXQBDJL(CZJPJ_JLBH,HYKTYPE,ZY,XYXQ,DJSJ,DJR,DJRMC,CZDD)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:ZY,:XYXQ,:DJSJ,:DJR,:DJRMC,:BGDDDM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("XYXQ").AsDateTime = FormatUtils.ParseDateString(dXYXQ);
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (HYKGL_YXQYCItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_YXQBDJLITEM(CZJPJ_JLBH,HYID,YYXQ,HYKNO)";
                query.SQL.Add(" values(:JLBH,:HYID,:YYXQ,:HYKNO)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("YYXQ").AsDateTime = FormatUtils.ParseDateString(one.dYYXQ);
                query.ParamByName("HYKNO").AsString = one.sHYK_NO;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_YXQBDJL", serverTime, "CZJPJ_JLBH");
            foreach (HYKGL_YXQYCItem one in itemTable)
            {
                query.SQL.Text = "update HYK_HYXX set YXQ=:YXQ";
                if (iGGZT != 0)
                {
                    query.SQL.Add(",STATUS=" + (FormatUtils.ParseDateString(dXYXQ) > serverTime.Date ? 1 : -4).ToString());
                }
                query.SQL.Add(" where HYID=:HYID");
                query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dXYXQ);
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);

            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");
            CondDict.Add("sBGDDDM", "W.CZDD");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            query.SQL.Text = "select W.*,D.HYKNAME,M.MDMC,B.BGDDMC,(select count(HYKNO) from HYK_YXQBDJLITEM I where I.CZJPJ_JLBH=W.CZJPJ_JLBH) KSL";
            query.SQL.Add(" from HYK_YXQBDJL W,HYKDEF D,HYK_BGDD B,MDDY M"); //
            query.SQL.Add(" where W.HYKTYPE=D.HYKTYPE");
            query.SQL.Add(" and W.CZDD=B.BGDDDM");
            query.SQL.Add(" and B.MDID=M.MDID(+)");
            if (sHYK_NO != "")
            {
                query.SQL.Add(" and exists(select 1 from HYK_YXQBDJLITEM I where I.CZJPJ_JLBH=W.CZJPJ_JLBH and  I.HYKNO='" + sHYK_NO + "')");
            }
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = " select I.*,D.HYKNAME,H.HY_NAME,G.SFZBH,F.WCLJF,E.QCYE,E.YE from HYK_YXQBDJLITEM I,HYK_HYXX H,HYKDEF D,HYK_GKDA G,HYK_JFZH F,HYK_JEZH E";
                query.SQL.Add(" where I.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE(+) and I.CZJPJ_JLBH=" + iJLBH);
                query.SQL.Add("and H.GKID=G.GKID(+) and H.HYID=F.HYID(+) and H.HYID=E.HYID(+)");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_YXQYCItem item = new HYKGL_YXQYCItem();
                    ((HYKGL_YXQYC_Proc)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYKNO").AsString;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.dYYXQ = FormatUtils.DateToString(query.FieldByName("YYXQ").AsDateTime);
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.sSFZBH = query.FieldByName("SFZBH").AsString;
                    item.fQCYE = query.FieldByName("QCYE").AsFloat;
                    item.fYE = query.FieldByName("YE").AsFloat;
                    item.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YXQYC_Proc obj = new HYKGL_YXQYC_Proc();
            obj.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.dXYXQ = FormatUtils.DateToString(query.FieldByName("XYXQ").AsDateTime);
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sBGDDDM = query.FieldByName("CZDD").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);

            obj.iKSL = query.FieldByName("KSL").AsInteger;
            return obj;
        }
    }
}
