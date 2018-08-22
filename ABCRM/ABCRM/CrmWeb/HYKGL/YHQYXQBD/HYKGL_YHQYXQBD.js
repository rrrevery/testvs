vUrl = "../HYKGL.ashx";
var rowNumer = 0;
var id;
var sOldValue;
sOldValue = "";

function InitGrid() {
    vColumnNames = ["会员ID", "卡号", "iYHQID", '优惠券', '门店范围代码', '原有效期', '金额'];
    vColumnModel = [
            { name: "iHYID", hidden: true, },
            { name: 'sHYK_NO', width: 200, },
            { name: "iYHQID", hidden: true, },
            { name: "sYHQMC", width: 200 },
            { name: 'sMDFWDM', hidden: true, },
            { name: "dJSRQ", width: 200 },
            { name: "fJE", width: 200 },
    ];
};

$(document).ready(function () {


    $("#AddItem").click(function () {
        if ($.trim($("#HF_YHQID").val()) == "") {
            ShowMessage("请选择优惠券！", 3);
            return;
        }
        if ($.trim($("#HF_CXID").val()) == "") {
            ShowMessage("请选择促销活动！", 3);
            return;
        }

        if ($("#TB_JSSJ1").val() == "" && $("#TB_JSSJ2").val() == "") {
            ShowMessage("请输入结束日期范围！", 3);
            return;
        }
        var condData = new Object();
        condData["iYHQID"] = $("#HF_YHQID").val();
        condData["iCXID"] = $("#HF_CXID").val();
        condData["dJSRQ1"] = $("#TB_JSSJ1").val();
        condData["dJSRQ2"] = $("#TB_JSSJ2").val();
        var checkRepeatField = ["iHYID", "iCXID", "sMDFWDM"];
        SelectYHQZH("list", condData, checkRepeatField);

    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });



    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", true);
    });
    $("#TB_CXZT").click(function () {
        if ($("#HF_YHQID").val() == "") {
            ShowMessage("请先选择优惠券", 3);
            return;
        }
        if ($("#list").datagrid("getRows").length > 0) {
            art.dialog({
                title: "删除",
                lock: true,
                content: "是否清空数据？",
                ok: function () {
                    $("#list").datagrid("loadData", { total: 0, rows: [] });
                    SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", true);
                },
                okVal: '是',
                cancelVal: '否',
                cancel: true
            });
        }
        else {
            SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", true, $("#HF_YHQID").val());
        }
    });

})



function IsValidData() {
    if ($("#TB_XYXQ").val() == "") {
        ShowMessage("请输入新有效期",3);
        return false;
    }
    if ($("#HF_MDID").val() == "") {
        ShowMessage("操作门店不能为空",3);
        return false;
    }
    if ($("#HF_YHQID").val() == "") {
        ShowMessage("优惠券不能为空",3);
        return false;
    }
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.dXYXQ = $("#TB_XYXQ").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.dJSSJ1 = $("#TB_JSSJ1").val();
    Obj.dJSSJ2 = $("#TB_JSSJ2").val();

    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;

    if ($("#HF_CXID").val() != "") {
        Obj.iCXID = $("#HF_CXID").val();
    }
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}





function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_XYXQ").val(Obj.dXYXQ);
    $("#TB_ZY").val(Obj.sZY);
    $("#HF_CXID").val(Obj.iCXID);
    $("#TB_CXZT").val(Obj.sCXZT);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_YHQMC").val(Obj.itemTable[0].sYHQMC);
    $("#HF_YHQID").val(Obj.itemTable[0].iYHQID);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#TB_JSSJ1").val(Obj.dJSSJ1);
    $("#TB_JSSJ2").val(Obj.dJSSJ2);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}




function LoadData(rowData) {
    var rownum = $("#list").getGridParam("reccount");
    // var mydata;
    var array = new Array();
    if (rownum <= 0) {
        for (var i = 0; i < rowData.length; i++) {
            var mydata = [
             {
                 iHYID: rowData[i].iHYID,
                 sHYK_NO: rowData[i].sHYK_NO,
                 iYHQID: rowData[i].iYHQID,
                 sYHQMC: rowData[i].sYHQMC,
                 fJE: rowData[i].fJE,
                 dYYXQ: rowData[i].dJSRQ,
                 sMDFWDM: rowData[i].sMDFWDM,

             }
            ];
            //  $("#list").addRowData($("#list").getGridParam("reccount"), mydata[0]);
            $("#list").addRowData(rowNumer, mydata);
            rowNumer = rowNumer + 1;
        }
    }

    else {
        var rowIDs = $("#list").getDataIDs();
        if (rowNumer == 0) {
            rowNumer = parseInt(rowIDs[rowIDs.length - 1]) + parseInt(1);
        }
        for (var j = 0; j < rowIDs.length; j++) {
            var ListRow = $("#list").getRowData(rowIDs[j]);
            array[j] = ListRow;
        }


        for (var q = 0; q < rowData.length; q++) {
            if (CheckReapet(array, rowData[q])) {
                var mydata = {
                    iHYID: rowData[q].iHYID,
                    sHYK_NO: rowData[q].sHYK_NO,
                    iYHQID: rowData[q].iYHQID,
                    sYHQMC: rowData[q].sYHQMC,
                    fJE: rowData[q].fJE,
                    dYYXQ: rowData[q].dJSRQ,
                    sMDFWDM: rowData[q].sMDFWDM,
                }
                $("#list").addRowData(rowNumer, mydata);
                rowNumer = rowNumer + 1;
            }
        }
    }
}

function CheckReapet(array, checkRow) {
    //HYID, YHQID, YYXQ, MDFWDM
    var boolInsert = true;
    for (var i = 0; i < array.length; i++) {
        if (array[i].iHYID == checkRow.iHYID && array[i].iYHQID == checkRow.iYHQID) {
            if (array[i].dYYXQ == checkRow.dJSRQ && array[i].sMDFWDM == checkRow.sMDFWDM) {
                boolInsert = false;
            }

        }
    }
    return boolInsert;
}


