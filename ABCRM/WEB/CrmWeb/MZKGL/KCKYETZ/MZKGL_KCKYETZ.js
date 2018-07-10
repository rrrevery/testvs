vUrl = "../MZKGL.ashx";
vHF = 0;
var rowNumer = 0;

function IsValidData() {
    return true;
}

function InitGrid() {
    vColumnNames = ['库存卡卡号', '期初余额（原）', '调整金额', '期初余额（新）', 'iHYKTYPE', '卡类型', '期初金额（新）', '有效期', ];
    vColumnModel = [

            { name: 'sCZKHM', index: 'sCZKHM', },
            { name: 'fQCYE_OLD', index: 'fQCYE_OLD', },
            { name: 'fTZJE', index: 'fTZJE', editor: 'text', editrules: { required: true, number: true, minValue: 1 } },
            { name: 'fQCYE_NEW', index: 'fQCYE_NEW' },//align: "right", editable: true,edittype:"select", editoptions: { value:"101:金卡;102:银卡" }
            { name: 'iHYKTYPE', hidden: true, },
            { name: 'sHYKNAME', },
            { name: 'fQCYE', hidden: true, },
            { name: 'dYXQ', width: 120 },

    ];
};


$(document).ready(function () {

    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE",2);
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");

    $("#list").datagrid({
        onAfterEdit: function (index, row) {
            row.editing = false;
            updateActions(index, row);
        },
        onCancelEdit: function (index, row) {
            row.editing = false;
            updateActions(index, row);
        }
    });



    $("#AddItem").click(function () {
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型", 3);
            return;
        }
        if ($("#HF_BGDDDM").val() == "") {
            ShowMessage("请选择保管地点", 3);
            return;
        }

        if ($("#TB_TZJE").val() == "") {
            ShowMessage("请输入调整金额", 3);
            return;
        }
        var DataArry = new Object();
        DataArry["iHYKTYPE"] = $("#HF_HYKTYPE").val();
        DataArry["sBGDDDM"] = $("#HF_BGDDDM").val();
        DataArry["vHF"] = "0";
        DataArry["vCZK"] = "1";
        SelectKCK('list', DataArry, 'sCZKHM');


    });

    $("#DelItem").click(function () {
        DeleteRows("list");
        var rows = $('#list').datagrid("getSelections");
        if (rows.length <= 0) {
            document.getElementById("TB_BGDDMC").disabled = false;
            document.getElementById("TB_HYKNAME").disabled = false;
        }
    });

});

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
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
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    //if ($("#list").getGridParam("reccount") > 0) {
    //    $("#TB_TZJE").val(Obj.itemTable[0].fTZJE);
    //}
    //else {
    //    $("TB_TZJE").val(0);
    //}
    if (Obj.itemTable.length > 0)
        $("#TB_TZJE").val(Obj.itemTable[0].fTZJE);
    else {
        $("TB_TZJE").val(0);
    }

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);


}

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenu("menuContentHYKTYPE");
}




function clearDataGrid() {
    $('#list').datagrid('loadData', { total: 0, rows: [] });
}

function InsertClickCustom() {
    window.setTimeout(function () {
        clearDataGrid();
    }, 1);

};


function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    for (var i = 0; i < lst.length; i++) {
        if (CheckReapet(array, CheckFieldId, lst[i])) {
            $('#list').datagrid('appendRow', {
                sCZKHM: lst[i].sCZKHM,
                fQCYE_OLD: lst[i].fQCYE,
                sHYKNAME: lst[i].sHYKNAME,
                iHYKTYPE: lst[i].iHYKTYPE,
                dYXQ: lst[i].dYXQ,
                fQCYE: parseFloat(lst[i].fQCYE) + parseFloat($("#TB_TZJE").val()),
                fQCYE_OLD: lst[i].fQCYE,
                fQCYE_NEW: parseFloat(lst[i].fQCYE) + parseFloat($("#TB_TZJE").val()),
                fTZJE: $("#TB_TZJE").val(),
            });
        }
    }

}

function updateActions(index, row) {
    $('#list').datagrid('updateRow', {
        index: index,
        row: {
            fQCYE_NEW: parseFloat(row.fQCYE_OLD) + parseFloat(row.fTZJE),
        }
    });
}