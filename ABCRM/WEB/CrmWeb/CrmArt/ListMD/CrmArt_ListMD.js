
vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "门店信息";
var vDialogName = "ListMD";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
var pSHDM = GetUrlParam("sSHDM");
function InitGrid() {
    vColumns = [
        { field: 'iMDID', title: '门店ID', hidden: true },
        { field: 'sMDMC', title: '门店名称', width: 100 },
        { field: 'sMDDM', title: '门店代码', width: 100 },
        { field: 'sSHMC', title: '商户名称', width: 100 },
    ];
    vIdField = "iMDID";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_MDDM", "sMDDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_MDMC", "sMDMC", "like", true);
    if (pSHDM) {
        MakeSrchCondition2(arrayObj, pSHDM, "sSHDM", "=", true);
    }
    return arrayObj;
};