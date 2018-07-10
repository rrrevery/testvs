vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");
var ListName = "";

function InitGrid() {
    vColumnNames = ["序号", "序号", "标签项目值", "标签名称"];
    vColumnModel = [
            { name: "iXH", width: 200, hidden: true },
            { name: "sXH", width: 200, },
            { name: "iSJNR", width: 150, hidden: true },
            { name: "sLABELVALUE", width: 450, },
    ];
};
$(document).ready(function () {
    $("#ZXR").hide();
    $("#ZXRQ").hide();
    $("#B_Exec").hide();

    $("#TB_HYBQ").click(function () {
        var condData = new Object();
        condData["iLABELLX"] = 1;
        SelectHYBQ("TB_HYBQ", "HF_HYBQ", "zHF_HYBQ", false, condData);
        ListName = ""
    });
    $("#TB_XHYBQ").click(function () {
        var condData = new Object();
        condData["iLABELLX"] = 1;
        SelectHYBQ("TB_XHYBQ", "HF_XHYBQ", "zHF_XHYBQ", false, condData);
        ListName = ""
    });

    $("#Add_BQ").click(function () {
        if ($("#TB_TJSL").val() == "") {
            ShowMessage('请输入统计数量!', 3);
            return;
        }
        var condData = new Object();
        condData["iLABELLX"] = 0;
        SelectHYBQ("", "", "", true, condData);
        ListName = "list";
    });

    $("#Del_BQ").click(function () {
        DeleteRows("list");
    });

});

function MoseDialogCustomerReturn(dialogName, lst, showField) {
    if (ListName == "list") {
        for (var i = 0; i <= lst.length - 1; i++) {
            if (CheckRepeat(lst[i].actid, $("#DDL_LabelGroup").val())) {
                $("#" + ListName).datagrid("appendRow", {
                    iSJNR: lst[i].actid,
                    sLABELVALUE: lst[i].shortName,
                    iXH: $("#DDL_LabelGroup").val(),
                    sXH: $("#DDL_LabelGroup [value='" + $("#DDL_LabelGroup").val() + "']").text()
                });
            }
        }
    }
}



function CheckRepeat(param, labelGroup) {
    var rowData = $("#list").datagrid("getRows");
    for (var j = 0; j < rowData.length  ; j++) {
        if (parseInt(param) == rowData[j].iSJNR && parseInt(labelGroup) == parseInt(rowData[j].iXH)) {
            return false;
        }
    }
    return true;
}


//function SetControlState() {
//    ;
//}

//function InsertClick() {
//    PageDate_Clear();
//    vProcStatus = cPS_ADD;
//    $("#LB_DJRMC").text(sDJRMC);
//    $("#HF_DJR").val(iDJR);
//    SetControlBaseState();
//    InsertClickCustom();
//    $("#List").jqGrid("clearGridData");


//};


//function WUC_HYBQ_Return1(HYBQMC, HYBQID, zHYBQID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            $("#" + HYBQMC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Depts;
//            for (var i = 0; i <= contractValues.length - 1; i++) {
//                tp_mc += contractValues[i].Name + ";";
//                tp_hf += "" + contractValues[i].Code.substr(2, contractValues[i].Code.length - 2) + ",";
//                //tp_hf += "" + contractValues[i].Code + ",";
//            }
//            $("#" + HYBQMC).val(tp_mc);
//            $("#" + HYBQID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zHYBQID).val(jsonString);
//        }
//    }
//}
//function WUC_HYBQ_Return(HYBQMC, HYBQID, zHYBQID) {
//    var tp_hf = "";

//    var tp_return = $.dialog.data('IpValuesReturn');//接收的应该是转换成对象，或者数组
//    if (tp_return) {

//        var contractValues = new Array();
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {

//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Depts;
//        }
//        if (contractValues.length > 0) {
//            for (var i = 0; i < contractValues.length; i++) {
//                if (contractValues[i].Code != "") {
//                    tp_hf = contractValues[i].Code.substr(2, contractValues[i].Code.length - 2);
//                    if (CheckRepeat(parseInt(tp_hf), $("#DDL_LabelGroup").val()) == false) {
//                        $("#List").addRowData($("#List").getGridParam("reccount"), {
//                            iSJNR: parseInt(tp_hf),
//                            iXH: $("#DDL_LabelGroup").val(),
//                            sXH: $("#DDL_LabelGroup [value='" + $("#DDL_LabelGroup").val() + "']").text(),
//                            sLABELVALUE: contractValues[i].Name
//                        });
//                    }
//                    else {
//                        art.dialog({ lock: true, time: 2, content: "选中的标签已存在" });
//                    }

//                }
//            }
//        }
//        //将添加到表中

//        $.dialog.data('IpValuesReturn', "");//清空数据

//    }
//}
function IsValidData() {
    var TJSL = $("#TB_TJSL").val();

    if ($("#HF_HYBQ").val() == "") {
        ShowMessage('请选择标签', 3);
        return false;
    }
    if (TJSL == "") {
        ShowMessage('请输入条件数量!', 3);
        return false;
    }
    if ($("#TB_TJYF").val() == "") {
        ShowMessage('请输入统计月份!', 3);
        return false;
    }
    if ($.trim($("#TB_YXYF").val()) == "") {
        if (($("#TB_XHYBQ").val()) != "") {
            ShowMessage("新有效月份为空，继承标签也应该为空", 3);
            return false;
        }
    }
    var conditionArray = new Array();
    var rows = $("#list").datagrid("getRows");
    for (var i = 0; i < rows.length; i++) {
        var tp_flag = false;
        for (var j = 0; j < conditionArray.length; j++) {
            if (parseInt(conditionArray[j]) == parseInt(rows[i].iXH)) {
                tp_flag = true;
            }
        }
        if (tp_flag == false) {
            conditionArray.push(rows[i].iXH);
        }
    }

    if (conditionArray.length != parseInt(TJSL)) {
        ShowMessage("选中条件组与条件数量不符!", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_HYBQ").val();
    Obj.iLABELID = $("#HF_HYBQ").val();
    Obj.iYXYF = $("#TB_YXYF").val()

    Obj.iNEW_LABELID = $("#HF_XHYBQ").val();

    Obj.iTJYF = $("#TB_TJYF").val();
    Obj.iTJSL = $("#TB_TJSL").val();
    Obj.iSTATUS = $("#CB_TY").prop("checked") ? "-1" : "0";

    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iLABELID);
    $("#HF_HYBQ").val(Obj.iLABELID);
    $("#TB_HYBQ").val(Obj.sLABEL_VALUE)
    $("#TB_YXYF").val(Obj.iYXYF)
    $("#HF_XHYBQ").val(Obj.iNEW_LABELID);
    $("#TB_XHYBQ").val(Obj.sNEW_LABELVALUE);
    $("#CB_TY").prop("checked", Obj.iSTATUS == "-1");

    $("#TB_TJYF").val(Obj.iTJYF);
    $("#TB_TJSL").val(Obj.iTJSL);
    FillLabelGroup();

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
};

function FillLabelGroup() {
    var selectName = $("#DDL_LabelGroup");
    selectName.append("<option></option>");
    selectName.empty();
    if ($("#TB_TJSL").val() != "" && !isNaN($("#TB_TJSL").val())) {
        var conditionLength = parseInt($("#TB_TJSL").val());
        for (var i = 0; i < conditionLength; i++) {
            selectName.append("<option  value='" + i + "'>第" + parseInt(i + 1) + "组</option>");
        }
    }
}
