vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
function InitGrid() {
    vColumnNames = ["卡号",  "门店名称",  "处理时间", "处理类型", "记录编号", "借方金额", "贷方金额", "余额", "收款台号","收款员代码","摘要" ];
    vColumnModel = [
            { name: 'sHYKNO', width: 80, },//sortable默认为true width默认150
			{ name: 'sMDMC', width: 80, },		         
            { name: 'dCLSJ', width: 80, },
            { name: 'sCLLX', width: 80, },
            { name: 'iJLBH', width: 80, },
            { name: 'fJFJE', width: 60, },
            { name: 'fDFJE', width: 80, },
            { name: 'fYE', width: 80, },
            { name: 'sSKTNO', width: 80, },
            { name: 'sSKYDM', width: 80, },
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
    })
  
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO1", "sHYKNO", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO2", "sHYKNO", "<=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_SKTNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_JFJE", "fJFJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_DFJE", "fDFJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZHYE", "fYE", "=", false);
    MakeSrchCondition(arrayObj, "DDL_CLLX", "iCLLX", "=", false);   
    MakeSrchCondition(arrayObj, "TB_CLRQ1", "dCLSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CLRQ2", "dCLSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};