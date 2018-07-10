var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillMD($("#cbMD"));
    FillSH($("#cbSH"));
    $("#TB_SPFL").prop("disabled", true)
    $("#TB_SHBMMC").prop("disabled", false)
    $("#TB_XFMD").click(function () {
        SelectMD("TB_XFMD", "HF_XFMD", "zHF_XFMD", false, "", "", "", 0, true)
    });

    var tp_length = $("#cbBMJC option:selected").attr("bmjc");
    if (tp_length != "1") {
        FillSHBMTreeBase("TreeSHBM", "TB_BMMC", "menuContent", $("#cbSH").val(), tp_length);
    }

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


    $("#TB_SPFL").click(function () {
        SelectSHSPFL("TB_SPFL", "HF_SPFLID", "zHF_SPFLID", $("#cbSH").val(), true)
    })

    $("#TB_SHBMMC").click(function () {
        var a = $("#cbSH").val();
        if ($("#cbSH").val() == "") {
            art.dialog({ lock: true, content: "请选择商户！" });
            return;
        }
        else {
            SelectSHBM("TB_SHBMMC", "HF_SHBMDM", "zHF_SHBMDM", $("#cbSH").val(), "");
        }
    });

});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=KQ_KDJFX.cpt";
    //var rq1 = new Date($("#edRQ1").val());
    //var rq2 = new Date($("#edRQ2").val());
    var tq1 = $("#mdRQ1").val();
    var tq2 = $("#mdRQ2").val();
    var mdid = $("#cbMD").val();
    var bmjc = parseInt($("#cbBMJC").val());
    //if (bmjc != 5) {
    //    bmjc = parseInt(bmjc) * 2;
    //}
    //else {
    //    bmjc = 13;
    //}
    var xfmd = $("#HF_XFMD").val();
    var bmlx = $("#cbBMLX").val();
    var jb = $("#cbJB").val();
    if (mdid == "" && jb == "2") {
        ShowMessage("请选择门店");
        return;
    }
    if (xfmd == "") {
        ShowMessage("请选择消费门店");
        return;
    }
    //if (xfmd == "" && bmjc == "2") {
    //    ShowMessage("请选择消费门店");
    //    return;
    //}

    // var srq1 = GetDateString(rq1, "0");
    // var srq2 = GetDateString(rq2, "0");
    //var stq1 = GetDateString(tq1, "1");
    // var stq2 = GetDateString(tq2, "1");

    if (xfmd != "")
        addr += "&XFMD=" + xfmd;
    if (mdid != "")
        addr += "&MDID=" + mdid;
    if ($("#HF_HYZ").val() != "")
        addr += "&GRPID=" + $("#HF_HYZ").val().substr(0, $("#HF_HYZ").val().length - 1);
    if ($("#HF_SHBMDM").val() != "" && bmlx == "21")
        addr += "&BMDM=" + $("#HF_SHBMDM").val();
    if ($("#HF_SPFLID").val() != "" && bmlx == "22")
        addr += "&SPFLID=" + $("#HF_SPFLID").val();
    if (bmjc != 2) {
        addr += "&BMLX=" + bmlx;
    }
    addr += "&JB=" + jb;
    //   if ($("#edRQ1").val() != "" && $("#edRQ2").val() != "") {
    //       addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    //   }
    if ($("#mdRQ1").val() != "" && $("#mdRQ2").val() != "") {
        addr += "&MT1=" + tq1 + "&MT2=" + tq2;
    }
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

function BMLXChange() {
    $("#TB_SHBMMC").val("");
    $("#HF_SHBMDM").val("");
    $("#zHF_SHBMDM").val("");
    $("#TB_SPFL").val("");
    $("#HF_SPFLID").val("");
    $("#zHF_SPFLID").val("");
    if ($("#cbBMLX").val() == 21) {
        $("#TB_SPFL").prop("disabled", true)
        $("#TB_SHBMMC").prop("disabled", false)

        //$("#cbBMJC").empty();
        //if ($("#cbSH").val() != "") {
        //    $("#cbBMJC").html("");
        //    var sBMJC = $("#cbSH option:selected").attr("bmjc");
        //    var iBMCD = 0;
        //    if ($("#cbBMLX").val() == 21) {
        //        for (var i = 0; i < sBMJC.length ; i++) {
        //            iBMCD += parseInt(sBMJC[i]);
        //            $("#cbBMJC").append("<option value='" + iBMCD + "' bmjc='" + i + "'>" + (i + 1).toString() + "级</option>");
        //        }
        //    }
        //}
    }
    else {
        $("#TB_SPFL").prop("disabled", false)
        $("#TB_SHBMMC").prop("disabled", true)
        //$("#cbBMJC").empty();
        //$("#cbBMJC").append("<option value='2'  bmjc='1'>1级</option>   ");
        //$("#cbBMJC").append("<option value='4'  bmjc='2'>2级</option>   ");
        //$("#cbBMJC").append("<option value='6' bmjc='3' >3级</option>   ");
        //$("#cbBMJC").append("<option value='8' bmjc='4' >4级</option>   ");
        //$("#cbBMJC").append("<option value='13' bmjc='5' >5级</option>   ");

    }
}

function onClick(e, treeId, treeNode) {
    $("#TB_BMMC").val(treeNode.name);
    $("#HF_BMDM").val(treeNode.id);
    hideMenuSHBM("menuContent");
}

function BMJCChange() {
    if ($("#cbBMJC").val() != "" && $("#cbBMJC").val() != "1") {
        //       var tp_length = parseInt($("#cbBMJC").val());
        var tp_length = $("#cbBMJC option:selected").attr("bmjc");
        FillSHBMTreeBase("TreeSHBM", "TB_BMMC", "menuContent", $("#cbSH").val(), tp_length);
    }
    else {
        $("#TreeSHBM").html("");
        $("#TB_BMMC").val("");
        $("#HF_BMDM").val("");


    }
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

function SHChange() {
    //if ($("#cbSH").val() != "") {
    //    $("#cbBMJC").html("");
    //    var sBMJC = $("#cbSH option:selected").attr("bmjc");
    //    var iBMCD = 0;
    //    if ($("#cbBMLX").val() == 21) {
    //        for (var i = 0; i < sBMJC.length ; i++) {
    //            iBMCD += parseInt(sBMJC[i]);
    //            $("#cbBMJC").append("<option value='" + iBMCD + "' bmjc='" + i + "'>" + (i + 1).toString() + "级</option>");
    //        }
    //    }
    //}
    $("#TB_SHBMMC").val("");
    $("#HF_SHBMDM").val("");
    $("#zHF_SHBMDM").val("");
    $("#TB_SPFL").val("");
    $("#HF_SPFLID").val("");
    $("#zHF_SPFLID").val("");

}