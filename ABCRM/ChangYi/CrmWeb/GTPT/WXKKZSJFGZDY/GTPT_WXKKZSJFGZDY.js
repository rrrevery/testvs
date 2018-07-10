vUrl = "../GTPT.ashx";

var irow = 0;
vGZLX = GetUrlParam("gzlx");
var vKLXColumnNames;
var vKLXColumnModel;

function InitGrid() {

    vColumnNames = ['iHYKTYPE', '卡类型', ];
    vColumnModel = [
         { name: 'iHYKTYPE', hidden: true },
         { name: 'sHYKNAME', width: 100},

    ];
    vKLXColumnNames = ['iHYKTYPE', '会员卡类型', 'iLBID', '礼包名称'];
    vKLXColumnModel = [
            { name: "iHYKTYPE", hidden: true },
              { name: "sHYKNAME", width: 100, },
              { name: "iLBID", hidden: true },
              { name: "sLBMC", width: 100, },
    ];

}
$(document).ready(function () {
    DrawGrid("list_KLX", vKLXColumnNames, vKLXColumnModel, false);

    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#QDR").show();
    $("#QDSJ").show();

    $("#AddKLX").click(function () {
        if ($("#TB_KSRQ").val() == "") {
            ShowMessage("请输入开始日期");
            return false;
        }
        if ($("#TB_JSRQ").val() == "") {
            ShowMessage("请选择结束日期");
            return false;
        }
        if ($("#TB_XZSL").val() == "") {
            ShowMessage("请输入限制数量");
            return false;
        }
        if ($("#TB_LJYXQ").val() == "") {
            ShowMessage("请输入领奖有效期");
            return false;
        }
        var DataArry = new Object();
        SelectKLXList('list', DataArry, 'iHYKTYPE', false);


    });

    $("#DelKLX").click(function () {
        var rows = $("#list").datagrid("getSelections");
        var copyRows = [];
        for (var j = 0; j < rows.length; j++) {
            copyRows.push(rows[j]);
        }
        for (var i = 0; i < copyRows.length; i++) {
            var index = $("#list").datagrid('getRowIndex', copyRows[i]);
            $("#list").datagrid('deleteRow', index);

            var rowsLB = $("#list_KLX").datagrid("getRows");
            for (var k = rowsLB.length - 1; k >= 0; k--) {
                if (rowsLB[k].iHYKTYPE == copyRows[i].iHYKTYPE) {
                    $("#list_KLX").datagrid('deleteRow', k);
                }
            }
        }

    });

    $("#AddLB").click(function () {
        var rows = $('#list').datagrid('getSelections');
        if (rows.length <= 0) {
            ShowMessage("还未选中要添加礼品卡类型");
            return;
        }
        var DataArry = new Object();
        var checkRepeatField = ["iHYKTYPE"];
        SelectLBList('list_KLX', DataArry, checkRepeatField,false);
    });
    $("#DelLB").click(function () {
        DeleteRows("list_KLX");
    });






});





function SetControlState() {
    $("#B_Start").show();
    $("#B_Stop").show();

}

function IsValidData() {
    
    if ($("#TB_XZSL").val() == "") {
        ShowMessage("请输入限制数量");

        return false;
    }
    if ($("#TB_KSRQ").val() == "") {
        ShowMessage("请选择开始日期");
        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请选择结束日期");
        return false;
    }
    if ($("#TB_YXQ").val() == "") {
        ShowMessage("请选择有效期");
        return false;
    }
    if ($("#TB_XZSL").val() == "" || isNaN($("#TB_XZSL").val()) || $("#TB_XZSL").val() <= 0) {
        ShowMessage("请填写正确的限制赠送人数！");
        return false;
    }
    if ($("#TB_WXZY").val() == "") {
        ShowMessage("请填写微信摘要，此项为赠送时提示语");
        return false;
    }
    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        ShowMessage("开始日期不得大于结束日期");
        return false;
    }
    var rowData2 = $("#list").datagrid("getData").rows;
    if (rowData2.length == 0) {
        ShowMessage("请添加礼包信息表");
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iXZSL = $("#TB_XZSL").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.dLJYXQ = $("#TB_YXQ").val();
    Obj.sWXZY = $("#TB_WXZY").val();
    Obj.iLX = vGZLX;


    var lst1 = new Array();
    lst1 = $("#list_KLX").datagrid("getData").rows;
    Obj.itemTable1 = lst1;

    var lst2 = new Array();
    lst2 = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst2;


    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
  
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#TB_XZSL").val(Obj.iXZSL);
    $("#TB_WXZY").val(Obj.sWXZY);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_YXQ").val(Obj.dLJYXQ);
 
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $('#list_KLX').datagrid('loadData', Obj.itemTable1, "json");
    $('#list_KLX').datagrid("loaded");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sSHRMC);
    $("#HF_ZXR").val(Obj.iSHR);
    $("#LB_ZXRQ").text(Obj.dSHRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDRQ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)

}
function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    if (listName == "list") {
        if (rows.length == 0) {
            $('#' + listName).datagrid('loadData', lst, "json");
        }
        else {
            for (var i = 0; i < lst.length; i++) {
                if (CheckReapet(array, CheckFieldId, lst[i])) {//[CheckFieldId]
                    $('#' + listName).datagrid('appendRow', lst[i]);
                }
            }
        }
    }
    else {
        for (var i = 0; i < lst.length; i++) {
            var rows = $('#list').datagrid('getSelections');
            for (var j = 0; j < rows.length; j++) {
                lst[i].iHYKTYPE = rows[j].iHYKTYPE;
                lst[i].sHYKNAME = rows[j].sHYKNAME;
                if (CheckReapet(rows[j].iHYKTYPE)) {
                    $('#list_KLX').datagrid('appendRow', {
                        iLBID: lst[i].iLBID,
                        sLBMC: lst[i].sLBMC,
                        iHYKTYPE:lst[i].iHYKTYPE,
                        sHYKNAME: lst[i].sHYKNAME,
                    });
                }
            }
        }
    }
}
function CheckReapetklx(hyktype) {
    var boolReapet = true;
    var lst = $("#list").datagrid("getRows");
    var d = lst.length;
    //var row = $("#list2").datagrid("getRows").rows;

    if (d > 0) {
        for (var i = 0; i < d ; i++) {
            var rowData = lst[i];

            if (rowData.iHYKTYPE == hyktype) {
                boolReapet = false;
            }
        }
    }
    return boolReapet;

}


function onClickCell(index, field) {
    if (this.id == "list") {
        if (endEditing() && vProcStatus != cPS_BROWSE) {
            $('#list').datagrid('selectRow', index)
                    .datagrid('editCell', { index: index, field: field });
            editIndex = index;

            var ed = $('#list').datagrid('getEditor', { index: index, field: field });
            if (ed) {
                $(ed.target).bind("keypress", function (event) {
                    if (event.keyCode == 13) {
                        if ($('#list').datagrid('validateRow', editIndex)) {
                            $('#list').datagrid('endEdit', editIndex);
                            editIndex = undefined;
                            return true;
                        }
                    }
                })
            }
        }
    }
}
