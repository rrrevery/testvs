vUrl = "../HYXF.ashx";
var HYKNO = GetUrlParam("HYKNO");
var rownum = 0;
var zrjf = 0;

function InitGrid() {
    vColumnNames = ["转出卡号", "HYID", "转出会员姓名", "未处理积分", "有效期", "转出积分"],
    vColumnModel = [
          { name: "sHYK_NO", width: 90, },
          { name: "iHYID", hidden: true },
          { name: "sHY_NAME", width: 90 },
          { name: "fWCLJF", width: 90 },
          //{ name: "fLJJF", width: 60 },
          { name: "dYXQ", width: 60 },
          { name: "fZCJF", width: 60, editor: 'text', editrules: { required: true, number: true, minValue: 1 } },//

    ];
};

$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");

    //$("#list").datagrid({
    //    onAfterEdit: function (index, row) {          
    //        var lst = new Array();
    //        lst = $("#list").datagrid("getData").rows;
    //        var total = 0;
    //        for (var i = 0; i < lst.length; i++) {
    //            total += parseFloat(lst[i].fTZJF);
    //        }
    //        $("#LB_ZRJF").text(total);
    //    },      
    //});
    $("#AddItem").click(function () {
        var DataArry = new Object();
        //DataArry["iMDID"] = parseInt($("#HF_MDID").val());
        SelectHYK('list', DataArry, 'iHYID');

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
function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val());
        if (str == "null" || str == "") {
            art.dialog({ lock: true, content: "没有找到卡号" });
            return;
        }
        var Obj = JSON.parse(str);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#HF_HYID_ZR").val(Obj.iHYID);
        $("#LB_HY_NAME").text(Obj.sHY_NAME);
        $("#LB_WCLJF").text(Obj.fWCLJF);
        $("#LB_LJJF").text(Obj.fLJJF);
        $("#LB_YXQ").text(Obj.dYXQ);
    }
}
function SetControlState() {

}

function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.iHYID = $("#HF_HYID_ZR").val();
    Obj.fWCLJF = $("#LB_WCLJF").text();
    Obj.fLJJF = $("#LB_LJJF").text();
    Obj.dYXQ = $("#LB_YXQ").text();
    Obj.iHYID = $("#HF_HYID_ZR").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();   
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    var total = 0;
    for (var i = 0; i < lst.length; i++) {
        total += parseFloat(lst[i].fZCJF);
    }
    $("#LB_ZRJF").text(total);
    Obj.fZRJF = $("#LB_ZRJF").text();
    Obj.itemTable = lst;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYKNO").val(Obj.sHYKNO);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#HF_HYID_ZR").val(Obj.iHYID);
    $("#LB_HY_NAME").text(Obj.sHY_NAME);
    $("#LB_WCLJF").text(Obj.fWCLJF);
    $("#LB_LJJF").text(Obj.fLJJF);
    $("#LB_YXQ").text(Obj.dYXQ);
    $("#LB_ZRJF").text(Obj.fZRJF);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_ZY").val(Obj.sZY);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

    //if (!$("#B_Exec").attr("disabled")) {
    //    $("#del_list").attr("disabled", true);
    //    $("#list_iladd").attr("disabled", true);
    //}
}

//function LoadHYKData(rowData) {
//    //var rownum = $("#list").getGridParam("reccount");
//    //$("#list").addRowData(rowNum1, { sCZKHM_BEGIN: rowData[0].sCZKHM, sCZKHM_END: rowData[rowData.length - 1].sCZKHM, iHYKTYPE: rowData[0].iHYKTYPE, sHYKNAME: rowData[0].sHYKNAME, iSKSL: rowData.length, fMZJE: rowData[0].fQCYE, });//开始卡号作为ROWID
//    for (i = 0; i < rowData.length; i++) {

//        var mydata = {
//            iHYID: rowData[i].iHYID,
//            sHYK_NO: rowData[i].sHYK_NO,
//            sHY_NAME: rowData[i].sHY_NAME,
//            fWCLJF: rowData[i].fWCLJF,
//            fLJJF: rowData[i].fLJJF,
//            dYXQ: rowData[i].dYXQ,
//            //fTZJF:0,
//        };
//        $("#list").addRowData(rownum, mydata);
//        rownum++;
//    }
//}

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    for (var i = 0; i < lst.length; i++) {
        if (CheckReapet(array, CheckFieldId, lst[i])) {
            $('#list').datagrid('appendRow', {
                iHYID: lst[i].iHYID,
                sHYK_NO: lst[i].sHYK_NO,
                sHY_NAME: lst[i].sHY_NAME,
                fWCLJF: lst[i].fWCLJF,
                //fLJJF: hyk_array[i].fLJJF,
                dYXQ: lst[i].dYXQ,
                fZCJF: lst[i].fWCLJF,
            });
        }      
    }
}

function GetZRJF()
{
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    var total = 0;
    for (var i = 0; i < lst.length; i++) {
        total += parseFloat(lst[i].fTZJF);
    }
    $("#LB_ZRJF").text(total);
}


