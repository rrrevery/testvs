vUrl = "../GTPT.ashx";
vCaption = "微信标签定义";
function InitGrid() {
    vColumnNames = ['标签ID', '标签名称',  ],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
		{ name: 'sBQMC', width: 120, },
       
    ]

}
$(document).ready(function () {

})





function MakeSearchCondition() {
    var arrayObj = new Array();

    MakeSrchCondition(arrayObj, "TB_BQMC", "sBQMC", "=", true);
  
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}

