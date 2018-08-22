vUrl = "../GTPT.ashx";


function InitGrid() {
    vColumnNames = ["记录编号", "微信号", "会员卡号", "YHQ", "优惠券名称", "金额", '规则名称', '门店名称', '会员卡类型', "优惠券结束日期", "登记时间","公共号名称"];
    vColumnModel = [
            { name: 'iJLBH', width: 80, index: 'iJLBH', },
            { name: 'sWX_NO', width: 80, },
            { name: 'sHYK_NO', width: 80, },
            { name: 'iYHQID', hidden: true },
            { name: 'sYHQMC', width: 80, },
            { name: 'fJE', width: 80, },
            { name: 'sGZMC', width: 80, },
            { name: 'sMDMC', width: 80, },
            { name: 'sHYKNAME', width: 80, },
            { name: 'dSYJSRQ', width: 150, },
            { name: 'dDJSJ', width: 150, },
            { name: 'sPUBLICNAME', width: 150, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    BFButtonClick("TB_YHQMC", function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID",false);
    });

    BFButtonClick("TB_GZMC", function () {
        SelectWXYHQGZ("TB_GZMC", "HF_GZID", "zHF_GZID", false);
    });

    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID",false);
    });
    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE");
    });
    RefreshButtonSep();
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "TB_WX_NO", "sWX_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "HF_GZID", "iGZID", "in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};