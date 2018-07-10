vUrl = "../HYKGL.ashx";
vCaption = "优惠券批量存款";
function InitGrid() {
    vColumnNames = ['会员卡卡号', '会员ID', '会员卡类型', '存款金额', '会员姓名', ];
    vColumnModel = [
            { name: 'sHYK_NO', width: 100, },
            { name: 'iHYID' },
            { name: 'iHYKTYPE', hidden: true },
            { name: 'fCKJE', editable: true, },
            { name: 'sHY_NAME' },
    ];
};
function IsValidData() {

    if ($("#DDL_YHQ").val() == "") {
        ShowMessage("请选择优惠券", 3);
        return false;
    }
    if ($("#HF_CXID").val() == "") {
        ShowMessage("请选择促销活动", 3);
        return false;
    }
    if ($("#TB_YHQCZJE").val() == "") {
        ShowMessage("请输入充值金额", 3);
        return false;
    }

    if ($("#TB_JSSJ").val() == "") {
        ShowMessage("请输入选择结束日期", 3);
        return false;
    }
    //if ($("#HF_MDID").val() == "") {
    //    ShowMessage("请选择门店", 3);
    //    return false;
    //}
    return true;
}

$(document).ready(function () {
    $("#menu").tabify();
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");

    $("#TB_YHQ").click(function () {
        SelectYHQ("TB_YHQ", "HF_YHQ", "zHF_YHQ", true)
    });

    $("#TB_CXHD").click(function () {
        var yhqid = $("#HF_YHQ").val();
        if (yhqid == "") {
            ShowMessage("请选择优惠券", 3);
            return;
        }
        var condData = new Object();
        condData["iYHQID"] = yhqid;
        SelectCXHD("TB_CXHD", "HF_CXID", "zHF_CXID", true, condData);
    });

    $("#TB_YQFWMC").click(function () {
        if ($("#HF_YHQ").val() == "") {
            ShowMessage("请先选择优惠券", 3);
            return;
        }
        switch (parseInt($("#HF_FS_YQMDFW").val())) {
            case 1:
                $("#TB_YQFWMC").val("集团").prop("readonly", true);
                $("#HF_YQFWDM").val("");
                break;
            case 2:
                $("#TB_YQFWMC").prop("readonly", false);
                $("#zHF_YQFWDM").val("");
                SelectSH("TB_YQFWMC", "HF_YQFWDM", "zHF_YQFWDM", true);
                break;
            case 3:
                $("#TB_YQFWMC").prop("readonly", false);
                $("#zHF_YQFWDM").val("");
                SelectMD("TB_YQFWMC", "HF_YQFWDM", "zHF_YQFWDM", true);
                break;
        }
    });

    $("#AddItem").click(function () {
        if ($("#TB_YHQCZJE").val() == "0" || $("#TB_YHQCZJE").val() == "") {
            ShowMessage("请输入存款金额", 3);
            return;
        }
        var DataArry = new Object();
        SelectHYK('list', DataArry, 'ListHYK', 'iHYID');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    $("#BTN_TZJE").click(function () {
        if ($("#TB_YHQCZJE").val() == "") {
            ShowMessage("请输入存款金额", 3);
            return;
        }
        var rows = $("#list").datagrid("getData").rows;
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                var index = $("#list").datagrid('getRowIndex', rows[i]);
                rows[i].fCKJE = $("#TB_YHQCZJE").val();
                $("#list").datagrid("updateRow", rows[i]);
                $("#list").datagrid('refreshRow', index);
            }
        }
    });

    $("#TB_YHQ").change(function () {
        $("#TB_CXHD").val("");
        $("#HF_CXID").val("");
        $("#zHF_CXID").val("");
        // $("#list").jqGrid("clearGridData");
    });

    // UploadInit();
})

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);//input控件被换成了label...   
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_ZY").val(Obj.sZY);
    $("#TB_YQFWMC").val(Obj.sMDFWMC);
    $("#HF_YQFWDM").val(Obj.sMDFWDM);
    $("#HF_YHQ").val(Obj.iYHQID);
    $("#TB_YHQ").val(Obj.sYHQMC);

    $("#TB_MDMC").val(Obj.sMDFWMC);
    $("#TB_JSSJ").val(Obj.dJSRQ);
    $("#TB_CXHD").val(Obj.sCXZT);
    $("#HF_CXID").val(Obj.iCXID);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#TB_YHQCZJE").val(Obj.fTYCKJE);
    $("#HF_FS_YQMDFW").val(Obj.iFS_YQMDFW);
};

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    Obj.iHYKTYPE = 0;
    Obj.sMDFWDM = $("#HF_YQFWDM").val() || " ";
    Obj.fCKJE = $("#TB_YHQCZJE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iCXID = $("#HF_CXID").val();
    Obj.dJSRQ = $("#TB_JSSJ").val();
    Obj.iYHQID = $("#HF_YHQ").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.fTYCKJE = $("#TB_YHQCZJE").val();
    return Obj;
};

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenu("menuContentHYKTYPE");
}

function check() {
    if (isNaN($("#TB_YHQCZJE").val())) {
        art.dialog({ lock: true, time: 2, content: "请输入数字" });
    }
}


function setUploadParam() {
    var colModels = "sHYKNO|fCKJE";
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
            GetHYXXNew(arr[i].sHYKNO, arr[i].fCKJE);
        }
    }
}

function GetHYXXNew(HYKNO, TZJF) {
    if (HYKNO != "") {
        //根据卡号查询信息
        var str = GetHYXXData(0, HYKNO);
        if (str != "null" && str != "") {
            var Obj = JSON.parse(str);
            if (CheckReapet(Obj)) {
                $("#list").addRowData($("#list").getGridParam("reccount"), {
                    sHYK_NO: Obj.sHYK_NO,
                    iHYKTYPE: Obj.iHYKTYPE,
                    iHYID: Obj.iHYID,
                    sHY_NAME: Obj.sHY_NAME,
                    fCKJE: TZJF,
                });
            }
        }
    }
}

function MoseDialogCustomerReturn(dialogName, lst) {
    switch (dialogName) {
        case "ListYHQ":
            $("#HF_FS_YQMDFW").val(lst[0].iFS_YQMDFW);
            $("#TB_YQFWMC").val("");
            $("#HF_YQFWDM").val("");
            $("#zHF_YQFWDM").val("");
            $("#HF_CXID").val("");
            $("#TB_CXHD").val("");
            $("#zHF_CXID").val("");
            break;
    }
}