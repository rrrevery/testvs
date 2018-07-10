vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "商品商标信息";
var vDialogName = "ListSPSB";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
var pSHDM = GetUrlParam("sSHDM");
function InitGrid() {
    vColumns = [
        { field: 'iSHSBID', title: '商户商标ID', hidden: true },
        { field: 'sSBDM', title: '商标代码', width: 100 },
        { field: 'sSBMC', title: '商标名称', width: 100 },
    ];
    vIdField = "iSHSBID";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    if (pSHDM)
        MakeSrchCondition2(arrayObj, pSHDM, "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_SBMC", "sSBMC", "like", true);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.dialogName = vDialogName;
}