vUrl = "../MZKGL.ashx";


function InitGrid() {
    vColumnNames = ["卡号", "门店名称", "卡类型", "收款员代码", "处理时间", "处理类型", "记录编号", "借方金额", "贷方金额", "余额", "收款台号", "摘要"];
    vColumnModel = [
            { name: 'sHYKNO', width: 80, },//sortable默认为true width默认150
			{ name: 'sMDMC', width: 100, },
            { name: 'sHYKNAME', width: 100, },
            { name: 'sSKYDM', width: 80, },
            { name: 'dCLSJ', width: 150, },
            { name: 'sCLLX', width: 80, },
            { name: 'iJLBH', width: 80, },
            { name: 'fJFJE', width: 80, },
            { name: 'fDFJE', width: 80, },
            { name: 'fYE', width: 80, },
            { name: 'sSKTNO', width: 80, },
            { name: 'sZY', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });


    BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });


    RefreshButtonSep();

});


function DoSearch() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO1", "sHYKNO", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO2", "sHYKNO", "<=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_SKTNO", "sSKTNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_JFJE", "fJFJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_DFJE", "fDFJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZHYE", "fYE", "=", false);
    // MakeSrchCondition(arrayObj, "DDL_CLLX", "iCLLX", "=", false);   
    MakeSrchCondition(arrayObj, "TB_CLRQ1", "dCLSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CLRQ2", "dCLSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);

    MakeMoreSrchCondition(arrayObj);
    $("#list").jqGrid('setGridParam', {
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        postData: { 'afterFirst': JSON.stringify(arrayObj) },
        page: 1
    }).trigger("reloadGrid");
    location.hash = "tab2-tab";
};