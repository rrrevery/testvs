vCZK = GetUrlParam("vCZK");
vUrl = vCZK == "0" ? "../../HYKGL/HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";

vCaption = "申请单信息";
var vDialogName = "ListSQD";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '申请单编号', width: 100 },

        { field: 'sBGDDMC_BR', title: '拨入地点', width: 100 },
        { field: 'sBGDDDM_BR', title: '拨入地点代码', hidden: true },
        { field: 'iHYKSL', title: '申请数量', width: 50 },
        { field: 'dDJSJ', title: '申请时间', width: 100 },
        { field: 'iDJR', title: 'iDJR', hidden: true },
        { field: 'sDJRMC', title: '申请人名称', width: 100 },
    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition2(arrayObj, 1, "iSTATUS", "=", false);
    MakeSrchCondition2(arrayObj, "is null", "iLQR", "", false);
    return arrayObj;
};

