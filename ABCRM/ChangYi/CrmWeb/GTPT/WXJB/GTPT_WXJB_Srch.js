vUrl = "../GTPT.ashx";
vCaption = "微信解绑";
function InitGrid() {
    vColumnNames = ['记录编号', '会员ID', '会员号', '卡类型', 'iHYKTYPE', '微信号', '变动时间', '登记人', '登记时间',],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
		{ name: 'iHYID', width: 120, hidden: true, },
        { name: 'sHYK_NO', width: 120 },
        { name: 'sHYKNAME', width: 80, },
	    { name: 'iHYKTYPE', hidden: true, },
        { name: 'sOPENID', width: 120 },

        		{ name: 'dBDSJ', width: 120, },
                { name: 'sDJRMC', width: 80, },
		{ name: 'dDJSJ', width: 120, },
     
    ]

}
$(document).ready(function () {

    $("#B_Exec").hide();


    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, vCZK);
    });
})



function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    //MakeSrchCondition(arrayObj, "TB_HYID", "iHYID", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_OPENID", "sOPENID", "=", true);

    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", false);
    //MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " like", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);

    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
    //MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " like", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
