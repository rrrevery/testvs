var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    AddToolButtons("查询", "B_Search");
    document.getElementById("B_Search").onclick = SearchClick;
    $(".btnsep:visible:last").hide();
    $("#fr1").height(745-$(".maininput").height());
});
