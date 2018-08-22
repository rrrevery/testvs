
vUrl = "../YHQGL.ashx";
vCaption = "按合同汇总查询收发券记录";

function InitGrid() {
    vColumnNames = ["门店名称", "优惠券名称", "合同号", "供货商名称", "用券金额", "发券金额", "参与返券消费金额", ];
    vColumnModel = [
            { name: 'sMDMC', width: 80, },
            { name: 'sYHQMC', width: 80, },
            { name: 'sHTH', width: 50 },
			{ name: 'sGHSMC', },
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


    $("#TB_XSBMMC").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请选择商户！", 3);
            return;
        }
        else {
            var condData = new Object();
            condData["sSHDM"] = $("#HF_SHDM").val();
            SelectSHBM("TB_XSBMMC", "HF_XSBMDM", "zHF_XSBMDM", true, condData);
        }
    });

    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    });
    $("#TB_CXZT").click(function () {
        SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", false);
    });

    $("#TB_GHSMC").click(function () {
        SelectGHS("TB_GHSMC", "HF_GHSID", "zHF_GHSID", false);
    });
    RefreshButtonSep();

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_XSBMDM", "sBMDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "HF_CXID", "iCXID", "in", false);
    MakeSrchCondition(arrayObj, "TB_HTH", "sHTH", "=", true);
    MakeSrchCondition(arrayObj, "TB_TJRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_TJRQ2", "dRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

