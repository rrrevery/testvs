//vPageMsgID = CM_CRMGL_YHQDY;
vUrl = "../CRMGL.ashx";
vCaption = "优惠券定义";
function InitGrid() {
    vColumnNames = ["优惠券ID", "优惠券名称", "用券范围", "发券范围", "发券类型", "券类型", "退货有效天数"];
    vColumnModel = [
        { name: "iJLBH", width: 80, },
        { name: "sYHQMC", width: 120, },
        { name: "sFS_YQMDFW", width: 60, },
        { name: "sFS_FQMDFW", width: 60, },
        { name: "sFQLX", width: 60, },
        { name: "sBJ_TS", width: 60, },
        { name: "iYXQTS", width: 60, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_YHQID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_YHQMC", "sYHQMC", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};