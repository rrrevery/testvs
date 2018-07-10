vUrl = "../CRMGL.ashx";
vCaption = "商圈类型定义";

function InitGrid() {
    vColumnNames = ['类型编号', '类型名称', '备注'];
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 150, },
		{ name: 'sLXMC', width: 150, },
	    { name: 'sBZ', width: 150, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    $("#HF_LXMC").val("%" + $("#TB_LXMC").val() + "%");
    MakeSrchCondition(arrayObj, "HF_LXMC", "sLXMC", "like", true);
    $("#HF_BZ").val("%" + $("#TB_BZ").val() + "%");
    MakeSrchCondition(arrayObj, "HF_BZ", "sBZ", "like", true);
    return arrayObj;
};