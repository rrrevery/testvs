vUrl = "../MZKGL.ashx";
var bj_zr = 0;
function InitGrid() {
    vColumnNames = ["iHYID", "卡号","sCZKHM","iHYKTYPE", "卡类型", "余额", "标记"],
    vColumnModel = [
            { name: "iHYID", hidden: true },
            { name: "sHYK_NO", },
            { name: "sCZKHM", hidden: true },
            { name: "iHYKTYPE", hidden: true },
            { name: "sHYKNAME", width: 80, },
            { name: "fJE", width: 60 },
            {
                name: "iBJ_ZR", width: 60, formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == 0) { return "转出卡"; }
                    if (cellvalue == 1) { return "转入卡"; }
                }
            },


    ];
};

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", 2);
    $("#AddItem_ZC").click(function () {
        var condData = new Object();
        condData["vZT"] = 1;
        var checkRepeatField = ["sHYK_NO"];
        bj_zr = 0;
        SelectMZK("list", condData, 'sHYK_NO');
        
    });
    $("#AddItem_ZR").click(function () {
        var DataArry = new Object();
        DataArry["vHF"] = "0";
        DataArry["vCZK"] = "1";
        bj_zr = 1;
        SelectKCK('list', DataArry, 'sCZKHM');

    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });
});

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
    }
}



function IsValidData() {
    var rows = $("#list").datagrid("getData").rows;
    if (rows.length < 1) {
        ShowMessage("没有选择可操作的卡", 3);
        return false;
    }
    else
    {
        var zrje = 0;
        var zcje = 0;
        for (var i = 0; i < rows.length; i++) {
            if (rows[i]['iBJ_ZR'] == "0")
                zcje += parseFloat(rows[i]['fJE']);
            if (rows[i]['iBJ_ZR'] == "1")
                zrje += parseFloat(rows[i]['fJE']);
        }
        if (zrje != zcje)
        {
            ShowMessage("转入金额与转出金额不等！");
            return false;
        }

    }
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请填写操作地点!", 3);
        return false;

    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZY = $("#TB_ZY").val();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
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

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    if (bj_zr == 0) {
        for (var i = 0; i < lst.length; i++) {
            if (CheckReapet(array, CheckFieldId, lst[i])) {
                $('#list').datagrid('appendRow', {
                    sHYK_NO: lst[i].sHYK_NO,
                    sCZKHM: lst[i].sHYK_NO,
                    iHYID: lst[i].iHYID,
                    iHYKTYPE: lst[i].iHYKTYPE,
                    sHYKNAME: lst[i].sHYKNAME,
                    fJE: lst[i].fYE,
                    iBJ_ZR: 0,
                });
            }
        }
    }
    else
    {
        for (var i = 0; i < lst.length; i++) {
            if (CheckReapet(array, CheckFieldId, lst[i]))
            {
                $('#list').datagrid('appendRow', {
                    iHYID: 0,
                    sHYK_NO: lst[i].sCZKHM,
                    sCZKHM: lst[i].sCZKHM,
                    iHYKTYPE: lst[i].iHYKTYPE,
                    sHYKNAME: lst[i].sHYKNAME,
                    fJE: lst[i].fQCYE,
                    iBJ_ZR: 1,
                });
            }
        }
    }

}

