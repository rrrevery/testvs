
vUrl = "../YHQGL.ashx";
vCaption = "优惠券账户当前余额查询";

function InitGrid() {
    vColumnNames = ["会员卡号", "姓名", "卡类型", "优惠券名称", "结束日期", "商户代码", "促销主题", "活动编号", "优惠券金额",  ];
    vColumnModel = [
            { name: 'sHYK_NO', width: 80, },//sortable默认为true width默认150
			{ name: 'sHY_NAME', width: 80, },
			{ name: 'sHYKNAME', },
	        { name: 'sYHQMC', },
            { name: 'dJSRQ', width: 80, },
            { name: 'sSHDM', width: 80, },
            { name: 'sCXZT', width: 80, },
            { name: 'iCXHDBH', width: 80, },           
            { name: 'fJE', width: 80, },           
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE");
    })

    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    });

    $("#TB_CXZT").click(function () {
        SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", false);
    })

    $("input[name='cld']").click(function () {
        if (this.checked) {
            var fs = $(this).val();
            $("#HF_HYKStatus").val(fs);
        }
    });

});


function MakeSearchCondition() {
    var arrayObj = new Array();

    MakeSrchCondition(arrayObj, "TB_HYKNO1", "sHYK_NO", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO2", "sHYK_NO", "<=", true);
    MakeSrchCondition(arrayObj, "TB_YE1", "fJE", ">=", false);
    MakeSrchCondition(arrayObj, "TB_YE2", "fJE", "<=", false);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_CXID", "iCXID", "in", false);
    if (parseInt($("#HF_HYKStatus").val()) == 1) {
        $("#HF_HYKStatus").val(0);
        MakeSrchCondition(arrayObj, "HF_HYKStatus", "iStatus", ">=", false);
    }
    if (parseInt($("#HF_HYKStatus").val()) == -4) {
        MakeSrchCondition(arrayObj, "HF_HYKStatus", "iStatus", "=", false);
    }
    if (parseInt($("#HF_HYKStatus").val()) == 3) {
        $("#HF_HYKStatus").val(0);
        MakeSrchCondition(arrayObj, "HF_HYKStatus", "iStatus", "<", false);
    }

    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};