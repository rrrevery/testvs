vUrl = "../LPGL.ashx";

function InitGrid() {
    vColumnNames = ["LPID", "礼品代码", "礼品名称", "礼品单价", "礼品积分", "库存数量", "报废数量", ];
    vColumnModel = [
          { name: "iLPID", hidden: true },
          { name: "sLPDM", width: 60, },
          { name: "sLPMC", width: 90 },
          { name: "fLPDJ", width: 60 },
          { name: "fLPJF", hidden: true },
          { name: "fKCSL", width: 60 },
          { name: "fBFSL", width: 60, editor: 'text' },
    ];
};
$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");

    $("#AddItem").click(function () {
        if ($("#HF_BGDDDM").val() == "") {
            ShowMessage("请先选择礼品保管地点", 3);
            return false;
        }
        var DataArry = new Object();
        DataArry["iDJLX"] = 1;
        DataArry["sBGDDDM"] = $("#HF_BGDDDM").val();
        SelectLP('list', DataArry, 'iLPID');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

});


function IsValidData() {
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请选择礼品保管地点", 3);
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

        ShowMessage("字符数不超过30");
        return false;
    }

    return true;
}

function onClick(e, treeId, treeNode) {
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

    hideMenu("menuContent");
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";  
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZY = $("#TB_ZY").val();
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
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);   
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
