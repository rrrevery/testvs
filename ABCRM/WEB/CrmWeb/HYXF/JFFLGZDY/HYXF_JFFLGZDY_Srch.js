vUrl = "../HYXF.ashx";
vCaption = "积分返利规则";
function InitGrid() {
	vColumnNames = ['记录编号', '规则名称', '门店名称', '商户代码', '商户名称', '卡类型', '开始时间', '结束时间', '优惠券', '优惠券结束时间', '有效期天数', '状态',  '登记人', '登记时间', '执行人', '执行时间', '终止人', '终止时间'];
	vColumnModel = [
			{ name: 'iJLBH' },
			{ name: 'sGZMC' },
			{ name: 'sMDMC' },
			{ name: 'sSHDM', hidden: true },
			{ name: 'sSHMC', },
			{ name: 'sHYKNAME', },
		  //  { name: 'iHBFS', },
			{ name: 'dKSRQ', },
			{ name: 'dJSRQ', },
			{ name: 'sYHQMC', },
			{ name: 'dYHQJSRQ' },
			{ name: 'iYHQSL' },
			{
				name: 'iSTATUS', formatter: function (cellvalues) {
					if (cellvalues == 0)
						return "未生效";
					if (cellvalues == 1)
						return "已生效"
					if (cellvalues == 2)
						return "已终止";

				}
			},
			//{
			//    name: 'iBJ_WCLJF', formatter: function (cellvalues) {
			//        if (cellvalues == 0)
			//            return "往年积分";
			//        if (cellvalues == 1)
			//            return "当前积分";
			//    }
			//},
			//{ name: 'iBJ_CX', formatter: "checkbox" },
			{ name: 'sDJRMC', },
			{ name: 'dDJSJ', },
			{ name: 'sZXRMC', },
			{ name: 'dZXRQ', },
			{ name: 'sZZRMC', },
			{ name: 'dZZRQ', },
	];
}
$(document).ready(function () {
	$("#B_Exec").hide();
  
	

	$("#TB_HYKNAME").click(function () {
		SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", true);
	});
	$("#TB_SHMC").click(function () {
		SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", false);
	});
	$("#TB_YHQMC").click(function () {
		SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", true);
	});
	$("#TB_MDMC").click(function () {
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
	});
	$("#TB_DJRMC").click(function () {
		SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", true);
	});
	$("#TB_ZXRMC").click(function () {
		SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", true);
	});
	$("#TB_ZZRMC").click(function () {
		SelectRYXX("TB_ZZRMC", "HF_ZZR", "zHF_ZZR", true);
	});

	RefreshButtonSep();
});


function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
	MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE"," in",false);
	MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
	MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
	MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
	MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
	MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
	MakeSrchCondition(arrayObj, "HF_ZXR", "iDJR", "in", false);
	MakeSrchCondition(arrayObj, "HF_ZZR", "iZZR", "in", false);
	MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
	MakeSrchCondition(arrayObj, "TB_ZZRQ1", "dZZRQ", ">=", true);
	MakeSrchCondition(arrayObj, "TB_ZZRQ2", "dZZRQ", "<=", true);
	MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
	var vDJZT = $("[name='DJZT']:checked").val();
	if (vDJZT) {
		MakeSrchCondition2(arrayObj, vDJZT, "iSTATUS", "=", false);
	}
	//var vBJ_CX=$("[name='BJ_CX']:checked").val();
	//if (vBJ_CX >= 0)
	//    MakeSrchCondition2(arrayObj, vBJ_CX, "iBJ_CX", "=", false);
	
	MakeMoreSrchCondition(arrayObj);
	return arrayObj;
};

