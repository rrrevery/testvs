
vUrl = "../YHQGL.ashx";


function InitGrid() {
    vColumnNames = ["门店名称", "优惠券名称", "商品分类名称", "用券金额", "发券金额", "参与返券销售金额", ];
    vColumnModel = [

            { name: 'sMDMC', width: 80, },
            { name: 'sYHQMC', width: 80, },
			{ name: 'sSPFLMC', },
            { name: 'fYQJE', width: 80, },
            { name: 'fFQJE', width: 80, },
            { name: 'fZXFJE', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID");
    })

    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
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

    $("#TB_SPFL").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请选择商户！", 3);
            return;
        }
        else {
            var condData = new Object();
            condData["sSHDM"] = $("#HF_SHDM").val();
            SelectSHSPFL("TB_SPFL", "HF_SPFLID", "zHF_SPFLID", true, condData);
        }
    });

    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    });
    $("#TB_CXZT").click(function () {
        SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", false);
    });

    $("#TB_SBMC").click(function () {
        SelectSPSB("TB_SBMC", "HF_SBID", "zHF_SBID", false);
    });



});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    // MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_SHBMDM", "sBMDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_SPFLID", "iSHSPFLID", "in", false);
    MakeSrchCondition(arrayObj, "TB_SPDM", "sSPDM", "=", true);
    MakeSrchCondition(arrayObj, "HF_SBID", "iSBID", "in", false);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "HF_CXID", "iCXID", "in", false);
    MakeSrchCondition(arrayObj, "TB_TJRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_TJRQ2", "dRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

function SelectSHSPFL(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeSHSPFL", hideField, showField, hideData, dialogUrl, "商品分类", mult, "shortName", "actid", condData);
}