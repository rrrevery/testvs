vUrl = "../GTPT.ashx";
var vCaption = "微调查内容定义";
function InitGrid() {
    vColumnNames = ['记录编号', '名称', '备注', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            { name: 'sMC', width: 120, },
            { name: 'sBZ', width: 120, },
    ];
}
$(document).ready(function () {

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}





