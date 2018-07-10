vUrl = "../GTPT.ashx";
var vCaption = "投诉处理";

function InitGrid() {
    vColumnNames = ['记录编号', '投诉日期', '顾客姓名', 'TSLX', '投诉内容', 'STATUS', '单据状态', '会员卡号', '门店名称', 'DJR', '登记人', '登记时间', 'SHR', '处理人', '处理日期', 'HFR', '回访日期', 'HFJG', '回访结果'];
    vColumnModel = [
           { name: 'iJLBH', index: 'iJLBH', width: 80, },
            { name: 'dTSRQ', width: 120, },
            { name: 'sGKXM', width: 120, },
            { name: 'iTSLX', width: 120, hidden: true },
            { name: 'sTSNR', width: 120, },
            { name: 'iSTATUS', width: 120, hidden: true },
            { name: 'sSTATUSMC', width: 120, },
            { name: 'sHYK_NO', width: 120, },
            { name: 'sMDMC', width: 80, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
            { name: 'iHFR', hidden: true, },
			{ name: 'dHFRQ', width: 120, },
             { name: 'iHFJG', width: 120, hidden: true },
             { name: 'sHFJGMC', width: 120, },
    ];
}
$(document).ready(function () {
    $("#B_Update").text("处理");
    BFButtonClick("TB_MDMC", function () {
        SelectWXMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    $("#B_Insert").hide();
    CheckBox("CB_STATUS", "HF_STATUS");
    RefreshButtonSep();
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "=", true);
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "=", false);
    MakeSrchCondition(arrayObj, "Select1", "iTSLX", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);  
    MakeSrchCondition(arrayObj, "TB_HFRQ1", "dHFRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HFRQ2", "dHFRQ", "<=", true);

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
