vUrl = "../../LPGL/LPGL.ashx";
vCaption = "供货商信息";
var vDialogName = "ListGHS";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
var vBJ_TY = GetUrlParam("iBJ_TY");
function InitGrid() {
    vColumns = [
        { field: 'iGHSID', title: '供货商ID', width: 100 },
        { field: 'sGHSMC', title: '供货商名称', width: 100 },
    ];
    vIdField = "iGHSID";
}

$(document).ready(function () {
   
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_GHSID", "iGHSID", "=", false);
    MakeSrchCondition(arrayObj, "TB_GHSMC", "sGHSMC", "like", true);
    if (vBJ_TY) {
        MakeSrchCondition2(arrayObj, vBJ_TY, "iBJ_TY", "=", false);
    }
    return arrayObj;
};