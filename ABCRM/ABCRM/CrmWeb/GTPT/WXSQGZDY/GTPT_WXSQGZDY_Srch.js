vUrl = "../GTPT.ashx";
vCaption = "微信送券规则定义";
function InitGrid() {
    vColumnNames = ['记录编号', '规则名称', '规则简介'];
    vColumnModel = [
            { name: 'iJLBH', width: 80, },
            { name: 'sGZMC', width: 120, },
            { name: 'sNOTE', width: 200, },
    ]
}

$(document).ready(function () {

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sGZMC", "like", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}




