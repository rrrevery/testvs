vUrl = "../HYXF.ashx";
vCaption = "CRM退货用钱买积分操作";

function InitGrid() {
    vColumnNames = ['记录编号', '操作地点', '卡类型', ' 卡号', 'iMDID', '门店名称', '收款台', '未处理积分',
                       '原年积分', '原优惠券金额', '原储值卡金额', '退货积分',
                       '应买积分', '实买积分', '应付金额', '实付金额', '现金',
                       '登记人', 'iDJR', '登记时间', '审核人', 'iZXR', '审核日期'];
    vColumnModel = [
               { name: 'iJLBH', },
               { name: 'sBGDDDM', width: 80, },
               { name: 'sHYKNAME', width: 80, },
               { name: 'sHYKNO', width: 60, },
               { name: 'iMDID', width: 80, hidden: true },
               { name: 'sMDMC', width: 80, },
               { name: 'sSKTNO', width: 60, },
               { name: 'fYWCLJF', },
               { name: 'fYNLJJF', },
               { name: 'fYYHQJE', },
               { name: 'fYCZKYE', },
               { name: 'fTHJF', },
               { name: 'fYMJJF', },
               { name: 'fSMJJF', },
               { name: 'fYFJE_J', },
               { name: 'fSFJE_J', },
               { name: 'fXJ', hidden:true},
               { name: 'sDJRMC', width: 80, },
               { name: 'iDJR', width: 80, hidden: true },
               { name: 'dDJSJ', width: 120, },
               { name: 'sZXRMC', width: 80, },
               { name: 'iZXR', width: 80, hidden: true },
               { name: 'dZXRQ', width: 120, },
    ];
};




$(document).ready(function () {
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_BGDDDM").click(function () {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM", false);
    });

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });



});




function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYKNO", "=", false);
    MakeSrchCondition(arrayObj, "HF_CCDDDM", "sBGDDDM", "=", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_YWCLJF", "fYWCLJF", "=", false);
    MakeSrchCondition(arrayObj, "TB_YNLJF", "fYNLJF", "=", false);
    MakeSrchCondition(arrayObj, "TB_YYHQJE", "fYYHQJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_YCZKJE", "fYCZKJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_THJF", "fTHJF", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_SKTNO", "sSKTNO", "=", true);
    MakeSrchCondition(arrayObj, "TB_YMJJF", "fYMJJF", "=", false);
    MakeSrchCondition(arrayObj, "TB_SMJJF", "fSMJJF", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXSJ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXSJ1", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};



