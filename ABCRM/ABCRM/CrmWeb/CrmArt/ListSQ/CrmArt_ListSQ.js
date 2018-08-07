vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "商圈信息";
var vDialogName = "ListSQ";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '商圈编号', hidden: true },
        { field: 'sSQMC', title: '商圈名称', width: 100 },
    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_SQMC", "sSQMC", "like", true);
    return arrayObj;
};

