vUrl = "../HYXF.ashx";
var cyxxarr = new Array();
cyxxarr.iHYID = 0;//当前选中的成员信息
//格式参照
//var cyxx = new Object();
//cyxx.iHYID = cyxxarr.iHYID;
//var bmxx = new Object();
//bmxx.iSJNR = contractValues[i].Id;
//bmxx.sSJMC = contractValues[i].Name;
//if (index == -1) {
//    cyxx.BM = new Array();
//    cyxx.PL = new Array();
//    cyxx.BM.push(bmxx);
//    index = cyxxarr.length;
//    cyxxarr[cyxxarr.length] = cyxx;

//}
var vBMXXColumnNames;
var vBMXXColumnModel;
var vPLXXColumnNames;
var vPLXXColumnModel;

function InitGrid() {
    vColumnNames = ['HYID', '姓名', '卡号', '卡类型'];
    vColumnModel = [
          { name: 'iHYID', hidden: true },
          { name: 'sHY_NAME', width: 100, },
          { name: 'sHYK_NO', width: 100, },
          { name: 'sHYKNAME', width: 100, },
    ];
    vBMXXColumnNames = ['HYID', 'SJLX', '部门ID', '部门名称'];
    vBMXXColumnModel = [
        { name: 'iHYID', hidden: true },
           { name: 'iSJLX', hidden: true },
           { name: 'iSJNR', width: 80, },
           { name: 'sSJMC', width: 80, },
    ];
    vPLXXColumnNames = ['HYID', 'SJLX', '品牌代码', '品牌名称'];
    vPLXXColumnModel = [
        { name: 'iHYID', hidden: true },
           { name: 'iSJLX', hidden: true },
           { name: 'iSJNR', width: 80, },
           { name: 'sSJMC', width: 80, },
    ];

}




$(document).ready(function () {
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);

    });
    $("#B_Stop").show();
    DrawGrid("list_BM", vBMXXColumnNames, vBMXXColumnModel, true);
    DrawGrid("list_PL", vPLXXColumnNames, vPLXXColumnModel, true);
    $("#S_QZLX").click(function () {
        if ($("#HF_MDID").val() == "") {
            ShowMessage("请选择门店");

        }
    })
    //$("#B_Stop").show().html("作废");
    var qzcyrs = $("#S_QZLX option:selected").attr("id");
    var index;
    if (qzcyrs != "" && qzcyrs != undefined) {
        index = qzcyrs.indexOf("_");
        qzcyrs = qzcyrs.substring(index + 1);
    }
    else
        qzcyrs = 0;

    $("#LB_QZCYRS").text(qzcyrs);
    $("#S_QZLX").change(function () {
        qzcyrs = $("#S_QZLX").children("option:selected").attr("id");
        if (qzcyrs != undefined) {
            var index = qzcyrs.indexOf("_");
            qzcyrs = qzcyrs.substring(index + 1);
            $("#LB_QZCYRS").text(qzcyrs);
            //$("#CYXXList").jqGrid("clearGridData");
        }

    });
    $("#AddItem").click(function () {
        if ($("#HF_MDID").val() == "" || $("#S_QZLX")[0].value == "") {
            ShowMessage("请选择门店和圈子类型", 3);
            return;
        }

        var DataArry = new Object();
        //DataArry["iMDID"] = parseInt($("#HF_MDID").val());
        SelectHYK('list', DataArry, 'iHYID');

    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });


});
function SetControlState() {
    //$("#B_Exec").hide();
    $(".title").show();
    if (vProcStatus == cPS_ADD) {
        cyxxarr = new Array();
        cyxxarr.iHYID = 0;
    }

    if (parseFloat($("#HF_STATUS").val()) == 2) {
        $("#B_Stop").prop("disabled", true);
    }
    if (parseFloat($("#HF_STATUS").val()) == 1) {
        $("#B_Stop").prop("disabled", false);
    }
    else {
        $("#B_Stop").prop("disabled", true);
    }
}

function IsValidData() {
    var rows = $("#list").datagrid("getData").rows.length;
    if (rows < 1) {
        ShowMessage("没有选择可操作的卡", 3);
        return false;
    }
    if (rows > $("#LB_QZCYRS").text())
    {
        ShowMessage("选择卡数超过圈子人数", 3);
        return false;
    }

    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sQZMC = $("#TB_QZMC").val();
    Obj.iQZLXID = $("#S_QZLX")[0].value;
    Obj.iQZCYRS = $("#LB_QZCYRS").text();
    Obj.iMDID = $("#HF_MDID").val();
    //var CYXXItemTable = new Array();
    //var IDs = $("#CYXXList").getDataIDs();
    //for (var i = 0; i < IDs.length; i++) {
    //    var rowData = $("#CYXXList").getRowData(IDs[i]);
    //    CYXXItemTable.push(rowData);
    //}

    //var BMPLItemTable = new Array();
    //for (var i = 0; i < cyxxarr.length; i++) {
    //    var data = new Object();
    //    for (var j = 0; j < cyxxarr[i].BM.length; j++) {
    //        data = new Object();
    //        data.iHYID = cyxxarr[i].iHYID;
    //        data.iSJLX = 1;
    //        data.iSJNR = cyxxarr[i].BM[j].iSJNR;
    //        BMPLItemTable.push(data);
    //    }

    //    for (var j = 0; j < cyxxarr[i].PL.length; j++) {
    //        data = new Object();
    //        data.iHYID = cyxxarr[i].iHYID;
    //        data.iSJLX = 2;
    //        data.iSJNR = cyxxarr[i].PL[j].iSJNR;
    //        BMPLItemTable.push(data);
    //    }


    //}
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.CYXXItemTable = lst;
    //Obj.CYXXItemTable = CYXXItemTable;
    //Obj.BMPLItemTable = BMPLItemTable;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#S_QZLX").empty();
    FiLLQZLX($("#S_QZLX"), Obj.iMDID);
    $("#TB_QZMC").val(Obj.sQZMC);
    $("#S_QZLX").val(Obj.iQZLXID);
    $("#LB_QZCYRS").text(Obj.iQZCYRS);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_STATUS").val(Obj.iSTATUS);
    $('#list').datagrid('loadData', Obj.CYXXItemTable, "json");
    $('#list').datagrid("loaded");

    //for (var i = 0; i < Obj.CYXXItemTable.length; i++) {
    //    $("#CYXXList").addRowData(i, Obj.CYXXItemTable[i]);
    //}
    //cyxxarr = new Array();
    //cyxxarr.iHYID = 0;
    //将成员信息及部门品牌信息保存在全局变量当中
    //for (var i = 0; i < Obj.BMPLItemTable.length; i++) {
    //    var isexists = false;
    //    var index = -1;
    //    var cyxx = new Object();

    //    for (var j = 0; j < cyxxarr.length; j++) {
    //        if (Obj.BMPLItemTable[i].iHYID == cyxxarr[j].iHYID) {
    //            isexists = true;
    //            index = j;
    //            break;
    //        }
    //    }

    //    if (!isexists) {
    //        cyxx.iHYID = Obj.BMPLItemTable[i].iHYID
    //        cyxx.BM = new Array();
    //        cyxx.PL = new Array();

    //    }
    //    else {
    //        cyxx = cyxxarr[index];
    //    }
    //    if (Obj.BMPLItemTable[i].iSJLX == 1) {//部门
    //        var bmxx = new Object();
    //        //bmxx.iSJLX = 1;
    //        bmxx.iSJNR = Obj.BMPLItemTable[i].iSJNR;
    //        bmxx.sSJMC = Obj.BMPLItemTable[i].sSJMC;
    //        cyxx.BM.push(bmxx);
    //        if (!isexists) {
    //            cyxxarr.push(cyxx);
    //        }
    //        else {
    //            cyxxarr[index] = cyxx;//数组是引用类型，此行应该可以去掉
    //        }
    //    }
    //    if (Obj.BMPLItemTable[i].iSJLX == 2) {//品牌
    //        var plxx = new Object();
    //        //bmxx.iSJLX = 2;
    //        plxx.iSJNR = Obj.BMPLItemTable[i].iSJNR;
    //        plxx.sSJMC = Obj.BMPLItemTable[i].sSJMC;
    //        cyxx.PL.push(plxx);

    //        if (!isexists) {

    //            cyxxarr.push(cyxx);
    //        }
    //        else {
    //            cyxxarr[index] = cyxx;//数组是引用类型，此行应该可以去掉
    //        }
    //    }


    //}
    //登记人
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);

    //审核人
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

    //设置显示
    //var rowids = $("#CYXXList").getDataIDs();
    //if (rowids.length > 0) {
    //    $("#CYXXList").setSelection(rowids[rowids.length - 1]);
    //}
}

//显示部门  品牌
function ShowBMPL(sjlx) {
    var list = "BMList";
    var BMPL = "BM";
    if (sjlx == 2) {
        list = "PLList";
        BMPL = "PL";
    }

    $("#" + list).clearGridData();

    for (var i = 0; i < cyxxarr.length; i++) {
        if (cyxxarr[i].iHYID == cyxxarr.iHYID) {
            for (var j = 0; j < cyxxarr[i][BMPL].length; j++) {
                $("#" + list).addRowData(j, {
                    iHYID: cyxxarr.iHYID,
                    iSJLX: sjlx,
                    iSJNR: cyxxarr[i][BMPL][j].iSJNR,
                    sSJMC: cyxxarr[i][BMPL][j].sSJMC,
                });
            }
            break;
        }
    }
    return;

}
// 删除部门  品牌 
function DelBMPL(sjlx, delAll) {
    var list = "BMList";
    var BMPL = "BM";
    var title = "部门";
    if (sjlx == 2) {
        list = "PLList";
        BMPL = "PL";
        title = "品牌";
    }

    if (sjlx != -1) {
        var selRow = $("#" + list).jqGrid("getGridParam", "selarrrow");
        if (selRow.length == 0) {
            art.dialog({ title: '没有选中', content: '没有被选中的' + title + '!' });
            return;

        }
    }

    if (!delAll) {
        var len = selRow.length;
        for (var k = 0; k < len; k++) {
            var rowData = new Object();
            rowData = $("#" + list).getRowData(selRow[0]);
            for (var i = 0; i < cyxxarr.length; i++) {
                if (cyxxarr[i].iHYID == cyxxarr.iHYID) {
                    for (var j = 0; j < cyxxarr[i][BMPL].length; j++) {
                        if (cyxxarr[i][BMPL][j].iSJNR == rowData.iSJNR) {
                            cyxxarr[i][BMPL].splice(j, 1);
                            j--;
                            break;

                        }
                    }
                    break;
                }
            }
            $("#" + list).jqGrid("delRowData", selRow[0]);

        }
    }

    if (delAll) {
        $("#PLList").clearGridData();
        $("#BMList").clearGridData();

        //从cyxxarr表中删除
        for (var i = 0; i < cyxxarr.length; i++) {
            if (cyxxarr.iHYID == cyxxarr[i].iHYID) {
                cyxxarr.splice(i, 1);
                i--;
                break;
            }
        }
    }


}
//部门 品牌 去重
function CheckRepeatBMPL(data, Id, list) {
    var rowids = $("#" + list).getDataIDs();
    var rowid = 0;
    if (rowids.length > 0) {
        rowid = rowids[rowids.length - 1] - 0;//以便转成整型
        //去重
        for (var j = 0; j < data.length; j++) {
            for (var i = 0; i < rowids.length; i++) {
                if (data[j][Id] == $("#" + list).getRowData(rowids[i]).iSJNR) {
                    data.splice(j, 1);
                    j--;
                    break;
                }

            }
        }

    }

}



function RequireContinue(hyk_array) {
    var boolInsert = true;
    for (var i = 0; i < hyk_array.length; i++) {
        if (CheckUniqueCustomer($("#S_QZLX")[0].value, hyk_array[i].iHYID) == false) {
            boolInsert = false;
        }
    }
    return boolInsert;

}

function CheckUniqueCustomer(CirleNumber, MemberId) {
    var boolInsert = false;
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=CheckUniqueCustomer&iCirleNumber=" + CirleNumber + "&MemberId=" + MemberId + "",
        dataType: "Text",
        async: false,
        success: function (data) {
            if (data == "True" || data == "true") {
                boolInsert = true;
            }
            else {
                boolInsert = false;
            }
        },
        error: function (data) {

        }
    })
    return boolInsert;
}




function MoseDialogCustomerReturn(dialogName, lstData, showField) {
    if (dialogName == "ListMD")
    {
        $("#S_QZLX option:not(:first)").remove();
        FiLLQZLX($("#S_QZLX"), $("#HF_MDID").val());
    }
}


