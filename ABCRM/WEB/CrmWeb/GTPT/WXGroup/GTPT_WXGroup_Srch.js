vUrl = "../GTPT.ashx";
vCaption = "微信分组定义";
function InitGrid() {
	vColumnNames = ['分组编号', '分组名称', '备注', '状态'],
	vColumnModel = [
			{ name: 'iJLBH', index: 'iJLBH', width: 80, },
			{ name: 'sGROUP_NAME', width: 120, },
			{ name: 'sZY', width: 120 },
			{
				name: 'iSTATUS', width: 80, formatter: function (cellvalue, icol) {
					switch (cellvalue) {
						case 0:
							return "有效";
							break;
						case -1:
							return "无效";
							break;
					}
				}
			},
	]

}



$(document).ready(function () {

	$("input[type='checkbox'][name='CB_STATUS']").click(function () {
		if (this.checked) {
			$(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
			$("#HF_STATUS").val($(this).val());
		}
		else {
			$(this).prop("checked", this.checked);
			$("#HF_STATUS").val("");
		}
	});
});

function SaveData() {
	var Obj = new Object();
	var rowid = $("#list").jqGrid("getGridParam", "selrow");
	var rowData = $("#list").getRowData(rowid);
	Obj.iJLBH = rowData.iJLBH;
	Obj.iLoginRYID = iDJR;
	Obj.sLoginRYMC = sDJRMC;
	return Obj;
}

function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
	MakeSrchCondition(arrayObj, "TB_GROUPID", "iGROUPID", "=", false);
	MakeSrchCondition(arrayObj, "TB_GROUP_NAME", "sGROUP_NAME", "=", true);
	MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
	MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "in", false);
	MakeMoreSrchCondition(arrayObj);
	return arrayObj;
};
