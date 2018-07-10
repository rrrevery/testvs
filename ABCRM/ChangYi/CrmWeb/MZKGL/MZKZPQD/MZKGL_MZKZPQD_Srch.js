vUrl = "../MZKGL.ashx";
vCaption = "面值卡支票启动";

function InitGrid() {
    vColumnNames = ["记录编号", "到账银行", "付款人名称", "支票金额", "到账日期", "门店名称", "支票状态", "DJR", "登记人", "登记时间", "ZXR", "审核人", "审核日期", ];
    vColumnModel = [
           { name: "iJLBH", width: 80, },
            { name: "sDZYH", width: 80, },
            { name: "sFKRMC", width: 80, },
            { name: "fJE", width: 80, },
            { name: "dDZRQ", width: 80, },
            { name: "sMDMC", width: 80, },
            {
                name: "iSTATUS", width: 80, formatter: function (cellvalues) {
                    if (cellvalues == 0)
                        return "未启动";
                    if (cellvalues == 1)
                        return "已启动";
                }
            },
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


});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_DZYH", "sDZYH", "=", true);
    MakeSrchCondition(arrayObj, "TB_FKRMC", "sFKRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZPJE", "fJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_DZRQ", "dDZRQ", "=", true);
    MakeSrchCondition(arrayObj, "TB_SYJE", "fSYJE", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
