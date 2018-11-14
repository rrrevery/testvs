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
    public class JKPT_YJHYR_Srch : BASECRMClass
    {
        public int iHYID = 0;
        public string sHYK_NO = string.Empty;
        public string sHY_NAME = string.Empty;
        public int iSEX = 0;
        public string sSEX = string.Empty;
        public int iNL = 0;
        public string sSJHM = string.Empty;
        public int iXFYJ = 0;
        public int iSKTYJ = 0;
        public int iBMYJ = 0;
        public int iFLYJ = 0;
        public int iMDID = 0;
        public double fQZ = 0;
        public int iKYCS = 0;

        public string dRQ = string.Empty;
        public int iYJLX = 0;
        public int iZBLX = 0;
        public int iYJZB = 0;
        public int iXFCS = 0;
        public int iTHCS = 0;
        public int iYJGZ = 0;
        public string sMDMC = string.Empty;
        public string sHYKNAME = string.Empty;
        public double fCCBL = 0;
        public double fXFJE = 0;
        public double fJF = 0;
        public string sZBLX = string.Empty;
        public string sYJLX = string.Empty;

        public int iKYHY = 0;

        //public new string[] asFieldNames = {
        //                                   "dRQ;Y.RQ",
        //                                   "iMDID;Y.MDID",
        //                                   "sHYK_NO;X.HYK_NO",
        //                                   "sSEX;G.SEX",
        //                                   "iNL;G.CSRQ",
        //                                   "sSJHM;G.SJHM",
        //                                   "iXFYJ;XFYJ",
        //                                   "iSKTYJ;SKTYJ",
        //                                   "iBMYJ;BMYJ",
        //                                   "iFLYJ;FLYJ",
        //                                   "sHYK_NO;X.HYK_NO",
        //                               };
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "Y.MDID");
            CondDict.Add("dRQ", "Y.RQ");


            if (iSEARCHMODE == 0)     //预警会员日数据
            {
                query.SQL.Text = "select X.HYID,X.HYK_NO,X.HY_NAME,G.SEX,extract(year from sysdate)-extract(year from csrq) NL,SJHM,Y.MDID,M.MDMC,sum(XFYJ) XFYJ,sum(SKTYJ) SKTYJ,sum(BMTJ) BMTJ,sum(FLYJ) FLYJ,max(QZ) QZ1 ";
                query.SQL.Add(",(select count(*) from HYK_KYJL L where L.HYID=X.HYID AND RQ=to_date(to_char(sysdate,'yyyy-mm-dd'),'yyyy-mm-dd')) KYCS");
                query.SQL.Add(" from YJHY Y,HYK_HYXX X,HYK_GRXX G ,MDDY M where Y.HYID=X.HYID and X.HYID=G.HYID(+) AND M.MDID=Y.MDID ");
                if (iKYHY == 1)
                    query.SQL.Add("and X.BJ_KY = 1 ");
                if (iKYHY == 2)
                    query.SQL.Add("and nvl(X.BJ_KY,0) = 0");
                SetSearchQuery(query, lst,true, "group by X.HYID,M.MDMC,X.HYK_NO,X.HY_NAME,G.SEX,extract(year from sysdate)-extract(year from csrq),SJHM,Y.MDID");

            }
            if (iSEARCHMODE == 1)      // 会员预警明细
            {
                query.SQL.Text = "select Y.RQ,Y.YJLX,F.ZBLX,Y.YJZB,Y.XFCS,(Y.CCBL*100) CCBL,Y.XFJE,Y.THCS,Y.JF,M.MDMC,Y.YJGZ,D.HYKNAME";
                query.SQL.Add(" from YJHYMX Y,YJGZDEF F,MDDY M,HYK_HYXX H,HYKDEF D");
                query.SQL.Add(" where Y.YJGZ=F.GZBH and Y.MDID=M.MDID and Y.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE");
                query.SQL.Add(" and Y.HYID = " + iHYID);
                query.SQL.Add(" and Y.MDID = " + iMDID);
                SetSearchQuery(query, lst);
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {

            JKPT_YJHYR_Srch obj = new JKPT_YJHYR_Srch();
            if (iSEARCHMODE == 0)
            {
                obj.iHYID = query.FieldByName("HYID").AsInteger;
                obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                obj.sMDMC = query.FieldByName("MDMC").AsString;

                obj.iSEX = query.FieldByName("SEX").AsInteger;
                obj.sSEX = CrmLibProc.GetSexStr_01(obj.iSEX);
                obj.iNL = query.FieldByName("NL").AsInteger;
                obj.sSJHM = query.FieldByName("SJHM").AsString;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.iXFYJ = query.FieldByName("XFYJ").AsInteger;
                obj.iSKTYJ = query.FieldByName("SKTYJ").AsInteger;
                obj.iBMYJ = query.FieldByName("BMTJ").AsInteger;
                obj.iFLYJ = query.FieldByName("FLYJ").AsInteger;
                obj.fQZ = query.FieldByName("QZ1").AsFloat;
                obj.iKYCS = query.FieldByName("KYCS").AsInteger;
            }
            if (iSEARCHMODE == 1)
            {
                obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
                obj.iYJLX = query.FieldByName("YJLX").AsInteger;
                obj.sYJLX = CrmLibProc.GetYJLX(obj.iYJLX);
                obj.iZBLX = query.FieldByName("ZBLX").AsInteger;
                obj.sZBLX = CrmLibProc.GetZBLX(obj.iZBLX);
                obj.iYJZB = query.FieldByName("YJZB").AsInteger;
                obj.iXFCS = query.FieldByName("XFCS").AsInteger;
                obj.fCCBL = query.FieldByName("CCBL").AsFloat;
                obj.fXFJE = query.FieldByName("XFJE").AsFloat;
                obj.iTHCS = query.FieldByName("THCS").AsInteger;
                obj.fJF = query.FieldByName("JF").AsFloat;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.iYJGZ = query.FieldByName("YJGZ").AsInteger;
            }
            return obj;
        }
    }
}
