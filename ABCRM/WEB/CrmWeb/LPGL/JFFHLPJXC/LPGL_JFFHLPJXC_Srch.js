vUrl = "../LPGL.ashx";
vCaption = "积分返还礼品进销存查询";

function InitGrid() {
    vColumnNames = ['日期', '礼品代码', '礼品名称', '门店名称', '期初数量', '进货数量', '拨入数量', '拨出数量', '发放数量', '作废数量', '使用数量', '退货数量', '调整数量', '结存数量'],
    vColumnModel = [
            { name: 'dRQ' },
            { name: 'sLPDM', },
            { name: 'sLPMC', },
            { name: 'sMDMC', },
            { name: 'fQCSL', },
            { name: 'fJHSL', },
            { name: 'fBRSL', },
            { name: 'fBCSL', },
            { name: 'fFFSL', },
            { name: 'fZFSL', },
            { name: 'fSYSL', },
            { name: 'fTHSL', },
            { name: 'fZTSL', },
            { name: 'fJCSL', },
    ]
}

$(document).ready(function () {

    $("#B_Insert").hide();
    $("#B_Update").hide();

    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM",false);
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    RefreshButtonSep();
});

function IsValidSearch() {
    //if ($("#HF_MDID").val() == "") {
    //    ShowMessage("请选择门店", 3);
    //    return;
    //}
    //if ($("#TB_RQ1").val() == "" || $("#TB_RQ2").val() == "") {
    //    ShowMessage("请选择汇总日期", 3);
    //    return;
    //}
    return true;
}
function MakeSearchCondition(){
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LPDM", "sLPDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_LPMC", "sLPMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_QCSL", "fQCSL", "=", false);
    MakeSrchCondition(arrayObj, "TB_JHSL", "fJHSL", "=", false);
    MakeSrchCondition(arrayObj, "TB_BRSL", "fBRSL", "=", false);
    MakeSrchCondition(arrayObj, "TB_BCSL", "fBCSL", "=", true);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};