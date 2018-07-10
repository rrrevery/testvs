var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillSH($("#cbSH"));
    $("#TB_RQ1").focus(function () {
        BindDateEvent($("#cbRQLX").val());
        WdatePicker({ onpicked: datechange1($("#TB_RQ1").val()) });
    });
    $("#TB_RQ2").focus(function () {
        BindDateEvent($("#cbRQLX").val());
        WdatePicker({ onpicked: datechange2($("#TB_RQ2").val()) });
    });
    $("#TB_TQ1").focus(function () {
        BindDateEvent($("#cbRQLX").val());
    });
    $("#TB_TQ2").focus(function () {
        BindDateEvent($("#cbRQLX").val());
    });
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=XZHYFX_RQ.cpt";
    var rqlx = $("#cbRQLX").val();
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
    addr += "&LX=" + rqlx;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};


function cbRQLXChange() {
    $("#TB_RQ1").val("");
    $("#TB_RQ2").val("");
    $("#TB_TQ1").val("");
    $("#TB_TQ2").val("");
}

function datechange1(date) {
    if (date != "") {
        if ($("#cbRQLX").val() == "1") {
            $("#TB_TQ1").val(parseInt(date) - 100);
        }
        if ($("#cbRQLX").val() == "0") {
            var rq = new Date($("#TB_RQ1").val());
            var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
            var stq = getDate(tq);
            $("#TB_TQ1").val(stq);
        }
    }
}
function datechange2(date) {
    if (date != "") {
        if ($("#cbRQLX").val() == "1") {
            $("#TB_TQ2").val(parseInt(date) - 100);
        }
        if ($("#cbRQLX").val() == "0") {
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