vUrl = "../HYKGL.ashx";
vCaption = "会员推荐规则定义";
function InitGrid() {
    vColumnNames = ['礼品名称', '礼品ID', '礼品规格', '礼品积分', '礼品金额', ];
    vColumnModel = [
            { name: 'sLPMC', width: 100, },
            { name: 'iLPID', width: 100, },
            { name: 'sLPGG', width: 100, },
            { name: 'fLPJF', width: 100, },
            { name: 'fLPDJ', width: 100, },
    ];
};

function IsValidData() {


    if (GetSelectValue("DDL_TJFW") == "" || GetSelectValue("DDL_TJFW") == null) {
        ShowMessage("请选择推荐范围", 3);
        return false;
    }
    if ($("#TB_TJSL").val() == "") {
        ShowMessage("请输入推荐数量", 3);
        return false;
    }
    if (GetSelectValue("DDL_HJFS") == "" || GetSelectValue("DDL_HJFS") == null) {
        ShowMessage("请选择获奖方式", 3);
        return false;
    }
    if (GetSelectValue("DDL_GZLX") == "" || GetSelectValue("DDL_GZLX") == null) {
        ShowMessage("请选择规则方式", 3);
        return false;
    }
    if (GetSelectValue("DDL_JLFS") == "" || GetSelectValue("DDL_JLFS") == null) {
        ShowMessage("请选择奖励方式", 3);
        return false;
    }

    switch (parseInt(GetSelectValue("DDL_JLFS"))) {
        case 1:
            if ($("#TB_JF").val() == "" || $("#TB_JF").val() == "0") {
                ShowMessage("请输入奖励积分", 3);
                return false;
            }
            break;
        case 2:
            if ($("#HF_YHQID").val() == "" || $("#HF_YHQID").val() == "-1") {
                ShowMessage("请选择优惠券", 3);
                return false;
            }
            if (GetSelectValue("DDL_JLGZ") == "" || GetSelectValue("DDL_JLGZ") == null) {
                ShowMessage("请选择优惠券奖励规则", 3);
                return false;
            }
            break;
    }

    if ($("#HF_CXID").val() == "") {
        ShowMessage("请选择促销活动", 3);
        return false;
    }
    return true;
}

$(document).ready(function () {
    $("#btnout_B_Start").show();
    $("#btnout_B_Stop").show();
    $("#B_Start").show();
    $("#B_Stop").show();
    $("#QDR").show();
    $("#QDSJ").show();
    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", true)
    });

    $("#DDL_JLFS").change(function () {
        switch (parseInt($("#DDL_JLFS").val())) {
            case 0:
                $("#DV_LP").show();
                $("#DV_YHQ").hide();
                $("#DV_JF").hide();
                $('#list').datagrid('loadData', [], "json");
                $('#list').datagrid("loaded");
                break;
            case 1:
                $("#DV_LP").hide();
                $("#DV_YHQ").hide();
                $("#DV_JF").show();
                bNeedItemData = false;
                break;
            case 2:
                $("#DV_LP").hide();
                $("#DV_YHQ").show();
                $("#DV_JF").hide();
                bNeedItemData = false;
                break;
        }
    })


    $("#TB_CXHD").click(function () {
        var condData = new Object();
        SelectCXHD("TB_CXHD", "HF_CXID", "zHF_CXID", true, condData);
    });
    $("#AddItem").click(function () {
        SelectLP("list", "", "iLPID");

    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });
})

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);//input控件被换成了label...   
    $("#TB_CXHD").val(Obj.sCXZT);
    $("#HF_CXID").val(Obj.iCXID);
    $("#DDL_TJFW").val(Obj.iTJFW);
    $("#TB_TJSL").val(Obj.iTJSL);
    $("#DDL_HJFS").val(Obj.iHJFS);
    $("#DDL_GZLX").val(Obj.iGZLX);
    $("#DDL_JLFS").val(Obj.iJLFS);
    switch (Obj.iJLFS) {
        case 0:
            $("#DV_LP").show();
            $("#DV_YHQ").hide();
            $("#DV_JF").hide();
            break;
        case 1:
            $("#DV_LP").hide();
            $("#DV_YHQ").hide();
            $("#DV_JF").show();
            bNeedItemData = false;
            break;
        case 2:
            $("#DV_LP").hide();
            $("#DV_YHQ").show();
            $("#DV_JF").hide();
            bNeedItemData = false;
            break;
    }

    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("#DDL_JLGZ").val(Obj.iJLGZ);
    $("#TB_JF").val(Obj.fJF);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);

};

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sCXZT = $("#TB_CXHD").val();
    Obj.iCXID = $("#HF_CXID").val();
    Obj.iTJFW = GetSelectValue("DDL_TJFW"); // $("#DDL_TJFW").val();
    Obj.iTJSL = $("#TB_TJSL").val();
    Obj.iHJFS = GetSelectValue("DDL_HJFS");// $("#DDL_HJFS").val();
    Obj.iGZLX = GetSelectValue("DDL_GZLX");//$("#DDL_GZLX").val();
    Obj.iJLFS = GetSelectValue("DDL_JLFS");// $("#DDL_JLFS").val();
    switch (parseInt(Obj.iJLFS)) {
        case 0:
            var lst = new Array();
            lst = $("#list").datagrid("getData").rows;
            Obj.itemTable = lst;
            break;
        case 1:
            Obj.fJF = $("#TB_JF").val();
            break;
        case 2:
            Obj.sYHQMC = $("#TB_YHQMC").val();
            Obj.iYHQID = $("#HF_YHQID").val();
            Obj.iJLGZ = GetSelectValue("DDL_JLGZ");// $("#DDL_JLGZ").val();
            break;
    }
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
};