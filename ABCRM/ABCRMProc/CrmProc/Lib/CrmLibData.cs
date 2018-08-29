using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using BF.Pub;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BF.CrmProc
{
    public class BASECRMDefine
    {
        public static byte[] DesKey = { (byte)'P', (byte)'d', (byte)'f', (byte)'S', (byte)'s', (byte)'o', (byte)'D', (byte)'i' };
        public enum CZK_DKGZCLLX
        {
            CZK_CLLX_JK = 0,                                                               //建卡
            CZK_CLLX_XK = 1,                                                             //写卡
            CZK_CLLX_LY = 2,                                                             //领用
            CZK_CLLX_DB = 3,                                                             //调拨
            CZK_CLLX_TL = 4,                                                             //退领
            CZK_CLLX_KCZF = 5,                                                           //库存卡作废
            CZK_CLLX_KCZFHF = 6,                                                         //库存卡作废恢复
            CZK_CLLX_KCBGYXQ = 7,                                                        //库存卡有效期变动
            CZK_CLLX_KCJETZ = 8,                                                         //库存卡金额调整
            CZK_CLLX_FS = 9,                                                             //发售
            CZK_CLLX_TS = 10,                                                            //退售
            CZK_CLLX_CK = 11,                                                            //存款
            CZK_CLLX_QK = 12,                                                            //取款
            CZK_CLLX_ZC = 13,                                                            //转储
            CZK_CLLX_QL = 14,                                                            //余额清零
            CZK_CLLX_ZTBD = 15,                                                          //状态变动
            CZK_CLLX_ZF = 16,                                                            //作废
            CZK_CLLX_HK = 17,                                                            //换卡
            CZK_CLLX_GS = 18,                                                            //挂失
            CZK_CLLX_GSHF = 19,                                                          //挂失恢复
            CZK_CLLX_BKCK = 20,                                                          //并卡拆卡
            CZK_CLLX_HS = 21,                                                            //回收
            CZK_CLLX_YXQYC = 22,                                                         //有效期延长
            CZK_CLLX_QTXF = 23                                                           //前台消费
        }

        public enum HYXXStatus
        {
            HYXX_STATUS_HS = -8,                                                        //无效卡(应回收卡)
            HYXX_STATUS_JJ = -7,                                                        //无效卡(应降级卡)
            HYXX_STATUS_ZZ = -6,                                                        //无效卡(终止卡)
            HYXX_STATUS_ZD = -5,                                                        //无效卡(纸券已经消费)
            HYXX_STATUS_TY = -4,                                                        //无效卡(停用卡)
            HYXX_STATUS_KX = -3,                                                        //无效卡(挂失卡)
            HYXX_STATUS_TK = -2,                                                        //无效卡(退卡)
            HYXX_STATUS_ZF = -1,                                                        //无效卡(作废卡)
            HYXX_STATUS_FS = 0,                                                         //有效卡(发售卡)
            HYXX_STATUS_XF = 1,                                                         //有效卡(已消费卡)
            HYXX_STATUS_KK = 2,                                                         //有效卡(升级卡)
            HYXX_STATUS_SM = 3,                                                         //有效卡(睡眠卡)
            HYXX_STATUS_YC = 4,                                                         //有效卡(异常卡）
            HYXX_STATUS_XM = 5,                                                         //休眠卡
            HYXX_STATUS_DZ = 6,                                                         //呆滞卡
            HYXX_STATUS_SJ = 7                                                         //升级卡
        }

        public static string[] HYXXStatusName =
        {
             "无效卡(应回收卡)",
             "无效卡(应降级卡)",
             "无效卡(终止卡)",
             "无效卡(纸券已经消费)",
             "无效卡(停用卡)",
             "无效卡(挂失卡)",
             "无效卡(退卡)",
             "无效卡(作废卡)",
             "有效卡(发售卡)",
             "有效卡(已消费卡)",
             "有效卡(升级卡)",
             "有效卡(睡眠卡)",
             "有效卡(异常卡）",
             "休眠卡",
             "呆滞卡",
             "(有效卡)可升级"
        };

        public const int CZK_CLLX_JK = 0;                               //'建卡记录';
        public const int CZK_CLLX_CK = 1;                               //'存款记录';
        public const int CZK_CLLX_QK = 2;                               // '取款记录';
        public const int CZK_CLLX_ZF = 3;                               //'作废记录';
        public const int CZK_CLLX_BD = 4;                               //'有效期变动';
        public const int CZK_CLLX_BK = 5;                               //并卡
        public const int CZK_CLLX_TK = 6;                               //退卡
        public const int CZK_CLLX_XF = 7;                               //消费
        public const int CZK_CLLX_KX = 8;                               //挂失
        public const int CZK_CLLX_WK = 9;                               //换卡
        public const int CZK_CLLX_FQ = 10;                              //前台返券
        public const int CZK_CLLX_TZ = 11;                              //销售调整
        public const int CZK_CLLX_QL = 12;                              //余额清零

        public const int HYK_JFBDCLLX_QTXF = 31;                        //前台消费积分
        public const int HYK_JFBDCLLX_JFBDD = 32;                       //积分变动单
        public const int HYK_JFBDCLLX_JFTZD = 33;                       //积分调整单
        public const int HYK_JFBDCLLX_JFZC = 34;                        //积分转储
        public const int HYK_JFBDCLLX_JFFLZX_CZ = 35;                   //积分返利执行与冲正
        public const int HYK_JFBDCLLX_QMJF = 36;                        //用钱买积分
        public const int HYK_JFBDCLLX_JFQL = 37;                        //积分清零
        public const int HYK_JFBDCLLX_GHKLX = 38;                       //更换卡类型
        public const int HYK_JFBDCLLX_SJHK = 39;                        //升级换卡
        public const int HYK_JFBDCLLX_JJHK = 40;                        //降级换卡
        public const int HYK_JFBDCLLX_CZKFKSF = 41;                     //储值卡发卡送分
        public const int HYK_JFBDCLLX_WGJF = 42;                        //网购积分
        public const int HYK_JFBDCLLX_JFDX = 43;                        //积分抵现

        public static string[] CZK_CLLX =
        {
            "建卡",
            "存款",
            "取款",
            "作废",
            "有效期变动",
            "并卡",
            "退卡",
            "消费",
            "挂失",
            "换卡",
            "前台返券",
            "消费调整",
            "余额清零"
        };

        public static string[] ZFFSType =
        {    "一般收款",
    "信用卡(一体)",
    "储值卡",
    "优惠券(纸券)",
    "优惠券(卡)",
    "IC卡",
    "支票",
    "银行卡(国内)",
    "银行卡(国外)",
    "信贷卡",
    "未入帐",
    "承兑"
        };

        public static string[] CXHDLX =
        {
            "","积分","折扣","返券","用券","满减"
        };

        public static string[] LPFFGZLX =
        {
            "生日礼","首刷礼","办卡礼","积分返礼","来店礼"
        };

        public const int HYKLYD_LYD = 0;
        public const int HYKLYD_DBD = 1;
        public const int HYKLYD_TLD = 2;

        public enum DJStatus
        {
            LR,
            SH,
            QD,
            ZZ
        }

        public string[] DJStatusName =
        {
            "录入",
            "审核",
            "启动",
            "终止"
        };

        public enum OperateType
        {
            OprSave,
            OprDelete,
            OprExec,
            OprUnexec,
            OprStrat,
            OprStop,
            OprShow,
            OprSearch,
        }

        public static string[] OperateTypeName =
        {
            "保存",
            "删除",
            "审核",
            "取消审核",
            "启动",
            "终止",
            "显示",
            "查询"
        };

        public enum KCKStatus
        {
            JK,
            LY,
            FS
        }

        public string[] KCKStatusName =
        {
            "建卡",
            "领用",
            "发售"
        };

        public static string[] ZKCLLXName =
        {
            "制卡",
            "验卡",
            "补磁"
        };


        public enum CLLX_LPBD
        {
            LPBD_JH,
            LPBD_BC,
            LPBD_BR,
            LPBD_TH,
            LPBD_FF,
            LPBD_ZF,
            LPBD_SY
        }

        public class HYXXStatus2
        {
            public int iStatus;
            public string sStatus = string.Empty;
        }
    }

    public class MenuPermit
    {
        public int iPERSON_ID = 0;
        public int iMSG_ID = 0;
        public int iBJ_FJQX1 = 0;
        public int iBJ_FJQX2 = 0;
        public int iBJ_FJQX3 = 0;
        public int iBJ_FJQX4 = 0;
    }
    public class CRMLIBASHX
    {
        //方便crmlib.ashx传参数用，别尼玛什么都写到HYXX_Detail里
        public int iJLBH = 0;
        public int iRYID = 0;
        public string sRYDM = string.Empty;
        public string sRYMC = string.Empty;
        public string sPASSWORD = string.Empty;
        public int iMODE = 0;
        public int iQX = 0;
        public int iMDID = 0;
        public int iZK = 0;
        public int iSK = 0;
        public string sSHDM = string.Empty;
        public string sBMDM = string.Empty;
        public int iMENUID = 0;
        public int iHYKTYPE = 0;
        public int iYHQID;
        public int iCXID;
        // 认为不合理、暂时这么写
        public string sCZKHM = string.Empty;
        public string sDBConnName = string.Empty;
        public string sCDNR = string.Empty;
        public string sCZKHM_BEGIN = string.Empty;
        public string sCZKHM_END = string.Empty;
        public string sBGDDDM = string.Empty;
        public int iSTATUS = 0;
        public int iSL = 0;
        public int iXMLX = 0;
        public int iXMID = 0;
        public string dCSRQ = string.Empty;
        public string sSFZBH = string.Empty;
        public string sSJHM = string.Empty;
        public int iHYID = 0;
        public int iBJ_CZK = 0;
        public int iKZID = 0;
        public string sHYK_NO = string.Empty;
        public int iSJ = 0;
        public double fBQJF = 0;
        public double fXFJE = 0;
        public int iXFJLID = 0;
        public int iXPH = 0;
        public string sSKTNO = string.Empty;
        public string dPDRQ = string.Empty;
        public int iBJ_TY = 0;
        public int iLEVEL = 0;
        public int iFLGZBH = 0;
        public int iLABELLBID = 0;
        public int iLABELXMID = 0;
        public int iGZLX = 0;
        public string sGZLX = string.Empty;
        public string sHYKNAME = string.Empty;
        public double fQCYE = 0, fYXTZJE = 0, fPDJE = 0, fKFJE = 0;
        public string sBGDDMC = string.Empty, dYXQ = string.Empty, dXKRQ = string.Empty;
        public int iFXDWID = 0;
        public int iBJ_KCK = 0;
        public double fCLJF = 0;
        public JFFLGZ FLGZ;
        public int iSKJLBH = 0;
        public int iID = 0;

        //微信
        public string sTABLENAME = string.Empty;
        public string sFIELD = string.Empty;
        public string sNBDM = string.Empty;

        public int iPUBLICID = 0;
        public int iTYPE = 0;
        public string sMSGTYPE = string.Empty;
        public int iQFDX = 0;
        public string sDM = string.Empty, sNAME = string.Empty, sURL = string.Empty;
        public int iGZID = 0;
        public DateTime dKSRQ;
        public DateTime dJSRQ;
        public bool iBJ_CHECK = false;
        public int iGKID = 0;
        public string sOPENID = string.Empty;
        public string sMSG_ID = string.Empty;
        //拍照
        public string sData = string.Empty;
        public string sDir = string.Empty;
        public string sFileName = string.Empty;
        //金额账户
        public double fYE = 0;
        public double fCZJE = 0;
        //
        public int iQYID = 0;
        //储值卡
        public double fZSJF = 0;
        public MZKGL.MZKGL_MZKFS.findkd ZKMX;

        public string sHYK_NO1 = string.Empty;
        public string sHYK_NO2 = string.Empty;
        public string dDJSJ1 = string.Empty;
        public string dDJSJ2 = string.Empty;
        public int iFP_FLAG = 0;
        public string sXKCDNR = string.Empty;


    }
    public class DJLR_CLass : BASECRMClass
    {
        public int iSTATUS = 0;
        public string sBGDDDM = string.Empty;
        public string sBGDDMC = string.Empty;
        public string sZY = string.Empty;
        public int iDJR = 0;
        public string sDJRMC = string.Empty;
        public string dDJSJ = string.Empty;
        public int iDJLX = 0;
        public string sBZ
        {
            set { sZY = value; }
            get { return sZY; }
        }
        //public string sDJLX
        //{
        //    get { return CrmLibProc.GetLPFFGZLXName(iDJLX); }
        //}
    }

    public class DJLR_ZX_CLass : DJLR_CLass
    {
        public int iZXR = 0;
        public string sZXRMC = string.Empty;
        public string dZXRQ = string.Empty;
        public int iSHR
        {
            set { iZXR = value; }
            get { return iZXR; }
        }
        public string sSHRMC
        {
            set { sZXRMC = value; }
            get { return sZXRMC; }
        }
        public string dSHRQ
        {
            set { dZXRQ = value; }
            get { return dZXRQ; }
        }
    }

    //以下为常用单据类
    public class DJLR_BRBCDD_CLass : DJLR_ZX_CLass//拨入拨出类单据，有两个保管地点
    {
        public string sBGDDDM_BR = string.Empty, sBGDDMC_BR = string.Empty;
        public string sBGDDDM_BC = string.Empty, sBGDDMC_BC = string.Empty;
    }

    public class DJLR_XG_CLass : DJLR_CLass//带修改人的
    {
        public int iXGR = 0;
        public string sXGRMC = string.Empty;
        public string dXGSJ = string.Empty;
    }

    public class DJLR_ZXQDZZ_CLass : DJLR_ZX_CLass//带启动人终止人的
    {
        public int iQDR = 0;
        public string sQDRMC = string.Empty;
        public string dQDSJ = string.Empty;
        public int iZZR = 0;
        public string sZZRMC = string.Empty;
        public string dZZRQ = string.Empty;
        //以下是个别非主流字段叫法，用属性来实现
        public string dQDRQ
        {
            set { dQDSJ = value; }
            get { return dQDSJ; }
        }
        public string dZZSJ
        {
            set { dZZRQ = value; }
            get { return dZZRQ; }
        }
    }

    public class HYK_DJLR_CLass : DJLR_ZX_CLass//会员卡单据录入
    {
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
    }

    public class HYKYHQ_DJLR_CLass : HYK_DJLR_CLass//会员卡优惠券单据录入
    {
        public int iYHQID = -1;
        public string sYHQMC = string.Empty;
        public string sMDFWDM = string.Empty;
        public string dJSRQ = string.Empty;
        public int iCXID = 0;
        public string sCXZT = string.Empty;
        public string sMDMC = string.Empty;
    }

    public class DZHYK_DJLR_CLass : HYK_DJLR_CLass//单张会员卡单据录入
    {
        public int iHYID = 0;
        public string sHY_NAME = string.Empty;
        public string sHYKNO = string.Empty;
        public string sHYK_NO
        {
            set { sHYKNO = value; }
            get { return sHYKNO; }
        }
    }

    public class DZHYKYHQCQK_DJLR_CLass : DZHYK_DJLR_CLass//单张会员卡优惠券存取款
    {
        public int iYHQID = -1;
        public string sYHQMC = string.Empty;
        public string sMDFWDM = string.Empty;
        public string dJSRQ = string.Empty;
        public int iCXID = 0;
        public string sCXZT = string.Empty;
        public double fYYE = 0;
    }

    public class JFFLGZ : DZHYKYHQCQK_DJLR_CLass
    {
        public int iFLGZBH = 0;
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public double fFLJE = 0;
        public string dYHQJSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iMDID = 0;
        public string sMDDM = string.Empty;
        public string sMDMC = string.Empty;
        public string sGZMC = string.Empty;
        public List<JFFLGZItem> itemTable = new List<JFFLGZItem>();

    }

    public class JFFLGZItem
    {
        public int iXH = 0;
        public double fJFXX = 0;
        public double fJFSX = 0;
        public double fFLBL = 0;
        public int iCLFS = 0;
        public double fCLJF = 0;
        public double fFQJE = 0;
    }
    public class JFFQ
    {
        public double fZCLJF = 0;
        public double fZFQJE = 0;
        public List<JFFQITEM> itemTable1 = new List<JFFQITEM>();
    }
    public class JFFQITEM
    {
        public int iGridId;
        public double fCLJF = 0;
        public double fFQJE = 0;
    }
    public class MZKSKD_Detail : DJLR_ZX_CLass
    {
        public int iKHID = 0;
        public int iHYKTYPE = 0;
        public string dYXQ = string.Empty;
        public int iSKSL = 0;
        public decimal cSSJE = 0;
        public string sKHMC = string.Empty;
        public string sMZKHH = string.Empty;
        public int iBJ_QKGX = 0;
        public double fDHJE = 0;
        public int iBJ_TS = 0;
        public int iYHQID = 0;
        public string sYHQMC = string.Empty;
        public double fSJZSJE = 0;
        public double fSJZSJF = 0;
        public double iYXQTS = 0;
        //       public int iBJ_JKD = -1;
        //      public int iDKHLX = -1;
    }

    //以下为常用类
    public class BGDD : BASECRMClass
    {
        public string sBGDDDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sPBGDDDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sBGDDMC
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public string sBGDDQC
        {
            set { sFULLNAME = value; }
            get { return sFULLNAME; }
        }
        public int bZK_BJ = 0;
        public int bXS_BJ = 0;
        public int bTY_BJ = 0;
        public int bMJBJ = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public int iChecked = 0;
    }
    public class PUBLICID : BASECRMClass
    {
        public int iPUBLICID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sPUBLICNAME = string.Empty;
        public string sPUBLICIF = string.Empty;
    }
    public class WXCD : BASECRMClass
    {
        public int JLBH = 0, iPUBLICID;

        public string sDM = string.Empty;
        public string sPDM = string.Empty;                       //父代码
        public string sASK = string.Empty;
        public int iTYPE = 0;
        public int iASKID = 0;
        public string sURL = string.Empty;
        public string sNBDM = string.Empty;
        public string sMC = string.Empty;
        public string sDMQC = string.Empty;
        public int iGZJLBH = 0;
        public string sTEXT = string.Empty;
        public string sTITLE = string.Empty;
        public string sDESCRIPTION = string.Empty;
        public string sMUSICURL = string.Empty;
        public string sTYPE = string.Empty;
        public List<WXNews> arrItem = new List<WXNews>();
    }
    public class WXNews
    {
        public string sDESCRIBE = string.Empty;
        public string sURL = string.Empty;
        public string sNAME = string.Empty;
        public string sIMG = string.Empty;
        public int iINX = 0;
    }
    public class WXGZ : BASECRMClass
    {
        public int iID = 0;
        public string sASK = string.Empty;
        //public string sPDM = string.Empty;                       //父代码
        //public string sNAME = string.Empty;
        //public int iTYPE = 0;
        //public string sURL = string.Empty;
        //public string sNBDM = string.Empty;
        //public string sMC = string.Empty;
        //public string sDMQC = string.Empty;
    }
    public class YDDDXFLDY : BASECRMClass
    {
        public int iFLID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sFLMC = string.Empty;
        public string sXFLMC = string.Empty;
    }
    public class HYKDEFBase : BASECRMClass
    {
        //修改了卡种和卡类型的继承关系
        public int iFXFS = 0;
        public string sFXFS
        {
            get { return iFXFS == 1 ? "外部卡" : "自发行卡"; }
        }
        public int iHMCD = 0;
        public string sKHQDM = string.Empty;
        public string sKHHZM = string.Empty;
        public int iFFDX = 0;
        public string sYXQCD = string.Empty;
        public int iBJ_PSW = 0;
        public int iBJ_XSJL = 0;
        public int iBJ_JF = 0;
        public int iBJ_YHQZH = 0;
        public int iBJ_CZZH = 0;
        public int iBJ_CZK = 0;
        public int iYHFS = 0;
        public double fFKQDXFJE = 0;
        public int iKXBJ = 0;
        public int iTKBJ = 0;
        public int iZFBJ = 0;
        public int iHYKJCID = 0;
        public double fKFJE = 0;
        public int iBJ_CDNRJM = 0;
        public int iCDJZ = 0;
        public string sCDJZ
        {
            get { return iCDJZ == 1 ? "IC卡" : "磁卡"; }
        }
        public int iBJ_CZYHQ = 0;
        public int iBJ_XTZK = 0;
        public int iFS_YXQ = 0;
        public string sFS_YXQ
        {
            get { return iFS_YXQ == 1 ? "售卡时指定" : "建卡时指定"; }
        }
        public int iBJ_QZYK = 0;
        public int iBJ_CX = 0;
        public int iBJ_ZQHY = 0;
        public int iBJ_TH = 0;
        public int iBJ_FPGL = 0;
        public int iBJ_XK = 0;
        public int iBJ_TS = 0;
        public int iBJ_FSK = 0;
        public int iFS_SYMD = 0;
        public int iJFCLFWFS = 0;
        public double fJFXX = 0;
        public int iBJ_YZM = 0;
        public int iTHBJ = 0;
        public int iBJ_ZERO = 0;
        public int iYZMCD = 0;
        public int iBJ_POSXK = 0;
        public double fCZJEMIN = 0;
        public double fCZJEMAX = 0;
        public int iBJ_CK = 0;
        public int iBJ_QK = 0;
        public int iBJ_JEZC = 0;
        public int iBJ_LMZK = 0;
        public string sSJJZQ = string.Empty;
        public double fFJBL = 0;
        public int iBJ_JFQL = 0;
        public int iBJ_WX = 0;
        public string sKBJC = string.Empty;
        public int iBJ_QTFK = 0;
        public double fBKJE = 0;
        public int iBJ_XFSJ = 0;
        public double fCZJESX = 0;
    }

    public class HYKKZDEF : HYKDEFBase
    {
        public int iHYKKZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sHYKKZNAME
        {
            set { sNAME = value; }
            get { return sNAME; }
        }

    }
    public class HYKDJDEF
    {
        public int iHYKKZID = 0, iHYKJCID = 0;
        public string sHYKJCNAME = string.Empty;
    }
    public class HYKDEF : HYKDEFBase
    {
        public int iHYKTYPE
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sHYKDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sHYKPDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sHYKNAME
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public int iHYKKZID = 0;
        public string sHYKKZNAME = string.Empty;
    }
    public class YHQDEF : BASECRMClass
    {
        public int iYHQID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sYHQMC = string.Empty;
        public int iFS_YQMDFW = 0, iFS_FQMDFW = 0, iYXQTS = 0;
        public string sFS_YQMDFW
        {
            get
            {
                switch (iFS_YQMDFW)
                {
                    case 1: return "按集团";
                    case 2: return "按商户";
                    case 3: return "按门店";
                    default: return "";
                }
            }
        }
        public string sFS_FQMDFW
        {
            get
            {
                switch (iFS_FQMDFW)
                {
                    //case 1: return "按集团";
                    case 2: return "按商户";
                    case 3: return "按门店";
                    default: return "";
                }
            }
        }
        public int iBJ_DZYHQ = 0, iBJ_TY = 0, iBJ_FQ = 0, iBJ_TS = 0, iBJ_CXYHQ = 0, iFQLX = 0;
        public string sBJ_TS
        {
            get
            {
                switch (iBJ_TS)
                {
                    case 0: return "一般券";
                    case 1: return "礼品券";
                    case 2: return "抽奖券";
                    case 3: return "积分券";
                    case 4: return "促销积分券";
                    case 5: return "积分抵现券";
                    default: return "";
                }
            }
        }
        public string sFQLX
        {
            get
            {
                switch (iFQLX)
                {
                    case 0: return "按商品";
                    case 2: return "按支付送";
                    case 3: return "开卡礼";
                    default: return "";
                }
            }
        }
        public int iISCODED = 0, iCODELEN = 0;
        public string sCODEPRE = string.Empty, sCODESUF = string.Empty;
        //public YHQDEF()
        //{
        //    iJLBH = -10;
        //}
    }

    public class CXHDYHQ : BASECRMClass
    {
        public int iYHQID = -10;
        public string sYHQMC = string.Empty;
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public int iCXID = -10;
        public string sCXZT = string.Empty;
        public string dYHQSYJSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string sZQDW = string.Empty;
        public string sZQBZ1 = string.Empty;
        public string sZQBZ2 = string.Empty;
        public int iYXQTS = 0;
        public int iCXHDBH = 0;
    }

    public class CXHD : DJLR_ZX_CLass
    {
        public int iCXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public int iCXHDBH = 0;
        public string sCXZT = string.Empty;
        public string sCXNR = string.Empty;
        public int iNIAN = 0;
        public int iCXQS = 0;
        public string dKSSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string dJSSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string dSCSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string sCXQC = string.Empty;
        public string sCXDM = string.Empty;
        public string sPCXDM = string.Empty;
    }
    public class SHDY : BASECRMClass
    {
        public string sSHDM
        {
            set { sJLBH = value; }
            get { return sJLBH; }
        }
        public string sSHMC = string.Empty;
        public string sOldSHDM = string.Empty;
        public string sBMJC = string.Empty;
        public string sBMJC_Full = string.Empty;
    }
    public interface ISHDY
    {
        string sSHDM { get; set; }
        string sSHMC { get; set; }
    }
    public class FTPCONFIG : BASECRMClass
    {

        public string sIP_PUB = string.Empty;
        public string sPSWD = string.Empty;
        public string sDIR = string.Empty;
        public string sIP_NET = string.Empty;
    }
    public class MOBILE_SHDY : BASECRMClass
    {
        public string sSHDM = string.Empty;

        public string sSHMC = string.Empty;

    }
    public class MOBILE_MDDY : BASECRMClass
    {
        public int iMDID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sMDMC = string.Empty;

    }
    public class CY_CTLXDY : BASECRMClass
    {
        public int iCTLXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iCTID = 0;
        public string sCTLXNAME = string.Empty;

    }
    public class CY_CTXXDY : BASECRMClass
    {
        public int iCTID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sCTMC = string.Empty;

    }

    public class APP_LPFLDY : BASECRMClass
    {
        public int iLPFLID
        {
            get { return iJLBH; }
            set { iJLBH = value; }
        }
        public string sLPFLDM = string.Empty;
        public string sLPFLMC = string.Empty;

    }
    public class CY_CZLXDY : BASECRMClass
    {
        public int iCZLXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iCTID = 0;
        public string sCZLXNAME = string.Empty;

    }

    public class CY_CPLXDY : BASECRMClass
    {
        public int iCPLXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iCTID = 0;
        public string sCPLXNAME = string.Empty;

    }

    public class MOBILE_LMSHDY : BASECRMClass
    {
        public int iID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sMC = string.Empty;

    }
    public class MOBILE_QYDY : BASECRMClass
    {
        public int iQYID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sQYMC = string.Empty;

    }
    public class JPJC : BASECRMClass
    {
        public int iJC = 0;
        public string sMC = string.Empty;
    }
    public class GXSJDY : BASECRMClass
    {
        public int iQYID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sQYMC = string.Empty;
        public int iQYLX = 0, iFTPBJ = 0, iSTATUS = 0, iTM = 0;
    }
    public class MDDY : BASECRMClass
    {
        public int iMDID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sMDDM = string.Empty;
        public string sMDMC = string.Empty;
        public string sGXSHDM = string.Empty;
        public int iSQID = 0;
    }
    public class CITY : BASECRMClass
    {
        public int iCITYID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sCSMC = string.Empty;
    }

    public class JCDEF : BASECRMClass
    {
        public int iJC
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sMC = string.Empty;
    }
    public class JJR : BASECRMClass
    {
        public int iJJRID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sJJRMC = string.Empty;
    }
    public class HYHDDY : BASECRMClass
    {
        public int iHDID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sHDMC = string.Empty;
    }
    public class HYHDXX : DJLR_ZX_CLass
    {
        public int iHDID, iKFRYID, iRS, iPSR = 0;
        public int iBMRS, iQRRS, iCJRS, iHFRS = 0;
        public double fPF = 0;
        public string sHDMC, sKFRYMC, sLDPY, sPSRMC = string.Empty;
        public string dKSSJ, dJSSJ, dPSSJ = string.Empty;
        //活动回访
        public int iHFJLBH, iHFJG = 0;
        public int iSFPS = 0;
        public string sCJBZ = string.Empty;
    }
    public class RWJL : DJLR_ZX_CLass
    {
        public int iRWDX = 0, iRWWCZT = 0;
        public double fFZ = 0;
        public string sRWZT, sRW, sWCQK, sHYKNO, sHYNAME, sLDPY = string.Empty;
        public string iPYR = "0";
        public string dKSRQ, dJSRQ, sPYRMC, dPYRQ = string.Empty;
    }

    public class SMZQDEF : BASECRMClass
    {
        public int iLBID = 0;
        public string sLBMC = string.Empty;
    }
    public class CXHDDEF : BASECRMClass
    {
        public int iCXID = 0;
        //{
        //    set { iJLBH = value; }
        //    get { return iJLBH; }
        //}
        public string sCXZT = string.Empty;
        //public string sSHMC = string.Empty;
        //public string sMDDM = string.Empty;
        //public string sMDMC = string.Empty;
        //public string sGXSHDM = string.Empty;
        //public int iSQID = 0;
    }

    public class QZLXMC : BASECRMClass
    {

        //{
        //    set { iJLBH = value; }
        //    get { return iJLBH; }
        //}
        public string sQZLXMC = string.Empty;
        public int iQZCYRS = 0;
        //public string sSHMC = string.Empty;
        //public string sMDDM = string.Empty;
        //public string sMDMC = string.Empty;
        //public string sGXSHDM = string.Empty;
        //public int iSQID = 0;
    }

    public class JXCDB : BASECRMClass
    {
        public int iMDID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sMDDM = string.Empty;
        public string sIP = string.Empty;
        public string sPORT = string.Empty;
        public string sDBNAME = string.Empty;
        public string sUSERNAME = string.Empty;
        public string sPASSWORD = string.Empty;
        public int iMDLX = 0;
        public int iBJ_BH = 0;
    }


    public class GTPT_YY : HYXX_Detail
    {
        public string sMC = string.Empty;
        public string dRQ = string.Empty;
        public string sFWNR = string.Empty;
        public string sGKXM = string.Empty;
        public int iID = 0;
    }

    public class HYXX : DJLR_ZX_CLass
    {
        public int iHYID = 0;
        public string sHYK_NO = string.Empty, sCDNR = string.Empty;
        public int iHYKTYPE = 0;
        public int iBJ_XFJE = 0;                                        //升级判断标准
        public string sHYKNAME = string.Empty;
        public string sHY_NAME = string.Empty;
        //public string sHY_NAME
        //{
        //    set
        //    {
        //        byte[] DesKey = { (byte)'P', (byte)'d', (byte)'f', (byte)'S', (byte)'s', (byte)'o', (byte)'D', (byte)'i' };
        //        byte[] key = Encoding.ASCII.GetBytes(EncryptUtils.DesEncryptCardTrackSecondly(iHYID.ToString(), DesKey).Substring(0, 8));
        //        _sHY_NAME = EncryptUtils.DecryptWebData(value, key);
        //    }
        //    get
        //    {
        //        return _sHY_NAME;
        //        //byte[] DesKey = { (byte)'P', (byte)'d', (byte)'f', (byte)'S', (byte)'s', (byte)'o', (byte)'D', (byte)'i' };
        //        //byte[] key = Encoding.ASCII.GetBytes(EncryptUtils.DesEncryptCardTrackSecondly(iHYID.ToString(), DesKey).Substring(0, 8));
        //        //return EncryptUtils.EncryptWebData(sHY_NAME0, key);
        //    }
        //}
        public string dYXQ = DateTime.MinValue.ToString("yyyy-MM-dd"), dJKRQ = DateTime.MinValue.ToString("yyyy-MM-dd");
        public int iMDID = 0, iGKID = 0;
        public string sMDMC = string.Empty;
        public string sStatusName
        {
            get
            {
                if (iSTATUS >= -8 && iSTATUS <= 7)
                    return BASECRMDefine.HYXXStatusName[iSTATUS + 8];
                else
                    return "";
            }
        }
        public string sPASSWORD = string.Empty;

        //add iFXDW sFXDWMC iSXMDID sSXMDMC 介似嘛???
        public int iFXDW = 0, iSXMDID = 0;
        public string sFXDWMC = string.Empty, sSXMDMC = string.Empty;
        public string sFXDWDM = string.Empty;
        public string sFFXDWMC = string.Empty;
        //add 
        public int iCDJZ = 0, iBJ_PARENT = 0;
        public int iBJ_CHILD = 0;

        //以下是查询条件用
        public string sHYKNO_Begin = string.Empty, sHYKNO_End = string.Empty;
        public int iSL = 0;
        public int iBJ_PSW = 0;
        public int iPUBLICID = 0;
        //卡费
        public double fKFJE = 0;
    }
    public class SQD_Detail : DJLR_BRBCDD_CLass
    {
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public int iSL = 0;
        public int iHYKSL = 0;
    }
    public class HYXX_Detail : HYXX
    {
        //附属卡相关字段
        public int iMAINHYID = 0;
        //
        public string sWX_NO = string.Empty;
        public string sOPENID = string.Empty;
        //KLX
        public int iBJ_CZZH = 0;
        public int iBJ_GS = 0;
        public int iBJ_TK = 0;
        public int iBJ_ZF = 0;
        //RYXX相关字段
        public string sPERSON_NAME = string.Empty;
        // public int iLoginRYID = 0;
        //JFZH相关字段
        public double fWCLJF = 0, fBQJF = 0, fBNLJJF = 0, fLJJF = 0;
        public double fWCLXFJE = 0, fXFJE = 0, fBNXFJE = 0, fLJXFJE = 0, fYE = 0, fZKJE = 0, fLJZKJE = 0;
        public int iXFCS = 0, iTHCS = 0;
        //JEZH
        public double fCZJE = 0, fQCYE = 0, fYXTZJE = 0, fPDJE = 0, fJYDJJE = 0;
        //YHQZH
        public double fYHQJE = 0, fYHQJYDJJE = 0;
        public string dYHQJSRQ = string.Empty;
        //GRXX
        public int iSEX = -1, iCANSMS = 0;
        public string dCSRQ = string.Empty, dJHJNR = string.Empty;
        public int iNL = 0;
        public string sSEX
        {
            get
            {
                if (iSEX == 0)
                    return "男";
                else if (iSEX == 1)
                    return "女";
                else
                    return "";
            }
        }
        // sSEX = string.Empty;
        public int iQYID = 0, iMZID = 0, iBJ_CLD = 0;
        public int iZJLXID = 0, iZYID = 0, iXLID = 0, iJTSRID = 0, iZSCSJID = 0, iJTGJID = 0, iJTCYID = 0;
        public string sZJLXMC = string.Empty;                           //, iZYID = 0, iXLID = 0, iJTSRID = 0, iZSCSJID = 0, iJTGJID = 0, iJTCYID = 0;
        public string sGK_NAME = string.Empty, sSFZBH = string.Empty, sQYMC = string.Empty, sTXDZ = string.Empty, sYZBM = string.Empty;
        public string sGZDW = string.Empty, sZW = string.Empty, sYYAH = string.Empty, sCXXX = string.Empty, sXXFS = string.Empty;
        public string sSJHM = string.Empty;
        public int iKFRYID = 0;
        public string sHYKNO_BEGIN = string.Empty;
        public string sHYKNO_END = string.Empty;
        public int iKSY, iKSR, iJSY, iJSR, iCOUNT, iMDSL, iHFSL, _iJLBH = 0;
        public string sHYKTYPE = string.Empty;
        public string sSX, sXZ, sCM, sSW, sXKCDNR, sCANVAS = string.Empty;
        //会员服务相关
        public int iFWNRID = 0;
        public double fFWSL, fZDFWSL, fSYFWSL = 0;
        public string sFWNRMC;
        public int iBJ_XZFWSL = 0;
        public string sDW = string.Empty;
        //会员标签相关，尼玛谁放到这里的
        //public int iLABELID, iSTATUS, iLABELXMID, iRS, iLABELXMLX, iLABEL_VALUEID, iBJ_WY = 0;
        //public string sLABELMC, sLABELXMMS, sLABELXMQC, sLABELXMMC, sPLABELXMDM, sLABELXMDM, sLABEL_VALUE = string.Empty;
        //会员商圈相关
        public int iXQID = 0, iSQID = 0;
        public string sXQMC = string.Empty, sSQMC = string.Empty, sXQDM = string.Empty, sQYDM = string.Empty;
        //会员卡升降级
        public int iHYKTYPE_NEW = 0;
        public string sHYKNAME_NEW = string.Empty;
        public int iGZID = 0, iINX = 0;
        public int iBJ_SJ = 0;

        //通讯地址相关
        public string sROAD = string.Empty;
        public string sHOUSENUM = string.Empty;
        //会员头像
        public string sIMGURL = string.Empty;
        public int iBJ_FSK = 0;

        //public string sSJHM
        //{
        //    set
        //    {
        //        byte[] DesKey = { (byte)'P', (byte)'d', (byte)'f', (byte)'S', (byte)'s', (byte)'o', (byte)'D', (byte)'i' };
        //        byte[] key = Encoding.ASCII.GetBytes(EncryptUtils.DesEncryptCardTrackSecondly(iGKID.ToString(), DesKey).Substring(0, 8));
        //        _sSJHM = EncryptUtils.DecryptWebData(value, key);
        //    }
        //    get
        //    {
        //        return _sSJHM;
        //        //byte[] DesKey = { (byte)'P', (byte)'d', (byte)'f', (byte)'S', (byte)'s', (byte)'o', (byte)'D', (byte)'i' };
        //        //byte[] key = Encoding.ASCII.GetBytes(EncryptUtils.DesEncryptCardTrackSecondly(iHYID.ToString(), DesKey).Substring(0, 8));
        //        //return EncryptUtils.EncryptWebData(sHY_NAME0, key);
        //    }
        //}

        public string sPHONE = string.Empty, sBGDH = string.Empty, sFAX = string.Empty, sEMAIL = string.Empty;
        public string sCPH = string.Empty;
        public string sQIYEMC = string.Empty, sQIYEXZ = string.Empty, sGKNC = string.Empty, sMZMC = string.Empty;

        public string iGXR = "0";
        public string sGXRMC = string.Empty;
        public string dGXSJ = string.Empty;


        //ZY ADD
        public int iBJ_YZZJLX = -1, iBJ_YZSJHM = -1;
        //推荐人;大客户标记;qq;wx;wb
        public int iTJRYID = -1;
        public string sQQ = string.Empty, sWX = string.Empty, sWB = string.Empty;
        public string sTJRYMC = string.Empty;
        //小区;TONGXUNDIZHI 1-4
        public string sXQ = string.Empty;
        //public string sTXDZ1 = string.Empty, sTXDZ2 = string.Empty, sTXDZ3 = string.Empty, sTXDZ4 = string.Empty;
        public string sPPXQ = string.Empty;

        //
        public int iQCPPID = -1;
        public int iJHBJ = -1;
        //public string sQCPP = string.Empty, sHYZK = string.Empty;
        public string sPPHY = string.Empty, sKHJLMC = string.Empty;
        public int iKHJLRYID = -1;
        public string sCanvas = string.Empty;
        //public int iTJRYID = -1;
        public string sKHDM = string.Empty, sKHMC = string.Empty, sPYM = string.Empty;
        public string sLXRXM = string.Empty;
        public string sLXRZJ = string.Empty, sLXRDH = string.Empty;
        public int iLX = 0;

        public string sHYKHM_OLD = string.Empty;
        public string sHYKHM_NEW = string.Empty;

        public string dKSRQ = string.Empty;

        //查询存卡信息相关
        //public List<HYXX_Detail> SubCardTable = new List<HYXX_Detail>();
        public int iSKJLBH = 0;
        public string sHYK_NO1 = string.Empty;
        public string sHYK_NO2 = string.Empty;
        public string dDJSJ1 = string.Empty;
        public string dDJSJ2 = string.Empty;
        public int iFP_FLAG = 0;
        public string sCZKHM = string.Empty;
        public int iBJ_ZK = 0;
        public double fJE = 0;
        public string sBINDCODE = string.Empty;//微信卡包会员卡号



    }

    public class GKDA : HYXX_Detail//DJLR_XG_CLass LBC修改
    {

    }
    public class HYSIGN : DJLR_ZXQDZZ_CLass
    {
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        //public string dQDRQ = string.Empty;
        public int iLX = 0;
        public int iHYKTYPE = 0;
        //public int iSHR = 0;
        //public string sSHRMC = string.Empty;
        //public string dSHRQ = string.Empty;

    }
    public class INX : BASECRMClass
    {
        public int iINX = 0;
        public int iXH = 0;
        public int iPAGE_ID = 0;
        public string sIP_NET = string.Empty;
        public string sPSWD = string.Empty;
        public string sDIR = string.Empty;
        public string sIP_PUB = string.Empty;
    }

    public class WX_CXHDDEF : BASECRMClass
    {
        public string sCXZT = string.Empty;
        public string sCXNR = string.Empty;
        public string dSTART_RQ1 = string.Empty;
        public string dEND_RQ1 = string.Empty;
        public string dSTART_RQ2 = string.Empty;
        public string dEND_RQ2 = string.Empty;
        public string dSTART_RQ = string.Empty;
        public string dEND_RQ = string.Empty;
        public int iCXID = 0;
    }
    public class MZKXX : HYXX
    {
        public double fJE = 0, fQCYE = 0, fYXTZJE = 0, fPDJE = 0, fJYDJJE = 0;
    }
    //zhangyu2014 0820 add----------------------------------------------------------------------
    /// <summary>
    /// 顾客档案圈子
    /// </summary>
    public class GKDAQZ : BASECRMClass
    {
        public int iQZID = 0, iHYID = 0;
        public string sQZDM = string.Empty, sQZMC = string.Empty, sBZ = string.Empty;
    }
    //zhangyu2014 0820 add------------------------end add---------------------------------------

    public class KCKXX : BASECRMClass
    {
        public string sCZKHM = string.Empty, sCDNR = string.Empty;
        public string sXKCDNR = string.Empty;
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public string dYXQ = DateTime.MinValue.ToString("yyyy-MM-dd"), dXKRQ = DateTime.MinValue.ToString("yyyy-MM-dd");
        public int iSTATUS = 0;
        public string sSTATUSNAME = string.Empty;
        public double fQCYE = 0, fYXTZJE = 0, fPDJE = 0, fKFJE = 0;
        public int iFS_YXQ;
        public string sBGDDDM = string.Empty, sBGDDMC = string.Empty;
        public int iBGR = 0;
        public int iBJ_YK = 0;
        public int iBJ_PSW = 0;
        public int iBJ = 0;
        public int iSKJLBH = 0;
        public string sBGRMC = string.Empty;
        public string sYZM = string.Empty;
        public string sKHQDM = string.Empty;
        public string sKHHZM = string.Empty;
        //以下是查询条件用
        public string sCZKHM_BEGIN = string.Empty, sCZKHM_END = string.Empty;
        public int iSL = 0;
        public int iHF;
        public int iFXDWID;
        public int iMDID;
        public string sHM_MIN = string.Empty, sHM_MAX = string.Empty;
        public string sPERSON_NAME = string.Empty, sFXDWMC = string.Empty;
        public double fMZJE = 0;
        public int iPDPC = 0;
        public string sBJMC = string.Empty;
    }

    public class KCKHD : BASECRMClass
    {
        public string sCZKHM_BEGIN = string.Empty, sCZKHM_END = string.Empty;
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public double fMZJE = 0;
        public int iSKSL = 0;
        public string sHM_MIN = string.Empty, sHM_MAX = string.Empty;
        public double fJE = 0;
        public int iSL = 0;
        public int iBGR = 0;
        public string sBGRMC = string.Empty;
        public int iBJ_PSW = 0;
    }

    public class MDJF
    {
        public int iMDID = 0;
        public double fWCLJF = 0, fBQJF = 0, fBNLJJF = 0, fLJJF = 0;
        public double fXFJE = 0;
    }

    public class YHQZH : HYXX
    {
        public int iYHQID = -10, iCXID = -1;
        public string sYHQMC = string.Empty, sMDFWDM = string.Empty, sMDFWMC = string.Empty, sCXZT = string.Empty;
        public int iFS_YQMDFW = 0;
        public string sMDFWDM1 = string.Empty, sMDFWDM2 = string.Empty;
        public string dJSRQ;
        public double fJE = 0;
        public string dJSSJ1 = string.Empty;
        public string dJSSJ2 = string.Empty;
        public string sHYK_NO_Begin = string.Empty;
        public string sHYK_NO_End = string.Empty;
    }

    public class GTPT1 : HYXX
    {
        public int iYHQID = -10, iCXID = -1;
        public string sYHQMC = string.Empty, sMDFWDM = string.Empty, sMDFWMC = string.Empty, sCXZT = string.Empty;
        public int iFS_YQMDFW = 0;
        public string sMDFWDM1 = string.Empty, sMDFWDM2 = string.Empty;
        public string dJSRQ;
        public double fJE = 0;
        public string dJSSJ1 = string.Empty;
        public string dJSSJ2 = string.Empty;
        public string sHYK_NO_Begin = string.Empty;
        public string sHYK_NO_End = string.Empty;
    }

    public class YHQSYSH : YHQDEF
    {
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public int iBJ_SYD = 0;
    }

    public class FXDW : BASECRMClass
    {
        public int iFXDWID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sFXDWDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sPFXDWDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sFXDWMC
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public string sFXDWQC
        {
            set { sFULLNAME = value; }
            get { return sFULLNAME; }
        }
        public int iHMCD = 0;
        public string sKHQDM = string.Empty;
        public string sKHHZM = string.Empty;
        public int iBJ_TY = 0;
        public int iMJBJ = 0;
    }

    public class HYXXXMDY : BASECRMClass
    {
        public int iXMID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iXMLX = -1;
        public int iSXH = 0;
        public string sNR = string.Empty;
    }

    public class HYXYDJDY : BASECRMClass
    {
        public int iXYDJID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sXYDJMC = string.Empty;
        public int iXYDJYS = 0;
        public int iFXZQ = 0;
        public int iTHCS = 0;
        public int iXFCS_TGZ = 0;
        public int iPPS_TLC = 0;
        public string sBZ = string.Empty;
        public int iHYID = 0;
    }

    public class NLDDY : BASECRMClass
    {
        public string sNLDMC = string.Empty;
        public int iNL1 = 0, iNL2 = 0;
    }

    public class YHXX : BASECRMClass
    {
        public int iYHID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sYHMC = string.Empty;
        public List<YHXXITEM> itme = new List<YHXXITEM>();
    }

    public class YHXXITEM : BASECRMClass
    {
        public int iID = 0;
        public int iYHID = 0;
        public int iINX = 0;
        public string sCODE1 = string.Empty;
        public string sCODE2 = string.Empty;
        public int iBJ_TY = 0;
        public string sSHDM = string.Empty;
        public int iCXID = 0;
    }

    public class SHZFFS : BASECRMClass
    {
        public int iSHZFFSID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sPZFFSDM = string.Empty;
        public string sZFFSDM = string.Empty;
        public string sZFFSMC = string.Empty;
        public int iMJBJ = 0;
        public int iZFFSID = 0;
        public int iBJ_JF = 0;
        public int iBJ_FQ = 0;
        public int iYHQID = 0;
        public string sYHQMC = string.Empty;
        public int iBJ_MBJZ = 0;
        public double fJFBL = 1;
        public string sTYPECODE = string.Empty;
        public int iTYPE = 0;
    }

    public class QMFGZDY : BASECRMClass
    {
        public double fJF_J = 0;
        public double fJF_N = 0;
        public int iBJ_TY = 0;
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public double fJF_JC = 0;
        public double fJF_TS = 0;
    }

    public class SPJGDDY : BASECRMClass
    {
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sJGDMC = string.Empty;
        public double fLSDJ1 = 0, fLSDJ2 = 0;
    }

    public class SDDEF : BASECRMClass
    {
        public int iCODE
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sTITLE = string.Empty;
        public int iKSSJ = 0, iJSSJ = 0;
    }

    public class HYQY : BASECRMClass
    {
        public int iQYID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        //public int iQYID = 0;
        public string sQYDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sPQYDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sQYMC
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public string sQYQC
        {
            set { sFULLNAME = value; }
            get { return sFULLNAME; }
        }
        public string sYZBM = string.Empty;
        public int iBJ_MJ = 0;
    }

    public class SQDY : BASECRMClass
    {
        public int iSQID, iMDID = 0;
        public string sSQMC = string.Empty, sSQMS = string.Empty;
        public string sMDMC = string.Empty;
    }

    public class ZFFS : BASECRMClass
    {
        public int iZFFSID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sZFFSDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sPZFFSDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sZFFSMC
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public string sZFFSQC
        {
            set { sFULLNAME = value; }
            get { return sFULLNAME; }
        }
        public int iBJ_MJ = 0;
        public int iTYPE = 0;
        public string sTYPE = string.Empty;
        public int iBJ_DZQDCZK = 0;
    }

    public class HYZLX : BASECRMClass
    {
        public int iHYZLXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sHYZLXDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sPHYZLXDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sHYZLXMC
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public int iBJ_MJ = 0;
        // HYK_HYGRP中字段
        public int iGRPID;
        public string sGRPMC = string.Empty;
        public string sGRPMS = string.Empty;
    }

    public class ZYLX : BASECRMClass
    {
        public int iZYID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sZYDM = string.Empty, sPZYDM = string.Empty;
        public string sZYMC = string.Empty, sZYQC = string.Empty;
        public int iBJ_MJ = 0;
        public int iBJ_TY = 0;
    }

    public class LPXX : DJLR_XG_CLass
    {
        public int iLPID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sLPMC = string.Empty, sLPDM = string.Empty, sLPGG = string.Empty;
        public double fLPDJ, fLPJJ, fLPJF, fZSJF;
        public int iBJ_DEL = 0, iBJ_WKC = 0;
        public string sGJBM = string.Empty;
        public int iLPFLID = 0;
        public string sLPFLMC = string.Empty;
        public int iLPLX = 0;
        public string sLPLXMC = string.Empty;
        public double fKCSL = 0;
        public int iJC = 0;
        public string sJCMC = string.Empty;
        public int iLBID = 0;
        public string sDJLX
        {
            get { return CrmLibProc.GetLPFFGZLXName(iDJLX); }
        }
    }

    public class LPFLDY : BASECRMClass
    {
        public int iLPFLID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sLPFLDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sPLPFLDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sLPFLMC
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public string sLPFLQC
        {
            set { sFULLNAME = value; }
            get { return sFULLNAME; }
        }

        public int iBJ_MJ = 0;
        public int iBJ_TY = 0;
    }
    public class BMQDY : BASECRMClass
    {
        public int iBMQID
        {
            get { return iJLBH; }
            set { iJLBH = value; }
        }
        public string sBMQMC = string.Empty;
    }
    public class LMSHDY : BASECRMClass
    {
        public string sLMSHMC = string.Empty;
        public string sSHMC = string.Empty;
    }
    public class BMQFFGZ : BASECRMClass
    {
        public int iBMQFFGZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sGZMC = string.Empty;
    }
    public class LPGHS : BASECRMClass
    {
        public int iGHSID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sGHSMC = string.Empty;
        public string sGHSDZ = string.Empty;
        public string sDHHM = string.Empty;
        public string sZYXM = string.Empty;
        public int iBJ_TY = 0;
    }

    public class LPSX : BASECRMClass
    {
        public int iLPSXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iLPSXLX = 0;
        public string sLPSXMC = string.Empty;
        public string sLPSXNR = string.Empty;
    }

    public class LPFFHDDY : DJLR_ZX_CLass
    {
        public int iMDID = -1;
        public string sMDMC = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sZT = string.Empty;
        public class LPGL_LPFFHDITEM : LPXX
        {
        }
        public List<LPGL_LPFFHDITEM> itemTable = new List<LPGL_LPFFHDITEM>();
    }

    public class SRLPLQXX : BASECRMClass
    {
        public int iHYID = 0;

        public string sCZDD = string.Empty, sBZ = string.Empty, sHYK_NO = string.Empty;
        //public double fLPDJ, fLPJJ, fLPJF, fZSJF;
        //public int iBJ_DEL = 0, iBJ_WKC = 0;

        public int iND = 0;
        //public string sLPFLMC = string.Empty;
        //public int iLPLX = 0;
    }

    public class SRLPYDXX : BASECRMClass
    {
        public int iHYID = 0;
        public string sCZDD, sBGDDMC = string.Empty;
        public string sBZ = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sDJSJ = string.Empty;
        public int iLX = 0;
        public string sLPDM, sLPMC = string.Empty;
    }

    public class LPJXC : LPXX
    {
        public DateTime dRQ;
        public int iMDID = 0;
        public double fQCSL = 0;
        public double fJHSL = 0;
        public double fBRSL = 0;
        public double fBCSL = 0;
        public double fFFSL = 0;
        public double fZFSL = 0;
        public double fSYSL = 0;
        public double fTHSL = 0;
        public double fZTSL = 0;
        public double fJCSL = 0;
    }
    public class LPJHDW : BASECRMClass
    {
        public int iJHDWID
        {
            get { return iJLBH; }
            set { iJLBH = value; }
        }
        public string sJHDWDM = string.Empty;
        public string sJHDWMC = string.Empty;
        public string sJHDWQC = string.Empty;
        public string sPJHDWDM = string.Empty;
        public int iBJ_TY = 0;
        public int iBJ_MJ = 0;
        public int iBJ_KTC = 0;
        public int iMDID;
        public string sMDMC = string.Empty;
    }

    public class LPFFGZ : DJLR_ZXQDZZ_CLass
    {
        public string sGZMC = string.Empty;
        public int iGZLX = 0;
        public string dKSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string dJSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iHYKTYPE = 0;
        public int iBJ_DC = 0;
        public int iBJ_SR = 0;
        public int iBJ_LJ = 0;
        public int iBJ_BK = 0;
        public int iBJ_SL = 0;
        public int iYHQID = 0;
        public int iCXID = 0;
        public string sHYKNAME, sCXZT, sYHQMC; //, sGZLXMC
        public List<LPFFGZ_LP> lpitem = new List<LPFFGZ_LP>();
        //以下为传参数用
        public int iBJ_CheckDate = 1, iFXMD = 0, iCZMD = 0;
        public string sGZLXMC
        {
            get { return CrmLibProc.GetLPFFGZLXName(iGZLX); }
        }

    }

    public class LPFFGZ_LP : LPXX
    {
        public int iLPGRP = 0;
        public double fLPJE = 0;
        public double fSL = 0;
    }
    public class LPFFGZ_TJ//获取礼品发放规则里的礼品用的传输条件
    {
        public int iHYID = 0;
        public int iFLGZBH = 0;
        public string sCZDD = string.Empty;
    }
    public class SysDate
    {
        public string dSysDate;
    }

    public class SJGZ : BASECRMClass
    {
        public int iHYKTYPE_OLD
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iHYKTYPE_NEW = 0;
        public string sHYKNAME_OLD = string.Empty, sHYKNAME_NEW = string.Empty;
        public int iXFSJ = -1;                                          //0表示 累计消费   1 当日消费
        public string sXFSJ
        {
            get
            {
                if (iXFSJ == 1)
                {
                    return "当日消费";
                }
                else
                {
                    return "累计消费";
                }
            }
        }
        public double fQDJF = 0;
        public int iBJ_SJ = 0;                                          // 1表示 升级 0 表示 降级
        public double fDRXFJE = 0;
        public double fSJYJJE = 0;
        public double fSJQDJE = 0;
        public string dBLRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iMDID = 0;
        public int iBJ_XFJE = 0;                                        //参考变量：1表示 消费金额，0表示积分
        public string sBJ_XFJE
        {
            get
            {
                if (iBJ_XFJE == 1)
                {
                    return "消费金额";
                }
                else
                {
                    return "积分";
                }
            }
        }
        //查询条件用
        public double fBQJF = 0, fXFJE = 0;
    }

    public class SHSPXX : BASECRMClass
    {
        public int iSHSPID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sSPDM = string.Empty;
        public string sSPMC = string.Empty;
        public string sSPJC = string.Empty;
        public string sPYM = string.Empty;
        public string sUNIT = string.Empty;
        public string sSPHS = string.Empty;
        public string sSPGG = string.Empty;
        public string sHH = string.Empty;
        public int iSHSPFLID = 0;
        public string sSPFLMC = string.Empty;
        public int iSHSBID = 0;
        public string sSBMC = string.Empty;
        public int iSHHTID = 0;
        public string sHTH = string.Empty;
        public string sGHSDM = string.Empty;
        public string sGHSMC = string.Empty;
        public string sSHSPFLDM = string.Empty;
        public string sSP_ID = string.Empty;
    }

    public class SHSPFL : BASECRMClass
    {
        public int iSHSPFLID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sSPFLDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sPSPFLDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sSPFLMC
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public string sSPFLQC
        {
            set { sFULLNAME = value; }
            get { return sFULLNAME; }
        }
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sPYM = string.Empty;
        public int iMJBJ = 0;
        public int iSPFLID = 0;
    }

    public class SHSPSB : BASECRMClass
    {
        public int iSHSBID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sSBDM = string.Empty;
        public string sSBMC = string.Empty;
        public string sPYM = string.Empty;
        public string sSYZ = string.Empty;
        public int iMJBJ = 0;
        public int iSBID = 0;
    }

    public class SHHT : BASECRMClass
    {
        public int iSHHTID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sHTH = string.Empty;
        public string sGHSDM = string.Empty;
        public string sGHSMC = string.Empty;
        public int iSHBMID = 0;
        public int iBJ_YX = 0;
    }

    public class SHBM : BASECRMClass
    {
        public int iSHBMID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sBMDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sPBMDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sBMMC
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public string sBMQC
        {
            set { sFULLNAME = value; }
            get { return sFULLNAME; }
        }

        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public int iDEPT_TYPE = 0;
        public int iQYID = 0;
        public string sMDDM = string.Empty;
    }

    public class XFJL : DZHYK_DJLR_CLass
    {
        public int iXFJLID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iMDID = 0, bHYXFJL = 0;
        public string sSHDM = string.Empty, sSKTNO = string.Empty, sMDMC = string.Empty;
        public int iXPH = 0;                                            //小票号，字段JLBH
        public int iXFJLID_OLD = 0;
        public int iHYID_FQ = 0;
        public int iHYID_TQ = 0;
        public string dXFSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string dJZRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string dCRMJZRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string sSKYDM = string.Empty;
        public string dSCSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public double fXSSL = 0;
        public double fJE = 0;
        public double fZK = 0;
        public double fZK_HY = 0;
        public double fJF = 0;
        public double fCZJE = 0;
        public int iFDBH;
        public int iBJ_HTBSK = 0;
        public string dXFRQ_FQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public double fJFBS = 0;
        public int iBSFS = 0;
        public int iPGRYID = 0;
        public int iFXDW = 0;
        public int iBJ_CHILD = 0;

        public List<XFJL_SP> itemTable = new List<XFJL_SP>();
    }

    public class XFJL_SP : BASECRMClass
    {
        public int iINX = 0, iSHSPID = 0;
        public string sBMDM = string.Empty, sBMMC = string.Empty, sSPDM = string.Empty, sSPMC = string.Empty;
        public double fXSJE = 0, fJF = 0, fZKJE = 0, fZKJE_HY = 0, fXSSL = 0;
        public int iJFDYDBH = 0;
        public double fJFJS = 0;
        public int iBJ_JFBS = 0;
    }

    public class XFMX : DZHYK_DJLR_CLass
    {
        public string dRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iMDID = 0;
        public string sSKTNO = string.Empty;
        public int iXPH = 0;                                            //小票号，字段JLBH
        public int iSHSPID = 0;
        public string sDEPTID = string.Empty;
        public string dJYSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iSHHTID = 0;
        public int iSHSPFLID = 0;
        public int iSHSBID = 0;
        public double fXSSL = 0;
        public double fXSJE = 0;
        public double fZKJE = 0;
        public double fZKJE_HY = 0;
        public double fJF = 0;
        public double fCLJF_ZF = 0;
        public double fCLJF_PP = 0;
        public double fCLJF_GHS = 0;
        public int iYEARMONTH = 0;
        public string sHTH = string.Empty;
        public int iXFCS = 0, iTHCS = 0;
        public string sMDMC = string.Empty;
        public string sBMMC = string.Empty;
        public string sSPMC = string.Empty;
        public string sSPDM = string.Empty;
        public string sSBMC = string.Empty;
        public string sSPFLMC = string.Empty;
        public string sSPFLDM = string.Empty;
    }

    public class SFQHZ : HYKYHQ_DJLR_CLass
    {
        public string dRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string sSHDM = string.Empty;
        public int iMDID = 0;
        public double fYQJE = 0;
        public double fFQJE = 0;
        public double fZXFJE = 0;
        public string sBMDM = string.Empty;
        public string sBMMC = string.Empty;
        public string sSHMC = string.Empty;
    }

    public class ZKXX : DZHYK_DJLR_CLass
    {
        public int iXH
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string dRQ
        {
            set { dZXRQ = value; }
            get { return dZXRQ; }
        }
        public int iCLLX
        {
            set { iDJLX = value; }
            get { return iDJLX; }
        }
        public string sCLLX = string.Empty;
        public int iJKJLBH = 0;                                         //建卡记录编号
    }

    public class CZYQX : BASECRMClass
    {
        public int iID//人员ID或组ID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        //iDJLX用来区分权限类型，0卡类型1保管地点2商户部门3门店4发行单位
        public int iDJLX = 0;
        public int iQXID = 0;                                           //具体权限的ID，如HYKTYPE、MDID等
        public string sQXDM = " ";                                      //BMDM、BGDDDM等
        public string sQXDM2 = " ";                                     //SHDM，仅用于部门权限
    }

    public class TreeNode
    {
        public int iID = 0;
        //public int iID
        //{
        //    set { iJLBH = value; }
        //    get { return iJLBH; }
        //}
        public string id = string.Empty;
        public string pId = string.Empty;                              //父代码
        public string sMC = string.Empty, name = string.Empty;
        public string rootId = string.Empty;
        public int iUID = 0;
    }

    public class CheckTreeNode : TreeNode
    {
        public bool @checked = false;
        public int iBJ_MR = 0;
        public int iBJ_FKFS = 0;
    }

    public class CZK_KHDA : BASECRMClass
    {
        public int iKHID = 0;
        public string sKHDM = string.Empty;
        public string sKHMC = string.Empty;
        public int iKHLX = -1;
        public string sHYKNO = string.Empty;
        public string sHYKHM = string.Empty;
        public string sGKRYSJHM = string.Empty;
        public string sDKHLXMC = string.Empty;
        public string sGKRYNAME = string.Empty;
        public string sGKRRYID = string.Empty;
        public string sKHDZ = string.Empty;
        public string sMD = string.Empty;
    }

    public class CZK_KFZY : BASECRMClass
    {
        public string sBGDDDM = string.Empty;
        public int iKHID = 0;
        public string sKHDM = string.Empty;
        public string sKHMC = string.Empty;
    }

    public class CXD : DJLR_ZXQDZZ_CLass
    {
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public int iSHBMID = 0;
        public string sSHBMDM = string.Empty;
        public string sBMMC = string.Empty;
        public int iYXCLBJ = 0;
        public DateTime rq1 = DateTime.MinValue;
        public string dRQ1
        {
            get
            {
                return rq1.ToString("yyyy-MM-dd");
            }
            set
            {

                rq1 = FormatUtils.StrToDate(value);

            }
        }
        private DateTime rq2 = DateTime.MinValue;
        public string dRQ2
        {
            get { return FormatUtils.DatetimeToString(rq2); }
            set
            {
                rq2 = FormatUtils.StrToDate(value);
                rq2 = rq2.Date.AddDays(1).AddSeconds(-1);
            }
        }
        //FormatUtils.DatetimeToString(DateTime.MinValue);

        public List<CXD_FD> itemFD = new List<CXD_FD>();
        public List<CXD_GZITEM> itemGZITEM = new List<CXD_GZITEM>();

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTableStatus(query, sMainTable, serverTime, (int)BASECRMDefine.DJStatus.SH, "JLBH", "SHR", "SHRMC", "SHSJ");
        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTableStatus(query, sMainTable, serverTime, (int)BASECRMDefine.DJStatus.QD, "JLBH", "QDR", "QDRMC", "QDSJ");
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTableStatus(query, sMainTable, serverTime, (int)BASECRMDefine.DJStatus.ZZ, "JLBH", "ZZR", "ZZRMC", "ZZRQ");
        }
        public override void UnExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "update " + sMainTable + " set SHR=null,SHRMC=null,SHSJ=null,STATUS=" + (int)BASECRMDefine.DJStatus.LR + " where JLBH=" + iJLBH;
            if (query.ExecSQL() != 1)
                throw new Exception(CrmLibStrings.msgUnExecExecuted);
        }
        public string GetCLDX(CyQuery query)//int bm,int ht,int sb,int spfl,int )
        {
            /*                    item_gzsd.iCLFS_BM = query.FieldByName("CLFS_BM").AsInteger;
                    item_gzsd.iCLFS_SPFL = query.FieldByName("").AsInteger;
                    item_gzsd.iCLFS_SPSB = query.FieldByName("").AsInteger;
                    item_gzsd.iCLFS_HT = query.FieldByName("").AsInteger;
                    item_gzsd.iCLFS_SP = query.FieldByName("").AsInteger;
                    item_gzsd.iCLFS_HYZ = query.FieldByName("").AsInteger;
                    item_gzsd.iCLFS_HYKLX = query.FieldByName("CLFS_HYKLX").AsInteger;
                    item_gzsd.iCLFS_FXDW = query.FieldByName("CLFS_FXDW").AsInteger;

*/
            string cldx = "指定";
            if (query.Fields.FieldByName("CLFS_BM") != null && query.FieldByName("CLFS_BM").AsInteger > 0)
                cldx += "部门、";
            if (query.Fields.FieldByName("CLFS_HT") != null && query.FieldByName("CLFS_HT").AsInteger > 0)
                cldx += "合同、";
            if (query.Fields.FieldByName("CLFS_SPFL") != null && query.FieldByName("CLFS_SPFL").AsInteger > 0)
                cldx += "分类、";
            if (query.Fields.FieldByName("CLFS_SPSB") != null && query.FieldByName("CLFS_SPSB").AsInteger > 0)
                cldx += "品牌、";
            if (query.Fields.FieldByName("CLFS_HYZ") != null && query.FieldByName("CLFS_HYZ").AsInteger > 0)
                cldx += "会员组、";
            if (query.Fields.FieldByName("CLFS_SP") != null && query.FieldByName("CLFS_SP").AsInteger > 0)
                cldx += "商品、";
            if (query.Fields.FieldByName("CLFS_HYKLX") != null && query.FieldByName("CLFS_HYKLX").AsInteger > 0)
                cldx += "卡类型、";
            if (query.Fields.FieldByName("CLFS_FXDW") != null && query.FieldByName("CLFS_FXDW").AsInteger > 0)
                cldx += "发行单位、";
            return cldx.Substring(0, cldx.Length - 1);
        }
    }
    public class CXD_FD
    {
        public int iINX = 0;
        public DateTime rq1 = DateTime.MinValue;
        public string dKSRQ
        {
            get
            {
                return rq1.ToString("yyyy-MM-dd");
            }
            set
            {

                rq1 = FormatUtils.StrToDate(value);

            }
        }
        //public string dKSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        private DateTime rq2 = DateTime.MinValue;
        public string dJSRQ
        {
            get { return FormatUtils.DatetimeToString(rq2); }
            set
            {
                rq2 = FormatUtils.StrToDate(value);
                rq2 = rq2.Date.AddDays(1).AddSeconds(-1);
            }
        }
        //public string dJSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string sSD = string.Empty;
        public byte[] sSD_Byte = new byte[48];
    }
    public class CXD_GZSD
    {
        public int iINX = 0;
        public int iGZBH = 0;
        public int iBJ_CJ = 0;
        public int iCLFS_BM = 0;
        public int iCLFS_SPFL = 0;
        public int iCLFS_SPSB = 0;
        public int iCLFS_HT = 0;
        public int iCLFS_HYKLX = 0;
        public int iCLFS_SP = 0;
        public int iCLFS_FXDW = 0;
        public int iCLFS_HYZ = 0;
        public int iCLFS_ZFFS = 0;
        public int iCLFS_BANK = 0;
        public int iCLFS_KH = 0;
        public string sCLDX = string.Empty;
        public int iXH = 0;
    }
    public class CXD_GZITEM
    {
        public int iINX = 0;
        public int iGZBH = 0;
        public int iXH = 0;
        public int iSJLX = 0;
        public int iSJNR = 0;
    }
    public class YHQCXD : CXD
    {
        public int iYHQID = 0;
        public string sYHQMC = string.Empty;
        public int iCXID = 0;
        public string sCXZT = string.Empty;
        public List<YHQCXD_GZSD> itemGZSD = new List<YHQCXD_GZSD>();
    }
    public class YHQCXD_GZSD : CXD_GZSD
    {
        public int iGZID = 0;
        public string sGZMC = string.Empty;
    }
    public class JFFLJL : DJLR_ZX_CLass
    {
    }
    public class QKDY : BASECRMClass
    {
        public int iQKID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sQKDM = string.Empty;
        public string sQKQC = string.Empty;
        public string sQKMC = string.Empty;

        public int iQYID = 0;
        public string sQYMC = string.Empty;
        //xxm start 用于FillzWUCTabs_QK
        public string sPQKDM = string.Empty;
        public int iBJ_QK = 0;
        //xxm stop
        public int iBoolInsert = 0;
        public List<QKDY> itemTable = new List<QKDY>();
    }
    public class LABEL_LB : DJLR_CLass
    {
        public int iLABELLBID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }

        public string sLABELMC = string.Empty;
    }
    public class LABEL_XM : DJLR_CLass
    {
        public int iLABELXMID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sLABELXMDM
        {
            set { id = value; }
            get { return id; }
        }
        public string sPLABELXMDM
        {
            set { pId = value; }
            get { return pId; }
        }
        public string sLABELXMMC
        {
            set { sNAME = value; }
            get { return sNAME; }
        }
        public string sLABELXMQC
        {
            set { sFULLNAME = value; }
            get { return sFULLNAME; }
        }
        public int iRS, iLABELLBID, iBJ_WY = 0;
        public string sLABELXMMS = string.Empty;
        //public int iHYKTYPE = 0;
        //public string sHYKNAME = string.Empty;

    }
    public class LABEL_XMITEM : DJLR_CLass
    {
        public int iLABELID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iLABEL_VALUEID = 0;
        public string sLABEL_VALUE = string.Empty;
        public int iLABELXMID = 0;
        public double fSJSX, fSJXX = 0;
        public string sLABELXMMC, sLABELXMDM = string.Empty;
        public int iLABELLX = 0;
    }
    //短信相关类  第三方接口
    public class MySMS
    {
        public string userName = string.Empty;
        public string userKey = string.Empty;
        public string userContent = string.Empty;
        public string ToPhone = string.Empty;
        public static string webServiceAddress = "http://utf8.sms.webchinese.cn/?";
        /// <summary>
        /// 初始化发短信类
        /// </summary>
        /// <param name="username"></param>  用户名
        /// <param name="userkey"></param>    用户密码
        /// <param name="usercontent"></param>  发送内容
        /// <param name="tophone"></param> 接受手机号
        public MySMS(string username, string userkey, string usercontent, string tophone)
        {
            this.userName = username;
            this.userContent = usercontent;
            this.userKey = userkey;
            this.ToPhone = tophone;
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <returns></returns>
        public string SendMessage()
        {
            string result = string.Empty;
            webServiceAddress += "Uid=" + userName + "&Key=" + userKey + "&smsMob=" + ToPhone + "&smsText=" + userContent + "【Min科技】";
            webServiceAddress = webServiceAddress.Trim();
            try
            {
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(webServiceAddress);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.Default);
                result = ser.ReadToEnd();
            }
            catch (Exception ex)
            {
                result = "短信发送失败，" + ex.Message + "";
            }
            return result;
        }

    }

    //邮件相关类
    public class MyEmail
    {
        private MailMessage mMailMessage;   //主要处理发送邮件的内容（如：收发人地址、标题、主体、图片等等）
        private SmtpClient mSmtpClient; //主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等）
        private int mSenderPort;   //发送邮件所用的端口号（htmp协议默认为25）
        private string mSenderServerHost;    //发件箱的邮件服务器地址（IP形式或字符串形式均可）
        private string mSenderPassword;    //发件箱的密码
        private string mSenderUsername;   //发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        private bool mEnableSsl;    //是否对邮件内容进行socket层加密传输
        private bool mEnablePwdAuthentication;  //是否对发件人邮箱进行密码验证

        ///<summary>
        /// 构造函数
        ///</summary>
        ///<param name="server">发件箱的邮件服务器地址</param>
        ///<param name="toMail">收件人地址（可以是多个收件人，程序中是以“;"进行区分的）</param>
        ///<param name="fromMail">发件人地址</param>
        ///<param name="subject">邮件标题</param>
        ///<param name="emailBody">邮件内容（可以以html格式进行设计）</param>
        ///<param name="username">发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）</param>
        ///<param name="password">发件人邮箱密码</param>
        ///<param name="port">发送邮件所用的端口号（htmp协议默认为25）</param>
        ///<param name="sslEnable">true表示对邮件内容进行socket层加密传输，false表示不加密</param>
        ///<param name="pwdCheckEnable">true表示对发件人邮箱进行密码验证，false表示不对发件人邮箱进行密码验证</param>
        public MyEmail(string server, string toMail, string fromMail, string subject, string emailBody, string username, string password, string port, bool sslEnable, bool pwdCheckEnable)
        {
            try
            {
                mMailMessage = new MailMessage();
                if (toMail != "")
                {
                    string[] toMailArray = toMail.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string item in toMailArray)
                    {
                        mMailMessage.To.Add(item);
                    }
                }
                mMailMessage.From = new MailAddress(fromMail);
                mMailMessage.Subject = subject;
                mMailMessage.Body = emailBody;
                mMailMessage.IsBodyHtml = true;
                mMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mMailMessage.Priority = MailPriority.Normal;
                this.mSenderServerHost = server;
                this.mSenderUsername = username;
                this.mSenderPassword = password;
                this.mSenderPort = Convert.ToInt32(port);
                this.mEnableSsl = sslEnable;
                this.mEnablePwdAuthentication = pwdCheckEnable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        public void AddAttachments(string attachmentsPath)
        {
            try
            {
                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                Attachment data;
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path[i]);
                    disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    disposition.ReadDate = File.GetLastAccessTime(path[i]);
                    mMailMessage.Attachments.Add(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ///<summary>
        /// 邮件的发送
        ///</summary>
        public void Send()
        {
            try
            {
                if (mMailMessage != null)
                {
                    mSmtpClient = new SmtpClient();
                    //mSmtpClient.Host = "smtp." + mMailMessage.From.Host;
                    mSmtpClient.Host = this.mSenderServerHost;
                    mSmtpClient.Port = this.mSenderPort;
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.EnableSsl = false;
                    if (this.mEnablePwdAuthentication)
                    {
                        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //NTLM: Secure Password Authentication in Microsoft Outlook Express
                        mSmtpClient.Credentials = nc.GetCredential(mSmtpClient.Host, mSmtpClient.Port, "NTLM");
                    }
                    else
                    {
                        mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                    }
                    mSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(mMailMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    //写卡程序
    public class RYXX
    {
        public int iRYID = 0;
        public string sRYMC = string.Empty;
    }
    public class JKJL
    {
        public int iJLBH = 0;
        public int iJKFS = 0;
        public int iHYKTYPE = 0;
        public string sBGDDDM = string.Empty;
        public string sZY = string.Empty;
        public string sCZKHM_BEGIN = string.Empty;
        public string sCZKHM_END = string.Empty;
        public int iJKSL = 0;
        public double fQCYE = 0;
        public double fYXTZJE = 0;
        public int iJKWJCS = 0;
        public int iBGR = 0;
        public string sBGRMC = string.Empty;
        public double fPDJE = 0;
        public int iBJ_CZK = 0;
        public string dDJSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iDJR = 0;
        public string sDJRMC = string.Empty;
        public string dZXRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iZXR = 0;
        public string sZXRMC = string.Empty;
        public int iSTATUS = 0;
        public int iDJLX = 0;
        public string dYXQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iXKSL = 0;
        public int iFXDWID = 0;

        public string sHYKNAME = string.Empty;
        public string sBGDDMC = string.Empty;

        public List<JKJLITEM> item = new List<JKJLITEM>();
    }
    public class JKJLITEM
    {
        public string sCZKHM = string.Empty;
        public string sCDNR = string.Empty;
        public double fJE = 0;
        public int iBJ_ZK = 0;
        public string dXKRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
    }
    public class CKXX
    {
        public string sCZKHM = string.Empty;
        public string sCDNR = string.Empty;
        public double fQCYE = 0;
        public double fJE = 0;
        public int iHYKTYPE = 0;
        public bool bKCK = false;
        public string sHY_NAME = string.Empty;
        public string sHYKNAME = string.Empty;
        public string sSFZBH = string.Empty;
        public string sSJHM = string.Empty;
    }
    public class  CardJson{ 
        public Card card = new Card();
    }
    public class Card
    {
        public string card_type = "MEMBER_CARD";
        public member_card member_card = new member_card();

        //public CardAdvanceInfo advanced_info { get; set; }
    }

    public class member_card 
    {
        public CardBaseInfo base_info = new CardBaseInfo();
        public string background_pic_url = null;
        public bool supply_bonus = false;
        public bool supply_balance = false;
        public string prerogative = null;
        public string activate_url = null;
        public bool wx_activate = false;
        public bool wx_activate_after_submit = false;
        public string wx_activate_after_submit_url = string.Empty;
        public custom_field custom_field1 = new custom_field();
        public custom_field custom_field2 = new custom_field();
        public custom_field custom_field3 = new custom_field();
    }
    public class CardBaseInfo
    {
        public string logo_url = null;
        public string code_type = "CODE_TYPE_QRCODE";
        public string brand_name = null;
        public string title = null;
        public string color = "Color010";
        public string notice = null;
        public string service_phone = null;
        public string description = null;
        public int get_limit = 1;
        public bool use_custom_code = false;
        public bool can_give_friend = false;
        public string custom_url = null;
        public string custom_url_name = null;
        public string custom_url_sub_title = null;
        public string center_url = null;
        public string center_title = null;
        public string center_sub_title = null;
        public string promotion_url = null;
        public string promotion_url_name = null;
        public string promotion_url_sub_title = null;
        public pay_info pay_info = new pay_info();
        public sku sku = new sku();
        public date_info date_info = new date_info();
        public bool is_pay_and_qrcode = false;

    }


    public class custom_field
    {
        public string name = null;
        public string url = null;
    }
    public class date_info
    {
        public string type = "DATE_TYPE_PERMANENT";
    }
    public class sku
    {
        public int quantity = 100000000;

    }
    public class pay_info
    {
        public swipe_card swipe_card = new swipe_card();

    }
    public class swipe_card
    {
        public bool is_swipe_card = false;
    }

    public class user_info 
    {
        public string card_id = string.Empty;
        public bind_old_card bind_old_card = new bind_old_card();
        public required_form required_form = new required_form();
        public optional_form optional_form = new optional_form();
        
    }
    public class required_form 
    {
        public bool can_modify = false;
        public string []common_field_id_list  = new string[5];


    }
    public class optional_form
    {
        public bool can_modify = false;
        public string[] common_field_id_list = new string[5];
    }

    public class bind_old_card 
    {
        public string name = string.Empty;
        public string url = string.Empty;
    }

    public class cardid {
        public string card_id = string.Empty;
    }


    public class paygiftcard
    {
        public paygiftcard_ruleinfo rule_info = null;

    }
    #region 支付即会员
    public class paygiftcard_ruleinfo
    {
        public string rule_id = null;
        public string type = "RULE_TYPE_PAY_MEMBER_CARD";
        public paygiftcard_baseinfo base_info = new paygiftcard_baseinfo();
        public paygiftcard_memberrule member_rule = new paygiftcard_memberrule();

    }
    public class paygiftcard_baseinfo 
    {
        public string[] mchid_list = new string[10];
        public int begin_time = 0;
        public int end_time = 0;
    }

    public class paygiftcard_memberrule 
    {
        public string card_id = string.Empty;
        public int least_cost = 0;
        public int max_cost = 0;
    }
    // 返回结果
    public class paygiftcard_result
    {
        public string errcode = string.Empty;
        public string errmsg = string.Empty;
        public string rule_id = string.Empty;

    }
    # endregion
}
