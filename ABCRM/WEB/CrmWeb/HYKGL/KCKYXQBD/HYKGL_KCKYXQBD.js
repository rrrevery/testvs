vCZK = GetUrlParam("czk");
vUrl = "../HYKGL.ashx";

function InitGrid() {
    vColumnNames = ['卡号码', '原有效期', '新有效期', '库存卡卡号', 'iHYKTYPE', '卡类型', ];
    vColumnModel = [
             { name: 'sHM', index: 'sHM', },
             { name: 'dYXQ', },
             { name: 'dXYXQ' },
             { name: 'sCZKHM', index: 'sCZKHM', width: 80, hidden: true, },
             { name: 'iHYKTYPE', hidden: true, },
             { name: 'sHYKNAME', },
    ]
};

function IsValidData() {
    if ($("#HF_HYKTYPE").val() == "") {
        art.dialog({ content: '请选择卡类型', lock: true, time: 2 });
        return false;
    }
    if ($("#TB_XYXQ").val() == "") {
        art.dialog({ content: '请输入新有效期', lock: true, time: 2 });
        return false;
    }

    if ($("#HF_BGDDDM").val() == "") {
        art.dialog({ lock: true, content: "请选择保管地点" });
        return false;
    }

    return true;
}


$(document).ready(function () {

    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE", vCZK == 0 ? 1 : 2);
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

        if ($("#TB_XYXQ").val() == "") {
            ShowMessage("请输入新有效期", 3);
            return;
        }
        var CondData = new Object();
        CondData.iHYKTYPE = $("#HF_HYKTYPE").val();
        CondData.sBGDDDM = $("#HF_BGDDDM").val();
        CondData.iFS_YXQ = 0;
        SelectKCK('list', CondData, 'sCZKHM');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
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

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    if (rows.length == 0) {
        var showList = new Array();
        for (var i = 0; i < lst.length ; i++) {
            var myData = {
                sHM: lst[i].sCZKHM,
                sHYKNAME: lst[i].sHYKNAME,
                dYXQ: lst[i].dYXQ == "" ? "0001-01-01" : lst[i].dYXQ,
                dXYXQ: $("#TB_XYXQ").val(),
                fQCYE_OLD: lst[i].fQCYE,
                sCZKHM: lst[i].sCZKHM,
            }
            showList.push(myData);
        }
        $('#' + listName).datagrid('loadData', showList, "json");
    }
    else {
        for (var i = 0; i < lst.length; i++) {
            if (CheckReapet(array, CheckFieldId, lst[i])) {
                var myData = {
                    sHM: lst[i].sCZKHM,
                    sHYKNAME: lst[i].sHYKNAME,
                    dYXQ: lst[i].dYXQ == "" ? "0001-01-01" : lst.dYXQ,
                    dXYXQ: $("#TB_XYXQ").val(),
                    fQCYE_OLD: lst[i].fQCYE,
                    sCZKHM: lst[i].sCZKHM,
                }
                $('#' + listName).datagrid('appendRow', myData);
            }
        }
    }
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDMC = $("#TB_BGDDMC").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.dXYXQ = $("#TB_XYXQ").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_XYXQ").val(Obj.dXYXQ);
    $("#TB_ZY").val(Obj.sZY);

    var showList = new Array();

    for (var i = 0; i < Obj.itemTable.length ; i++) {
        var myData = {
            sHM: Obj.itemTable[i].sCZKHM,
            sHYKNAME: Obj.sHYKNAME,
            dYXQ: Obj.itemTable[i].dYXQ == "" ? "0001-01-01" : Obj.itemTable[i].dYXQ,
            dXYXQ: Obj.dXYXQ,
            fQCYE_OLD: Obj.itemTable[i].fQCYE_OLD,
            sCZKHM: Obj.itemTable[i].sCZKHM,
        }
        showList.push(myData);
    }
    $('#list').datagrid('loadData', showList, "json");
    $('#list').datagrid("loaded");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}