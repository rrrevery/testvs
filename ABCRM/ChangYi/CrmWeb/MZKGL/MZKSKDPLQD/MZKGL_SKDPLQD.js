vUrl = "../MZKGL.ashx";
vHF = 0;
var rowNumer = 0;

function IsValidData() {
    return true;
}

function InitGrid() {
    vColumnNames = ["JLBH", "售卡单编号", "售卡数量", "实收金额", "客户", "登记时间", "登记人", "审核日期", "审核人", "摘要"];
    vColumnModel = [
            { name: "iJLBH", hidden: true },
            { name: "iSKDBH", width: 80, },
            { name: "iSKSL", width: 60, },
            { name: "cSSJE", width: 80, },
            { name: "sKHMC", width: 100, },
            { name: "dDJSJ", width: 150, },
            { name: "sDJRMC", width: 80, },
            { name: "dZXRQ", width: 90, },
            { name: "sZXRMC", width: 80, },
            { name: "sZY", width: 100, },
    ];
};


$(document).ready(function () {

    FillBGDDTree("TreeBGDD", "TB_BGDDMC");



    $("#AddItem").click(function () {

        if ($("#HF_BGDDDM").val() == "") {
            ShowMessage("请选择操作地点", 3);
            return;
        }
        var DataArry = new Object();
        DataArry["iSTATUS"] = 1;
        SelectMZKSKDList('list', DataArry, 'iJLBH');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

});

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
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

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
    }
}



function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    for (var i = 0; i < lst.length; i++) {
        if (CheckReapet(array, CheckFieldId, lst[i])) {
            $('#list').datagrid('appendRow', {
                iJLBH: lst[i].iJLBH,
                iSKDBH: lst[i].iJLBH,
                iSKSL: lst[i].iSKSL,
                cSSJE: lst[i].cSSJE,
                sKHMC: lst[i].sKHMC,
                dDJSJ: lst[i].dDJSJ,
                sDJRMC: lst[i].sDJRMC,
                dZXRQ: lst[i].dZXRQ,
                sZXRMC: lst[i].sZXRMC,
                sZY: lst[i].sZY,
            });
        }
    }

}
