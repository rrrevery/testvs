vUrl = "../LPGL.ashx";
var DJLX = GetUrlParam("iDJLX");

function InitGrid() {
    vColumnNames = ["LPID", "礼品代码", "礼品名称", "礼品积分", "礼品规格", "礼品单价", "礼品分类", "LPFLID", "礼品进价", "不含税进价", "礼品库存", GetNAME(), ];
    vColumnModel = [
          { name: "iLPID", hidden: true },
          { name: "sLPDM", width: 60, },
          { name: "sLPMC", width: 90 },
          { name: "fLPJF", width: 50 },
          { name: "sLPGG", width: 60 },
          { name: "fLPDJ", width: 60 },
          { name: "sLPFLMC", width: 60 },
          { name: "iLPFLID", hidden: true },
          { name: "fLPJJ", width: 60, },
          { name: "fLPSJJJ", width: 60, hidden: true },
          { name: "fKCSL", width: 60, hidden: DJLX == 0 ? true : false },
          { name: "fJHSL", width: 60, editor: 'text' },
    ];
};
function GetNAME() {
    if (DJLX == 0) {
        return "进货数量";
    }
    else {
        return "退货数量";
    }

}


$(document).ready(function () {
    if (DJLX == 0) {
        document.getElementById("BGDDSTR").innerHTML = "礼品保管地点";
        document.getElementById("JHDWSTR").innerHTML = "礼品进货单位";
        document.getElementById("ChangeTitle").innerHTML = "总进货金额";
    }
    else {
        document.getElementById("BGDDSTR").innerHTML = "退货保管地点";
        document.getElementById("JHDWSTR").innerHTML = "礼品退货单位";
        document.getElementById("ChangeTitle").innerHTML = "总退货金额";
        $("#DV_JHFS").hide();
    }

    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
    BFButtonClick("TB_GHSMC", function () {
        SelectGHS("TB_GHSMC", "HF_GHSID", "zHF_GHSID", true, 0); //iBJ_TY=0 未停用
    });

    $("#AddItem").click(function () {
        if (DJLX == 1 && $("#HF_BGDDDM").val() == "") {
            ShowMessage("退货请先选择礼品现保管地点", 3);
            return false;
        }
        var DataArry = new Object();
        DataArry["iDJLX"] = DJLX;
        if (DJLX == 1) {
            DataArry["sBGDDDM"] = $("#HF_BGDDDM").val();
        }
        SelectLP('list', DataArry, 'iLPID');

    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    $("#B_Update").click(function () {
        if ($("#list").datagrid("getRows").length > 0) {
            document.getElementById("TB_BGDDMC").disabled = true;
        }
    });

});


function IsValidData() {
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请先选择保管地点", 3);
        return false;
    }
    str = $("#TB_ZY").val();
    var len = 0;
    for (var i = 0; i < str.length; i++) {
        var c = str.charCodeAt(i);
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
    if ($("#HF_BGDDDM").val() != "") {
        ShowYesNoMessage("是否清空数据？", function () {
            $('#list').datagrid('loadData', { total: 0, rows: [] });
            $("#TB_BGDDMC").val(treeNode.name);
            $("#HF_BGDDDM").val(treeNode.id);
        });
    }
    else {
        $("#TB_BGDDMC").val(treeNode.name);
        $("#HF_BGDDDM").val(treeNode.id);
    }
}

function SaveData() {

    var zsl = 0;
    var zje = 0;
    var zjjje = 0;

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    //Obj.sGYSDM = $("#TB_GYSDM").val();
    Obj.iGHSID = $("#HF_GHSID").val();
    Obj.sGHSMC = $("#TB_GHSMC").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZY = $("#TB_ZY").val();

    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    for (var i = 0; i < lst.length; i++) {
        zsl += lst[i]['fJHSL'] * 1;
        zje += lst[i]['fJHSL'] * lst[i]['fLPDJ'] * 1;
        zjjje += lst[i]['fJHSL'] * lst[i]['fLPJJ'] * 1;
    }

    if (DJLX == 0) {
        Obj.iJHFS = IsNullValue($("#DDL_JHFS")[0].value, 0);
    }
    Obj.iJHDWID = IsNullValue($("#HF_JHDWID").val(), 0);
    $("#LB_ZSL").text(zsl);
    $("#LB_ZJE").text(zje);
    $("#LB_JJJE").text(zjjje);
    Obj.itemTable = lst;

    Obj.fZSL = $("#LB_ZSL").text();
    Obj.fZJE = $("#LB_ZJE").text();
    Obj.fJJJE = $("#LB_JJJE").text();
    Obj.iDJLX = DJLX;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;



    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_GHSMC").val(Obj.sGHSMC);
    $("#HF_GHSID").val(Obj.iGHSID);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#LB_ZSL").text(Obj.fZSL);
    $("#LB_ZJE").text(Obj.fZJE);
    $("#LB_JJJE").text(Obj.fJJJE);
    $("#TB_ZY").val(Obj.sZY);
    $("#DDL_JHFS").val(Obj.iJHFS);
    $("#TB_JHDWMC").val(Obj.sJHDWMC);
    $("#HF_JHDWID").val(Obj.iJHDWID);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}




