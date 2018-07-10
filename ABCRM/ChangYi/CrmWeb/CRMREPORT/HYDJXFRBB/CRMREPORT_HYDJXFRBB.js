var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#HF_SHDM").val());
    });

    var Currentdate = new Date();
    Currentdate = new Date(Currentdate.getFullYear(), Currentdate.getMonth(), Currentdate.getDate() - 1);
    var PastTime = new Date(Currentdate.getFullYear() - 1, Currentdate.getMonth(), Currentdate.getDate());
    $("#RQ1").val(getDate(Currentdate));
    $("#RQ2").val(getDate(Currentdate));
    $("#TQ1").val(getDate(PastTime));
    $("#TQ2").val(getDate(PastTime));
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=R_HYDJXFFX.cpt";
    var rq1 = $("#RQ1").val();
    var rq2 = $("#RQ2").val();
    var tq1 = $("#TQ1").val();
    var tq2 = $("#TQ2").val();

    if ($("#HF_SHDM").val() != "") {
        addr += "&SHDM=" + $("#HF_SHDM").val();
    }
    if ($("#HF_MDID").val() != "") {
        addr += "&MDID=" + $("#HF_MDID").val();
    }

    if ($("#RQ1").val() != "" && $("#RQ2").val() != "") {
        addr += "&RQ1=" + rq1 + "&RQ2=" + rq2;
    }

    if ($("#TQ1").val() != "" && $("#TQ2").val() != "") {
        addr += "&TQ1=" + tq1 + "&TQ2=" + tq2;
    }

    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

//getMonth()  返回0~11
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