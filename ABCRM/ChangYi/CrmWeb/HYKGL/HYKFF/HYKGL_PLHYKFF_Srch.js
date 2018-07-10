vUrl = "../HYKGL.ashx";
vCaption = "批量会员卡发放";
function InitGrid() {
    vColumnNames = ['记录编号', '方式', 'mdid', 'sBGDDDM', '保管地点', '售卡数量', '应收总额', '实收金额', '有效期', '摘要', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            {
                name: 'iFS', width: 50, hidden: true, formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 0) { return "单张"; }
                    if (cellvalue == 1) { return "批量"; }
                },
            },
            { name: 'iMDID', hidden: true, },
            { name: 'sBGDDDM', hidden: true, },
            { name: 'sBGDDMC', },
            { name: 'iSKSL', width: 60, },
            { name: 'fYSZE', width: 60, },
            { name: 'fSSJE', width: 60, },
            { name: 'dYXQ', hidden: true, },
			{ name: 'sZY', width: 120, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
    ];
}

$(document).ready(function () {
    $("#B_Exec").hide();

    $("#TB_BGDDDM").click(function () {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM");
    });
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", true);
    MakeSrchCondition(arrayObj, "TB_SKSL", "iSKSL", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", " in", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", true);
    MakeSrchCondition(arrayObj, "HF_FS", "iFS", " =", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
