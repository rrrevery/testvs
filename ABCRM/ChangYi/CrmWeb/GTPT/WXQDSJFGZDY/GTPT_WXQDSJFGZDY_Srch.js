vUrl = "../GTPT.ashx";

vCaption = "微信签到送积分积分规则定义";

function InitGrid() {
    vColumnNames = ['记录编号', '开始日期', '结束日期', 'DJR', '登记人', '登记时间', 'ZXR', '执行人', '审核日期', 'DQR', '启动人', '启动日期', 'ZZR', '终止人', '终止日期', '状态'],
    vColumnModel = [   
            { name: 'iJLBH', width: 80, },
            { name: 'dKSRQ', width: 120, },
            { name: 'dJSRQ', width: 120, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
            { name: 'iQDR', hidden: true, },
			{ name: 'sQDRMC', width: 80, },
			{ name: 'dQDRQ', width: 120, },
			{ name: 'iZZR', hidden: true, },
			{ name: 'sZZRMC', width: 80, },
			{ name: 'dZZRQ', width: 120, },
             {
                 name: 'iSTATUS', width: 80, formatter: function (cellvalue, icol) {
                     switch (cellvalue) {
                         case 0:
                             return "保存";
                             break;
                         case 1:
                             return "审核";
                             break;
                         case 2:
                             return "启动";
                             break;
                         case 3:
                             return "终止";
                             break;
                     }
                 }
             },

    ]

}
$(document).ready(function () {
   
    CheckBox("CB_STATUS", "HF_STATUS");

    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    BFButtonClick("TB_DQRMC", function () {
        SelectRYXX("TB_DQRMC", "HF_QDR", "zHF_QDR", false);
    });
    BFButtonClick("TB_ZZRMC", function () {
        SelectRYXX("TB_ZZRMC", "HF_ZZR", "zHF_ZZR", false);
    });
});


function MakeSearchCondition() {
    var arrayObj = new Array();  
    MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "=", true);
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ", "dKSRQ", "=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ", "dJSRQ", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_QDRMC", "sQDRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRMC", "sZZRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRQ1", "dZZRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRQ2", "dZZRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ1", "dQDRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ2", "dQDRQ", "<=", true);

    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}



//复选框控制
function CheckBox(cbname, hfname) {
    $("input[type='checkbox'][name='" + cbname + "']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#" + hfname).val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#" + hfname).val("");
        }
    });

}
