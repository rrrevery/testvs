vUrl = "../HYXF.ashx";

function InitGrid() {
    vColumnNames = ['序号', '积分下限', '积分上限', '比例'];
    vColumnModel = [
            { name: "iXH", width: 90,},
            { name: "fJFXX", width: 90, editor: 'text', },
            { name: "fJFSX", width: 90, editor: 'text', },
            { name: "fFLBL", width: 90, editor: 'text', },
    ];
};

$(document).ready(function () {
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", false);
    });
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE", 1);
    $("#DDL_SH").change(function () {
        $("#TB_MDMC").val("");
        $("#HF_MDID").val("");
        $("#zHF_MDID").val("");
    });
    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    });

    $("#B_Insert").click(function () {
        $("[name='BJ_CLFS'][value='2']").prop("checked", true);
        $("[name='BJ_JEJQFS'][value='0']").prop("checked", true);
        $("[name='BJ_YXQDW'][value='1']").prop("checked", true);
        $("[name='BJ_FQCSXZ'][value='0']").prop("checked", true);
        $("[name='BJ_CLJFLX'][value='1']").prop("checked", true);
        $("[name='BJ_MD'][value='0']").prop("checked", true);
    });

    $("#TB_MDMC").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请先选择商户", 3);
        }
        else {
            SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#HF_SHDM").val());
        }
    });

    $("#AddItem").click(function () {
        $("#list").datagrid("appendRow", {
            iXH:$("#list").datagrid("getRows").length+1,
        });
    })

    $("#DelItem").click(function () {
        DeleteRows("list");

    });

    vJLBH = GetUrlParam("jlbh");//$.getUrlParam("jlbh");
    if (vJLBH != "") {
        ShowDataBase(vJLBH);
    };
})

function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenu("menuContentHYKTYPE");
}




function SetControlState() {
    $("#B_Stop").show();
    $("#ZZR").show();
    $("#ZZSJ").show();
    if ($("#HF_ZXR").val() != "" && $("#HF_ZZR").val() == "0" && $("#HF_ZXR").val() != "0") {
        $("#B_Stop").prop("disabled", false);
    } else {
        $("#B_Stop").prop("disabled", true);
    }
}

function IsValidData() {

    if ($("#TB_GZMC").val() == "") {
        ShowMessage("请输入规则名称", 3);
        return false;
    }
    if ($("#TB_YHQJSRQ").val() == "" && $("#TB_YXQSL").val() == "") {
        ShowMessage("请输入优惠券结束日期或者有效时间", 3);
        return false;
    }
    if ($("#TB_YHQJSRQ").val() != "" && $("#TB_YXQSL").val() != "") {
        ShowMessage("优惠券结束日期或者有效时间只能输入一个", 3);
        return false;
    }
    if ($("#HF_HYKTYPE").val() == "") {
        ShowMessage("请选择卡类型", 3);
        return false;
    }

    if ($("#HF_SHDM").val() == "") {
        ShowMessage("请选择商户", 3);
        return false;
    }

    if ($("#TB_KSRQ").val() == "" || $("#TB_JSRQ").val() == "") {
        ShowMessage("请输入起止日期", 3);
        return false;
    }
    if ($("#HF_YHQID").val() == "") {
        ShowMessage("请选择优惠券", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sSHDM = $("#HF_SHDM").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.iHBFS = 1;
    Obj.iCLFS = ($("[name='BJ_CLFS']:checked").val() != undefined) ? $("[name='BJ_CLFS']:checked").val() : "-1";
    Obj.iCLRC = 0;
    Obj.iXSWS = ($("[name='BJ_JEJQFS']:checked").val() != undefined) ? $("[name='BJ_JEJQFS']:checked").val() : "-1";
    Obj.iBJ_MD = ($("[name='BJ_MD']:checked").val() != undefined) ? $("[name='BJ_MD']:checked").val() : "-1";
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.dYHQJSRQ = $("#TB_YHQJSRQ").val();
    Obj.iYHQSL = $("#TB_YXQSL").val() == "" ? 0 : $("#TB_YXQSL").val();
    Obj.iYHQDW = ($("[name='BJ_YXQDW']:checked").val() != undefined) ? $("[name='BJ_YXQDW']:checked").val() : "-1";
    Obj.iBJ_WCLJF = ($("[name='BJ_CLJFLX']:checked").val() != undefined) ? $("[name='BJ_CLJFLX']:checked").val() : "-1";
    Obj.iBJ_XZ = ($("[name='BJ_FQCSXZ']:checked").val() != undefined) ? $("[name='BJ_FQCSXZ']:checked").val() : "-1";
    Obj.iSTATUS = 0;
    Obj.iBJ_KCDYJF = 0;
    Obj.fZXDW = 0;
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sGZMC = $("#TB_GZMC").val();
    Obj.iBJ_CX = ($("[name='BJ_CX']:checked").val() != undefined) ? $("[name='BJ_CX']:checked").val() : "-1";

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
    $("#TB_SHMC").val(Obj.sSHMC);
    $("#HF_SHDM").val(Obj.sSHDM);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#DDL_HBFS").val(Obj.iHBFS);
    $("[name='BJ_CLFS'][value='" + Obj.iCLFS + "']").prop("checked", true);
    $("[name='BJ_JEJQFS'][value='" + Obj.iXSWS + "']").prop("checked", true);
    $("[name='BJ_YXQDW'][value='" + Obj.iYHQDW + "']").prop("checked", true);
    $("[name='BJ_FQCSXZ'][value='" + Obj.iBJ_XZ + "']").prop("checked", true);
    $("[name='BJ_CX'][value='" + Obj.iBJ_CX + "']").prop("checked", true);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#TB_YHQJSRQ").val(Obj.dYHQJSRQ);
    $("#TB_YXQSL").val(Obj.iYHQSL == 0 ? "" : Obj.iYHQSL);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_GZMC").val(Obj.sGZMC);
    $("[name='BJ_CLJFLX'][value='" + Obj.iBJ_WCLJF + "']").prop("checked", true);
    $("[name='BJ_MD'][value='" + Obj.iBJ_MD + "']").prop("checked", true);

    $("#list").datagrid("loadData", Obj.itemTable, 'json');
    $("#list").datagrid("loaded");
}
