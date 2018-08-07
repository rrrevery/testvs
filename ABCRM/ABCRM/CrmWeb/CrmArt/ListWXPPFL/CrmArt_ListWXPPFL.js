vUrl = "../../GTPT/GTPT.ashx";
vCaption = "品牌分类信息";
var vDialogName = "ListPPFL";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '分类ID', hidden: true },
        { field: 'sFLMC', title: '分类名称', width: 100 },
    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_FLID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_FLMC", "sFLMC", "like", true);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.dialogName = vDialogName;
}