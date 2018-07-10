vUrl = "../LPGL.ashx";
vCaption = "礼品领取单";

function InitGrid() {
    vColumnNames = ['记录编号', '操作门店', '拨出地点', 'BGDDDM', '拨入地点', 'BGDDDM_BR', '总数量', '总金额', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },
               { name: 'sMDMC', width: 60, hidden: true },
               { name: 'sBGDDMC_BC', width: 60, },
               { name: 'sBGDDDM_BC', hidden: true },
               { name: 'sBGDDMC_BR', width: 60, },
               { name: 'sBGDDDM_BR', hidden: true },
               { name: 'fZSL', width: 60, },
               { name: 'fZJE', width: 60, },
               { name: 'iDJR', hidden: true, },
               { name: 'sDJRMC', width: 80, },
               { name: 'dDJSJ', width: 120, },
               { name: 'iZXR', hidden: true, },
               { name: 'sZXRMC', width: 80, },
               { name: 'dZXRQ', width: 120, },
    ];
}

$(document).ready(function () {
    BFButtonClick("TB_BGDDMC", function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    });
    BFButtonClick("TB_BGDDMC_BC", function () {
        SelectBGDD("TB_BGDDMC_BC", "HF_BGDDDM_BC", "zHF_BGDDDM_BC");
    });
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM_BC", "sBGDDDM_BC", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM_BR", "in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in",false );
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};


