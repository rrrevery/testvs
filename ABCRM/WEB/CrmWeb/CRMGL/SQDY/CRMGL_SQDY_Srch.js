vUrl = "../CRMGL.ashx";
vCaption = "商圈定义";
function InitGrid() {
    vColumnNames = ['商圈编号', '商圈名称', '商圈描述', 'iMDID', '门店名称'];
    vColumnModel = [
            { name: 'iSQID', width: 150, },
			{ name: 'sSQMC', width: 150, },
            { name: 'sSQMS', width: 150, },
            { name: 'iMDID', hidden: true, },
			{ name: 'sMDMC', width: 150, },
    ];
};
$(document).ready(function () {
    $("#B_Exec").hide();
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    })

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_SQMC", "sSQMC", "like", true);
    return arrayObj;
}

