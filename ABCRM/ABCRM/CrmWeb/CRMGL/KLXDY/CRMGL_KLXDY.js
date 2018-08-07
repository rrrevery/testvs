vUrl = "../CRMGL.ashx";
var vCZK = GetUrlParam("czk");

function InitGrid() {
	vColumnNames = ["门店ID", "门店", "折扣方式ID", "折扣方式"];
	vColumnModel = [
			{ name: "iMDID", hidden: true, },
			{ name: "sMDMC", width: 100, },
			{ name: "iZKFS", hidden: true, },
			{ name: "sZKFS", width: 100, },
	];
};

$(document).ready(function () {
	$("#B_Exec").hide();
	$("#JLBHCaption").html("卡类型代码");
	$("#status-bar").hide();
	bNeedItemData = false;
	FillKZ("S_KZ", vCZK);
	FillHYKJC("S_HYKJC", GetSelectValue("S_KZ"));
	$("#S_KZ").change(function () {
		$("#S_HYKJC").empty();
		GetKZData($("#S_KZ").val());
	});
	if (GetUrlParam("jlbh") == "") {
		GetKZData(GetSelectValue("S_KZ"));
	}
	$("#TB_MDMC").click(function () {
		SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
	});



	$("#AddItem").click(function () {
		if ($("#HF_MDID").val() == "") {
			ShowMessage("请选择门店", 3);
			return;
		}
		if (GetSelectValue("S_ZKFS") == "") {
			ShowMessage("请选择折扣方式", 3);
			return;
		}
		var rows = $("#list").datagrid("getRows");
		var boolRepeat = true;
		for (var i = 0; i < rows.length; i++) {
		    if (rows[i].iMDID == $("#HF_MDID").val()) {
		        boolRepeat = false;
		        ShowMessage("门店重复");
		        return;
		    }
		}
		if (boolRepeat) {
		    $('#list').datagrid('appendRow', {
		        iMDID: $("#HF_MDID").val(),
		        sMDMC: $("#TB_MDMC").val(),
		        iZKFS: GetSelectValue("S_ZKFS"),
		        sZKFS: $("#S_ZKFS option:selected").text(),
		    });
		}

	});
	$("#DelItem").click(function () {
		DeleteRows("list");
	});
	//$("#cdnrjm").hide();
});

function SetControlState() {
	//$("#DDL_FS_SYMD option")[0].selected = "selectd";
	if (vProcStatus == cPS_MODIFY || vProcStatus == cPS_BROWSE) {
		document.getElementById('S_KZ').disabled = true;
	}
	else {
		document.getElementById('S_KZ').disabled = false;
	}
	if ($("#TB_JLBH").val()) {
		return;
	}
	if (vCZK == "1") {
	    $("#BJ_CZZH")[0].checked = true;
	    $("#BJ_CZZH").attr("disabled", true);
	    $("#hyk").hide();
	    $("#hyk1").hide();
	    $("#hyk2").hide();
	    $("#hyk3").hide();
	    $("#hyk4").hide();
	    $("#hyk5").hide();
	}

}

function IsValidData() {
	if ($("#S_KZ").val() == "") {
		ShowMessage("请选择卡种", 3);
		return false;
	}
	if ($("#TB_HYKNAME").val() == "") {
		ShowMessage("请输入卡类型名称", 3);
		return false;
	}
	if ($("#S_HYKJC").val() == "") {
		ShowMessage("请选择卡级次", 3);
		return false;
	}
	return true;
}

function SaveData() {
	var Obj = new Object();
	Obj.iJLBH = $("#TB_JLBH").val();
	if (Obj.iJLBH == "")
		Obj.iJLBH = "0";
	Obj.iHYKKZID = GetSelectValue("S_KZ");
	Obj.sHYKNAME = $("#TB_HYKNAME").val();
	Obj.iFXFS = GetSelectValue("DDL_FXFS");
	Obj.iJFCLFWFS = GetSelectValue("DDL_CLFS") == null ? 0 : GetSelectValue("DDL_CLFS");
	Obj.fJFXX = $("#TB_JFXX").val();

	Obj.iFS_SYMD = GetSelectValue("DDL_FS_SYMD");

	Obj.fKFJE = $("#TB_KFJE").val();
	Obj.iHMCD = $("#TB_HMCD").val();
	Obj.sKHQDM = $("#TB_KHQDM").val();
	Obj.sKHHZM = $("#TB_KHHZM").val();
	Obj.iCDJZ = GetSelectValue("DDL_CDJZ");
	Obj.iFS_YXQ = GetSelectValue("DDL_FS_YXQ");
	Obj.sYXQCD = $("#TB_YXQCD").val();
	Obj.iBJ_XSJL = $("#BJ_XSJL").prop("checked") ? 1 : 0;
	Obj.iBJ_JF = $("#BJ_JF").prop("checked") ? 1 : 0;
	Obj.iBJ_YHQZH = $("#BJ_YHQZH").prop("checked") ? 1 : 0;
	Obj.iBJ_CZZH = $("#BJ_CZZH").prop("checked") ? 1 : 0;
	Obj.iBJ_CZK = vCZK;
	Obj.iKXBJ = $("#BJ_GS").prop("checked") ? 1 : 0;
	Obj.iTKBJ = $("#BJ_TK").prop("checked") ? 1 : 0;
	Obj.iZFBJ = $("#BJ_ZF").prop("checked") ? 1 : 0;
	Obj.iBJ_TH = $("#BJ_TH").prop("checked") ? 1 : 0;
	Obj.iBJ_XK = $("#BJ_XK").prop("checked") ? 1 : 0;
	Obj.iBJ_XTZK = $("#BJ_XTZK").prop("checked") ? 1 : 0;
	Obj.iBJ_QZYK = $("#BJ_QZYK").prop("checked") ? 1 : 0;
	Obj.iBJ_FSK = $("#BJ_FSK").prop("checked") ? 1 : 0;
	Obj.iBJ_YZM = $("#BJ_YZM").prop("checked") ? 1 : 0;
	Obj.iBJ_PSW = $("#BJ_MMBS").prop("checked") ? 1 : 0;
	Obj.iBJ_CDNRJM = $("#BJ_CDNRJM").prop("checked") ? 1 : 0;
	Obj.iHYKJCID = GetSelectValue("S_HYKJC")||0;
	Obj.sSJJZQ = $("#TB_SJJZQ").val();
	Obj.iYHFS = GetSelectValue("DDL_YHFS")||0;
	var lst = new Array();
	lst = $("#list").datagrid("getData").rows;
	Obj.itemTable = lst;

	Obj.iLoginRYID = iDJR;
	Obj.sLoginRYMC = sDJRMC;

	return Obj;

}

function ShowData(data) {
	var Obj = JSON.parse(data);
	$("#TB_JLBH").val(Obj.iJLBH);
	$("#TB_HYKNAME").val(Obj.sHYKNAME);

	$("#DDL_CLFS").val(Obj.iJFCLFWFS);
	$("#DDL_FS_SYMD").val(Obj.iFS_SYMD);
	$("#S_KZ").val(Obj.iHYKKZID);

	//显式卡类型的新数据
	ShowKZData(Obj);
	FillHYKJC("S_HYKJC", GetSelectValue("S_KZ"));

	$("#S_HYKJC").val(Obj.iHYKJCID);



	$('#list').datagrid('loadData', Obj.itemTable, "json");
	$('#list').datagrid("loaded");
}

function GetKZData(iJLBH) {
	var sjson = "{'iJLBH':" + iJLBH + "}";
	result = "";
	$.ajax({
		type: 'post',
		url: vUrl + "?func=" + vPageMsgID2 + "&mode=View",//调用卡种定义的查询方法
		dataType: "json",
		async: false,
		data: { json: sjson, titles: 'ys' },
		success: function (data) {
			ShowKZData(data);
			FillHYKJC("S_HYKJC", $("#S_KZ").val());
		},
		error: function (data) {
			result = "";
		}
	});
	return result;
}

function GetMDString() {
	var result = "";
	sjson = "{}";
	$.ajax({
		type: 'post',
		url: "../../CrmLib/CrmLib.ashx?func=FillMD",
		dataType: "json",
		async: false,
		data: { json: JSON.stringify(sjson), titles: 'ys' },
		success: function (data) {
			for (i = 0; i < data.length; i++) {
				result += data[i].iMDID + ":" + data[i].sMDMC + ";";
			}
		},
		error: function (data) {
			art.dialog({ content: data.responseText, lock: true, time: 2 });
		}
	});
	return result.substr(0, result.length - 1);
}

function ShowKZData(Obj) {
	$("#DDL_FXFS").val(Obj.iFXFS);
	$("#TB_JFXX").val(Obj.fJFXX);
	$("#TB_KFJE").val(Obj.fKFJE);
	$("#TB_HMCD").val(Obj.iHMCD);
	$("#TB_KHQDM").val(Obj.sKHQDM);
	$("#TB_KHHZM").val(Obj.sKHHZM);
	$("#DDL_CDJZ").val(Obj.iCDJZ);
	$("#DDL_FS_YXQ").val(Obj.iFS_YXQ);
	$("#TB_YXQCD").val(Obj.sYXQCD);
	$("#DDL_YHFS").val(Obj.iYHFS);
	$("#BJ_XSJL").prop("checked", Obj.iBJ_XSJL == 1 ? true : false);
	$("#BJ_JF").prop("checked", Obj.iBJ_JF == 1 ? true : false);
	$("#BJ_YHQZH").prop("checked", Obj.iBJ_YHQZH == 1 ? true : false);
	$("#BJ_CZZH").prop("checked", Obj.iBJ_CZZH == 1 ? true : false);
	$("#BJ_CZK").prop("checked", Obj.iBJ_CZK == 1 ? true : false);

	$("#BJ_GS").prop("checked", Obj.iKXBJ == 1 ? true : false);
	$("#BJ_TK").prop("checked", Obj.iTKBJ == 1 ? true : false);
	$("#BJ_ZF").prop("checked", Obj.iZFBJ == 1 ? true : false);
	$("#BJ_TH").prop("checked", Obj.iBJ_TH == 1 ? true : false);
	$("#BJ_XK").prop("checked", Obj.iBJ_XK == 1 ? true : false);

	$("#BJ_XTZK").prop("checked", Obj.iBJ_XTZK == 1 ? true : false);
	$("#BJ_QZYK").prop("checked", Obj.iBJ_QZYK == 1 ? true : false);
	$("#BJ_FSK").prop("checked", Obj.iBJ_FSK == 1 ? true : false);
	$("#BJ_YZM").prop("checked", Obj.iBJ_YZM == 1 ? true : false);
	$("#BJ_MMBS").prop("checked", Obj.iBJ_PSW == 1 ? true : false);
	$("#TB_SJJZQ").val(Obj.sSJJZQ);
	$("#BJ_CDNRJM").prop("checked", Obj.iBJ_CDNRJM == 1 ? true : false);
}
function setDisplay(id, is) {
	if ($("#" + id).css("display") == "none") {
		$("#" + id).css("display", "block");   //  显示
	} else {
		$("#" + id).css("display", "none");   //  隐藏
	}
	$("html,body").animate({ scrollTop: $("#" + is).offset().top }, 1000);
}