vUrl = "../HYXF.ashx";
vCaption = "积分汇总查询";
function InitGrid() {
    vColumnNames = ["商户代码", "商户名称", "iMDID", '门店代码', "门店名称", "iSHBMID", "部门代码", "部门名称", "供应商编码", "供应商名称", "积分"];
    vColumnModel = [
            { name: 'sSHDM', width: 80, },
			{ name: 'sSHMC', width: 80, },
			{ name: 'iMDID', hidden: true },
            { name: 'sMDDM', width: 100, },
			{ name: 'sMDMC', width: 80, },
            { name: 'iSHBMID', hidden: true },
            { name: 'sBMDM', width: 150, },
			{ name: 'sBMMC', width: 150, },
            { name: 'sGHSDM', width: 150, },
            { name: 'sGHSMC', width: 150, },
            { name: 'fJF', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    RefreshButtonSep();
    $("#TB_MDMC").click(function () {
        if ($("#HF_SHDM").val() == "" || $("#HF_SHDM").val() == null) {
            ShowMessage("请先选择商户");
        }
        else {
            SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#HF_SHDM").val());
        }
    });
    $("#TB_GHSMC").click(function () {
        SelectHTGHS("TB_GHSMC", "HF_GHSDM", "zHF_GHSDM", false);
    });

    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", false);
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

});

//function IsValidSearch() {
//    if ($("#HF_SHDM").val() == "" || $("#HF_SHDM").val() == null) {
//        art.dialog({ lock: true, content: "请选择商户！" });
//        return false;
//    }
//    return true;
//}


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
    if ($("#HF_SHBMDM").val() != "") {
        MakeSrchCondition2(arrayObj, $("#HF_SHBMDM").val(), "sBMDM", "like", true);
    }
    MakeSrchCondition(arrayObj, "HF_GHSDM", "sGHSDM", "=", true);
    return arrayObj;
}


