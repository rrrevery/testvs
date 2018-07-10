vUrl = "../CRMGL.ashx";
var vCaption = "优惠券使用商户定义";
var shdm = "";

function InitGrid() {
    vColumnNames = ["记录编号", "商户名称", "商户代码", "优惠券名称", "使用单标记"];
    vColumnModel = [
          { name: "iJLBH", hidden: true, },
          { name: "sSHDM", hidden: true, },
          { name: "sSHMC", width: 100, },
          { name: "sYHQMC", width: 160, },
          { name: "iBJ_SYD", width: 60, formatter: BoolCellFormat },
    ];
}

$(document).ready(function () {
    FillSH($("#DDL_SHMC"));
    FillYHQ($("#DDL_YHQMC"));
    $("#B_Exec").hide();
    $("#B_Delete").hide();

});

function UpdateClick() {
    //T-- 修改事件
    var sUpdateCurrentPath = "";
    sUpdateCurrentPath = sCurrentPath + "?action=edit&jlbh=" + vJLBH;
    var rowData = $("#list").datagrid("getSelected");
    ConbinDataArry["shdm"] = rowData.sSHDM;
    sUpdateCurrentPath = ConbinPath(sUpdateCurrentPath);
    MakeNewTab(sUpdateCurrentPath, vCaption, vPageMsgID);
};
function DBClickRow(rowid) {
    //T--表格双击事件
    var sDbCurrentPath = "";
    var rowData = $("#list").datagrid("getSelected");
    sDbCurrentPath = sCurrentPath + "?jlbh=" + rowData.iJLBH;
    ConbinDataArry["shdm"] = rowData.sSHDM;
    sDbCurrentPath = ConbinPath(sDbCurrentPath);
    MakeNewTab(sDbCurrentPath, vCaption, vPageMsgID);
};
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "DDL_SHMC", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "DDL_YHQMC", "iYHQID", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};