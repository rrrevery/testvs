vUrl = "../HYXF.ashx";
var HYKNO = GetUrlParam("HYKNO");

function InitGrid() {
    vColumnNames = ["商品代码", "商品名称", "部门代码", "部门名称", "积分（原）", "调整积分", "SHSPID"],
    vColumnModel = [
          { name: "sSPDM", hidden: true },
          { name: "sSPMC", width: 90 },
          { name: "sBMDM", width: 60 },
          { name: "sBMMC", width: 90 },
          { name: "fJF", width: 60 },
          { name: "fTZJF", width: 60, editor: 'text', editrules: { required: true, number: true, minValue: 1 } },
          { name: "iSHSPID", hidden: true },

    ];
};


$(document).ready(function () {


    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYKNO").attr("readonly", "readonly");
    }


    // FillMD($("#DDL_MDID"));

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    $("#AddItem").click(function () {
        if ($("#HF_MDID").val() == "") {
            ShowMessage("请先选择门店", 3);
            return;
        }
        if ($("#TB_HYKNO").val() == "") {
            ShowMessage("请先输入会员卡号", 3);
            return;
        }
        if ($("#TB_SKTNO").val() == "") {
            ShowMessage("请先输入收款台号", 3);
            return;
        }
        if ($("#TB_XSJYBH").val() == "") {
            ShowMessage("请先输入交易号", 3);
            return;
        }
        var str = GetHYXF($("#TB_XSJYBH").val(), $("#HF_MDID").val(), $("#TB_SKTNO").val(), $("#HF_HYID").val());
        var Obj = JSON.parse(str);
        $('#list').datagrid('loadData', { total: 0, rows: [] });
        for (var i = 0; i < Obj.itemTable.length; i++) {
            $('#list').datagrid('appendRow', {
                sSPDM: Obj.itemTable[i].sSPDM,
                sSPMC: Obj.itemTable[i].sSPMC,
                sBMDM: Obj.itemTable[i].sBMDM,
                sBMMC: Obj.itemTable[i].sBMMC,
                fJF: Obj.itemTable[i].fJF,
                fTZJF: 0,
                iSHSPID: Obj.itemTable[i].iSHSPID,
            });
        }

    });

});


function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val());
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号", 3);
            $("#TB_HYKNO").val("");
            $("#HF_HYID").val("");
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.sHYK_NO == "") {
            ShowMessage("没有找到卡号", 3);
            $("#TB_HYKNO").val("");
            $("#HF_HYID").val("");
            $('#list').datagrid('loadData', [], "json");
            $('#list').datagrid("loaded");
            return;
        }

        $("#LB_HYKTYPE").text(Obj.sHYKNAME);
        $("#HF_HYID").val(Obj.iHYID);
    }
}
function SetControlState() {

}

function IsValidData() {
    if ($("#HF_HYID").val() == "") {
        ShowMessage("会员卡号不能为空", 3);
        return false;
    }
    if ($("#HF_MDID").val() == "") {
        ShowMessage("门店不能为空", 3);
        return false;
    }
    if ($("#TB_SKTNO").val() == "") {
        ShowMessage("收款台号不能为空", 3);
        return false;
    }
    var rows = $("#list").datagrid("getData").rows;
    if (rows.length < 1) {
        ShowMessage("请先选择交易明细", 3);
        return false;
    }
    for (var i = 0; i < rows.length; i++) {

        if (rows[i].fTZJF == "") {
            ShowMessage("请输入调整积分", 3);
            return false;
        }
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sHYKNO = $("#TB_HYKNO").val();
    Obj.sSKTNO = $("#TB_SKTNO").val();
    Obj.iXSJYBH = $("#TB_XSJYBH").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sZY = $("#TB_ZY").val();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    var total = 0;
    for (var i = 0; i < lst.length; i++) {
        total += parseFloat(lst[i].fTZJF);
    }
    Obj.fTZJF = total
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_HYID").val(Obj.iHYID);
    $("#TB_HYKNO").val(Obj.sHYKNO);
    $("#TB_SKTNO").val(Obj.sSKTNO);
    $("#TB_XSJYBH").val(Obj.iXSJYBH);
    $("#TB_ZY").val(Obj.sZY);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}
