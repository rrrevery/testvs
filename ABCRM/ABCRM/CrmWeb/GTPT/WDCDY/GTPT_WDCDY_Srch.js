vUrl = "../GTPT.ashx";
var vCaption = "微调查定义";
function InitGrid() {
    vColumnNames = ['记录编号', '开始日期', '结束日期', 'DJR', '登记人', '登记时间', 'SHR', '审核人', '审核日期', 'DQR', '启动人', '启动日期', 'ZZR', '终止人', '终止日期', '状态'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
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
			{ name: 'dQDSJ', width: 120, },
			{ name: 'iZZR', hidden: true, },
			{ name: 'sZZRMC', width: 80, },
			{ name: 'dZZRQ', width: 120, },
            { name: 'iSTATUS', hidden: true, },
    ];
}
$(document).ready(function () {

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_DQRMC").click(function () {
        SelectRYXX("TB_DQRMC", "HF_QDR", "zHF_QDR", false);
    });
    CheckBox("CB_STATUS", "HF_STATUS");

});


function MakeSearchCondition() {

    var arrayObj = new Array();
    var status = $("#HF_STATUS").val();
    switch (status) {
        case "0":
            MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "=", true);
            break;
        case "1":
            MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "=", true);
            break;
        case "2":
            MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "=", true);
            break;
        case "-1":
            MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "=", true);
            break;
    }

    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);


    MakeSrchCondition(arrayObj, "TB_KSRQ", "dKSRQ", "=", true);
    var a = $("#TB_KSRQ").val();
    MakeSrchCondition(arrayObj, "TB_JSRQ", "dJSRQ", "=", true);
    var b = $("#TB_JSRQ").val();
    MakeSrchCondition(arrayObj, "TB_DCZT", "sDCZT", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_SHRMC", "sSHRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_QDRMC", "sQDRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRMC", "sZZRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_SHRQ1", "dSHRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_SHRQ2", "dSHRQ", "<=", true);
    //MakeSrchCondition(arrayObj, "TB_ZZRQ1", "dZZRQ", ">=", true);
    //MakeSrchCondition(arrayObj, "TB_ZZRQ2", "dZZRQ", "<=", true);
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