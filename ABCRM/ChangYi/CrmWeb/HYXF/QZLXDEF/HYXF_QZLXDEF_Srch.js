vUrl = "../HYXF.ashx";
vCaption="圈子类型定义";

function InitGrid() {
	vColumnNames = ['记录编号', '圈子类型名称', '圈子成员人数', '状态', '登记时间'];
	vColumnModel = [
			{ name: 'iJLBH', hidden: false },
			{ name: 'sQZLXMC', },//sortable默认为true width默认150
			{ name: 'iQZCYRS', },
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
			{ name: 'dDJSJ', },
	];
};


$(document).ready(function () {





});


function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
	MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", "=", true);
	MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "=", true);
	MakeSrchCondition(arrayObj, "TB_QZLXMC", "sQZLXMC", "in", true);
	MakeMoreSrchCondition(arrayObj);
	return arrayObj;
};


