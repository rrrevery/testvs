var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillMD($("#cbMD"));

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
    var addr = ReportServer + "?reportlet=KQ_JTSXFX.cpt";
    // var rq1 = new Date($("#edRQ1").val());
    // var rq2 = new Date($("#edRQ2").val());
    var mdid = $("#cbMD").val();
    var jb = $("#cbJB").val();
    if (mdid == "" && jb == "2") {
        ShowMessage("请选择门店");
        return;
    }

    //var srq1 = GetDateString(rq1, "0");
    // var srq2 = GetDateString(rq2, "0");
    if (mdid != "") {
        addr += "&MDID=" + mdid;
    }
    addr += "&JB=" + jb;
    if ($("#HF_HYZ").val() != "")
        addr += "&GRPID=" + $("#HF_HYZ").val().substr(0, $("#HF_HYZ").val().length - 1);
    // if ($("#edRQ1").val() != "" && $("#edRQ2").val() != "") {
    //     addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    // }
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

