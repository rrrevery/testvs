vUrl = "../../GTPT/GTPT.ashx";
vCaption = "联盟商户类型";
var vDialogName = "ListLMSHLX";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '类型ID', width: 100 },
        { field: 'sLXMC', title: '类型名称', width: 100 },
    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LXID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_LXMC", "sLXMC", "=", true);
    return arrayObj;
};