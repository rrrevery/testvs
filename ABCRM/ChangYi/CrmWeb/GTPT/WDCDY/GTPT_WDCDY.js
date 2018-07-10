
vUrl = "../GTPT.ashx";

var irow = 0;
var icol = 0;
var KLX = "";
var Name = "";
var id = 0;
var irow2 = 0;
var icol2 = 0;
var a;
var b;

function InitGrid() {
    vColumnNames = ['ID', '名称', '页号', '类型'];
    vColumnModel = [
             { name: 'iID', width: 20, hidden: true },
          { name: 'sMC', width: 150, },
          { name: 'iPAGE_ID', width: 100, hidden: true },
          {
              name: 'iBJ_TYPE', editor: {
                  type: 'combobox',
                  options: {
                      data: GetName_TYPE(), /* JSON格式的对象数组 */
                      valueField: "value",/* value是JSON对象的属性名 */
                      textField: "text",/* name是JSON对象的属性名 */
                      editable: false,
                      panelHeight: 70,
                      //required: true,
                  }
              },
              formatter: BSFStext,
          },
    ];


}
function GetName_TYPE() {
    return [{ "value": "0", "text": "单选" }, { "value": "1", "text": "多选" }];

}

function BSFStext(value, row) {
    var comboboxData = GetName_TYPE();
    for (var i = 0; i < comboboxData.length; i++) {
        if (comboboxData[i].value == value) {
            return comboboxData[i].text;
        }
    }
    return row.value;
}
function IsValidData() {
    if ($("#TB_DCZT").val() == "") {
        ShowMessage("请输入调查主题");
        return false;
    }
    if (isNaN($("#TB_XZSL").val()) || $("#TB_XZSL").val() == "") {
        ShowMessage("请输入正确的人数限制");
        return false;
    }

    if ($("#TB_WXZY").val() == "") {
        ShowMessage("请输入微信提示");
        return false;
    }

    if ($("#TB_JLMS").val() == "") {
        ShowMessage("请输入奖励描述");
        return false;
    }

    if ($("#TB_FINISHNOTE").val() == "") {
        ShowMessage("请输入调查问券完成提示");
        return false;
    }

    if (GetSelectValue("DDL_QD") == null) {
        ShowMessage("请选择渠道");
        return false;
    }
    //if (!IsDateCheck($("#TB_KSRQ").val(), $("#TB_JSRQ").val()))
    //    return false;


    //if ($("#TB_LBMC").val() != "") {
    //    if (!IsDate($("#TB_LJYXQ").val())) {
    //        ShowMessage("请输入领奖有效期");
    //        return false;
    //    }

    //}



    return true;
}

function SetControlState() {

    $("#B_Start").show();
    $("#B_Stop").show();

    if ($("#LB_ZXRMC").text() != "") {
        $("#QDR").show();
        $("#QDSJ").show();

    }
    if ($("#LB_QDRMC").text() != "") {
        $("#ZZR").show();
        $("#ZZSJ").show();
    }
    if ($("#LB_ZZRMC").text() != "") {
        document.getElementById("B_Stop").disabled = true;
    }

}


$(document).ready(function () {
    BFButtonClick("TB_LBMC", function () {
        SelectLB("TB_LBMC", "HF_LBID", "zHF_LBID", true);
    });
    document.getElementById("B_Update").onclick = function () {
        vProcStatus = cPS_MODIFY;
        SetControlBaseState();
    };

    $("#Add").click(function () {
        var DataArry = new Object();

        SelectTM('list', DataArry, 'iID');




    });


    $("#Del").click(function () {
        DeleteRows("list");
    });



})

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    if (rows.length == 0) {
        //$('#' + listName).datagrid('loadData', lst, "json");
        for (var i = 0; i < lst.length; i++) {
            lst[i].iPAGE_ID = 1;
            $('#' + listName).datagrid('appendRow', lst[i]);
        }
    }
    else {
        for (var i = 0; i < lst.length; i++) {
            if (CheckReapet(array, CheckFieldId, lst[i])) {//[CheckFieldId]
                lst[i].iPAGE_ID = 1;
                $('#' + listName).datagrid('appendRow', lst[i]);


            }
        }
    }
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sDCZT = $("#TB_DCZT").val();
    Obj.sZY = $("#TB_WXZY").val();
    Obj.iXZSL = $("#TB_XZSL").val();
    Obj.iLBID = $("#HF_LBID").val();
    Obj.dLJYXQ = $("#TB_LJYXQ").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iCHANNELID = $("#DDL_QD").find("option:selected").val();
    Obj.sJLMS = $("#TB_JLMS").val();
    Obj.sFINISHNOTE = $("#TB_FINISHNOTE").val();
    var lst1 = new Array();
    lst1 = $("#list").datagrid("getRows");
    Obj.itemTable1 = lst1;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}


function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_DCZT").val(Obj.sDCZT);
    $("#TB_WXZY").val(Obj.sZY);
    $("#TB_XZSL").val(Obj.iXZSL);
    $("#HF_LBID").val(Obj.iLBID);
    $("#TB_LBMC").val(Obj.sLBMC)
    $("#TB_LJYXQ").val(Obj.dLJYXQ);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);

    $("#TB_JLMS").val(Obj.sJLMS);
    $("#DDL_QD").val(Obj.iCHANNELID);
    $("#TB_FINISHNOTE").val(Obj.sFINISHNOTE);

    $('#list').datagrid('loadData', Obj.itemTable1, "json");
    $('#list').datagrid("loaded");


    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)

}





