vUrl = "../../GTPT/GTPT.ashx";
vCaption = "素材";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
var vDialogName = "ListMEDIA";

function InitGrid() {
    vColumns = [
        { field: 'sMEDIA_ID', title: '标识', hidden: true },
        { field: 'sTITLE', title: '标题', width: 100 },
        { field: 'sDESCRIPTION', title: '描述', width: 100 },
    ];
    vIdField = "sMEDIA_ID";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_TITLE", "sTITLE", "like", true);
    if (data) {
        MakeSrchCondition2(arrayObj, data["iTYPE"], "iTYPE", "=", false);
        MakeSrchCondition2(arrayObj, data["iPUBLICID"], "iPUBLICID", "=", false);
    }
    return arrayObj;
};