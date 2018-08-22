vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ['记录编号', 'ID', 'OPENID', '会员ID', '日期', '顾客姓名', '联系电话', '门店名称', '主题', '预约时间', '备注', 'MDID'];'人数', 
    vColumnModel = [
      { name: 'iJLBH',  width: 100,  },
      { name: 'iID', width: 150,  hidden: true },
      { name: 'sOPENID',  width: 50,  hidden: true },
      { name: 'iHYID',  width: 150,  hidden: true },
      { name: 'sRQ',  width: 150,  hidden: true },
      { name: 'sGKXM', width: 150,  },
      { name: 'sLXDH',  width: 150, },
      { name: 'sMDMC',  width: 150, },
      { name: 'sMC', width: 150, },
      { name: 'dDJSJ', width: 200, },
      { name: 'sBZ',  width: 150, },
      { name: 'iMDID',  width: 150,  hidden: true },

    ];

    vBYYColumnNames = ['记录编号', 'ID', 'OPENID', '会员ID', '日期', '顾客姓名', '联系电话', '门店名称', '主题', '预约时间', '备注', 'MDID']; '人数',
    vBYYColumnModel = [
      { name: 'iJLBH',  width: 100,  },
      { name: 'iID', width: 150,  hidden: true },
      { name: 'sOPENID',  width: 50,  hidden: true },
      { name: 'iHYID',  width: 150,  hidden: true },
      { name: 'sRQ',  width: 150,  hidden: true },
      { name: 'sGKXM', width: 150,  },
      { name: 'sLXDH',  width: 150, },
      { name: 'sMDMC',  width: 150, },
      { name: 'sMC', width: 150, },
      { name: 'dDJSJ', width: 200, },
      { name: 'sBZ',  width: 150, },
      { name: 'iMDID',  width: 150,  hidden: true },

    ];

}


function IsValidData() {
    return true;
}

$(document).ready(function () {
    DrawGrid("listBYY", vBYYColumnNames, vBYYColumnModel, false);


    //document.getElementById("B_Update").onclick = function () {
    //    vProcStatus = cPS_MODIFY;
    //    SetControlBaseState();
    //};

    $("#AddItem").click(function () {
        var DataObject = new Object();
        DataObject["iBJ_CJ"] = 0;
        DataObject["iSTATUS"] = 1;
        SelectYYJLList("list", DataObject, "iJLBH", false);
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });



    $("#AddBYY").click(function () {
        var DataObject = new Object();
        DataObject["iBJ_CJ"] = 0;
        DataObject["iSTATUS"] = 1;
        SelectYYJLList("listBYY", DataObject, "iJLBH", false);
    });

    $("#DelBYY").click(function () {
        DeleteRows("listBYY");
    });


})



function SaveData() {

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBZ1 = $("#TB_BZ").val();

    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;

    var lstBYY = new Array();
    lstBYY = $("#listBYY").datagrid("getRows");
    Obj.itemTable2 = lstBYY;



    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_BZ").val(Obj.sBZ1);
    $('#list').datagrid("loadData", { total: 0, rows: [] });
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $('#listBYY').datagrid("loadData", { total: 0, rows: [] });
    $('#listBYY').datagrid('loadData', Obj.itemTable2, "json");
    $('#listBYY').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}


