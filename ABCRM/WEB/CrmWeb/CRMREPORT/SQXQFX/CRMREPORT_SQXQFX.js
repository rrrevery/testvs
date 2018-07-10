vUrl = "../CRMREPORT.ashx";


function InitGrid() {
    vColumnNames = ["商圈名称", "门店名称", "小区名称", "会员数量", "小区户数", "渗透率(%)"];
    vColumnModel = [
            { name: 'sMDMC', width: 80 },
            { name: 'sSQMC', width: 150 },
            { name: 'sXQMC', width: 80 },
            { name: 'iHYSL', width: 80 },
            { name: 'iXQHS', width: 80 },
            { name: 'fSTL', width: 80 },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false)
    })
    $("#TB_SQMC").click(function () {
        SelectSQ("TB_SQMC", "HF_SQID", "zHF_SQID", false)
    })
    $("#TB_XQMC").click(function () {
        SelectLQXQ("TB_XQMC", "HF_XQID", "zHF_XQID", false)
    })

    ZSel_MoreCondition_Load(v_ZSel_rownum);
});


function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}


function MakeSearchCondition() {
    var arrayObj = new Array();
    var iBJ_NOSQ = $("#CB_WSQXQ").prop("checked") == true ? 1 : 0;
    if (iBJ_NOSQ == false) {
        MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
        MakeSrchCondition(arrayObj, "HF_SQID", "iSQID", "in", false);
    }
    MakeSrchCondition(arrayObj, "HF_XQID", "iXQID", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}


function CustomerAddOtherSearchCondition(conditionStr) {
    var iBJ_NOSQ = $("#CB_WSQXQ").prop("checked") == true ? 1 : 0;
    if (conditionStr != "") {
        conditionStr += "','iBJ_NOSQ':'" + iBJ_NOSQ;
    }
    else {
        conditionStr = "{'iBJ_NOSQ':'" + iBJ_NOSQ + "";
    }
    return conditionStr;
}