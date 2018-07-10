vUrl = "../../GTPT/GTPT.ashx";
vCaption = "微信楼层";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
var vDialogName = "ListWXLC";

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '楼层ID', hidden: true },
        { field: 'sNAME', title: '楼层名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LCID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_LCMC", "sNAME", "like", true);
    if (data) {
        MakeSrchCondition2(arrayObj, data["iMDID"], "iMDID", "=", false);
    }
    return arrayObj;
};