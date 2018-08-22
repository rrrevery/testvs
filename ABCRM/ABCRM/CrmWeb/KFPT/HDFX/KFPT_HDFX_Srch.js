vUrl = "../KFPT.ashx";

function InitGrid() {
    vColumnNames = ['活动编号', '活动名称', '开始时间', '结束时间','提前报名人数','现场报名人数', '报名人数', '参加人数','参加报名比例', '回访人数','客服人员','领导评语','分值'];
    vColumnModel = [
             { name: 'iHDID', width: 80, },
             { name: 'sHDMC', width: 100, },
             { name: 'dKSSJ', width: 80, },
             { name: 'dJSSJ', width: 80, },
             { name: 'iTQBMRS', width: 80, },
             { name: 'iXCBMRS', width: 80, },
             { name: 'iBMRS', width: 80, },
             { name: 'iCJRS', width: 80, },
             { name: 'fCJBMBL', width: 80, },
             { name: 'iHFRS', width: 80, },
             { name: 'sPERSON_NAME', width: 100, },
             { name: 'sLDPY', width: 100, },
             { name: 'fFZ', width: 100, },
    ];
}


$(document).ready(function () {
    $("#B_Update").hide();
    $("#B_Insert").hide();
    FillHDMC($("#DDL_HDID"));//活动下拉框
    RefreshButtonSep();
})



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "DDL_HDID", "iHDID", "=", false);
    MakeSrchCondition(arrayObj, "TB_KSSJ1", "dKSSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSSJ2", "dKSSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSSJ1", "dJSSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSSJ2", "dJSSJ", "<=", true);
    return arrayObj;
};
