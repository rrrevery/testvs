vUrl = "../HYXF.ashx";
vCaption = "补积分";

function InitGrid() {
    vColumnNames = ['记录编号', '规则名称', '金额', '积分', '卡号', '登记人', 'iDJR', '登记时间', '审核人', 'iZXR', '审核日期'];
    vColumnModel = [
               { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
               { name: 'sMC', width: 120, },
               { name: "fJE", width: 80, },
               { name: "fJF", width: 80, },
               { name: "sHYK_NO", width: 120, },
               { name: 'sDJRMC', width: 80, },
               { name: 'iDJR', width: 80, hidden: true },
               { name: 'dDJSJ', width: 120, },
               { name: 'sZXRMC', width: 80, },
               { name: 'iZXR', width: 80, hidden: true },
               { name: 'dZXRQ', width: 120, },
    ];
};




$(document).ready(function () {
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });






});




function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", false); 
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXSJ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXSJ1", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};



