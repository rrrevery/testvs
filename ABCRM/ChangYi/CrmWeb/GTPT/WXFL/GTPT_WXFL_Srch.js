vUrl = "../GTPT.ashx";
vCaption = "微信品牌分类";

function InitGrid() {
    vColumnNames = ['分类ID', '分类名称'],//, '序号'
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
		{ name: 'sFLMC', width: 120, },
    ]
    }
$(document).ready(function () {

})



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_FLID", "iFLID", "=", false);
    MakeSrchCondition(arrayObj, "TB_FLMC", "sFLMC", "like", true);
    MakeSrchCondition(arrayObj, "TB_INX", "iINX", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
