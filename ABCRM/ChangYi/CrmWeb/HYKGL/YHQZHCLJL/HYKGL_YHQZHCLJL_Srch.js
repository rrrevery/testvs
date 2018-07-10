vUrl = "../HYKGL.ashx";
vCaption = "优惠券账户处理记录查询";

function InitGrid() {
    vColumnNames = ["卡号", "优惠券", "截止日期", "操作门店","促销活动编号", "促销主题", "商户代码", "处理时间", "处理类型", "记录编号", "借方金额", "贷方金额", "余额", "收款台号","摘要"  ];
    vColumnModel = [
            { name: 'sHYKNO', width: 80, },//sortable默认为true width默认150
			{ name: 'sYHQMC', width: 80, },
			{ name: 'dJZRQ', },
            { name: 'sMDMC', width:80},
	        { name: 'iCXHDID', hidden: true, },
            { name: 'sCXZT', width: 80, },
            { name: 'sSHDM', width: 80, },
            { name: 'dCLSJ', width: 80, },
            { name: 'sCLLX', width: 80, },
            { name: 'iJLBH', width: 80, },
            { name: 'fJFJE', width: 60,  },
            { name: 'fDFJE', width: 80, },
            { name: 'fYE', width: 80, },
            { name: 'sSKTNO', width: 80, hidden: true, },
            { name: 'sZY', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    });
   
    $("#TB_CXZT").click(function () {
        SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", false);
    });
});

//function IsValidSearch() {
//    if ($("#HF_MDID").val() == "" && $("#TB_HYKNO").val() == "") {
//        ShowMessage("请先选择操作门店或者输入会员卡号", 3);
//        return;
//    }
//    return true;
//}

function MakeSearchCondition(){
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iHYQID", "in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_SKTNO", "sHYKNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_JFJE", "fJFJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_DFJE", "fDFJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZHYE", "fYE", "=", false);
    MakeSrchCondition(arrayObj, "DDL_CLLX", "iCLLX", "=", false);
    MakeSrchCondition(arrayObj, "HF_CXID", "iCXID", "in", false);
    MakeSrchCondition(arrayObj, "TB_JZRQ", "dJZRQ", "=", true);
    MakeSrchCondition(arrayObj, "TB_CLRQ1", "dCLSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CLRQ2", "dCLSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};