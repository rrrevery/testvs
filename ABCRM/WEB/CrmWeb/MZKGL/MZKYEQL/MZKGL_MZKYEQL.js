vUrl = "../MZKGL.ashx";
var rowNumer = 0;

function InitGrid() {
    vColumnNames = ["iHYID", "卡号", "原余额", "调整金额"];
    vColumnModel = [
            { name: "iHYID", hidden: true },
            { name: "sHYK_NO", width: 80, },
            { name: "fYYE", width: 60 },
            { name: "fTZJE", width: 60, },
    ];
};

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME",  2);

    $("#AddItem").click(function () {

        if ($.trim($("#TB_HYKNAME").val()) == "") {
            ShowMessage("请选择卡类型！", 3);
            return;
        }

        var condData = new Object();
        condData["iHYKTYPE"] = $("#HF_HYKTYPE").val();
        condData["vZT"] = 1;
        var checkRepeatField = ["iHYID"];
        SelectMZK("list", condData, checkRepeatField);
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });


})


function IsValidData() {
    if ($.trim($("#HF_BGDDDM").val()) == "") {
        ShowMessage("请选择操作地点！", 3);
        return;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.fTZJE = compute("list", "fTZJE");
    Obj.iTZSL = $("#list").datagrid("getRows").length;
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    Obj.sZY = $("#TB_ZY").val();
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
    $("#LB_TZJE").text(Obj.fTZJE);
    $("#LB_TZSL").text(Obj.iTZSL);
    $("#TB_ZY").val(Obj.sZY);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    //$("#TB_JSSJ1").val(Obj.dJSSJ1);
    //$("#TB_JSSJ2").val(Obj.dJSSJ2);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function compute(lisName, colName) {
    var rows = $('#' + lisName + '').datagrid('getRows')//获取当前的数据行
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        total += parseFloat(rows[i]['' + colName + '']);
    }
    return total;
}
function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

//function onHYKClick(e, treeId, treeNode) {
//    if ($("#list").datagrid("getData").rows.length > 0) {
//        art.dialog({
//            title: "删除",
//            lock: true,
//            content: "是否清空数据？",
//            ok: function () {
//                $('#list').datagrid("loadData", { total: 0, rows: [] });
//                $("#TB_HYKNAME").val(treeNode.name);
//                $("#HF_HYKTYPE").val(treeNode.id);
//            },
//            okVal: '是',
//            cancelVal: '否',
//            cancel: true
//        });
//    }
//    else {
//        $("#TB_HYKNAME").val(treeNode.name);
//        $("#HF_HYKTYPE").val(treeNode.id);
//    }
//    hideMenu("menuContentHYKTYPE");
//}

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
        case "TreeHYKTYPE":
            if ($("#list").datagrid("getData").rows.length > 0) {
                art.dialog({
                    title: "删除",
                    lock: true,
                    content: "是否清空数据？",
                    ok: function () {
                        $('#list').datagrid("loadData", { total: 0, rows: [] });
                        $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
                    },
                    okVal: '是',
                    cancelVal: '否',
                    cancel: true
                });
            }
            else {
                $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
            }
            break;
    }
}

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    for (var i = 0; i < lst.length; i++) {
        if (CheckReapet(array, CheckFieldId, lst[i])) {
            $('#list').datagrid('appendRow', {
                sHYK_NO: lst[i].sHYK_NO,
                iHYID: lst[i].iHYID,
                fYYE: lst[i].fYE,
                fTZJE: lst[i].fYE,
            });
        }
    }

}