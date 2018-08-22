vUrl = "../LPGL.ashx";


function InitGrid() {
    vColumnNames = ["统计日期", "保管地点", "礼品名称", "礼品进价", "礼品单价", "礼品金额", "礼品积分", "礼品发放数量",'发放类型'  ];
    vColumnModel = [
            { name: 'dRQ', width: 80, },
            { name: 'sBGDDMC', width: 50 },
            { name: 'sLPMC', width: 80, },
			{ name: 'fLPJJ', width: 80, },
            { name: 'fLPDJ', width: 80, },
			{ name: 'fLPJE', width: 80, },
	        { name: 'fLPJF', width: 80, },
            { name: 'iLPSL', width: 80, },
            { name: 'sDJLX', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Insert").hide();
    $("#B_Update").hide();

    BFButtonClick("TB_BGDDMC", function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM",false);
    });

    BFButtonClick("TB_LPMC", function () {
        SelectLPXX("TB_LPMC", "HF_LPID", "zHF_LPID", false);
    });
    RefreshButtonSep();
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false); 
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_LPID", "iLPID", "in", false);
    MakeSrchCondition(arrayObj, "TB_FFRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_FFRQ2", "dRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};