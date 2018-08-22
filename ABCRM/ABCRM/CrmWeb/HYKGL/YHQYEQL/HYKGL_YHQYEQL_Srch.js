vUrl = "../HYKGL.ashx";
vCaption = "优惠券余额清零"

function InitGrid() {
    vColumnNames = ['记录编号', '卡类型', 'HYKTYPE', '操作门店', 'sBGDDDM', '优惠券', 'YHQID', '调整金额', '调整数量', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期', '摘要', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },
			{ name: 'sHYKNAME', width: 80, },
            { name: 'iHYKTYPE', hidden: true, },
            { name: 'sMDMC', width: 80, },
            { name: 'sBGDDDM', hidden: true, },
            { name: 'sYHQMC', width: 80, },
			{ name: 'iYHQID', hidden: true, },
            { name: 'fTZJE', width: 80, },
            { name: 'iTZSL', width: 80, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			{ name: 'sZY', width: 120, },
    ];
};

$(document).ready(function () {
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
    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    });
   
});



function AddCustomerCondition(Obj) {
    Obj.sHYKNO = $("#TB_HYKNO").val();
}
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
