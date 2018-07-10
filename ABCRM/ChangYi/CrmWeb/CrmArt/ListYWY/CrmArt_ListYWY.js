
vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "业务员信息";
var vDialogName = "ListYWY";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
function InitGrid() {
    vColumns = [
        { field: 'iYWYID', title: '业务员ID', hidden: true },
        { field: 'sYWYMC', title: '业务员名称', width: 100 },
        { field: 'sYWYDM', title: '业务员代码', width: 100 },
        { field: 'sMDMC', title: '所属门店', width: 100 },
    ];
    vIdField = "iYWYID";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_YWYDM", "sYWYDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_YWYMC", "sYWYMC", "like", true);
    return arrayObj;
};