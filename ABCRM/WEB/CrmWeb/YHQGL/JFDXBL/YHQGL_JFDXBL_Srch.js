vUrl = "../YHQGL.ashx";
vCaption = "卡类型积分低限比例";

function InitGrid() {
    vColumnNames = ['记录编号', '门店名称', '卡类型', 'HYKTYPE', '开始日期', '结束日期', '状态', 'iDJR', '登记人', '登记时间', 'iZXR', '审核人', '审核日期', 'iZZR', '终止人', '终止时间', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            { name: 'sMDMC', width: 80, },
			{ name: 'sHYKNAME', width: 80, },
			{ name: 'iHYKTYPE', hidden: true, },
            { name: 'dKSRQ', width: 120, },
            { name: 'dJSRQ', width: 120, },
            {
                name: 'iSTATUS', width: 80, formatter: function (cellvalue) {
                    if (cellvalue == "0") {
                        return "未审核";
                    }
                    else if (cellvalue == "1") {
                        return "已审核";
                    }
                    else if (cellvalue == "3") {
                        return "已停用";
                    }
                    else
                        return "";
                }
            },
			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
            { name: 'iZZR', hidden: true, },
			{ name: 'sZZRMC', width: 80, },
			{ name: 'dZZRQ', width: 120, },
    ];
}

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

    //卡类型弹出框 
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

    $("input[type='checkbox']").click(function () {
        var ele = $(this);
        var name = ele.attr("name");
        ele.prop("checked", this.checked);
        if (this.checked) {
            ele.siblings("[name='" + name + "']").
            prop("checked", !this.checked);
        }
        var hf = "#" + name.replace("CB", "HF");
        $(hf).val(this.checked ? ele.val() : "");

    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_BJ_TY", "iBJ_TY", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    switch ($("[name='djzt']:checked").val()) {
        case "1": MakeSrchCondition2(arrayObj, "not null", "iZZR", "is", false); break;
        case "2": MakeSrchCondition2(arrayObj, "null", "iZZR", "is", false); break;
    }
    return arrayObj;
};
