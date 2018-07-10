vUrl = "../YHQGL.ashx";
vCaption = "优惠券使用规则";
function InitGrid() {
    vColumnNames = ['规则编号', '使用规则名称', '停用标记', '用券上限', '门店'];// '消费金额', '优惠券金额',
    vColumnModel = [
        { name: 'iJLBH', index: 'iYHQSYGZID', width: 100 },
        { name: 'sYHQSYGZMC' },
        { name: 'iBJ_TY', width: 100, formatter: "checkbox" },
        //{ name: 'fYQBL_XFJE', width: 100 },
        //{ name: 'fYQBL_YHQJE', width: 100 },
        { name: 'fZDYQJE' },
        { name: 'sMDMC', width: 120 },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sYHQSYGZMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_XFJE", "fYQBL_XFJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_YQSX", "fZDYQJE", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    return arrayObj;
};