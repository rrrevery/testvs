vUrl = "../LPGL.ashx";
vCaption = "积分返还礼品库存查询";

function InitGrid() {
    vColumnNames = ['礼品代码', '礼品名称', '保管地点', '礼品单价', '礼品进价', '礼品积分', '礼品数量', '礼品规格', '国际编码'];
    vColumnModel = [
            { name: 'sLPDM', },
            { name: 'sLPMC', },
            { name: 'sBGDDMC', },
            { name: 'fLPDJ', },
            { name: 'fLPJJ', },
            { name: 'fLPJF', },
            { name: 'fKCSL', },
            { name: 'sLPGG', },
            { name: 'sGJBM', },
    ];
}

$(document).ready(function () {    
    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM",false);
    });

    $("#B_Insert").hide();
    $("#B_Update").hide();
    RefreshButtonSep();
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LPDM", "sLPDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_LPMC", "sLPMC", "like", true);
    MakeSrchCondition(arrayObj, "TB_LPDJ", "fLPDJ", "=", false);
    MakeSrchCondition(arrayObj, "TB_LPJJ", "fLPJJ", "=", false);
    MakeSrchCondition(arrayObj, "TB_LPJF", "fLPJF", "=", false);
    MakeSrchCondition(arrayObj, "TB_LPGG", "sLPGG", "=", true);
    MakeSrchCondition(arrayObj, "TB_LPGJM", "sGJBH", "=", true);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "TB_LPGJM", "sGJBM", "=", true);
    return arrayObj;
}







