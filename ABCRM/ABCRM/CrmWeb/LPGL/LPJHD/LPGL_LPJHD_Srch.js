vUrl = "../LPGL.ashx";
vCaption = "礼品" + (DJLX == 0 ? "进货单" : "退货单");

function InitGrid() {
    vColumnNames = ['记录编号', '操作门店', '供应商', 'GYSDM', '礼品地点', 'BGDDDM', '总数量', '总金额', '进价金额', '摘要', '登记人', '登记人名称', '登记时间', '审核人', '审核人名称', '审核日期'];
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
               { name: 'sMDMC', width: 60, hidden: true },
               { name: 'sGHSMC', width: 80 },
               { name: 'iGHSID', hidden: true },
               { name: 'sBGDDMC', width: 60, },
               { name: 'sBGDDDM', hidden: true },
               { name: 'fZSL', width: 60, },
               { name: 'fZJE', width: 60, },
               { name: 'fJJJE', width: 60, },
               { name: 'sZY', width: 120, },
               { name: 'iDJR', hidden: true, },
               { name: 'sDJRMC', width: 80, },
               { name: 'dDJSJ', width: 120, },
               { name: 'iZXR', hidden: true, },
               { name: 'sZXRMC', width: 80, },
               { name: 'dZXRQ', width: 120, },
    ];
};

$(document).ready(function () {
    ConbinDataArry["iDJLX"] = DJLX;
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    BFButtonClick("TB_GHSMC", function () {
        SelectGHS("TB_GHSMC", "HF_GHSID", "zHF_GHSID", true);
    });

    BFButtonClick("TB_BGDDMC", function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    });

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_GHSID", "iGHSID", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "TB_JJJE", "fJJJE", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition2(arrayObj, DJLX, "iDJLX", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};