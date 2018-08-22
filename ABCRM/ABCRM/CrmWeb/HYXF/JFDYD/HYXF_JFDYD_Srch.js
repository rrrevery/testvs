vUrl = "../HYXF.ashx";
vCaption = "积分定义单";

function InitGrid() {
    vColumnNames = ["单据号", "商户代码", "部门代码", "部门名称", "促销活动", "是否优先执行", "开始时间", "结束时间", "单据状态", "特定积分单", "DJR", "登记人", "登记时间", "ZXR", "审核人", "审核时间", "QDR", "启动人", "启动时间", "ZZR", "终止人", "终止时间"],
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 85, },
            { name: 'sSHDM', width: 55, },
            { name: 'sSHBMDM', width: 55, },
            { name: 'sBMMC', width: 90, },
            { name: 'iCXID', width: 45, hidden: true },
            { name: 'iYXCLBJ', width: 55, formatter: BoolCellFormat, },
            { name: 'dRQ1', width: 60, },
            { name: 'dRQ2', width: 60, },
            { name: 'iSTATUS', width: 55, formatter: DJZTCellFormat, },
            { name: 'iBJ_JFBS', width: 55, formatter: BoolCellFormat, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 70, },
			{ name: 'dDJSJ', width: 135, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 70, },
			{ name: 'dZXRQ', width: 135, },
            { name: 'iQDR', hidden: true, },
			{ name: 'sQDRMC', width: 70, },
			{ name: 'dQDSJ', width: 135, },
			{ name: 'iZZR', hidden: true, },
			{ name: 'sZZRMC', width: 70, },
			{ name: 'dZZRQ', width: 135, },
    ]
}

$(document).ready(function () {
    FillSH($("#S_SH"));

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_QDRMC").click(function () {
        SelectRYXX("TB_QDRMC", "HF_QDR", "zHF_QDR", false);
    });
    $("#TB_ZZRMC").click(function () {
        SelectRYXX("TB_ZZRMC", "HF_ZZR", "zHF_ZZR", false);
    });
});

function selComSH(record) {
    if (record.value != "")
        FillSHBMTreeBase("TreeSHBM", "TB_BMMC", record.value, 4);
}

function TreeNodeClickCustom(e, treeId, treeNode) {
    $("#HF_BMDM").val(treeNode.id);
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "S_SH", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "HF_BMDM", "sBMDM", "like", true);
    MakeSrchCondition(arrayObj, "TB_RQ11", "dRQ1", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ12", "dRQ1", "<=", true);
    MakeSrchCondition(arrayObj, "TB_RQ21", "dRQ2", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ22", "dRQ2", "<=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_QDR", "iQDR", " =", false);
    MakeSrchCondition(arrayObj, "TB_QDSJ1", "dQDSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ2", "dQDSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZZR", "iZZR", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZZRQ1", "dZZRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRQ2", "dZZRQ", "<=", true);
    var djzt = $("[name='djzt']:checked").val();
    if (djzt != 10)
        MakeSrchCondition2(arrayObj, djzt, "iSTATUS", "=", false);
    return arrayObj;
}

function InsertClick() {
    //T --添加事件
    var sInsertCurrentPath = "";
    sInsertCurrentPath = sCurrentPath + "?1=1";
    sInsertCurrentPath = ConbinPath(sInsertCurrentPath);
    MakeNewTab(sInsertCurrentPath, vCaption, vPageMsgID);
};