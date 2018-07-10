vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "商品信息";
var vDialogName = "ListSHSP";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");

function InitGrid() {
    vColumns = [
        { field: 'iSHSPID', title: '商户商品ID', hidden: true },
        { field: 'sSPDM', title: '商品代码', width: 100 },
        { field: 'sSPMC', title: '商品名称', width: 100 },
    ];
    vIdField = "Id";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_SPDM", "sSPDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_SPMC", "sSPMC", "like", true);
    if (data) {
        MakeSrchCondition2(arrayObj, data["sSPFLDM"], "sSPFLDM", "=", true);
    }
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.dialogName = vDialogName;
}