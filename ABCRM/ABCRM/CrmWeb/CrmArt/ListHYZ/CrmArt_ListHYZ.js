vUrl = "../../HYXF/HYXF.ashx";
vCaption = "会员组";
var vDialogName = "ListHYZ";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");


function InitGrid() {
    vColumns = [
        { field: 'iGRPID', title: '会员组ID', width: 100 },
        { field: 'sGRPMC', title: '会员组名称', width: 100 },
    ];
    vIdField = "iGRPID";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYZID", "iGRPID", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYZMC", "sGRPMC", "like", true);
    return arrayObj;
};

