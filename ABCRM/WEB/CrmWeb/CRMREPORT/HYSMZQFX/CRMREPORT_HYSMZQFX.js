var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    $("#TB_XFMD").click(function () {
        SelectMD("TB_XFMD", "HF_XFMD", "zHF_XFMD", false, "", "", "", 0, true)
    });
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYSMZQFX.cpt";
    var rq1 = $("#RQ1").val();

    if ($("#RQ1").val() != "") {
        addr += "&RQ=" + rq1;
    }
    if ($("#HF_XFMD").val() != "") {
        addr += "&MDID=" + $("#HF_XFMD").val();
    }
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};