vUrl = "../../GTPT/GTPT.ashx";
vCaption = "微信分组";
var vDialogName = "ListWXGroup";

function InitGrid() {
    vColumns = [
        { field: 'iGROUPID', title: '分组ID', hidden: true },
        { field: 'sGROUP_NAME', title: '分组名称', width: 100 },

    ];
    vIdField = "iGROUPID";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GROUP_NAME", "sGROUP_NAME", "like", true);
    return arrayObj;
};