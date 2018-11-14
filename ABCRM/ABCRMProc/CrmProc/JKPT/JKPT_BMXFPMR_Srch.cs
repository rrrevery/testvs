using BF;

using BF.Pub;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace BF.CrmProc.JKPT
{
    public class JKPT_BMXFPMR_Srch : BASECRMClass
    {
        public int iHYID = 0;
        public string sHYK_NO = string.Empty;
        public string sHYKNAME = string.Empty;
        public string sHY_NAME = string.Empty;
        public string sDEPTID = string.Empty;
        public int iSEX = 0;
        public string sSEX = string.Empty;
        public string sSJHM = string.Empty;
        public double fXFJE = 0;
        public double fJF = 0;
        public double fZXJE = 0;
        public double fZDJE = 0;
        public double fZKJE = 0;
        public int iXFCS = 0;
        public int iTHCS = 0;
        public int iPM = 0;
        public int irownum = 0;
        public string dRQ = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "R.MDID");
            query.SQL.Text = "select R.* from (select R.HYID,X.HYK_NO,F.HYKNAME,X.HY_NAME,G.SEX,G.SJHM,R.XSJE XFJE,R.JF,R.ZXJE,R.ZDJE,R.ZKJE_HY ZKJE,R.XFCS,R.THCS";
            query.SQL.Add(" from HYK_XF_BM_R R ,HYK_HYXX X,HYK_GRXX G,HYKDEF F");
            query.SQL.Add(" where R.HYID=X.HYID and R.HYID=G.HYID(+) and R.HYKTYPE=F.HYKTYPE ");
            MakeSrchCondition(query, "",false);
            switch (iPM)
            {
                case 1:
                    query.SQL.Add(" order by R.XFCS desc) R");
                    query.SQL.Add(" where rownum <=" + irownum);
                    break;
                case 2:
                    query.SQL.Add(" and R.THCS<>0 order by R.THCS desc) R");
                    query.SQL.Add(" where rownum <=" + irownum);
                    break;
                case 3:
                    query.SQL.Add(" order by R.XSJE desc) R");
                    query.SQL.Add(" where rownum <=" + irownum);
                    break;
                default:
                    break;
            }
            SetSearchQuery(query, lst, false);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            JKPT_BMXFPMR_Srch obj = new JKPT_BMXFPMR_Srch();
            obj.iXFCS = query.FieldByName("XFCS").AsInteger;
            obj.iSEX = query.FieldByName("SEX").AsInteger;
            if (obj.iSEX == 0)
                obj.sSEX = "男";
            else
                obj.sSEX = "女";
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.sSJHM = query.FieldByName("SJHM").AsString;
            obj.fXFJE = query.FieldByName("XFJE").AsFloat;
            obj.fJF = query.FieldByName("JF").AsFloat;
            obj.fZXJE = query.FieldByName("ZXJE").AsFloat;
            obj.fZDJE = query.FieldByName("ZDJE").AsFloat;
            obj.fZKJE = query.FieldByName("ZKJE").AsFloat;
            obj.iTHCS = query.FieldByName("THCS").AsInteger;
            return obj;
        }

    }
}
