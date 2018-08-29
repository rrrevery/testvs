vUrl = "../HYKGL.ashx";
vCaption = "优惠券批量取款";
var rowNumer = 0;
function SetControlState() {
    ;
}

function IsValidData() {
    if ($("#HF_MDID").val() == "") {
        ShowMessage("请选择门店", 3);
        return false;
    }
    return true;
}

function InitGrid() {
    vColumnNames = ['会员卡卡号', '卡类型', '会员ID', '会员姓名', '会员卡类型', '门店范围代码', '用券范围', '优惠券余额', '取款金额', ],//'门店范围代码', '状态', '促销活动编号', '促销主题', ;
    vColumnModel = [
                { name: 'sHYK_NO', width: 100, },
                { name: 'iHYKTYPE', hidden: true },
                { name: 'iHYID', width: 80 },
                { name: 'sHY_NAME' },
                { name: 'iHYKTYPE', hidden: true },
                { name: 'sMDFWDM', hidden: true },
                { name: 'sMDFWMC', hidden: false, width: 50, },
                { name: 'fJE', width: 50, },
                { name: 'fQKJE', editable: true, },
                //{ name: 'iSTATUS', hidden: true },//align: "right", editable: true,edittype:"select", editoptions: { value:"101:金卡;102:银卡" }
                //{ name: 'iCXID', hidden: true },
                //{ name: 'sCXZT', hidden: true },
    ];
};




$(document).ready(function () {
    $("#menu").tabify();
    //FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE");


    $("#TB_YHQ").click(function () {
        SelectYHQ("TB_YHQ", "HF_YHQ", "zHF_YHQ", true)
    });

    $("#TB_CXHD").click(function () {
        var yhqid = $("#HF_YHQ").val();
        if (yhqid == "") {
            ShowMessage('请选择优惠券', 2);
            return;
        }
        var condData = new Object();
        condData["iYHQID"] = yhqid;
        SelectCXHD("TB_CXHD", "HF_CXID", "zHF_CXID", true, condData);
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });


    $("#AddItem").click(function () {
        if ($("#HF_YHQ").val() == "") {
            ShowMessage("请选择优惠券", 3);
            return;
        }

        if ($("#HF_CXID").val() == "") {
            ShowMessage("请选择促销活动", 3);
            return;
        }
        if ($("#TB_JSSJ").val() == "") {
            ShowMessage("请选择结束日期", 3);
            return;
        }

        if ($("#TB_YHQQKJE").val() == "0" || $("#TB_YHQQKJE").val() == "") {
            ShowMessage("请输入调整金额", 3);
            return;
        }

        var condData = new Object();
        condData["iYHQID"] = $("#HF_YHQ").val();
        condData["iCXID"] = $("#HF_CXID").val();
        condData["dJSRQ"] = $("#TB_JSSJ").val();
        var checkRepeatField = ["iYHQID", "iHYID", "sMDFWDM"];
        SelectYHQZH("list", condData, checkRepeatField);
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    $("#BTN_TZJE").click(function () {
        if ($("#TB_YHQQKJE").val() == "") {
            ShowMessage("请输入统一调整金额", 3);
            return;
        }
        var rows = $("#list").datagrid("getData").rows;
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                var index = $("#list").datagrid('getRowIndex', rows[i]);
                rows[i].fQKJE = $("#TB_YHQQKJE").val();
                $("#list").datagrid("updateRow", rows[i]);
                $("#list").datagrid('refreshRow', index);
            }
        }

    });
    //UploadInit();
})


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    if ($("#HF_CZYMDID").val() != "") {
        Obj.iMDID = $("#HF_CZYMDID").val();
    }
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    for (var i = 0; i < lst.length ; i++) {
        lst[i].sMDFWDM = lst[i].sMDFWDM || " ";
    }
    Obj.itemTable = lst;
    Obj.iHYKTYPE = 0;
    Obj.fCKJE = $("#TB_YHQCZJE").val();
    Obj.iCXID = $("#HF_CXID").val();
    Obj.dJSRQ = $("#TB_JSSJ").val();
    Obj.iYHQID = $("#HF_YHQ").val();
    Obj.fTYQKJE = $("#TB_YHQQKJE").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);//input控件被换成了label...   
    //$("#HF_BGDDDM").val(Obj.sBGDDDM);
    //$("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_ZY").val(Obj.sZY);
    $("#HF_CZYMDID").val(Obj.iMDID);
    //$("#TB_HYKNAME").val(Obj.itemTable[0].sHYKNAME);
    //$("#HF_HYKTYPE").val(Obj.itemTable[0].iHYKTYPE);
    //$("#DDL_YHQ").val(Obj.iYHQID);
    $("#HF_YHQ").val(Obj.iYHQID);
    $("#TB_YHQ").val(Obj.sYHQMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_JSSJ").val(Obj.dJSRQ);
    $("#TB_CXHD").val(Obj.sCXZT);
    $("#HF_CXID").val(Obj.iCXID);
    $("#TB_YHQQKJE").val(Obj.fTYQKJE);

    //$("#DDL_CXHD").val(Obj.iCXID);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}


function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenu("menuContentHYKTYPE");
}

function WUC_Return_YHQZH(MDFWDM, MDFWMC, YHQJE, JSONSTRING) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mdfwdm = "";
            var tp_mdfwmc = "";
            var tp_je = "";
            var tp_hf = "";
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput;
            AddHYK(contractValues);
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mdfwdm += contractValues[i].sMDFWDM + ",";
                tp_mdfwmc += contractValues[i].sMDFWMC + ";";
                tp_je = contractValues[i].fJE + ";";
            }

            $("#" + MDFWDM).text(tp_mdfwdm.substr(0, tp_mdfwdm.length - 1));
            $("#" + MDFWMC).text(tp_mdfwmc.substr(0, tp_mdfwmc.length - 1));
            $("#" + YHQJE).text(tp_je.substr(0, tp_je.length - 1));
            $("#" + JSONSTRING).val(jsonString);

        }
    }
}


function AddHYK(contractValues) {
    var rownum = $("#list").getGridParam("reccount");
    var array = new Array();
    if (rownum <= 0) {
        for (var i = 0; i < contractValues.length; i++) {
            var mydata = [
             {
                 sHYK_NO: contractValues[i].sHYK_NO,
                 iHYID: contractValues[i].iHYID,
                 iHYKTYPE: 0,
                 sHY_NAME: contractValues[i].sHY_NAME,
                 sHYKNAME: "",
                 fQKJE: $("#TB_YHQQKJE").val(),
                 fYHQYE: contractValues[i].fJE,
                 sMDFWDM: contractValues[i].sMDFWDM,
                 sMDFWMC: contractValues[i].sMDFWMC,
             }
            ];
            // jQuery("#list").addRowData(mydata[0].sHYKNO, mydata[0]);
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
            array[j] = ListRow.iHYID;
        }

        for (var q = 0; q < contractValues.length; q++) {
            if (CheckReapet(array, contractValues[q].iHYID)) {
                var mydata = {
                    sHYK_NO: contractValues[q].sHYK_NO,
                    iHYID: contractValues[q].iHYID,
                    iHYKTYPE: 0,
                    sHY_NAME: contractValues[q].sHY_NAME,
                    sHYKNAME: "",
                    fQKJE: $("#TB_YHQQKJE").val(),
                    fYHQYE: contractValues[q].fJE,
                    sMDFWDM: contractValues[q].sMDFWDM,
                    sMDFWMC: contractValues[q].sMDFWMC,
                }
                $("#list").addRowData(rowNumer, mydata);
                rowNumer = rowNumer + 1;
            }
        }



    }
}

//function CheckReapet(array, HYID) {
//    var boolInsert = true;
//    for (i = 0; i < array.length; i++) {
//        if (HYID == array[i]) {
//            boolInsert = false;
//        }

//    }

//    return boolInsert;
//}
function check() {
    if (isNaN($("#TB_YHQQKJE").val())) {
        ShowMessage("请输入数字", 3);
    }
}



function setUploadParam() {
    var colModels = "sHYKNO|fQKJE";
    //var cols = $("#list").jqGrid("getGridParam", "colModel");
    //for (i = 1; i < cols.length; i++) {
    //    if (cols[i].name != "cb") {
    //        colModels += cols[i].name + "|";
    //    }
    //}
    uploader = new plupload.Uploader({
        browse_button: 'B_Import',
        url: '../../CrmLib/CrmLib_BaseImport.ashx?cols=' + colModels,
        filters: {
            mime_types: [
              { title: "Excel Files", extensions: "xlsx,xls" },
            ]
        },
        chunk_size: "200kb",
    });
    //初始化
    uploader.init();
}

function setGridData(result) {
    if (result == "") {
        art.dialog({ content: "数据绑定失败,请重新上传", times: 2 });
        return;
    }
    var arr = new Array();
    arr = JSON.parse(result);
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] != null && arr[i] != "") {
            GetHYXXNew(arr[i].sHYKNO, arr[i].fQKJE);
        }
    }
}

function GetHYXXNew(HYKNO, TZJF) {
    if (HYKNO != "") {
        //根据卡号查询信息
        var str = GetHYXXData(0, HYKNO);
        if (str) {
            var Obj = JSON.parse(str);
            if (CheckReapetImport(Obj)) {
                var otherObj = GetBalance($("#HF_MDID").val(), $("#HF_YHQ").val(), Obj.iHYID, $("#TB_JSSJ").val(), $("#HF_CXID").val());
                if (otherObj != "") {
                    otherObj = JSON.parse(otherObj);
                    if (parseFloat(otherObj.fYHQYE) != 0) {
                        $("#list").addRowData($("#list").getGridParam("reccount"), {
                            sHYK_NO: Obj.sHYK_NO,
                            iHYKTYPE: Obj.iHYKTYPE,
                            iHYID: Obj.iHYID,
                            fYHQYE: otherObj.fYHQYE,
                            sMDFEDM: otherObj.sMDFWDM,
                            sMDFWMC: otherObj.sMDFWMC,
                            sHY_NAME: Obj.sHY_NAME,
                            fQKJE: TZJF,
                        });
                    }
                }
            }
        }
    }
}


function CheckReapetImport(arr) {
    var boolInsert = true;
    var rowIDs = $("#list").getDataIDs();
    for (var i = 0; i < rowIDs.length; i++) {
        var rowData = $("#list").getRowData(rowIDs[i]);
        if (rowData.iHYID == arr.iHYID) {
            boolInsert = false;
        }

    }
    return boolInsert;
}

function WUC_YHQ_ReturnCustom() {
    $("#TB_CXHD").val("");
    $("#HF_CXID").val("");
    $("#zHF_CXID").val("");
    $("#list").jqGrid("clearGridData");
}


function GetBalance(iMDID, iYHQID, iHYID, endDate, iCXID) {
    var tmp = "";
    $.ajax({
        type: 'get',
        url: "../../CrmLib/CrmLib.ashx?func=GetBalance&MDID=" + iMDID + "&YHQID=" + iYHQID + "&EndDate=" + endDate + "&CXID=" + iCXID + "&HYID=" + iHYID + "",
        dataType: "text",
        async: false,
        success: function (data) {
            tmp = data;
        },
        error: function (data) {
            tmp = "";
        }
    });
    return tmp;
}