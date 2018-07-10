vUrl = "../YHQGL.ashx";
var djlx = IsNullValue(GetUrlParam("djlx"), "0");
switch (djlx) {
    case "0": vCaption = "优惠券发放规则"; break;
    case "1": vCaption = "前台礼品发放规则"; break;
    case "2": vCaption = "前台抽奖发放规则"; break;
}

function InitGrid() {
    vColumnNames = ['规则编号', '规则名称', '停用标记', '发放限额', '起点金额', '门店'];
    vColumnModel = [
        { name: 'iJLBH', index: 'iYHQFFGZID', width: 100 },
        { name: 'sYHQFFGZMC' },
        { name: 'iBJ_TY', width: 100, formatter: "checkbox" },
        { name: 'fFFXE', width: 100 },
        { name: 'fFFQDJE', width: 100 },
        { name: 'sMDMC', width: 120 },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZDM", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BJ_SF", "iBJ_SF", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sYHQFFGZMC", "like", true);
    MakeSrchCondition(arrayObj, "TB_FFXE", "fFFXE", "=", false);
    MakeSrchCondition(arrayObj, "TB_QDJE", "fFFQDJE", "=", false);
    MakeSrchCondition2(arrayObj, djlx, "iLX", "=", false);
    //MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};


