vDJLX = GetUrlParam("djlx");
vCZK = GetUrlParam("czk");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vDBName = vCZK == "0" ? "CRMDB" : "CRMDBMZK";

function InitGrid() {
    vColumnNames = ['iHYKTYPE', '卡类型', '申请数量' ];
    vColumnModel = [
         { name: 'iHYKTYPE', hidden: true },
         { name: 'sHYKNAME', width: 150 },
         { name: 'iHYKSL', width: 150, editor: 'text', editrules: { required: true, number: true, minValue: 1 } },

    ];
};

$(document).ready(function () {
    bStartBeforeStop = false;
    FillBGDDTreeSK("TreeBGDD", "TB_BGDDMC_BR", "menuContent");
    var lastsel;
    $("#B_Stop").show();
    $("#ZZR").show();
    $("#ZZSJ").show();
    
    //$("#list").addRowData($("#list").getGridParam("reccount"), {});
    $("#AddItem").click(function () {
        var DataArry = new Object();
        DataArry["vCZK"] = vCZK;
        SelectKLXList('list', DataArry, 'iHYKTYPE', false);
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });
    $("#list").datagrid({
        onAfterEdit: function (index, row) {
            updateActions(index, row);
        },
    });
});

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC_BR").val(treeNode.name);
    $("#HF_BGDDDM_BR").val(treeNode.id);
    hideMenu("menuContent");
}



function IsValidData() {

    var rows = $("#list").datagrid("getData").rows.length;
    if (rows < 1) {
        ShowMessage("没有选择数据", 3);
        return false;
    }
    if ($("#HF_BGDDDM_BR").val() == "") {
        ShowMessage("请填写操作地点!", 3);
        return false;

    }
  
    return true;

}

function SaveData() {
    var zsl = 0;
    var Obj = new Object();
    if (vCZK == 1) {
        Obj.sDBConnName = "CRMDBMZK";
    }
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
   
    Obj.sBGDDDM_BR = $("#HF_BGDDDM_BR").val();
    Obj.sBZ = $("#TB_BZ").val();
    Obj.iBJ_CZK = vCZK;
    Obj.iHYKSL = compute("list", "iHYKSL");//$("#list").getCol('iSKSL', false, 'sum');
    Obj.iDJLX = vDJLX;
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    for (var i = 0; i < lst.length; i++) {
        zsl += parseInt(lst[i]['iHYKSL']);
    }


    Obj.itemTable = lst;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#LB_HYKSL").text(Obj.iHYKSL);
    $("#HF_BGDDDM_BR").val(Obj.sBGDDDM_BR);
    $("#TB_BGDDMC_BR").val(Obj.sBGDDMC_BR);
    $("#TB_BZ").val(Obj.sBZ);



    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
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
        total += parseInt(rows[i]['' + colName + '']);
    }
    return total;
}


function updateActions(index, row) {
    var str = GetKCKSL(row.iHYKTYPE,vCZK);
    var Obj = JSON.parse(str);
    if (row.iHYKSL > Obj.iSL) {
        ShowMessage("申请数量大于目前库存数量，目前库存数量为" + Obj.iSL);
        row['iHYKSL'] = "";
        $('#list').datagrid('refreshRow', index);
        return;
    }
}