vUrl = "../HYXF.ashx";

function InitGrid() {
    vColumnNames = ["会员卡号", "卡类型", "会员姓名", '性别', '出生日期', '年龄', "身份证号", "消费门店", "会员折扣", "积分", "唯一商品码", "商品代码", "商品名称", "分类名称", "分类代码", "销售数量", "销售金额", "通讯地址", "邮政编码", "手机号码", "办公电话", "电子邮件", "区域名称", "部门代码", "部门名称", "交易时间"];
    vColumnModel = [
            { name: 'sHYKNO', width: 100 },
            { name: 'sHYKNAME', width: 80 },
            { name: 'sHY_NAME', width: 80, },
            { name: 'sSEX', width: 80, },
            { name: 'dCSRQ', width: 120, },
            { name: 'iAGE', width: 80, },
            { name: 'sSFZBH', width: 150, },
            { name: 'sMDMC', width: 120, },
			{ name: 'fZKJE_HY', width: 80, },
            { name: 'fJF', width: 80, },
            { name: 'iSHSPID', width: 80 },
            { name: 'sSPDM', width: 80, },
            { name: 'sSPMC', width: 150, },
            { name: 'sSPFLMC', width: 150, },
            { name: "sSPFLDM", width: 150, },
            { name: 'fXSSL', width: 80, },
            { name: 'fXSJE', width: 80, },
            { name: 'sTXDZ', width: 200, },
            { name: 'sYZBM', width: 200, hidden: true },
            { name: 'sPHONE', width: 120, },
            { name: 'sBGDH', width: 200, hidden: true },
            { name: 'sEmail', width: 120, },
            { name: 'sQYMC', width: 120, },
            { name: 'sBMDM', width: 200, hidden: true },
            { name: 'sBMMC', width: 200, },
			{ name: 'dJYSJ', width: 200, },

    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_ExportMember").show();

    // DrawView();

    //CYCBL_ADD_ITEM("CBL_HYBJ", GetHYXXXM(12));

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });

    $("#TB_SBMC").click(function () {
        SelectSPSB("TB_SBMC", "HF_SBID", "zHF_SBID", false, $("#HF_SHDM").val());
    });


    $("#TB_SPFL").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请选择商户！", 3);
            return;
        }
        else {
            var condData = new Object();
            condData["sSHDM"] = $("#HF_SHDM").val();
            SelectSHSPFL("TB_SPFL", "HF_SPFLDM", "zHF_SPFLDM", true, condData);
        }
    });
    $("#TB_SHBMMC").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请选择商户！", 3);
            return;
        }
        else {
            var condData = new Object();
            condData["sSHDM"] = $("#HF_SHDM").val();
            SelectSHBM("TB_SHBMMC", "HF_SHBMDM", "zHF_SHBMDM", true, condData);
        }
    });

    $("#TB_QYMC").click(function () {
        SelectQY("TB_QYMC", "HF_QYDM", "zHF_QYDM", true);
    });





    $("input[type='checkbox'][name='CB_SEX']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#HF_SEX").val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#HF_SEX").val("");
        }
        if ($.trim($(this).val()) == "") {
            $("#HF_SEX").val("");
        }
    });
    RefreshButtonSep();
});


function AddCustomerCondition(Obj) {
    if ($("#TB_AGE1").val() != "")
        Obj.iAGE1 = $("#TB_AGE1").val();
    if ($("#TB_AGE2").val() != "")
        Obj.iAGE2 = $("#TB_AGE2").val();
    Obj.sHYKNO_Begin = $("#TB_HYKNO1").val();
    Obj.sHYKNO_End = $("#TB_HYKNO2").val();


}
function MakeSearchCondition() {
    var arrayObj = new Array();
    var tp_yt = "";
    var tp_else = "";
    $("[type='checkbox'][name='CB_YT']").each(function () {
        if (this.checked == true) {
            if (parseFloat(this.value) != 5) {
                if (tp_yt == "") {
                    tp_yt += this.value;
                }
                else {
                    tp_yt += "," + this.value;
                }
            }
            else {
                tp_else = this.value;
            }
        }
    })
    if (tp_yt != "" && tp_else == "") {
        MakeSrchCondition2(arrayObj, tp_yt, "iBJ_YT", "in", false);
    }
    if (tp_yt == "" && tp_else != "") {
        MakeSrchCondition2(arrayObj, "null", "iBJ_YT", "is", false);
    }
    if (tp_yt != "" && tp_else != "") {
        MakeSrchCondition2(arrayObj, "in(" + tp_yt + ") or B.BJ_YT is null", "iBJ_YT", "", false);
    }


    MakeSrchCondition(arrayObj, "HF_HYBQ", "iXH", "in", false);
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    if ($("#HF_SPFLDM").val() != "") {
        MakeSrchCondition(arrayObj, "HF_SPFLDM", "sSPFLDM", "like", false);
    }
    MakeSrchCondition(arrayObj, "HF_QYDM", "sQYDM", "in", false);
    MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "HF_SEX", "iSEX", "=", false);
    MakeSrchCondition(arrayObj, "TB_JF1", "fJF", ">=", false);
    MakeSrchCondition(arrayObj, "TB_JF2", "fJF", "<=", false);
    MakeSrchCondition(arrayObj, "TB_XFJE1", "fXFJE", ">=", false);
    MakeSrchCondition(arrayObj, "TB_XFJE2", "fXFJE", "<=", false);
    MakeSrchCondition(arrayObj, "TB_TJRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_TJRQ2", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_SBID", "iSBID", "in", false);

    if ($("#HF_SHBMDM").val() != "") {
        MakeSrchCondition(arrayObj, "HF_SHBMDM", "sBMDM", "like", false);
    }
    MakeSrchCondition(arrayObj, "TB_SPMC", "sSPMC", "like", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};







function ReturnQX() {
    return bCanShowPublic;
}



function onHYZLXClick(e, treeId, treeNode) {
    $("#TB_HYZLXMC").val(treeNode.name);
    $("#HF_HYZLXID").val(treeNode.data);
    hideMenu("menuContentHYZLX");
};
