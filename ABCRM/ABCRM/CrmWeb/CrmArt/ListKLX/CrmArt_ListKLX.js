
vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "卡类型信息";
var vDialogName = "ListKLX";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '卡类型ID', width: 100 },
        { field: 'sHYKNAME', title: '卡类型名称', width: 100 },
    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYKTYPE", "iJLBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNAME", "sHYKNAME", "like", true);
    if (data) {
        MakeSrchCondition2(arrayObj, data["vCZK"], "iBJ_CZK", "=", false);
    }
    else
        MakeSrchCondition2(arrayObj, 0, "iBJ_CZK", "=", false);
    return arrayObj;
};