var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#HF_SHDM").val(), "", "", "", true);
    });

    $("#TB_KQMDMC").click(function () {
        SelectMD("TB_KQMDMC", "HF_KQMDID", "zHF_KQMDID", true, $("#HF_SHDM").val(), "", "", "", 1);
    });

    $("#TB_HYZMC").click(function () {
        var mdid = $("#HF_KQMDID").val();
        var jb = $("#cbJB").val();
        if (mdid == "" && jb == "2") {
            ShowMessage("请选择门店");
            return;
        }
        var data = $("#zHF_HYZ").val();
        var el = $("<input>", { type: 'text', val: data });
        $.dialog.data('IpValues', el);
        $.dialog.data('IpValuesReturn', "");
        $.dialog.data('IpValuesLevel', $("#cbJB").val());
        $.dialog.data('IpValuesMDID', mdid);
        $.dialog.data('IpValuesChoiceOne', false);
        $.dialog.open("../../WUC/HYZ/WUC_HYZ.aspx", {
            lock: true, width: 400, height: 430, cancel: false
                    , close: function () {
                        WUC_HYZ_Return();
                    }
        }, false);
    });
    var Currentdate = new Date();
    Currentdate = new Date(Currentdate.getFullYear(), Currentdate.getMonth(), Currentdate.getDate() - 1);
    var PastTime = new Date(Currentdate.getFullYear() - 1, Currentdate.getMonth(), Currentdate.getDate());
    $("#RQ1").val(getDate(Currentdate));
    $("#RQ2").val(getDate(Currentdate));
    //$("#TQ1").val(getDate(PastTime));
    //$("#TQ2").val(getDate(PastTime));
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYXFXQXG_KQ.cpt";
    // var rqlx = $("#cbRQLX").val();
    if ($("#RQ1").val() != "" && $("#RQ2").val() != "") {
        var rq1 = $("#RQ1").val();
        var rq2 = $("#RQ2").val();
        var tq1 = GetYF1();
        var tq2 = GetYF2();
        addr += "&RQ1=" + rq1 + "&RQ2=" + rq2;
        addr += "&TQ1=" + tq1 + "&TQ2=" + tq2
    }
    else
        ShowMessage("请选择日期");
    var jb = $("#cbJB").val();
    var shdm = $("#HF_SHDM").val();
    var mdid = $("#HF_MDID").val();
    if (shdm == "") {
        ShowMessage("请选择商户");
        return;
    }
    if (mdid == "" && jb == "2") {
        ShowMessage("请选择门店");
        return;
    }
    if ($("#HF_KQMDID").val() != "") {
        addr += "&KQMD=" + $("#HF_KQMDID").val();
    }
    if ($("#HF_SHDM").val() != "") {
        addr += "&SHDM=" + $("#HF_SHDM").val();
    }
    if ($("#HF_MDID").val() != "") {
        addr += "&MDID=" + $("#HF_MDID").val();
    }
    addr += "&JB=" + jb;
    if ($("#HF_HYZ").val() != "")
        addr += "&GRPID=" + $("#HF_HYZ").val().substr(0, $("#HF_HYZ").val().length - 1);
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

//getMonth()  返回0~11
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
function WUC_HYZ_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";

            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                if (tp_return_ChoiceOne) {
                    tp_mc += contractValues[i].Name;
                    tp_hf += contractValues[i].Id;
                } else {
                    tp_mc += contractValues[i].Name + ",";
                    tp_hf += contractValues[i].Id + ",";
                }
            }
            $("#TB_HYZMC").val(tp_mc);
            $("#HF_HYZ").val(tp_hf);
            $("#zHF_HYZ").val(jsonString);
        }
    }
}
function GetYF1() {
    var dateString = $("#RQ1").val();
    var pattern = /(\d{4})(\d{2})/;
    var formatedDate = new Date(dateString.replace(pattern, '$1-$2-01'));
    formatedDate.setMonth(formatedDate.getMonth() - 12);
    rq1 = FormatDate(formatedDate, "yyyyMM");
    return rq1;
}
function GetYF2() {
    var dateString = $("#RQ2").val();
    var pattern = /(\d{4})(\d{2})/;
    var formatedDate = new Date(dateString.replace(pattern, '$1-$2-01'));
    formatedDate.setMonth(formatedDate.getMonth() - 12);
    rq2 = FormatDate(formatedDate, "yyyyMM");
    return rq2;
}
