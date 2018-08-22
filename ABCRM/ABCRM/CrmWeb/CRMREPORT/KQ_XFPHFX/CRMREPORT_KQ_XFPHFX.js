var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillMD($("#cbMD"));
    //FillMD($("#cbXFMD"));
    $("#TB_XFMD").click(function () {
        SelectMD("TB_XFMD", "HF_XFMD", "zHF_XFMD", false, "", "", "", 0, true)
    });
    $("#TB_HYZMC").click(function () {
        var mdid = $("#cbMD").val();
        var jb = $("#cbJB").val();
        if (mdid == "" && jb == "2") {
            ShowMessage("请选择门店");
            return;
        }
        var data = $("#zHF_HYZ").val();
        var el = $("<input>", { type: 'text', val: data });
        $.dialog.data('IpValues', el);
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
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=KQ_XFPHFX.cpt";

    var stq1 = $("#jdRQ1").val();
    var stq2 = $("#jdRQ2").val();

    var mdid = $("#cbMD").val();
    var xfmd = $("#HF_XFMD").val();
    var fxlx = $("#cbWDLX").val();
    var jb = $("#cbJB").val();
    if (mdid == "" && jb == "2") {
        ShowMessage("请选择门店");
        return;
    }
    //if (xfmd == "") {
    //    ShowMessage("请选择消费门店");
    //    return;
    //}
    //if (xfmd != "")
    //    addr += "&XFMD=" + xfmd;
    if (mdid != "")
        addr += "&MDID=" + mdid;
    if ($("#HF_HYZ").val() != "")
        addr += "&GRPID=" + $("#HF_HYZ").val().substr(0, $("#HF_HYZ").val().length - 1);
    addr += "&WDLX=" + fxlx + "&JB=" + jb;
    //if ($("#jdRQ1").val() != "" && $("#jdRQ2").val() != "") {
    //    addr += "&JD1=" + stq1 + "&JD2=" + stq2;
    //}
    if ($("#jdRQ1").val() != "") {
        addr += "&JD1=" + stq1;
    }
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

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