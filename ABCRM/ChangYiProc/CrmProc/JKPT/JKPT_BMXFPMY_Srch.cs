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
    public class JKPT_BMXFPMY_Srch :BASECRMClass
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
        public int iYEARMONTH = 0;
        public int irownum = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID","R.MDID");
            query.SQL.Text = "select R.* from (select R.HYID,X.HYK_NO,F.HYKNAME,X.HY_NAME,G.SEX,G.SJHM,";
            query.SQL.Add(" sum(R.XSJE) XFJE,sum(R.JF) JF,min(R.ZXJE) ZXJE,max(R.ZDJE) ZDJE,SUM(ZKJE_HY) ZKJE,sum(R.XFCS) XFCS,sum(R.THCS) THCS");
            query.SQL.Add(" from HYK_XF_BM_R R,HYK_HYXX X,HYK_GRXX G,HYKDEF F");
            query.SQL.Add(" where R.HYID=X.HYID and R.HYID=G.HYID(+) and R.HYKTYPE=F.HYKTYPE");
            MakeSrchCondition(query, "group by R.HYID,X.HYK_NO,F.HYKNAME,X.HY_NAME,G.SEX,G.SJHM",false);
            switch (iPM)
            {
                case 1:
                    query.SQL.Add(" order by sum(R.XFCS) desc) R");
                    query.SQL.Add(" where rownum <=" + irownum);
                    break;
                case 2:
                    query.SQL.Add(" having(sum(R.THCS))<>0 order by sum(R.THCS) desc) R");
                    query.SQL.Add(" where rownum <=" + irownum);
                    break;
                case 3:
                    query.SQL.Add(" order by sum(R.XSJE) desc) R");
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
            JKPT_BMXFPMY_Srch obj = new JKPT_BMXFPMY_Srch();
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
