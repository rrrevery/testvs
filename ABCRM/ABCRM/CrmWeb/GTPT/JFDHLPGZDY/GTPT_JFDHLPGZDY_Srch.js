vUrl = "../GTPT.ashx";

var vCaption = "积分兑换礼品规则定义";

function InitGrid() {
    vColumnNames = ['规则编号','规则名称'],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
        { name: 'sGZMC', width: 200, },
    ]

}
$(document).ready(function () {
  
})

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sGZMC", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
