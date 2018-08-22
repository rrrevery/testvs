var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillSH($("#cbSH2"));
    FillSH($("#cbSH"));
    $("#TB_MDMC").click(function () {
        if ($("#cbSH2").val() != "") {
            SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#cbSH2").val(), "", "", "", true);
        }
        else {
            SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, "", "", "", "", true);
        }
    })
    $("#TB_MD").click(function () {
        SelectMD("TB_MD", "HF_MD", "zHF_MD", false, $("#cbSH").val(), "", "", "", true);
    });
    $("#TB_RQ1").focus(function () {
        BindDateEvent($("#cbRQLX2").val());
        WdatePicker({ onpicked: datechange1($("#TB_RQ1").val()) });
    });
    $("#TB_RQ2").focus(function () {
        BindDateEvent($("#cbRQLX2").val());
        WdatePicker({ onpicked: datechange2($("#TB_RQ2").val()) });
    });
    $("#TB_TQ1").focus(function () {
        BindDateEvent($("#cbRQLX2").val());
    });
    $("#TB_TQ2").focus(function () {
        BindDateEvent($("#cbRQLX2").val());
    });

    //tab1 deal date formate
    $("#edRQ1").focus(function () {
        BindDateEvent($("#cbRQLX").val());
    });
    $("#edRQ2").focus(function () {
        BindDateEvent($("#cbRQLX").val());
    });

});

function SH2Change() {
    $("#TB_MDMC").val("");
    $("#HF_MDID").val("");
    $("#zHF_MDID").val("");
}

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYFKZKDJ.cpt";
    var rqlx = parseInt($("#cbRQLX").val());
    var srq1;
    var srq2;
    var stq1;
    var stq2;
    if (rqlx == 0) {
        var rq1 = new Date($("#edRQ1").val());
        var rq2 = new Date($("#edRQ2").val());
        var tq1 = GetContrastDate($("#edRQ1").val(), rqlx);
        var tq2 = GetContrastDate($("#edRQ2").val(), rqlx);
        srq1 = GetDateString(rq1, rqlx);
        srq2 = GetDateString(rq2, rqlx);
        stq1 = GetDateString(tq1, rqlx);
        stq2 = GetDateString(tq2, rqlx);
    }
    else {
        srq1 = $("#edRQ1").val();
        srq2 = $("#edRQ2").val();
        stq1 = GetContrastDate($("#edRQ1").val(), rqlx);
        stq2 = GetContrastDate($("#edRQ2").val(), rqlx);
    }
    if ($("#cbSH2").val() != "") {
        addr += "&SHDM=" + $("#cbSH2").val();
    }
    if ($("#HF_MDID").val() != "") {
        addr += "&MDID=" + $("#HF_MDID").val();
    }
    else {
        art.dialog({ lock: true, content: "请选择门店", time: true });
        return;
    }
    addr += "&LX=" + rqlx;
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};
function btnSrch2Click() {
    var addr = ReportServer + "?reportlet=HYFKZKDJ_RQ.cpt";
    var rqlx = $("#cbRQLX2").val();
    if (rqlx == "0") {
        var rq1 = new Date($("#TB_RQ1").val());
        var rq2 = new Date($("#TB_RQ2").val());
        var tq1 = new Date($("#TB_TQ1").val());
        var tq2 = new Date($("#TB_TQ2").val());
        var srq1 = GetDateString(rq1, rqlx);
        var srq2 = GetDateString(rq2, rqlx);
        var stq1 = GetDateString(tq1, rqlx);
        var stq2 = GetDateString(tq2, rqlx);
        addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
        addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    }
    else {
        addr += "&RQ1=" + $("#TB_RQ1").val() + "&RQ2=" + $("#TB_RQ2").val();
        addr += "&TQ1=" + $("#TB_TQ1").val() + "&TQ2=" + $("#TB_TQ2").val();
    }
    if ($("#cbSH").val() != "") {
        addr += "&SHDM=" + $("#cbSH").val();
    }
    else {
        ShowMessage("请选择商户");
        return;
    }
    var mdid = $("#HF_MD").val();
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    else
        addr += "&MDID=" + mdid;
    addr += "&LX=" + rqlx;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr2").attr("src", addr);
};

function SHChange() {
    $("#TB_MD").val("");
    $("#HF_MD").val("");
    $("#zHF_MD").val("");
}

function datechange1(date) {
    if (date != "") {
        if ($("#cbRQLX2").val() == "1") {
            $("#TB_TQ1").val(parseInt(date) - 100);
        }
        if ($("#cbRQLX2").val() == "2") {
            $("#TB_TQ1").val(parseInt(date) - 10);
        }
        if ($("#cbRQLX2").val() == "0") {
            var rq = new Date($("#TB_RQ1").val());
            var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
            var stq = getDate(tq);
            $("#TB_TQ1").val(stq);
        }
    }
}
function datechange2(date) {
    if (date != "") {
        if ($("#cbRQLX2").val() == "1") {
            $("#TB_TQ2").val(parseInt(date) - 100);
        }
        if ($("#cbRQLX2").val() == "2") {
            $("#TB_TQ2").val(parseInt(date) - 10);
        }
        if ($("#cbRQLX2").val() == "0") {
            var rq = new Date($("#TB_RQ2").val());
            var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
            var stq = getDate(tq);
            $("#TB_TQ2").val(stq);
        }
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
function cbRQLX2Change() {
    $("#TB_RQ1").val("");
    $("#TB_RQ2").val("");
    $("#TB_TQ1").val("");
    $("#TB_TQ2").val("");
}