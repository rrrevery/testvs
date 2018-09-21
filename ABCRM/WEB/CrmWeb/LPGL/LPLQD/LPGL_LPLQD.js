vUrl = "../LPGL.ashx";

function InitGrid() {
    vColumnNames = ["LPID", "礼品代码", "礼品名称", "礼品规格", "礼品单价", "库存数量", "调拨数量", ];
    vColumnModel = [
          { name: "iLPID", hidden: true },
          { name: "sLPDM", width: 60, },
          { name: "sLPMC", width: 90 },
          { name: "sLPGG", width: 60 },
          { name: "fLPDJ", width: 60 },
          { name: "fKCSL", width: 60 },
          { name: "fLQSL", width: 60, editor: 'text' },//editable: vProcStatus == cPS_BROWSE ? false : true
    ];
};


$(document).ready(function () {
    FillBGDDTree("TreeBGDD1", "TB_BGDDMC_BC");
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");

    $("#AddItem").click(function () {
        if ($("#TB_BGDDMC_BC").val() == "") {
            ShowMessage("请选择拨出地点", 3);
            return;
        }
        else {
            var DataArry = new Object();
            DataArry["sBGDDDM"] = $("#HF_BGDDDM_BC").val();
            DataArry["iDJLX"] = 1;
            SelectLP('list', DataArry, 'iLPID');
        }
    });


    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    $("#B_Update").click(function () {
        if ($("#list").datagrid("getRows").length > 0) {
            document.getElementById("TB_BGDDMC_BC").disabled = true;
        }
    });
    BFButtonClick("TB_LQRMC", function () {
        SelectRYXX("TB_LQRMC", "HF_LQR", "zHF_LQR", true);
    });
});


function IsValidData() {
    if ($("#HF_LQR").val() == "") {
        ShowMessage("请选择领取人", 3);
        return false;
    }

    if ($("#HF_BGDDDM_BC").val() == $("#HF_BGDDDM").val()) {
        ShowMessage("拨入地点不可与拨出地点相同", 3);
        return false;
    }

    str = $("#TB_ZY").val();
    var len = 0;
    for (var i = 0; i < str.length; i++) {
        var c = str.charCodeAt(i);
        //单字节加1 
        if ((c >= 0x0001 && c <= 0x007e) || (0xff60 <= c && c <= 0xff9f)) {
            len++;
        }
        else {
            len += 2;
        }
    }
    if (len > 30) {
        ShowMessage("字符数不超过30", 3);
        return false;
    }

    return true;
}
function TreeNodeClickCustom(e, treeId, treeNode) {
    if (treeId == "TreeBGDD1") {
        if ($("#HF_BGDDDM_BC").val() != "") {
            ShowYesNoMessage("是否清空数据？", function () {
                $('#list').datagrid('loadData', { total: 0, rows: [] });
                $("#TB_BGDDMC_BC").val(treeNode.name);
                $("#HF_BGDDDM_BC").val(treeNode.id);
            });
        }
        else {
            $("#TB_BGDDMC_BC").val(treeNode.name);
            $("#HF_BGDDDM_BC").val(treeNode.id);
        }
    }
    else {
        $("#TB_BGDDMC").val(treeNode.name);
        $("#HF_BGDDDM").val(treeNode.id);
    }
}

function SaveData() {
    var Obj = new Object();
    var zsl = 0;
    var zje = 0;
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBGDDDM_BC = $("#HF_BGDDDM_BC").val();
    Obj.sBGDDDM_BR = $("#HF_BGDDDM").val();

    Obj.sZY = $("#TB_ZY").val();

    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    for (var i = 0; i < lst.length; i++) {
        zsl += lst[i]['fLQSL']*1;
        zje += lst[i]['fLQSL'] * lst[i]['fLPDJ'];
    }
    $("#LB_ZSL").text(zsl);
    $("#LB_ZJE").text(zje);
    Obj.itemTable = lst;

    Obj.fZSL = $("#LB_ZSL").text();
    Obj.fZJE = $("#LB_ZJE").text();
    Obj.iLQR = $("#HF_LQR").val();
    Obj.sLQRMC = $("#TB_LQRMC").val();

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_BGDDDM_BC").val(Obj.sBGDDDM_BC);
    $("#TB_BGDDMC_BC").val(Obj.sBGDDMC_BC);
    $("#HF_BGDDDM").val(Obj.sBGDDDM_BR);
    $("#TB_BGDDMC").val(Obj.sBGDDMC_BR);
    $("#LB_ZSL").text(Obj.fZSL);
    $("#LB_ZJE").text(Obj.fZJE);
    $("#TB_ZY").val(Obj.sZY);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#HF_LQR").val(Obj.iLQR);
    $("#TB_LQRMC").val(Obj.sLQRMC);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

