using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;


namespace BF.CrmProc.JKPT
{
    public class JKPT_XFCSJK_Srch : BASECRMClass
    {
        public int iHYID = 0;
        public string sHY_NAME = string.Empty;
        public string sHYK_NO = string.Empty;
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public string sHYKNAME = string.Empty;
        public int iXFCS = 0;
        public int iTHCS = 0;
        public double fXFJE = 0;
        public double fJF = 0;
        public double fZDJE = 0;
        public double fZXJE = 0;
        public double fXSSL = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iMDID","X.MDID");            
            CondDict.Add("iXFCS", "X.XFCS");           
            switch (iSEARCHMODE)
            {               
                case 0:
                    //门店消费次数监控日
                    CondDict.Add("dRQ", "X.RQ");
                    query.SQL.Text = "select X.*,M.MDMC,H.HY_NAME,H.HYK_NO,F.HYKNAME";
                    query.SQL.Add(" from CR_HYK_XF_R X,MDDY M,HYK_HYXX H,HYKDEF F");
                    query.SQL.Add(" where X.MDID=M.MDID and X.HYID=H.HYID and X.HYKTYPE=F.HYKTYPE");
                    break;
                
                case 1:
                    //门店消费次数月监控
                    CondDict.Add("dRQ", "X.YEARMONTH");
                    query.SQL.Text = "select X.*,M.MDMC,H.HY_NAME,H.HYK_NO,F.HYKNAME";
                    query.SQL.Add(" from CR_HYK_XF_Y X,MDDY M,HYK_HYXX H,HYKDEF F");
                    query.SQL.Add(" where X.MDID=M.MDID and X.HYID=H.HYID and X.HYKTYPE=F.HYKTYPE");
                    break;
                case 2:
                    //单部门消费次数日监控
                    CondDict.Add("sBMDM", "B.BMDM");
                    CondDict.Add("dRQ", "X.RQ");
                    query.SQL.Text = "select X.*,M.MDMC,H.HY_NAME,H.HYK_NO,F.HYKNAME,B.BMDM,B.BMMC";
                    query.SQL.Add(" from CR_HYK_BM_XFMX_R X,MDDY M,HYK_HYXX H,HYKDEF F,SHBM B");
                    query.SQL.Add(" where X.MDID=M.MDID and X.HYID=H.HYID and X.HYKTYPE=F.HYKTYPE and X.SHBMID=B.SHBMID");
                    break;
                case 3:
                    //单部门消费次数月监控
                    CondDict.Add("sBMDM", "B.BMDM");
                    CondDict.Add("dRQ", "X.YEARMONTH");
                    query.SQL.Text = "select X.*,M.MDMC,H.HY_NAME,H.HYK_NO,F.HYKNAME,B.BMDM,B.BMMC";
                    query.SQL.Add(" from CR_HYK_BM_XFMX_Y X,MDDY M,HYK_HYXX H,HYKDEF F,SHBM B");
                    query.SQL.Add(" where X.MDID=M.MDID and X.HYID=H.HYID and X.HYKTYPE=F.HYKTYPE and X.SHBMID=B.SHBMID");
                    break;
                case 4:
                    //收款台消费次数日监控
                    CondDict.Add("sSKTNO", "X.SKTNO");
                    CondDict.Add("dRQ", "X.RQ");
                    query.SQL.Text = "select X.*,M.MDMC,H.HY_NAME,H.HYK_NO,F.HYKNAME";
                    query.SQL.Add(" from HYK_XF_SKT_R X,MDDY M,HYK_HYXX H,HYKDEF F");
                    query.SQL.Add(" where X.MDID=M.MDID and X.HYID=H.HYID and X.HYKTYPE=F.HYKTYPE");
                    break;
                case 5:
                    //收款台消费次数月监控
                    CondDict.Add("sSKTNO", "X.SKTNO");
                    CondDict.Add("dRQ", "X.YEARMONTH");
                    query.SQL.Text = "select X.*,M.MDMC,H.HY_NAME,H.HYK_NO,F.HYKNAME";
                    query.SQL.Add(" from HYK_XF_SKT_Y X,MDDY M,HYK_HYXX H,HYKDEF F");
                    query.SQL.Add(" where X.MDID=M.MDID and X.HYID=H.HYID and X.HYKTYPE=F.HYKTYPE");
                    break;
                default :
                    break;
            }
            MakeSrchCondition(query, "", false);
            query.SQL.Add(" order by X.HYID");
            SetSearchQuery(query, lst, false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            JKPT_XFCSJK_Srch obj = new JKPT_XFCSJK_Srch();
            switch (iSEARCHMODE)
            {
                case 0:
                case 1:
                    obj.iHYID = query.FieldByName("HYID").AsInteger;
                    obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    obj.iMDID = query.FieldByName("MDID").AsInteger;
                    obj.sMDMC = query.FieldByName("MDMC").AsString;
                    obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    obj.iXFCS = query.FieldByName("XFCS").AsInteger;
                    obj.iTHCS = query.FieldByName("THCS").AsInteger;
                    obj.fXFJE = query.FieldByName("XFJE").AsFloat;
                    obj.fJF = query.FieldByName("JF").AsFloat;
                    obj.fZDJE = query.FieldByName("ZDJE").AsFloat;
                    obj.fZXJE = query.FieldByName("ZXJE").AsFloat;
                    break;
                case 2:
                case 3:
                    obj.iHYID = query.FieldByName("HYID").AsInteger;
                    obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    obj.iMDID = query.FieldByName("MDID").AsInteger;
                    obj.sMDMC = query.FieldByName("MDMC").AsString;
                    obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    obj.iXFCS = query.FieldByName("XFCS").AsInteger;
                    obj.iTHCS = query.FieldByName("THCS").AsInteger;
                    obj.fXFJE = query.FieldByName("XSJE").AsFloat;  //XSJE
                    obj.fXSSL = query.FieldByName("XSSL").AsFloat;   //XSSL
                    obj.fJF = query.FieldByName("JF").AsFloat;
                    break;
                case 4:
                case 5:
                    obj.iHYID = query.FieldByName("HYID").AsInteger;
                    obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    obj.iMDID = query.FieldByName("MDID").AsInteger;
                    obj.sMDMC = query.FieldByName("MDMC").AsString;
                    obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    obj.iXFCS = query.FieldByName("XFCS").AsInteger;
                    obj.iTHCS = query.FieldByName("THCS").AsInteger;
                    obj.fXFJE = query.FieldByName("XSJE").AsFloat;  //XSJE
                    obj.fJF = query.FieldByName("JF").AsFloat;
                    obj.fZDJE = query.FieldByName("ZDJE").AsFloat;
                    obj.fZXJE = query.FieldByName("ZXJE").AsFloat;
                    break;
            }
            return obj;
        }
    }
}
