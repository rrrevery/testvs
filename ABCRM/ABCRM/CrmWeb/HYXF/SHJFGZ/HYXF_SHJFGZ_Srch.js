vUrl = "../HYXF.ashx";
vCaption = "商户积分规则";

function InitGrid() {
    vColumnNames = ['编号', '名称', '积分', '金额']
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 150, },
			{ name: 'sMC', width: 150, },
			{ name: 'fJF', width: 150, },
            { name: 'fJE', width: 150, },
]
};




$(document).ready(function () {



});




function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_MC", "sMC", "like", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};



