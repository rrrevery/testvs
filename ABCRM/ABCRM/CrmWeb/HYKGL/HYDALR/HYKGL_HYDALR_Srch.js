vUrl = "../HYKGL.ashx";
vCaption = "会员档案录入";

function InitGrid() {
    vColumnNames = ['顾客ID', '姓名', 'iSEX', '性别', "手机号码", '身份证号', "出生日期", 'qyid', "通讯地址", "电子邮件", "工作单位", "备注", "职位", '固定电话', "传真号码", "办公电话", "ZJLXID", '证件类型', '小区名称', 'DJR', '登记人', '登记时间', 'GXR', '更新人', '更新日期', '摘要', ];
    vColumnModel = [
    { name: 'iJLBH', index: 'iJLBH', width: 80, },
    { name: 'sHY_NAME', width: 80, },
     { name: "iSEX", hidden: true, },
     { name: "sSEX", width: 50, },
    { name: 'sSJHM', },
    {
        name: 'sSFZBH', formatter: function (cellvalue, cell) {
            return MakePrivateNumber(cellvalue);
        }
    },
    { name: 'dCSRQ', width: 80, },
    { name: 'iQYID', hidden: true, },
    { name: 'sTXDZ', width: 80, },
    { name: 'sEMAIL', width: 80, },
    { name: 'sGZDW', width: 80, },
    { name: 'sBZ', width: 80, },
    { name: 'sZW', width: 80, },
    { name: 'sPHONE', width: 80, },
    { name: 'sFAX', width: 80, },
    { name: 'sBGDH', width: 80, },
    { name: 'iZJLXID', hidden: true },
    { name: 'sZJLXMC' },
    //{ name: 'sSQMC' },
    { name: 'sXQMC' },
    { name: 'iDJR', hidden: true, },
    { name: 'sDJRMC', width: 80, },
    { name: 'dDJSJ', width: 120, },
    { name: 'iGXR', hidden: true, },
    { name: 'sGXRMC', width: 80, },
    { name: 'dGXSJ', width: 120, },
    { name: 'sZY', width: 120, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });

    $("#TB_GXRMC").click(function () {
        SelectRYXX("TB_GXRMC", "HF_GXR", "zHF_GXR",false);
    });
    $("#TB_XQMC").click(function () {
        SelectXQ("TB_XQMC", "HF_XQID", "zHF_XQID", false);
    });
    $("#TB_SQMC").click(function () {
        SelectSQ("TB_SQMC", "HF_SQID", "zHF_SQID", false);
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_GKID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GKNAME" , "sHY_NAME", "like", true);
    MakeSrchCondition(arrayObj, "S_SEX", "iSEX", "=", false);
    MakeSrchCondition(arrayObj, "TB_SJHM", "sSJHM", "=", true);
    MakeSrchCondition(arrayObj, "TB_SFZBH", "sSFZBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_GXRMC", "sGXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_GXSJ1", "dGXSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_GXSJ2", "dGXSJ", "<=", true);
    return arrayObj;
}

function AddCustomerCondition(Obj) {
    Obj.sHYK_NO = $("#TB_HYKNO").val();
};

function WUC_Return_SQ(MC, ID, zID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + MC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
            }
            $("#" + MC).val(tp_mc);
            $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zID).val(jsonString);

        }
    }
}

function WUC_ReturnLQXQ(MC, ID, zID, SQID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var tp_sqmc = "";
            var tp_SQID = "";
            $("#" + MC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_sqmc += contractValues[i].FreeField + ";";
                tp_hf += contractValues[i].Id + ",";
                tp_SQID += contractValues[i].Id1 + ",";
            }
            $("#" + MC).val(tp_mc);
            $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zID).val(jsonString);
        }
    }
}
