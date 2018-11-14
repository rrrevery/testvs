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
    public class HYKGL_KCKYXQ : HYK_DJLR_CLass
    {
        public string dXYXQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string sHYKNO = "";
        public int iKSL = 0;

        public List<HYKGL_KCKYXQItem> itemTable = new List<HYKGL_KCKYXQItem>();

        public class HYKGL_KCKYXQItem
        {
            public string sCZKHM = string.Empty;
            public string dYXQ = FormatUtils.DatetimeToString(DateTime.MinValue);
            // public string dYYXQ = "";
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //if (iKSL <= 0)
            //{
            //    msg = "卡数量不能小于等于0";
            //    return false;
            //}
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "KCK_YXQBDJL;KCK_YXQBDJLITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("KCK_YXQBDJL", 1, sDBConnName);
            query.SQL.Text = "insert into KCK_YXQBDJL(JLBH,HYKTYPE,ZY,XYXQ,DJR,DJRMC,DJSJ,CZDD)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:ZY,:XYXQ,:DJR,:DJRMC,:DJSJ,:CZDD)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("XYXQ").AsDateTime = FormatUtils.ParseDateString(dXYXQ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ExecSQL();
            foreach (HYKGL_KCKYXQItem one in itemTable)
            {
                query.SQL.Text = "insert into KCK_YXQBDJLITEM(JLBH,CZKHM,YYXQ)";
                query.SQL.Add(" values(:JLBH,:CZKHM,:YYXQ)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ParamByName("YYXQ").AsDateTime = FormatUtils.ParseDateString(one.dYXQ);
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "KCK_YXQBDJL", serverTime);
            foreach (HYKGL_KCKYXQItem one in itemTable)
            {
                query.SQL.Text = "update HYKCARD set YXQ=:YXQ where CZKHM=:CZKHM";
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dXYXQ);
                query.ExecSQL();
            }
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_KCKYXQ obj = new HYKGL_KCKYXQ();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.sBGDDDM = query.FieldByName("CZDD").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.dXYXQ = FormatUtils.DateToString(query.FieldByName("XYXQ").AsDateTime);
            obj.iKSL = query.FieldByName("KSL").AsInteger;
            return obj;
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sHYKNAME", "D.HYKNAME");
            CondDict.Add("dXYXQ", "W.XYXQ");
            CondDict.Add("iMDID", "B.MDID");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iCZK", "D.BJ_CZK");
            CondDict.Add("iKSL", "KSL");
            query.SQL.Text = "select W.*,D.HYKNAME,M.MDMC,B.BGDDMC, (select count(CZKHM) from KCK_YXQBDJLITEM I where I.JLBH=W.JLBH) KSL";
            query.SQL.Add("     from KCK_YXQBDJL W,HYKDEF D,HYK_BGDD B,MDDY M");
            query.SQL.Add("    where W.CZDD=B.BGDDDM(+) and B.MDID=M.MDID(+) and W.HYKTYPE=D.HYKTYPE");
            if (sHYKNO != "")
            {
                query.SQL.Add(" and exists(select 1 from KCK_YXQBDJLITEM I where I.JLBH=W.JLBH and  I.CZKHM='" + sHYKNO + "'  )");
            }
            if (iJLBH != 0)
                query.SQL.Add("  and W.JLBH=" + iJLBH);
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.* from KCK_YXQBDJLITEM I where I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_KCKYXQItem obj = new HYKGL_KCKYXQItem();
                    ((HYKGL_KCKYXQ)lst[0]).itemTable.Add(obj);
                    obj.sCZKHM = query.FieldByName("CZKHM").AsString;
                    obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YYXQ").AsDateTime);
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }
    }
}
