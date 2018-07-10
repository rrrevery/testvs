vUrl = "../../GTPT/GTPT.ashx";
vCaption = "微信编码券";
var vDialogName = "ListBMQ";

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '编码券ID', hidden: true },
        { field: 'sBMQMC', title: '编码券名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_BMQID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_BMQMC", "sBMQMC", "like", true);
    return arrayObj;
};