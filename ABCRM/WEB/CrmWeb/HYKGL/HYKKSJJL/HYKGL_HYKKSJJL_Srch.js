vUrl = "../HYKGL.ashx";

vCaption = "会员可升级记录查询";

function InitGrid() {
    vColumnNames = ['卡号', "会员姓名", "身份证", "iSEX", "性别", "门店名称", "门店代码", "出生日期", "原卡类型", "新卡类型", "升级积分", "可升级日期", "规则编号", "规则序号", "有效期", "邮箱地址", "iCANSMS", "接受短信", "通讯地址", "电话号码", "手机号码", "客户经理", "登记人", "登记时间", "更新人", "更新时间"];
    vColumnModel = [
          { name: "sHYKNO", width: 100 },
                    { name: "sHYNAME", width: 100 },
                    {
                        name: "sSFZBH", width: 140, formatter: function (cellvalue, cell) {
                            return cellvalue.substring(0, cellvalue.length - 4) + "****";
                        }
                    },
                    { name: "iSEX", hidden: true, },
                    { name: "sSEX", width: 50 },
                    { name: "sMDMC", width: 100 },
                    { name: "sMDDM", width: 100 },
                    { name: "dCSRQ", width: 100 },
                    { name: "sHYKNAME", width: 100 },
                    { name: "sHYKNAME_NEW", width: 100 },
                    { name: "fBQJF", width: 100 },
                    { name: "dSJRQ", width: 100 },
                    { name: "iGZID", width: 100 },
                    { name: "iINX", width: 100 },
                    { name: "dYXQ", width: 100 },
                    { name: "sE_MAIL", width: 100 },
                    { name: "iCANSMS", width: 50, hidden: true, },
                    { name: "sCANSMS", width: 50, },
                    { name: "sTXDZ", width: 100 },
                    { name: "sPHONE", width: 100 },
                    { name: "sSJHM", width: 100 },
                    { name: "sPERSON_NAME", width: 100 },
                    { name: "sDJRMC", width: 100 },
                    { name: "dDJSJ", width: 100 },
                    { name: "sGXRMC", width: 100 },
                    { name: "dGXSJ", width: 100 }
    ];
}




$(document).ready(function () {
    SetControlState();
    //卡类型弹出框 
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });
    $("input[type='checkbox']").click(function () {
        var ele = $(this);
        var name = ele.attr("name");
        ele.prop("checked", this.checked);
        if (this.checked) {
            ele.siblings("[name='" + name + "']").
            prop("checked", !this.checked);
        }
        var hf = "#" + name.replace("CB", "HF");
        $(hf).val(this.checked ? ele.val() : "");
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    //卡类型弹出框 
    $("#TB_HYKNAME_NEW").click(function () {
        SelectKLX("TB_HYKNAME_NEW", "HF_HYKTYPE_NEW", "zHF_HYKTYPE_NEW", false);
    });
    CheckBox("CB_KY", "HF_KY");
    RefreshButtonSep();
});

function SetControlState() {
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();

}


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE_NEW", "iHYKTYPE_NEW", "in", false);
    var mdid = $("#HF_MDID").val().split(',');
    var exist_zb = false;
    if (mdid.length > 0) {
        for (var i = 0; i < mdid.length; i++) {
            if (mdid[i] == 0)
                exist_zb = true;
        }
    }
    if (exist_zb == true && $("#HF_MDID").val() != "") {
        MakeSrchCondition2(arrayObj, '' + $("#HF_MDID").val() + ') or X.MDID is null ', "iMDID", "in", false);
    }
    else {
        MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    }
    var status = $("#HF_KY").val();
    if (status == "1")
        MakeSrchCondition2(arrayObj, 1 + ' or X.BJ_KY is null', "iBJ_KY", " != ", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO1", "sHYKNO", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO2", "sHYKNO", "<=", true);
    MakeSrchCondition(arrayObj, "TB_BQJF1", "fBQJF", ">=", false);
    MakeSrchCondition(arrayObj, "TB_BQJF2", "fBQJF", "<=", false);
    MakeSrchCondition(arrayObj, "TB_SJRQ1", "dSJRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_SJRQ2", "dSJRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ1", "dYXQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ2", "dYXQ", "<=", true);
    return arrayObj;
};

function CheckBox(cbname, hfname) {
    $("input[type='checkbox'][name='" + cbname + "']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#" + hfname).val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#" + hfname).val("");
        }
    });

}