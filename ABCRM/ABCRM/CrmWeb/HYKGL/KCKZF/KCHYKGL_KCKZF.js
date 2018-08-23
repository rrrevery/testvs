vUrl = "../HYKGL.ashx";
var vHF = GetUrlParam("hf");

var rowNumer = 0;


function InitGrid() {
    vColumnNames = ['卡号', '面值金额', ];
    vColumnModel = [
            { name: 'sCZKHM', width: 100, },
            { name: 'fQCYE', width: 50, },
    ];
};


function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val());
        if (str) {
            var Obj = JSON.parse(str);
            if (Obj.iSTATUS < 0 && GetUrlParam("lx") == "0") {
                ShowMessage("此卡为无效卡，无需挂失", 3);
                return;
            }
            if (Obj.iSTATUS != -3 && GetUrlParam("lx") == "1") {
                ShowMessage("此卡非挂失状态，不能恢复", 3);
                return;
            }
            $("#HF_HYID").val(Obj.iHYID);
            $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
            $("#LB_HYNAME").text(Obj.sHY_NAME);
            $("#LB_HYKNAME").text(Obj.sHYKNAME);
            $("#LB_JF").text(Obj.fWCLJF);
            $("#LB_JE").text(Obj.fCZJE);
        }
    }
}

function IsValidData() {

    if ($("#S_CZMD").val() == "") {
        ShowMessage("请选择门店", 3);
        return false;
    }
    if ($("#list").datagrid("getData").rows.length == 0) {
        ShowMessage("没有添加卡", 3);
        return false;
    }
    return true;
}

$(document).ready(function () {

    if (vHF == 1) {
        $("#dv_je").html("恢复金额");
        $("#dv_sl").html("恢复数量");
        $("#dv_yy").html("恢复原因");
    }
    else {
        $("#dv_je").html("作废金额");
        $("#dv_sl").html("作废数量");
        $("#dv_yy").html("作废原因");
    }



    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE")
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");


    $("#AddItem").click(function () {
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型", 3);
            return;
        }
        if ($("#HF_BGDDDM").val() == "") {
            ShowMessage("请选择保管地点", 3);
            return;
        }

        var DataArry = new Object();
        DataArry["iHYKTYPE"] = $("#HF_HYKTYPE").val();
        DataArry["sBGDDDM"] = $("#HF_BGDDDM").val();
        DataArry["vHF"] = vHF;
        SelectKCK('list', DataArry, 'sCZKHM');

    })
    $("#DelItem").click(function () {
        DeleteRows("list");
        var rows = $('#list').datagrid("getSelections");

        if (rows.length <= 0) {
            document.getElementById("TB_BGDDMC").disabled = false;
            document.getElementById("TB_HYKNAME").disabled = false;
        }

    });
    $("#B_Update").click(function () {
        if ($("#list").datagrid("getSelections") > 0) {
            document.getElementById("TB_BGDDMC").disabled = true;
            $("#TB_HYKNAME").attr("disabled", true);
        }
    });
});

function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenu("menuContentHYKTYPE");
}

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}


function compute(lisName, colName) {
    var rows = $('#' + lisName + '').datagrid('getRows')//获取当前的数据行
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        total += rows[i]['' + colName + ''];
    }
    return total;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    $("#LB_ZFKJE").text(compute("list", "fQCYE"));  //$("#list").getCol('fQCYE', false, "sum")
    var listnum = $("#list").datagrid("getRows").length;
    $("#LB_ZFKSL").text(listnum);
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZFKYY = $("#TB_ZFKYY").val();
    Obj.iZFKSL = $("#LB_ZFKSL").text();
    Obj.fZFKJE = $("#LB_ZFKJE").text();
    Obj.iBJ_HF = vHF;

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
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#LB_ZFKJE").text(Obj.fZFKJE);
    $("#LB_ZFKSL").text(Obj.iZFKSL);
    $("#TB_ZFKYY").val(Obj.sZFKYY);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

//function CheckReapet(array,checkField) {
//    var boolInsert = true;
//    for (var j = 0; j < array.length; j++) {
//        if (array[j].sCZKHM == checkField) {
//            boolInsert = false;
//        }
//    }
//    return boolInsert;
//}


