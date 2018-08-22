vUrl = "../GTPT.ashx";
vCaption = "微调查信息记录";
var vWDCXXJLMXColumnNames;
var vWDCXXJLMXColumnModel;
var iSEARCHMODE = 0;
var iJLBH = 0;

function InitGrid() {
    vColumnNames = ["记录编号", '调查主题', "调查时间", "微信号", "会员卡号", "会员姓名",];
    vColumnModel = [
             { name: 'iJLBH', width: 80 },
             { name: 'sDCZT', width: 80 },
             { name: 'dDJSJ', width: 80, },
             { name: 'sWX_NO', width: 80, },
             { name: 'sHYK_NO', width: 80, },
             { name: 'sHY_NAME', width: 80, },
    ];
    vWDCXXJLMXColumnNames = ['题号', '题目', '选项', '内容', ];
    vWDCXXJLMXColumnModel = [
              { name: 'iID', width: 100, },
            { name: 'sMC', width: 200, },
            { name: 'iNRID', width: 100, },
            { name: 'sNRMC', width: 200, },
    ];
};
$(document).ready(function () {
    SetControlStatus();
    DrawGrid("list_1", vWDCXXJLMXColumnNames, vWDCXXJLMXColumnModel);
    $("#SearchResult .maininput").not("#WDCXXJL_Hidden").css("display", "none");
    RefreshButtonSep();

})
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_OPENID", "sWX_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_DCZT", "sDCZT", "=", true);
    MakeSrchCondition(arrayObj, "TB_DCRQ1", "dDCRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DCRQ2", "dDCRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
function SetControlStatus() {
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
}
function ToggleHiddenPanelCustomer(elementCurrent) {
    if ($("#" + elementCurrent + "").parent()[0].id = "SearchResult") {
        $("#SearchResult .maininput").not("#" + elementCurrent + "_Hidden").slideUp();
        switch (elementCurrent) {
            case "WDCXXJL": iSEARCHMODE = 0;
                break;
            case "WDCXXJLMX": iSEARCHMODE = 1;
                break;
        }
    }
}

function OnClickRow(rowIndex, rowData) {
    if (this.id == "list") {
        iJLBH= rowData.iJLBH;   
        GetWDCXXJLMX();
    }
}
function GetWDCXXJLMX() {
    $('#list_1').datagrid('loadData', { total: 0, rows: [] });
    $("#SearchResult .maininput").not("#WDCXXJLMX_Hidden").slideUp();
    $("#WDCXXJLMX_Hidden").slideDown();
    iSEARCHMODE = 1
    SearchData(undefined, undefined, undefined, undefined, "list_1");

}

function AddCustomerCondition(Obj) {
    Obj.iSEARCHMODE = iSEARCHMODE;
    Obj.iJLBH = iJLBH;
   
}