var HYKNO = GetUrlParam("HYKNO");
vUrl = "../HYKGL.ashx" ;

function InitGrid() {
    vColumnNames = ["HYID", "卡号", "FXDWID" ,"原发行单位", "MDID", "门店", "iBJ_CHILD"],
    vColumnModel = [
          { name: "iHYID", hidden: true },
          { name: "sHYK_NO", width: 150 },
          { name: "iYFXDWID", width: 80, hidden: true },
          { name: "sYFXDWMC", width: 80 },
          { name: "iMDID", hidden: true, },
          { name: "sMDMC" },
          { name: "iBJ_CHILD", hidden: true, }

    ];
};

$(document).ready(function () {

    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    FillFXDWTree("TreeFXDW", "TB_FXDWMC", "menuContentFXDW");


    if (HYKNO != "") {
        if (vCZK == 1) {
            var str = GetHYXXData(0, HYKNO, "CRMDBMZK");
        } else {
            var str = GetHYXXData(0, HYKNO);
        }
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号", 3);
            return;
        }
        var Obj = JSON.parse(str);
        $('#list').datagrid('appendRow', {
            sHYK_NO: HYKNO,
            iHYID: Obj.iHYID,
            sHY_NAME: Obj.sHY_NAME,
            iYFXDWID: Obj.iFXDW,
            sYFXDWMC: Obj.sFXDWMC,
            iMDID: Obj.iMDID,
            sMDMC: Obj.sMDMC,
            iBJ_CHILD: Obj.iBJ_CHILD,
        });
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }
    //$("#list").addRowData($("#list").getGridParam("reccount"), {});
    $("#AddItem").click(function () {
        var DataArry = new Object();
        SelectHYK('list', DataArry, 'ListHYK', 'iHYID');
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });

});

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function SetControlState() {
    if (HYKNO != "") {
        $("#AddItem").prop("disabled", true);
        $("#DelItem").prop("disabled", true);
    }
}


function IsValidData() {

    var rows = $("#list").datagrid("getData").rows.length;
    if (rows < 1) {
        ShowMessage("没有选择可操作的卡", 3);
        return false;
    }
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请填写操作地点!", 3);
        return false;

    }
    if ($("#HF_FXDWID").val() == "") {
        ShowMessage("请填写新发行单位!", 3);
        return false;

    }

    return true;

}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iXFXDWID = $("#HF_FXDWID").val();
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
    $("#TB_FXDWMC").val(Obj.sFXDWMC);
    $("#HF_FXDWID").val(Obj.iXFXDWID);


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
    for (var i = 0; i < lst.length; i++) {
        if (CheckReapet(array, CheckFieldId, lst[i])) {
            $('#list').datagrid('appendRow', {
                sHYK_NO: lst[i].sHYK_NO,
                iHYID: lst[i].iHYID,
                sHY_NAME: lst[i].sHY_NAME,
                iYFXDWID: lst[i].iFXDW,
                sYFXDWMC: lst[i].sFXDWMC,
                iMDID: lst[i].iMDID,
                sMDMC: lst[i].sMDMC,
                iBJ_CHILD: lst[i].iBJ_CHILD,
            });
        }
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
function onFXDWClick(e, treeId, treeNode) {
    $("#TB_FXDWMC").val(treeNode.name);
    $("#HF_FXDWDM").val(treeNode.id);
    $("#HF_FXDWID").val(treeNode.data);
    hideMenu("menuContentFXDW");
}