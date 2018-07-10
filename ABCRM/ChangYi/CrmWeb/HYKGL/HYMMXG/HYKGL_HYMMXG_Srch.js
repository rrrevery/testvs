vUrl = "../HYKGL.ashx";

function InitGrid() {
    vColumnNames = ['记录编号', '卡号', 'HYID', 'DJR', '登记人', '登记时间', ];
    vColumnModel = [
           { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150           
           { name: 'sHYK_NO',width:130, },
           { name: 'iHYID', hidden: true },
           { name: 'iDJR', hidden: true, },
		   { name: 'sDJRMC', width: 80, },
		   { name: 'dDJSJ', width: 120, },
    ];
}

$(document).ready(function () {
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });
    ConbinDataArry["iTYPE"] = TYPE;
    RefreshButtonSep();
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    //MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    //MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    //MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);  
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    //MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    //MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition2(arrayObj, TYPE, "iTYPE", "=", false);
    return arrayObj;
};