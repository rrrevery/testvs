vUrl = "../HYXF.ashx";
vCaption = "积分变动单";
function InitGrid() {
    vColumnNames = ['记录编号', '操作门店', '调整兑奖积分', '调整升级积分', '调整本年积分', '调整累计积分', '变动积分数', '包含卡数', '摘要', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sMDMC', width: 120, },
            { name: 'iBJ_CLWCLJF', width: 120, formatter: "checkbox", },
            { name: 'iBJ_CLBQJF', width: 120, formatter: "checkbox", },
            { name: 'iBJ_CLBNLJJF', width: 120, formatter: "checkbox", },
            { name: 'iBJ_CLLJJF', width: 120, formatter: "checkbox", },
            { name: 'fZWCLJF', width: 100 },
            { name: 'iBHKS', width: 50 },
			{ name: 'sZY', width: 120, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
    ];
};

$(document).ready(function () {
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.sHYK_NO = $("#TB_BHKH").val();
};





