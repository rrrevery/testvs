vUrl = "../HYKGL.ashx";
vCaption = "组合标签定义";

function InitGrid() {
    vColumnNames = ['标签值','标签名', '条件数量', '统计月份', '登记人', '登记人', '登记时间',];
    vColumnModel = [
			{ name: 'iJLBH', width: 80, },
            { name: 'sLABEL_VALUE', width: 150, },
			{ name: 'iTJSL', width: 120, },
            { name: 'iTJYF', },
			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
		
    ];
}

$(document).ready(function () {
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });

    BFButtonClick("TB_HYBQ", function () {
        SelectHYBQ("TB_HYBQ", "HF_HYBQ", "zHF_HYBQ", false);
    });

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_HYBQ", "iJLBH", "in", false);
    MakeSrchCondition(arrayObj, "TB_TJYF", "iTJYF", "=", false);
    MakeSrchCondition(arrayObj, "TB_TJSL", "iTJSL", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
