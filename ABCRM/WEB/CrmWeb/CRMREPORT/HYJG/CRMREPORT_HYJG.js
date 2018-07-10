var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillSH($("#cbSH3"));
    FillSH($("#cbSH5"));
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
    $("#TB_MD2").click(function () {
        SelectMD("TB_MD2", "HF_MD2", "zHF_MD2", false, $("#cbSH3").val(), "", "", "", true);
    });
    $("#TB_MD5").click(function () {
        SelectMD("TB_MD5", "HF_MD5", "zHF_MD5", false, $("#cbSH5").val(), "", "", "", true);
    });
    $("#TB_RQ1").focus(function () {
        WdatePicker({ onpicked: datechange1($("#TB_RQ1").val()) });
    });
    $("#TB_RQ2").focus(function () {
        WdatePicker({ onpicked: datechange2($("#TB_RQ2").val()) });
    });

    var Currentdate = new Date();
    Currentdate = new Date(Currentdate.getFullYear(), Currentdate.getMonth() - 1, Currentdate.getDate() - 1);
    $("#TB_YF").val(getDate(Currentdate));
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYJGFX.cpt";
    var rq1 = new Date($("#edRQ1").val());
    var rq2 = new Date($("#edRQ2").val());
    var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
    var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
    var rqlx = $("#cbRQLX").val();

    var srq1 = GetDateString(rq1, rqlx);
    var srq2 = GetDateString(rq2, rqlx);
    var stq1 = GetDateString(tq1, rqlx);
    var stq2 = GetDateString(tq2, rqlx);

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
    addr += "&RQ=" + srq1;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

function btnSrch2Click() {
    var addr = ReportServer + "?reportlet=HYJGFX_NLXB.cpt";

    var rqlx = parseInt($("#cbRQLX2").val());
    var srq1;
    var srq2;
    var stq1;
    var stq2;
    if (rqlx == 0) {
        var rq1 = new Date($("#edBMRQ1").val());
        var rq2 = new Date($("#edBMRQ2").val());
        var tq1 = GetContrastDate($("#edBMRQ1").val(), rqlx);
        var tq2 = GetContrastDate($("#edBMRQ2").val(), rqlx);
        srq1 = GetDateString(rq1, rqlx);
        srq2 = GetDateString(rq2, rqlx);
        stq1 = GetDateString(tq1, rqlx);
        stq2 = GetDateString(tq2, rqlx);
    }
    else {
        srq1 = $("#edBMRQ1").val();
        srq2 = $("#edBMRQ2").val();
        stq1 = GetContrastDate($("#edBMRQ1").val(), rqlx);
        stq2 = GetContrastDate($("#edBMRQ2").val(), rqlx);
    }
    addr += "&WD=" + parseInt($("#cbFXWD").val())
    addr += "&LX=" + rqlx;
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr2").attr("src", addr);
};

function btnSrch3Click() {
    var addr = ReportServer + "?reportlet=HYJGFX_RQ.cpt";
    addr += "&RQ1=" + $("#TB_RQ1").val() + "&RQ2=" + $("#TB_RQ2").val();
    addr += "&TQ1=" + $("#TB_TQ1").val() + "&TQ2=" + $("#TB_TQ2").val();
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
    addr += "&LX=" + 1;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr3").attr("src", addr);
};
function btnSrch4Click() {
    var addr = ReportServer + "?reportlet=HYJGFX_DJ.cpt";
    addr += "&RQ=" + $("#TB_YF").val();
    if ($("#cbSH3").val() != "") {
        addr += "&SHDM=" + $("#cbSH3").val();
    }
    else {
        ShowMessage("请选择商户");
        return;
    }
    var mdid = $("#HF_MD2").val();
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    else
        addr += "&MDID=" + mdid;
    addr += "&LX=" + $("#cbTJWD").val();
    addr += "&HYDMC=" + $("#cbHYD").val();
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr4").attr("src", addr);
};


function btnSrch5Click() {
    var addr = ReportServer + "?reportlet=HYJGFX_SQ.cpt";
    var mdid = $("#HF_MD5").val();
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    else
        addr += "&MDID=" + mdid;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr5").attr("src", addr);
};


function SH5Change() {
    $("#TB_MD5").val("");
    $("#HF_MD5").val("");
    $("#zHF_MD5").val("");
}

function SH3Change() {
    $("#TB_MD2").val("");
    $("#HF_MD2").val("");
    $("#zHF_MD2").val("");
}
function SH2Change() {
    $("#TB_MDMC").val("");
    $("#HF_MDID").val("");
    $("#zHF_MDID").val("");
}
function SHChange() {
    $("#TB_MD").val("");
    $("#HF_MD").val("");
    $("#zHF_MD").val("");
}
function datechange1(date) {
    if (date != "") {
        $("#TB_TQ1").val(parseInt(date) - 100);
    }
}
function datechange2(date) {
    if (date != "") {
        $("#TB_TQ2").val(parseInt(date) - 100);
    }
}
function getDate(date) {
    var currentDate = date.getFullYear();
    if (date.getMonth() < 9) {
        currentDate += "0" + (date.getMonth() + 1);
    }
    else {
        currentDate += date.getMonth() + 1;
    }
    return currentDate;
}