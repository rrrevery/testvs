vUrl = "../GTPT.ashx";
vCaption = "微信会员权益定义";
function InitGrid() {
    vColumnNames = ['记录编号', '名称', '描述', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            { name: 'sNAME', width: 120, },
            { name: 'sHEAD', width: 120, },
    ];
}
$(document).ready(function () {
  
});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_NAME", "sNAME", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}





