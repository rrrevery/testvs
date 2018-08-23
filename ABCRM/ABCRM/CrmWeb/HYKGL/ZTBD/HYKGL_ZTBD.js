var HYKNO = GetUrlParam("HYKNO");
var vCZK = IsNullValue(GetUrlParam("czk"), "0");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vDBName = vCZK == "0" ? "CRMDB" : "CRMDBMZK";

function InitGrid() {
    vColumnNames = ["HYID", "卡号", "姓名", "原状态id", "原状态", "iBJ_CHILD"],
    vColumnModel = [
          { name: "iHYID", hidden: true },
          { name: "sHYK_NO", width: 150 },
          { name: "sHY_NAME", width: 60 ,hidden: vCZK != "0"},
          { name: "iOLD_STATUS", width: 80, hidden: true },
          { name: "sStatusName", width: 80 },
          { name: "iBJ_CHILD", hidden: true },

    ];
};

$(document).ready(function () {

    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    var lastsel;

    if (HYKNO != "") {
        if (vCZK == 1) {
            var str = GetMZKXXData(0, HYKNO, "", "CRMDBMZK");
        } else {
            var str = GetHYXXData(0, HYKNO);
        }
        if (str) {
            var Obj = JSON.parse(str);
            $('#list').datagrid('appendRow', {
                sHYK_NO: HYKNO,
                iHYID: Obj.iHYID,
                sHY_NAME: Obj.sHY_NAME,
                iOLD_STATUS: Obj.iSTATUS,
                sStatusName: Obj.sStatusName,
                iBJ_CHILD: Obj.iBJ_CHILD,
            });
            vProcStatus = cPS_ADD;
            SetControlBaseState();
        }
    }
    //$("#list").addRowData($("#list").getGridParam("reccount"), {});
    $("#AddItem").click(function () {
        if (vCZK == "0") {
            var DataArry = new Object();
            DataArry["vZT"] = 1;
            SelectHYK('list', DataArry, 'iHYID');
        }
        if (vCZK == "1") {
            var condData = new Object();
            //condData["vZT"] = 1;
            var checkRepeatField = ["iHYID"];
            SelectMZK("list", condData, checkRepeatField);
        }
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
    if ($("#rd_FSK")[0].checked == false) {
        if ($("#rd_TY")[0].checked == false) {
            if ($("#rd_YXFK")[0].checked == false) {

                ShowMessage("请选择修改的状态", 3);
                return false;
            }


        }
    }


    return true;

}

function SaveData() {
    var Obj = new Object();
    if (vCZK == 1) {
        Obj.sDBConnName = "CRMDBMZK";
    }
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.fGBF = $("#TB_GBF").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.iNEW_STATUS = $("[name='status']:checked").val();

    if (Obj.fGBF == "")
        Obj.fGBF = "0";
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
    $("#TB_GBF").val(Obj.fGBF);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_ZY").val(Obj.sZY);
    if (Obj.iNEW_STATUS == "0") {
        $("#rd_FSK")[0].checked = true;
    }
    if (Obj.iNEW_STATUS == "1") {
        $("#rd_YXFK")[0].checked = true;
    }
    if (Obj.iNEW_STATUS == "-4") {
        $("#rd_TY")[0].checked = true;
    }




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
                iHYID: lst[i].iHYID,
                sHYK_NO: lst[i].sHYK_NO,
                sHY_NAME: lst[i].sHY_NAME,
                iOLD_STATUS: lst[i].iSTATUS,
                iBJ_CHILD: lst[i].iBJ_CHILD,
                sStatusName: lst[i].sStatusName,
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
