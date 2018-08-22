vUrl = "../LPGL.ashx";
var rowNumer = 0;

function InitGrid() {
    vColumnNames = ["LPID", "礼品代码", "礼品名称", "礼品单价", "礼品积分", "礼品库存", "盈亏数量", "盘点库存"];
    vColumnModel = [
          { name: "iLPID", hidden: true },
          { name: "sLPDM", width: 60, },
          { name: "sLPMC", width: 90 },
          { name: "fLPDJ", hidden: true },
          { name: "fLPJF", hidden: true },
          { name: "fKCSL", width: 60 },
          { name: "fPKSL", width: 60, },
          { name: "fPDKC", width: 60, editor: 'text' },
    ];
};

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");

    $("#AddItem").click(function () {
        if ($("#HF_BGDDDM").val() == "") {
            ShowMessage("请选择保管地点!", 3);
            return false;
        }
        if ($("#TB_PDRQ").val() == "") {
            ShowMessage("请选择盘点日期!", 3);
            return false;
        }
        var DataArry = new Object();
        DataArry["iDJLX"] = 1;
        DataArry["sBGDDDM"] = $("#HF_BGDDDM").val();
        DataArry["iPDCX"] = 1;  
        SelectLP('list', DataArry, 'iLPID');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
        var rows = $('#list').datagrid("getSelections");
        if (rows.length <= 0) {
            document.getElementById("TB_BGDDMC").disabled = false;
        }
    });

});


function IsValidData() {
    if ($("#TB_BGDDMC").val() == "") {
        ShowMessage("保管地点名称不能为空", 3);
        return false;
    }
    return true;
}
function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.dPDRQ = $("#TB_PDRQ").val();
    
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
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
    $("#TB_PDRQ").val(Obj.dPDRQ);
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


function CheckReapet(array, lpDate) {
    var boolInsert = true;
    for (i = 0; i < array.length; i++) {
        if (lpDate == array[i]) {
            boolInsert = false;
        }
    }

    return boolInsert;
}

//function LoadData(rowData) {
//    var rownum = $("#list").getGridParam("reccount");
//    var mydata;
//    var array = new Array();
//    if (rownum <= 0) {
//        for (k = 0; k < rowData.length; k++) {
//            mydata = {
//                iLPID: rowData[k].iLPID,
//                sLPDM: rowData[k].sLPDM,
//                sLPMC: rowData[k].sLPMC,
//                fLPDJ: rowData[k].cLPDJ,
//                fLPJF: rowData[k].fLPJF,
//                fLPKC: rowData[k].fKCSL,
//                fPKSL:0,
//            };
//            $("#list").addRowData(rowNumer, mydata);
//            rowNumer = rowNumer + 1;

//        }
//    }
//    else {
//        if (rowNumer == 0) {
//            var rowIDs = $("#list").getDataIDs();
//            rowNumer = rowIDs[rowIDs.length - 1] + 1;
//        }

//        for (j = 0; j < rowNumer; j++) {
//            if ($("#list").getRowData(j) != "") {
//                var ListRow = $("#list").getRowData(j);
//                array[j] = ListRow.sLPDM;
//            }
//        }


//        for (q = 0; q < rowData.length; q++) {
//            if (CheckReapet(array, rowData[q].sLPDM)) {
//                mydata = {
//                    iLPID: rowData[q].iLPID,
//                    sLPDM: rowData[q].sLPDM,
//                    sLPMC: rowData[q].sLPMC,
//                    fLPDJ: rowData[q].cLPDJ,
//                    fLPJF: rowData[q].fLPJF,
//                    fLPKC: rowData[q].fKCSL,
//                    fPKSL: 0,
//                }

//                $("#list").addRowData(rowNumer, mydata);
//                rowNumer = rowNumer + 1;
//            }
//        }
//    }
//    if ($("#list").getGridParam("reccount") > 0) {
//        //  $("#TB_BGDDMC_BC").disabled = true;
//        document.getElementById("TB_BGDDMC").disabled = true;
//    }
//}

function CheckInventoryDate(dPDRQ)
{
    var sjson = "{'dPDRQ':'" + dPDRQ + "','iRQYZ':'1'}";

    var result = "";
    $.ajax({
        type: "post",
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        async: false,
        data: { json: sjson, titles: 'sss' },
        success: function (data) {
            result = data;                
        },
        error: function (data) {
            result = "";
            alert(data);         
        }
    });
    return result;
}


