var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillSH($("#cbSH"));
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#cbSH").val(), "", "", "", true);
    });
    $("#TB_RQ1").focus(function () {
        WdatePicker({ isShowWeek: true, onpicked: datechange1($("#TB_RQ1").val()) });
    });
    $("#TB_RQ2").focus(function () {
        WdatePicker({ isShowWeek: true, onpicked: datechange2($("#TB_RQ2").val()) });
    });
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=SQXFFX.cpt";
    var rq1 = new Date($("#TB_RQ1").val());
    var rq2 = new Date($("#TB_RQ2").val());
    var tq1 = new Date($("#TB_TQ1").val());
    var tq2 = new Date($("#TB_TQ2").val());

    var srq1 = GetDateString(rq1, "0");
    var srq2 = GetDateString(rq2, "0");
    var stq1 = GetDateString(tq1, "0");
    var stq2 = GetDateString(tq2, "0");

    if ($("#cbSH").val() != "") {
        addr += "&SHDM=" + $("#cbSH").val();
    }
    else {
        ShowMessage("请选择商户");
        return;
    }
    var mdid = $("#HF_MDID").val();
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    else
        addr += "&MDID=" + mdid;
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

function SHChange() {
    $("#TB_MDMC").val("");
    $("#HF_MDID").val("");
    $("#zHF_MDID").val("");
}
function datechange1(date) {
    if (date!="") {
        var rq = new Date($("#TB_RQ1").val());
        var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
        var stq = getDate(tq);


        $("#TB_TQ1").val(stq);

    }
}
function datechange2(date) {
    if (date != "") {
        var rq = new Date($("#TB_RQ2").val());
        var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
        var stq = getDate(tq);

        $("#TB_TQ2").val(stq);

    }
}

function getDate(date) {
    var currentDate = date.getFullYear() + "-";
    if (date.getMonth() < 9) {
        currentDate += "0" + (date.getMonth() + 1) + "-";
    }
    else {
        currentDate += date.getMonth() + 1 + "-";
    }
    if (date.getDate() < 10) {
        currentDate += "0" + date.getDate();
    }
    else {
        currentDate += date.getDate();
    }
    return currentDate;
}