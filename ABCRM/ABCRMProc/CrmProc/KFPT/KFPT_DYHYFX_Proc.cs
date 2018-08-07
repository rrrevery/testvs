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



namespace BF.CrmProc.KFPT
{
    public class KFPT_DYHYFX_Proc : BASECRMClass
    {
        public string sLDLX = string.Empty;
        public int iLDCS = 1;
        public int yhqtype = 0, bqlx = 0;
        public int Xmonth = 0, fljc = 0;
        public string spfldm = string.Empty;
        public class JCHYXX : HYXX_Detail
        {
            public string dZHXFRQ = string.Empty, sEmail = string.Empty, sWGKFJL = string.Empty;
            public string sKFJL = string.Empty, sZY = string.Empty, sKZT = string.Empty;
            public string sZJLXSTR = string.Empty;
            public double fZDJE = 0, fMYD = 0;
            public int iLDCS = 0, iGKJZID = 0, iPM = 0, iHYZSL = 0, iPM_KFJL = 0, iHYZSL_KFJL = 0, iSMZQ = 0, dXGLDSJ = 0, iFHYTS = 0;
            public double fXFPM = 0, fXFZPM = 0;
            public int iKSNY = 0, iJSNY = 0;
            public int iXYDJID = 0;
            public string sXYDJMC = string.Empty;
            public double fAVE_XFE = 0, fLDPL = 0;
            public string sSMZQ = string.Empty, sXGLDSJ = string.Empty, sGKJZFLMC = string.Empty;
            public string sBZZY = string.Empty;
            public List<ZJXFJEYF> list = new List<ZJXFJEYF>();
            public List<ZJXFJEYF> listBrand = new List<ZJXFJEYF>();
            public List<ZJXFJEYF> listKinds = new List<ZJXFJEYF>();
            public List<Object> lstOther = new List<Object>();
            public List<FLFX> listFLFX = new List<FLFX>();
            public List<FLFXMX> listMD = new List<FLFXMX>();
            public List<FLFXMX> listXH = new List<FLFXMX>();
            public List<PMQSFX> listXFPM = new List<PMQSFX>();
            public List<PMQSFX> listJZFL = new List<PMQSFX>();
            public List<JFZH> listJFZH = new List<JFZH>();
            public List<MDXFFX> listMDXFFX_J = new List<MDXFFX>();
            public List<MDXFFX> listMDXFFX_Y = new List<MDXFFX>();
            public List<XFJL_SP> listXFJLSP = new List<XFJL_SP>();
            public List<SKFS> listSKFS = new List<SKFS>();
            public List<KFPT_TSJL> itemtsjl = new List<KFPT_TSJL>();

        }
        public class ZJXFJEYF
        {
            public int iYearMonth = 0;
            public double fXFJE = 0;
            public int iXFCS = 0;
            public string sName = string.Empty;
            public string sFreeField = string.Empty;
        }
        public class SKFS
        {
            public int ID = 0;
            public string DM = string.Empty;
            public string NAME = string.Empty;
            public Decimal JE = 0;
        }
        public class FLFX
        {
            public string sYHQMC = string.Empty;
            public double fFLJE = 0;
            public double fYQJE = 0;
            public double fQYE = 0;
            public int iBL1 = 0;
            public int iYHQID = 0;
            public string dSYJSRQ = string.Empty;
            public int iJLBH = 0;
        }
        public class FLFXMX
        {
            public string sMDMC = string.Empty;
            public double fCLJF = 0;
            public int iXH = 0;
            public double fFLJE = 0;
        }
        public class PMQSFX
        {
            public int iJD = 0;
            public double fPMBL = 0;
            public int iSJPM = 0;
            public int iZRS = 0;
            public double fXFJE = 0;
            public int iGKFL = 0;
            public string sGKMC = string.Empty;
            public int iBL = 0;
        }
        public class QZHMX
        {
            public int iYHQID = 0;
            public string sYHQMC = string.Empty;
            public string dJSRQ = string.Empty;
            public double fJE = 0;
            public string sMDFWDM = string.Empty;
            public double fJYDJJE = 0;
            public int iHYID = 0;

            public string dCLSJ = string.Empty;
            public double iCLLX = 0;
            public double fJFJE = 0;
            public double fDFJE = 0;
            public double fYE = 0;
            public string sMDMC = string.Empty;
            public string sSKTNO = string.Empty;
            public double iJLBH = 0;
            public double iJYID = 0;
        }
        public class JFZH
        {
            public string fWCLJF = string.Empty, fBQJF = string.Empty, fBNLJJF = string.Empty, fLJJF = string.Empty;
            public string fXFJE = string.Empty, fBNXFJE = string.Empty, fLJXFJE = string.Empty, fYE = string.Empty, fZKJE = string.Empty, fLJZKJE = string.Empty;
            public string sMDMC = string.Empty;
        }
        public class HFJL : DJLR_CLass
        {
            public int iHFJG = 0;
            public string sPSR = string.Empty;
            public string dPSRQ = string.Empty;
            public string sHFNR = string.Empty;
            public string sZGPS = string.Empty;
        }
        public class PSJL : DJLR_CLass
        {
            public int iPSJG = 0;
            public string sPSJG = string.Empty;
            public string sPSNR = string.Empty;
        }
        public class MDXFFX
        {
            public string dNF = string.Empty;
            public int iMDID = 0, iXFCS = 0;
            public string sMDMC = string.Empty;
            public double fXFJE = 0, fZB_JE = 0, fZB_CS = 0;
            public int iJD, iYEARMONTH = 0;
        }
        public class KLX : DJLR_ZX_CLass
        {
            public string sHYKNAME_NEW = string.Empty;
            public string sHYKNAME_OLD = string.Empty;
            public string sHYKHM_OLD = string.Empty;
            public new string sNAME = string.Empty;
            public double fTZJF = 0;
            public int iNEW_STATUS = 0;
            public string sStatusName
            {
                get
                {
                    if (iNEW_STATUS >= -8 && iNEW_STATUS <= 7)
                        return BASECRMDefine.HYXXStatusName[iSTATUS + 8];
                    else
                        return "";
                }
            }
        }
        public class KLX_JFTZ : DJLR_ZX_CLass
        {
            public new double sNAME = 0;
        }
        public class BQMX
        {
            public string sLABEL_VALUE = string.Empty;
            public string sBQRQ = string.Empty;
            public string sLABELXMMC = string.Empty;
            public string dSCRQ = string.Empty, dZHSCRQ = string.Empty;
            public int iBJ_TRANS = 0;
        }
        public class KFPT_TSJL
        {
            public string sTSNR = string.Empty;
            public string sTSJG = string.Empty;
            public string sHYKNO = string.Empty;
            public string sMDMC = string.Empty;
            public int iJLBH = 0;

            public int iMDID = 0;
            public string iZXR = "0";
            public string sZXRMC = string.Empty;
            public string dZXRQ = string.Empty;
            public string iDJR = "0";
            public string sDJRMC = string.Empty;
            public string dDJSJ = string.Empty;
        }

        public static string getTimeSegement(int SJID)
        {
            string Result = "未定义";
            switch (SJID)
            {
                #region TimeSegement
                case 0:
                    Result = "00:00-01:00"; break;
                case 1:
                    Result = "01:00-02:00"; break;
                case 2:
                    Result = "02:00-03:00"; break;
                case 3:
                    Result = "03:00-04:00"; break;
                case 4:
                    Result = "04:00-05:00"; break;
                case 5:
                    Result = "05:00-06:00"; break;
                case 6:
                    Result = "06:00-07:00"; break;
                case 7:
                    Result = "07:00-08:00"; break;
                case 8:
                    Result = "08:00-09:00"; break;
                case 9:
                    Result = "09:00-10:00"; break;
                case 10:
                    Result = "10:00-11:00"; break;
                case 11:
                    Result = "11:00-12:00"; break;
                case 12:
                    Result = "12:00-13:00"; break;
                case 13:
                    Result = "13:00-14:00"; break;
                case 14:
                    Result = "14:00-15:00"; break;
                case 15:
                    Result = "15:00-16:00"; break;
                case 16:
                    Result = "16:00-17:00"; break;
                case 17:
                    Result = "17:00-18:00"; break;
                case 18:
                    Result = "18:00-19:00"; break;
                case 19:
                    Result = "19:00-20:00"; break;
                case 20:
                    Result = "20:00-21:00"; break;
                case 21:
                    Result = "21:00-22:00"; break;
                case 22:
                    Result = "22:00-23:00"; break;
                case 23:
                    Result = "23:00-00:00"; break;
                #endregion
            }
            return Result;
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            string returnResult = string.Empty;
            JCHYXX item = new JCHYXX();
            List<Object> listHYXF = new List<object>();
            CondDict.Clear();
            int myJD = 0;
            switch (iSEARCHMODE)
            {
                case 1:
                    #region 会员基本资料
                    CondDict.Add("sHYK_NO", "H.HYK_NO");
                    double ljxfje = 0;
                    query.SQL.Text = "select SUM(JE) LJXFJE from HYXFJL X, HYK_HYXX H where X.HYID=H.HYID(+)";
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        ljxfje = query.FieldByName("LJXFJE").AsFloat;
                    }
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.Close();
                    //query.SQL.Text = " select H.*,K.HYKNAME,F.XGLDSJ,F.ZDJE,";
                    //query.SQL.Add("  F.LDCS,F.GKJZFL_Z,F.PM,F.HYZSL,F.PM_KFJL,F.HYZSL_KFJL,F.MYD,");
                    //query.SQL.Add("  G.SEX,G.CSRQ,G.SFZBH,G.TXDZ,G.PHONE,G.YZBM,G.ZJLXID,G.ZYID,G.JTSRID,G.XLID,G.QYID,G.LX_SMZQ,G.E_MAIL,G.BZ,");
                    //query.SQL.Add("  G.SJHM,J.WCLJF,J.XFJE,J.BQJF,J.BNLJJF,J.LJJF,J.LJXFJE,J.THCS,J.XFCS,Y.XYDJMC,Z.MC GKJZFLMC");
                    //query.SQL.Add("   from  HYK_HYXX H ,HYKDEF K,HYK_FXXX F  ,HYK_GKDA G ,HYK_JFZH J,HYK_HYXYDJDEF Y,CR_GKJZDEF Z");
                    //query.SQL.Add("   where H.HYKTYPE=K.HYKTYPE and H.HYID=F.HYID(+) and H.GKID=G.GKID(+) and H.HYID=J.HYID(+) and H.XYDJID=Y.XYDJID(+)");
                    //query.SQL.Add("   and F.GKJZFL_Z=Z.GKJZID(+)");
                    query.SQL.Text = " select H.*,K.HYKNAME,";
                    //query.SQL.Add("  F.LDCS,F.GKJZFL_Z,F.PM,F.HYZSL,F.PM_KFJL,F.HYZSL_KFJL,F.MYD,");
                    query.SQL.Add("  G.SEX,G.CSRQ,G.SFZBH,G.TXDZ,G.PHONE,G.YZBM,G.ZJLXID,G.ZYID,G.JTSRID,G.XLID,G.QYID,G.LX_SMZQ,G.E_MAIL,G.BZ,");
                    query.SQL.Add("  G.SJHM,J.WCLJF,J.XFJE,J.BQJF,J.BNLJJF,J.LJJF,J.LJXFJE,J.THCS,J.XFCS");
                    query.SQL.Add("   from  HYK_HYXX H ,HYKDEF K,HYK_GKDA G ,HYK_JFZH J");
                    query.SQL.Add("   where H.HYKTYPE=K.HYKTYPE and H.GKID=G.GKID(+) and H.HYID=J.HYID(+) ");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        #region HYJCXX  会员基础信息
                        item.iHYID = query.FieldByName("HYID").AsInteger;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        item.dJKRQ = FormatUtils.DateToString(query.FieldByName("JKRQ").AsDateTime);
                        item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                        item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        //item.iPM = query.FieldByName("PM").AsInteger;
                        //item.iHYZSL = query.FieldByName("HYZSL").AsInteger;
                        //item.iPM_KFJL = query.FieldByName("PM_KFJL").AsInteger;
                        //item.iHYZSL_KFJL = query.FieldByName("HYZSL_KFJL").AsInteger;
                        item.iSEX = query.FieldByName("SEX").AsInteger;
                        item.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
                        item.sSFZBH = query.FieldByName("SFZBH").AsString;
                        item.sTXDZ = query.FieldByName("TXDZ").AsString;
                        item.sPHONE = query.FieldByName("PHONE").AsString;
                        item.sYZBM = query.FieldByName("YZBM").AsString;
                        item.iZJLXID = query.FieldByName("ZJLXID").AsInteger;
                        item.iZYID = query.FieldByName("ZYID").AsInteger;
                        item.iJTSRID = query.FieldByName("JTSRID").AsInteger;
                        item.iXLID = query.FieldByName("XLID").AsInteger;
                        item.iQYID = query.FieldByName("QYID").AsInteger;
                        item.sEmail = query.FieldByName("E_MAIL").AsString;
                        item.sSJHM = query.FieldByName("SJHM").AsString;
                        //item.iXYDJID = query.FieldByName("XYDJID").AsInteger;
                        //item.sXYDJMC = query.FieldByName("XYDJMC").AsString;
                        item.sBZZY = query.FieldByName("BZ").AsString;
                        /*购买行为评价*/
                        DateTime zhxfrq = new DateTime();
                        zhxfrq = query.FieldByName("ZHXFRQ").AsDateTime;
                        item.dZHXFRQ = FormatUtils.DateToString(zhxfrq);  //最近来店时间
                        //if (item.dZHXFRQ == "")
                        //    item.iFHYTS = (serverTime - query.FieldByName("JKRQ").AsDateTime).Days;
                        //else
                        //    item.iFHYTS = (serverTime - zhxfrq).Days;  //非活跃天数 如果一直没消费过，要按发卡日期即JKRQ                
                        //item.dXGLDSJ = query.FieldByName("XGLDSJ").AsInteger;    //习惯来店时间 某个星期
                        //item.iLDCS = query.FieldByName("LDCS").AsInteger;
                        //item.iXFCS = query.FieldByName("XFCS").AsInteger;  //消费次数
                        DateTime jkrq = new DateTime();
                        jkrq = query.FieldByName("JKRQ").AsDateTime;
                        //Xmonth = (serverTime - jkrq).Days / 30;
                        //item.fLDPL = Math.Round(item.iLDCS * 1.00 / (1 + Xmonth), 2);//来店频率
                        //switch (item.dXGLDSJ)
                        //{
                        //    case 1: item.sXGLDSJ = "星期日"; break;
                        //    case 2: item.sXGLDSJ = "星期一"; break;
                        //    case 3: item.sXGLDSJ = "星期二"; break;
                        //    case 4: item.sXGLDSJ = "星期三"; break;
                        //    case 5: item.sXGLDSJ = "星期四"; break;
                        //    case 6: item.sXGLDSJ = "星期五"; break;
                        //    case 7: item.sXGLDSJ = "星期六"; break;
                        //    default: break;
                        //}


                        /*购买力评价*/
                        item.fWCLJF = Math.Round(query.FieldByName("WCLJF").AsFloat, 2);
                        item.fXFJE = Math.Round(query.FieldByName("XFJE").AsFloat, 2);
                        item.fBQJF = Math.Round(query.FieldByName("BQJF").AsFloat, 2);
                        item.fBNLJJF = Math.Round(query.FieldByName("BNLJJF").AsFloat, 2);
                        item.fLJJF = Math.Round(query.FieldByName("LJJF").AsFloat, 2);
                        item.fLJXFJE = Math.Round(query.FieldByName("LJXFJE").AsFloat, 2);
                        //item.iLDCS = query.FieldByName("LDCS").AsInteger;  //来店次数
                        //if (item.iXFCS > 0)
                        //    //item.fAVE_XFE = Math.Round((item.fLJXFJE * 1.00) / item.iXFCS, 2);  
                        //    item.fAVE_XFE = Math.Round((ljxfje * 1.00) / item.iXFCS, 2);    //平均消费额 累计消费金额要从2016年2月24号开始计算SUM(HYXFJL.XF) 因为消费次数只有2月24号之后的
                        //item.fZDJE = Math.Round(query.FieldByName("ZDJE").AsFloat, 2);     //最大消费额      
                        //item.iTHCS = query.FieldByName("THCS").AsInteger;   //退货次数
                        //item.iPM = query.FieldByName("PM").AsInteger;
                        //item.iHYZSL = query.FieldByName("HYZSL").AsInteger;
                        //if (item.iHYZSL > 0)
                        ////    item.fXFZPM = Math.Round(1 + 100 * item.iPM * 1.00 / item.iHYZSL, 2); //消费总排名  

                        //item.iPM_KFJL = query.FieldByName("PM_KFJL").AsInteger;
                        //item.iHYZSL_KFJL = query.FieldByName("HYZSL_KFJL").AsInteger;
                        //if (item.iHYZSL_KFJL > 0)
                        //    item.fXFPM = Math.Round(1 + 100 * item.iPM_KFJL * 1.00 / item.iHYZSL_KFJL, 2);     //消费排名(客服经理)         

                        ///*综合评价*/
                        //item.fMYD = query.FieldByName("MYD").AsFloat * 100;
                        //item.iGKJZID = query.FieldByName("GKJZFL_Z").AsInteger;
                        //item.sGKJZFLMC = query.FieldByName("GKJZFLMC").AsString;
                        //item.iSMZQ = query.FieldByName("LX_SMZQ").AsInteger;
                        //switch (item.iSMZQ)
                        //{
                        //    case 1: item.sSMZQ = "新顾客"; break;
                        //    case 2: item.sSMZQ = "重新活跃顾客"; break;
                        //    case 3: item.sSMZQ = "活跃顾客"; break;
                        //    case 4: item.sSMZQ = "维护期顾客"; break;
                        //    case 5: item.sSMZQ = "活动度减弱的顾客"; break;
                        //    case 6: item.sSMZQ = "睡眠顾客"; break;
                        //    case 7: item.sSMZQ = "流失顾客"; break;
                        //    default: break;
                        //}
                        #endregion
                    }
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.Close();
                    if (item.iHYID > 0)
                    {
                        #region WGKFJL  会员附加信息
                        query.SQL.Text = "  select R.PERSON_NAME,G.BZ,Y.PERSON_NAME as WGKFJL from HYK_HYXX X,RYXX R,HYK_GKDA G,RYXX Y";
                        query.SQL.Add("  where X.GKID=G.GKID(+) and X.KFRYID_WG=Y.PERSON_ID(+)");
                        query.SQL.Add("  and X.KFRYID=R.PERSON_ID(+) and X.HYID=:HYID");
                        query.ParamByName("HYID").AsInteger = item.iHYID;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            item.sWGKFJL = query.FieldByName("WGKFJL").AsString;
                            item.sKFJL = query.FieldByName("PERSON_NAME").AsString;
                            item.sZY = query.FieldByName("BZ").AsString;
                            item.sKZT = BASECRMDefine.HYXXStatusName[item.iSTATUS + 8];
                        }
                        query.Params.Clear();
                        query.SQL.Clear();

                        query.SQL.Text = "  select  NR from  HYXXXMDEF   where XMID=:XMID";
                        query.ParamByName("XMID").AsInteger = item.iZJLXID;
                        query.Open();
                        if (!query.IsEmpty)
                            item.sZJLXSTR = query.FieldByName("NR").AsString;
                        query.Params.Clear();
                        query.SQL.Clear();

                        query.SQL.Text = "  select QYMC from HYK_HYQYDY where QYID=:QYID";
                        query.ParamByName("QYID").AsInteger = item.iQYID;
                        query.Open();
                        if (!query.IsEmpty)
                            item.sQYMC = query.FieldByName("QYMC").AsString;
                        query.Params.Clear();
                        query.SQL.Clear();
                        #endregion

                        //#region ChartMoneyAndNumber  最近12月消费金额与次数
                        //query.SQL.Text = " select extract(year from sysdate) NF,extract(month from sysdate) YF from dual";
                        //query.Open();
                        //if (!query.IsEmpty)
                        //{
                        //    int tp_yf = query.FieldByName("YF").AsInteger;
                        //    item.iJSNY = query.FieldByName("NF").AsInteger * 100 + tp_yf;
                        //    tp_yf = tp_yf <= 11 ? (tp_yf + 12 - 11) : (tp_yf - 11);
                        //    item.iKSNY = (query.FieldByName("NF").AsInteger - 1) * 100 + tp_yf;
                        //}
                        //query.Params.Clear();
                        //query.SQL.Clear();
                        //query.SQL.Text = "  select YEARMONTH,sum(XFJE) XFJE,sum(XFCS) XFCS  from CR_HYK_XF_R  ";
                        //query.SQL.Add("  where YEARMONTH between :KSNY and :JSNY and  HYID=:HYID");
                        //query.SQL.Add("  group by YEARMONTH order by YEARMONTH");
                        //query.ParamByName("KSNY").AsInteger = item.iKSNY;
                        //query.ParamByName("JSNY").AsInteger = item.iJSNY;
                        //query.ParamByName("HYID").AsInteger = item.iHYID;
                        //query.Open();
                        //int tp_yearMOnth = item.iKSNY;
                        //while (!query.Eof)
                        //{
                        //    ZJXFJEYF itemOne = new ZJXFJEYF();
                        //    itemOne.iYearMonth = query.FieldByName("YEARMONTH").AsInteger;
                        //    itemOne.iXFCS = query.FieldByName("XFCS").AsInteger;
                        //    itemOne.fXFJE = query.FieldByName("XFJE").AsFloat;
                        //    while (itemOne.iYearMonth != tp_yearMOnth && tp_yearMOnth <= item.iJSNY)
                        //    {
                        //        ZJXFJEYF itemNull = new ZJXFJEYF();
                        //        itemNull.iYearMonth = tp_yearMOnth;
                        //        itemNull.iXFCS = 0;
                        //        itemNull.fXFJE = 0;
                        //        item.list.Add(itemNull);
                        //        tp_yearMOnth += 1;
                        //        string strYM = Convert.ToString(tp_yearMOnth);
                        //        int month = Convert.ToInt32(strYM.Substring(strYM.Length - 2));
                        //        int year = Convert.ToInt32(strYM.Substring(0, 4));
                        //        if (month == 13)
                        //        {
                        //            year += 1;
                        //            tp_yearMOnth = Convert.ToInt32(Convert.ToString(year) + "01");
                        //        }
                        //    }
                        //    item.list.Add(itemOne);
                        //    tp_yearMOnth += 1;
                        //    query.Next();
                        //}
                        //query.Params.Clear();
                        //query.SQL.Clear();
                        //while (item.list.Count < 12)
                        //{
                        //    ZJXFJEYF itemNullOther = new ZJXFJEYF();
                        //    if (item.list.Count == 0)
                        //    {
                        //        itemNullOther.iYearMonth = tp_yearMOnth;
                        //    }
                        //    else
                        //    {
                        //        if ((item.list[item.list.Count - 1].iYearMonth + 1) % 100 > 12)
                        //        {
                        //            itemNullOther.iYearMonth = item.list[item.list.Count - 1].iYearMonth + 100 - 11;
                        //        }
                        //        else
                        //        {
                        //            itemNullOther.iYearMonth = item.list[item.list.Count - 1].iYearMonth + 1;
                        //        }
                        //    }
                        //    itemNullOther.fXFJE = 0;
                        //    itemNullOther.iXFCS = 0;
                        //    item.list.Add(itemNullOther);
                        //}
                        //#endregion

                        //#region ChartPieLikeBrand  品牌忠诚度
                        //query.SQL.Text = "  select B.SBMC,sum(XFJE) as XFJE,sum(XFCS) as XFCS,B.SBID  from HYK_SPSB H,SHSPSB B";
                        //query.SQL.Add("  where HYID=:HYID and H.SBID=B.SHSBID group by B.SBMC,B.SBID order by XFJE desc ");
                        //query.ParamByName("HYID").AsInteger = item.iHYID;
                        //query.Open();
                        //double tp_elseMoney = 0;
                        //double tp_totalMoney = 0;
                        //while (!query.Eof)
                        //{
                        //    if (item.listBrand.Count < 3)
                        //    {
                        //        ZJXFJEYF itemBrand = new ZJXFJEYF();
                        //        itemBrand.sName = query.FieldByName("SBMC").AsString;
                        //        itemBrand.fXFJE = query.FieldByName("XFJE").AsFloat;
                        //        tp_totalMoney += itemBrand.fXFJE;
                        //        item.listBrand.Add(itemBrand);
                        //    }
                        //    else
                        //    {
                        //        tp_elseMoney += query.FieldByName("XFJE").AsFloat;
                        //        tp_totalMoney += query.FieldByName("XFJE").AsFloat;
                        //    }
                        //    query.Next();
                        //}
                        //ZJXFJEYF itemBrandElse = new ZJXFJEYF();
                        //itemBrandElse.sName = "其他";
                        //itemBrandElse.fXFJE = tp_elseMoney;
                        //item.listBrand.Add(itemBrandElse);
                        //for (int i = 0; i < item.listBrand.Count; i++)
                        //{
                        //    if (tp_totalMoney != 0)
                        //    {
                        //        item.listBrand[i].fXFJE = Math.Round((item.listBrand[i].fXFJE / tp_totalMoney) * 100, 2);
                        //    }
                        //}
                        //query.Params.Clear();
                        //query.SQL.Clear();
                        //#endregion

                        //#region ChartPieLikeKinds  分类喜好
                        //query.SQL.Text = "  select B.SPFLMC,sum(XFJE) as XFJE,sum(XFCS) as XFCS,B.SPFLID from HYK_SPFL H,SHSPFL B ";
                        //query.SQL.Add("    where HYID=:HYID and  H.SPFLID=B.SHSPFLID  group by B.SPFLMC,B.SPFLID   order by XFJE desc");
                        //query.ParamByName("HYID").AsInteger = item.iHYID;
                        //query.Open();
                        //double tp_KindsElseMoney = 0;
                        //double tp_KindTotalMoney = 0;
                        //while (!query.Eof)
                        //{
                        //    if (item.listKinds.Count < 3)
                        //    {
                        //        ZJXFJEYF itemKinds = new ZJXFJEYF();
                        //        itemKinds.sName = query.FieldByName("SPFLMC").AsString;
                        //        itemKinds.fXFJE = query.FieldByName("XFJE").AsFloat;
                        //        tp_KindTotalMoney += itemKinds.fXFJE;
                        //        item.listKinds.Add(itemKinds);
                        //    }
                        //    else
                        //    {
                        //        tp_KindsElseMoney += query.FieldByName("XFJE").AsFloat;
                        //        tp_KindTotalMoney += query.FieldByName("XFJE").AsFloat;
                        //    }
                        //    query.Next();
                        //}
                        //ZJXFJEYF itemKIndsElse = new ZJXFJEYF();
                        //itemKIndsElse.sName = "其他";
                        //itemKIndsElse.fXFJE = tp_KindsElseMoney;
                        //item.listKinds.Add(itemKIndsElse);
                        //for (int i = 0; i < item.listKinds.Count; i++)
                        //{
                        //    if (tp_totalMoney != 0)
                        //    {
                        //        item.listKinds[i].fXFJE = Math.Round((item.listKinds[i].fXFJE / tp_KindTotalMoney) * 100, 2);
                        //    }
                        //}
                        //query.Params.Clear();
                        //query.SQL.Clear();
                        //query.Close();
                        //#endregion

                    }
                    lst.Add(item);
                    break;
                    #endregion
                case 2:
                    #region 会员消费记录
                    CondDict.Add("iHYID", "H.HYID");
                    CondDict.Add("dXFRQ", "A.XFSJ");
                    query.SQL.Text = " select Z.* from ( ";
                    query.SQL.Add("  select A.Hykno, M.MDMC,A.SKTNO,A.JLBH as XPH,A.XFSJ,A.JE,A.ZK,A.JF,A.JFBS,A.SKYDM,A.BJ_HTBSK ,XFJLID,to_char(XFSJ,'D') as WEEKDAY");
                    query.SQL.Add("  from  HYK_XFJL A,HYK_HYXX H ,MDDY M where (A.HYID=H.HYID or A.HYID_FQ=H.HYID ) and A.MDID=M.MDID ");
                    query.SQL.Add("  and A.STATUS=1 ");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("  union");
                    query.SQL.Add("  select A.Hykno, M.MDMC,A.SKTNO,A.JLBH as XPH,A.XFSJ,A.JE,A.ZK,A.JF,A.JFBS,A.SKYDM,A.BJ_HTBSK ,XFJLID,to_char(XFSJ,'D') as WEEKDAY");
                    query.SQL.Add("  from  HYXFJL A,HYK_HYXX H ,MDDY M where (A.HYID=H.HYID or A.HYID_FQ=H.HYID ) and A.MDID=M.MDID");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add(" )Z   order by Z.XFSJ desc");
                    query.Open();
                    lst.Clear();
                    while (!query.Eof)
                    {
                        XFJL xfjlitem = new XFJL();
                        xfjlitem.sHYKNO = query.FieldByName("Hykno").AsString;
                        xfjlitem.sMDMC = query.FieldByName("MDMC").AsString;
                        xfjlitem.sSKTNO = query.FieldByName("SKTNO").AsString;
                        xfjlitem.iXPH = query.FieldByName("XPH").AsInteger;
                        xfjlitem.dXFSJ = FormatUtils.DatetimeToString(query.FieldByName("XFSJ").AsDateTime);
                        xfjlitem.fJE = query.FieldByName("JE").AsFloat;
                        xfjlitem.fJF = query.FieldByName("JF").AsFloat;
                        xfjlitem.fZK = query.FieldByName("ZK").AsFloat;
                        xfjlitem.fJFBS = query.FieldByName("JFBS").AsFloat;
                        xfjlitem.sSKYDM = query.FieldByName("SKYDM").AsString;
                        xfjlitem.iBJ_HTBSK = query.FieldByName("BJ_HTBSK").AsInteger;
                        xfjlitem.iXFJLID = query.FieldByName("XFJLID").AsInteger;
                        lst.Add(xfjlitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 21:
                    #region 会员消费小票信息
                    CondDict.Add("iXFJLID", "L.XFJLID");
                    query.SQL.Text = "  select S.BMDM,M.BMMC,X.SPMC,sum(S.XSSL) XSSL,sum(S.XSJE) XSJE,sum(S.JF) JF,sum(S.ZKJE) ZKJE,sum(S.ZKJE_HY) ZKJE_HY,";
                    query.SQL.Add("  S.SPDM,S.JFDYDBH,S.JFJS,S.BJ_JFBS from HYXFJL_SP  S,HYXFJL L ,SHSPXX X ,SHBM M");
                    query.SQL.Add("  where  S.XFJLID=L.XFJLID and  S.SHSPID=X.SHSPID and  L.SHDM = M.SHDM and S.BMDM = M.BMDM ");
                    MakeSrchCondition(query, " group by S.BMDM,M.BMMC,X.SPMC,S.SPDM,S.JFDYDBH,S.JFJS,S.BJ_JFBS", false);
                    query.SQL.Add("union");
                    query.SQL.Add("  select S.BMDM,M.BMMC,X.SPMC,sum(S.XSSL) XSSL,sum(S.XSJE) XSJE,sum(S.JF) JF,sum(S.ZKJE) ZKJE,sum(S.ZKJE_HY) ZKJE_HY,");
                    query.SQL.Add("  S.SPDM ,S.JFDYDBH,S.JFJS,S.BJ_JFBS from HYK_XFJL_SP  S,HYK_XFJL L ,SHSPXX X ,SHBM M");
                    query.SQL.Add("   where   S.XFJLID=L.XFJLID and S.SHSPID=X.SHSPID and L.SHDM = M.SHDM and S.BMDM = M.BMDM ");
                    MakeSrchCondition(query, " group by S.BMDM,M.BMMC,X.SPMC,S.SPDM,S.JFDYDBH,S.JFJS,S.BJ_JFBS", false);
                    query.Open();
                    lst.Clear();
                    while (!query.Eof)
                    {
                        XFJL_SP item_xfsp = new XFJL_SP();
                        item_xfsp.sBMDM = query.FieldByName("BMDM").AsString;
                        item_xfsp.sBMMC = query.FieldByName("BMMC").AsString;
                        item_xfsp.fXSSL = query.FieldByName("XSSL").AsFloat;
                        item_xfsp.fXSJE = query.FieldByName("XSJE").AsFloat;
                        item_xfsp.fJF = query.FieldByName("JF").AsFloat;
                        item_xfsp.fZKJE = query.FieldByName("ZKJE").AsFloat;
                        item_xfsp.fZKJE_HY = query.FieldByName("ZKJE_HY").AsFloat;
                        item_xfsp.sSPDM = query.FieldByName("SPDM").AsString;
                        item_xfsp.sSPMC = query.FieldByName("SPMC").AsString;
                        item_xfsp.iJFDYDBH = query.FieldByName("JFDYDBH").AsInteger;
                        item_xfsp.fJFJS = query.FieldByName("JFJS").AsFloat;
                        item_xfsp.iBJ_JFBS = query.FieldByName("BJ_JFBS").AsInteger;
                        item.listXFJLSP.Add(item_xfsp);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    #endregion
                    #region 支付方式
                    query.SQL.Text = "SELECT L.JE,F.ZFFSMC FROM HYK_XFJL_ZFFS L,SHZFFS F WHERE F.SHZFFSID=L.ZFFSID";
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("union");
                    query.SQL.Add(" SELECT L.JE,F.ZFFSMC FROM HYXFJL_ZFFS L,SHZFFS F WHERE F.SHZFFSID=L.ZFFSID");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        SKFS skfs_item = new SKFS();
                        skfs_item.NAME = query.FieldByName("ZFFSMC").AsString;
                        skfs_item.JE = query.FieldByName("JE").AsDecimal;
                        item.listSKFS.Add(skfs_item);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    lst.Add(item);
                    break;
                    #endregion
                case 3:
                    #region HYPPXFFX 会员品牌消费分析
                    CondDict.Add("iHYID", "A.HYID");
                    query.SQL.Text = "  select B.SBMC ,sum(XFJE) as XFJE ,sum(XFCS) as XFCS ,A.SBID  from HYK_SPSB A,SHSPSB B";
                    query.SQL.Add("  where  A.SBID=B.SHSBID");
                    MakeSrchCondition(query, "group by B.SBMC,A.SBID", false);
                    query.SQL.Add("  order by XFJE desc");
                    query.Open();
                    while (!query.Eof)
                    {
                        ZJXFJEYF item_sb = new ZJXFJEYF();
                        item_sb.iYearMonth = query.FieldByName("SBID").AsInteger;//此处获取商标ID
                        item_sb.sName = query.FieldByName("SBMC").AsString;
                        item_sb.iXFCS = query.FieldByName("XFCS").AsInteger;
                        item_sb.fXFJE = query.FieldByName("XFJE").AsFloat;
                        lst.Add(item_sb);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 31:
                    #region HYPPXFFX 会员品牌消费分析
                    item = new JCHYXX();
                    CondDict.Add("iSBID", "A.SBID");
                    CondDict.Add("iHYID", "A.HYID");
                    query.SQL.Text = "  select YEARMONTH,sum(XSJE) XFJE ,sum(XFCS) XFCS,sum(XSSL) XSSL,sum(JF) JF,sum(THCS) THCS,sum(ZKJE) ZKJE";
                    query.SQL.Add("  from CR_HYK_XFMX_RHZ A,MDDY M  where  A.MDID=M.MDID");
                    MakeSrchCondition(query, "group by YEARMONTH", false);
                    query.SQL.Add(" order by YEARMONTH");
                    query.Open();
                    while (!query.Eof)
                    {
                        ZJXFJEYF item_chartsb = new ZJXFJEYF();
                        item_chartsb.iYearMonth = query.FieldByName("YEARMONTH").AsInteger;
                        item_chartsb.fXFJE = query.FieldByName("XFJE").AsFloat;
                        item.listBrand.Add(item_chartsb);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();

                    query.SQL.Text = "  select YEARMONTH,MDMC,A.MDID,sum(XSJE) XFJE ,sum(XFCS) XFCS,sum(XSSL) XSSL,sum(JF) JF,sum(THCS) THCS,sum(ZKJE) ZKJE";
                    query.SQL.Add("  from CR_HYK_XFMX_RHZ A,MDDY M  where  A.MDID=M.MDID");
                    MakeSrchCondition(query, "group by YEARMONTH,MDMC,A.MDID", false);
                    query.SQL.Add(" order by A.MDID,YEARMONTH");
                    query.Open();
                    while (!query.Eof)
                    {
                        XFMX item_mdxfmx = new XFMX();
                        item_mdxfmx.iYEARMONTH = query.FieldByName("YEARMONTH").AsInteger;
                        item_mdxfmx.iMDID = query.FieldByName("MDID").AsInteger;
                        item_mdxfmx.sMDMC = query.FieldByName("MDMC").AsString;
                        item_mdxfmx.fXSJE = query.FieldByName("XFJE").AsFloat;
                        item_mdxfmx.fXSSL = query.FieldByName("XSSL").AsFloat;
                        item_mdxfmx.fJF = query.FieldByName("JF").AsFloat;
                        item_mdxfmx.iXFCS = query.FieldByName("XFCS").AsInteger;
                        item_mdxfmx.fZKJE = query.FieldByName("ZKJE").AsFloat;
                        item_mdxfmx.iTHCS = query.FieldByName("THCS").AsInteger;
                        item.lstOther.Add(item_mdxfmx);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    lst.Add(item);
                    break;
                case 32:
                    CondDict.Add("iYearMonth", "J.YEARMONTH");
                    CondDict.Add("iSHSBID", "X.SHSBID");
                    CondDict.Add("iMDID", "L.MDID");
                    CondDict.Add("iHYID", "L.HYID");
                    query.SQL.Text = " select S.BMDM as DEPTID,B.BMMC,X.SPMC,S.XSJE XFJE,S.JF,S.XSSL,S.ZKJE  from HYXFJL_SP S,HYXFJL L,SHSPXX X,SHBM B,CR_JZQJ J";
                    query.SQL.Add("  where   trunc(L.XFSJ) between J.KSRQ and J.JSRQ and S.SHSPID=X.SHSPID");
                    query.SQL.Add("  and S.XFJLID=L.XFJLID and S.BMDM=B.BMDM");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    lst.Clear();
                    while (!query.Eof)
                    {
                        XFJL_SP item_xfjlsp = new XFJL_SP();
                        item_xfjlsp.fZKJE = query.FieldByName("ZKJE").AsFloat;
                        item_xfjlsp.fXSSL = query.FieldByName("XSSL").AsFloat;
                        item_xfjlsp.fJF = query.FieldByName("JF").AsFloat;
                        item_xfjlsp.fXSJE = query.FieldByName("XFJE").AsFloat;
                        item_xfjlsp.sSPMC = query.FieldByName("SPMC").AsString;
                        item_xfjlsp.sBMMC = query.FieldByName("BMMC").AsString;
                        item_xfjlsp.sBMDM = query.FieldByName("DEPTID").AsString;
                        lst.Add(item_xfjlsp);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.Close();
                    break;
                    #endregion
                case 4:
                    #region HYFLXFFX 会员分类消费分析
                    CondDict.Add("iHYID", "A.HYID");
                    query.SQL.Text = "select B.SPFLDM,B.SPFLMC ,sum(XFJE) XFJE ,sum(XFCS) XFCS";
                    query.SQL.Add(" from HYK_SPFL A,SHSPFL B,SHSPFL C");
                    query.SQL.Add(" where  A.SPFLID=C.SHSPFLID  and B.SHDM=C.SHDM");
                    query.SQL.Add("  and  B.SPFLDM =substr(C.SPFLDM,1,:fljc)");
                    MakeSrchCondition(query, "group by B.SPFLDM,B.SPFLMC", false);
                    query.SQL.Add("  order by XFJE DESC");
                    query.ParamByName("fljc").AsInteger = fljc;
                    lst.Clear();
                    query.Open();
                    while (!query.Eof)
                    {
                        ZJXFJEYF item_fl = new ZJXFJEYF();
                        item_fl.sFreeField = query.FieldByName("SPFLDM").AsString;//此处获取商标ID
                        item_fl.sName = query.FieldByName("SPFLMC").AsString;
                        item_fl.iXFCS = query.FieldByName("XFCS").AsInteger;
                        item_fl.fXFJE = query.FieldByName("XFJE").AsFloat;
                        lst.Add(item_fl);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                case 41:
                    item = new JCHYXX();
                    CondDict.Add("iHYID", "A.HYID");
                    query.SQL.Text = "    select YEARMONTH,sum(XSJE) XFJE ,sum(XFCS) XFCS,sum(XSSL) XSSL,sum(JF) JF,sum(THCS) THCS,sum(ZKJE) ZKJE";
                    query.SQL.Add("    from CR_HYK_XFMX_RHZ A,MDDY M,SHSPFL L    where  A.MDID=M.MDID and A.SPFLID=L.SHSPFLID ");
                    query.SQL.Add(" and L.SPFLDM like '" + spfldm + "%'");
                    MakeSrchCondition(query, " group by YEARMONTH ", false);
                    query.SQL.Add(" order by YEARMONTH");
                    query.Open();
                    while (!query.Eof)
                    {
                        ZJXFJEYF item_chartfl = new ZJXFJEYF();
                        item_chartfl.iYearMonth = query.FieldByName("YEARMONTH").AsInteger;
                        item_chartfl.fXFJE = query.FieldByName("XFJE").AsFloat;
                        item.listBrand.Add(item_chartfl);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();

                    query.SQL.Text = "    select YEARMONTH,MDMC,A.MDID,sum(XSJE) XFJE ,sum(XFCS) XFCS,sum(XSSL) XSSL,sum(JF) JF,sum(THCS) THCS,sum(ZKJE) ZKJE";
                    query.SQL.Add("    from CR_HYK_XFMX_RHZ A,MDDY M,SHSPFL L    where  A.MDID=M.MDID and A.SPFLID=L.SHSPFLID ");
                    query.SQL.Add(" and L.SPFLDM like '" + spfldm + "%'");
                    MakeSrchCondition(query, " group by YEARMONTH,MDMC,A.MDID ", false);
                    query.SQL.Add("  order by A.MDID,YEARMONTH");
                    query.Open();
                    while (!query.Eof)
                    {
                        XFMX item_flmdxfmx = new XFMX();
                        item_flmdxfmx.iYEARMONTH = query.FieldByName("YEARMONTH").AsInteger;
                        item_flmdxfmx.sMDMC = query.FieldByName("MDMC").AsString;
                        item_flmdxfmx.iMDID = query.FieldByName("MDID").AsInteger;
                        item_flmdxfmx.fXSJE = query.FieldByName("XFJE").AsFloat;
                        item_flmdxfmx.fXSSL = query.FieldByName("XSSL").AsFloat;
                        item_flmdxfmx.fJF = query.FieldByName("JF").AsFloat;
                        item_flmdxfmx.iXFCS = query.FieldByName("XFCS").AsInteger;
                        item_flmdxfmx.fZKJE = query.FieldByName("ZKJE").AsFloat;
                        item_flmdxfmx.iTHCS = query.FieldByName("THCS").AsInteger;
                        item.lstOther.Add(item_flmdxfmx);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    lst.Add(item);
                    break;
                case 42:
                    CondDict.Add("iSHSBID", "X.SHSBID");
                    CondDict.Add("iYearMonth", "J.YEARMONTH");
                    CondDict.Add("iMDID", "L.MDID");
                    CondDict.Add("iHYID", "L.HYID");
                    query.SQL.Text = "  select S.BMDM as DEPTID,B.BMMC,X.SPMC,S.XSJE XFJE,S.JF,S.XSSL,S.ZKJE  from HYXFJL_SP S,HYXFJL L,SHSPXX X,SHBM B,CR_JZQJ J,SHSPFL P";
                    query.SQL.Add("  where trunc(L.XFSJ) between J.KSRQ and J.JSRQ and S.SHSPID=X.SHSPID and S.XFJLID=L.XFJLID  ");
                    query.SQL.Add(" and S.BMDM=B.BMDM and X.SHSPFLID=P.SHSPFLID and substr(P.SPFLDM,1,:fljc) =:SPFLDM");
                    MakeSrchCondition(query, "", false);
                    query.ParamByName("fljc").AsInteger = fljc;
                    query.ParamByName("SPFLDM").AsString = spfldm;
                    query.Open();
                    lst.Clear();
                    while (!query.Eof)
                    {
                        XFJL_SP item_xfjlsp = new XFJL_SP();
                        item_xfjlsp.fZKJE = query.FieldByName("ZKJE").AsFloat;
                        item_xfjlsp.fXSSL = query.FieldByName("XSSL").AsFloat;
                        item_xfjlsp.fJF = query.FieldByName("JF").AsFloat;
                        item_xfjlsp.fXSJE = query.FieldByName("XFJE").AsFloat;
                        item_xfjlsp.sSPMC = query.FieldByName("SPMC").AsString;
                        item_xfjlsp.sBMMC = query.FieldByName("BMMC").AsString;
                        item_xfjlsp.sBMDM = query.FieldByName("DEPTID").AsString;
                        lst.Add(item_xfjlsp);
                        query.Next();
                    }
                    break;
                    #endregion
                case 5:
                    #region HYLDXFFX 会员来店消费分析
                    CondDict.Add("iHYID", "A.HYID");
                    item = new JCHYXX();
                    query.SQL.Text = "select SUM(LDCS) ZLDCS from HYK_XGXFSJ A where 1=1";
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    int zldcs = query.FieldByName("ZLDCS").AsInteger;
                    query.Close();

                    query.SQL.Text = " select  case WEEKID when 1 then '星期日'  when 2 then '星期一' when 3 then '星期二'";
                    query.SQL.Add("  when 4 then '星期三'  when 5 then '星期四' when 6 then '星期五'  when 7 then '星期六' end WEEK");
                    query.SQL.Add("     ,SUM(LDCS) LDCS,Round(SUM(LDCS)*100/:ZLDCS,2) as BL  from HYK_XGXFSJ A where 1=1");
                    MakeSrchCondition(query, "group by WEEKID", false);
                    query.ParamByName("ZLDCS").AsInteger = zldcs;
                    query.Open();
                    while (!query.Eof)
                    {
                        ZJXFJEYF item_week = new ZJXFJEYF();
                        item_week.sName = query.FieldByName("WEEK").AsString;
                        item_week.iXFCS = query.FieldByName("LDCS").AsInteger;
                        item_week.fXFJE = query.FieldByName("BL").AsFloat;
                        item.list.Add(item_week);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.SQL.Text = "select sum(XFCS) ZCS from CR_HYK_XF_XG_Y A where 1=1";
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    int zcs = query.FieldByName("ZCS").AsInteger;
                    query.Close();

                    query.SQL.Text = "  select SJID,sum(XFJE) XFJE,sum(XFCS) XFCS,Round(sum(XFCS)*100/:ZCS,2) as BL";
                    query.SQL.Add("  from CR_HYK_XF_XG_Y  A where 1=1");
                    MakeSrchCondition(query, "group by SJID", false);
                    query.SQL.Add(" order by SJID");
                    query.ParamByName("ZCS").AsInteger = zcs;
                    query.Open();
                    while (!query.Eof)
                    {
                        ZJXFJEYF item_time = new ZJXFJEYF();
                        item_time.sName = getTimeSegement(query.FieldByName("SJID").AsInteger);
                        item_time.fXFJE = query.FieldByName("XFJE").AsFloat;
                        item_time.iXFCS = query.FieldByName("XFCS").AsInteger;
                        item_time.sFreeField = query.FieldByName("BL").AsFloat.ToString();
                        item.listBrand.Add(item_time);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.SQL.Text = "  select count(RQ) ZCS from CR_HYK_XF_R A where 1=1";
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    int tp_totalNumber = 0;
                    if (!query.IsEmpty)
                    {
                        tp_totalNumber = query.FieldByName("ZCS").AsInteger;
                    }
                    query.SQL.Clear();
                    if (tp_totalNumber > 0)
                    {
                        query.SQL.Text = "  select '周末' as SJ,sum(XFJE) XFJE ,sum(XFCS) XFCS,count(RQ) LDCS,Round(count(RQ)*100/:ZCS,2) LDBL";
                        query.SQL.Add("  from CR_HYK_XF_R A where   to_char(RQ,'D') in (1,7)");
                        MakeSrchCondition(query, "", false);
                        query.SQL.Add("union");
                        query.SQL.Add("  select '节假日' as SJ,sum(XFJE) XFJE ,sum(XFCS) XFCS,count(RQ) LDCS,Round(count(RQ)*100/:ZCS,2) LDBL");
                        query.SQL.Add("  from CR_HYK_XF_R A,CR_JJR_RQ_DEF J  where   A.RQ between KSRQ and JSRQ");
                        MakeSrchCondition(query, "", false);
                        query.SQL.Add("union");
                        query.SQL.Add(" select '营销活动' as SJ,sum(XFJE) XFJE ,sum(XFCS) XFCS,count(RQ) LDCS,Round(count(RQ)*100/:ZCS,2) LDBL");
                        query.SQL.Add("  from CR_HYK_XF_R A,CR_YXHDDEF J  where  A.RQ between KSRQ and JSRQ");
                        MakeSrchCondition(query, "", false);
                        query.ParamByName("ZCS").AsInteger = tp_totalNumber;
                        query.Open();
                        while (!query.Eof)
                        {
                            ZJXFJEYF item_day = new ZJXFJEYF();
                            item_day.sName = query.FieldByName("SJ").AsString;
                            item_day.fXFJE = query.FieldByName("XFJE").AsFloat;
                            item_day.iXFCS = query.FieldByName("XFCS").AsInteger;
                            item_day.iYearMonth = query.FieldByName("LDCS").AsInteger;
                            item_day.sFreeField = query.FieldByName("LDBL").AsFloat.ToString();
                            item.listKinds.Add(item_day);
                            query.Next();
                        }

                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    lst.Add(item);
                    break;
                case 51:
                    CondDict.Add("iHYID", "A.HYID");
                    if (sLDLX == "周末")
                    {
                        query.SQL.Text = "  select case to_char(RQ,'D') when '1' then '周日' else '周六' end MC,sum(XFJE) XFJE ,";
                        query.SQL.Add("  sum(XFCS) XFCS,count(RQ) LDCS,round(count(RQ) * 100 /:CS,0) LDBL");
                        query.SQL.Add("   from CR_HYK_XF_R A where  to_char(RQ,'D') in (1,7)");
                        MakeSrchCondition(query, " group by to_char(RQ,'D')", false);
                    }
                    else if (sLDLX == "节假日")
                    {
                        query.SQL.Text = "  select J.JJRMC MC,sum(XFJE) XFJE ,sum(XFCS) XFCS,count(RQ) LDCS,round(count(RQ) * 100 /:CS,0) LDBL";
                        query.SQL.Add("  from CR_HYK_XF_R A,CR_JJR_RQ_DEF D ,CR_JJRDEF J");
                        query.SQL.Add("  where   A.RQ between KSRQ and JSRQ and D.JJRID=J.JJRID");
                        MakeSrchCondition(query, " group by J.JJRMC", false);
                    }
                    else
                    {

                        query.SQL.Text = "  select J.HDZT MC ,sum(XFJE) XFJE ,sum(XFCS) XFCS,count(RQ) LDCS,round(count(RQ) * 100 /:CS,0) LDBL";
                        query.SQL.Add("   from CR_HYK_XF_R A,CR_YXHDDEF J");
                        query.SQL.Add("  where   A.RQ between KSRQ and JSRQ");
                        MakeSrchCondition(query, " group by J.HDZT", false);
                    }
                    query.ParamByName("CS").AsInteger = iLDCS;
                    query.Open();
                    //  listHYXF.Clear();
                    item = new JCHYXX();
                    item.iLDCS = 0;
                    while (!query.Eof)
                    {
                        ZJXFJEYF item_dayfx = new ZJXFJEYF();
                        item_dayfx.sName = query.FieldByName("MC").AsString;
                        item_dayfx.fXFJE = query.FieldByName("XFJE").AsFloat;
                        item_dayfx.iXFCS = query.FieldByName("XFCS").AsInteger;
                        item_dayfx.iYearMonth = query.FieldByName("LDCS").AsInteger;
                        item_dayfx.sFreeField = query.FieldByName("LDBL").AsFloat.ToString();
                        item.list.Add(item_dayfx);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    lst.Add(item);
                    break;
                    #endregion
                case 6:
                    #region 购买力分析
                    CondDict.Add("iHYID", "A.HYID");
                    item = new JCHYXX();

                    //query.SQL.Text = "  select A.JGDID,JGDMC,sum(XFJE) XFJE,sum(XFCS) XFCS ,sum(XSSL) XSSL";
                    //query.SQL.Add("  from CR_HYK_XF_XG_R A,SPJGDDY J where A.JGDID=J.JGDID ");
                    query.SQL.Text = "  select A.JGDID,JGDMC,sum(XFJE) XFJE,sum(XFSL) XSSL";
                    query.SQL.Add("  from CR_HYK_XF_JGD_R A,SPJGDDY J where A.JGDID=J.JGDID ");
                    MakeSrchCondition(query, " group by A.JGDID,JGDMC ", false);
                    query.SQL.Add(" order by JGDID");
                    query.Open();
                    while (!query.Eof)
                    {
                        ZJXFJEYF item_gflfx = new ZJXFJEYF();
                        item_gflfx.iYearMonth = query.FieldByName("JGDID").AsInteger;
                        item_gflfx.sName = query.FieldByName("JGDMC").AsString;
                        item_gflfx.fXFJE = query.FieldByName("XFJE").AsFloat;
                        //item_gflfx.iXFCS = query.FieldByName("XFCS").AsInteger;
                        item_gflfx.sFreeField = query.FieldByName("XSSL").AsFloat.ToString();
                        item.list.Add(item_gflfx);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();

                    query.SQL.Text = "  select A.YEARMONTH,sum(XFJE) XFJE,sum(XFCS) XFCS ,max(ZDJE) ZDJE,round(avg(XFJE/XFCS),2) PJJE";
                    query.SQL.Add("  from CR_HYK_XF_R A where 1=1");
                    MakeSrchCondition(query, " group by A.YEARMONTH ", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        ZJXFJEYF item_gmlfxOne = new ZJXFJEYF();
                        item_gmlfxOne.iYearMonth = query.FieldByName("YEARMONTH").AsInteger;
                        item_gmlfxOne.fXFJE = query.FieldByName("XFJE").AsFloat;
                        item_gmlfxOne.iXFCS = query.FieldByName("XFCS").AsInteger;
                        item_gmlfxOne.sName = query.FieldByName("ZDJE").AsFloat.ToString();
                        item_gmlfxOne.sFreeField = query.FieldByName("PJJE").AsFloat.ToString();
                        item.listBrand.Add(item_gmlfxOne);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    lst.Add(item);
                    break;
                    #endregion
                case 7:
                    #region  返利分析
                    CondDict.Add("iHYID", "X.HYID");
                    query.SQL.Text = "select YHQMC,sum(X.FLJE) FLJE,0.0 YQJE,0.0 QYE,0 BL1, X.YHQID,X.SYJSRQ,X.JLBH ";
                    query.SQL.Add(" from HYK_VIPJLCLJL X,YHQDEF Y ");
                    query.SQL.Add(" where X.YHQID=Y.YHQID ");
                    MakeSrchCondition(query, "group by YHQMC,X.YHQID,X.SYJSRQ ,X.JLBH ", false);
                    query.Open();
                    listHYXF.Clear();
                    while (!query.Eof)
                    {
                        FLFX flfxitem = new FLFX();
                        flfxitem.sYHQMC = query.FieldByName("YHQMC").AsString;
                        flfxitem.fFLJE = query.FieldByName("FLJE").AsFloat;
                        flfxitem.fYQJE = query.FieldByName("YQJE").AsFloat;
                        flfxitem.fQYE = query.FieldByName("QYE").AsFloat;
                        flfxitem.iBL1 = query.FieldByName("BL1").AsInteger;
                        flfxitem.iYHQID = query.FieldByName("YHQID").AsInteger;
                        flfxitem.dSYJSRQ = FormatUtils.DateToString(query.FieldByName("SYJSRQ").AsDateTime);
                        flfxitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        item.listFLFX.Add(flfxitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    lst.Add(item);
                    break;
                    #endregion
                case 71:
                    #region  返利明细
                    CondDict.Add("iJLBH", "Y.JLBH");
                    query.SQL.Text = "select MDMC,CLJF from HYK_VIPJLCLJL_MD Y,MDDY M ";
                    query.SQL.Add("  where Y.MDID=M.MDID");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    listHYXF.Clear();
                    while (!query.Eof)
                    {
                        FLFXMX mditem = new FLFXMX();
                        mditem.sMDMC = query.FieldByName("MDMC").AsString;
                        mditem.fCLJF = query.FieldByName("CLJF").AsFloat;
                        item.listMD.Add(mditem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();

                    query.SQL.Text = "select  0 XH,CLJF,FLJE from  HYK_VIPJLCLJL Y where 1=1 ";
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        FLFXMX xhitem = new FLFXMX();
                        xhitem.iXH = query.FieldByName("XH").AsInteger;
                        xhitem.fCLJF = query.FieldByName("CLJF").AsFloat;
                        xhitem.fFLJE = query.FieldByName("FLJE").AsFloat;
                        item.listXH.Add(xhitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    lst.Add(item);
                    break;
                    #endregion
                case 8:
                    #region 顾客价值
                    query.SQL.Text = "select JD from CR_JZQJ_JD where trunc(sysdate) between KSRQ and JSRQ";
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        myJD = query.FieldByName("JD").AsInteger;
                    }
                    #region  顾客价值 消费排名趋势分析
                    CondDict.Add("iHYID", "H.HYID");
                    query.SQL.Text = " select H.JD,round((H.PM - A.PM + 1)  *100 /ZRS,1) PMBL, (H.PM - A.PM + 1) SJPM,A.ZRS,XFJE";
                    query.SQL.Add(" from CR_GKJZFL_JD H,(select JD,min(PM) PM,count(HYID) ZRS from CR_GKJZFL_JD group by JD) A ");
                    query.SQL.Add(" where A.JD=H.JD");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        PMQSFX xfpmxitem = new PMQSFX();
                        xfpmxitem.iJD = query.FieldByName("JD").AsInteger;
                        xfpmxitem.fPMBL = query.FieldByName("PMBL").AsFloat;
                        xfpmxitem.iSJPM = query.FieldByName("SJPM").AsInteger;
                        xfpmxitem.iZRS = query.FieldByName("ZRS").AsInteger;
                        xfpmxitem.fXFJE = query.FieldByName("XFJE").AsFloat;
                        item.listXFPM.Add(xfpmxitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    #endregion
                    #region 顾客价值 价值分类趋势分析
                    query.SQL.Text = "select H.JD,D.GKJZID GKFL,D.MC GKMC ,round(BL*100,0) BL";
                    query.SQL.Add(" from CR_GKJZFL_JD H,CR_GKJZDEF D");
                    query.SQL.Add(" where H.GKFL=D.GKJZID and H.JD <" + myJD);
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        PMQSFX jzflitem = new PMQSFX();
                        jzflitem.iJD = query.FieldByName("JD").AsInteger;
                        jzflitem.iGKFL = query.FieldByName("GKFL").AsInteger;
                        jzflitem.sGKMC = query.FieldByName("GKMC").AsString;
                        jzflitem.iBL = query.FieldByName("BL").AsInteger;
                        item.listJZFL.Add(jzflitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    lst.Add(item);
                    #endregion
                    break;
                    #endregion
                case 9:
                    #region  券账户明细
                    CondDict.Add("iHYID", "B.HYID");
                    query.SQL.Text = "select B.YHQID,Y.YHQMC,B.JSRQ ,B.JE ,B.MDFWDM,B.JYDJJE ";
                    query.SQL.Add(" from HYK_YHQZH B,YHQDEF Y");
                    query.SQL.Add(" where B.YHQID=Y.YHQID");
                    switch (yhqtype)
                    {
                        case 1:
                            query.SQL.Add(" and trunc(B.JSRQ)-trunc(sysdate) >=0");
                            break;
                        case 2:
                            query.SQL.Add(" and trunc(B.JSRQ)-trunc(sysdate) < 0");
                            break;
                        case 3:
                            query.SQL.Add("  and trunc(B.JSRQ)-trunc(sysdate) >=0 and JE >0");
                            break;
                        case 4:
                            query.SQL.Add("  and trunc(B.JSRQ)-trunc(sysdate) >=0 and JE <=0");
                            break;
                    }
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        QZHMX qzhmxitem = new QZHMX();
                        qzhmxitem.iYHQID = query.FieldByName("YHQID").AsInteger;
                        qzhmxitem.sYHQMC = query.FieldByName("YHQMC").AsString;
                        qzhmxitem.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                        qzhmxitem.fJE = query.FieldByName("JE").AsFloat;
                        qzhmxitem.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                        qzhmxitem.fJYDJJE = query.FieldByName("JYDJJE").AsFloat;
                        lst.Add(qzhmxitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 91:
                    #region  券账户明细 对某张券的处理
                    CondDict.Add("iHYID", "B.HYID");
                    CondDict.Add("iYHQID", "B.YHQID");
                    CondDict.Add("dJSRQ", "B.JSRQ");
                    CondDict.Add("sMDFWDM", "D.MDDM");

                    query.SQL.Text = " select  B.CLSJ,B.CLLX,B.JFJE,B.DFJE,B.YE,D.MDMC,B.SKTNO,B.JLBH,B.JYID ";
                    query.SQL.Add(" from HYK_YHQCLJL B,MDDY D");
                    query.SQL.Add(" where B.MDID=D.MDID(+)");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add(" order by B.CLSJ,JYBH ");
                    query.Open();
                    while (!query.Eof)
                    {
                        QZHMX qzhmxitem = new QZHMX();
                        qzhmxitem.dCLSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);
                        qzhmxitem.iCLLX = query.FieldByName("CLLX").AsInteger;
                        qzhmxitem.fJFJE = query.FieldByName("JFJE").AsFloat;
                        qzhmxitem.fDFJE = query.FieldByName("DFJE").AsFloat;
                        qzhmxitem.fYE = query.FieldByName("YE").AsFloat;
                        qzhmxitem.sMDMC = query.FieldByName("MDMC").AsString;
                        qzhmxitem.sSKTNO = query.FieldByName("SKTNO").AsString;
                        qzhmxitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        qzhmxitem.iJYID = query.FieldByName("JYID").AsInteger;
                        lst.Add(qzhmxitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 10:
                    #region  积分账户明细
                    CondDict.Add("iHYID", "B.HYID");
                    query.SQL.Text = " select B.*";
                    query.SQL.Add(" from HYK_JFZH B where 1=1");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        item.fWCLJF = Math.Round(query.FieldByName("WCLJF").AsFloat, 2);
                        item.fBQJF = Math.Round(query.FieldByName("BQJF").AsFloat, 2);
                        item.fBNLJJF = Math.Round(query.FieldByName("BNLJJF").AsFloat, 2);
                        item.fLJJF = Math.Round(query.FieldByName("LJJF").AsFloat, 2);
                        item.fXFJE = Math.Round(query.FieldByName("XFJE").AsFloat, 2);
                        item.fZKJE = Math.Round(query.FieldByName("ZKJE").AsFloat, 2);
                        item.fLJXFJE = Math.Round(query.FieldByName("LJXFJE").AsFloat, 2);
                        item.fLJZKJE = Math.Round(query.FieldByName("LJZKJE").AsFloat, 2);
                        query.Next();
                    }
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.Close();
                    query.SQL.Text = "select D.MDMC,WCLJF,BQJF,BNLJJF,LJJF,XFJE,ZKJE,LJXFJE,LJZKJE ";
                    query.SQL.Add("  from HYK_MDJF B,MDDY D");
                    query.SQL.Add("  where B.MDID=D.MDID");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        JFZH jfzhitem = new JFZH();
                        jfzhitem.sMDMC = query.FieldByName("MDMC").AsString;
                        jfzhitem.fWCLJF = query.FieldByName("WCLJF").AsFloat.ToString("#0.00");
                        jfzhitem.fBQJF = query.FieldByName("BQJF").AsFloat.ToString("#0.00");
                        jfzhitem.fBNLJJF = query.FieldByName("BNLJJF").AsFloat.ToString("#0.00");
                        jfzhitem.fLJJF = query.FieldByName("LJJF").AsFloat.ToString("#0.00");
                        jfzhitem.fXFJE = query.FieldByName("XFJE").AsFloat.ToString("#0.00");
                        jfzhitem.fZKJE = query.FieldByName("ZKJE").AsFloat.ToString("#0.00");
                        jfzhitem.fLJXFJE = query.FieldByName("LJXFJE").AsFloat.ToString("#0.00");
                        jfzhitem.fLJZKJE = query.FieldByName("LJZKJE").AsFloat.ToString("#0.00");
                        item.listJFZH.Add(jfzhitem);
                        query.Next();
                    }
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.Close();
                    lst.Add(item);

                    break;
                    #endregion
                case 11:
                    #region  回访记录
                    CondDict.Add("iHYID", "B.HYID");
                    //query.SQL.Text = "select B.HFJG,DJRMC,DJSJ,ZXRMC PSR,ZXRQ PSRQ,B.HFNR,B.ZGPS from HYHFJL B";
                    query.SQL.Text = " select F.HFJG,F.DJRMC,F.DJSJ,F.BZ from HYK_HDCJJL_HFXX F,HYK_HDCJJL B where F.HFJLBH=B.JLBH(+)";
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        HFJL hfjlitem = new HFJL();
                        hfjlitem.iHFJG = query.FieldByName("HFJG").AsInteger;
                        hfjlitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        hfjlitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        hfjlitem.sBZ = query.FieldByName("BZ").AsString;
                        //hfjlitem.sPSR = query.FieldByName("PSR").AsString;
                        //hfjlitem.dPSRQ = FormatUtils.DateToString(query.FieldByName("PSRQ").AsDateTime);
                        //hfjlitem.sHFNR = query.FieldByName("HFNR").AsString;
                        //hfjlitem.sZGPS = query.FieldByName("ZGPS").AsString;
                        lst.Add(hfjlitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 12:
                    #region  评述记录
                    CondDict.Add("iHYID", "B.HYID");
                    //query.SQL.Text = "select B.PSJG,B.DJRMC,B.DJSJ,B.PSNR from YWYPS B";
                    query.SQL.Text = "select B.*, X.NR from YWYPS B,HYXXXMDEF X where B.PSJG=X.XMID(+) ";
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        PSJL psjlitem = new PSJL();
                        psjlitem.iPSJG = query.FieldByName("PSJG").AsInteger;
                        psjlitem.sPSJG = query.FieldByName("NR").AsString;
                        psjlitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        psjlitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        psjlitem.sPSNR = query.FieldByName("PSNR").AsString;
                        lst.Add(psjlitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 131:
                    #region  挂失
                    CondDict.Add("iHYID", "X.VIPID");
                    query.SQL.Text = "select X.CZJPJ_JLBH as JLBH,X.ZY,X.DJRMC,X.DJSJ,X.ZXRMC,X.ZXRQ ";
                    query.SQL.Add(" from HYK_CZK_KX X ");
                    query.SQL.Add(" where X.LX=0 ");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        DJLR_ZX_CLass listitem = new DJLR_ZX_CLass();
                        listitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        listitem.sZY = query.FieldByName("ZY").AsString;
                        listitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        listitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        listitem.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        listitem.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
                        lst.Add(listitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 132:
                    #region  挂失恢复
                    CondDict.Add("iHYID", "X.VIPID");
                    query.SQL.Text = "select CZJPJ_JLBH as JLBH,ZY,DJRMC,DJSJ,ZXRMC,ZXRQ ";
                    query.SQL.Add(" from HYK_CZK_KX X ");
                    query.SQL.Add(" where X.LX=1");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        DJLR_ZX_CLass listitem = new DJLR_ZX_CLass();
                        listitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        listitem.sZY = query.FieldByName("ZY").AsString;
                        listitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        listitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        listitem.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        listitem.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
                        lst.Add(listitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 133:
                    #region  换卡
                    CondDict.Add("iHYID", "B.HYID");
                    query.SQL.Text = "select B.CZJPJ_JLBH JLBH,B.HYKHM_OLD as sNAME,B.ZY,B.DJSJ,B.DJRMC,B.ZXRMC,B.ZXRQ ";
                    query.SQL.Add("  from HYK_CZK_WK B");
                    query.SQL.Add(" where 1=1");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        KLX listitem = new KLX();
                        listitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        listitem.sNAME = query.FieldByName("sNAME").AsString;
                        listitem.sZY = query.FieldByName("ZY").AsString;
                        listitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        listitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        listitem.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        listitem.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        lst.Add(listitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 134:
                    #region  更换卡类型
                    CondDict.Add("iHYID", "X.HYID");
                    query.SQL.Text = "select JLBH,D.HYKNAME HYKNAME_NEW,F.HYKNAME HYKNAME_OLD,HYKHM_OLD,ZY,DJSJ,DJRMC,ZXRMC,ZXRQ ";
                    query.SQL.Add("  from HYK_GHKLX X,HYKDEF F,HYKDEF D ");
                    query.SQL.Add("   where X.HYKTYPE_OLD=F.HYKTYPE and X.HYKTYPE_NEW=D.HYKTYPE");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        KLX listitem = new KLX();
                        listitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        listitem.sHYKNAME_NEW = query.FieldByName("HYKNAME_NEW").AsString;
                        listitem.sHYKNAME_OLD = query.FieldByName("HYKNAME_OLD").AsString;
                        listitem.sHYKHM_OLD = query.FieldByName("HYKHM_OLD").AsString;
                        listitem.sZY = query.FieldByName("ZY").AsString;
                        listitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        listitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        listitem.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        listitem.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        lst.Add(listitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 135:
                    #region  升级
                    CondDict.Add("iHYID", "X.HYID");
                    query.SQL.Text = "select X.JLBH,D.HYKNAME HYKNAME_NEW,F.HYKNAME HYKNAME_OLD,HYKHM_OLD,ZY,X.DJRMC,X.DJSJ,X.ZXRMC,X.ZXRQ";
                    query.SQL.Add("  from HYK_SJJL X,HYKDEF F,HYKDEF D,HYK_DJSQGZ G");
                    query.SQL.Add("  where G.HYKTYPE=F.HYKTYPE and G.HYKTYPE_NEW=D.HYKTYPE and X.BJ_SJ=1");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        KLX listitem = new KLX();
                        listitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        listitem.sHYKNAME_NEW = query.FieldByName("HYKNAME_NEW").AsString;
                        listitem.sHYKNAME_OLD = query.FieldByName("HYKNAME_OLD").AsString;
                        listitem.sHYKHM_OLD = query.FieldByName("HYKHM_OLD").AsString;
                        listitem.sZY = query.FieldByName("ZY").AsString;
                        listitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        listitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        listitem.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        listitem.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        lst.Add(listitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 136:
                    #region  作废
                    CondDict.Add("iHYID", "B.HYID");
                    query.SQL.Text = "select JLBH,ZY,DJRMC,DJSJ,ZXRMC,ZXRQ";
                    query.SQL.Add("   from HYK_ZFJL Z ");
                    query.SQL.Add("  where exists (select 1 from HYK_ZFJLITEM B where B.JLBH=Z.JLBH");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("  )");
                    query.Open();
                    while (!query.Eof)
                    {
                        DJLR_ZX_CLass listitem = new DJLR_ZX_CLass();
                        listitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        listitem.sZY = query.FieldByName("ZY").AsString;
                        listitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        listitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        listitem.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        listitem.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        lst.Add(listitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 137:
                    #region  有效期更改
                    CondDict.Add("iHYID", "B.HYID");
                    query.SQL.Text = "select CZJPJ_JLBH JLBH,ZY,XYXQ as sNAME,DJRMC,DJSJ,ZXRMC,ZXRQ";
                    query.SQL.Add("   from HYK_YXQBDJL L");
                    query.SQL.Add("  where exists (select 1 from HYK_YXQBDJLITEM B where B.CZJPJ_JLBH=L.CZJPJ_JLBH ");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add(")");
                    query.Open();
                    while (!query.Eof)
                    {
                        KLX listitem = new KLX();
                        listitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        listitem.sNAME = FormatUtils.DateToString(query.FieldByName("sNAME").AsDateTime);
                        listitem.sZY = query.FieldByName("ZY").AsString;
                        listitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        listitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        listitem.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        listitem.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        lst.Add(listitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 138:
                    #region  状态变动
                    CondDict.Add("iHYID", "B.HYID");
                    query.SQL.Text = "select JLBH,ZY,NEW_STATUS,DJRMC,DJSJ,ZXRMC,ZXRQ";
                    query.SQL.Add("  from HYK_ZTBDJL L");
                    query.SQL.Add("  where exists (select 1 from HYK_ZTBDJLITEM B where B.JLBH=L.JLBH");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add(")");
                    query.Open();
                    while (!query.Eof)
                    {
                        KLX listitem = new KLX();
                        listitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        listitem.iNEW_STATUS = query.FieldByName("NEW_STATUS").AsInteger;
                        listitem.sZY = query.FieldByName("ZY").AsString;
                        listitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        listitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        listitem.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        listitem.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        lst.Add(listitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 139:
                    #region  积分调整
                    CondDict.Add("iHYID", "B.HYID");
                    query.SQL.Text = "select JLBH,TZJF as sNAME,ZY,DJRMC,DJSJ,ZXRMC,ZXRQ";
                    query.SQL.Add("  from HYK_JFTZJL B");
                    query.SQL.Add("  where 1=1");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        KLX_JFTZ listitem = new KLX_JFTZ();
                        listitem.iJLBH = query.FieldByName("JLBH").AsInteger;
                        listitem.sNAME = query.FieldByName("sNAME").AsFloat;
                        listitem.sZY = query.FieldByName("ZY").AsString;
                        listitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        listitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        listitem.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        listitem.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        lst.Add(listitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();

                    break;
                    #endregion
                case 14:
                    #region  年门店消费分析
                    CondDict.Add("iHYID", "B.HYID");
                    CondDict.Add("iBMDID", "B.MDID");
                    CondDict.Add("dRQ", "B.CRMJZRQ");
                    int zxfcs = 0;
                    double zxfje = 0;
                    query.SQL.Text = "select count(distinct XFJLID) as ZCS,sum(JE) as ZJE from HYXFJL B where 1=1";
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        zxfcs = query.FieldByName("ZCS").AsInteger;
                        zxfje = query.FieldByName("ZJE").AsFloat;
                    }
                    query.Close();

                    query.SQL.Text = "select to_char(CRMJZRQ,'yyyy') NF,B.MDID,count(distinct XFJLID) as XFCS,sum(JE) as XFJE";
                    query.SQL.Add(" ,round(sum(JE) / :ZJE,4)*100 as ZB_JE,round(count(distinct XFJLID) /:ZCS,4)*100 as ZB_CS,M.MDMC");
                    query.SQL.Add(" from HYXFJL B,MDDY M where B.MDID=M.MDID");
                    MakeSrchCondition(query, "group by to_char(CRMJZRQ,'yyyy'),B.MDID,M.MDMC", false);
                    query.ParamByName("ZCS").AsInteger = zxfcs;
                    query.ParamByName("ZJE").AsFloat = zxfje;
                    query.Open();
                    while (!query.Eof)
                    {
                        MDXFFX mdxffxitem = new MDXFFX();
                        mdxffxitem.dNF = query.FieldByName("NF").AsString;
                        mdxffxitem.iMDID = query.FieldByName("MDID").AsInteger;
                        mdxffxitem.sMDMC = query.FieldByName("MDMC").AsString;
                        mdxffxitem.iXFCS = query.FieldByName("XFCS").AsInteger;
                        mdxffxitem.fXFJE = query.FieldByName("XFJE").AsFloat;
                        mdxffxitem.fZB_JE = query.FieldByName("ZB_JE").AsFloat;
                        mdxffxitem.fZB_CS = query.FieldByName("ZB_CS").AsFloat;
                        lst.Add(mdxffxitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 141:
                    #region 季度门店消费分析
                    CondDict.Add("iHYID", "B.HYID");
                    CondDict.Add("iBMDID", "B.MDID");
                    CondDict.Add("dRQ", "B.CRMJZRQ");
                    int pzxfcs = 0;
                    double pzxfje = 0;
                    query.SQL.Text = "select count(distinct XFJLID) as ZCS,sum(JE) as ZJE from HYXFJL B where 1=1";
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        pzxfcs = query.FieldByName("ZCS").AsInteger;
                        pzxfje = query.FieldByName("ZJE").AsFloat;
                    }
                    query.Close();
                    query.SQL.Text = "select Y.JD,B.MDID,count(distinct XFJLID) as XFCS,sum(JE) as XFJE";
                    query.SQL.Add(",round(sum(JE)/:ZJE,4)*100 as ZB_JE,round(count(distinct XFJLID)/:ZCS,4)*100 as ZB_CS,MDMC");
                    query.SQL.Add(" from HYXFJL B,CR_JZQJ_JD Y,MDDY M");
                    query.SQL.Add(" where B.MDID=M.MDID(+) and B.CRMJZRQ>=Y.KSRQ and B.CRMJZRQ<=Y.JSRQ");
                    MakeSrchCondition(query, "group by Y.JD,B.MDID,MDMC", false);
                    query.SQL.Add(" order by Y.JD");
                    query.ParamByName("ZCS").AsInteger = pzxfcs;
                    query.ParamByName("ZJE").AsFloat = pzxfje;
                    query.Open();
                    while (!query.Eof)
                    {
                        MDXFFX mdxffxitem = new MDXFFX();
                        mdxffxitem.iJD = query.FieldByName("JD").AsInteger;
                        mdxffxitem.iMDID = query.FieldByName("MDID").AsInteger;
                        mdxffxitem.sMDMC = query.FieldByName("MDMC").AsString;
                        mdxffxitem.iXFCS = query.FieldByName("XFCS").AsInteger;
                        mdxffxitem.fXFJE = query.FieldByName("XFJE").AsFloat;
                        mdxffxitem.fZB_JE = query.FieldByName("ZB_JE").AsFloat;
                        mdxffxitem.fZB_CS = query.FieldByName("ZB_CS").AsFloat;
                        item.listMDXFFX_J.Add(mdxffxitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    #endregion
                    #region 月门店消费分析
                    query.SQL.Text = "select Y.YEARMONTH,B.MDID,count(distinct XFJLID) as XFCS,sum(JE) as XFJE";
                    query.SQL.Add(",round(sum(JE)/:ZJE,4)*100 as ZB_JE,round(count(distinct XFJLID)/:ZCS,4)*100 as ZB_CS,MDMC");
                    query.SQL.Add(" from HYXFJL B,CR_JZQJ Y,MDDY M");
                    query.SQL.Add(" where B.MDID=M.MDID(+) and B.CRMJZRQ>=Y.KSRQ and B.CRMJZRQ<=Y.JSRQ");
                    MakeSrchCondition(query, "group by Y.YEARMONTH,B.MDID,MDMC", false);
                    query.SQL.Add(" order by Y.YEARMONTH");
                    query.ParamByName("ZCS").AsInteger = pzxfcs;
                    query.ParamByName("ZJE").AsFloat = pzxfje;
                    query.Open();
                    while (!query.Eof)
                    {
                        MDXFFX mdxffxitem = new MDXFFX();
                        mdxffxitem.iYEARMONTH = query.FieldByName("YEARMONTH").AsInteger;
                        mdxffxitem.iMDID = query.FieldByName("MDID").AsInteger;
                        mdxffxitem.sMDMC = query.FieldByName("MDMC").AsString;
                        mdxffxitem.iXFCS = query.FieldByName("XFCS").AsInteger;
                        mdxffxitem.fXFJE = query.FieldByName("XFJE").AsFloat;
                        mdxffxitem.fZB_JE = query.FieldByName("ZB_JE").AsFloat;
                        mdxffxitem.fZB_CS = query.FieldByName("ZB_CS").AsFloat;
                        item.listMDXFFX_Y.Add(mdxffxitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    lst.Add(item);
                    break;
                    #endregion
                case 15:
                    #region  备注信息
                    CondDict.Add("iHYID", "B.HYID");
                    query.SQL.Text = "select B.DJSJ,B.DJRMC,B.BZ from VIP_BZXX B where 1=1";
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("order by B.DJSJ desc");
                    query.Open();
                    while (!query.Eof)
                    {
                        DJLR_CLass bzxxitem = new DJLR_CLass();
                        bzxxitem.sDJRMC = query.FieldByName("DJRMC").AsString;
                        bzxxitem.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        bzxxitem.sZY = query.FieldByName("BZ").AsString;
                        lst.Add(bzxxitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 16:
                    #region  会员标签
                    CondDict.Add("iHYID", "B.HYID");
                    switch (bqlx)
                    {
                        case 1:  //按日查询
                            query.SQL.Text = "select B.HYID,B.LABELID,X.LABEL_VALUE,R.RQ BQRQ,L.LABELXMMC";
                            query.SQL.Add(" from HYK_HYBQ B,LABEL_XMITEM X,HY_LABEL_BASE_R R,LABEL_XM L ");
                            query.SQL.Add(" where B.LABELID=X.LABELID(+) and B.HYID=R.HYID(+) and B.LABELID=R.LABELID  and B.LABELXMID=L.LABELXMID(+)");
                            query.SQL.Add(" and B.STATUS>=0");
                            break;
                        case 2:   //按月查询
                            query.SQL.Text = "select B.HYID,B.LABELID,X.LABEL_VALUE,R.YEARMONTH BQRQ,L.LABELXMMC";
                            query.SQL.Add(" from HYK_HYBQ B,LABEL_XMITEM X,HY_LABEL_SJ R,LABEL_XM L ");
                            query.SQL.Add(" where B.LABELID=X.LABELID(+) and B.HYID=R.HYID(+) and B.LABELID=R.LABELID  and B.LABELXMID=L.LABELXMID(+)");
                            query.SQL.Add(" and B.STATUS>=0");
                            break;
                    }
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("  order by BQRQ ");
                    query.Open();
                    while (!query.Eof)
                    {
                        BQMX bqitem = new BQMX();
                        bqitem.sLABELXMMC = query.FieldByName("LABELXMMC").AsString;
                        bqitem.sLABEL_VALUE = bqitem.sLABELXMMC + ":" + query.FieldByName("LABEL_VALUE").AsString;
                        if (bqlx == 1)
                            bqitem.sBQRQ = FormatUtils.DateToString(query.FieldByName("BQRQ").AsDateTime);
                        if (bqlx == 2)
                            bqitem.sBQRQ = Convert.ToString(query.FieldByName("BQRQ").AsInteger);

                        lst.Add(bqitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 17:
                    #region  会员标签明细
                    CondDict.Add("iHYID", "B.HYID");
                    query.SQL.Text = "select B.*,X.LABEL_VALUE,L.LABELXMMC";
                    query.SQL.Add(" from HYK_HYBQ B,LABEL_XMITEM X,LABEL_XM L");
                    query.SQL.Add(" where B.LABELID=X.LABELID(+)  and B.LABELXMID=L.LABELXMID(+)");
                    MakeSrchCondition(query, "", false);
                    query.Open();
                    while (!query.Eof)
                    {
                        BQMX bqmxitem = new BQMX();
                        bqmxitem.sLABELXMMC = query.FieldByName("LABELXMMC").AsString;
                        bqmxitem.sLABEL_VALUE = bqmxitem.sLABELXMMC + ":" + query.FieldByName("LABEL_VALUE").AsString;
                        bqmxitem.dSCRQ = FormatUtils.DateToString(query.FieldByName("SCRQ").AsDateTime);
                        bqmxitem.dZHSCRQ = FormatUtils.DateToString(query.FieldByName("ZHSCRQ").AsDateTime);
                        bqmxitem.iBJ_TRANS = query.FieldByName("BJ_TRANS").AsInteger;
                        lst.Add(bqmxitem);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
                case 19:
                    #region  投诉记录
                    CondDict.Add("sHYKNO", "H.HYK_NO");
                    query.SQL.Text = "select H.*,M.MDMC from  TSJLCL H,MDDY M ";
                    query.SQL.Add(" where H.JLBH is not null AND M.MDID=H.MDID");

                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("  order by H.DJSJ ");
                    query.Open();
                    while (!query.Eof)
                    {
                        KFPT_TSJL itemtsjl = new KFPT_TSJL();
                        lst.Add(itemtsjl);
                        itemtsjl.sHYKNO = query.FieldByName("HYK_NO").AsString;
                        itemtsjl.iJLBH = query.FieldByName("JLBH").AsInteger;
                        itemtsjl.iMDID = query.FieldByName("MDID").AsInteger;
                        itemtsjl.sTSNR = query.FieldByName("TSNR").AsString;
                        itemtsjl.sTSJG = query.FieldByName("TSJG").AsString;
                        itemtsjl.sMDMC = query.FieldByName("MDMC").AsString;
                        itemtsjl.iDJR = query.FieldByName("DJR").AsInteger.ToString();
                        itemtsjl.sDJRMC = query.FieldByName("DJRMC").AsString;
                        itemtsjl.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        itemtsjl.iZXR = query.FieldByName("ZXR").AsInteger.ToString();
                        itemtsjl.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        itemtsjl.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        query.Next();
                    }
                    query.Params.Clear();
                    query.SQL.Clear();
                    query.Close();
                    break;
                    #endregion
            }
            return lst;
        }
    }
}

