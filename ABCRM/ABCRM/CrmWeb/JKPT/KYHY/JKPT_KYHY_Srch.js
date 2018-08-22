vUrl = "../JKPT.ashx";
vCaption = "可疑会员";
function InitGrid() {
    vColumnNames = ['记录编号', '会员卡号码', '会员卡类型', '会员姓名', '性别', '手机号码', '可疑消费日期', '可疑消费年月', '状态', '状态', 'iBJ_KY', '新预警状态', '登记人名称', '登记时间', 'ZXR', '审核人', '审核日期'];
    vColumnModel = [
            { name: 'iJLBH', width: 55, },
			{ name: 'sHYK_NO', width: 100, },
            { name: 'sHYKNAME', width: 100, },
			{ name: 'sHY_NAME', width: 80 },
            { name: 'sSEX', width: 50 },
            { name: 'sSJHM', width: 100, },
            { name: 'dXFRQ', width: 80, },
            { name: 'iXFNY', width: 80, },
            { name: 'iSTATUS', width: 100, hidden: true },
			{ name: 'sSTATUS', width: 100, },
            { name: 'iBJ_KY', width: 100, hidden: true },
			{ name: 'sBJ_KY', width: 100, },
			//{ name: 'iBJ_CL', width: 50 },
            { name: 'sDJRMC', width: 80 },
            { name: 'dDJSJ', width: 150 },
            { name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
    ];
}



$(document).ready(function () {
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_XFRQ", "dXFRQ", "=", true);
    MakeSrchCondition(arrayObj, "TB_XFNY", "iXFNY", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_BZ", "sBZ", "like", true);
    var vDJZT = $("[name='djzt']:checked").val();
    if (vDJZT == 1) {
        MakeSrchCondition2(arrayObj, "null", "iZXR", "is", false);
    }

    if (vDJZT == 2) {
        MakeSrchCondition2(arrayObj, " not null", "iZXR", "is", false);
    }
    //if ($("#TB_HYKNO").val() != "") {
    //    MakeSrchCondition2(arrayObj, tp_condition + "'" + $("#TB_HYKNO").val() + "'" + tp_conditionOne + "'" + $("#TB_HYKNO").val() + "')", "sHYK_NO", "=", false);
    //}

    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.sHYK_NO = $("#TB_HYKNO").val();
}