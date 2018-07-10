vUrl = "../GTPT.ashx";
vCaption = "商品推荐定义";
function InitGrid() {
    vColumnNames = ['记录编号', '门店名称', '商户名称', 'SHDM', '商品名称', 'SPDM'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },
            { name: 'sMDMC', width: 120, },
            { name: 'sSHMC', width: 120, },
            { name: 'sSHDM', hidden: true },
            { name: 'sSPMC', width: 120, },
            { name: 'sSPDM', hidden: true },
    ];
};

$(document).ready(function () {
    BFButtonClick("TB_SHMC", function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM",false);
    });
    BFButtonClick("TB_MDMC", function () {
        SelectWXMD("TB_MDMC", "HF_MDID", "zHF_MDID",false);
    });
    BFButtonClick("TB_SPMC", function () {
        SelectSHSP("TB_SPMC", "HF_SPDM", "zHF_SPDM",false);
    });
    
});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_SPDM", "sSPDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
  
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}

function SelectSHSP(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListSHSP/CrmArt_ListSHSP.aspx";
    MoseDialogModel("ListSHSP", hideField, showField, hideData, dialogUrl, "商品信息", mult, "Name", "Code", condData);
}




