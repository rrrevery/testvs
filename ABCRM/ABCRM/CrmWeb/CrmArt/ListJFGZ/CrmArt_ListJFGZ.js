vUrl = "../../HYXF/HYXF.ashx";
vCaption = "补积分规则";
var vDialogName = "ListJFGZ";



function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '规则ID', hidden: true },
        { field: 'sMC', title: '规则名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_ID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_MC", "sMC", "like", true);
    return arrayObj;
};

