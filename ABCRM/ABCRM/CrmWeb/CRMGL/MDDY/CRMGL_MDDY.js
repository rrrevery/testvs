vUrl = "../CRMGL.ashx";
vCaption = "门店定义";

function InitGrid() {
	vColumnNames = ['门店编号', 'SHDM', '商户名称', '门店代码', '门店名称', 'GXSHDM', '管辖商户', '进销存分店编号'];
	vColumnModel = [
			{ name: 'iMDID', },
			{ name: 'sSHDM', hidden: true, },
			{ name: 'sSHMC', },
			{ name: 'sMDDM', },//sortable默认为true width默认150
			{ name: 'sMDMC', },
			{ name: 'sGXSHDM', hidden: true },
			{ name: 'sGXSHMC', hidden: true },
			{ name: 'iFDBH_JXC'},
	];
}


$(document).ready(function () {
    $("#JLBHCaption").html("门店编号");
	$("#B_Insert").show();
	$("#B_Exec").hide();
	$("#TB_FDBH_JXC").inputmask({ "mask": "9{*}" });;

	FillSH($("#S_SH"));
	FillSH($("#S_GXSH"));

	//$("#S_SH").change(function () {
	//    if ($("#S_SH").val() == "") {
	//        art.dialog({ lock: true, content: "请先选择商户" });
	//    } else {
	//        $("#TB_MDDM").val($("#S_SH").val());
	//        $("#TB_MDDM").attr("maxlength", $("#S_SH").val().length + parseInt(4));
	//        //$("#TB_MDDM").inputmask("mask", { "mask": $("#S_SH").val().replace("9", "\\9") + "9999" });
	//        //$("#TB_MDDM").inputmask($("#S_SH").val() + "aaaa" );
	//    }
	//})
});

function selComSH(record) {;
	$("#TB_MDDM").val(record.value);
	$("#TB_MDDM").attr("maxlength", record.value.length + parseInt(4));
}

function IsValidData() {
	if ($("#TB_MDDM").val().indexOf($("#S_SH").combobox("getValue")) != 0) {
		ShowMessage("门店代码必须以商户代码开头！");
		return false;
	}
	if ($("#S_SH").combobox("getValue") == "") {
		ShowMessage("请选择商户");
		return false;
	}
	return true;
}

function SaveData() {
	var Obj = new Object();
	Obj.iJLBH = $("#TB_JLBH").val();
	if (Obj.iJLBH == "")
		Obj.iJLBH = "0";
	Obj.sSHDM = $("#S_SH").combobox("getValue");
	//Obj.sGXSHDM = $("#S_GXSH").combobox("getValue");
	Obj.sMDDM = $("#TB_MDDM").val();
	Obj.sMDMC = $("#TB_MDMC").val();
	Obj.iFDBH_JXC = IsNullValue($("#TB_FDBH_JXC").val(), 0);
	return Obj;
}

function ShowData(data) {
    //var Obj = JSON.parse(data);
    $("#S_SH").combobox("setValue", '');
    $("#TB_JLBH").val(data.iMDID);
   // $("#S_SH").combobox("select", data.sSHDM);
	$("#S_SH").combobox("setValue", data.sSHDM);
	$("#S_SH").combobox("setText", data.sSHMC);
	//$("#S_SH").val(data.sSHDM);
	$("#TB_MDDM").val(data.sMDDM);
	$("#TB_MDMC").val(data.sMDMC);
	$("#TB_FDBH_JXC").val(data.iFDBH_JXC);
}
function InsertClickCustom() {
    $("#S_SH").combobox("setValue", "选择商户");
};
function AddCustomerCondition(Obj) {
    Obj.qx = false;
}