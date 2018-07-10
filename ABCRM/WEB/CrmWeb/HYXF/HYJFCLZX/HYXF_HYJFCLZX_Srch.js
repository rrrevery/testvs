vUrl = "../HYXF.ashx";
vJFLX = IsNullValue(GetUrlParam("JFLX"), "1");
vCaption = "积分返利执行" + (vJFLX == 1 ? "(当前)" : "(往年)");
function InitGrid() {
    vColumnNames = ['记录编号', '操作门店', '处理日期', '处理积分', '返利金额', '原积分', '登记人', '登记时间', '冲正人', '冲正日期'];
    vColumnModel = [
            { name: 'iJLBH' },
            { name: 'sMDMC', },
            { name: 'dCLRQ', hidden: true },
            { name: 'fCLJF', },
            { name: 'fFQJE' },
            { name: 'fWCLJF_OLD', },
            { name: 'sDJRMC' },
            { name: 'dDJSJ', },
            { name: 'sCZRMC' },
            { name: 'dCZRQ', },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Update").hide();
    ConbinDataArry["JFLX"] = vJFLX;


    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    $("#TB_CZRMC").click(function () {
        SelectRYXX("TB_CZRMC", "HF_CZR", "zHF_CZR", false);
    });

    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM",false);
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

    RefreshButtonSep();
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", " in", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dSHRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dSHRQ", "<=", true);
    MakeSrchCondition(arrayObj, "DDL_CZLX", "iCZJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_CZR", "iCZR", "in", false);
    MakeSrchCondition(arrayObj, "TB_CZRQ1", "dCZRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CZRQ2", "dCZRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);   
    return arrayObj;
};
