vUrl = "../HYXF.ashx";
vCaption = "积分转储单";
function InitGrid() {
    vColumnNames = ['记录编号', '会员卡号', '转入会员姓名', 'HYID', '转入积分', '保管地点', 'sBGDDDM', '摘要', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期'];
    vColumnModel = [
               { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
               { name: 'sHYKNO', width: 60, },
               { name: 'sHY_NAME', width: 120, },
               { name: 'iHYID', hidden: true, },
               { name: 'fZRJF', width: 60, },
               { name: 'sBGDDMC', width: 60, },
               { name: 'sBGDDDM', hidden: true, },
               { name: 'sZY', width: 120, },
               { name: 'iDJR', hidden: true, },
               { name: 'sDJRMC', width: 80, },
               { name: 'dDJSJ', width: 120, },
               { name: 'iZXR', hidden: true, },
               { name: 'sZXRMC', width: 80, },
               { name: 'dZXRQ', width: 120, },
    ];
}

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    $("#TB_BGDDDM").click(function () {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM", false);
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "W.JLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO_ZR", "W.HYKNO_ZR", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYNAME", "H.HY_NAME", "like", true);
    MakeSrchCondition(arrayObj, "TB_BGDDDM", "B.BGDDDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ", "H.YXQ", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
