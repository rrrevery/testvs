vUrl = "../HYKGL.ashx";

function InitGrid() {
    vColumnNames = ['日期', '门店代码', '门店名称', '上期金额', '存款金额', '取款金额', '并卡金额', '消费金额', '调整金额', '期末金额', ];
    vColumnModel = [
            { name: 'dRQ', width: 80, },
            { name: 'sMDDM',hidden:true, },
            { name: 'sMDMC', width: 80, },
			{ name: 'fSQYE', width: 80, },
			{ name: 'fCKJE', },
            { name: 'fQKJE', width: 80, },
			{ name: 'fBKJE', width: 80, },
			{ name: 'fXFJE', width: 80, },
            { name: 'fTZJE', width: 80, },
			{ name: 'fQMYE', width: 80, },
    ];
}



$(document).ready(function () {

    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

    $("#TB_YHQMC").click(function () {

        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    })
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

    RefreshButtonSep();
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "TB_CXRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CXRQ2", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

