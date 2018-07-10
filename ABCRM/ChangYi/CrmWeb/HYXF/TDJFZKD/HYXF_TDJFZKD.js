vUrl = "../HYXF.ashx";
vDJLX = GetUrlParam("djlx");
function InitGrid() {

    vColumnNames = ["规则编号", GetName_JFBS(), GetName_BSFS(), "GRPID", "会员组", ];
    vColumnModel = [
            { name: 'iGZBH', },
			{ name: 'fJFBS', editor: 'text', },
            {
                name: 'iBSFS', editor: {
                    type: 'combobox',
                    options: {
                        data: GetString_BSFS(), /* JSON格式的对象数组 */
                        valueField: "value",/* value是JSON对象的属性名 */
                        textField: "text",/* name是JSON对象的属性名 */
                        editable: false,
                        panelHeight: 70,
                        //required: true,
                    }
                },
                formatter: BSFStext,
            },
            { name: 'iGRPID', hidden: true, },
            { name: 'sGRPMC', editor: 'text',},
    ];
};



$(document).ready(function () {
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    $("#TB_HYZMC").click(function () {
        SelectHYZXX("TB_HYZMC", "HF_HYZID", "zHF_HYZID", true);
    });

    if (parseInt(vDJLX) == 1) {
        $("#SJJFBS").hide();
    }
    $("#AddItem").click(function () {
        if ($("#HF_HYZID").val() == "") {
            ShowMessage("请先选择会员组", 3);
            return false;
        }
        $("#list").datagrid('appendRow', {
            iGZBH: $("#list").datagrid("getRows").length + 1,
            iGRPID: $("#HF_HYZID").val(),
            sGRPMC: $("#TB_HYZMC").val(),
        });
    })
    $("#DelItem").click(function () {
        DeleteRows("list");
    });


    if (vProcStatus == cPS_BROWSE) {
        $("#pager_left").hide();
    } else { $("#pager_left").show(); }
});

function SetControlState() {
    $("#B_Stop").show();
    $("#ZZR").show();
    $("#ZZSJ").show();
    //由于审核后直接启动，此处需要重写
    if ($("#HF_ZXR").val() != "" && $("#HF_ZZR").val() == "0") {
        $("#B_Stop").prop("disabled", false);
    } else {
        $("#B_Stop").prop("disabled", true);
    }

    if (vProcStatus == cPS_BROWSE) {
        $("#pager_left").hide();
    } else { $("#pager_left").show(); }
}

function IsValidData() {
    if ($("#TB_MDMC").val() == "") {
        ShowMessage("请选择门店", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.iMDID = $("#HF_MDID").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iDJLX = vDJLX;
    Obj.iBJ_BQJFBS = $("#BJ_BQJFBS")[0].checked ? "1" : "0";

    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);

    $("#BJ_BQJFBS").prop("checked", Obj.iBJ_BQJFBS == "1" ? true : false);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);

    $("#list").datagrid('loadData', Obj.itemTable, "json");
    $("#list").datagrid('loaded');

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
}

function GetName_JFBS() {
    if (vDJLX == 0) {
        return "积分倍数";
    } else {
        return "折扣率（0-1）";
    }
}

function GetName_BSFS() {
    if (vDJLX == 0) {
        return "倍数方式";
    } else {
        return "折扣方式";
    }
}

function GetString_BSFS() {
    if (vDJLX == 0) {
        return [{ "value": "0", "text": "倍数之和" }, { "value": "1", "text": "取最大倍数" }]; 
    } else {
        return [{ "value": "0", "text": "最大折扣" }, { "value": "1", "text": "折上折" }];  
    }
}

function BSFStext(value, row) {
    var comboboxData = GetString_BSFS();
    for (var i = 0; i < comboboxData.length; i++) {
        if (comboboxData[i].value == value) {
            return comboboxData[i].text;
        }
    }
    return row.value;
}

function GetString_GRP() {
    var result = "";
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillHYGRP",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'ys' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                result += data[i].iGRPID + ":" + data[i].sGRPMC + ";";
            }
        },
        error: function (data) {
            ;
        }
    });
    return result.substr(0, result.length - 1);
}

function WUC_HYZ_Return() {
    var rowid = $("#list").getGridParam("selrow");
    var rowdata = $("#list").getRowData(rowid);

    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            //$("#TB_HYZ").val(tp_mc);
            $("#" + rowid + "_sGRPMC").val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                if (tp_return_ChoiceOne) {
                    tp_mc += contractValues[i].Name;
                    tp_hf += contractValues[i].Id;
                } else {
                    tp_mc += contractValues[i].Name + ";";
                    tp_hf += contractValues[i].Id + ";";
                }
            }
            //$("#TB_HYZ").val(tp_mc);
            //$("#HF_HYZ").val(tp_hf);
            $("#list").setRowData(rowid, { iGRPID: tp_hf });
            $("#" + rowid + "_sGRPMC").val(tp_mc);
            $("#zHF_HYZ").val(jsonString);
        }
    }
}