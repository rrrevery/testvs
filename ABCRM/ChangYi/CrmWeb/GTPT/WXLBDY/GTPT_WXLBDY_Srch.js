vUrl = "../GTPT.ashx";

var vCaption = "礼包定义";

function InitGrid() {
    vColumnNames = ['礼包ID', '礼包名称', ],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
		{ name: 'sLBMC', width: 120, },
    
    ]

}
$(document).ready(function () {



})
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
   
    MakeSrchCondition(arrayObj, "TB_LBMC", "sLBMC", "=", true);
    
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
