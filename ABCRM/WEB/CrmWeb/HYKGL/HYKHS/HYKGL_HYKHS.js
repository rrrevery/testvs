vUrl = "../HYKGL.ashx";

function InitGrid() {
    vColumnNames = ['会员编号', '会员卡号', '储值卡原金额', '优惠券余额', '期初金额', 'inx', 'bj_child'],
    vColumnModel = [
            { name: 'iHYID', index: 'iHYID', width: 100, },
            { name: 'sHYK_NO', index: 'sHYK_NO', width: 100, },
            { name: 'fCZKYE_OLD', index: 'fCZKYE_OLD', },
            { name: 'fYHQYE_OLD', index: 'fYHQYE_OLD', width: 50, },//align: "right", editable: true,edittype:"select", editoptions: { value:"101:金卡;102:银卡" }
            { name: 'fQCJE', index: 'fQCJE', width: 50, },
            { name: 'iINX', index: 'iINX', width: 20, hidden: true },
            { name: 'iBJ_CHILD', indes: 'iBJ_CHILD', hidden: true }
    ];
};

function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iTKSL = $("#TB_TKSL").val();
    Obj.fJE = 0;
    Obj.fTKJE = 0;
    Obj.iTKFS = 0;
    Obj.iMDID = 0;
    Obj.iBGR = $("#HF_BGR").val();
    Obj.sBGRMC = $("#TB_BGRMC").val();
    Obj.iFXDWID = $("#HF_FXDWID").val();
    Obj.dYXQ_NEW = "2020-09-10";
    Obj.sZY = $("#TB_ZY").val();

    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
    FillFXDWTree("TreeFXDW", "TB_FXDWMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", 1);

    $("#TB_BGRMC").click(function () {
        SelectRYXX("TB_BGRMC", "HF_BGR", "", true);
    });
    $("#AddItem").click(function () {
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型", 3);
            return;
        }
        var DataArry = new Object();
        DataArry["iHYKTYPE"] = parseInt($("#HF_HYKTYPE").val());
        SelectHYK('list', DataArry, 'ListHYK', 'iHYID');

    });

    $("#DelItem").click(function () {
        DeleteRows("list");
        var data = $('#list').datagrid('getData');//tt为表格id
        $("#TB_TKSL").val(data.total);
    });

})

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);//input控件被换成了label...
    $("#TB_TKSL").val(Obj.iTKSL);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_ZY").val(Obj.sZY);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_FXDWMC").val(Obj.sFXDWMC);
    $("#TB_BGRMC").val(Obj.sBGRMC);
    $("#HF_BGR").val(Obj.iBGR);
    $("#HF_FXDWID").val(Obj.iFXDWID);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
}

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
        case "TreeFXDW": $("#HF_FXDWID").val(treeNode.iFXDWID); break;
        case "TreeHYKTYPE":
            var ids = $("#list").datagrid("getData").rows;
            if (ids.length > 0) {
                var rowdata = $('#list').datagrid('getData').rows[0];
                if (rowdata.iHYKTYPE != treeNode.id) {
                    ShowYesNoMessage("卡类型不一致，是否清空卡号列表？", function () {
                        $('#list').datagrid('loadData', { total: 0, rows: [] });
                        $("#TB_HYKNAME").val(treeNode.name);
                        $("#HF_HYKTYPE").val(treeNode.id);
                        hideMenu("menuContentHYKTYPE");
                    });
                }
            }
            else {
                $("#TB_HYKNAME").val(treeNode.name);
                $("#HF_HYKTYPE").val(treeNode.id);
                hideMenu("menuContentHYKTYPE");
            }
            break;
    }
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
                sHYK_NO: lst[i].sHYK_NO,
                iHYID: lst[i].iHYID,
                iHYKTYPE: lst[i].iHYKTYPE,
                fQCJE: lst[i].fQCYE,
                fCZKYE_OLD: lst[i].fCZJE,
                fYHQYE_OLD: 0,
                iINX: 0,
                iBJ_CHILD: lst[i].iBJ_CHILD,
            });
        }
    }
    var data = $('#list').datagrid('getData');
    $("#TB_TKSL").val(data.total);
}