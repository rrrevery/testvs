vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ['MDID', '门店名称'];
    vColumnModel = [
             { name: 'iMDID', hidden: true, },
             { name: 'sMDMC', width: 100, },
    ];
}


function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();
}

function IsValidData() {
    if ($("#TB_HDMC").val() == "") {
        ShowMessage("请输入活动名称！");
        return false;
    }
    if ($("#TB_INX").val() == "") {
        ShowMessage("请输入活动顺序号！");
        return false;
    }
    if (!IsNumber($("#TB_INX").val())) {
        ShowMessage("活动顺序号必须为整形！");
        return false;
    }
    if ($("#TB_INX").val().length >10) {
        ShowMessage("活动顺序号不能超过10位数字！");
        return false;
    }
    if ($("#TB_HDSJ").val() == "") {
        ShowMessage("请输入活动时间！");
        return false;
    }
    if ($("#TB_HDJJ").val() == "") {
        ShowMessage("请输入活动简介！");
        return false;
    }
    if ($("#TB_IMG").val() == "") {
        ShowMessage("请选择活动图片！");
        return false;
    }
    if ($("#TB_KSRQ").val() == "") {
        ShowMessage("请选择开始日期！");
        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请选择结束日期！");
        return false;
    }

    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        art.dialog({ lock: true, time: 2, content: "开始日期不得大于结束日期" });
        return false;
    }
    return true;
}



$(document).ready(function () {


    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/ZXHD");


    $("#md_Add").click(function () {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?iWXPID=" + iWXPID;
        vDialogName = "ListWXMD";
        var DataObject = new Object();
        OpenDialog(dialogUrl, "list", DataObject, vDialogName, "iMDID", false);
    });

    $("#md_Del").click(function () {
        DeleteRows("list");
    });

})

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sACTNAME = $("#TB_HDMC").val();
    Obj.iINX = $("#TB_INX").val();
    Obj.sACTIME = $("#TB_HDSJ").val();
    Obj.sIMG = $("#TB_IMG").val();
    Obj.sDESCRIBE = $("#TB_HDJJ").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iTY = $("#CB_BJ_TY")[0].checked ? "1" : "0";

    var lst1 = new Array();
    lst1 = $("#list").datagrid("getRows");
    Obj.itemTable = lst1;
    Obj.sHDNR = editor.html();
    return Obj;
}


function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HDMC").val(Obj.sACTNAME);
    $("#TB_INX").val(Obj.iINX);
    $("#TB_HDSJ").val(Obj.sACTIME);
    $("#TB_IMG").val(Obj.sIMG);
    $("#TB_HDJJ").val(Obj.sDESCRIBE);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#CB_BJ_TY").prop("checked", Obj.iTY == "1" ? true : false);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    editor.html(decodeURIComponent(Obj.sHDNR));
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)


}





