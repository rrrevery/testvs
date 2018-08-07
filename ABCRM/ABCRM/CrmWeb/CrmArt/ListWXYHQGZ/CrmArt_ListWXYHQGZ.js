vUrl = "../../GTPT/GTPT.ashx";
vCaption = "微信优惠券规则";
var vDialogName = "ListWXYHQGZ";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '规则ID', hidden: true },
        { field: 'sGZMC', title: '规则名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_GZID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sGZMC", "like", true);
    return arrayObj;
};