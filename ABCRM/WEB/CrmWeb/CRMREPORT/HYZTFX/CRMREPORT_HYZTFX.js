var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillSH($("#cbSH"));
    $("#TB_MDMC").click(function () {
        if ($("#cbSH").val() != "") {
            SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#cbSH").val(), "", "", "", true);
        }
        else {
            SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, "", "", "", "", true);
        }
    })
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYZTFX.cpt";
    var rq1 = new Date($("#edRQ1").val());
    var srq1 = GetDateString(rq1, '0');
    if ($("#HF_MDID").val() != "") {
        addr += "&MDID=" + $("#HF_MDID").val();
    }
    addr += "&RQ=" + srq1;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};


function SHChange() {
    $("#TB_MDMC").val("");
    $("#HF_MDID").val("");
    $("#zHF_MDID").val("");
}
