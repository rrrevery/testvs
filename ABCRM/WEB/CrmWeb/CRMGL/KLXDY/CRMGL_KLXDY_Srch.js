vUrl = "../CRMGL.ashx";
var vCZK = GetUrlParam("czk");
vCaption = vCZK == "0" ? "会员卡类型定义" : "面值卡类型定义";

function InitGrid() {
	vColumnNames = ["卡类型代码", "卡类型名称", "卡种名称","发行方式", "允许退货积分下限", "年费金额", "卡号长度", "卡号前导码", "卡号后置码", "磁道介质", "卡有效期指定方式", "有效期长度", "销售记录", "参与消费积分", "开通优惠券账户", "开通储值账户", "发卡时有面值", "允许挂失", "允许退卡", "允许作废", "退货标记", "续款标记", "需要系统制卡", "需要强制验卡", "有附属卡标识", "需要输入验证码", "需要密码标识", "磁道内容加密"];
	vColumnModel = [
			{ name: "iJLBH", },
		  { name: "sHYKNAME", },
		  { name: "sHYKKZNAME", },
		  { name :"sFXFS",},
		  { name: "fJFXX", },
		  { name: "fKFJE", },
		  { name: "iHMCD", },
		  { name: "sKHQDM", },
		  { name: "sKHHZM", },
		  { name: "sCDJZ", },
		  { name: "sFS_YXQ", },
		  { name: "sYXQCD", },
		  { name: "iBJ_XSJL", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_JF", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_YHQZH", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_CZZH", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_CZK", width: 60, formatter: BoolCellFormat, },
		  { name: "iKXBJ", width: 60, formatter: BoolCellFormat, },
		  { name: "iTKBJ", width: 60, formatter: BoolCellFormat, },
		  { name: "iZFBJ", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_TH", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_XK", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_XTZK", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_QZYK", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_FSK", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_YZM", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_PSW", width: 60, formatter: BoolCellFormat, },
		  { name: "iBJ_CDNRJM", width: 60, formatter: BoolCellFormat, },
	];
};

$(document).ready(function () {
	ConbinDataArry["czk"] = vCZK;
	$("#B_Exec").hide();
	FillKZ("S_KZ", vCZK);
});


function MakeSearchCondition() {
	var arrayObj = new Array();
	MakeSrchCondition(arrayObj, "TB_HYKTYPE", "iHYKTYPE", "=", false);
	MakeSrchCondition(arrayObj, "TB_HYKNAME", "sHYKNAME", "=", true);
	MakeSrchCondition(arrayObj, "S_KZ", "iHYKKZID", "=", false);
	MakeSrchCondition2(arrayObj, vCZK, "iBJ_CZK", "=", false);
	MakeMoreSrchCondition(arrayObj);
	return arrayObj;
};
function AddCustomerCondition(Obj) {
    Obj.qx = false;
}