using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public class BasePage : BFPage
{
    public string V_Head_Input, V_Head_Search, V_Head_Tree, V_Head_InputList, V_Head_None, V_Head_JKPT, V_Head_CXD, V_Head_WX, V_Head_Report;
    public string V_InputBodyBegin, V_InputBodyEnd;
    public string V_SearchBodyBegin, V_SearchBodyEnd;
    public string V_TreeBodyBegin, V_TreeBodyEnd;
    public string V_InputListBegin, V_InputListEnd;
    public string V_NoneBodyBegin, V_NoneBodyEnd;
    public string V_JKPTBodyBegin, V_JKPTBodyEnd;
    public string V_DHTBodyBegin, V_DHTBodyEnd;
    public string V_ArtToolBar;
    public string V_Head_WebArt, V_Head_WebArtList, V_Head_WebArtTree;

    public override void BFPage_Load(object sender, EventArgs e)
    {
        base.BFPage_Load(sender, e);

        string bftitle = Request.QueryString["bftitle"];
        if (bftitle == null)
            bftitle = "";
        string t = DateTime.Now.Minute.ToString();
        /*
        V_HeadConfig = "<meta http-equiv='X-UA-Compatible' content='IE=edge' >";
        V_HeadConfig += "<title></title>";
        //css
        V_HeadConfig += "<link href='../../../Css/jquery-ui.css' rel='stylesheet' type='text/css' />";
        //V_HeadConfig += "<link href='../../../Css/JQueryUI/Base/jquery-ui.min.css' rel='stylesheet' type='text/css' />";
        V_HeadConfig += "<link href='../../../Css/JQueryUI/Blitzer/jquery-ui.min.css' rel='stylesheet' type='text/css' />";
        //V_HeadConfig += "<link href='../../../Css/JQueryUI/Flick/jquery-ui.min.css' rel='stylesheet' type='text/css' />";
        //V_HeadConfig += "<link href='../../../Css/JQueryUI/Humanity/jquery-ui.min.css' rel='stylesheet' type='text/css' />";
        //V_HeadConfig += "<link href='../../../Css/JQueryUI/Overcast/jquery-ui.min.css' rel='stylesheet' type='text/css' />";
        //V_HeadConfig += "<link href='../../../Css/JQueryUI/Pepper/jquery-ui.min.css' rel='stylesheet' type='text/css' />";        
        //V_HeadConfig += "<link href='../../../Css/JQueryUI/Redmond/jquery-ui.min.css' rel='stylesheet' type='text/css' />";
        //V_HeadConfig += "<link href='../../../Css/JQueryUI/Smoothness/jquery-ui.min.css' rel='stylesheet' type='text/css' />";
        V_HeadConfig += "<link href='../../../Css/ui.jqgrid.css' rel='stylesheet' type='text/css' />";
        V_HeadConfig += "<link href='../../../Css/zTreeStyle/zTreeStyle.css' rel='stylesheet' type='text/css' />";
        V_HeadConfig += "<link href='../../../Css/DialogSkins/chrome.css' rel='stylesheet' type='text/css' />";
        V_HeadConfig += "<link href='../../../Css/BF.style.css' rel='stylesheet' type='text/css' />";

        V_HeadConfig += "<script src='../../../Js/jquery.js'></script>";
        V_HeadConfig += "<script src='../../../Js/jquery-ui.js'></script>";
        V_HeadConfig += "<script src='../../../Js/jquery.jqGrid.min.js'></script>";
        V_HeadConfig += "<script src='../../../Js/jquery.tabify.js'></script>";
        V_HeadConfig += "<script src='../../../Js/grid.locale-cn.js'></script>";
        V_HeadConfig += "<script src='../../../Js/jquery.artDialog.js'></script>";
        V_HeadConfig += "<script src='../../../Js/jquery.ztree.all-3.5.min.js'></script>";
        V_HeadConfig += "<script src='../../../Js/jquery.inputmask.js'></script>";
        V_HeadConfig += "<script src='../../../Js/My97DatePicker/WdatePicker.js'></script>";
        V_HeadConfig += "<script src='../../../Js/plugins/iframeTools.js'></script>";
        V_HeadConfig += "<script src='../../../Js/CommonFunctionCrm.js'></script>";
        V_HeadConfig += "<script src='../../../Js/LodopFuncs.js'></script>";//LodopFuncs
        V_HeadConfig += "<script src='" + System.Configuration.ConfigurationManager.AppSettings["CloudPrintServer"] + "/CLodopfuncs.js'></script>";
        */
        //新的
        //V_Head_Input += "<link href='../../../Css/EasyUI/default/easyui.css' rel='stylesheet' type='text/css' />";
        //V_Head_Input += "<link href='../../../Css/EasyUI/black/easyui.css' rel='stylesheet' type='text/css' />";
        //V_Head_Input += "<link href='../../../Css/EasyUI/bootstrap/easyui.css' rel='stylesheet' type='text/css' />";
        //V_Head_Input += "<link href='../../../Css/EasyUI/gray/easyui.css' rel='stylesheet' type='text/css' />";
        V_Head_Input += "<link href='../../../Css/easyform.css' rel='stylesheet' />";
        V_Head_Input += "<link href='../../../Css/EasyUI/material/easyui.css' rel='stylesheet' type='text/css' />";
        //V_Head_Input += "<link href='../../../Css/EasyUI/metro/easyui.css' rel='stylesheet' type='text/css' />";
        V_Head_Input += "<link href='../../../Css/EasyUI/icon.css' rel='stylesheet' type='text/css' />";
        V_Head_Input += "<link href='../../../Css/zTreeStyle/zTreeStyle.css' rel='stylesheet' type='text/css' />";
        V_Head_Input += "<link href='../../../Css/DialogSkins/simple.css' rel='stylesheet' type='text/css' />";
        V_Head_Input += "<link href='../../../Css/font-awesome.min.css' rel='stylesheet' type='text/css' />";
        V_Head_Input += "<link href='../../../Css/magic-check.css' rel='stylesheet' type='text/css' />";
        //V_Head_Input += "<link href='../../../Css/buttons.css' rel='stylesheet' type='text/css' />";
        V_Head_Input += "<link href='../../../Css/BF.style.css' rel='stylesheet' type='text/css' />";


        V_Head_Input += "<script src='../../../Js/jquery.js'></script>";
        //V_Head_Input += "<script src='../../../Js/jquery.tabify.js'></script>";
        V_Head_Input += "<script src='../../../Js/jquery.artDialog.js'></script>";
        //V_Head_Input += "<script src='../../CrmLib/jquery.artDialog.js'></script>";
        V_Head_Input += "<script src='../../../Js/jquery.ztree.all-3.5.min.js'></script>";
        V_Head_Input += "<script src='../../../Js/jquery.easyui.min.js'></script>";
        V_Head_Input += "<script src='../../../Js/easyui-lang-zh_CN.js'></script>";
        V_Head_Input += "<script src='../../../Js/My97DatePicker/WdatePicker.js'></script>";
        V_Head_Input += "<script src='../../../Js/iframeTools.js'></script>";
        V_Head_Input += "<script src='../../../Js/CommonFunctionCrm.js?t=" + t + "'></script>";
        V_Head_WebArt = V_Head_Input;
        V_Head_Input += "<script src='../../../Js/jquery.inputmask.js'></script>";
        V_Head_Input += "<script src='../../../Js/jquery.form.js'></script>";
        V_Head_Input += "<script src='../../../Js/LodopFuncs.js'></script>";//LodopFuncs
        V_Head_Input += "<script src='../../../Js/easyform.js'></script>";
        string printsrv = ConfigurationManager.AppSettings["CloudPrintServer"];
        if (printsrv != null && printsrv != "")
            V_Head_Input += "<script src='" + System.Configuration.ConfigurationManager.AppSettings["CloudPrintServer"] + "/CLodopfuncs.js'></script>";
        string tmpjs = "<script>";
        tmpjs += "var iDJR = '" + V_UserID + "';";
        tmpjs += "var sDJRMC = '" + V_UserName + "';";
        tmpjs += "var sIPAddress = '" + V_IPAddress + "';";
        tmpjs += "var iPID = '" + 1 + "';";
        tmpjs += "var sPIF = '" + 1 + "';";
        tmpjs += "</script>";
        //Print Object
        tmpjs += "<object id='LODOP_OB' classid='clsid:2105C259-1E0C-4534-8141-A753534CB4CA' width='0' height='0' style='display:none' >";
        tmpjs += "<embed id='LODOP_EM' type='application/x-print-lodop' width='0' height='0' pluginspage='install_lodop32.exe'></embed></object>";

        //V_HeadConfig += V_Head_JS;
        V_Head_Input += tmpjs;
        V_Head_WebArt += tmpjs;

        V_Head_None = V_Head_Input;

        V_Head_Search = V_Head_Input + "<script src='../../CrmLib/CrmLib_BillList.js?t=" + t + "'></script>";
        V_Head_Tree = V_Head_Input + "<script src='../../CrmLib/CrmLib_TreeInput.js?t=" + t + "'></script>";
        V_Head_InputList = V_Head_Input + "<script src='../../CrmLib/CrmLib_BillInputWithList.js?t=" + t + "'></script>";
        V_Head_JKPT = V_Head_Input + "<script src='../../CrmLib/CrmLib_SingleList.js?t=" + t + "'></script>";
        V_Head_JKPT += "<script src='../../../Js/highcharts.js'></script>";
        V_Head_Report = V_Head_Input + "<script src='../../CrmLib/CrmLib_GetData.js?t=" + t + "'></script>";
        V_Head_Report += "<script src='../../CrmLib/CrmLib_CRMReport.js?t=" + t + "'></script>";

        V_Head_Input += "<script src='../../CrmLib/CrmLib_BillInput.js?t=" + t + "'></script>";
        V_Head_CXD = V_Head_Input + "<script src='../../CrmLib/CrmLib_GetData.js?t=" + t + "'></script>";
        V_Head_CXD += "<script src='../../CrmLib/CrmLib_CXDInput.js?t=" + t + "'></script>";

        V_Head_WebArtList = V_Head_WebArt;
        V_Head_WebArtList += "<script src='../../CrmLib/CrmLib_BillArtList.js?t=" + t + "'></script>";
        V_Head_WebArtList += "<script src='../../../Js/datagrid-dnd.js'></script>";

        string top = "<div id='reg-form'><div id='TopPanel' class='topbox'>";
        string loc = "<div id='location'><div id='switchspace'></div></div>";
        string toolbar = "<div id='btn-toolbar'><div id='morebuttons'><i class='fa fa-list-ul fa-lg' aria-hidden='true' style='color: rgb(140,151,157)'></i></div></div>";//<div id='WXPublicID'><select id='selectPublicID' class='easyui-combobox'></select></div>
        string clear = "<div class='clear'></div>";
        string begin = top + loc + toolbar + "</div>";

        //录入页面头尾
        V_InputBodyBegin = begin;// "<div id='reg-form'><div id='TopPanel' class='topbox'>" + loc + toolbar + "</div>";
        V_InputBodyBegin += "<div id='MainPanel' class='bfbox'><div class='common_menu_tit'><span id='bftitle'>" + bftitle + "</span></div><div class='maininput'>";

        V_InputBodyEnd = "</div></div><div id='status-bar'></div><div class='clear'></div></div>";

        //查询页面头尾
        V_SearchBodyBegin = begin;//"<div><div id='TopPanel' class='topbox'>" + loc + toolbar + "</div>";
        V_SearchBodyBegin += "<div id='MainPanel' class='bfbox'><div id='SearchPanel' class='common_menu_tit slide_down_title'><span>查询条件</span></div><div id='SearchPanel_Hidden' class='maininput'>";

        V_SearchBodyEnd += "<div class='clear'></div></div></div><div id='SearchResult' class='bfbox'><div class='common_menu_tit'><span>列表</span></div><table id='list'></table></div></div>";

        //树形录入页面头尾
        V_TreeBodyBegin = begin;//"<div><div id='TopPanel' class='topbox'>" + loc + toolbar + "</div>";
        V_TreeBodyBegin += "<div class='bfbox'><div class='common_menu_tit'><span id='bftitle'>" + bftitle + "</span></div><div class='maininput'><div id='TreePanel' class='bfblock_left'></div><div id='MainPanel' class='bfblock_right'>";

        V_TreeBodyEnd = "</div></div></div></div>";

        //录入列表页面头尾
        V_InputListBegin = begin;//"<div id='reg-form'><div id='TopPanel' class='topbox'>" + loc + toolbar + "</div>";
        V_InputListBegin += "<div id='MainPanel' class='bfbox'><div class='common_menu_tit'><span id='bftitle'>" + bftitle + "</span></div><div class='maininput bfborder_bottom'>";

        V_InputListEnd = "<div class='clear'></div></div><table id='list'></table></div></div>";

        //特殊页面，什么都没有，就有个地址栏和按钮栏
        V_NoneBodyBegin = begin;//"<div><div id='TopPanel' class='topbox'>" + loc + toolbar + "</div>";
        V_NoneBodyEnd = clear;// "<div class='clear'></div>";

        //监控平台
        V_JKPTBodyBegin = begin;// "<div><div id='TopPanel' class='topbox'>" + loc + toolbar + "</div>";
        V_JKPTBodyBegin += "<div id='MainPanel' class='bfbox'><div class='common_menu_tit'><span id='bftitle'>" + bftitle + "</span></div><div class='maininput'>";
        V_JKPTBodyEnd = clear + "</div></div>";

        //导航图
        V_DHTBodyBegin = begin;// "<div><div id='TopPanel' class='topbox'>" + loc + toolbar + "</div>";
        V_DHTBodyBegin += "<div id='MainPanel' class='bfbox'><div class='common_menu_tit'><span id='bftitle'>" + bftitle + "</span></div><div class='nav_count_wall'>";
        V_DHTBodyEnd = "<div class='clear'></div></div></div>";

        V_ArtToolBar = "<div id='btn-toolbar' class='common_menu_tit art_toolbar'><div class='art_title' id='bftitle'></div></div>";
    }

    #region CM_CRMGL
    public const int CM_CRMGL_SHDEF = 5160001;                            //商户定义
    public const int CM_CRMGL_MDDEF = 5160002;                            //门店定义
    public const int CM_CRMGL_ZFFSDY = 5160003;                           //支付方式定义
    public const int CM_CRMGL_SHZFFSDY = 5160004;                         //商户支付方式定义
    public const int CM_CRMGL_SPSBDY = 5160005;                           //商品品牌定义
    public const int CM_CRMGL_SPFLDY = 5160006;                           //商品分类定义
    public const int CM_CRMGL_ZFFSDZDY = 5160007;                         //支付方式对照关系定义
    public const int CM_CRMGL_SPSBDZDY = 5160008;                         //商品品牌对照关系定义
    public const int CM_CRMGL_SPFLDZDY = 5160009;                         //商品分类对照关系定义
    public const int CM_CRMGL_HYKLXDY = 5160010;                          //会员卡类型定义
    public const int CM_CRMGL_HYKKZDY = 5160011;                          //会员卡卡种定义
    public const int CM_CRMGL_HYNLDDY = 5160012;                          //会员年龄段定义
    public const int CM_CRMGL_HYQYDY = 5160013;                           //会员地区定义
    public const int CM_CRMGL_YHQDY = 5160014;                            //优惠券定义
    public const int CM_CRMGL_DEFCRMUSER = 5160015;                       //定义会员管理用户
    public const int CM_CRMGL_DEFCRMPOS = 5160016;                        //定义CRMPOS操作员
    public const int CM_CRMGL_CheckXTSJ = 5160017;                        //系统数据平衡检查
    public const int CM_CRMGL_YHPOSXX = 5160018;                          //移动POS信息定义
    public const int CM_CRMGL_FXDW = 5160019;                             //发行单位定义
    public const int CM_CRMGL_HYXXXMDEF = 5160020;                        //会员信息项目定义
    public const int CM_CRMGL_CRMToJXC = 5160021;                         //CRM连JXC数据库设置
    public const int CM_CRMGL_DEFYHQSYSH = 5160022;                       //优惠券使用商户定义
    public const int CM_CRMGL_DEFYHQCXHD = 5160023;                       //促销活动优惠券定义
    public const int CM_CRMGL_DefSD = 5160024;                            //时段定义
    public const int CM_CRMGL_MZKKZDY = 5160025;                          //面值卡卡种定义
    public const int CM_CRMGL_MZKLXDY = 5160026;                          //面值卡类型定义
    public const int CM_CRMGL_GXSJDY = 5160027;                           //管辖数据定义
    public const int CM_CRMGL_YHXXDY = 5160028;                           //银行信息定义
    public const int CM_CRMGL_CRMQXDY = 5160029;                          //CRM操作员权限定义
    public const int CM_CRMGL_CRMZQXDY = 5160030;                         //CRM操作员组权限定义
    public const int CM_CRMGL_XTCSDY = 5160031;                           //系统参数定义
    public const int CM_CRMGL_CXDHT = 5160032;                            //促销导航图
    public const int CM_CRMGL_JFDHT = 5160033;                            //积分导航图
    public const int CM_CRMGL_ZKDHT = 5160034;                            //折扣导航图
    public const int CM_CRMGL_CXMJDHT = 5160035;                          //促销满减导航图
    public const int CM_CRMGL_CXMDDHT = 5160036;                          //促销满抵导航图
    public const int CM_CRMGL_CXYHQDHT = 5160037;                         //促销优惠券导航图
    public const int CM_CRMGL_LPCXDHT = 5160038;                          //礼品促销导航图
    public const int CM_CRMGL_LPFFDHT = 5160039;                          //礼品发放导航图
    public const int CM_CRMGL_CJDHT = 5160040;                            //抽奖导航图
    public const int CM_CRMGL_KQZDHT = 5160041;                           //客群组导航图
    public const int CM_CRMGL_FXQDDY = 5160042;                           //发行渠道定义
    public const int CM_CRMGL_SQLXDY = 5160043;                           //商圈类型定义
    public const int CM_CRMGL_SQDY = 5160044;                             //商圈定义
    public const int CM_CRMGL_QTKGLY = 5160045;                           //定义前台卡管理员
    public const int CM_CRMGL_ZYDY = 5160046;                             //职业定义
    public const int CM_CRMGL_SHZFFSMDJFBL = 5160047;                     //商户支付方式门店积分比例
    public const int CM_CRMGL_XQSQDY = 5160048;                           //小区所属商圈定义
    public const int CM_CRMGL_SPJGDDY = 5160049;                           //商品价格带定义
    public const int CM_CRMGL_DXFS = 5160050;                             //短信发送接口
    public const int CM_CRMGL_SHYHQZFFS = 5160051;                        //商户优惠券支付方式定义
    public const int CM_CRMGL_XTCZY = 5160052;                            //系统操作员
    public const int CM_CRMGL_GXSHDEF = 5160053;                          //管辖商户定义
    public const int CM_CRMGL_RYXXDY = 5160054;                           //人员信息定义
    public const int CM_CRMGL_SHHTCX = 5160055;                           //商户合同查询
    public const int CM_CRMGL_SHSPCX = 5160056;                           //商户商品查询
    public const int CM_CRMGL_YWYDY = 5160057;                            //业务员定义
    public const int CM_CRMGL_YWYDY_CX = 5160058;                         //业务员定义查询
    public const int CM_CRMGL_YWYDY_LR = 5160059;                         //业务员定义录入
    public const int CM_CRMGL_SPSBCX = 5160060;                           //商户商品查询
    #endregion
    #region CM_HYKGL
    public const int CM_HYKGL_HYKBGDDDY = 5161001;                        //会员卡保管地点定义
    public const int CM_HYKGL_HYKJK = 5161002;                            //会员卡建卡处理
    public const int CM_HYKGL_HYKJK_LR = 5161003;                         //会员卡建卡录入
    public const int CM_HYKGL_HYKJK_SH = 5161004;                         //会员卡建卡审核
    public const int CM_HYKGL_HYKJK_XK = 5161005;                         //会员卡建卡写卡
    public const int CM_HYKGL_HYKJK_CX = 5161006;                         //会员卡建卡查询
    public const int CM_HYKGL_HYKLQ = 5161007;                            //会员卡领取处理
    public const int CM_HYKGL_HYKLQ_LR = 5161008;                         //会员卡领取录入
    public const int CM_HYKGL_HYKLQ_SH = 5161009;                         //会员卡领取审核
    public const int CM_HYKGL_HYKLQ_CX = 5161010;                         //会员卡领取查询
    public const int CM_HYKGL_HYKDB = 5161011;                            //会员卡调拨处理
    public const int CM_HYKGL_HYKDB_LR = 5161012;                         //会员卡调拨录入
    public const int CM_HYKGL_HYKDB_SH = 5161013;                         //会员卡调拨审核
    public const int CM_HYKGL_HYKDB_CX = 5161014;                         //会员卡调拨查询
    public const int CM_HYKGL_HYKTL = 5161015;                            //会员卡退领处理
    public const int CM_HYKGL_HYKTL_LR = 5161016;                         //会员卡退领录入
    public const int CM_HYKGL_HYKTL_SH = 5161017;                         //会员卡退领审核
    public const int CM_HYKGL_HYKTL_CX = 5161018;                         //会员卡退领查询
    public const int CM_HYKGL_HYKBGZBB = 5161019;                         //库存卡保管帐报表
    public const int CM_HYKGL_HYKBGKC_CX = 5161020;                       //会员卡保管库存查询
    public const int CM_HYKGL_HYKKCBB = 5161021;                          //会员卡库存报表
    public const int CM_HYKGL_KCKZF = 5161022;                            //库存卡作废
    public const int CM_HYKGL_KCKZF_LR = 5161023;                         //库存卡作废录入
    public const int CM_HYKGL_KCKZF_SH = 5161024;                         //库存卡作废审核
    public const int CM_HYKGL_KCKZF_XS = 5161025;                         //库存卡作废显示
    public const int CM_HYKGL_KCKZF_CX = 5161026;                         //库存卡作废记录查询
    public const int CM_HYKGL_KCKZFHF = 5161027;                          //库存卡作废恢复
    public const int CM_HYKGL_KCKZFHF_LR = 5161028;                       //库存卡作废恢复录入
    public const int CM_HYKGL_KCKZFHF_SH = 5161029;                       //库存卡作废恢复审核
    public const int CM_HYKGL_KCKZFHF_XS = 5161030;                       //库存卡作废恢复显示
    public const int CM_HYKGL_KCKZFHF_CX = 5161031;                       //库存卡作废恢复记录查询
    public const int CM_HYKGL_KCKYETZD = 5161032;                         //库存卡余额调整单
    public const int CM_HYKGL_KCKYETZD_LR = 5161033;                      //库存卡余额调整单录入
    public const int CM_HYKGL_KCKYETZD_SH = 5161034;                      //库存卡余额调整单审核
    public const int CM_HYKGL_KCKYETZD_XS = 5161035;                      //库存卡余额调整单显示
    public const int CM_HYKGL_KCKYETZD_CX = 5161036;                      //库存卡余额调整单查询
    public const int CM_HYKGL_SrchZKXX = 5161037;                         //按卡号查询制卡信息
    public const int CM_HYKGL_JFKFF = 5161038;                            //会员卡发放
    public const int CM_HYKGL_JFKPLFF = 5161039;                          //会员卡批量发放
    public const int CM_HYKGL_JFKPLFF_LR = 5161040;                       //会员卡批量发放录入
    public const int CM_HYKGL_JFKPLFF_SH = 5161041;                       //会员卡批量发放审核
    public const int CM_HYKGL_JFKPLFF_XS = 5161042;                       //会员卡批量发放显示
    public const int CM_HYKGL_HYDALR = 5161043;                           //会员档案录入
    public const int CM_HYKGL_HYKFS_TJ = 5161044;                         //会员卡发售统计
    public const int CM_HYKGL_HYKHK = 5161045;                            //会员卡换卡
    public const int CM_HYKGL_HYKHK_LR = 5161046;                         //会员卡换卡录入
    public const int CM_HYKGL_HYKHK_SH = 5161047;                         //会员卡换卡审核
    public const int CM_HYKGL_HYKHK_XS = 5161048;                         //会员卡换卡显示
    public const int CM_HYKGL_HYKHK_CX = 5161049;                         //会员卡换卡记录查询
    public const int CM_HYKGL_HYKGHKLX = 5161050;                         //会员卡更换卡类型
    public const int CM_HYKGL_HYKGHKLX_LR = 5161051;                      //会员卡更换卡类型录入
    public const int CM_HYKGL_HYKGHKLX_SH = 5161052;                      //会员卡更换卡类型审核
    public const int CM_HYKGL_HYKGHKLX_XS = 5161053;                      //会员卡更换卡类型显示
    public const int CM_HYKGL_HYKGHKLX_CX = 5161054;                      //会员卡更换卡类型记录查询
    public const int CM_HYKGL_YXQGG = 5161055;                            //会员卡有效期更改
    public const int CM_HYKGL_YXQGG_LR = 5161056;                         //会员卡有效期更改录入
    public const int CM_HYKGL_YXQGG_SH = 5161057;                         //会员卡有效期更改审核
    public const int CM_HYKGL_YXQGG_XS = 5161058;                         //会员卡有效期更改显示
    public const int CM_HYKGL_YXQGG_CX = 5161059;                         //会员卡有效期更改记录查询
    public const int CM_HYKGL_HYKGS = 5161060;                            //会员卡挂失
    public const int CM_HYKGL_HYKGS_LR = 5161061;                         //会员卡挂失录入
    public const int CM_HYKGL_HYKGS_SH = 5161062;                         //会员卡挂失审核
    public const int CM_HYKGL_HYKGS_XS = 5161063;                         //会员卡挂失显示
    public const int CM_HYKGL_HYKGS_CX = 5161064;                         //会员卡挂失记录查询
    public const int CM_HYKGL_HYKGSHF = 5161065;                          //会员卡挂失恢复
    public const int CM_HYKGL_HYKGSHF_LR = 5161066;                       //会员卡挂失恢复录入
    public const int CM_HYKGL_HYKGSHF_SH = 5161067;                       //会员卡挂失恢复审核
    public const int CM_HYKGL_HYKGSHF_XS = 5161068;                       //会员卡挂失恢复显示
    public const int CM_HYKGL_HYKGSHF_CX = 5161069;                       //会员卡挂失恢复记录查询
    public const int CM_HYKGL_HYKZF = 5161070;                            //会员卡作废
    public const int CM_HYKGL_HYKZF_LR = 5161071;                         //会员卡作废录入
    public const int CM_HYKGL_HYKZF_SH = 5161072;                         //会员卡作废审核
    public const int CM_HYKGL_HYKZF_XS = 5161073;                         //会员卡作废显示
    public const int CM_HYKGL_HYKZF_CX = 5161074;                         //会员卡作废记录查询
    public const int CM_HYKGL_HYKZTBD = 5161075;                          //会员卡状态变动
    public const int CM_HYKGL_HYKZTBD_LR = 5161076;                       //会员卡状态变动录入
    public const int CM_HYKGL_HYKZTBD_SH = 5161077;                       //会员卡状态变动审核
    public const int CM_HYKGL_HYKZTBD_XS = 5161078;                       //会员卡状态变动显示
    public const int CM_HYKGL_HYKZTBD_CX = 5161079;                       //会员卡状态变动记录查询
    public const int CM_HYKGL_HYSJHKCL = 5161080;                         //会员升级换卡处理
    public const int CM_HYKGL_HYSJHKCL_LR = 5161081;                      //会员升级换卡处理录入
    public const int CM_HYKGL_HYSJHKCL_SH = 5161082;                      //会员升级换卡处理审核
    public const int CM_HYKGL_HYSJHKCL_XS = 5161083;                      //会员升级换卡处理显示
    public const int CM_HYKGL_SrchHYKSJJL = 5161084;                      //会员卡升级记录查询
    public const int CM_HYKGL_HYJJHKCL = 5161085;                         //会员降级换卡处理
    public const int CM_HYKGL_HYJJHKCL_LR = 5161086;                      //会员降级换卡处理录入
    public const int CM_HYKGL_HYJJHKCL_SH = 5161087;                      //会员降级换卡处理审核
    public const int CM_HYKGL_HYJJHKCL_XS = 5161088;                      //会员降级换卡处理显示
    public const int CM_HYKGL_SrchHYKJJJL = 5161089;                      //会员卡降级记录查询
    public const int CM_HYKGL_YHQZHCKCL = 5161090;                        //优惠券账户存款处理
    public const int CM_HYKGL_YHQZHCKCL_LR = 5161091;                     //优惠券账户存款处理录入
    public const int CM_HYKGL_YHQZHCKCL_SH = 5161092;                     //优惠券账户存款处理审核
    public const int CM_HYKGL_YHQZHCKCL_XS = 5161093;                     //优惠券账户存款处理显示
    public const int CM_HYKGL_YHQZHCKCL_CX = 5161094;                     //优惠券账户存款处理记录查询
    public const int CM_HYKGL_YHQZHPLCKCL = 5161095;                      //优惠券账户批量存款处理
    public const int CM_HYKGL_YHQZHPLCKCL_LR = 5161096;                   //优惠券账户批量存款处理录入
    public const int CM_HYKGL_YHQZHPLCKCL_SH = 5161097;                   //优惠券账户批量存款处理审核
    public const int CM_HYKGL_YHQZHPLCKCL_XS = 5161098;                   //优惠券账户批量存款处理显示
    public const int CM_HYKGL_YHQZHPLCKCL_CX = 5161099;                   //优惠券账户批量存款处理记录查询
    public const int CM_HYKGL_YHQZHQKCL = 5161100;                        //优惠券账户取款处理
    public const int CM_HYKGL_YHQZHQKCL_LR = 5161101;                     //优惠券账户取款处理录入
    public const int CM_HYKGL_YHQZHQKCL_SH = 5161102;                     //优惠券账户取款处理审核
    public const int CM_HYKGL_YHQZHQKCL_XS = 5161103;                     //优惠券账户取款处理显示
    public const int CM_HYKGL_YHQZHQKCL_CX = 5161104;                     //优惠券账户取款处理记录查询
    public const int CM_HYKGL_YHQYEQLTZD = 5161105;                       //优惠券余额清零调整单
    public const int CM_HYKGL_YHQYEQLTZD_LR = 5161106;                    //优惠券余额清零调整单录入
    public const int CM_HYKGL_YHQYEQLTZD_SH = 5161107;                    //优惠券余额清零调整单审核
    public const int CM_HYKGL_YHQYEQLTZD_XS = 5161108;                    //优惠券余额清零调整单显示
    public const int CM_HYKGL_YHQYEQLTZD_CX = 5161109;                    //优惠券余额清零调整单查询
    public const int CM_HYKGL_YHQZHZC = 5161110;                          //优惠券账户转储处理
    public const int CM_HYKGL_YHQZHZC_LR = 5161111;                       //优惠券账户转储录入
    public const int CM_HYKGL_YHQZHZC_SH = 5161112;                       //优惠券账户转储审核
    public const int CM_HYKGL_YHQZHZC_XS = 5161113;                       //优惠券账户转储显示
    public const int CM_HYKGL_YHQZHZC_CX = 5161114;                       //优惠券账户转储记录查询
    public const int CM_HYKGL_JEZQKCL = 5161115;                          //金额账取款处理
    public const int CM_HYKGL_JEZQKCL_LR = 5161116;                       //金额账取款处理录入
    public const int CM_HYKGL_JEZQKCL_SH = 5161117;                       //金额账取款处理审核
    public const int CM_HYKGL_JEZQKCL_XS = 5161118;                       //金额账取款处理显示
    public const int CM_HYKGL_JEZQKCL_CX = 5161119;                       //金额账取款处理记录查询
    public const int CM_HYKGL_CZT = 5161120;                              //会员卡管理操作台
    public const int CM_HYKGL_HYKHS = 5161121;                            //会员卡回收
    public const int CM_HYKGL_HYKHS_LR = 5161122;                         //会员卡回收录入
    public const int CM_HYKGL_HYKHS_SH = 5161123;                         //会员卡回收审核
    public const int CM_HYKGL_HYKHS_XS = 5161124;                         //会员卡回收显示
    public const int CM_HYKGL_HYKHS_CX = 5161125;                         //会员卡回收记录查询
    public const int CM_HYKGL_YHQBGYXQ = 5161126;                         //会员卡变更券帐户有效期
    public const int CM_HYKGL_XGPASSWORD = 5161127;                       //会员卡密码修改
    public const int CM_HYKGL_YHQZHRBB = 5161128;                         //优惠券账户日报表
    public const int CM_HYKGL_YHQZHYBB = 5161129;                         //优惠券账户月报表
    public const int CM_HYKGL_YHQZHNBB = 5161130;                         //优惠券账户年报表
    public const int CM_HYKGL_YHQZHRBB_MD = 5161131;                      //优惠券账户门店日报表
    public const int CM_HYKGL_YHQZHQJDCX = 5161132;                       //按区间段查询优惠券账户报表
    public const int CM_HYKGL_KCKYXQBG = 5161133;                         //库存卡有效期变更
    public const int CM_HYKGL_KCKYXQBG_LR = 5161134;                      //库存卡有效期变更录入
    public const int CM_HYKGL_KCKYXQBG_SH = 5161135;                      //库存卡有效期变更审核
    public const int CM_HYKGL_KCKYXQBG_CX = 5161136;                      //库存卡有效期变更记录查询
    public const int CM_HYKGL_YHQZHCLJLCX = 5161137;                      //优惠券账户处理记录查询
    public const int CM_HYKGL_JEZHCLJLCX = 5161138;                       //金额账户处理记录查询
    public const int CM_HYKGL_HYDALR_LR = 5161139;                        //会员档案录入录入
    public const int CM_HYKGL_HYDALR_QTZJ = 5161140;                      //会员档案录入选择其他证件
    public const int CM_HYKGL_TZDZDY = 5161141;                           //会员档案-通知地址定义
    public const int CM_HYKGL_XQDY = 5161142;                             //会员档案-小区定义
    public const int CM_HYKGL_HYDACX = 5161143;                           //会员档案查询
    public const int CM_HYKGL_YHQ_CurrYE = 5161144;                       //查询优惠券当前余额
    public const int CM_HYKGL_MMCZ = 5161145;                             //会员卡密码重置
    public const int CM_HYKGL_SrchHYKJFRBB = 5161146;                     //会员卡积分日报表
    public const int CM_HYKGL_SrchHYKJFYBB = 5161147;                     //会员卡积分月报表
    public const int CM_HYKGL_CXHYXX = 5161148;                           //查询会员信息
    public const int CM_HYKGL_HYKDJSQGZDY = 5161149;                      //会员卡升级规则定义
    public const int CM_HYKGL_HYKJJGZDY = 5161150;                        //会员卡降级规则定义
    public const int CM_HYKGL_SrchHYKKSJJL = 5161151;                     //会员卡可升级记录查询
    public const int CM_HYKGL_SrchKXXByZT = 5161152;                      //按卡状态汇总查询卡信息
    public const int CM_HYKGL_YHQ_XFTJ = 5161153;                         //按日期统计会员卡优惠券消费
    public const int CM_HYKGL_YHQ_QTCLTJ = 5161154;                       //会员卡优惠券前台处理统计
    public const int CM_HYKGL_YHQ_SSCX_XFTJ = 5161155;                    //会员卡优惠券消费实时查询
    public const int CM_HYKGL_YHQ_XFTJBySH = 5161156;                     //按商户统计会员卡优惠券消费
    public const int CM_HYKGL_YHQ_XFTJByMD = 5161157;                     //按门店统计会员卡优惠券消费
    public const int CM_HYKGL_KCKXXYZ = 5161158;                          //库存卡信息验卡
    public const int CM_HYKGL_HYKBC = 5161159;                            //会员卡磁卡补磁
    public const int CM_HYKGL_KCKBC = 5161160;                            //库存卡磁卡补磁



    public const int CM_HYKGL_ZTBGGZDY = 5161161;                         //会员卡状态变更规则定义
    public const int CM_HYKGL_HYKJKZK = 5161162;                          //会员卡写卡
    public const int CM_HYKGL_JEZCKCL = 5161163;                          //金额账存款处理
    public const int CM_HYKGL_JEZCKCL_LR = 5161164;                       //金额账存款处理录入
    public const int CM_HYKGL_JEZCKCL_SH = 5161165;                       //金额账存款处理审核
    public const int CM_HYKGL_JEZCKCL_CX = 5161166;                       //金额账存款处理记录查询
    public const int CM_HYKGL_GHFXDW = 5161167;                           //会员卡更换发行单位
    public const int CM_HYKGL_ZDBTYBB = 5161168;                          //会员卡状态变动月报表
    public const int CM_HYKGL_QKDY = 5161169;                             //区块定义
    public const int CM_HYKGL_JFKFF_FAST = 5161189;                       //会员卡快速发放
    public const int CM_HYKGL_KCKDC = 5161191;                            //库存卡导出
    public const int CM_HYKGL_JFKFF_CX = 5161192;                         //会员卡发放查询
    public const int CM_HYKGL_YHQZHPLQKCL = 5161193;                      //优惠券账户批量取款
    public const int CM_HYKGL_YHQZHPLQKCL_LR = 5161194;                   //优惠券账户批量取款录入
    public const int CM_HYKGL_YHQZHPLQKCL_SH = 5161195;                   //优惠券账户批量取款审核
    public const int CM_HYKGL_YHQZHPLQKCL_XS = 5161196;                   //优惠券账户批量取款显示
    public const int CM_HYKGL_YHQZHPLQKCL_CX = 5161197;                   //优惠券账户批量取款查询
    public const int CM_HYKGL_HYDHQK = 5161198;                           //会员兑换情况
    public const int CM_HYKGL_JFXFMX = 5161199;                           //消费积分明细查询
    public const int CM_HYKGL_GXSHJFMX = 5161200;                         //管辖商户积分明细查询
    public const int CM_HYKGL_JFBDMX = 5161201;                           //积分变动明细查询
    public const int CM_HYKGL_YHQZHMX = 5161202;                          //优惠券账户明细查询


    public const int CM_HYKGL_GHKLXGZ1 = 5161205;                           //会员卡更换卡类型规则新

    public const int CM_HYKGL_PLGHKLX = 5161206;                         //批量更换卡类型
    public const int CM_HYKGL_PLGHKLX_LR = 5161207;                      //批量更换卡类型录入
    public const int CM_HYKGL_PLGHKLX_SH = 5161208;                      //批量更换卡类型审核
    public const int CM_HYKGL_PLGHKLX_XS = 5161209;                      //批量更换卡类型显示
    public const int CM_HYKGL_PLGHKLX_CX = 5161210;                      //批量更换卡类型记录查询

    public const int CM_HYKGL_SFQMX = 5161203;                            //收发券记录明细查询
    public const int CM_HYKGL_HYTJGZ = 5161204;                           //会员推荐规则

    public const int CM_HYKGL_SJGZ = 5161205;                           //会员推荐规则
    public const int CM_HYKGL_KCMZKPD = 5161206;                           //库存面值卡盘点
    public const int CM_HYKGL_FWNRDEF = 5161211;                           //服务内容定义
    public const int CM_HYKGL_XSFWDEF = 5161212;                           //会员享受服务定义
    public const int CM_HYKGL_FWXS = 5161213;                             //会员享受服务
    public const int CM_HYKGL_FWXS_LR = 5161214;                             //会员享受服务录入
    public const int CM_HYKGL_FWXS_SH = 5161215;                             //会员享受服务审核
    public const int CM_HYKGL_FWXS_CX = 5161216;                             //会员享受服务查询
    public const int CM_HYKGL_HYFWPLYC = 5161217;                             //会员服务批量预存
    public const int CM_HYKGL_HYFWPLYC_LR = 5161218;                             //会员服务批量预存录入
    public const int CM_HYKGL_HYFWPLYC_SH = 5161219;                             //会员服务批量预存审核
    public const int CM_HYKGL_HYFWPLYC_CX = 5161220;                             //会员服务批量预存查询
    public const int CM_HYKGL_FWCLCX = 5161221;                             //会员服务处理记录查询

    public const int CM_HYKGL_EYJFGZCX = 5161222;                         //会员恶意积分跟踪查询
    public const int CM_HYKGL_CSJWXFHYCX = 5161223;                       //长时间未消费会员查询
    public const int CM_HYKGL_HYZLBQCX = 5161224;                         //会员资料不全查询
    public const int CM_HYKGL_WXFDJKCX = 5161225;                         //未消费冻结卡查询
    public const int CM_HYKGL_HYJFCX = 5161226;                           //会员积分查询
    public const int CM_HYKGL_HYGLXFCX = 5161227;                         //会员品牌关联消费查询
    public const int CM_HYKGL_GLGTGYCX = 5161228;                         //共同会员关联消费查询
    public const int CM_HYKGL_HYPLGLXFCX = 5161229;                       //会员品类关联消费查询

    public const int CM_HYKGL_FWZHCX = 5161230;                             //会员服务账户查询
    public const int CM_HYKGL_BQLBDY = 5161231;                             //标签类别定义
    public const int CM_HYKGL_BQXMDY = 5161232;                             //标签项目定义
    public const int CM_HYKGL_BQZDY = 5161233;                              //标签值定义
    public const int CM_HYKGL_HYPLDBQ = 5161234;                            //会员批量打标签
    public const int CM_HYKGL_HYDBQ = 5161235;                              //会员打标签
    public const int CM_HYKGL_MDJFBB = 5161236;                             //按门店积分对账表
    public const int CM_HYKGL_FWQKCX = 5161237;                             //服务享受情况查询
    public const int CM_HYKGL_JEZHHZ = 5161238;                             //金额账户汇总查询
    public const int CM_HYKGL_GKZYDALR = 5161239;                           //顾客重要档案修改
    public const int CM_HYKGL_LMSHDY = 5161240;                             //联盟商户定义 
    public const int CM_HYKGL_LMSHDY_LR = 5161241;                          //联盟商户定义录入
    public const int CM_HYKGL_LMSHDY_SH = 5161242;                          //联盟商户定义审核
    public const int CM_HYKGL_LMSHDY_CX = 5161243;                          //联盟商户定义查询
    public const int CM_HYKGL_LMSHJCXXDY = 5161244;                         //联盟商户基础信息定义 
    public const int CM_HYKGL_LMSHFWDY = 5161245;                           //联盟商户服务定义 
    public const int CM_HYKGL_HYSJHMQL = 5161246;                           //会员手机号码清理 
    public const int CM_HYKGL_HYSJHMQL_LR = 5161247;                        //会员手机号码清理录入
    public const int CM_HYKGL_HYSJHMQL_CX = 5161248;                        //会员手机号码清理查询
    public const int CM_HYKGL_HYFSKBD = 5161249;                            //会员附属卡绑定 
    public const int CM_HYKGL_HYFSKBD_LR = 5161250;                         //会员附属卡绑定录入 
    public const int CM_HYKGL_HYFSKBD_SH = 5161251;                         //会员附属卡绑定审核
    public const int CM_HYKGL_HYFSKBD_CX = 5161252;                         //会员附属卡绑定查询
    public const int CM_HYKGL_CXHYXX_XS = 5161253;                          //查询会员信息公开显示
    public const int CM_HYKGL_LMSHHTDY = 5161254;                           //联盟商户合同定义 
    public const int CM_HYKGL_LMSHHTDY_LR = 5161255;                        //联盟商户合同定义录入
    public const int CM_HYKGL_LMSHHTDY_SH = 5161256;                        //联盟商户合同定义审核
    public const int CM_HYKGL_LMSHHTDY_CX = 5161257;                        //联盟商户合同定义查询
    public const int CM_HYKGL_LMSHFYLR = 5161258;                           //联盟商户费用录入 
    public const int CM_HYKGL_LMSHFYLR_LR = 5161259;                        //联盟商户费用录入录入
    public const int CM_HYKGL_LMSHFYLR_SH = 5161260;                        //联盟商户费用录入审核
    public const int CM_HYKGL_LMSHFYLR_CX = 5161261;                        //联盟商户费用录入查询
    public const int CM_HYKGL_LMSHJS = 5161262;                             //联盟商户结算录入 
    public const int CM_HYKGL_LMSHJS_LR = 5161263;                          //联盟商户费用结算录入
    public const int CM_HYKGL_LMSHJS_SH = 5161264;                          //联盟商户费用结算审核
    public const int CM_HYKGL_LMSHJS_CX = 5161265;                          //联盟商户费用结算查询

    public const int CM_HYKGL_HYKRFM = 5161266;                             //RFM会员查询
    public const int CM_HYKGL_SMZQ = 5161267;                               //会员卡生命周期
    public const int CM_HYKGL_HYBQPLDR = 5161268;                           //会员标签批量导入
    public const int CM_HYKGL_SRHYHZ = 5161269;                             //生日会员统计
    public const int CM_HYKGL_YSJHYHZ = 5161270;                            //已升级会员统计
    public const int CM_HYKGL_LMSHDHMHX = 5161271;                          //联盟商户兑换码核销
    public const int CM_HYKGL_LMSHDHMCX = 5161272;                          //联盟商户兑换码核销记录查询
    public const int CM_HYKGL_LMSHDHMFF = 5161273;                          //联盟商户兑换码发放
    public const int CM_HYKGL_HYSJJLCX = 5161274;                          //会员升级记录查询
    public const int CM_HYKGL_HYJCBQGZDY = 5161275;                         //会员基础标签规则定义
    public const int CM_HYKGL_HYJCBQGZDY_LR = 5161276;                      //会员基础标签规则定义录入
    public const int CM_HYKGL_HYJCBQGZDY_CX = 5161277;                      //会员基础标签规则定义查询

    public const int CM_HYKGL_ZHBQGZDY = 5161278;                         //组合标签规则定义
    public const int CM_HYKGL_HYKTY = 5161279;                         //会员卡停用
    public const int CM_HYKGL_HYKTY_LR = 5161280;                          //会员卡停用录入
    public const int CM_HYKGL_HYKTY_SH = 5161281;                          //会员卡停用审核
    public const int CM_HYKGL_HYKTY_CX = 5161282;                          //会员卡停用查询
    public const int CM_HYKGL_HYFSDXYJ = 5161283;                          //会员短信邮件发送
    public const int CM_HYKGL_HYFSDXYJ_CX = 5161284;                       //会员短信邮件发送查询
    public const int CM_HYKGL_HYFSDXYJ_LR = 5161285;                       //会员短信邮件发送录入
    public const int CM_HYKGL_HYFSDXYJ_SH = 5161286;                       //会员短信邮件发送审核
    public const int CM_HYKGL_HYFSDXYJ_QD = 5161287;                       //会员短信邮件发送启动
    public const int CM_HYKGL_HYFSDXYJ_ZZ = 5161288;                       //会员短信邮件发送终止
    public const int CM_HYKGL_BQHYCX = 5161289;                               //标签会员查询
    public const int CM_HYKGL_THHYYJCX = 5161290;                             //退货会员预警查询
    public const int CM_HYKGL_CXFQJL = 5161291;                               //查询发券记录信息
    public const int CM_HYKGL_CXFQJL_CX = 5161292;                             //查询发券记录信息
    public const int CM_HYKGL_JEZRBB = 5161293;                               //金额帐日报表
    public const int CM_HYKGL_JEZRBB_CX = 5161294;                             //金额帐日报表查询
    public const int CM_HYKGL_JEZYBB = 5161295;                               //金额帐月报表
    public const int CM_HYKGL_JEZYBB_CX = 5161296;                             //金额帐月报表查询
    public const int CM_HYKGL_JEZYECX = 5161297;                               //金额帐余额查询
    public const int CM_HYKGL_JEZYECX_CX = 5161298;                             //金额帐余额查询
    public const int CM_HYKGL_THJLCX = 5161299;                               //退货记录查询
    public const int CM_HYKGL_YSJHYCX = 5161300;                               //已升级会员查询
    public const int CM_HYKGL_YSJHYCX_CX = 5161301;                             //已升级会员查询
    public const int CM_HYKGL_YEZQXJC = 5161302;                              //检验金额帐余额正确性
    public const int CM_HYKGL_PLXGHYXQ = 5161303;                               //批量修改会员所属小区
    public const int CM_HYKGL_PLXGHYXQ_CX = 5161304;                            //批量修改会员所属小区查询
    public const int CM_HYKGL_PLXGHYXQ_LR = 5161305;                            //批量修改会员所属小区录入
    public const int CM_HYKGL_PLXGHYXQ_SH = 5161306;                            //批量修改会员所属小区审核
    public const int CM_HYKGL_HYDALR_SC = 5161307;                              //会员档案录入删除
    public const int CM_HYKGL_SBGLD = 5161308;                                  //商标关联度查询

    public const int CM_LMSHGL_BMQDY = 5161309;                           //编码券定义
    public const int CM_LMSHGL_BMQDY_CX = 5161310;                        //编码券定义查询
    public const int CM_LMSHGL_BMQDY_LR = 5161311;                        //编码券定义录入
    public const int CM_LMSHGL_BMQFFGZDY = 5161312;                       //编码券发放规则定义
    public const int CM_LMSHGL_BMQFFGZDY_CX = 5161313;                    //编码券发放规则定义查询
    public const int CM_LMSHGL_BMQFFGZDY_LR = 5161314;                    //编码券发放规则定义录入

    public const int CM_LMSHGL_BMQFFD = 5161315;                          //编码券发放单
    public const int CM_LMSHGL_BMQFFD_CX = 5161316;                       //编码券发放单查询
    public const int CM_LMSHGL_BMQFFD_LR = 5161317;                       //编码券发放单录入
    public const int CM_LMSHGL_BMQFFD_SH = 5161318;                       //编码券发放单审核
    public const int CM_LMSHGL_BMQFFD_QD = 5161319;                       //编码券发放单启动
    public const int CM_LMSHGL_BMQFFD_ZZ = 5161320;                       //编码券发放单终止
    //弹出框菜单
    public const int CM_HYKGL_HYKCX = 5161321;                            //弹出框会员卡查询
    public const int CM_HYKGL_KCKCX = 5161322;                            //弹出框库存卡查询
    public const int CM_CRMART_TREE = 5161323;                            //所有树形弹出框查询
    public const int CM_CRMART_TREEFL = 5161800;                            //所有树形弹出框查询FL

    public const int CM_CRMART_YHQZHCX = 5161324;                         //优惠券账户弹出查询
    public const int CM_CRMART_XZJFDHLP = 5161327;                        //微信积分兑换礼品
    //public const int CM_CRMART_WXYYJL = 5161328;                          //微信预约记录弹出框

    public const int CM_HYKGL_HYKLYSQ = 5161329;                          //会员卡领用申请                     
    public const int CM_HYKGL_HYKLYSQ_CX = 5161330;                       //会员卡领用申请单查询
    public const int CM_HYKGL_HYKLYSQ_LR = 5161331;                       //会员卡领用申请单录入
    public const int CM_HYKGL_HYKLYSQ_SH = 5161332;                       //会员卡领用申请单审核

    public const int CM_HYKGL_HYKTK = 5161333;                            //会员卡退卡                     
    public const int CM_HYKGL_HYKTK_CX = 5161334;                         //会员卡退卡单查询
    public const int CM_HYKGL_HYKTK_LR = 5161335;                         //会员卡退卡单录入
    public const int CM_HYKGL_HYKTK_SH = 5161336;                         //会员卡退卡单审核

    //public const int CM_HYKGL_HYKZR = 5161337;                            //会员卡转让
    //public const int CM_HYKGL_HYKZR_LR = 5161338;                         //会员卡转让单录入                     
    //public const int CM_HYKGL_HYKZR_CX = 5161339;                         //会员卡转让单查询
    //public const int CM_HYKGL_HYKZR_SH = 5161340;                         //会员卡转让单审核
    //public const int CM_HYKGL_DZKZSTK = 5161341;                          //会员卡转让
    //public const int CM_HYKGL_DZKZSTK_LR = 5161342;                       //会员卡转让单录入                     
    //public const int CM_HYKGL_DZKZSTK_CX = 5161343;                       //会员卡转让单查询
    //public const int CM_HYKGL_DZKZSTK_SH = 5161344;                       //会员卡转让单审核
    public const int CM_HYKGL_BQZKLXDY = 5161345;
    public const int CM_HYKGL_HYTJGZ_CX = 5161346;                        //会员推荐规则录入
    public const int CM_HYKGL_HYTJGZ_LR = 5161347;                        //会员推荐规则查询
    public const int CM_HYKGL_HYTJGZ_SH = 5161348;                        //会员推荐规则审核
    public const int CM_HYKGL_HYTJGZ_QD = 5161349;                        //会员推荐规则启动
    public const int CM_HYKGL_HYTJGZ_ZZ = 5161350;                        //会员推荐规则终止
    public const int CM_HYKGL_HYDADR = 5161352;                           //会员档案导入
    public const int CM_HYKGL_HYDADR_LR = 5161353;                        //会员档案导入录入
    public const int CM_HYKGL_HYKXF = 5161354;                            //会员续卡费
    public const int CM_HYKGL_HYKXF_CX = 5161355;                         //会员续卡费录入
    public const int CM_HYKGL_HYKXF_LR = 5161356;                         //会员续卡费查询
    public const int CM_HYKGL_HYKXF_SH = 5161357;                         //会员续卡费审核

    #endregion
    #region CM_MZKGL

    public const int CM_MZKGL_QKCL = 5165001;                             //面值卡取款处理
    public const int CM_MZKGL_QKCL_LR = 5165002;                          //面值卡取款处理录入
    public const int CM_MZKGL_QKCL_SH = 5165003;                          //面值卡取款处理审核
    public const int CM_MZKGL_QKCL_XS = 5165004;                          //面值卡取款处理显示
    public const int CM_MZKGL_QKCL_CX = 5165005;                          //面值卡取款处理记录查询
    public const int CM_MZKGL_KCKZF = 5165006;                            //库存面值卡作废(有下级菜单)
    public const int CM_MZKGL_KCKZF_LR = 5165007;                         //库存面值卡作废录入
    public const int CM_MZKGL_KCKZF_SH = 5165008;                         //库存面值卡作废审核
    public const int CM_MZKGL_KCKZF_XS = 5165009;                         //库存面值卡作废显示
    public const int CM_MZKGL_KCKZF_CX = 5165010;                         //库存面值卡作废记录查询
    public const int CM_MZKGL_KCKZFHF = 5165011;                          //库存面值卡作废恢复(有下级菜单)
    public const int CM_MZKGL_KCKZFHF_LR = 5165012;                       //库存面值卡作废恢复录入
    public const int CM_MZKGL_KCKZFHF_SH = 5165013;                       //库存面值卡作废恢复审核
    public const int CM_MZKGL_KCKZFHF_XS = 5165014;                       //库存面值卡作废恢复显示
    public const int CM_MZKGL_KCKZFHF_CX = 5165015;                       //库存面值卡作废恢复记录查询
    public const int CM_MZKGL_KCKYXQBG = 5165016;                         //库存卡有效期变更
    public const int CM_MZKGL_KCKYXQBG_LR = 5165017;                      //库存卡有效期变更录入
    public const int CM_MZKGL_KCKYXQBG_SH = 5165018;                      //库存卡有效期变更审核
    public const int CM_MZKGL_KCKYXQBG_CX = 5165019;                      //库存卡有效期变更记录查询
    public const int CM_MZKGL_CKCL = 5165020;                             //面值卡存款处理
    public const int CM_MZKGL_CKCL_LR = 5165021;                          //面值卡存款处理录入
    public const int CM_MZKGL_CKCL_SH = 5165022;                          //面值卡存款处理审核
    public const int CM_MZKGL_CKCL_CX = 5165023;                          //面值卡存款处理记录查询
    public const int CM_MZKGL_PLCKCL = 5165024;                           //面值卡批量存款处理
    public const int CM_MZKGL_PLCKCL_LR = 5165025;                        //面值卡批量存款处理录入
    public const int CM_MZKGL_PLCKCL_SH = 5165026;                        //面值卡批量存款处理审核
    public const int CM_MZKGL_PLCKCL_CX = 5165027;                        //面值卡批量存款处理记录查询
    public const int CM_MZKGL_CKMX_CX = 5165028;                          //面值卡存款明细查询
    public const int CM_MZKGL_CZKXYMDDEF = 5165029;                       //面值卡限用门店设置
    public const int CM_MZKGL_CZT = 5165030;                              //面值卡操作台
    public const int CM_MZKGL_MZKJEZC = 5165031;                          //面值卡转储
    public const int CM_MZKGL_MZKJEZC_LR = 5165032;                       //面值卡转储录入
    public const int CM_MZKGL_MZKJEZC_SH = 5165033;                       //面值卡转储审核
    public const int CM_MZKGL_MZKJEZC_CX = 5165034;                       //面值卡转储记录查询
    public const int CM_MZKGL_MZKYEQL = 5165035;                          //面值卡清零
    public const int CM_MZKGL_MZKYEQL_LR = 5165036;                       //面值卡清零录入
    public const int CM_MZKGL_MZKYEQL_SH = 5165037;                       //面值卡清零审核
    public const int CM_MZKGL_MZKYEQL_CX = 5165038;                       //面值卡清零记录查询
    public const int CM_MZKGL_MZKCLJL_CX = 5165039;                       //面值卡金额账处理记录
    public const int CM_MZKGL_JEZRBB = 5165040;                           //面值卡金额账日报表
    public const int CM_MZKGL_FS = 5165041;                               //面值卡发售
    public const int CM_MZKGL_JEZYBB = 5165042;                           //面值卡金额账月报表
    public const int CM_MZKGL_XFCX = 5165043;	                          //面值卡消费查询
    public const int CM_MZKGL_JEZ_XFTJ = 5165044;	                      //面值卡消费统计
    public const int CM_MZKGL_JEZ_SSCX_XFTJ = 5165045;	                  //面值卡消费统计实时查询 //未添加why
    public const int CM_MZKGL_SKDPLQD = 5165046;                          //面值卡售卡单批量启动
    public const int CM_MZKGL_SKDPLQD_LR = 5165047;                       //面值卡售卡单批量启动录入
    public const int CM_MZKGL_SKDPLQD_SH = 5165048;                       //面值卡售卡单批量启动审核
    public const int CM_MZKGL_SKDPLQD_CX = 5165049;                       //面值卡售卡单批量启动记录查询
    public const int CM_MZKGL_MZKCZCL = 5165050;                          //面值卡冲正处理记录查询
    public const int CM_MZKGL_MZKCZCLJL_LR = 5165051;                     //面值卡冲正处理录入
    public const int CM_MZKGL_DQYECX = 5165052;	                          //面值卡当前余额查询
    public const int CM_MZKGL_DQYEHZ = 5165053;	                          //面值卡当前余额汇总
    public const int CM_MZKGL_MZKBKCK = 5165054;                          //面值卡并卡拆卡
    public const int CM_MZKGL_MZKBKCK_LR = 5165055;                       //面值卡并卡拆卡录入
    public const int CM_MZKGL_MZKBKCK_SH = 5165056;                       //面值卡并卡拆卡审核
    public const int CM_MZKGL_MZKBKCK_CX = 5165057;                       //面值卡并卡拆卡记录查询
    public const int CM_MZKGL_MZKZF = 5165058;                            //面值卡作废
    public const int CM_MZKGL_MZKZF_LR = 5165059;                         //面值卡作废录入
    public const int CM_MZKGL_MZKZF_SH = 5165060;                         //面值卡作废审核
    public const int CM_MZKGL_MZKZF_CX = 5165061;                         //面值卡作废记录查询
    public const int CM_MZKGL_DKHQYKHDA = 5165062;                        //大客户企业客户档案
    public const int CM_MZKGL_DKHQYKHDA_LR = 5165063;                     //大客户企业客户档案录入
    public const int CM_MZKGL_DKHQYKHDA_SH = 5165064;                     //大客户企业客户档案审核
    public const int CM_MZKGL_DKHQYKHDA_CX = 5165065;                     //大客户企业客户档案记录查询
    public const int CM_MZKGL_DKHSXHT = 5165066;                          //大客户赊销合同
    public const int CM_MZKGL_DKHSXHT_LR = 5165067;                       //大客户赊销合同录入
    public const int CM_MZKGL_DKHSXHT_SH = 5165068;                       //大客户赊销合同审核
    public const int CM_MZKGL_DKHSXHT_CX = 5165069;                       //大客户赊销合同记录查询
    public const int CM_MZKGL_DKHSXD = 5165070;                           //大客户赊销单
    public const int CM_MZKGL_DKHSXD_LR = 5165071;                        //大客户赊销单录入
    public const int CM_MZKGL_DKHSXD_SH = 5165072;                        //大客户赊销单审核
    public const int CM_MZKGL_DKHSXD_CX = 5165073;                        //大客户赊销单记录查询
    public const int CM_MZKDKHGL_DKHZSLPCX = 5165074;                     //大客户赠送礼品查询
    public const int CM_MZKDKHGL_MZKFSTJ = 5165075;                       //面值卡发售统计（面值）
    public const int CM_MZKDKHGL_DKHXSTJ = 5165076;                     //大客户销售统计（按收款方式）     
    public const int CM_MZKGL_MZKMRCZJEXZ_SH = 5165077;                   //面值卡每日充值金额限制(审核)
    public const int CM_MZKGL_MZKZPQD = 5165078;                          //面值卡支票启动
    public const int CM_MZKGL_MZKZPQD_LR = 5165079;                       //面值卡支票启动(录入)
    public const int CM_MZKGL_MZKZPQD_CX = 5165080;                       //面值卡支票启动(查询)
    public const int CM_MZKGL_MZKZPQD_SH = 5165081;                       //面值卡支票启动(审核)
    public const int CM_MZKGL_MZKHS = 5165082;                            //面值卡回收
    public const int CM_MZKGL_MZKHS_LR = 5165083;                         //面值卡回收(录入)
    public const int CM_MZKGL_MZKHS_CX = 5165084;                         //面值卡回收(查询)
    public const int CM_MZKGL_MZKHS_SH = 5165085;                         //面值卡回收(审核)
    public const int CM_MZKGL_MZKZSGZ = 5165086;                          //面值卡赠送规则
    public const int CM_MZKGL_MZKZSGZ_LR = 5165087;                       //面值卡赠送规则(录入)
    public const int CM_MZKGL_MZKZSGZ_CX = 5165088;                       //面值卡赠送规则(查询)
    public const int CM_MZKDKHGL_DKHDJDY = 5165089;                       //大客户等级定义
    public const int CM_MZKDKHGL_DKHDJDY_LR = 5165090;                    //大客户等级定义(录入)
    public const int CM_MZKDKHGL_DKHSXFK = 5165091;                       //大客户赊销还款单
    public const int CM_MZKDKHGL_DKHSXFK_LR = 5165092;                    //大客户赊销还款单(录入)
    public const int CM_MZKDKHGL_DKHSXFK_CX = 5165093;                    //大客户赊销还款单(查询)
    public const int CM_MZKDKHGL_DKHSXFK_SH = 5165094;                    //大客户赊销还款单(审核)
    public const int CM_MZKDKHGL_DKHZLLR = 5165095;                       //大客户资料录入
    public const int CM_MZKDKHGL_DKHZLLR_LR = 5165096;                    //大客户资料录入(录入)
    public const int CM_MZKDKHGL_DKHZLLR_CX = 5165097;                    //大客户资料录入(查询)
    public const int CM_MZKDKHGL_KFZYBGJL = 5165098;                      //客服专员变更记录
    public const int CM_MZKDKHGL_KFZYBGJL_LR = 5165099;                   //客服专员变更记录(录入)
    public const int CM_MZKDKHGL_KFZYBGJL_CX = 5165100;                   //客服专员变更记录(查询)
    public const int CM_MZKDKHGL_GLHYYDBY = 5165101;                      //关联会员与代办员
    public const int CM_MZKDKHGL_GLHYYDBY_LR = 5165102;                   //关联会员与代办员(录入)
    public const int CM_MZKDKHGL_GLHYYDBY_CX = 5165103;                   //关联会员与代办员(查询)
    public const int CM_MZKDKHGL_DBYZLXG = 5165104;                       //代办员资料修改（未关联会员卡）
    public const int CM_MZKDKHGL_CZKZKGZ = 5165105;                       //储值卡赠卡规则
    public const int CM_MZKDKHGL_CZKZKGZ_LR = 5165106;                    //储值卡赠卡规则(录入)
    public const int CM_MZKDKHGL_CZKZLPGZ = 5165107;                      //储值卡赠礼品规则
    public const int CM_MZKDKHGL_CZKZLPGZ_LR = 5165108;                   //储值卡赠礼品规则(录入)
    public const int CM_MZKDKHGL_DKHZYDY = 5165109;                       //大客户专员定义
    public const int CM_MZKDKHGL_DKHZYDY_LR = 5165110;                    //大客户专员定义(录入)
    public const int CM_MZKDKHGL_DKHJFSZ = 5165111;                       //大客户售卡积分设置
    public const int CM_MZKDKHGL_QYDBYGL = 5165112;                       //企业代办员管理
    public const int CM_MZKDKHGL_ZFFSDY = 5165113;                        //大客户支付方式定义
    public const int CM_MZKGL_FS_LR = 5165114;                            //面值卡发售(录入)
    public const int CM_MZKGL_FS_CX = 5165115;                            //面值卡发售（查询）
    public const int CM_MZKGL_FS_SH = 5165116;                            //面值卡发售（审核）
    public const int CM_MZKGL_SKJKD = 5165119;                            //面值卡售卡缴款单
    public const int CM_MZKGL_SKJKD_LR = 5165120;                         //面值卡售卡缴款单录入
    public const int CM_MZKGL_SKJKD_SH = 5165121;                         //面值卡售卡缴款单审核
    public const int CM_MZKGL_SKJKD_CX = 5165122;                         //面值卡售卡缴款单查询  
    public const int CM_MZKGL_JKDPLQD = 5165123;                          //面值卡缴款单批量启动
    public const int CM_MZKGL_JKDPLQD_LR = 5165124;                       //面值卡缴款单批量启动录入
    public const int CM_MZKGL_JKDPLQD_SH = 5165125;                       //面值卡缴款单批量启动审核
    public const int CM_MZKGL_JKDPLQD_CX = 5165126;                       //面值卡缴款单批量启动查询
    public const int CM_MZKGL_JEZHCLJLCX = 5165127;                       //金额账户处理记录查询
    public const int CM_MZKGL_MZKJKD = 5165128;                           //面值卡交款单
    public const int CM_MZKGL_MZK_LQSQJL = 5165129;                       //面值卡领取申请记录    
    public const int CM_MZKDKHGL_SKDXGKHZL_CX = 5165130;                  //售卡单修改客户资料(查询)
    public const int CM_MZKDKHGL_SKDXGKHZL_SH = 5165131;                  //售卡单修改客户资料(审核)    
    public const int CM_MZKGL_FSNEW = 5165132;                          //面值卡售卡单（新）
    public const int CM_MZKGL_FSNEW_LR = 5165133;                       //面值卡售卡单（新）录入
    public const int CM_MZKGL_FSNEW_SH = 5165134;                       //面值卡售卡单（新）审核
    public const int CM_MZKGL_FSNEW_CX = 5165135;                       //面值卡售卡单（新）查询    
    public const int CM_MZKGL_MZKJKDNEW = 5165136;                        //面值卡交款单新
    public const int CM_MZKGL_MZKPDHZ = 5165137;                          //面值卡盘点汇总
    public const int CM_MZKGL_MZKPDSY = 5165138;                          //面值卡盘点损益
    public const int CM_MZKGL_MZKJEZBB = 5165139;                         //面值卡金额账报表   
    public const int CM_MZKGL_TS = 5165140;                           //面值卡退卡单（新）
    public const int CM_MZKGL_TS_LR = 5165141;                        //面值卡退卡单（新）录入
    public const int CM_MZKGL_TS_SH = 5165142;                        //面值卡退卡单（新）审核
    public const int CM_MZKGL_TS_CX = 5165143;                        //面值卡退卡单（新）查询
    public const int CM_MZKGL_TS0 = 5165144;                          //0面值卡退卡单（新）
    public const int CM_MZKGL_TS0_LR = 5165145;                       //0面值卡退卡单（新）录入
    public const int CM_MZKGL_TS0_SH = 5165146;                       //0面值卡退卡单（新）审核
    public const int CM_MZKGL_TS0_CX = 5165147;                       //0面值卡退卡单（新）查询

    public const int CM_MZKGL_TSA = 5165148;                          //面值卡作废单（新）
    public const int CM_MZKGL_TSA_LR = 5165149;                       //面值卡作废单（新）录入
    public const int CM_MZKGL_TSA_SH = 5165150;                       //面值卡作废单（新）审核
    public const int CM_MZKGL_TSA_CX = 5165151;                       //面值卡作废单（新）查询

    public const int CM_MZKGL_FSCK = 5165152;                          //面值卡售卡存款单（新）
    public const int CM_MZKGL_FSCK_LR = 5165153;                       //面值卡售卡存款单（新）录入
    public const int CM_MZKGL_FSCK_SH = 5165154;                       //面值卡售卡存款单（新）审核
    public const int CM_MZKGL_FSCK_CX = 5165155;                       //面值卡售卡存款单（新）查询

    public const int CM_MZKGL_TSCK = 5165156;                          //面值卡退卡存款单（新）
    public const int CM_MZKGL_TSCK_LR = 5165157;                       //面值卡退卡存款单（新）录入
    public const int CM_MZKGL_TSCK_SH = 5165158;                       //面值卡退卡存款单（新）审核
    public const int CM_MZKGL_TSCK_CX = 5165159;                       //面值卡退卡存款单（新）查询


    public const int CM_MZKGL_MZKJK = 5165160;                            //面值卡建卡处理
    public const int CM_MZKGL_MZKJK_LR = 5165161;                         //面值卡建卡录入
    public const int CM_MZKGL_MZKJK_SH = 5165162;                         //面值卡建卡审核
    public const int CM_MZKGL_MZKJK_XK = 5165163;                         //面值卡建卡写卡
    public const int CM_MZKGL_MZKJK_CX = 5165164;                         //面值卡建卡查询

    public const int CM_MZKGL_MZKLQ = 5165165;                            //面值卡领取处理
    public const int CM_MZKGL_MZKLQ_LR = 5165166;                         //面值卡领取录入
    public const int CM_MZKGL_MZKLQ_SH = 5165167;                         //面值卡领取审核
    public const int CM_MZKGL_MZKLQ_CX = 5165168;                         //面值卡领取查询

    public const int CM_MZKGL_MZKKCBB = 5165169;                          //面值卡库存报表

    public const int CM_MZKGL_MZKDB = 5165170;                            //面值卡调拨处理
    public const int CM_MZKGL_MZKDB_LR = 5165171;                         //面值卡调拨录入
    public const int CM_MZKGL_MZKDB_SH = 5165172;                         //面值卡调拨审核
    public const int CM_MZKGL_MZKDB_CX = 5165173;                         //面值卡调拨查询
    public const int CM_MZKGL_MZKZPPLQD = 5165174;                        //面值卡支票批量启动
    public const int CM_MZKGL_MZKQKGX = 5165175;                          //面值卡欠款购销
    public const int CM_MZKGL_MZKQKGX_LR = 5165176;                       //面值卡欠款购销录入
    public const int CM_MZKGL_MZKQKGX_CX = 5165253;                       //面值卡欠款购销查询
    public const int CM_MZKGL_MZKHK = 5165177;                            //面值卡换卡    
    public const int CM_MZKGL_MZKHK_LR = 5165178;                         //面值卡换卡录入
    public const int CM_MZKGL_MZKHK_SH = 5165179;                         //面值卡换卡审核
    public const int CM_MZKGL_MZKHK_CX = 5165180;                         //面值卡换卡查询

    public const int CM_MZKGL_MZKGS = 5165181;                            //面值卡挂失
    public const int CM_MZKGL_MZKGS_LR = 5165182;                         //面值卡挂失录入
    public const int CM_MZKGL_MZKGS_SH = 5165183;                         //面值卡挂失审核
    public const int CM_MZKGL_MZKGS_XS = 5165184;                         //面值卡挂失显示
    public const int CM_MZKGL_MZKGS_CX = 5165185;                         //面值卡挂失记录查询
    public const int CM_MZKGL_MZKGSHF = 5165186;                          //面值卡挂失恢复
    public const int CM_MZKGL_MZKGSHF_LR = 5165187;                       //面值卡挂失恢复录入
    public const int CM_MZKGL_MZKGSHF_SH = 5165188;                       //面值卡挂失恢复审核
    public const int CM_MZKGL_MZKGSHF_XS = 5165189;                       //面值卡挂失恢复显示
    public const int CM_MZKGL_MZKGSHF_CX = 5165190;                       //面值卡挂失恢复记录查询
    public const int CM_HYKGL_MZKZTBD = 5165191;                          //面值卡状态变动
    public const int CM_HYKGL_MZKZTBD_LR = 5165192;                       //面值卡状态变动
    public const int CM_HYKGL_MZKZTBD_SH = 5165193;                       //面值卡状态变动
    public const int CM_HYKGL_MZKZTBD_CX = 5165194;                       //面值卡状态变动

    public const int CM_MZKDKHGL_CZKZJFGZ = 5165195;                      //储值卡赠积分规则
    public const int CM_MZKDKHGL_CZKZMZKGZ = 5165196;                     //储值卡赠面值卡规则
    public const int CM_MZKGL_MZKBGZBB = 5165197;                         //面值卡库存卡保管帐报表
    public const int CM_MZKGL_MZKBGKC_CX = 5165198;                       //面值卡保管库存查询
    public const int CM_MZKGL_KCKYETZD = 5165201;                         //面值卡库存卡余额调整单
    public const int CM_MZKGL_KCKYETZD_LR = 5165202;                      //面值卡库存卡余额调整单录入
    public const int CM_MZKGL_KCKYETZD_SH = 5165203;                      //面值卡库存卡余额调整单审核
    public const int CM_MZKGL_KCKYETZD_XS = 5165204;                      //面值卡库存卡余额调整单显示
    public const int CM_MZKGL_KCKYETZD_CX = 5165205;                      //面值卡库存卡余额调整单查询
    public const int CM_MZKGL_YXQGG = 5165216;                            //面值卡有效期更改
    public const int CM_MZKGL_YXQGG_LR = 5165217;                         //面值卡有效期更改录入
    public const int CM_MZKGL_YXQGG_SH = 5165218;                         //面值卡有效期更改审核
    public const int CM_MZKGL_YXQGG_XS = 5165219;                         //面值卡有效期更改显示
    public const int CM_MZKGL_YXQGG_CX = 5165220;                         //面值卡有效期更改记录查询
    public const int CM_HYKGL_MZKHK_CX = 5165221;                         //面值卡换卡查询

    public const int CM_MZKGL_MZKASKFSCXSKXX = 5165222;                   //面值卡按收款方式查询收款信息
    public const int CM_MZKGL_MZKJEPLZC = 5165223;                        //面值卡金额批量转储
    public const int CM_MZKGL_MZKJEPLZC_LR = 5165224;                     //面值卡金额批量转储录入
    public const int CM_MZKGL_MZKJEPLZC_SH = 5165225;                     //面值卡金额批量转储审核
    public const int CM_MZKGL_MZKJEPLZC_CX = 5165226;                     //面值卡金额批量转储记录查询
    public const int CM_MZKGL_MZKTL = 5165227;                            //面值卡退领处理
    public const int CM_MZKGL_MZKTL_LR = 5165228;                         //面值卡退领录入
    public const int CM_MZKGL_MZKTL_SH = 5165229;                         //面值卡退领审核
    public const int CM_MZKGL_MZKTL_CX = 5165230;                         //面值卡退领查询
    public const int CM_MZKGL_MZKSKQKCX = 5165231;                        //面值卡售卡欠款查询
    public const int CM_MZKGL_MZKJEDY = 5165232;                          //面值金额定义

    public const int CM_MZKGL_AKHTJSKHZQK = 5165233;                      // 按客户统计售卡汇总情况
    public const int CM_MZKGL_AMDTJMZKFS = 5165234;                       // 按门店统计面值卡发售
    public const int CM_MZKGL_MZKSKZQMXCX = 5165235;                      //面值卡售卡赠券明细查询
    public const int CM_MZKGL_MZKSKZFMXCX = 5165236;                      //面值卡售卡赠分明细查询
    public const int CM_MZKGL_ASHTJMZKXF = 5165237;                       //面值卡售卡赠分明细查询
    public const int CM_MZKGL_MZKSKKHMXCX = 5165238;                      //面值卡售卡卡号明细查询
    public const int CM_MZKGL_SKGLYDY = 5165239;                          //面值卡售卡管理员定义
    public const int CM_MZKGL_SKTJKCSR = 5165240;
    public const int CM_MZKGL_PDHZCX = 5165241;                           //面值卡盘点汇总查询
    public const int CM_MZKGL_MZKJKDZ = 5165242;                          //面值卡缴款对账
    public const int CM_MZKGL_MZKMRCZJEXZ = 5165243;                      //面值卡每日充值金额限制
    public const int CM_MZKGL_MZKMRCZJEXZ_LR = 5165244;                   //面值卡每日充值金额限制(录入)
    public const int CM_MZKGL_MZKMRCZJEXZ_CX = 5165245;                   //面值卡每日充值金额限制(查询)
    public const int CM_MZKDKHGL_QYDKHZLDR = 5165246;                     //企业大客户资料excel导入
    public const int CM_MZKDKHGL_SKDXGKHZL = 5165247;                     //售卡单修改客户资料
    public const int CM_MZKDKHGL_SKDXGKHZL_LR = 5165248;                  //售卡单修改客户资料(录入)
    public const int CM_MZKGL_ZFFS = 5165249;
    public const int CM_MZKGL_MZKPDQCL = 5165250;                         //面值卡盘点前处理
    public const int CM_MZKGL_MZKPDLR = 5165251;                          //面值卡盘点录入
    public const int CM_MZKGL_JKDZ = 5165252;                             //面值卡交款对账
    public const int CM_MZKGL_FS0 = 5165254;                              //0面值卡售卡单（新）
    public const int CM_MZKGL_FS0_LR = 5165255;                           //0面值卡售卡单（新）录入
    public const int CM_MZKGL_FS0_SH = 5165256;                           //0面值卡售卡单（新）审核
    public const int CM_MZKGL_FS0_CX = 5165257;                           //0面值卡售卡单（新）查询

    //现有菜单重复至61
    public const int CM_MZKGL_MZKLYSQ = 5165261;                          //面值卡领用申请                     
    public const int CM_MZKGL_MZKLYSQ_CX = 5165263;                       //面值卡领用申请单查询
    public const int CM_MZKGL_MZKLYSQ_LR = 5165262;                       //面值卡领用申请单录入
    public const int CM_MZKGL_MZKLYSQ_SH = 5165264;                       //面值卡领用申请单审核

    public const int CM_MZKGL_FSZXGZDY = 5165265;                          //面值卡发售折现规则定义   
    public const int CM_MZKGL_MZKKFP = 5165266;                          //面值卡开发票  

    //public const int CM_MZKGL_MZKPLQK = 5165261;                          //面值卡领用申请                     
    //public const int CM_MZKGL_MZKPLQK_CX = 5165263;                       //面值卡领用申请单查询
    //public const int CM_MZKGL_MZKPLQK_LR = 5165262;                       //面值卡领用申请单录入
    //public const int CM_MZKGL_MZKPLQK_SH = 5165264;                       //面值卡领用申请单审核
    public const int CM_MZKGL_MZKCQK = 5165267;                             //面值卡存取款
    public const int CM_MZKGL_MZKCQK_LR = 5165268;                           //面值卡存取款录入
    public const int CM_MZKGL_MZKCQK_SH = 5165269;                           //面值卡存取款审核
    public const int CM_MZKGL_MZKCQK_CX = 5165270;                           //面值卡存取款查询
    public const int CM_MZKGL_ASKFSTJMZKCQK = 5165271;                     //按收款方式统计，0面值卡存款情况


    #endregion

    #region CM_HYXF
    public const int CM_HYXF_JFTZ = 5166001;                              //会员卡积分调整
    public const int CM_HYXF_JFTZ_LR = 5166002;                           //会员卡积分调整录入
    public const int CM_HYXF_JFTZ_SH = 5166003;                           //会员卡积分调整审核
    public const int CM_HYXF_JFTZ_XS = 5166004;                           //会员卡积分调整显示
    public const int CM_HYXF_JFTZ_CX = 5166005;                           //会员卡积分调整查询
    public const int CM_HYXF_JFBD = 5166006;                              //会员卡积分变动单
    public const int CM_HYXF_JFBD_LR = 5166007;                           //会员卡积分变动单录入
    public const int CM_HYXF_JFBD_SH = 5166008;                           //会员卡积分变动单审核
    public const int CM_HYXF_JFBD_XS = 5166009;                           //会员卡积分变动单显示
    public const int CM_HYXF_JFBD_CX = 5166010;                           //会员卡积分变动单查询
    public const int CM_HYXF_JFZC = 5166011;                              //会员卡积分转储
    public const int CM_HYXF_JFZC_LR = 5166012;                           //会员卡积分转储录入
    public const int CM_HYXF_JFZC_SH = 5166013;                           //会员卡积分转储审核
    public const int CM_HYXF_JFZC_CX = 5166014;                           //会员卡积分转储查询
    public const int CM_HYXF_HYKYQMJFGZ = 5166015;                        //用钱买积分规则定义
    public const int CM_HYXF_CRMYQMJF = 5166016;                          //CRM退货用钱买积分
    public const int CM_HYXF_CRMYQMJF_LR = 5166017;                       //CRM退货用钱买积分录入
    public const int CM_HYXF_CRMYQMJF_SH = 5166018;                       //CRM退货用钱买积分审核
    public const int CM_HYXF_CRMYQMJF_CX = 5166019;                       //CRM退货用钱买积分查询
    public const int CM_HYXF_HYZDY = 5166020;                             //静态客群组定义
    public const int CM_HYXF_HYZDY_LR = 5166021;                          //客群组定义录入
    public const int CM_HYXF_HYZDY_SH = 5166022;                          //客群组定义审核
    public const int CM_HYXF_HYZDY_XS = 5166023;                          //客群组定义显示
    public const int CM_HYXF_HYZDY_CX = 5166024;                          //客群组定义查询
    public const int CM_HYXF_HYZLXDY = 5166025;                           //客群组类型定义
    public const int CM_HYXF_JFGZ = 5166026;                              //会员卡积分规则设定
    public const int CM_HYXF_JFGZ_LR = 5166027;                           //会员卡积分规则设定录入
    public const int CM_HYXF_JFGZ_SH = 5166028;                           //会员卡积分规则设定审核
    public const int CM_HYXF_JFGZ_QD = 5166029;                           //会员卡积分规则设定启动
    public const int CM_HYXF_JFGZ_ZZ = 5166030;                           //会员卡积分规则设定终止
    public const int CM_HYXF_SrchCurrJFGZ = 5166031;                      //会员卡积分规则定义单查询
    public const int CM_HYXF_HYZLXZS = 5166032;                           //客群组类型展示
    public const int CM_HYXF_JFCLGZDY = 5166033;                          //会员卡积分处理规则定义
    public const int CM_HYXF_JFCLGZDY_LR = 5166034;                       //会员卡积分处理规则定义录入
    public const int CM_HYXF_JFCLGZDY_XS = 5166035;                       //会员卡积分处理规则定义显示
    public const int CM_HYXF_JFCLGZDY_XG = 5166036;                       //会员卡积分处理规则定义修改
    public const int CM_HYXF_HYKJFHBZX = 5166037;                         //会员卡积分回报执行
    public const int CM_HYXF_HYKJFHBZX_LR = 5166038;                      //会员卡积分回报执行录入
    public const int CM_HYXF_HYKJFHBZX_DY = 5166039;                      //会员卡积分回报执行打印
    public const int CM_HYXF_HYKJFHBZX_CZ = 5166040;                      //会员卡积分回报执行冲正
    public const int CM_HYXF_HYKJFBDJCLCX = 5166041;                      //会员卡积分变动及处理查询
    public const int CM_HYXF_HYXFMXCX = 5166042;                          //会员消费记录查询
    public const int CM_HYXF_HYXFMXQJHZ = 5166043;                        //会员消费明细期间汇总
    public const int CM_HYXF_HYXFMXYBLB = 5166044;                        //会员消费明细一般列表

    public const int CM_HYXF_BMXF = 5166045;                              //按部门汇总消费情况
    public const int CM_HYXF_BMKLXXF = 5166046;                           //按部门卡类型汇总消费情况
    public const int CM_HYXF_BMSBXF = 5166047;                            //按部门品牌汇总消费情况
    public const int CM_HYXF_HYTDJFDYD = 5166048;                         //会员特定积分定义单
    public const int CM_HYXF_HYTDJFDYD_LR = 5166049;                      //会员特定积分定义单录入
    public const int CM_HYXF_HYTDJFDYD_SH = 5166050;                      //会员特定积分定义单审核
    public const int CM_HYXF_HYTDJFDYD_QD = 5166051;                      //会员特定积分定义单启动
    public const int CM_HYXF_HYTDJFDYD_ZZ = 5166052;                      //会员特定积分定义单终止
    public const int CM_HYXF_HYTDZKDYD = 5166053;                         //会员特定折扣定义单
    public const int CM_HYXF_HYTDZKDYD_LR = 5166054;                      //会员特定折扣定义单录入
    public const int CM_HYXF_HYTDZKDYD_SH = 5166055;                      //会员特定折扣定义单审核
    public const int CM_HYXF_HYTDZKDYD_QD = 5166056;                      //会员特定折扣定义单启动
    public const int CM_HYXF_HYTDZKDYD_ZZ = 5166057;                      //会员特定折扣定义单终止
    public const int CM_HYXF_FLFHSJTJ = 5166058;                          //会员卡积分返券统计
    public const int CM_HYXF_XFZKL = 5166059;                             //会员卡折扣率设定
    public const int CM_HYXF_XFZKL_LR = 5166060;                          //会员卡折扣率设定录入
    public const int CM_HYXF_XFZKL_SH = 5166061;                          //会员卡折扣率设定审核
    public const int CM_HYXF_XFZKL_QD = 5166062;                          //会员卡折扣率设定启动
    public const int CM_HYXF_XFZKL_ZZ = 5166063;                          //会员卡折扣率设定终止
    public const int CM_HYXF_SrchCurrZKLGZ = 5166064;                     //会员卡折扣规则定义单查询
    public const int CM_HYXF_THQMFBB = 5166065;                           //退货买积分报表查询
    public const int CM_HYXF_HYKJFHBZXWN = 5166066;                       //会员积分处理回报(往年未处理积分)
    public const int CM_HYXF_HYKJFHBZXWN_LR = 5166067;                    //会员积分处理回报(往年未处理积分)录入
    public const int CM_HYXF_HYKJFHBZXWN_DY = 5166068;                    //会员积分处理回报(往年未处理积分)打印
    public const int CM_HYXF_HYKJFHBZXWN_CZ = 5166069;                    //会员积分处理回报(往年未处理积分)冲正
    public const int CM_HYXF_HYZDY_DT = 5166070;                          //动态客群组定义
    public const int CM_HYXF_HYZDY_DT_LR = 5166071;                       //动态客群组定义
    public const int CM_HYXF_HYZDY_DT_SH = 5166072;                       //动态客群组定义
    public const int CM_HYXF_HYZDY_DT_CX = 5166073;                       //动态客群组定义
    public const int CM_HYXF_QZLXDEF = 5166075;                           //圈子类型定义
    public const int CM_HYXF_QZLXDEF_LR = 5166076;                        //圈子类型定义录入
    public const int CM_HYXF_QZLXDEF_CX = 5166077;                        //圈子类型定义查询  
    public const int CM_HYXF_QZCXHDDEF = 5166078;                         //圈子促销活动定义
    public const int CM_HYXF_QZXXDY = 5166079;                            //圈子信息定义
    public const int CM_HYXF_QZXXDY_LR = 5166080;                         //圈子信息 录入
    public const int CM_HYXF_QZXXDY_SH = 5166081;                         //圈子信息 审核
    public const int CM_HYXF_JFHZCX = 5166082;                            //积分汇总查询
    public const int CM_HYXF_HYXFMXQJHZ_XS = 5166093;                     //会员消费期间汇总信息公开显示
    public const int CM_HYXF_HYXFMXQJHZ_CX = 5166094;                     //会员消费期间汇总信息查询
    public const int CM_HYXF_HYXFMXYBLB_XS = 5166095;                     //会员消费明细一般列表公开显示
    public const int CM_HYXF_HYJSPXSCX = 5166096;                     //会员价商品销售查询
    public const int CM_HYXF_HYZXSPXSCX = 5166097;                     //会员专享商品销售查询；
    public const int CM_HYXF_GYSRQDNMRJF = 5166098;                     //按日期段查询供应商日期段内的每日积分
    public const int CM_HYXF_JFGZ_CJCX = 5166099;                         //积分规则修改参加促销权限
    public const int CM_HYXF_BJF = 5166100;
    public const int CM_HYXF_SHJFGZ = 5166101;
    public const int CM_HYXF_HYXFMXQJHZDBQ = 5166102;               

    #endregion
    #region CM_LPGL
    public const int CM_LPGL_JFFHLPDY = 5168501;                          //礼品定义
    public const int CM_LPGL_JFFHLPDY_DEL = 5168502;                      //礼品定义删除
    public const int CM_LPGL_JFFHLPDY_XG = 5168503;                       //礼品定义修改
    public const int CM_LPGL_JFFHLPJHD = 5168504;                         //礼品进货单
    public const int CM_LPGL_JFFHLPJHD_LR = 5168505;                      //礼品进货单录入
    public const int CM_LPGL_JFFHLPJHD_SH = 5168506;                      //礼品进货单审核
    public const int CM_LPGL_JFFHLPJHD_XS = 5168507;                      //礼品进货单显示
    public const int CM_LPGL_JFFHLPJHD_CX = 5168508;                      //礼品进货单记录查询
    public const int CM_LPGL_JFFHLPLQD = 5168509;                         //礼品领取单
    public const int CM_LPGL_JFFHLPLQD_LR = 5168510;                      //礼品领取单录入
    public const int CM_LPGL_JFFHLPLQD_SH = 5168511;                      //礼品领取单审核
    public const int CM_LPGL_JFFHLPLQD_XS = 5168512;                      //礼品领取单显示
    public const int CM_LPGL_JFFHLPLQD_CX = 5168513;                      //礼品领取单记录查询
    public const int CM_LPGL_LPFFGZDEF = 5168514;                         //礼品发放规则定义
    public const int CM_LPGL_LPFF_SR = 5168515;                           //生日礼品兑换
    public const int CM_LPGL_LPFF_SS = 5168516;                           //首刷礼品兑换
    public const int CM_LPGL_LPFF_BK = 5168517;                           //办卡礼品兑换
    public const int CM_LPGL_LPFF_JFFL = 5168518;                         //积分返礼兑换
    public const int CM_LPGL_LPFF_LD = 5168519;                           //来店礼品兑换
    public const int CM_LPGL_JFFHLPFF_CX = 5168520;                       //积分返还礼品发放查询
    public const int CM_LPGL_GHSDY = 5168521;                             //礼品供货商定义
    public const int CM_LPGL_LPJHDWDY = 5168522;                          //礼品进货单位定义
    public const int CM_LPGL_JFFHLPKC_CX = 5168523;                       //礼品库存查询
    public const int CM_LPGL_JFFHLPMX_CX = 5168524;                       //礼品进销存查询
    public const int CM_LPGL_LPSXDY = 5168525;                            //礼品属性定义
    public const int CM_LPGL_LPFLDY = 5168526;                            //礼品分类定义
    public const int CM_LPGL_JFFHLPTHD = 5168527;                         //礼品退货单
    public const int CM_LPGL_JFFHLPTHD_SH = 5168528;                      //礼品退货单审核
    public const int CM_LPGL_JFFHLPTHD_XS = 5168529;                      //礼品退货单显示
    public const int CM_LPGL_JFFHLPTHD_CX = 5168530;                      //礼品进货单记录查询
    public const int CM_LPGL_LPFFHZ = 5168531;                            //礼品发放汇总查询
    public const int CM_LPGL_LPKCBDJL = 5168532;                          //礼品库存变动记录
    public const int CM_LPGL_LPBFCL = 5168533;                            //礼品报废处理
    public const int CM_LPGL_LPBFCL_LR = 5168534;                         //礼品报废处理录入
    public const int CM_LPGL_LPBFCL_SH = 5168535;                         //礼品报废处理审核
    public const int CM_LPGL_LPBFCL_CX = 5168536;                         //礼品报废处理查询
    public const int CM_LPGL_JSZLGZ = 5168537;                            //满额赠礼规则
    public const int CM_LPGL_LPPDCL = 5168538;                            //礼品盘点处理
    public const int CM_LPGL_LPPDCL_LR = 5168539;                         //礼品盘点处理录入
    public const int CM_LPGL_LPPDCL_SH = 5168540;                         //礼品盘点处理审核
    public const int CM_LPGL_LPPDCL_CX = 5168541;                         //礼品盘点处理查询
    public const int CM_LPGL_RCLPFF = 5168542;                            //日常礼品发放(当前积分)
    public const int CM_LPGL_RCLPFF_LR = 5168544;                         //日常礼品发放(当前积分)录入
    public const int CM_LPGL_RCLPFF_CZ = 5168545;                         //日常礼品发放(当前积分)冲正
    public const int CM_LPGL_RCLPFFWN = 5168543;                          //日常礼品发放(往年积分)
    public const int CM_LPGL_RCLPFFWN_LR = 5168546;                       //日常礼品发放(往年积分)录入
    public const int CM_LPGL_RCLPFFWN_CZ = 5168547;                       //日常礼品发放(往年积分)冲正
    public const int CM_LPGL_LPFFHDDY = 5168548;                          //礼品发放活动定义
    public const int CM_LPGL_LPFFHDDY_CX = 5168549;                       //礼品发放活动定义（查询）
    public const int CM_LPGL_LPFFHDDY_LR = 5168550;                       //礼品发放活动定义（录入）
    public const int CM_LPGL_HDLPFF = 5168551;                            //礼品发放活动定义
    public const int CM_LPGL_HDLPFF_CX = 5168552;                         //礼品发放活动定义
    public const int CM_LPGL_HDLPFF_LR = 5168553;                         //礼品发放活动定义
    public const int CM_LPGL_HDLPFF_SH = 5168554;                         //礼品发放活动定义
    public const int CM_LPGL_JFFHLPTHD_LR = 5168554;                      //礼品退货单录入
    public const int CM_CRMART_CXLPXX = 5168555;
    public const int CM_LPGL_JFFHLPFFCX = 5168556;        //积分返回礼品发放查询

    #endregion
    #region CM_YHQGL
    public const int CM_YHQGL_CXHDZT = 5167001;                           //促销活动主题定义
    public const int CM_YHQGL_DEFJFDXBL = 5167002;                        //定义卡类型积分抵现比例
    public const int CM_YHQGL_YHQDEFFFGZ = 5167003;                       //定义优惠券发放规则
    public const int CM_YHQGL_YHQDEFSYGZ = 5167004;                       //定义优惠券使用规则
    public const int CM_YHQGL_LDZQHDDY = 5167005;                         //来店赠券活动定义
    public const int CM_YHQGL_LDZQGZ = 5167006;                           //来店赠券规则
    public const int CM_YHQGL_LDZQ = 5167007;                             //来店赠券
    public const int CM_YHQGL_HZCXJLByHT = 5167008;                       //按合同汇总查询收发券记录
    public const int CM_YHQGL_HZCXJLByXSBM = 5167009;                     //按销售部门汇总查询收发券记录
    public const int CM_YHQGL_HZCXJLBySP = 5167010;                       //按商品汇总查询收发券记录
    public const int CM_YHQGL_HZCXJLBySPFL = 5167011;                     //按商品分类汇总查询收发券记录
    public const int CM_YHQGL_HZCXJLBYCGBM = 5167012;                     //按采购部门汇总收发券记录
    public const int CM_YHQGL_SrchCXBySP = 5167013;                       //按商品查询促销信息
    public const int CM_YHQGL_CXMBJZDYD = 5167014;                        //促销满百减折定义单
    public const int CM_YHQGL_CXMBJZDYD_LR = 5167015;                     //促销满百减折定义单录入
    public const int CM_YHQGL_CXMBJZDYD_SH = 5167016;                     //促销满百减折定义单审核
    public const int CM_YHQGL_CXMBJZDYD_QD = 5167017;                     //促销满百减折定义单启动
    public const int CM_YHQGL_CXMBJZDYD_ZZ = 5167018;                     //促销满百减折定义单终止
    public const int CM_YHQGL_CXMBJZDYD_XS = 5167019;                     //促销满百减折定义单显示
    public const int CM_YHQGL_CXMBJZDYD_CX = 5167020;                     //促销满百减折定义单查询
    public const int CM_YHQGL_MBJZGZ = 5167021;                           //定义满百减折规则
    public const int CM_YHQGL_JFBSGZ = 5167022;                           //积分翻倍规则
    public const int CM_YHQGL_SJFGZ = 5167023;                            //消费送积分规则
    public const int CM_YHQGL_YHQDEFFFGZ_CJ = 5167024;                    //定义促销抽奖发放规则
    public const int CM_YHQGL_YHQDEFFFGZ_LP = 5167025;                    //定义促销礼品发放规则
    public const int CM_YHQGL_YHQSYDYD = 5167026;                         //会员卡优惠券使用定义单
    public const int CM_YHQGL_YHQSYDYD_LR = 5167027;                      //会员卡优惠券使用定义单录入
    public const int CM_YHQGL_YHQSYDYD_SH = 5167028;                      //会员卡优惠券使用定义单审核
    public const int CM_YHQGL_YHQSYDYD_QD = 5167029;                      //会员卡优惠券使用定义单启动
    public const int CM_YHQGL_YHQSYDYD_ZZ = 5167030;                      //会员卡优惠券使用定义单终止
    public const int CM_YHQGL_YHQSYDYD_XS = 5167031;                      //会员卡优惠券使用定义单显示
    public const int CM_YHQGL_YHQSYDYD_CX = 5167032;                      //会员卡优惠券使用定义单查询
    public const int CM_YHQGL_SHOWCURRSYD = 5167033;                      //查询当前执行的优惠券使用单
    public const int CM_YHQGL_YHQFFDYD = 5167034;                         //会员卡优惠券发放定义单
    public const int CM_YHQGL_YHQFFDYD_LR = 5167035;                      //会员卡优惠券发放定义单录入
    public const int CM_YHQGL_YHQFFDYD_SH = 5167036;                      //会员卡优惠券发放定义单审核
    public const int CM_YHQGL_YHQFFDYD_QD = 5167037;                      //会员卡优惠券发放定义单启动
    public const int CM_YHQGL_YHQFFDYD_ZZ = 5167038;                      //会员卡优惠券发放定义单终止
    public const int CM_YHQGL_YHQFFDYD_XS = 5167039;                      //会员卡优惠券发放定义单显示
    public const int CM_YHQGL_YHQFFDYD_CX = 5167040;                      //会员卡优惠券发放定义单查询
    public const int CM_YHQGL_SHOWCURRFFD = 5167041;                      //查询当前执行的优惠券发放单
    public const int CM_YHQGL_CXMDDYD = 5167042;                          //促销满抵定义单
    public const int CM_YHQGL_CXMDDYD_LR = 5167043;                       //促销满抵定义单录入
    public const int CM_YHQGL_CXMDDYD_SH = 5167044;                       //促销满抵定义单审核
    public const int CM_YHQGL_CXMDDYD_QD = 5167045;                       //促销满抵定义单启动
    public const int CM_YHQGL_CXMDDYD_ZZ = 5167046;                       //促销满抵定义单终止
    public const int CM_YHQGL_CXMDDYD_XS = 5167047;                       //促销满抵定义单显示
    public const int CM_YHQGL_CXMDDYD_CX = 5167048;                       //促销满抵定义单查询
    public const int CM_YHQGL_YHQYCZZ = 5167049;                          //优惠券预存增值规则
    public const int CM_YHQGL_QTJFDHJL = 5167050;                         //前台积分抵现记录查询
    public const int CM_YHQGL_MZDEF = 5167051;                            //定义优惠券面值
    public const int CM_YHQGL_YHQFFDYD_LP = 5167052;                      //促销礼品发放定义单
    public const int CM_YHQGL_YHQFFDYD_LP_LR = 5167053;                   //促销礼品发放定义单录入
    public const int CM_YHQGL_YHQFFDYD_LP_SH = 5167054;                   //促销礼品发放定义单审核
    public const int CM_YHQGL_YHQFFDYD_LP_QD = 5167055;                   //促销礼品发放定义单启动
    public const int CM_YHQGL_YHQFFDYD_LP_ZZ = 5167056;                   //促销礼品发放定义单终止
    public const int CM_YHQGL_YHQFFDYD_LP_XS = 5167057;                   //促销礼品发放定义单显示
    public const int CM_YHQGL_YHQFFDYD_LP_CX = 5167058;                   //促销礼品发放定义单查询
    public const int CM_YHQGL_DEFPTJFDXBL = 5167059;                      //定义卡类型积分抵现比例(普通)
    public const int CM_YHQGL_YHQFFDYD_CJ = 5167060;                      //促销抽奖定义单
    public const int CM_YHQGL_YHQFFDYD_CJ_LR = 5167061;                   //促销抽奖定义单录入
    public const int CM_YHQGL_YHQFFDYD_CJ_SH = 5167062;                   //促销抽奖定义单审核
    public const int CM_YHQGL_YHQFFDYD_CJ_QD = 5167063;                   //促销抽奖定义单启动
    public const int CM_YHQGL_YHQFFDYD_CJ_ZZ = 5167064;                   //促销抽奖定义单终止
    public const int CM_YHQGL_YHQFFDYD_CJ_XS = 5167065;                   //促销抽奖定义单显示
    public const int CM_YHQGL_YHQFFDYD_CJ_CX = 5167066;                   //促销抽奖定义单查询
    public const int CM_YHQGL_YHQFFDYD_JF = 5167067;                      //赠送积分定义单
    public const int CM_YHQGL_YHQFFDYD_JF_LR = 5167068;                   //赠送积分定义单录入
    public const int CM_YHQGL_YHQFFDYD_JF_SH = 5167069;                   //赠送积分定义单审核
    public const int CM_YHQGL_YHQFFDYD_JF_QD = 5167070;                   //赠送积分定义单启动
    public const int CM_YHQGL_YHQFFDYD_JF_ZZ = 5167071;                   //赠送积分定义单终止
    public const int CM_YHQGL_YHQFFDYD_JF_XS = 5167072;                   //赠送积分定义单显示
    public const int CM_YHQGL_YHQFFDYD_JF_CX = 5167073;                   //赠送积分定义单查询
    public const int CM_YHQGL_DEFCXJFDXBL = 5167074;                      //促销积分抵现比例定义
    public const int CM_YHQGL_DEFCXJFDXBL_LR = 5167075;                   //促销积分抵现比例定义录入
    public const int CM_YHQGL_DEFCXJFDXBL_SH = 5167076;                   //促销积分抵现比例定义审核
    public const int CM_YHQGL_DEFCXJFDXBL_CX = 5167077;                   //促销积分抵现比例定义查询
    public const int CM_YHQGL_DEFCXJFDXBL_QD = 5167078;                   //促销积分抵现比例定义启动
    public const int CM_YHQGL_DFQJLXXCX = 5167080;                        //按合同需要提供的功能，查询待发券记录信息
    public const int CM_YHQGL_DFQJLXXHZCX = 5167081;                      //按合同需要提供的功能，待发券记录汇总查询
    public const int CM_YHQGL_JEZJEZCJLCX = 5167082;                      //按合同需要提供的功能，金额帐记录转储记录查询
    public const int CM_YHQGL_SQJLHZXXCX = 5167083;                       //按合同需要提供的功能，收券记录汇总信息
    public const int CM_YHQGL_SrchJCJFBySP = 5167084;                     //按商品查询当前基础积分
    public const int CM_YHQGL_SrchZKBySP = 5167085;                       //按商品查询折扣信息
    public const int CM_YHQGL_SrchFQBySP = 5167086;                       //按商品查询返券信息
    public const int CM_YHQGL_SrchYQBySP = 5167087;                       //按商品查询用券信息
    public const int CM_YHQGL_YHQCXFX = 5167088;                          //优惠券查询分析
    public const int CM_YHQGL_YHQFFDYD_JF_ZFS = 5167089;                      //赠送积分定义单支付送
    public const int CM_YHQGL_YHQFFDYD_JF_ZFS_LR = 5167090;                   //赠送积分定义单录入
    public const int CM_YHQGL_YHQFFDYD_JF_ZFS_SH = 5167091;                   //赠送积分定义单审核
    public const int CM_YHQGL_YHQFFDYD_JF_ZFS_QD = 5167092;                   //赠送积分定义单启动
    public const int CM_YHQGL_YHQFFDYD_JF_ZFS_ZZ = 5167093;                   //赠送积分定义单终止
    public const int CM_YHQGL_YHQFFDYD_JF_ZFS_XS = 5167094;                   //赠送积分定义单显示
    public const int CM_YHQGL_YHQFFDYD_JF_ZFS_CX = 5167095;                   //赠送积分定义单查询
    public const int CM_YHQGL_YHQFFDYD_JF_KK = 5167096;                      //赠送积分定义单开卡礼
    public const int CM_YHQGL_YHQFFDYD_JF_KK_LR = 5167097;                   //赠送积分定义单录入
    public const int CM_YHQGL_YHQFFDYD_JF_KK_SH = 5167098;                   //赠送积分定义单审核
    public const int CM_YHQGL_YHQFFDYD_JF_KK_QD = 5167099;                   //赠送积分定义单启动
    public const int CM_YHQGL_YHQFFDYD_JF_KK_ZZ = 5167100;                   //赠送积分定义单终止
    public const int CM_YHQGL_YHQFFDYD_JF_KK_XS = 5167101;                   //赠送积分定义单显示
    public const int CM_YHQGL_YHQFFDYD_JF_KK_CX = 5167102;                   //赠送积分定义单查询
    #endregion
    #region CM_GTPT
    public const int CM_GTPT_WXGROUP = 5167501;                           //微信分组
    public const int CM_GTPT_WXUSER_GROUP = 5167502;                      //微信用户分组单据
    public const int CM_GTPT_WXGRXX = 5167503;                            //微信用户信息
    public const int CM_GTPT_WXRESOURCE = 5167504;                        //微信资源
    public const int CM_GTPT_WXMESSAGE = 5167505;                         //微信群发消息
    public const int CM_GTPT_WXAUTOREPLY = 5167506;                       //微信自动回复
    public const int CM_GTPT_WXCUSTOMERMESSAGE = 5167507;                 //微信客服消息
    public const int CM_GTPT_WXCDDY = 5167508;                            //微信菜单定义
    public const int CM_GTPT_WXCDNRDY = 5167509;                          //微信菜单内容定义
    public const int CM_GTPT_WXWTDY = 5167510;                            //微信问题定义
    public const int CM_GTPT_WXHF_GZ = 5167511;                           //微信关注回复
    public const int CM_GTPT_WXHF_MR = 5167512;                           //微信默认回复
    public const int CM_GTPT_WXHF_GJC = 5167513;                          //微信关键词回复
    public const int CM_GTPT_WXHF_TS = 5167514;                          //微信推送回复

    public const int CM_GTPT_WXGJCJL = 5167515;                           //微信关键词触发记录
    public const int CM_GTPT_WXZXHD = 5167516;                            //微信最新活动定义
    public const int CM_GTPT_WXMDDY = 5167517;                            //微信门店定义
    public const int CM_GTPT_WXLCDY = 5167518;                            //微信楼层定义
    public const int CM_GTPT_WXPPFL = 5167519;                            //微信品牌分类定义
    public const int CM_GTPT_WXPPSB = 5167520;                            //微信品牌商标定义
    public const int CM_GTPT_WXLCSB = 5167521;                            //微信楼层商标定义
    public const int CM_GTPT_WXSPTJ = 5167522;                            //微信商品推荐定义
    public const int CM_GTPT_WXHYQY = 5167523;                            //微信会员权益定义
    public const int CM_GTPT_WXSHLM = 5167524;                            //微信商户联盟定义
    public const int CM_GTPT_WXJTYM = 5167525;                            //微信静态页面定义
    public const int CM_GTPT_WXJB = 5167526;                              //微信解绑处理
    public const int CM_GTPT_WXJB_LR = 5167527;                           //微信解绑处理录入
    public const int CM_GTPT_WXJB_SH = 5167528;                           //微信解绑处理审核
    public const int CM_GTPT_WXJB_CX = 5167529;                           //微信解绑处理查询
    public const int CM_GTPT_WXCXHD = 5167530;                            //微信促销活动定义
    public const int CM_GTPT_WXSQGZ = 5167531;                            //微信送券规则定义
    public const int CM_GTPT_WXSQDYD = 5167532;                           //微信送券规则定义单
    public const int CM_GTPT_WXSQDYD_LR = 5167533;                         //微信送券规则定义单录入
    public const int CM_GTPT_WXSQDYD_SH = 5167534;                         //微信送券规则定义单审核
    public const int CM_GTPT_WXSQDYD_QD = 5167535;                         //微信送券规则定义单启动
    public const int CM_GTPT_WXSQDYD_ZZ = 5167536;                         //微信送券规则定义单终止
    public const int CM_GTPT_WXSQDYD_CX = 5167537;                         //微信送券规则定义单查询
    public const int CM_GTPT_WXQDGZ = 5167538;                            //微信签到赠积分规则定义
    public const int CM_GTPT_WXQDGZ_LR = 5167539;                         //微信签到赠积分规则定义录入
    public const int CM_GTPT_WXQDGZ_SH = 5167540;                         //微信签到赠积分规则定义审核
    public const int CM_GTPT_WXQDGZ_QD = 5167541;                         //微信签到赠积分规则定义启动
    public const int CM_GTPT_WXQDGZ_ZZ = 5167542;                         //微信签到赠积分规则定义终止
    public const int CM_GTPT_WXQDGZ_CX = 5167543;                         //微信签到赠积分规则定义查询
    public const int CM_GTPT_WXLBDY = 5167544;                            //微信礼包定义
    public const int CM_GTPT_WXLBFFGZ = 5167545;                          //微信礼包发放规则定义
    public const int CM_GTPT_WXCJDYD = 5167546;                           //微信抽奖定义单
    public const int CM_GTPT_WXCJDYD_LR = 5167547;                        //微信抽奖(大转盘)定义单录入
    public const int CM_GTPT_WXCJDYD_SH = 5167548;                        //微信抽奖(大转盘)定义单审核
    public const int CM_GTPT_WXCJDYD_QD = 5167549;                        //微信抽奖(大转盘)定义单启动
    public const int CM_GTPT_WXCJDYD_ZZ = 5167550;                        //微信抽奖(大转盘)定义单终止
    public const int CM_GTPT_WXCJDYD_CX = 5167551;                        //微信抽奖(大转盘)定义单查询
    public const int CM_GTPT_WXKKSFGZ = 5167552;                          //微信开卡赠积分规则定义
    public const int CM_GTPT_WXKKSFGZ_LR = 5167553;                       //微信开卡赠积分规则定义录入
    public const int CM_GTPT_WXKKSFGZ_SH = 5167554;                       //微信开卡赠积分规则定义审核
    public const int CM_GTPT_WXKKSFGZ_QD = 5167555;                       //微信开卡赠积分规则定义启动
    public const int CM_GTPT_WXKKSFGZ_ZZ = 5167556;                       //微信开卡赠积分规则定义终止
    public const int CM_GTPT_WXKKSFGZ_CX = 5167557;                       //微信开卡赠积分规则定义查询
    public const int CM_GTPT_WXBKSFGZ = 5167558;                          //微信绑卡赠积分规则定义
    public const int CM_GTPT_WXBKSFGZ_LR = 5167559;                       //微信绑卡赠积分规则定义录入
    public const int CM_GTPT_WXBKSFGZ_SH = 5167560;                       //微信绑卡赠积分规则定义审核
    public const int CM_GTPT_WXBKSFGZ_QD = 5167561;                       //微信绑卡赠积分规则定义启动
    public const int CM_GTPT_WXBKSFGZ_ZZ = 5167562;                       //微信绑卡赠积分规则定义终止
    public const int CM_GTPT_WXBKSFGZ_CX = 5167563;                       //微信绑卡赠积分规则定义查询
    public const int CM_GTPT_WDCNRDY = 5167564;                           //微调查内容定义
    public const int CM_GTPT_WDCDY = 5167565;                             //微调查定义
    public const int CM_GTPT_WDCDY_LR = 5167566;                          //微调查定义录入
    public const int CM_GTPT_WDCDY_SH = 5167567;                          //微调查定义审核
    public const int CM_GTPT_WDCDY_QD = 5167568;                          //微调查定义启动
    public const int CM_GTPT_WDCDY_ZZ = 5167569;                          //微调查定义终止
    public const int CM_GTPT_WDCDY_CX = 5167570;                          //微调查定义查询
    public const int CM_GTPT_WDCTJCX = 5167571;                           //微调查结果统计
    public const int CM_GTPT_WDCMXCX = 5167572;                           //微调查明细查询
    public const int CM_GTPT_TSLXDY = 5167573;                            //投诉类型定义
    public const int CM_GTPT_TSCL = 5167574;                              //投诉处理
    public const int CM_GTPT_TSCL_CL = 5167575;                           //投诉处理处理
    public const int CM_GTPT_TSCL_HF = 5167576;                           //投诉处理回访
    public const int CM_GTPT_TSCL_CX = 5167577;                           //投诉处理查询
    public const int CM_GTPT_WXQHBDYD = 5167578;                          //微信抢红包定义单
    public const int CM_GTPT_WXQHBDYD_LR = 5167579;                       //微信抢红包定义单录入
    public const int CM_GTPT_WXQHBDYD_SH = 5167580;                       //微信抢红包定义单审核
    public const int CM_GTPT_WXQHBDYD_QD = 5167581;                       //微信抢红包定义单启动
    public const int CM_GTPT_WXQHBDYD_ZZ = 5167582;                       //微信抢红包定义单终止
    public const int CM_GTPT_WXQHBDYD_CX = 5167583;                       //微信抢红包定义单查询
    //public const int CM_GTPT_WXDZPDYD = 5167584;                          //微信大转盘定义单
    //public const int CM_GTPT_WXDZPDYD_LR = 5167585;                       //微信大转盘定义单录入
    //public const int CM_GTPT_WXDZPDYD_SH = 5167586;                       //微信大转盘定义单审核
    //public const int CM_GTPT_WXDZPDYD_QD = 5167587;                       //微信大转盘定义单启动
    //public const int CM_GTPT_WXDZPDYD_ZZ = 5167588;                       //微信大转盘定义单终止
    //public const int CM_GTPT_WXDZPDYD_CX = 5167589;                       //微信大转盘定义单查询
    public const int CM_GTPT_WXGGKDYD = 5167590;                          //微信刮刮卡定义单
    public const int CM_GTPT_WXGGKDYD_LR = 5167591;                       //微信刮刮卡定义单录入
    public const int CM_GTPT_WXGGKDYD_SH = 5167592;                       //微信刮刮卡定义单审核
    public const int CM_GTPT_WXGGKDYD_QD = 5167593;                       //微信刮刮卡定义单启动
    public const int CM_GTPT_WXGGKDYD_ZZ = 5167594;                       //微信刮刮卡定义单终止
    public const int CM_GTPT_WXGGKDYD_CX = 5167595;                       //微信刮刮卡定义单查询
    public const int CM_GTPT_WXBDHYTJ = 5167596;                          //微信绑定会员统计
    public const int CM_GTPT_WXGZGNFX = 5167597;                          //微信关注功能分析
    public const int CM_GTPT_AKLXFXWXBDHY = 5167598;                      //按卡类型分析微信绑定会员
    public const int CM_GTPT_WXYHGZFX = 5167599;                          //微信用户关注分析
    public const int CM_GTPT_WXGJCCFJLCX = 5167600;                       //微信关键词触发记录查询
    public const int CM_GTPT_WXYHCX = 5167601;                            //微信用户查询
    public const int CM_GTPT_WXJPFFJLCX = 5167602;                        //微信奖品发放记录查询
    public const int CM_GTPT_WDCZLJLCX = 5167603;                         //微调查赠礼记录查询
    public const int CM_GTPT_WDCYHCX = 5167604;                           //微调查用户查询
    public const int CM_GTPT_WXYYFWDY = 5167605;                          //微信预约服务定义
    public const int CM_GTPT_WXZXHDDY = 5167606;                          //微信最新活动定义
    public const int CM_GTPT_WXJPJCDY = 5167607;                          //微信奖品级次定义
    public const int CM_GTPT_WXLJD = 5167608;                             //微信领奖单
    public const int CM_GTPT_WXLJD_LR = 5167609;                          //微信领奖单录入
    public const int CM_GTPT_WXLJD_SH = 5167610;                          //微信领奖单审核
    public const int CM_GTPT_WXLJD_CX = 5167611;                          //微信领奖单查询
    public const int CM_GTPT_WXSQJLCX = 5167612;                          //微信送券记录查询
    public const int CM_GTPT_WXKKBKZLJLCX = 5167613;                      //微信开卡绑卡赠礼记录查询
    public const int CM_GTPT_WXYYFWJLCX = 5167614;                        //微信预约服务记录查询
    //public const int CM_GTPT_WXJFDHLPD = 5167615;                         //微信积分兑换礼品单
    //public const int CM_GTPT_WXJFDHLPD_LR = 5167616;                      //微信积分兑换礼品单录入
    //public const int CM_GTPT_WXJFDHLPD_SH = 5167617;                      //微信积分兑换礼品单审核
    //public const int CM_GTPT_WXJFDHLPD_QD = 5167618;                      //微信积分兑换礼品单启动
    //public const int CM_GTPT_WXJFDHLPD_ZZ = 5167619;                      //微信积分兑换礼品单终止
    //public const int CM_GTPT_WXJFDHLPD_CX = 5167620;                      //微信积分兑换礼品单查询
    public const int CM_GTPT_WXJFDHYHQ = 5167621;                         //微信积分兑换优惠券单
    public const int CM_GTPT_WXJFDHYHQ_LR = 5167622;                      //微信积分兑换优惠券单录入
    public const int CM_GTPT_WXJFDHYHQ_SH = 5167623;                      //微信积分兑换优惠券单审核
    public const int CM_GTPT_WXJFDHYHQ_QD = 5167624;                      //微信积分兑换优惠券单启动
    public const int CM_GTPT_WXJFDHYHQ_ZZ = 5167625;                      //微信积分兑换优惠券单终止
    public const int CM_GTPT_WXJFDHYHQ_CX = 5167626;                      //微信积分兑换优惠券单查询
    public const int CM_GTPT_WXJFDHLPLQD = 5167627;                         //微信积分兑换礼品领取单
    public const int CM_GTPT_WXJFDHLPLQD_LR = 5167628;                      //微信积分兑换礼品领取单录入
    public const int CM_GTPT_WXJFDHLPLQD_SH = 5167629;                      //微信积分兑换礼品领取单审核
    public const int CM_GTPT_WXJFDHLPLQD_QD = 5167630;                      //微信积分兑换礼品领取单启动
    public const int CM_GTPT_WXJFDHLPLQD_ZZ = 5167631;                      //微信积分兑换礼品领取单终止
    public const int CM_GTPT_WXJFDHLPLQD_CX = 5167632;                      //微信积分兑换礼品领取单查询
    public const int CM_GTPT_WXHYKTPSC = 5167633;                           //微信会员卡图片上传
    public const int CM_GTPT_LPTPSC = 5167634;                              //礼品图片上传
    public const int CM_GTPT_WXXHJFMXCX = 5167635;                          //微信消耗积分明细查询
    public const int CM_GTPT_WDCLPLQD = 5167636;                            //微调查礼品领奖单
    public const int CM_GTPT_WDCLPLQD_LR = 5167637;                         //微调查礼品领奖单录入
    public const int CM_GTPT_WDCLPLQD_SH = 5167638;                         //微调查礼品领奖单审核
    public const int CM_GTPT_WDCLPLQD_CX = 5167639;                         //微调查礼品领奖单查询
    public const int CM_GTPT_WXSYDHDY = 5167640;                            //微信首页导航定义
    public const int CM_GTPT_CSDEF = 5167641;                            //城市定义
    public const int CM_GTPT_WXXFLDY = 5167642;                          //微信小分类定义
    public const int CM_GTPT_WXZXHDCXDY = 5167643;                          //微信最新活动定义new
    public const int CM_GTPT_QMQGZDYD = 5167644;                          //钱买券规则定义单
    public const int CM_GTPT_QMQGZDYD_LR = 5167645;                      //钱买券规则定义单录入
    public const int CM_GTPT_QMQGZDYD_SH = 5167646;                      //钱买券规则定义单审核
    public const int CM_GTPT_QMQGZDYD_QD = 5167647;                      //钱买券规则定义单启动
    public const int CM_GTPT_QMQGZDYD_ZZ = 5167648;                      //钱买券规则定义单终止
    public const int CM_GTPT_QMQGZDYD_CX = 5167678;                      //钱买券规则定义单查询
    public const int CM_GTPT_WXYYFWJLCL = 5167650;                       //微信预约服务记录处理
    public const int CM_GTPT_WXYYFWJLCL_LR = 5167651;                    //微信预约服务记录处理录入
    public const int CM_GTPT_WXYYFWJLCL_SH = 5167652;                    //微信预约服务记录处理审核
    public const int CM_GTPT_WXYYFWJLCL_CX = 5167677;                    //微信预约服务记录处理查询

    public const int CM_GTPT_LMSHLXDY = 5167649;                          //微信小分类定义   商户联盟类型定义


    public const int CM_GTPT_WXQMQJLCX = 5167662;                          //微信钱买券记录查询

    public const int CM_GTPT_SBZKDYD = 5167663;                         //微信品牌折扣定义单
    public const int CM_GTPT_SBZKDYD_LR = 5167664;                      //微信品牌折扣定义单录入
    public const int CM_GTPT_SBZKDYD_SH = 5167665;                      //微信品牌折扣定义单审核
    public const int CM_GTPT_SBZKDYD_QD = 5167666;                      //微信品牌折扣定义单启动
    public const int CM_GTPT_SBZKDYD_ZZ = 5167667;                      //微信品牌折扣定义单终止
    public const int CM_GTPT_SBZKDYD_CX = 5167668;                      //微信品牌折扣定义单查询
    public const int CM_GTPT_GGDY = 5167669;                            //广告定义
    public const int CM_GTPT_GXHFHDY = 5167670;                         //微信个性化符号定义
    public const int CM_GTPT_WXKBKTPSC = 5167671;                         //微信开绑卡图片上传
    public const int CM_GTPT_BMQLBDEF = 5167672;                           //编码券礼包定义
    public const int CM_GTPT_WXZJHYMDCX = 5167673;                      //微信中奖会员名单查询
    public const int CM_GTPT_DQJLCX = 5167674;                          //签到记录查询

    public const int CM_GTPT_JFDHLPGZDY = 5167653;                       //微信积分兑换礼品规则定义
    public const int CM_GTPT_JFDHLPGZDY_LR = 5167654;                    //微信积分兑换礼品规则定义录入
    public const int CM_GTPT_JFDHLPGZDY_CX = 5167655;                    //微信积分兑换礼品规则定义查询


    public const int CM_GTPT_JFDHLPDYD = 5167656;                         //积分兑换礼品定义单
    public const int CM_GTPT_JFDHLPDYD_LR = 5167657;                      //积分兑换礼品定义单录入
    public const int CM_GTPT_JFDHLPDYD_SH = 5167658;                      //积分兑换礼品定义单审核
    public const int CM_GTPT_JFDHLPDYD_QD = 5167659;                      //积分兑换礼品定义单启动
    public const int CM_GTPT_JFDHLPDYD_ZZ = 5167660;                      //积分兑换礼品定义单终止
    public const int CM_GTPT_JFDHLPDYD_CX = 5167661;                      //积分兑换礼品定义单查询

    public const int CM_GTPT_WXFXDY = 5167675;                            //微信分享定义
    public const int CM_GTPT_WXLSSCSC = 5167676;                            //微信临时素材上传
    public const int CM_GTPT_QMQTH = 5167677;       //钱买券后台退款
    public const int CM_GTPT_WXSCYJTWSC = 5167678;                            //微信永久图文素材上传
    public const int CM_GTPT_BQDY = 5167679;                            //标签定义
    public const int CM_GTPT_HQBQXFS = 5167680;                            //获取标签下粉丝列表
    public const int CM_GTPT_HQHMD = 5167681;                            //获取黑名单

    public const int CM_GTPT_MEDIADY = 5167682;                          //媒体文件素材定义
    public const int CM_GTPT_NEWSDY = 5167683;                           //图文素材定义
    public const int CM_GTPT_WXKLXDY = 5167684;                           //图文素材定义

    public const int CM_GTPT_WXQFXXDY = 5167685;                           //图文素材定义

    public const int CM_GTPT_HYDBQ = 5167686;                           //会员打标签
    public const int CM_GTPT_WXHYKCX = 5167687;
    public const int CM_GTPT_HYZDBQ = 5167688;                           //动态会员组打标签
    public const int CM_GTPT_WXKBHYKTF = 5167689;                           //微信卡包会员卡投放设置


    public const int CM_GTPT_JFDHBLDY = 5167690;                         //积分兑换比例规则定义
    public const int CM_GTPT_JFDHBLDY_LR = 5167691;                      //积分兑换比例规则定义录入
    public const int CM_GTPT_JFDHBLDY_SH = 5167692;                      //积分兑换比例规则定义审核
    public const int CM_GTPT_JFDHBLDY_CX = 5167693;                      //积分兑换比例规则定义查询
    public const int CM_GTPT_JFDHBLDY_ZZ = 5167694;                      //积分兑换比例规则定义终止
    public const int CM_GTPT_JFDHBLDY_QD = 5167695;                      //积分兑换比例规则定义启动

    //public const int CM_GTPT_TCCDKDYD = 5167690;                         //停车场抵扣定义单
    //public const int CM_GTPT_TCCDKDYD_LR = 5167691;                      //停车场抵扣定义单录入
    //public const int CM_GTPT_TCCDKDYD_SH = 5167692;                      //停车场抵扣定义单审核
    //public const int CM_GTPT_TCCDKDYD_CX = 5167693;                      //停车场抵扣定义单查询
    //public const int CM_GTPT_TCCDKDYD_ZZ = 5167694;                      //停车场抵扣定义单终止
    //public const int CM_GTPT_TCCDKDYD_QD = 5167695;                      //停车场抵扣定义单启动


    //public const int CM_GTPT_WXSQDYD = 5167532;                           //微信送券规则定义单
    //public const int CM_GTPT_WXQDGZ = 5167533;                            //微信签到赠积分规则定义
    //public const int CM_GTPT_WXQDGZ_LR = 5167534;                         //微信签到赠积分规则定义录入
    //public const int CM_GTPT_WXQDGZ_SH = 5167535;                         //微信签到赠积分规则定义审核
    //public const int CM_GTPT_WXQDGZ_QD = 5167536;                         //微信签到赠积分规则定义启动
    //public const int CM_GTPT_WXQDGZ_ZZ = 5167537;                         //微信签到赠积分规则定义终止
    //public const int CM_GTPT_WXQDGZ_CX = 5167538;                         //微信签到赠积分规则定义查询
    //public const int CM_GTPT_WXLBDY = 5167539;                            //微信礼包定义
    //public const int CM_GTPT_WXLBFFGZ = 5167540;                          //微信礼包发放规则定义
    //public const int CM_GTPT_WXCJDYD = 5167541;                           //微信抽奖定义单
    //public const int CM_GTPT_WXCJDYD_LR = 5167542;                        //微信抽奖定义单录入
    //public const int CM_GTPT_WXCJDYD_SH = 5167543;                        //微信抽奖定义单审核
    //public const int CM_GTPT_WXCJDYD_QD = 5167544;                        //微信抽奖定义单启动
    //public const int CM_GTPT_WXCJDYD_ZZ = 5167545;                        //微信抽奖定义单终止
    //public const int CM_GTPT_WXCJDYD_CX = 5167546;                        //微信抽奖定义单查询
    //public const int CM_GTPT_WXKKSFGZ = 5167547;                          //微信开卡赠积分规则定义
    //public const int CM_GTPT_WXKKSFGZ_LR = 5167548;                       //微信开卡赠积分规则定义录入
    //public const int CM_GTPT_WXKKSFGZ_SH = 5167549;                       //微信开卡赠积分规则定义审核
    //public const int CM_GTPT_WXKKSFGZ_QD = 5167550;                       //微信开卡赠积分规则定义启动
    //public const int CM_GTPT_WXKKSFGZ_ZZ = 5167551;                       //微信开卡赠积分规则定义终止
    //public const int CM_GTPT_WXKKSFGZ_CX = 5167552;                       //微信开卡赠积分规则定义查询
    //public const int CM_GTPT_WXBKSFGZ = 5167553;                          //微信绑卡赠积分规则定义
    //public const int CM_GTPT_WXBKSFGZ_LR = 5167554;                       //微信绑卡赠积分规则定义录入
    //public const int CM_GTPT_WXBKSFGZ_SH = 5167555;                       //微信绑卡赠积分规则定义审核
    //public const int CM_GTPT_WXBKSFGZ_QD = 5167556;                       //微信绑卡赠积分规则定义启动
    //public const int CM_GTPT_WXBKSFGZ_ZZ = 5167557;                       //微信绑卡赠积分规则定义终止
    //public const int CM_GTPT_WXBKSFGZ_CX = 5167558;                       //微信绑卡赠积分规则定义查询
    //public const int CM_GTPT_WDCNRDY = 5167559;                           //微调查内容定义
    //public const int CM_GTPT_WDCDY = 5167560;                             //微调查定义
    //public const int CM_GTPT_WDCDY_LR = 5167561;                          //微调查定义录入
    //public const int CM_GTPT_WDCDY_SH = 5167562;                          //微调查定义审核
    //public const int CM_GTPT_WDCDY_QD = 5167563;                          //微调查定义启动
    //public const int CM_GTPT_WDCDY_ZZ = 5167564;                          //微调查定义终止
    //public const int CM_GTPT_WDCDY_CX = 5167565;                          //微调查定义查询
    //public const int CM_GTPT_WDCTJCX = 5167566;                           //微调查结果统计
    //public const int CM_GTPT_WDCMXCX = 5167567;                           //微调查明细查询
    //public const int CM_GTPT_TSLXDY = 5167568;                            //投诉类型定义
    //public const int CM_GTPT_TSCL = 5167569;                              //投诉处理
    //public const int CM_GTPT_TSCL_CL = 5167570;                           //投诉处理处理
    //public const int CM_GTPT_TSCL_HF = 5167571;                           //投诉处理回访
    //public const int CM_GTPT_TSCL_CX = 5167572;                           //投诉处理查询
    //public const int CM_GTPT_WXQHBDYD = 5167573;                          //微信抢红包定义单
    //public const int CM_GTPT_WXQHBDYD_LR = 5167574;                       //微信抢红包定义单录入
    //public const int CM_GTPT_WXQHBDYD_SH = 5167575;                       //微信抢红包定义单审核
    //public const int CM_GTPT_WXQHBDYD_QD = 5167576;                       //微信抢红包定义单启动
    //public const int CM_GTPT_WXQHBDYD_ZZ = 5167577;                       //微信抢红包定义单终止
    //public const int CM_GTPT_WXQHBDYD_CX = 5167578;                       //微信抢红包定义单查询
    //public const int CM_GTPT_WXDZPDYD = 5167579;                          //微信大转盘定义单
    //public const int CM_GTPT_WXDZPDYD_LR = 5167580;                       //微信大转盘定义单录入
    //public const int CM_GTPT_WXDZPDYD_SH = 5167581;                       //微信大转盘定义单审核
    //public const int CM_GTPT_WXDZPDYD_QD = 5167582;                       //微信大转盘定义单启动
    //public const int CM_GTPT_WXDZPDYD_ZZ = 5167583;                       //微信大转盘定义单终止
    //public const int CM_GTPT_WXDZPDYD_CX = 5167584;                       //微信大转盘定义单查询
    //public const int CM_GTPT_WXGGKDYD = 5167585;                          //微信刮刮卡定义单
    //public const int CM_GTPT_WXGGKDYD_LR = 5167586;                       //微信刮刮卡定义单录入
    //public const int CM_GTPT_WXGGKDYD_SH = 5167587;                       //微信刮刮卡定义单审核
    //public const int CM_GTPT_WXGGKDYD_QD = 5167588;                       //微信刮刮卡定义单启动
    //public const int CM_GTPT_WXGGKDYD_ZZ = 5167589;                       //微信刮刮卡定义单终止
    //public const int CM_GTPT_WXGGKDYD_CX = 5167590;                       //微信刮刮卡定义单查询
    //public const int CM_GTPT_WXBDHYTJ = 5167591;                          //微信绑定会员统计
    //public const int CM_GTPT_WXGZGNFX = 5167592;                          //微信关注功能分析
    //public const int CM_GTPT_AKLXFXWXBDHY = 5167593;                      //按卡类型分析微信绑定会员
    //public const int CM_GTPT_WXYHGZFX = 5167594;                          //微信用户关注分析
    //public const int CM_GTPT_WXGJCCFJLCX = 5167595;                       //微信关键词触发记录查询
    //public const int CM_GTPT_WXYHCX = 5167596;                            //微信用户查询
    //public const int CM_GTPT_WXJPFFJLCX = 5167597;                        //微信奖品发放记录查询
    //public const int CM_GTPT_WDCZLJLCX = 5167598;                         //微调查赠礼记录查询
    //public const int CM_GTPT_WDCYHCX = 5167599;                           //微调查用户查询
    //public const int CM_GTPT_WXYYFWDY = 5167600;                          //微信预约服务定义
    //public const int CM_GTPT_WXZXHDDY = 5167601;                          //微信最新活动定义
    //public const int CM_GTPT_WXJPJCDY = 5167602;                          //微信奖品级次定义
    //public const int CM_GTPT_WXLJD = 5167603;                             //微信领奖单
    //public const int CM_GTPT_WXLJD_LR = 5167604;                          //微信领奖单录入
    //public const int CM_GTPT_WXLJD_SH = 5167605;                          //微信领奖单审核
    //public const int CM_GTPT_WXLJD_CX = 5167606;                          //微信领奖单查询
    //public const int CM_GTPT_WXSQJLCX = 5167607;                          //微信送券记录查询
    //public const int CM_GTPT_WXKKBKZLJLCX = 5167608;                      //微信开卡绑卡赠礼记录查询
    //public const int CM_GTPT_WXYYFWJLCX = 5167609;                        //微信预约服务记录查询
    //public const int CM_GTPT_WXJFDHLPD = 5167610;                         //微信积分兑换礼品单
    //public const int CM_GTPT_WXJFDHLPD_LR = 5167611;                      //微信积分兑换礼品单录入
    //public const int CM_GTPT_WXJFDHLPD_SH = 5167612;                      //微信积分兑换礼品单审核
    //public const int CM_GTPT_WXJFDHLPD_QD = 5167613;                      //微信积分兑换礼品单启动
    //public const int CM_GTPT_WXJFDHLPD_ZZ = 5167614;                      //微信积分兑换礼品单终止
    //public const int CM_GTPT_WXJFDHLPD_CX = 5167615;                      //微信积分兑换礼品单查询
    //public const int CM_GTPT_WXJFDHYHQ = 5167616;                         //微信积分兑换优惠券单
    //public const int CM_GTPT_WXJFDHYHQ_LR = 5167617;                      //微信积分兑换优惠券单录入
    //public const int CM_GTPT_WXJFDHYHQ_SH = 5167618;                      //微信积分兑换优惠券单审核
    //public const int CM_GTPT_WXJFDHYHQ_QD = 5167619;                      //微信积分兑换优惠券单启动
    //public const int CM_GTPT_WXJFDHYHQ_ZZ = 5167620;                      //微信积分兑换优惠券单终止
    //public const int CM_GTPT_WXJFDHYHQ_CX = 5167621;                      //微信积分兑换优惠券单查询
    //public const int CM_GTPT_WXJFDHLPLQD = 5167622;                       //微信积分兑换礼品领取单
    //public const int CM_GTPT_WXJFDHLPLQD_LR = 5167623;                    //微信积分兑换礼品领取单录入
    //public const int CM_GTPT_WXJFDHLPLQD_SH = 5167624;                    //微信积分兑换礼品领取单审核
    //public const int CM_GTPT_WXJFDHLPLQD_QD = 5167625;                    //微信积分兑换礼品领取单启动
    //public const int CM_GTPT_WXJFDHLPLQD_ZZ = 5167626;                    //微信积分兑换礼品领取单终止
    //public const int CM_GTPT_WXJFDHLPLQD_CX = 5167627;                    //微信积分兑换礼品领取单查询
    //public const int CM_GTPT_WXHYKTPSC = 5167628;                         //微信会员卡图片上传
    //public const int CM_GTPT_WXKKBK_CX = 5167629;                         //微信开卡绑卡查询
    //public const int CM_GTPT_WXKBKTPSC = 5167630;                         //微信开卡绑卡图片上传
    //public const int CM_GTPT_WXKKBKSJF_CX = 5167631;                      //微信开卡绑卡送积分查询
    //public const int CM_GTPT_WXKKBKTJR_CX = 5167632;                      //微信开卡绑卡推荐人查询
    //public const int CM_GTPT_SBZKDYD = 5167633;                         //微信品牌折扣定义单
    //public const int CM_GTPT_SBZKDYD_LR = 5167634;                      //微信品牌折扣定义单录入
    //public const int CM_GTPT_SBZKDYD_SH = 5167635;                      //微信品牌折扣定义单审核
    //public const int CM_GTPT_SBZKDYD_QD = 5167636;                      //微信品牌折扣定义单启动
    //public const int CM_GTPT_SBZKDYD_ZZ = 5167637;                      //微信品牌折扣定义单终止
    //public const int CM_GTPT_SBZKDYD_CX = 5167638;                      //微信品牌折扣定义单查询
    //public const int CM_GTPT_GGDY = 5167639;                            //广告定义
    //public const int CM_GTPT_GXHFHDY = 5167640;                         //微信个性化符号定义
    //public const int CM_GTPT_CSDEF = 5167641;                            //微信城市定义
    //public const int CM_GTPT_LMSHLXDY = 5167642;                          // 商户联盟类型定义

    //public const int CM_GTPT_WXYYFWJLCL = 5167650;                       //微信预约服务记录处理
    //public const int CM_GTPT_WXYYFWJLCL_LR = 5167651;                    //微信预约服务记录处理录入
    //public const int CM_GTPT_WXYYFWJLCL_SH = 5167652;                    //微信预约服务记录处理审核
    //public const int CM_GTPT_WXYYFWJLCL_CX = 5167677;                    //微信预约服务记录处理查询

    //public const int CM_GTPT_WXSQDYD_LR = 5167678;                       //微信送券规则定义单录入
    //public const int CM_GTPT_WXSQDYD_SH = 5167679;                      //微信送券规则定义单审核
    //public const int CM_GTPT_WXSQDYD_QD = 5167680;                      //微信送券规则定义单启动
    //public const int CM_GTPT_WXSQDYD_ZZ = 5167681;                      //微信送券规则定义单终止
    //public const int CM_GTPT_WXSQDYD_CX = 5167682;                       //微信送券规则定义单查询

    #endregion
    #region CRMREPORT
    public const int CM_CRMREPORT_RFMJB = 5169001;                        //RFM级别定义
    public const int CM_CRMREPORT_RFMQZ = 5169002;                        //RFM权重定义
    public const int CM_CRMREPORT_RFMFXZB = 5169003;                      //RFM分析指标定义
    public const int CM_CRMREPORT_RFMFXZB_MD = 5169004;                   //门店RFM分析指标定义
    public const int CM_CRMREPORT_HYXFFX = 5169005;                       //会员消费分析    
    public const int CM_CRMREPORT_CFGML = 5169006;                        //重复购买率分析
    public const int CM_CRMREPORT_HYJG = 5169007;                         //会员结构分析
    public const int CM_CRMREPORT_HYKDJ = 5169008;                        //会员客单价分析
    public const int CM_CRMREPORT_HYXZLS = 5169009;                       //会员新增与流失
    public const int CM_CRMREPORT_RFMJZFX = 5169010;                      //RFM价值分析
    public const int CM_CRMREPORT_RFMPLFX = 5169011;                      //RFM品类分析
    public const int CM_CRMREPORT_RFMWDXFX = 5169012;                     //RFM稳定性分析
    public const int CM_CRMREPORT_BMFLJF = 5169012;                       //按部门品牌查询积分
    public const int CM_CRMREPORT_RFMPHFX = 5169014;                      //RFM偏好分析
    public const int CM_CRMREPORT_HYDFX = 5169015;                        //会员活跃度分析
    public const int CM_CRMREPORT_LSLFX = 5169016;                        //会员流失率分析
    public const int CM_CRMREPORT_XZHYXFFX = 5169017;                     //新增会员消费分析
    public const int CM_CRMREPORT_HYFKZKDJ = 5169018;                     //会员分卡种客单价分析
    public const int CM_CRMREPORT_KQ_KQZFX = 5169019;                     //客户群销售对比
    public const int CM_CRMREPORT_KQ_JTSXFX = 5169020;                    //客户群静态属性分析
    public const int CM_CRMREPORT_KQ_XFQSFX = 5169021;                    //客户群消费趋势分析
    public const int CM_CRMREPORT_KQ_XFPLFX = 5169022;                    //客户群消费品类分析
    public const int CM_CRMREPORT_KQ_XFPHFX = 5169023;                    //客户群消费偏好分析
    public const int CM_CRMREPORT_KQ_DJFX = 5169024;                      //客户群单价分析
    public const int CM_CRMREPORT_HYDJXFRBB = 5169025;                    //会员等级消费日报表
    public const int CM_CRMREPORT_HYFLXFRBB = 5169026;                    //会员分类消费日报表
    public const int CM_CRMREPORT_HYXFRBB = 5169027;                      //会员消费日报表
    public const int CM_CRMREPORT_HYSMZQFX = 5169028;                     //会员生命周期分析
    public const int CM_CRMREPORT_HYXFXQXG = 5169029;                     //按门店分析消费星期习惯
    public const int CM_CRMREPORT_HYXFSDXG = 5169030;                     //按门店分析消费时段习惯
    public const int CM_CRMREPORT_KQ_XFSDXG = 5169031;                    //按客群分析消费时段习惯
    public const int CM_CRMREPORT_KQ_XFXQXG = 5169032;                    //按客群分析消费星期习惯
    public const int CM_CRMREPORT_HYJGDFX = 5169033;                      //门店价格带分析
    public const int CM_CRMREPORT_HYJZFX = 5169034;                      //会员价值分析
    public const int CM_CRMREPORT_HYJJRFX = 5169035;                      //会员节假日分析
    public const int CM_CRMREPORT_HYYXHDFX = 5169036;                     //会员营销活动分析
    public const int CM_CRMREPORT_HYJFFLFX = 5169037;                     //会员积分返利分析报表
    public const int CM_CRMREPORT_JJR_RQ_DEF = 5169038;                     //节假日RIQI定义
    public const int CM_CRMREPORT_YXHHDEF = 5169039;                     //营销活动定义
    public const int CM_CRMREPORT_XZHYFX_RQ = 5169040;                     //新增会员日期分析
    public const int CM_CRMREPORT_JJR_DEF = 5169042;                     //节假日定义
    public const int CM_CRMREPORT_SQXQXFFX = 5169043;                    //商圈小区消费分析
    public const int CM_CRMREPORT_HYJSPXSCX = 5169044;                    //会员价商品销售查询
    public const int CM_CRMREPORT_HYZXSPXSCX = 5169045;                    //会员专项商品销售查询
    public const int CM_CRMREPORT_SQXQFX = 5169046;                       //商圈小区分析
    public const int CM_CRMREPORT_HYZTFX = 5169047;                       //会员卡状态分析
    public const int CM_CRMREPORT_XZHYSL = 5169048;                       //新增会员数量
    public const int CM_CRMREPORT_XSYHHYQS = 5169049;                     //线上用户活跃趋势
    public const int CM_CRMREPORT_YHHX = 5169050;                         //用户画像
    public const int CM_CRMREPORT_SJZDFX = 5169051;                       //手机终端分析



    #endregion
    #region CM_KFPT
    public const int CM_KFPT_CXXX = 5164001;                              //促销信息
    public const int CM_KFPT_DYHYFX = 5164002;                            //单一会员分析
    public const int CM_KFPT_YRGZAP = 5164003;                            //一日工作安排
    public const int CM_KFPT_XFYJ = 5164004;                              //会员日消费预警
    public const int CM_KFPT_KHXFDJZDY = 5164005;                         //客户消费等级组定义
    public const int CM_KFPT_HYXYDJZDY = 5164006;                         //会员信用等级组定义
    public const int CM_KFPT_BMHYXFFX = 5164007;                          //部门会员消费分析
    public const int CM_KFPT_GZLTJ = 5164008;                             //工作量统计
    public const int CM_KFPT_DRXFJEYJ = 5164009;                          //当日消费金额预警
    public const int CM_KFPT_HYSJYJ = 5164010;                            //会员升级预警
    public const int CM_KFPT_HYHDDY = 5164011;                            //会员活动定义
    public const int CM_KFPT_HYHDDY_LR = 5164012;                         //会员活动定义录入
    public const int CM_KFPT_HYHDDY_SH = 5164013;                         //会员活动定义审核
    public const int CM_KFPT_HYHDDY_CX = 5164014;                         //会员活动定义查询
    public const int CM_KFPT_KFZDY = 5164015;                             //客服组定义
    public const int CM_KFPT_KFJLDY = 5164016;                            //会员客户经理定义
    public const int CM_KFPT_JFYJDY = 5164017;                            //积分预警规则定义
    public const int CM_KFPT_SRFWLBDY = 5164018;                          //私人定制服务类别定义
    public const int CM_KFPT_YDYFWLBDY = 5164019;                         //一对一服务类别定义
    public const int CM_KFPT_XFYJDY = 5164020;                            //当日消费金额预警定义
    public const int CM_KFPT_TSLXDY = 5164021;                            //投诉类型定义
    public const int CM_KFPT_CXHDLR = 5164022;                            //促销活动录入
    public const int CM_KFPT_KFJLXG = 5164023;                            //会员客服经理修改
    public const int CM_KFPT_KFJLXG_LR = 5164024;                         //会员客服经理修改录入
    public const int CM_KFPT_KFJLXG_SH = 5164025;                         //会员客服经理修改审核
    public const int CM_KFPT_KFJLXG_CX = 5164026;                         //会员客服经理修改查询
    public const int CM_KFPT_HYHDBM = 5164027;                            //会员活动报名
    public const int CM_KFPT_HYHDBM_LR = 5164028;                         //会员活动报名录入
    public const int CM_KFPT_HYHDBM_SH = 5164029;                         //会员活动报名审核
    public const int CM_KFPT_HYHDBM_CX = 5164030;                         //会员活动报名查询
    public const int CM_KFPT_HDFX = 5164031;                              //活动分析
    public const int CM_KFPT_HYHDCJ = 5164032;                            //会员活动参加
    public const int CM_KFPT_RWFB = 5164033;                              //任务发布
    public const int CM_KFPT_HYHDLDPS = 5164034;                          //会员活动领导评述
    public const int CM_KFPT_HYHDLDPS_LR = 5164035;                       //会员活动领导评述录入
    public const int CM_KFPT_HYHDLDPS_CX = 5164036;                       //会员活动领导评述查询
    public const int CM_KFPT_HYHDHF = 5164037;                            //会员活动回访
    public const int CM_KFPT_HYHDHF_LR = 5164038;                         //会员活动回访录入
    public const int CM_KFPT_HYHDHF_CX = 5164039;                         //会员活动回访查询
    public const int CM_KFPT_RWZX = 5164040;                              //任务执行
    public const int CM_KFPT_RWCL = 5164041;                              //任务处理
    public const int CM_KFPT_CXYZXRW = 5164042;                           //查询已执行任务
    public const int CM_KFPT_QTHYXHSB = 5164043;                          //群体会员喜好品牌
    public const int CM_KFPT_SRHY = 5164044;                              //生日会员
    public const int CM_KFPT_HYXFTQB = 5164045;                           //会员消费同、环比分析
    public const int CM_KFPT_KHXFDJZFX = 5164046;                         //客户消费等级组分析
    public const int CM_KFPT_HYSRDHJC = 5164047;                          //会员生日电话检查
    public const int CM_KFPT_DQHYDHWH = 5164048;                          //定期会员电话维护
    public const int CM_KFPT_WXFHYDHWH = 5164059;                         //未消费会员电话维护
    public const int CM_KFPT_DHWHJLCX = 5164060;                          //电话维护记录查询
    public const int CM_KFPT_KFJLHYCX = 5164061;                          //客服经理会员查询
    public const int CM_KFPT_VIPBZXXCX = 5164062;                         //客服人员填写备注信息查询
    public const int CM_KFPT_VIPBZXXCX_CX = 5164063;                         //客服人员填写备注信息查询
    public const int CM_KFPT_TSJL = 5164064;                          //投诉记录
    public const int CM_KFPT_TSJL_LR = 5164065;                       //投诉记录录入
    public const int CM_KFPT_TSJL_CX = 5164066;                       //投诉记录查询
    public const int CM_KFPT_TSJL_SH = 5164067;                       //投诉记录审核

    #endregion
    #region JKPT
    public const int CM_JKPT_YJGZDY = 5164501;                            //预警规则定义

    public const int CM_JKPT_YJHYR = 5164502;                             //预警会员日数据
    public const int CM_JKPT_YJHYY = 5164503;                             //预警会员月数据
    public const int CM_JKPT_MDXFR = 5164504;                             //门店消费次数监控日
    public const int CM_JKPT_MDXFY = 5164505;                             //门店消费次数监控月
    public const int CM_JKPT_SKTXFR = 5164506;                            //收款台消费次数监控日
    public const int CM_JKPT_SKTXFR_1 = 5164507;                          //单一收款台监控消费次数日
    public const int CM_JKPT_SKTXFY = 5164508;                            //收款台消费次数监控月
    public const int CM_JKPT_SKTXFY_1 = 5164509;                          //单一收款台监控消费次数月
    public const int CM_JKPT_BMXFR = 5164510;                             //部门消费次数监控日
    public const int CM_JKPT_BMXFR_1 = 5164511;                           //单一部门监控消费次数日
    public const int CM_JKPT_BMXFY = 5164512;                             //部门消费次数监控月
    public const int CM_JKPT_BMXFY_1 = 5164513;                           //单一部门监控消费次数月
    public const int CM_JKPT_MDPMR = 5164514;                             //会员门店消费日排名
    public const int CM_JKPT_SKTPMR = 5164515;                            //会员收款台消费日排名
    public const int CM_JKPT_BMPMR = 5164516;                             //会员部门消费日排名
    public const int CM_JKPT_MDPMY = 5164517;                             //会员门店消费月排名
    public const int CM_JKPT_SKTPMY = 5164518;                            //会员收款台消费月排名
    public const int CM_JKPT_BMPMY = 5164519;                             //会员部门消费月排名
    public const int CM_JKPT_KYHY = 5164520;                                //可疑会员
    public const int CM_JKPT_KYHY_LR = 5164521;                            //可疑会员录入
    public const int CM_JKPT_YJGZDY_LR = 5164522;                        //预警规则定义录入
    public const int CM_JKPT_YJHYRZB = 5164523;                             //预警会员日数据总部
    public const int CM_JKPT_YJHYYZB = 5164524;                             //预警会员月数据总部
    public const int CM_JKPT_XFCSJK = 5164525;                             //消费次数监控

    #endregion
    //DXPT

    #region DXPT

    public const int CM_DXPT_KDQTX = 5163001;                            //卡到期提醒
    public const int CM_DXPT_SBPMTX = 5163002;                           //商标排名提醒
    public const int CM_DXPT_SPFLPMTX = 5163003;                         //商品分类排名提醒
    public const int CM_DXPT_SRDX = 5163004;                             //生日短信
    public const int CM_DXPT_KSJTX = 5163005;                            //可升级提醒
    public const int CM_DXPT_JJSJTX = 5163006;                           //即将升级提醒
    public const int CM_DXPT_PPFLHYXFPM = 5163007;                       //按门店品牌分类排名
    public const int CM_DXPT_HYXFPM = 5163008;                           //会员消费排名
    public const int CM_DXPT_DXTDDY = 5163009;                           //短信通道定义
    public const int CM_DXPT_PPXFPM = 5163010;                           //品牌消费排名
    public const int CM_DXPT_FLXFPM = 5163011;                           //分类消费排名
    public const int CM_DXPT_JJJJHY = 5163012;                            //即将到期提醒
    public const int CM_DXPT_YJJHY = 5163013;                            //已降级提醒
    public const int CM_DXPT_HYGKJZXFPM = 5163014;                       //顾客价值分类消费排名
    public const int CM_DXPT_GXHFHDY = 5163015;                          //个性化符号定义
    public const int CM_DXPT_SENDSMSDEF = 5163016;                       //会员短信发送设置
    public const int CM_DXPT_SENDSMSDEF_LR = 5163017;                    //会员短信发送设置录入

    public const int CM_DXPT_DXHDZTDY = 5163030;                         //短信活动主题定义 
    public const int CM_DXPT_DXHDZTDY_LR = 5163031;                      //短信活动主题定义录入
    public const int CM_DXPT_DXHDZTDY_SH = 5163032;                      //短信活动主题定义审核
    public const int CM_DXPT_DXHDZTDY_CX = 5163033;                      //短信活动主题定义查询
    public const int CM_DXPT_KJFSNRDEF = 5163034;                        //快捷发送内容设置


    #endregion

    #region APP
    public const int CM_APP_YDDPPJS = 5162001;                           //移动端品牌介绍
    public const int CM_APP_SHFWLJS = 5162002;                           //移动端生活服务类介绍
    public const int CM_APP_LPZBDY = 5162003;                            //移动端礼品组包定义
    public const int CM_APP_YTMDDY = 5162004;                            //移动端业态门店定义
    public const int CM_APP_YDDZXHDMDDY = 5162005;                       //移动端最新活动门店定义
    public const int CM_APP_YDDYXJPJCDY = 5162006;                       //移动端游戏奖品级次定义
    public const int CM_APP_YDDYXLPFFGZ = 5162007;                       //移动端游戏礼品发放规则
    public const int CM_APP_SHDY = 5162008;                              //移动端商户定义
    public const int CM_APP_QYDY = 5162009;                              //移动端区域定义
    public const int CM_APP_YDDYYFWDY = 5162010;                         //移动端预约服务定义
    public const int CM_APP_YDDDCTMDY = 5162011;                         //移动端调查题目定义
    public const int CM_APP_YDDDCDY = 5162012;                            //移动端调查定义

    public const int CM_APP_YDDDCDY_LR = 5162013;                          //移动端调查录入
    public const int CM_APP_YDDDCDY_SH = 5162014;                          //移动端调查审核
    public const int CM_APP_YDDDCDY_QD = 5162015;                          //移动端调查启动
    public const int CM_APP_YDDDCDY_ZZ = 5162016;                          //移动端调查终止
    public const int CM_APP_YDDDCDY_CX = 5162017;                          //移动端调查查询
    public const int CM_APP_YDDPPTJ = 5162018;                             //移动端品牌推荐
    public const int CM_APP_KKSFGZ = 5162019;                          //移动端开卡赠积分规则定义
    public const int CM_APP_KKSFGZ_LR = 5162020;                       //移动端开卡赠积分规则定义录入
    public const int CM_APP_KKSFGZ_SH = 5162021;                       //移动端开卡赠积分规则定义审核
    public const int CM_APP_KKSFGZ_QD = 5162022;                       //移动端开卡赠积分规则定义启动
    public const int CM_APP_KKSFGZ_ZZ = 5162023;                       //移动端开卡赠积分规则定义终止
    public const int CM_APP_KKSFGZ_CX = 5162024;                       //移动端开卡赠积分规则定义查询
    public const int CM_APP_BKSFGZ = 5162025;                          //移动端绑卡赠积分规则定义
    public const int CM_APP_BKSFGZ_LR = 5162026;                       //移动端绑卡赠积分规则定义录入
    public const int CM_APP_BKSFGZ_SH = 5162027;                       //移动端绑卡赠积分规则定义审核
    public const int CM_APP_BKSFGZ_QD = 5162028;                       //移动端绑卡赠积分规则定义启动
    public const int CM_APP_BKSFGZ_ZZ = 5162029;                       //移动端绑卡赠积分规则定义终止
    public const int CM_APP_BKSFGZ_CX = 5162030;                        //移动端绑卡赠积分规则定义查询
    public const int CM_APP_HYQYDY = 5162031;                        //移动端会员权益定义
    public const int CM_APP_TSLXDY = 5162032;                        //移动端投诉类型定义
    public const int CM_APP_LMSHLXDY = 5162033;                        //移动端联盟商户类型定义
    public const int CM_APP_LMSHDY = 5162034;                        //移动端联盟商户定义

    public const int CM_APP_QDGZ = 5162035;                            //移动端签到赠积分规则定义
    public const int CM_APP_QDGZ_LR = 5162036;                         //移动端签到赠积分规则定义录入
    public const int CM_APP_QDGZ_SH = 5162037;                         //移动端签到赠积分规则定义审核
    public const int CM_APP_QDGZ_QD = 5162038;                         //移动端签到赠积分规则定义启动
    public const int CM_APP_QDGZ_ZZ = 5162039;                         //移动端签到赠积分规则定义终止
    public const int CM_APP_QDGZ_CX = 5162040;                         //移动端签到赠积分规则定义查询
    public const int CM_APP_DXFL = 5162041;                            //移动端大小分类定义
    public const int CM_APP_YDDSPTJ = 5162042;                         //移动端商品推荐
    public const int CM_APP_MDLCSBDY = 5162043;                        //移动端门店内楼层商标定义
    public const int CM_APP_YDDZXHDLXDY = 5162044;                     //移动端最新活动定义

    public const int CM_APP_MDLCDY = 5162045;                             //移动端门店楼层定义
    public const int CM_APP_XFLDY = 5162046;                             //移动端小分类定义
    public const int CM_APP_CTLXDY = 5162047;                             //移动端餐厅类型定义
    public const int CM_APP_CTDY = 5162048;                             //移动端餐厅定义
    public const int CM_APP_CZLXDY = 5162049;                             //移动端餐桌类型定义
    public const int CM_APP_CZDY = 5162050;                             //移动端餐桌定义
    public const int CM_APP_CPLXDY = 5162051;                             //移动端菜品类型定义
    public const int CM_APP_CPDY = 5162052;                             //移动端菜品定义
    public const int CM_APP_YDDCXHDDY = 5162053;                            //移动端促销活动定义

    public const int CM_APP_CJDYD = 5162054;                           //微信抽奖定义单
    public const int CM_APP_CJDYD_LR = 5162055;                        //微信抽奖定义单录入
    public const int CM_APP_CJDYD_SH = 5162056;                        //微信抽奖定义单审核
    public const int CM_APP_CJDYD_QD = 5162057;                        //微信抽奖定义单启动
    public const int CM_APP_CJDYD_ZZ = 5162058;                        //微信抽奖定义单终止
    public const int CM_APP_CJDYD_CX = 5162059;                        //微信抽奖定义单查询

    public const int CM_APP_QHBDYD = 5162060;                          //微信抢红包定义单
    public const int CM_APP_QHBDYD_LR = 5162061;                       //微信抢红包定义单录入
    public const int CM_APP_QHBDYD_SH = 5162062;                       //微信抢红包定义单审核
    public const int CM_APP_QHBDYD_QD = 5162063;                       //微信抢红包定义单启动
    public const int CM_APP_QHBDYD_ZZ = 5162064;                       //微信抢红包定义单终止
    public const int CM_APP_QHBDYD_CX = 5162065;                       //微信抢红包定义单查询

    public const int CM_APP_GGKDYD = 5162066;                          //微信刮刮卡定义单
    public const int CM_APP_GGKDYD_LR = 5162067;                       //微信刮刮卡定义单录入
    public const int CM_APP_GGKDYD_SH = 5162068;                       //微信刮刮卡定义单审核
    public const int CM_APP_GGKDYD_QD = 5162069;                       //微信刮刮卡定义单启动
    public const int CM_APP_GGKDYD_ZZ = 5162070;                       //微信刮刮卡定义单终止
    public const int CM_APP_GGKDYD_CX = 5162071;                       //微信刮刮卡定义单查询

    public const int CM_APP_DHT_MDXX = 5162072;                          //移动端门店基础信息定义导航图
    public const int CM_APP_DHT_DPDH = 5162073;                          //移动端店铺导航导航图
    public const int CM_APP_DHT_KFZX = 5162074;                          //移动端客服中心定义导航图
    public const int CM_APP_DHT_LMSH = 5162075;                          //移动端联盟商户定义导航图
    public const int CM_APP_DHT_PWDC = 5162076;                          //移动端排位点餐定义导航图
    public const int CM_APP_DHT_HDXX = 5162077;                          //移动端活动定义导航图
    public const int CM_APP_DHT_QDGZ = 5162078;                          //移动端签到规则定义导航图
    public const int CM_APP_DHT_DCWQ = 5162079;                          //移动端调查问券导航图
    public const int CM_APP_DHT_YWFF = 5162080;                          //移动端预约服务导航图
    public const int CM_APP_DHT_YXZL = 5162081;                          //移动端游戏赠礼导航图
    public const int CM_APP_DHT_KKBK = 5162082;                          //移动端开卡、绑卡赠礼导航图
    public const int CM_APP_DHT_CXBMQ = 5162083;                         //移动端促销编码券发放导航图
    public const int CM_APP_DHT_JFDL = 5162084;                          //移动端积分兑换礼品导航图
    public const int CM_APP_DHT_DXFL = 5162085;                          //移动端大小分类导航图
    public const int CM_APP_DHT_APPALL = 5162086;                        //移动端管理系统导航图
    public const int CM_APP_TXFWDY = 5162087;                            //贴心服务定义
    public const int CM_APP_LPFLDY = 5162088;                            //礼品分类定义
    public const int CM_APP_DHT_TCFW = 5162089;                        //移动端停车服务系统导航图

    #endregion

}
