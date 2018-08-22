vUrl = "../HYKGL.ashx";
vCaption = "优惠券批量存款"

function InitGrid() {
    vColumnNames = ['记录编号', '操作门店', '优惠券', 'iYHQID', '结束日期', '促销活动', '存款卡数', '总存款金额', 'iCXID', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', '摘要', ];
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            { name: 'sMDMC', },
            { name: 'sYHQMC', },
            { name: 'iYHQID', hidden: true, },
            { name: 'dJSRQ', width: 120, },
            { name: 'sCXZT', },
            { name: 'iBHKS', width: 50 },
            { name: 'fZCKJE', width: 80 },
            { name: 'iCXID', hidden: true, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			{ name: 'sZY', width: 120, },
    ];
}



$(document).ready(function () {
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, "", "", "", 1);
    });

    $("#TB_YHQ").click(function () {
        SelectYHQ("TB_YHQ", "HF_YHQ", "zHF_YHQ", false)
    });

    $("#TB_CXHD").click(function () {
        SelectCXHD("TB_CXHD", "HF_CXHD", "zTB_CXHD", false, "");
    });
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

});




function AddCustomerCondition(Obj) {
    Obj.sHYKNO = $("#TB_HYKNO").val();
}


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_YHQ", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "TB_JSRQ", "dJSRQ", "=", true);
    MakeSrchCondition(arrayObj, "HF_CXHD", "iCXID", "in", false);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
