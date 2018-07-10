vUrl = "../CRMGL.ashx";
var vCaption = "促销活动优惠券定义";

function InitGrid() {
    vColumnNames = ['YHQID', '促销活动编号', '优惠券名称', 'SHDM', '商户', 'CXID', '促销主题', '用劵结束日期', '有效期天数', ];
    vColumnModel = [
            { name: 'iJLBH', hidden: true, },
              { name: 'iCXHDBH', width: 100 },
              { name: 'sYHQMC', width: 230, },
              { name: 'sSHDM', hidden: true, },
              { name: 'sSHMC', },
              { name: 'iCXID', hidden: true },
              { name: 'sCXZT', },
              { name: 'dYHQSYJSRQ', width: 230, },
              { name: 'iYXQTS', width: 230, },
    ];
}


$(document).ready(function () {
    $("#B_Exec").hide();
    //ConbinDataArry["yhqid"] = vyhqid;
    //ConbinDataArry["shdm"] = vshdm;
    //ConbinDataArry["cxid"] = vcxid;

    $("#TB_CXZT").prop("disabled", true);


    BFButtonClick("TB_SHMC", function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    BFButtonClick("TB_YHQMC", function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    });
});

function UpdateClick() {
    //T-- 修改事件
    var sUpdateCurrentPath = "";
    sUpdateCurrentPath = sCurrentPath + "?action=edit&jlbh=" + vJLBH;
    var rowData = $("#list").datagrid("getSelected");
    ConbinDataArry["shdm"] = rowData.sSHDM;
    ConbinDataArry["cxid"] = rowData.iCXID;
    sUpdateCurrentPath = ConbinPath(sUpdateCurrentPath);
    MakeNewTab(sUpdateCurrentPath, vCaption, vPageMsgID);
};

function DBClickRow(rowid) {
    //T--表格双击事件
    var sDbCurrentPath = "";
    var rowData = $("#list").datagrid("getSelected");
    sDbCurrentPath = sCurrentPath + "?jlbh=" + rowData.iYHQID;
    ConbinDataArry["shdm"] = rowData.sSHDM;
    ConbinDataArry["cxid"] = rowData.iCXID;
    sDbCurrentPath = ConbinPath(sDbCurrentPath);
    MakeNewTab(sDbCurrentPath, vCaption, vPageMsgID);
};

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_CXHDBH", "sCXZT", "like", true);
    MakeSrchCondition(arrayObj, "TB_CXHDBH", "iCXHDBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "TB_YHQJSRQ1", "dYHQSYJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_YHQJSRQ2", "dYHQSYJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_CXZT", "iCXID", "=", false);
    MakeSrchCondition(arrayObj, "TB_KSSJ1", "dKSSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSSJ2", "dKSSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSSJ1", "dJSSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSSJ2", "dJSSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};