using System;
using System.Collections.Generic;
using BF.Pub;


namespace BF.CrmProc.JKPT
{
    public class JKPT_YJHYY_Srch :BASECRMClass
    {
        public int iHYID = 0;
        public string sHYK_NO = string.Empty;
        public string sHY_NAME = string.Empty;
        public int iSEX = 0;
        public string sSEX = string.Empty;
        public int iNL = 0;
        public string sSJHM = string.Empty;
        public int iMDID = 0;
        public int iXFYJ = 0;
        public int iSKTYJ = 0;
        public int iBMTJ = 0;
        public int iFLYJ = 0;
        public double fQZ = 0;
        public double fYEARMONTH = 0;

        public int iYJLX = 0;
        public string sYJLX = string.Empty;
        public int iZBLX = 0;
        public string sZBLX = string.Empty;
        public double fYJZB = 0;
        public int iXFCS = 0;
        public double fCCBL = 0;
        public double fXFJE = 0;
        public int iTHCS = 0;
        public double fJF = 0;
        public string sMDMC = string.Empty;
        public string sHYKNAME = string.Empty;
        public int iYJGZ = 0;
        public int iKYHY = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "Y.MDID");
            if (iSEARCHMODE == 1)
            {
                query.SQL.Text = "select X.YEARMONTH,X.YJLX,F.ZBLX,X.YJZB,X.XFCS,(X.CCBL*100) CCBL,X.XFJE,X.THCS,X.JF,Y.MDMC,X.YJGZ,D.HYKNAME";
                query.SQL.Add(" from YJHYMX_Y X,YJGZDEF F,MDDY Y,HYK_HYXX H,HYKDEF D");
                query.SQL.Add(" where X.YJGZ=F.GZBH and X.MDID=Y.MDID and X.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE");
                query.SQL.Add(" and X.HYID=" + iHYID);
                SetSearchQuery(query, lst);
            }
            if (iSEARCHMODE == 0)
            {
                query.SQL.Text = "select X.HYID,X.HYK_NO,X.HY_NAME,G.SEX,extract(year from sysdate)-extract(year from csrq) NL,SJHM,Y.MDID,M.MDMC, sum(XFYJ) XFYJ,sum(SKTYJ) SKTYJ,sum(BMTJ) BMTJ,sum(FLYJ) FLYJ,max(QZ) QZ1  ";
                query.SQL.Add(" ,(select count(*) from HYK_KYJL L where L.HYID=X.HYID AND RQ=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd')) KYCS  ");
                query.SQL.Add("  from YJHY_Y Y,HYK_HYXX X,HYK_GRXX G,MDDY M ");
                query.SQL.Add("  where  Y.HYID=X.HYID and X.HYID=G.HYID(+) AND M.MDID=Y.MDID ");
                if (iKYHY == 1)
                    query.SQL.Add("and X.BJ_KY = 1 ");
                if (iKYHY == 2)
                    query.SQL.Add("and nvl(X.BJ_KY,0) = 0");
                SetSearchQuery(query, lst, true, "group by X.HYID,M.MDMC,X.HYK_NO,X.HY_NAME,G.SEX,extract(year from sysdate)-extract(year from csrq),SJHM,Y.MDID,YEARMONTH");
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {

            JKPT_YJHYY_Srch obj = new JKPT_YJHYY_Srch();
            if (iSEARCHMODE == 1)
            {
                obj.fYEARMONTH = query.FieldByName("YEARMONTH").AsFloat;
                obj.iYJLX = query.FieldByName("YJLX").AsInteger;
                obj.sYJLX = CrmLibProc.GetYJLX(obj.iYJLX);
                obj.iZBLX = query.FieldByName("ZBLX").AsInteger;
                obj.sZBLX = CrmLibProc.GetZBLX(obj.iZBLX);
                obj.fYJZB = query.FieldByName("YJZB").AsFloat;
                obj.iXFCS = query.FieldByName("XFCS").AsInteger;
                obj.fCCBL = query.FieldByName("CCBL").AsFloat;
                obj.fXFJE = query.FieldByName("XFJE").AsFloat;
                obj.iTHCS = query.FieldByName("THCS").AsInteger;
                obj.fJF = query.FieldByName("JF").AsFloat;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.iYJGZ = query.FieldByName("YJGZ").AsInteger;
            }
            if (iSEARCHMODE == 0)
            {
                obj.iHYID = query.FieldByName("HYID").AsInteger;
                obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                obj.iSEX = query.FieldByName("SEX").AsInteger;
                obj.sSEX = CrmLibProc.GetSexStr_01(obj.iSEX);
                obj.iNL = query.FieldByName("NL").AsInteger;
                obj.sSJHM = query.FieldByName("SJHM").AsString;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.iXFYJ = query.FieldByName("XFYJ").AsInteger;
                obj.iSKTYJ = query.FieldByName("SKTYJ").AsInteger;
                obj.iBMTJ = query.FieldByName("BMTJ").AsInteger;
                obj.iFLYJ = query.FieldByName("FLYJ").AsInteger;
                obj.fQZ = query.FieldByName("QZ1").AsFloat;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
            }
            return obj;
        }
    }
}
