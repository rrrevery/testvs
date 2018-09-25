vUrl = "../HYXF.ashx";
var HYKNO = GetUrlParam("HYKNO");
function InitGrid() {
    vColumnNames = ["HYID", "会员卡号", "HYKTYPE", "卡类型", "原未处理积分", "调整积分", "调整金额"],
    vColumnModel = [
          { name: "iHYID", hidden: true },
          //{ name: "sHYKNO", width: 60, index: true, },
           { name: "sHYKNO", width: 60, },
          //{ name: "sHY_NAME", width: 60 },
          //会员卡类型隐藏 实际出来的是会员卡名称
          { name: "iHYKTYPE", width: 60, hidden: true, },
          { name: "sHYKNAME", width: 60 },
          { name: "fWCLJF_OLD", width: 90 },
          { name: "fTZJF", width: 90, editor: 'text', editrules: { required: true, number: true, minValue: 1 } },
          { name: "fTZJE", width: 90, editor: 'text', editrules: { required: true, number: true, minValue: 1 } },

    ];
};

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
    $("#AddItem").click(function () {
        var DataArry = new Object();
        //DataArry["iMDID"] = parseInt($("#HF_MDID").val());
        SelectHYK('list', DataArry, 'iHYID');

    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });
    if (HYKNO != "") {
        GetHYXX(HYKNO);
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }

    //$("#TB_MDMC").click(function () {
    //    if ($("#HF_MDID").val() == "") {
    //        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    //    }
    //    else {
    //        ShowYesNoMessage("是否确定更换门店？", function () {
    //        $('#list').datagrid('loadData', { total: 0, rows: [] });
    //        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    //        });
    //    }
    //});

    $("#BTN_TZJF").click(function () {
        var row = $("#list").datagrid("getRows");
        for (var i = 0 ; i < row.length; i++) {
            var index = $('#list').datagrid('getRowIndex',row[i]);
            $('#list').datagrid('updateRow', {
                index: index,
                row: {                   
                  fTZJF: $("#TB_JF").val(),
                }
            });
        }

    });
    $("#BTN_TZJE").click(function () {
        var row = $("#list").datagrid("getRows");
        for (var i = 0 ; i < row.length; i++) {
            var index = $('#list').datagrid('getRowIndex', row[i]);
            $('#list').datagrid('updateRow', {
                index: index,
                row: {
                    fTZJE: $("#TB_JE").val(),
                }
            });
        }

    });
});

function InsertClickCustom() {
    $("#CK_iBJ_CLWCLJF")[0].checked = true;
    //$("#CK_iBJ_CLBQJF")[0].checked = true;
    //$("#CK_iBJ_CLBNLJJF")[0].checked = true;
    //$("#CK_iBJ_CLLJJF")[0].checked = true;
};

function TreeNodeClickCustom(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
}

function IsValidData() {
    //var rows = $("#list").getGridParam("records");
    //if (rows < 1) {
    //    art.dialog({ content: '没有选择可操作的卡' });
    //    return false;
    //}
    //if ($("#HF_MDID").val() == "") {
    //    art.dialog({ content: '请填写操作门店!' });
    //    return false;
    //}
    if ($("#HF_BGDDDM").val() == "") {
        art.dialog({ content: '请选择操作地点!' });
        return false;
    }
    var rows = $("#list").datagrid("getData").rows;
    if (rows.length < 1) {
        ShowMessage("没有选择可操作的卡", 3);
        return false;
    }
    for (var i = 0; i < rows.length; i++) {

        if (rows[i].fTZJF=="") {
            ShowMessage("请输入调整积分", 3);
            return false;
        }
        if (isNaN(rows[i].fTZJE)) {
            ShowMessage("请输入调整金额", 3);
            return false;
        }
    }
  
    


    //if (($("#").val() == "")||($("#").val()=="") ){
    //    art.dialog({ content: '请填写变动积分或者变动金额!' });
    //}
    //for (i = 0; i < $("#list").getGridParam("reccount") ; i++) {

    //    var rowData = $("#list").jqGrid("getRowData", i);//$("#list").getRowData(i);
    //    if (rowData.fTZJE == "" && rowData.fTZJF == "") {
    //        art.dialog({ content: '请填写变动积分或者变动金额!' });
    //        return false;
    //    }
    //}

    if ($("#CK_iBJ_CLWCLJF")[0].checked == false) {
        if ($("#CK_iBJ_CLBQJF")[0].checked == false) {
            if ($("#CK_iBJ_CLBNLJJF")[0].checked == false) {
                if ($("#CK_iBJ_CLLJJF")[0].checked == false) {
                    ShowMessage('请选择积分的调整方式',3 );
                    return false;
                }
            }
        }
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sZY = $("#TB_ZY").val();
    if ($("#CK_iBJ_CLWCLJF")[0].checked) {
        Obj.iBJ_CLWCLJF = 1;
    }
    else
        Obj.iBJ_CLWCLJF = 0;
    if ($("#CK_iBJ_CLBQJF")[0].checked) {
        Obj.iBJ_CLBQJF = 1;
    }
    else
        Obj.iBJ_CLBQJF = 0;
    if ($("#CK_iBJ_CLBNLJJF")[0].checked) {
        Obj.iBJ_CLBNLJJF = 1;
    }
    else
        Obj.iBJ_CLBNLJJF = 0;
    if ($("#CK_iBJ_CLLJJF")[0].checked) {
        Obj.iBJ_CLLJJF = 1;
    }
    else
        Obj.iBJ_CLLJJF = 0;


    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    //前面那个表中的所有数据
    var Obj = JSON.parse(data);
    //list 上面的数据
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_ZY").val(Obj.sZY);
    //兑奖积分
    if (Obj.iBJ_CLWCLJF == "1") {
        $("#CK_iBJ_CLWCLJF")[0].checked = true;
    }
    else {
        $("#CK_iBJ_CLWCLJF")[0].checked = false;
    }
    //升级积分
    if (Obj.iBJ_CLBQJF == "1") {
        $("#CK_iBJ_CLBQJF")[0].checked = true;
    }
    //本年积分
    if (Obj.iBJ_CLBNLJJF == "1") {
        $("#CK_iBJ_CLBNLJJF")[0].checked = true;
    }
    //累计积分
    if (Obj.iBJ_CLLJJF == "1") {
        $("#CK_iBJ_CLLJJF")[0].checked = true;
    }

    //list数据
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    //list下面的数据   
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function GetHYXX(HYKNO) {
    if (HYKNO != "") {
        //根据卡号查询信息
        var str = GetHYXXData(0, HYKNO);
        if (str == "null" || str == "") {
            art.dialog({ lock: true, content: "没有找到卡号" });
            return;
        }
        var Obj = JSON.parse(str);
        //$("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        //$("#TB_HYKNAME").val(Obj.sHYKNAME);

        //$("#list").addRowData($("#list").getGridParam("reccount"), {
        //    sHYKNO: Obj.sHYK_NO,
        //    iHYKTYPE: Obj.iHYKTYPE,
        //    fWCLJF_OLD: Obj.fWCLJF,
        //    iHYID: Obj.iHYID,
        //    sHYKNAME: Obj.sHYKNAME,

        //});
        $('#list').datagrid('appendRow', {
            sHYKNO: Obj.sHYK_NO,
            iHYKTYPE: Obj.iHYKTYPE,
            fWCLJF_OLD: Obj.fWCLJF,
            iHYID: Obj.iHYID,
            sHYKNAME: Obj.sHYKNAME,
            fTZJF: 0,
            fTZJE: 0,

        });

    }
}

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    for (var i = 0; i < lst.length; i++) {
        if (CheckReapet(array, CheckFieldId, lst[i])) {
            $('#list').datagrid('appendRow', {
                sHYKNO: lst[i].sHYK_NO,
                iHYKTYPE: lst[i].iHYKTYPE,
                fWCLJF_OLD: lst[i].fWCLJF,
                iHYID: lst[i].iHYID,
                sHYKNAME: lst[i].sHYKNAME,
                fTZJF: 0,
                fTZJE: 0,
            });
        }
    }

}

//function setUploadParam() {
//    var colModels = "sHYKNO|fTZJF";
//    //var cols = $("#list").jqGrid("getGridParam", "colModel");
//    //for (i = 1; i < cols.length; i++) {
//    //    if (cols[i].name != "cb") {
//    //        colModels += cols[i].name + "|";
//    //    }
//    //}
//    uploader = new plupload.Uploader({
//        browse_button: 'B_Import',
//        url: '../../CrmLib/CrmLib_BaseImport.ashx?cols=' + colModels,
//        filters: {
//            mime_types: [
//              { title: "Excel Files", extensions: "xlsx,xls" },
//            ]
//        },
//        chunk_size: "200kb",
//    });
//    //初始化
//    uploader.init();
//}

//function setGridData(result) {
//    if (result == "") {
//        art.dialog({ content: "数据绑定失败,请重新上传", times: 2 });
//        return;
//    }
//    var arr = new Array();
//    arr = JSON.parse(result);
//    for (var i = 0; i < arr.length; i++) {
//        if (arr[i] != null && arr[i] != "") {
//            GetHYXXNew(arr[i].sHYKNO, arr[i].fTZJF);
//        }
//    }
//}

//function GetHYXXNew(HYKNO, TZJF) {
//    if (HYKNO != "") {
//        //根据卡号查询信息
//        var str = GetHYXXData(0, HYKNO);
//        if (str != "null" && str != "") {
//            var Obj = JSON.parse(str);
//            if (CheckReapet(Obj)) {
//                $("#list").addRowData($("#list").getGridParam("reccount"), {
//                    sHYKNO: Obj.sHYK_NO,
//                    iHYKTYPE: Obj.iHYKTYPE,
//                    fWCLJF_OLD: Obj.fWCLJF,
//                    iHYID: Obj.iHYID,
//                    sHYKNAME: Obj.sHYKNAME,
//                    fTZJF: TZJF,
//                    fTZJE: 0,
//                });
//            }
//        }
//    }
//}

//function CheckReapet(arr) {
//    var boolInsert = true;
//    var rowIDs = $("#list").getDataIDs();
//    for (var i = 0; i < rowIDs.length; i++) {
//        var rowData = $("#list").getRowData(rowIDs[i]);
//        if (rowData.iHYID == arr.iHYID) {
//            boolInsert = false;
//        }

//    }
//    return boolInsert;
//}