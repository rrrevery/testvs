
vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "商户信息";
var vDialogName = "ListSH";
function InitGrid() {
    vColumns = [
        { field: 'sSHDM', title: '商户代码', width: 100 },
        { field: 'sSHMC', title: '商户名称', width: 100 },
    ];
    vIdField = "sSHDM";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_SHDM", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_SHMC", "sSHMC", "like", true);
    return arrayObj;
};