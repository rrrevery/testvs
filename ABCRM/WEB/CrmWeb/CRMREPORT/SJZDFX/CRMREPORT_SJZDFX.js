$(document).ready(function () {
    FillRQLX("#S_RQLX", "001010");
    FillChannel("#S_CHANNEL");
});

function SearchClick() {
    var addr = ReportServer + "?reportlet=SJZDFX.cpt";
    var rqlx = parseInt($("#S_RQLX").combobox("getValue"));
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

    addr += "&LX=" + rqlx;
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    //addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);

}