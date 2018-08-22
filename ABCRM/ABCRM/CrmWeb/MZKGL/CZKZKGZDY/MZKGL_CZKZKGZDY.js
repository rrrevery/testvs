vUrl = "../MZKGL.ashx";
var vRowID = 0;
var selRowID;
var iMDID;
var iHYKTYPE;
var fQDJE;

var vCaption = "面值卡赠卡规则";
function InitGrid() {
	vColumnNames = ['起点金额', '终点金额', '有效天数', '优惠券', 'YHQID', '赠送比例'];
	vColumnModel = [
			{ name: 'fQDJE', width: 130 },
			{ name: 'fZDJE', width: 130 },
			{ name: 'fYXQTS', width: 130,  },
			{ name: 'sYHQMC', width: 130, },
			{ name: 'iYHQID', hidden: true },
			{ name: 'fZSBL', width: 130 },
	];
}


$(document).ready(function () {
   
	$("#TB_MDMC").click(function () {
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
	})
	$("#TB_YHQMC").click(function () {
		SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", true);
	});
	$("#TB_HYKNAME").click(function () {
		SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", true, 1);
	});



  
});

function IsValidData() {

	if ($("#HF_YHQID").val() == "" && vGZLX == "0") {
		art.dialog({ content: "优惠券不能为空", lock: true, time: 2 });
		return false;
	}
	if ($("#TB_ZSBL").val() == "" && vGZLX == "0") {
		art.dialog({ content: "赠送比例不能为空", lock: true, time: 2 });
		return false;
	}
 
	if ($("#TB_QDJE").val() == "" || $("#TB_ZDJE").val() == "") {
		art.dialog({ content: "销售金额不能为空", lock: true, time: 2 });
		return false;
	}
	return true;
}

function SaveData() {
	var Obj = new Object();
	Obj.iJLBH = $("#TB_JLBH").val();
	if (Obj.iJLBH == "")
		Obj.iJLBH = "0";
	Obj.fQDJE = $("#TB_QDJE").val();
	Obj.fZDJE = $("#TB_ZDJE").val();
	Obj.fZSBL = $("#TB_ZSBL").val() == "" ? 0 : $("#TB_ZSBL").val();
	Obj.fYXQTS = $("#TB_YXQTS").val() == "" ? 0 : $("#TB_YXQTS").val();
	Obj.iYHQID = $("#HF_YHQID").val();
	Obj.sYHQMC = $("#TB_YHQMC").val();
	
	Obj.sDBConnName = "CRMDBMZK";
	Obj.iLoginRYID = iDJR;
	Obj.sLoginRYMC = sDJRMC;
	if (vProcStatus == cPS_ADD) {
		fQDJE = $("#TB_QDJE").val();
	}
	if (vProcStatus != cPS_ADD) {
		Obj.fOLDQDJE =  $("#HF_QDJE").val();
	}
	return Obj;
}

function ShowData(data) {

	$("#TB_JLBH").val(data.iJLBH);
	$("#TB_QDJE").val(data.fQDJE);
	$("#HF_QDJE").val(data.fQDJE);
	$("#TB_ZDJE").val(data.fZDJE);
	$("#HF_ZDJE").val(data.fZDJE);
	$("#TB_ZSBL").val(data.fZSBL);
	$("#TB_YXQTS").val(data.fYXQTS);
	$("#HF_YHQID").val(data.iYHQID);
	$("#TB_YHQMC").val(data.sYHQMC);
	
}

function SetControlState() {
	$("#jlbh").hide();
	$("#B_Insert").show();
	$("#B_Exec").hide();
	$("#status-bar").hide();
}