vUrl = "../HYKGL.ashx";
function InitGrid() {
    vColumnNames = ['有效人数', '奖励数量'];
    vColumnModel = [
            { name: 'iYXRS', width: 60, editor: 'text' },
            { name: 'fJLSL', width: 60, editor: 'text' },
    ];

    vKQColumnNames = ["会员组ID", "会员组名称", ];
    vKQColumnModel = [
              { name: "iGRPID",  },
              { name: "sGRPMC",  },
    ];
}
function SetControlState() {
    $("#B_Stop").show();
    $("#ZZR").show();
    $("#ZZSJ").show();
    
    if ($("#HF_ZXR").val() != "" && $("#HF_ZZR").val() == "0" && $("#HF_ZXR").val() != "0") {
        $("#B_Stop").prop("disabled", false);
    } else {
        $("#B_Stop").prop("disabled", true);
    }
}
function IsValidData() {
    if ($("#TB_XFJE").val() == "") {
        ShowMessage("请输入消费金额", 3);
        return false;
    }
    if ($("#TB_XFCS").val() == "") {
        ShowMessage("请输入消费次数", 3);
        return false;
    }
    if ($("#TB_XFJF").val() == "") {
        ShowMessage("请输入消费积分", 3);
        return false;
    }
    if ($("#DDL_JLFS")[0].value == 0) {
        ShowMessage("请选择奖励方式", 3);
        return false;
    }

    if ($("#TB_KSRQ").val() == "" || $("#TB_JSRQ").val() == "") {
        ShowMessage("请输入起止日期", 3);
        return false;
    }
    return true;
}

$(document).ready(function () {
    DrawGrid("listKQ", vKQColumnNames, vKQColumnModel);
    document.getElementById("B_Stop").disabled = !($("#LB_ZXRMC").text() != "");
    
    vJLBH = GetUrlParam("jlbh");
    if (vJLBH != "") {
        ShowDataBase(vJLBH);
    };

    $("#AddGZ").click(function () {
        $('#list').datagrid('appendRow', {});
    });

    $("#DelGZ").click(function () {
        DeleteRows("list");
    });


    $("#AddKQ").click(function () {
        var DataArry = new Object();
        SelectHYZ('listKQ', DataArry, 'iGRPID');
    });



    $("#DelKQ").click(function () {
        DeleteRows("listKQ");
    });
});
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.fXFJE = $("#TB_XFJE").val();
    Obj.iXFCS = $("#TB_XFCS").val();
    Obj.fXFJF = $("#TB_XFJF").val();
    Obj.iJLFS = $("#DDL_JLFS")[0].value;
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iSTATUS = 0;
    
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    var lstKQ = new Array();
    lstKQ = $("#listKQ").datagrid("getRows");

    Obj.itemTable = lst;
    Obj.itemTable1 = lstKQ;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_XFJE").val(Obj.fXFJE);
    $("#TB_XFCS").val(Obj.iXFCS);
    $("#TB_XFJF").val(Obj.fXFJF);
    $("#DDL_JLFS").val(Obj.iJLFS);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $('#listKQ').datagrid('loadData', Obj.itemTable1, "json");
    $('#listKQ').datagrid("loaded");
}



