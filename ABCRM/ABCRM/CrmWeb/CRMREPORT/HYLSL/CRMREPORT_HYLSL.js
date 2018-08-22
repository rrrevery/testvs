var ReportServer = "";

$(document).ready(function () {
    var myDate = new Date();
    $("#edRQ1").val(myDate.getFullYear());
    ReportServer = GetReportServer();
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYLSLQS.cpt";
    var rq1 = $("#edRQ1").val();
    
    addr += "&RQ=" + rq1;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

