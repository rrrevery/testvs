vUrl = "../GTPT.ashx";
var irow = 0;
function InitGrid() {
    vColumnNames = ['序号', '积分下限', '积分上限', '返利倍率'];
    vColumnModel = [
            { name: 'iXH', width: 100, editor: 'text', editrules: { required: true, number: true, minValue: 1 }, },
            { name: 'fJFXX', width: 100, editor: 'text', editrules: { required: true, number: true, minValue: 1 }, },
            { name: 'fJFSX', width: 200, editor: 'text', editrules: { required: true, number: true, minValue: 1 }, },
            { name: 'fFLBL', width: 200, editor: 'text', editrules: { required: true, number: true, minValue: 1 }, },
    ];

}
$(document).ready(function () {
    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", true);
    });
    BFButtonClick("TB_YHQMC", function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", true);
    });
    $("#DDL_YHQCL").combobox("setValue", "");
    $("#a").hide();
    $("#b").hide();
    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#QDR").show();
    $("#QDSJ").show();
    $("#B_Start").show();
    $("#B_Stop").show();

    $("#AddItem").click(function () {
        if (endEditing()) {
            $('#list').datagrid('appendRow', {});
            editIndex = $('#list').datagrid('getRows').length - 1;
            $('#list').datagrid('selectRow', editIndex)
                    .datagrid('beginEdit', editIndex);
           
        }
        $('#list').datagrid('acceptChanges');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    BFButtonClick("TB_MDMC", function () {
        SelectWXMD("TB_MDMC", "HF_MDID", "zHF_MDID", true,iWXPID);
    });
    $("#DDL_YHQCL").combobox({
        onSelect: selCom,
    });
})
function SetControlState() {
    if ($("#LB_ZZRMC").text() != "") {
        document.getElementById("B_Stop").disabled = true;
    }
    $("#B_Start").show();
    $("#B_Stop").show();
}
function selCom(record) {
    if (record.value == 0) {
        $("#a").show();
        $("#b").hide();
    }
    if (record.value == 1) {
        $("#a").hide();
        $("#b").show();
    }
}
function IsValidData() {

    var row = $("#list").datagrid("getData").rows;
    if ($("#TB_KSRQ").val() == "") {
        ShowMessage("请选择开始日期");
        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请选择结束日期");
        return false;
    }
    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        ShowMessage("开始日期不得大于结束日期");
        return false;
    }
    if (row.length == 0) {
        ShowMessage('请输入定义返还比例！');
        return false;
    }
    else {
        for (i = 0; i < row.length  ; i++) {
            var rowData = row[i];
            if (rowData.fFLBL != "" && rowData.fFLBL!=undefined) {
                if (rowData.fFLBL < 0) {
                    ShowMessage("返利倍率不能为负");
                    return false;
                }
                if (rowData.fFLBL == 0) {
                    ShowMessage("返利倍率不能为零");
                    return false;
                }
            }
            if (rowData.iXH == "" || rowData.iXH == undefined) {
                ShowMessage("请输入序号");
                return false;
            }
            if (rowData.fJFXX == "" || rowData.fJFXX == undefined) {
                ShowMessage("请输入积分下限");
                return false;
            }
            if (rowData.fJFSX == "" || rowData.fJFSX == undefined) {
                ShowMessage("请输入积分上限");
                return false;
            }
            if (parseFloat(rowData.fJFXX) > parseFloat(rowData.fJFSX)) {
                ShowMessage("积分下限超过积分上限");
                return false;
            }
            if (rowData.fFLBL == "" || rowData.fFLBL == undefined) {
                ShowMessage("请输入返利倍率");
                return false;
            }
        }
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iCLFS = $("[name='CLFS']:checked").val();
    Obj.iJFCLFS = $("[name='JFCLFS']:checked").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.iYHQFS = $("#DDL_YHQCL").combobox('getValue');
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBZ = $("#TB_BZ").val();
    Obj.iSYDAY = $("#TB_SYDAY").val();
    if (Obj.iSYDAY == "")
        Obj.iSYDAY = "0";
    Obj.dSYJSRQ = $("#TB_SYJSRQ").val();
    //Obj.iBJ_KCDYJF = $("#CB_BJ_DY")[0].checked ? "1" : "0";
    var lst = new Array();
    lst=$("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("[name='CLFS'][value='" + Obj.iCLFS + "']").prop("checked", true);
    $("[name='JFCLFS'][value='" + Obj.iJFCLFS + "']").prop("checked", true);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_BZ").val(Obj.sBZ);
    $("#DDL_YHQCL").combobox('select', Obj.iYHQFS);
    if (Obj.iYHQFS == 0) {
        $("#a").show();
        $("#TB_SYJSRQ").val(Obj.dSYJSRQ);
    }
    if (Obj.iYHQFS == 1) {
        $("#b").show();
        $("#TB_SYDAY").val(Obj.iSYDAY);
    }
    //$("#CB_BJ_DY").prop("checked", Obj.iBJ_KCDYJF == "1" ? true : false);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)



}
function StartClick() {
    ShowYesNoMessage("启动本单将自动终止此卡类型启动状态单据，是否启动?", function () {
        if (posttosever(SaveDataBase(), vUrl, "Start") == true) {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH);
            SetControlBaseState();
        }
    });
};