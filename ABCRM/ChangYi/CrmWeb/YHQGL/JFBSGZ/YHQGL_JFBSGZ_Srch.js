vUrl = "../YHQGL.ashx";
vCaption = "积分倍数规则";

function InitGrid() {
    vColumnNames = ['规则代码', '规则名称', ],
    vColumnModel = [
       { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
	   { name: 'sGZMC', width: 120, },
    ]
}


$(document).ready(function () {
    $("#B_Exec").hide();
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sGZMC", "=", true);
    //MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};