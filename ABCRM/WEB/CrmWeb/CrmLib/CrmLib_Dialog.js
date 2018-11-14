/*弹出框从common里移出来*/
function MoseDialogModel(dialogName, hideField, showField, hideData, dialogUrl, dialogTitle, multiSelect, nameValue, idValue, condData, autoShow, dialogWidth, dialogHeight) {
    if (autoShow == undefined) {
        autoShow == true;
    }

    $.dialog.data("dialogName", dialogName);
    $.dialog.data("dialogTitle", dialogTitle);
    if (typeof (hideData) == 'object')
        $.dialog.data("dialogInput", JSON.stringify(hideData));
    else {
        $.dialog.data("dialogInput", $("#" + hideData).val());
    }
    $.dialog.data("SingleSelect", !multiSelect);//尼玛叫Single给赋值multi
    $.dialog.data("autoShow", autoShow);
    $.dialog.data("DialogCondition", "");
    if (condData) {
        $.dialog.data("DialogCondition", JSON.stringify(condData));
    }
    var vWidth = dialogWidth || 600;
    var vHeight = dialogHeight || 550;
    if (dialogName.indexOf("Tree") >= 0) {
        vHeight = 400;
        vWidth = 400;
    }
    $.dialog.open(dialogUrl, {
        lock: true, width: vWidth, height: vHeight, cancel: false,
        drag: true, fixed: false,
        close: function () {
            var tp_mc = "";
            var tp_hf = "";
            var bSelected = $.dialog.data('dialogSelected');
            if (bSelected) {
                var lst = JSON.parse($.dialog.data(dialogName));
                for (var i = 0; i <= lst.length - 1; i++) {
                    tp_mc += lst[i][nameValue] + ",";
                    if (typeof (lst[i][idValue]) == "string" && lst.length > 1) {
                        tp_hf += "'" + lst[i][idValue] + "',";
                    }
                    else {
                        tp_hf += lst[i][idValue] + ",";
                    }
                }
                $("#" + showField).val(tp_mc.substr(0, tp_mc.length - 1));
                $("#" + hideField).val(tp_hf.substr(0, tp_hf.length - 1));
                if (typeof (hideData) != 'object')
                    $("#" + hideData).val($.dialog.data(dialogName));
                MoseDialogCustomerReturn(dialogName, lst, showField);
            }
            else {
                if (dialogName.indexOf("DialogSK") < 0) {
                    $("#" + showField).val("");
                    $("#" + hideField).val("");
                    if (typeof (hideData) != 'object') {
                        $("#" + hideData).val("");
                    }
                }
            }
            $.dialog.data(dialogName, "");
            $.dialog.data("dialogInput", "");
            $.dialog.data("DialogCondition", "");
        }
    }, false);
    //dialog({
    //    id: 'test-dialog',
    //    title: 'loading..',
    //    url: dialogUrl,
    //    width: vWidth,
    //    height: vHeight,
    //    //quickClose: true,
    //    data:{dialogName: dialogName,
    //        dialogTitle:dialogTitle,
    //        SingleSelect:!multiSelect,
    //        autoShow:autoShow,
    //    },
    //    onclose: function () {
    //        if (this.returnValue) {
    //            $('#value').html(JSON.stringify(this.returnValue));
    //        }
    //    },
    //}).showModal(this);
};

//T--自定义返回方法
function MoseDialogCustomerReturn(dialogName, lstData, showField) {
};

//function SelectWX_XQGZ(GZMC, GZID, zGZID, Single) {  //SelectWXLPFFGZ
//    var data = $("#" + zGZID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    $.dialog.open("../../WX_WUC/WX_XQGZ/WUC_XQGZ.aspx?", {
//        lock: true, width: 450, height: 470, cancel: false
//        , close: function () {
//            WUC_WX_XQGZ_Return(GZMC, GZID, zGZID);
//        }
//    }, false);
//}
//function WUC_WX_XQGZ_Return(GZMC, GZID, zGZID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            $("#" + GZMC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Articles;
//            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].Name; i++) {
//                tp_mc += contractValues[i].Name + ";";
//                tp_hf += contractValues[i].Id + ",";

//            }
//            $("#" + GZMC).val(tp_mc);
//            $("#" + GZID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zGZID).val(jsonString);
//        }
//    }
//}
function SelectWX_CXHD(CXZT, CXID, zCXID, Single) {
    var data = $("#" + zCXID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WX_WUC/WX_CXHD/WUC_WXCXHD.aspx", {
        lock: true, width: 450, height: 470, cancel: false
        , close: function () {
            WUC_WX_CXHD_Return(CXZT, CXID, zCXID);
        }
    }, false);
}

function WUC_WX_CXHD_Return(CXZT, CXID, zCXID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + CXZT).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            //contractValues = jsonInput.Articles;
            contractValues = jsonInput;
            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].sCXZT; i++) {
                tp_mc += contractValues[i].sCXZT + ";";
                tp_hf += contractValues[i].iCXID + ",";

            }
            $("#" + CXZT).val(tp_mc);
            $("#" + CXID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zCXID).val(jsonString);
        }
    }
}


function WUC_CXHD_Return(CXZT, CXID, zCXID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + CXZT).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].sCXZT; i++) {
                tp_mc += contractValues[i].sCXZT + ";";
                tp_hf += contractValues[i].iCXID + ",";

            }
            $("#" + CXZT).val(tp_mc);
            $("#" + CXID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zCXID).val(jsonString);
        }
    }
}
function SelectMDLC(MDMC, MDID, zMDID, Single, LCMC, LCID) {
    var data = $("#" + zMDID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WX_WUC/WX_MDLC/WUC_WXMDLC.aspx", {
        lock: true, width: 450, height: 450, cancel: false
        , close: function () {
            WUC_MDLC_Return(MDMC, MDID, zMDID, LCMC, LCID);
        }
    }, false);

}

function WUC_MDLC_Return(MDMC, MDID, zMDID, LCMC, LCID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var tp_lcid = ""
            var tp_lcmc = ""
            $("#" + MDMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Code + ";";
                tp_hf += contractValues[i].Id + ",";
                tp_lcmc += contractValues[i].Name + ",";
                tp_lcid += contractValues[i].Id1;
            }
            $("#" + MDMC).val(tp_mc);//赋值
            $("#" + MDID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zMDID).val(jsonString);
            $("#HF_LCMC").val(tp_lcmc);
            $("#HF_LCID").val(tp_lcid);
        }
    }
}
function SelectLCSB(SBMC, SBID, zSBID, Single, FLMC) {
    var data = $("#" + zSBID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WX_WUC/LCSB/WUC_LCSB.aspx", {
        lock: true, width: 450, height: 470, cancel: false
        , close: function () {
            WUC_LCSB_Return(SBMC, SBID, zSBID, FLMC);
        }
    }, false);

}

function WUC_LCSB_Return(SBMC, SBID, zSBID, FLMC) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var tp_lcid = ""
            $("#" + SBMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Code + ";";
                tp_hf += contractValues[i].Id + ",";
                tp_lcid += contractValues[i].Name + ",";
            }
            $("#" + SBMC).val(tp_mc);
            $("#" + SBID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zSBID).val(jsonString);
        }
    }
}
function SelectHYKFXDW(FXDWMC, FXDWID, zFXDWID, Single) {
    var data = $("#" + zFXDWID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('FXDWReturnValue', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/HYKFXDW/WUC_HYKFXDW.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_HYKFXDW_Return(FXDWMC, FXDWID, zFXDWID);
        }
    }, false);
}

function WUC_HYKFXDW_Return(FXDWMC, FXDWID, zFXDWID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + FXDWMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].id + ",";

            }
            $("#" + FXDWMC).val(tp_mc);
            $("#" + FXDWID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zFXDWID).val(jsonString);
            $.dialog.data('IpValuesReturn', "");
        }
    }
}


function SelectSRLP(LPMC, LPDM, zLPID, bgdddm, Single, subTableID) {
    var data = $("#" + zLPID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    //  $.dialog.data('IpValuesEnable', tp_enable);
    $.dialog.open("../../WUC/LPGL/WUC_SRLP.aspx?sBGDDDM=" + bgdddm, {
        lock: true, width: 480, height: 410, cancel: false
        , close: function () {
            WUC_LP_Return2(LPMC, LPDM, zLPID, subTableID);
        }
    }, false);
}

//function WUC_LP_Return(LPMC, LPID, zLPID, subTableID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
//    //XXM start
//    if (subTableID && subtable != "") {
//        var subtable = $("#" + subTableID);
//        var count = subtable.jqGrid("getGridParam", "records");
//        var ids = subtable.jqGrid("getDataIDs");
//        var lpids = new Array();
//        for (var j = 0; j < ids.length; j++) {
//            lpids[j] = parseInt(subtable.jqGrid("getRowData", ids[j]).iLPID);
//        }
//    }
//    //XXM stop
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            var extendname = new Array();
//            $("#" + LPMC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Articles;
//            if (contractValues.length > 0) {
//                for (var i = 0; i <= contractValues.length - 1; i++) {
//                    tp_mc += contractValues[i].Name + ";";
//                    tp_hf += contractValues[i].Id + ",";
//                    //XXM start
//                    extendname[i] = contractValues[i].ExtendName.split(",")[i];
//                    if (subTableID && subtable != "") {
//                        if (subTableID && subTableID != "") {
//                            //判断重复选择
//                            if (jQuery.inArray(parseInt(contractValues[i].Id), lpids) == "-1") {
//                                subtable.addRowData("subGridRow_" + count + i + 1, {
//                                    iLPID: contractValues[i].Id,
//                                    sLPMC: contractValues[i].Name
//                                });
//                            }
//                        }
//                    }
//                    //XXM stop
//                }
//            }
//            $("#" + LPMC).val(tp_mc);
//            $("#" + LPID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zLPID).val(jsonString);
//        }
//    }
//}
function WUC_LP_Return2(LPMC, LPDM, zLPID, subTableID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    //XXM start
    if (subTableID && subtable != "") {
        var subtable = $("#" + subTableID);
        var count = subtable.jqGrid("getGridParam", "records");
        var ids = subtable.jqGrid("getDataIDs");
        var lpids = new Array();
        for (var j = 0; j < ids.length; j++) {
            lpids[j] = subtable.jqGrid("getRowData", ids[j]).sLPDM;
        }
    }
    //XXM stop
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var extendname = new Array();
            $("#" + LPMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            if (contractValues.length > 0) {
                for (var i = 0; i <= contractValues.length - 1; i++) {
                    tp_mc += contractValues[i].Name + ";";
                    tp_hf += contractValues[i].Code + ",";
                    //XXM start
                    extendname[i] = contractValues[i].ExtendName.split(",")[i];
                    if (subTableID && subtable != "") {
                        if (subTableID && subTableID != "") {
                            //判断重复选择
                            if (jQuery.inArray(parseInt(contractValues[i].Id), lpids) == "-1") {
                                subtable.addRowData("subGridRow_" + count + i + 1, {
                                    sLPDM: contractValues[i].Code,
                                    sLPMC: contractValues[i].Name
                                });
                            }
                        }
                    }
                    //XXM stop
                }
            }
            $("#" + LPMC).val(tp_mc);
            $("#" + LPDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zLPID).val(jsonString);
        }
    }
}

function WUC_YHQ_Return(YHQMC, YHQID, zYHQID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + YHQMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";

            }
            $("#" + YHQMC).val(tp_mc);
            $("#" + YHQID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zYHQID).val(jsonString);
            WUC_YHQ_ReturnCustom();
        }
    }
}

function WUC_YHQ_ReturnCustom() {
}



//function SelectFLMC(FLMC, FLID, zFLID, Single) {
//    var data = $("#" + zFLID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    $.dialog.open("../../WUC/FLMC/WUC_FLMC.aspx", {
//        lock: true, width: 400, height: 450, cancel: false
//        , close: function () {
//            WUC_FLMC_Return(FLMC, FLID, zFLID);
//        }
//    }, false);
//}


//function WUC_FLMC_Return(FLMC, FLID, zFLID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            $("#" + FLMC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Articles;
//            for (var i = 0; i <= contractValues.length - 1; i++) {
//                tp_mc += contractValues[i].Name + ";";
//                tp_hf += contractValues[i].Id + ",";
//                //if (tp_return_ChoiceOne) {
//                //    tp_hf += contractValues[i].Id;
//                //} else {

//                //}
//            }
//            $("#" + FLMC).val(tp_mc);
//            $("#" + FLID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zFLID).val(jsonString);
//        }
//    }
//}


function SelectKH(KHMC, KHDM, zKHDM, Single) {
    var data = $("#" + zKHDM).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/MZKGL/WUC_SelectKH.aspx", {
        lock: true, width: 600, height: 400, cancel: false
        , close: function () {
            WUC_KH_Return(KHMC, KHDM, zKHDM);
        }
    }, false);
}


function WUC_KH_Return(KHMC, KHDM, zKHDM) {


    var returnData = $.dialog.data('IpValuesReturn');//接收的应该是转换成对象，或者数组
    var dataArray = new Array();

    //有数据返回
    if (returnData != null && returnData.length > 0) {

        dataArray = JSON.parse(returnData);//返回的数据需要符合JSON字符串格式，才能进行转换成数组，或者对象                 
    }

    //将添加到文本框
    for (var i = 0; i <= dataArray.length - 1; i++) {
        if (dataArray[i].sKHDM != "") {


            $("#" + KHMC).val(dataArray[i].sKHMC);
            $("#" + KHDM).val(dataArray[i].sKHDM);
        }
    }
    $.dialog.data('IpValuesReturn', "");//清空数据


}



function SelectSBMC(SBMC, SBID, zSBID, Single) {
    var data = $("#" + zSBID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/SBMC/WUC_SBMC.aspx", {
        lock: true, width: 600, height: 400, cancel: false
        , close: function () {
            WUC_SBMC_Return(SBMC, SBID, zSBID);
        }
    }, false);
}


function WUC_SBMC_Return(SBMC, SBID, zSBID) {


    var returnData = $.dialog.data('IpValuesReturn');//接收的应该是转换成对象，或者数组
    var dataArray = new Array();

    //有数据返回
    if (returnData != null && returnData.length > 0) {

        dataArray = JSON.parse(returnData);//返回的数据需要符合JSON字符串格式，才能进行转换成数组，或者对象                 
    }

    //将添加到文本框
    for (var i = 0; i <= dataArray.length - 1; i++) {
        if (dataArray[i].sSHMC != "") {


            $("#" + SBMC).val(dataArray[i].sSBMC);
            $("#" + SBID).val(dataArray[i].iSBID);
        }
    }
    $.dialog.data('IpValuesReturn', "");//清空数据


}
function SelectPDPC(PDPC, zPDPC, Single) {
    var data = $("#" + zPDPC).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/PDPC/WUC_PDPC.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_PDPC_Return(PDPC);
        }
    }, false);
}



function WUC_PDPC_Return(PDPC, zPDPC) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + PDPC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id;
                //if (tp_return_ChoiceOne) {
                //    tp_hf += contractValues[i].Id;
                //} else {

                //}
            }
            $("#" + PDPC).val(tp_hf);
            //$("#" + HYKTYPE).val(tp_hf.substr(0, tp_hf.length - 1));
            //$("#" + zHYKTYPE).val(jsonString);
        }
    }
}
//function SelectWXCXHD(CXZT, CXID, zCXID, Single) {
//    var data = $("#" + zCXID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    $.dialog.open("../../WX_WUC/WX_CXHD/WUC_WXCXHD.aspx", {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function () {
//            WUC_WXCXHD_Return(CXZT, CXID, zCXID);
//        }
//    }, false);
//}

//function WUC_WXCXHD_Return(CXZT, CXID, zCXID) {


//    var returnData = $.dialog.data('IpValuesReturn');//接收的应该是转换成对象，或者数组
//    var dataArray = new Array();

//    //有数据返回
//    if (returnData != null && returnData.length > 0) {

//        dataArray = JSON.parse(returnData);//返回的数据需要符合JSON字符串格式，才能进行转换成数组，或者对象                 
//    }

//    //将添加到文本框
//    for (var i = 0; i <= dataArray.length - 1; i++) {
//        if (dataArray[i].sKHDM != "") {


//            $("#" + CXZT).val(dataArray[i].sCXZT);
//            $("#" + CXID).val(dataArray[i].iCXID);
//        }
//    }
//    $.dialog.data('IpValuesReturn', "");//清空数据


//}





function SelectLCMC(LCMC, LCID, zLCID, Single) {
    var data = $("#" + zLCID).val();
    MDID = $("#HF_MDID").val()
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesshdm', MDID);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WX_WUC/WX_LCMD/WUC_WXLCMD.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_LCMC_Return(LCMC, LCID, zLCID);
        }
    }, false);
}



function WUC_LCMC_Return(LCMC, LCID, zLCID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + LCMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name;
                tp_hf += contractValues[i].Id;
                //if (tp_return_ChoiceOne) {
                //    tp_hf += contractValues[i].Id;
                //} else {

                //}
            }
            $("#" + LCMC).val(tp_mc);
            $("#" + LCID).val(tp_hf);
            $("#" + zLCID).val(jsonString);
        }
    }
}
//礼品供货商（非停用状态）
//function SelectGHS(GHSNAME, GHSID, zGHSID, ChoiceOne) {
//    var data = $("#" + zGHSID).val();//存的是选中列后，返回的json字符串
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('hasSelected', data);
//    $.dialog.data('isChoiceOne', ChoiceOne);
//    $.dialog.open("../../WUC/GHS/WUC_GHS.aspx", {
//        lock: true, width: 420, height: 470, cancel: false
//        , close: function () {
//            //窗口关闭，数据返回
//            WUC_GHS_Return(GHSNAME, GHSID, zGHSID);
//        }
//    }, false);
//}
//function WUC_GHS_Return(GHSNAME, GHSID, zGHSID) {
//    var tp_return = $.dialog.data('returnValues');
//    var tp_return_ChoiceOne = $.dialog.data('isChoiceOne');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            $("#" + GHSNAME).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput;
//            for (var i = 0; i <= contractValues.length - 1; i++) {
//                tp_mc += contractValues[i].sGHSMC + ";";
//                tp_hf += contractValues[i].iGHSID + ",";

//            }
//            $("#" + GHSNAME).val(tp_mc);
//            $("#" + GHSID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zGHSID).val(jsonString);
//        }
//    }
//}
//礼品进货单位（非停用状态）
function SelectJHDW(JHDWMC, JHDWID, zJHDWID, ChoiceOne) {
    var data = $("#" + zJHDWID).val();//存的是选中列后，返回的json字符串
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('hasSelected', data);
    $.dialog.data('isChoiceOne', ChoiceOne);
    $.dialog.open("../../WUC/JHDW/WUC_JHDW.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            //窗口关闭，数据返回
            WUC_JHDW_Return(JHDWMC, JHDWID, zJHDWID);
        }
    }, false);
}
function WUC_JHDW_Return(JHDWMC, JHDWID, zJHDWID) {
    var tp_return = $.dialog.data('returnValues');
    var tp_return_ChoiceOne = $.dialog.data('isChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + JHDWMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].sJHDWMC + ";";
                tp_hf += contractValues[i].iJHDWID + ",";

            }
            $("#" + JHDWMC).val(tp_mc);
            $("#" + JHDWID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zJHDWID).val(jsonString);
        }
    }
}
//礼品分类（非停用状态）
function SelectLPFL(LPFLMC, LPFLID, zLPFLID, ChoiceOne) {
    var data = $("#" + zLPFLID).val();//存的是选中列后，返回的json字符串
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('hasSelected', data);
    $.dialog.data('isChoiceOne', ChoiceOne);
    $.dialog.open("../../WUC/LPFL/WUC_LPFL.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            //窗口关闭，数据返回
            WUC_LPFL_Return(LPFLMC, LPFLID, zLPFLID);
        }
    }, false);
}
function WUC_LPFL_Return(LPFLMC, LPFLID, zLPFLID) {
    var tp_return = $.dialog.data('returnValues');
    var tp_return_ChoiceOne = $.dialog.data('isChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + LPFLMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].sLPFLMC + ";";
                tp_hf += contractValues[i].iLPFLID + ",";

            }
            $("#" + LPFLMC).val(tp_mc);
            $("#" + LPFLID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zLPFLID).val(jsonString);
        }
    }
}
//function SelectHYQY(QYMC, QYDM, zQYDM)
//{
//    var data = $("#" + zQYDM).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.open("../../WUC/HYQY/WUC_HYQY.aspx?ryid=1001", {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function ()
//        {
//            WUC_HYQY_Return(QYMC, QYDM, zQYDM);
//        }
//    }, false);
//}

function WUC_HYQY_Return(QYMC, QYDM, zQYDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;


        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + QYMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                //tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";

                sjson = "{}";
                $.ajax({
                    type: 'post',
                    url: "../../CrmLib/CrmLib.ashx?func=GetHYQYMX&QYDM=" + contractValues[i].Code,
                    dataType: "json",
                    async: false,
                    cache: false,
                    data: {
                        json: JSON.stringify(sjson), titles: 'cecece'
                    },
                    success: function (data) {
                        result = "";
                        if (data && data != "null") {
                            tp_mc += data.sQYMC + ";";
                        }
                        return;

                    },
                    error: function (data) {
                        result = "";
                    }
                });
            }
            $("#" + QYMC).val(tp_mc);
            $("#" + QYDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zQYDM).val(jsonString);
        }
    }
}

//function SelectBGDD_Muti(BGDDMC, BGDDDM, zBGDDDM) {
//    var data = $("#" + zBGDDDM).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValues', el);
//    $.dialog.open("../../WUC/BGDD/WUC_BGDD.aspx?ryid=1001", {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function () {
//            WUC_BGDD_Return(BGDDMC, BGDDDM, zBGDDDM);
//        }
//    }, false);
//}

//function SelectHYBQ(HYBQMC, HYBQID, zHYBQID, Single, LabelType) {
//    var data = $("#" + zHYBQID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesLabelType', LabelType);
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValuesChoiceOne', Single);
//    $.dialog.open("../../WUC/HYBQ/WUC_HYBQ.aspx?ryid=1001", {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function () {
//            WUC_HYBQ_Return(HYBQMC, HYBQID, zHYBQID);
//        }
//    }, false);
//}

function SelectJHDW_Muti(JHDWMC, JHDWID, zJHDWID) {
    var data = $("#" + zJHDWID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', true);
    $.dialog.open("../../WUC/JHDWTree/WUC_JHDW.aspx?ryid=1001", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_JHDWMuti_Return(JHDWMC, JHDWID, zJHDWID);
        }
    }, false);
}

function WUC_JHDWMuti_Return(JHDWMC, JHDWID, zJHDWID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + JHDWMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "" + contractValues[i].id + ",";
            }
            $("#" + JHDWMC).val(tp_mc);
            $("#" + JHDWID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zJHDWID).val(jsonString);
        }
    }
}

function WUC_BGDD_Return(BGDDMC, BGDDDM, zBGDDDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;


        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + BGDDMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";
                //tp_hf += "" + contractValues[i].Code + ",";
            }
            $("#" + BGDDMC).val(tp_mc);
            $("#" + BGDDDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zBGDDDM).val(jsonString);
        }
    }
}

function WUC_HYBQ_Return(HYBQMC, HYBQID, zHYBQID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + HYBQMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code.substr(2, contractValues[i].Code.length - 2) + "',";
                //tp_hf += "" + contractValues[i].Code + ",";
            }
            $("#" + HYBQMC).val(tp_mc);
            $("#" + HYBQID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zHYBQID).val(jsonString);
        }
    }
}


function SelectQK(QKMC, QKDM, zQKDM, isChooseOne) {
    var data = $("#" + zQKDM).val();
    if (isChooseOne) {
        $.dialog.data('ChoiceOne_QK', isChooseOne);
    }
    $.dialog.data('SelectedQkValues', data);
    $.dialog.open("../../WUC/QK/WUC_QK.aspx?ryid=1001", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_QK_Return(QKMC, QKDM, zQKDM);
        }
    }, false);
}

function WUC_QK_Return(QKMC, QKDM, zQKDM) {
    var tp_return = $.dialog.data('QKValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + QKMC).val(tp_mc);
            var jsonReturn = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonReturn;
            for (var i = 0; i <= contractValues.length - 1; i++) {

                tp_hf += contractValues[i].qkid + ",";
                tp_mc += contractValues[i].Name + ";";
            }
            $("#" + QKMC).val(tp_mc);
            $("#" + QKDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zQKDM).val(jsonString);
        }
    }
}

function SelectHYXQ(HYXQMC, HYXQDM, zHYXQDM) {
    var data = $("#" + zHYXQDM).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValues', el);
    $.dialog.open("../../WUC/XQ/WUC_XQ.aspx?ryid=1001", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_XQ_Return(HYXQMC, HYXQDM, zHYXQDM);
        }
    }, false);
}

function WUC_XQ_Return(HYXQMC, HYXQDM, zHYXQDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + HYXQMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";
            }
            $("#" + HYXQMC).val(tp_mc);
            $("#" + HYXQDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zHYXQDM).val(jsonString);
        }
    }
}

function SelectBMQDY1(BMQMC, BMQID, zBMQID, Single) {
    var data = $("#" + zBMQID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/BMQDY/WUC_BMQDY.aspx?", {
        lock: true, width: 450, height: 470, cancel: false
        , close: function () {
            WUC_BMQDY_Return(BMQMC, BMQID, zBMQID);
        }
    }, false);

}

function WUC_BMQDY_Return(BMQMC, BMQID, zBMQID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + BMQMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].sBMQMC; i++) {
                tp_mc += contractValues[i].sBMQMC + ";";
                tp_hf += contractValues[i].iBMQID + ",";

            }
            $("#" + BMQMC).val(tp_mc);
            $("#" + BMQID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zBMQID).val(jsonString);
        }
    }
}

function SelectLMSHDY(SHMC, LMSHID, zLMSHID, Single) {
    var data = $("#" + zLMSHID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/LMSHDY/WUC_LMSHDY.aspx?", {
        lock: true, width: 450, height: 470, cancel: false
        , close: function () {
            WUC_LMSHDY_Return(SHMC, LMSHID, zLMSHID);
        }
    }, false);

}

function WUC_LMSHDY_Return(SHMC, LMSHID, zLMSHID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + SHMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].sSHMC; i++) {
                tp_mc += contractValues[i].sSHMC + ";";
                tp_hf += contractValues[i].iJLBH + ",";

            }
            $("#" + SHMC).val(tp_mc);
            $("#" + LMSHID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zLMSHID).val(jsonString);
        }
    }
}
//function SelectSHBM(SHBMMC, SHBMDM, zSHBMDM, SHDM, shbmdm) {
//    var data = $("#" + zSHBMDM).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.open("../../WUC/BM/WUC_BM.aspx?shdm=" + SHDM + "&shbmdm=" + shbmdm, {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function () {
//            WUC_SHBM_Return(SHBMMC, SHBMDM, zSHBMDM);

//        }
//    }, false);
//}

function WUC_SHBM_Return(SHBMMC, SHBMDM, zSHBMDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;


        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + SHBMMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Code + ",";
            }
            $("#" + SHBMMC).val(tp_mc);
            $("#" + SHBMDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zSHBMDM).val(jsonString);
        }
    }
}

function WUC_Return(MC, ID, zID, SHDM, SHMC, jqxhr) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var extendname1 = "";
            var extendname2 = "";
            $("#" + MC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                if (tp_mc != "undefined;") {
                    extendname1 += contractValues[i].ExtendName.split(",")[0] + ",";
                    extendname2 += contractValues[i].ExtendName.split(",")[1] + ",";
                }

            }
            if (tp_mc != "undefined;") {
                $("#" + MC).val(tp_mc);
                $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
                $("#" + zID).val(jsonString);
                $("#" + SHDM).val(extendname1.substr(0, extendname1.length - 1));
                $("#" + SHMC).text(extendname2.substr(0, extendname2.length - 1));
            }
            else {
                $("#" + MC).val("");
                $("#" + ID).val("");
                $("#" + zID).val("");
                $("#" + SHDM).val("");
                $("#" + SHMC).text("");
            }
            //xxm start
            if (jqxhr) {
                jqxhr.resolve("ok");
            }
            //stop
            WUC_MD_ReturnCustom();
        }
    }
}

function WUC_MD_ReturnCustom() {
}

//function SelectSQ(SQMC, SQID, zSQID, Single) {
//    var data = $("#" + zSQID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    return $.dialog.open("../../WUC/HYSQ/WUC_HYSQ.aspx", {
//        lock: true, width: 420, height: 470, cancel: false
//          , close: function () {
//              WUC_Return_SQ(SQMC, SQID, zSQID);
//          }
//    }, false);
//}

//function WUC_Return_SQ(MC, ID, zID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            $("#" + MC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Articles;
//            for (var i = 0; i <= contractValues.length - 1; i++) {
//                tp_mc += contractValues[i].Name + ";";
//                tp_hf += contractValues[i].Id + ",";
//            }
//            $("#" + MC).val(tp_mc);
//            $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zID).val(jsonString);
//            $("#TB_PPXQ").val("");
//            $("#HF_XQID").val("");
//            $("#HF_zXQID").val("");

//        }
//    }
//}

//function SelectLQXQ(MC, ID, zID, Single) {
//    var data = $("#" + zID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    //$.dialog.data('IpValuesSQID', SQID);
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    return $.dialog.open("../../WUC/LQXQ/WUC_LQXQ.aspx", {
//        lock: true, width: 450, height: 550, cancel: false
//          , close: function () {
//              WUC_ReturnLQXQ(MC, ID, zID);
//          }
//    }, false);
//}

function WUC_ReturnLQXQ(MC, ID, zID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var tp_qymc = "";
            //var tp_SQID = "";
            $("#" + MC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_qymc += contractValues[i].FreeField + ";";
                tp_hf += contractValues[i].Id + ",";
                //tp_SQID += contractValues[i].Id1 + ",";
            }
            if (tp_mc == "undefined;") {
                $("#" + MC).val("");
                $("#" + ID).val("");
                //$("#TB_SQMC").val(tp_sqmc);
                //$("#HF_SQID").val(tp_SQID.substr(0, tp_SQID.length - 1));
                $("#" + zID).val("");

            }
            else {
                $("#" + MC).val(tp_mc);
                $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
                //$("#TB_SQMC").val(tp_sqmc);
                //$("#HF_SQID").val(tp_SQID.substr(0, tp_SQID.length - 1));
                $("#" + zID).val(jsonString);
            }
            WUC_LQXQ_ReturnCustom();
        }
    }
}

function WUC_LQXQ_ReturnCustom() {

}

function SelectBGDD(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeBGDD", hideField, showField, hideData, dialogUrl, "保管地点", mult, "shortName", "id");
}
function SelectHYBQ(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeHYBQ", hideField, showField, hideData, dialogUrl, "会员标签", mult, "shortName", "actid", ConData);
}
function SelectQY(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeQY", hideField, showField, hideData, dialogUrl, "区域", mult, "shortName", "id");
}
function SelectMD(showField, hideField, hideData, mult, shdm) {
    var dialogUrl = "../../CrmArt/ListMD/CrmArt_ListMD.aspx?";
    if (shdm) {
        dialogUrl += "sSHDM=" + shdm;
    }
    MoseDialogModel("ListMD", hideField, showField, hideData, dialogUrl, "门店信息", mult, "sMDMC", "iJLBH");
};
function SelectTSLX(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ListTSLX/CrmArt_ListTSLX.aspx?";
    MoseDialogModel("ListTSLX", hideField, showField, hideData, dialogUrl, "投诉类型", mult, "sLXMC", "iJLBH", ConData);
};
function SelectMEDIA(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListMEDIA/CrmArt_ListMEDIA.aspx?";
    MoseDialogModel("ListMEDIA", hideField, showField, hideData, dialogUrl, "媒体素材", mult, "sTITLE", "sMEDIA_ID", condData);
};
function SelectNEWS(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListNEWS/CrmArt_ListNEWS.aspx?";
    MoseDialogModel("ListNEWS", hideField, showField, hideData, dialogUrl, "图文素材", mult, "sNAME", "sMEDIA_ID", condData);
};
function TakePhoto(showField, hideField) {
    var dialogUrl = "../../CrmArt/WebCamera/WebCamera.aspx";
    $.dialog.open(dialogUrl, {
        lock: true, width: 340, height: 380, cancel: false,
        drag: true, fixed: false,
        close: function () {
            var bSelected = $.dialog.data('dialogSelected');
            if (bSelected) {
                var imagePath = $.dialog.data("WebCamera");
                $("#" + showField).attr('src', imagePath);
                $("#" + hideField).val(imagePath);
            }
            //else {
            //    $("#" + showField).val("");
            //}
            $.dialog.data("WebCamera", "");
            $.dialog.data('dialogSelected', "");
        }
    }, false);
}

function SelectLB(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListWXLB/CrmArt_ListWXLB.aspx?";

    MoseDialogModel("ListLB", hideField, showField, hideData, dialogUrl, "礼包信息", mult, "sLBMC", "iJLBH");
};



function SelectCLDX(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ArtCLDX/CrmArt_ArtCLDX.aspx";

    MoseDialogModel("ArtCLDX", hideField, showField, hideData, dialogUrl, "处理对象信息", mult, "sMDMC", "iJLBH", ConData, true, 800, 600);
};

function SelectYWY(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ListYWY/CrmArt_ListYWY.aspx";
    MoseDialogModel("ListYWY", hideField, showField, hideData, dialogUrl, "业务员信息", mult, "sYWYMC", "iYWYID", ConData);
};

function SelectCXHD(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListCXHD/CrmArt_ListCXHD.aspx";
    MoseDialogModel("ListCXHD", hideField, showField, hideData, dialogUrl, "促销活动", mult, "sCXZT", "iCXID", condData);
}

function SelectSK(showField, hideField, hideData, condData) {
    var dialogUrl = "../../CrmArt/SK/Crm_Art_SK.aspx";
    MoseDialogModel("DialogSK", hideField, showField, hideData, dialogUrl, "CRM刷卡", "", "sHYK_NO", "iHYID", condData, true, 600, 70);
}

function SelectYHQ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListYHQ/CrmArt_ListYHQ.aspx?";
    MoseDialogModel("ListYHQ", hideField, showField, hideData, dialogUrl, "优惠券信息", mult, "sYHQMC", "iYHQID", condData);
};

//微信标签
function SelectWXBQ(showField, hideField, hideData, wxpid, wxpif, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXBQ/CrmArt_ListWXBQ.aspx?wxpid=" + wxpid + "&wxpif=" + wxpif;
    MoseDialogModel("ListWXBQ", hideField, showField, hideData, dialogUrl, "微信标签信息", mult, "sBQMC", "iTAGID", condData);
};




function SelectBMQDY(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListBMQ/CrmArt_ListBMQ.aspx?";
    MoseDialogModel("ListBMQ", hideField, showField, hideData, dialogUrl, "编码券信息", mult, "sBMQMC", "iBMQID", condData);
};

function SelectBMQLBMC(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListBMQLB/CrmArt_ListBMQLB.aspx?";
    MoseDialogModel("ListBMQLB", hideField, showField, hideData, dialogUrl, "编码券礼包信息", mult, "sLBMC", "iLBID", condData);
};


function SelectBMQFFGZ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListBMQFFGZ/CrmArt_ListBMQFFGZ.aspx?";
    MoseDialogModel("ListWXBMQFFGZ", hideField, showField, hideData, dialogUrl, "编码券发放规则信息", mult, "sGZMC", "iBMQFFGZID", condData);
};

function SelectWXCXHD(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXCXHD/CrmArt_ListWXCXHD.aspx";
    MoseDialogModel("ListWXCXHD", hideField, showField, hideData, dialogUrl, "移动端促销活动", mult, "sCXZT", "iJLBH", condData);
}
////短信个性化符号选择
//function SelectFH(tb, tf) {
//    var dialogUrl = " ../../WebDialog/SelectFH/SelectFH.aspx?autoshow=1";
//    MoseDialogModel("dialogFH", hideField, showField, hideData, dialogUrl, "符号选择", "", "sFH", "iID");
//}


function SelectSH(showField, hideField, hideData, mult, shdm) {
    var dialogUrl = "../../CrmArt/ListSH/CrmArt_ListSH.aspx";
    MoseDialogModel("ListSH", hideField, showField, hideData, dialogUrl, "商户信息", mult, "sSHMC", "sSHDM");
}


function SelectKLX(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListKLX/CrmArt_ListKLX.aspx?";
    MoseDialogModel("ListKLX", hideField, showField, hideData, dialogUrl, "卡类型信息", mult, "sHYKNAME", "iJLBH", condData);
}

function SelectRYXX(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListRY/CrmArt_ListRY.aspx";
    MoseDialogModel("ListRY", hideField, showField, hideData, dialogUrl, "人员信息", mult, "sRYMC", "iJLBH");
};

function SelectKFJL(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListKFJL/CrmArt_ListKFJL.aspx";
    MoseDialogModel("ListKFJL", hideField, showField, hideData, dialogUrl, "经理信息", mult, "sRYMC", "iKFRYID");
};

function SelectGKXX(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListGKXX/CrmArt_ListGKXX.aspx";
    MoseDialogModel("ListGKXX", hideField, showField, hideData, dialogUrl, "顾客信息", mult, "sHY_NAME", "iJLBH");
};

function SelectGHS(showField, hideField, hideData, mult, bj_ty) {
    var dialogUrl = "../../CrmArt/ListGHS/CrmArt_ListGHS.aspx?";
    if (bj_ty == 0 || bj_ty == 1) {
        dialogUrl += "iBJ_TY=" + bj_ty;
    }
    MoseDialogModel("ListGHS", hideField, showField, hideData, dialogUrl, "供货商信息", mult, "sGHSMC", "iJLBH");
};

function SelectFXDW(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeFXDW", hideField, showField, hideData, dialogUrl, "发行单位", mult, "shortName", "id");
}

function SelectSPSB(showField, hideField, hideData, mult, shdm) {
    var dialogUrl = "../../CrmArt/ListSPSB/CrmArt_ListSPSB.aspx?";
    if (shdm) {
        dialogUrl += "sSHDM=" + shdm;
    }
    MoseDialogModel("ListSPSB", hideField, showField, hideData, dialogUrl, "商品商标", mult, "sSBMC", "iSHSBID");
};
function SelectFLMC(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXPPFL/CrmArt_ListWXPPFL.aspx?";
    MoseDialogModel("ListPPFL", hideField, showField, hideData, dialogUrl, "品牌分类", mult, "sFLMC", "iJLBH", condData);
};
function SelectSHBM(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeSHBM", hideField, showField, hideData, dialogUrl, "商户部门", mult, "shortName", "id", condData);
}

function SelectSHSPFL(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeSHSPFL", hideField, showField, hideData, dialogUrl, "商品分类", mult, "shortName", "id", condData);
}
function SelectSHSP(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListSHSP/CrmArt_ListSHSP.aspx";
    MoseDialogModel("ListSHSP", hideField, showField, hideData, dialogUrl, "商品信息", mult, "sSPMC", "iSHSPID", condData);
}
function SelectLPXX(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListLP/CrmArt_ListLP.aspx?";
    MoseDialogModel("ListLP", hideField, showField, hideData, dialogUrl, "礼品信息", mult, "sLPMC", "iLPID");
}

function SelectHYZXX(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListHYZ/CrmArt_ListHYZ.aspx?";
    MoseDialogModel("ListHYZ", hideField, showField, hideData, dialogUrl, "会员组信息", mult, "sGRPMC", "iGRPID");
}
function SelectWX_JFDHLPGZ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXJFDHLPGZ/CrmArt_ListWXJFDHLPGZ.aspx?";
    MoseDialogModel("ListWXJFDHLPGZ", hideField, showField, hideData, dialogUrl, "积分兑换礼品规则", mult, "sGZMC", "iGZID", condData);
};
//function SelectWXMD(showField, hideField, hideData, mult) {
//    var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?";
//    MoseDialogModel("ListWXMD", hideField, showField, hideData, dialogUrl, "微信门店", mult, "sMDMC", "iJLBH");
//};

function SelectWXMD(showField, hideField, hideData, mult, iWXPID) {
    if (iWXPID == "" || iWXPID == undefined || iWXPID == 0 || iWXPID == null) {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?";

    }
    else {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?iWXPID=" + iWXPID;

    }

    MoseDialogModel("ListWXMD", hideField, showField, hideData, dialogUrl, "微信门店", mult, "sMDMC", "iJLBH");
};


function SelectJFGZ(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListJFGZ/CrmArt_ListJFGZ.aspx?";
    MoseDialogModel("ListJFGZ", hideField, showField, hideData, dialogUrl, "补积分规则", mult, "sMC", "iJLBH");
};
function SelectCS(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListCS/CrmArt_ListCS.aspx?";
    MoseDialogModel("ListCS", hideField, showField, hideData, dialogUrl, "城市定义", mult, "sCSMC", "iJLBH");
};
function SelectWXLC(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXLC/CrmArt_ListWXLC.aspx?";
    MoseDialogModel("ListWXLC", hideField, showField, hideData, dialogUrl, "微信楼层", mult, "sNAME", "iJLBH", condData);
};
function SelectWXSBMC(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListWXSB/CrmArt_ListWXSB.aspx?";
    MoseDialogModel("ListWXSB", hideField, showField, hideData, dialogUrl, "微信品牌商标", mult, "sSBMC", "iJLBH");
};

function SelectWXLPFFGZ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXLPFFGZ/CrmArt_ListWXLPFFGZ.aspx?";
    MoseDialogModel("ListWXLPFFGZ", hideField, showField, hideData, dialogUrl, "微信礼品发放规则", mult, "sGZMC", "iJLBH", condData);
};
function SelectWXYHQGZ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXYHQGZ/CrmArt_ListWXYHQGZ.aspx?";
    MoseDialogModel("ListWXYHQGZ", hideField, showField, hideData, dialogUrl, "微信优惠券规则", mult, "sGZMC", "iJLBH", condData);
};
function SelectSQ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListSQ/CrmArt_ListSQ.aspx?";
    MoseDialogModel("ListSQ", hideField, showField, hideData, dialogUrl, "商圈", mult, "sSQMC", "iJLBH");

}
function SelectLMSHLX(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListLMSHLX/CrmArt_ListLMSHLX.aspx?";
    MoseDialogModel("ListLMSHLX", hideField, showField, hideData, dialogUrl, "联盟商户类型", mult, "sLXMC", "iJLBH", condData);
}
//list列表
function SelectKCK(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListKCK/CrmArt_ListKCK.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListKCK', CheckFieldId);
}

function SelectXQ(showField, hideField, hideData, mult, conData) {
    var dialogUrl = "../../CrmArt/ListXQ/CrmArt_ListXQ.aspx";
    MoseDialogModel("ListXQ", hideField, showField, hideData, dialogUrl, "小区信息", mult, "sXQMC", "iXQID", conData);
};


function SelectHYK(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListHYK/CrmArt_ListHYK.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListHYK', CheckFieldId);
}


function SelectWXHYK(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListWXHYK/CrmArt_ListWXHYK.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListWXHYK', CheckFieldId);
}
function SelectLP(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListLP/CrmArt_ListLP.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListLP', CheckFieldId);
}
function SelectTM(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListWXTM/CrmArt_ListWXTM.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListTM', CheckFieldId);
}

function SelectYHQZH(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListYHQZH/CrmArt_ListYHQZH.aspx?";
    var vDialogName = "ListYHQZH";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}


function SelectHYZList(showField, hideField, hideData, mult, conData) {
    var dialogUrl = "../../CrmArt/ListHYZ/CrmArt_ListHYZ.aspx?";
    MoseDialogModel("ListHYZ", hideField, showField, hideData, dialogUrl, "会员组", mult, "sGROUPMC", "iGROUPID", conData);
};



function SelectHYZ(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListHYZ/CrmArt_ListHYZ.aspx?";
    var vDialogName = "ListHYZ";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}



function SelectWXMDList(listName, DataObject, CheckFieldId, mult, iWXPID) {
    if (iWXPID == "" || iWXPID == undefined || iWXPID == 0 || iWXPID == null) {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?";

    }
    else {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?iWXPID" + iWXPID;

    }
    var vDialogName = "ListWXMD";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);

}
function SelectKLXList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListKLX/CrmArt_ListKLX.aspx?";
    var vDialogName = "ListKLX";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);

}
function SelectMZKSKDList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListMZKSKD/CrmArt_ListMZKSKD.aspx?";
    var vDialogName = "ListMZKSKD";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}
function SelectMZKSKD(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListMZKSKD/CrmArt_ListMZKSKD.aspx?";
    var vDialogName = "ListMZKSKD";
    MoseDialogModel("ListMZKSKD", hideField, showField, hideData, dialogUrl, "售卡单信息", mult, "iJLBH", "iJLBH", condData);
}
function SelectMZKSKMX(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListMZKSKMX/CrmArt_ListMZKSKMX.aspx?";
    var vDialogName = "ListMZKSKMX";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}

function SelectMZK(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListMZK/CrmArt_ListMZK.aspx?";
    var vDialogName = "ListMZK";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}
function SelectBMQList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListBMQ/CrmArt_ListBMQ.aspx?";
    var vDialogName = "ListWXBMQ";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}
function SelectYYJLList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListWXYYJL/CrmArt_ListWXYYJL.aspx?";
    var vDialogName = "ListYYJL";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}

function SelectLBList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListWXLB/CrmArt_ListWXLB.aspx?";
    var vDialogName = "ListLB";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}

function SelectYHQList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListYHQ/CrmArt_ListYHQ.aspx?";
    var vDialogName = "ListYHQ";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}
function SelectXZJFDHLP(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListLP/CrmArt_ListXZJFDHLP.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListXZJFDHLP', CheckFieldId);
}
function SelectXZLP(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListLP/CrmArt_ListXZLP.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListXZLP', CheckFieldId);
}
function SelectWXUSER(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListWXUSER/CrmArt_ListWXUSER.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListWXUSER', CheckFieldId);
}

function SelectWXGroup(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListWXGroup/CrmArt_ListWXGroup.aspx";
    MoseDialogModel("ListWXGroup", hideField, showField, hideData, dialogUrl, "微信分组", mult, "sGROUP_NAME", "iGROUPID");
};

function SelectSQD(showField, hideField, hideData, mult, vCZK) {
    vCZK = vCZK || 0;
    var dialogUrl = "../../CrmArt/ListSQD/CrmArt_ListSQD.aspx?";
    dialogUrl += "vCZK=" + vCZK;
    MoseDialogModel("ListSQD", hideField, showField, hideData, dialogUrl, "申请单信息", mult, "iJLBH", "iJLBH");
};

function SelectKH(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListKH/CrmArt_ListKH.aspx?";
    MoseDialogModel("ListKH", hideField, showField, hideData, dialogUrl, "客户信息", mult, "sKHMC", "iJLBH");
}


function SelectWX_ZXHD(NAME, ID, zID, Single, SHDM, SHMC, jqxhr) {
    var data = $("#" + zID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('SHDM', SHDM);
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    return $.dialog.open("../../WX_WUC/WX_ZXHD/WX_ZXHD.aspx", {
        lock: true, width: 420, height: 470, cancel: false
          , close: function () {
              WUC_WX_ZXHDReturn(NAME, ID, zID, SHDM, SHMC, jqxhr);
          }
    }, false);
}
function WUC_WX_ZXHDReturn(NAME, ID, zID, SHDM, SHMC, jqxhr) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var extendname1 = "";
            var extendname2 = "";
            $("#" + NAME).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                if (tp_mc != "undefined;") {
                    extendname1 += contractValues[i].ExtendName.split(",")[0] + ",";
                    extendname2 += contractValues[i].ExtendName.split(",")[1] + ",";
                }

            }
            if (tp_mc != "undefined;") {
                $("#" + NAME).val(tp_mc);
                $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
                $("#" + zID).val(jsonString);
                $("#" + SHDM).val(extendname1.substr(0, extendname1.length - 1));
                $("#" + SHMC).text(extendname2.substr(0, extendname2.length - 1));
            }
            else {
                $("#" + NAME).val("");
                $("#" + ID).val("");
                $("#" + zID).val("");
                $("#" + SHDM).val("");
                $("#" + SHMC).text("");
            }
            //xxm start
            if (jqxhr) {
                jqxhr.resolve("ok");
            }
            //stop
            WUC_MD_ReturnCustom();
        }
    }
}
function SelectKFZY(ZYMC, ZYID, zZYID, Single) {
    var data = $("#" + zZYID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    return $.dialog.open("../../WUC/KFZY/WUC_KFZY.aspx", {
        lock: true, width: 420, height: 470, cancel: false
          , close: function () {
              WUC_Return_KFZY(ZYMC, ZYID, zZYID);
          }
    }, false);
}

function WUC_Return_KFZY(ZYMC, ZYID, zZYID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + ZYMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
            }
            $("#" + ZYMC).val(tp_mc);
            $("#" + ZYID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zZYID).val(jsonString);
        }
    }
}
//function SelectYHQZH(hyid, yhqid, cxid, jsrq, MDFWDM, MDFWMC, YHQJE, JSONSTRING, Single) {
//    var data = $("#" + JSONSTRING).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    if (!isNaN(YHQJE)) {
//        $.dialog.data('IpValuesReturnBalance', YHQJE);
//    }
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    return $.dialog.open("../../WUC/HYKYHQ/WUC_HYKYHQ.aspx?hyid=" + hyid + "&yhqid=" + yhqid + "&cxid=" + cxid + "&jsrq=" + jsrq,
//        {
//            lock: true, width: 740, heigth: 500, cancel: false
//          , close: function () {
//              WUC_Return_YHQZH(MDFWDM, MDFWMC, YHQJE, JSONSTRING);
//          }
//        }, false);
//}

function WUC_Return_YHQZH(MDFWDM, MDFWMC, YHQJE, JSONSTRING) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mdfwdm = "";
            var tp_mdfwmc = "";
            var tp_je = "";
            var tp_hf = "";
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mdfwdm += contractValues[i].sMDFWDM + ",";
                tp_mdfwmc += contractValues[i].sMDFWMC + ";";
                tp_je = contractValues[i].fJE + ";";

            }
            $("#" + MDFWDM).text(tp_mdfwdm.substr(0, tp_mdfwdm.length - 1));
            $("#" + MDFWMC).text(tp_mdfwmc.substr(0, tp_mdfwmc.length - 1));
            $("#" + YHQJE).text(tp_je.substr(0, tp_je.length - 1));
            $("#" + JSONSTRING).val(jsonString);
        }
    }
}
function SelectLMSH(tb, hf, mult) {
    $.dialog.data("dialogLMSH", "");
    $.dialog.data("dialogInput", $("#" + hf).val());
    $.dialog.open("../../CrmLib/SelectLMSH/SelectLMSH.aspx?autoshow=1&mult=" + mult, {
        lock: true,
        width: 500,
        height: 350,
        close: function () {
            LMSH_Return(tb, hf, mult);
        }
    }, false);
}
function LMSH_Return(tb, hf, mult) {
    var lst = $.dialog.data('dialogLMSH');
    var bSelected = $.dialog.data('dialogSelected');
    if (bSelected) {
        if (lst.length == 0) {
            $("#" + tb).val("");
            $("#" + hf).val("");
            WUC_LMSH_ReturnCustom();
        }
        else {
            if (!mult) {
                $("#" + tb).val(lst[0].sSHMC);
                $("#" + hf).val(lst[0].iJLBH);
                WUC_LMSH_ReturnCustom();
            }
            else {
                var tp_mc = "";
                var tp_hf = "";
                for (var i = 0; i <= lst.length - 1; i++) {
                    tp_mc += lst[i].sSHMC + ",";
                    tp_hf += lst[i].iJLBH + ",";
                }
                $("#" + tb).val(tp_mc.substr(0, tp_mc.length - 1));
                $("#" + hf).val(tp_hf.substr(0, tp_hf.length - 1));
                WUC_LMSH_ReturnCustom();
            }
        }
    }
}

function WUC_LMSH_ReturnCustom() {
}
function SelectJJR(JJRMC, JJRID, zJJRID, Single, SelectAll) {
    var data = $("#" + zJJRID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.data('IpValuesSelectAll', SelectAll);
    $.dialog.open("../../WUC/JJR/WUC_JJR.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_JJR_Return(JJRMC, JJRID, zJJRID);
        }
    }, false);
}

function WUC_JJR_Return(JJRMC, JJRID, zJJRID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + JJRMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                //if (tp_return_ChoiceOne) {
                //    tp_hf += contractValues[i].Id;
                //} else {

                //}
            }
            $("#" + JJRMC).val(tp_mc);
            $("#" + JJRID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zJJRID).val(jsonString);
        }
    }
}

function SelectYXHD(HDZT, YXHDID, zYXHDID, Single, SelectAll) {
    var data = $("#" + zYXHDID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.data('IpValuesSelectAll', SelectAll);
    $.dialog.open("../../WUC/YXHD/WUC_YXHD.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_JJR_Return(HDZT, YXHDID, zYXHDID);
        }
    }, false);
}

function WUC_YXHD_Return(HDZT, YXHDID, zYXHDID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + HDZT).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                //if (tp_return_ChoiceOne) {
                //    tp_hf += contractValues[i].Id;
                //} else {

                //}
            }
            $("#" + HDZT).val(tp_mc);
            $("#" + YXHDID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zYXHDID).val(jsonString);
        }
    }
}

function SelectYDDZXHD(ACTNAME, ACTID, zACTID, Single) {
    var data = $("#" + zACTID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    return $.dialog.open("../../WUC/YDDZXHD/WUC_YDDZXHD.aspx", {
        lock: true, width: 420, height: 470, cancel: false
          , close: function () {
              WUC_Return_YDDZXHD(ACTNAME, ACTID, zACTID);
          }
    }, false);
}

function WUC_Return_YDDZXHD(ACTNAME, ACTID, zACTID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + ACTNAME).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
            }
            $("#" + ACTNAME).val(tp_mc);
            $("#" + ACTID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zACTID).val(jsonString);
        }
    }
}
function SelectDYDGZ(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ListDYDGZ/CrmArt_ListDYDGZ.aspx";
    MoseDialogModel("ListDYDGZ", hideField, showField, hideData, dialogUrl, "选择规则", mult, "sGZMC", "iJLBH", ConData, false);
};
function SelectHTGHS(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ListHTGHS/CrmArt_ListHTGHS.aspx";
    MoseDialogModel("ListHTGHS", hideField, showField, hideData, dialogUrl, "供货商信息", mult, "sGHSMC", "sGHSDM");
};