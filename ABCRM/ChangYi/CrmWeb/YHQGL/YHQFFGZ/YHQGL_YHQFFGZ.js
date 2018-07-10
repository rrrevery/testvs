vUrl = "../YHQGL.ashx";

function SetControlState() {
}


function InitGrid() {
    vColumnNames = ['销售金额', '发券金额', ];
    vColumnModel = [
         { name: "fXSJE", width: 90, editor: 'text' },
         { name: "fFQJE", width: 90, editor: 'text' },
    ];
};


function IsValidData() {
    if ($("#TB_GZMC").val() == "") {
        art.dialog({ lock: true, content: "请输入规则名称" });
        return false;
    }
    //if ($("#HF_MDID").val() == "") {
    //    art.dialog({ lock: true, content: "请选择门店" });
    //    return false;
    //}    
    if ($("#TB_FFXE").val() == "") {
        art.dialog({ lock: true, content: "请输入发放限额" });
        return false;
    }

    if ($("#TB_QDJE").val() == "") {
        art.dialog({ lock: true, content: "请输入起点金额" });
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();

    Obj.iJLBH = $("#TB_JLBH").val();//
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sYHQFFGZMC = $("#TB_GZMC").val();
    Obj.fFFXE = $("#TB_FFXE").val();
    Obj.fFFQDJE = $("#TB_QDJE").val();
    Obj.iBJ_TY = $("#CB_BJ_TY").prop("checked") ? "1" : "0";
    Obj.iMDID = $("#HF_MDID").val() == "" ? "-1" : $("#HF_MDID").val();
    Obj.iBJ_SF = 0;//0 表明该记录为为发放规则，不是其它数据
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    $("#AddItem").click(function () {
        $("#list").datagrid("appendRow", {});
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });
})
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_GZDM").val(Obj.iYHQFFGZID);
    $("#TB_GZMC").val(Obj.sYHQFFGZMC);
    $("#TB_FFXE").val(Obj.fFFXE);
    $("#TB_QDJE").val(Obj.fFFQDJE);
    if (Obj.iBJ_TY == 1) {
        $("#CB_BJ_TY").attr("checked", true);
    }
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}


function CheckReapet(value) {
    var boolReapet = true;
    for (i = 0; i < $("#list").getGridParam("reccount") - 1; i++) {
        var RowDate = $("#list").getRowData(i);
        if (RowDate.fXSJE == value) {
            boolReapet = false;
        }

    }

    return boolReapet;

}


