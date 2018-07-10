vUrl = "../LPGL.ashx";
vCaption = "礼品定义";

function InitGrid() {
    vColumnNames = ['礼品代码', '礼品ID', '礼品名称', '礼品规格', '礼品单价', '礼品进价', '礼品国际码', '扣减积分', 'shsbid', '商标', 'LPFLid', '礼品分类', "登记人", "登记时间", "修改人", "修改时间"],
    vColumnModel = [
        { name: 'sLPDM', width: 80, },
        { name: 'iJLBH', hidden: true, },
        { name: 'sLPMC', width: 120, },
        { name: 'sLPGG', width: 80, },
        { name: 'fLPDJ', width: 80, },
        { name: 'fLPJJ', width: 80, },
        { name: 'sGJBM', width: 80, },
        { name: 'fLPJF', width: 80, },
        { name: 'iSHSBID', hidden: true, },
        { name: 'sSBMC', hidden: true, },
        { name: 'iLPFLID', hidden: true, },
        { name: 'sLPFLMC', width: 80, },
        { name: 'sDJRMC', width: 100, },
        { name: 'dDJSJ', width: 150 },
        { name: 'sXGRMC', width: 100, },
        { name: 'dXGSJ', width: 150 }
    ]
}

$(document).ready(function () {
    //$("#B_Exec").hide();
    //$("#B_Update").hide();

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_XGRMC").click(function () {
        SelectRYXX("TB_XGRMC", "HF_XGR", "zHF_XGR", false);
    });

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LPDM", "sLPDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_LPMC", "sLPMC", "like", true);
    MakeSrchCondition(arrayObj, "TB_LPGG", "sLPGG", "=", true);
    MakeSrchCondition(arrayObj, "TB_LPDJ", "fLPDJ", "=", false);
    MakeSrchCondition(arrayObj, "TB_LPJJ", "fLPJJ", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_XGRMC", "sXGRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_XGSJ1", "dXGSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_XGSJ2", "dXGSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_SHSBID", "iSHSBID", "in", false);
    MakeSrchCondition(arrayObj, "TB_LPGJM", "sGJBM", "=", true);
    return arrayObj;
}

