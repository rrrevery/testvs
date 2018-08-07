
vUrl = "../CRMGL.ashx";
var vCZK = GetUrlParam("czk");

var ROWID = "listrow_" + 0;
var OLDROWID = ROWID;

function InitGrid() {
	vColumnNames = ["等级编号", "等级名称", ];
	vColumnModel = [
			{ name: "iHYKJCID", width: 80, },
			{ name: "sHYKJCNAME", width: 120, },
	];
};

$(document).ready(function () {
	$("#JLBHCaption").html("卡种编号");
	$("#status-bar").hide();

	$("#AddItem").click(function () {
	    if ($("#TB_DJBH").val() == "") {
	        ShowMessage("请输入等级编号", 3);
	        return;
	    }
	    if ($("#TB_JCMC").val() == "") {
	        ShowMessage("请输入级次名称", 3);
	        return;
	    }
	    var rows = $("#list").datagrid("getRows");
	    var boolRepeat = true;
	    for (var i = 0; i < rows.length; i++) {
	        if (rows[i].iHYKJCID == $("#TB_DJBH").val()) {
	            boolRepeat = false;
	            ShowMessage("级次编号重复");
	            return;
	        }
	    }
	    if (boolRepeat) {
	        $('#list').datagrid('appendRow', {
	            iHYKJCID: $("#TB_DJBH").val(),
	            sHYKJCNAME: $("#TB_JCMC").val(),
	        });
	    }
	});
	$("#DelItem").click(function () {
		DeleteRows("list");
	});
	SetControlState();
	if (vCZK == "1")
	{
		bNeedItemData = false;
		$("#zMP7").hide();
		$("#zMP7_Hidden").hide();
	}
});

function SetControlState() {
	//$("#S_SJFS option")[0].selected = "selectd";
    $("#B_Exec").hide();
    if (vCZK=="1")
    {
        $("#BJ_CZZH")[0].checked = true;
        $("#BJ_CZZH").attr("disabled",true); 
    }
}

function IsValidData() {
	if ($("#TB_HYKKZNAME").val() == "") {
		ShowMessage("请输入卡种名称", 3);
		return false;
	}
	if ($("#DDL_FXFS").val() == "") {
		ShowMessage("请选择发行方式", 3);
		return false;
	}
	if (vCZK == "0") {
		if ($("#DDL_YHFS").val() == "") {
		    ShowMessage("请选择默认折扣方式", 3);
			return false;
		}
	}
	if ($("#TB_HMCD").val() == "") {
		ShowMessage("请输入卡号长度", 3);
		return false;
	}
	if ($("#TB_YXQCD").val() == "") {
		ShowMessage("请输入有效期长度", 3);
		return false;
	}
	else {
		var pattern = /^[1-9]\d*[Y|M|D]$/;
		if (pattern.test($("#TB_YXQCD").val()) == false) {
			ShowMessage("有效期长度输入错误", 3);
			return false;
		}

	}
	if ($("#DDL_CDJZ").val() == null) {
		ShowMessage("请选择磁道介质", 3);
		return false;
	}
	if ($("#DDL_FS_YXQ").val() == null) {
		ShowMessage("请选择有效期指定方式", 3);
		return false;
	}
	return true;
}


function SaveData() {
	var Obj = new Object();
	Obj.iJLBH = $("#TB_JLBH").val();
	if (Obj.iJLBH == "")
		Obj.iJLBH = "0";
	Obj.sHYKKZNAME = $("#TB_HYKKZNAME").val();

	Obj.iFXFS = GetSelectValue("DDL_FXFS");//$("#DDL_FXFS").val();
	Obj.iKXBJ = $("#BJ_GS").prop("checked") ? 1 : 0;
	Obj.iZFBJ = $("#BJ_ZF").prop("checked") ? 1 : 0;
	Obj.iTKBJ = $("#BJ_TK").prop("checked") ? 1 : 0;

	Obj.iBJ_XSJL = $("#BJ_XSJL")[0].checked ? 1 : 0;
	Obj.iBJ_JF = $("#BJ_JF")[0].checked ? 1 : 0;

	Obj.iYHFS = GetSelectValue("DDL_YHFS")||0;
	Obj.iBJ_YHQZH = $("#BJ_YHQZH")[0].checked ? 1 : 0;

	Obj.iBJ_CZZH = $("#BJ_CZZH")[0].checked ? 1 : 0;
	Obj.iBJ_CZK = vCZK;

	Obj.iCDJZ = GetSelectValue("DDL_CDJZ");
	Obj.iFS_YXQ = GetSelectValue("DDL_FS_YXQ");
	Obj.iHMCD = $("#TB_HMCD").val();
	Obj.sKHQDM = $("#TB_KHQDM").val();
	Obj.sKHHZM = $("#TB_KHHZM").val();
	Obj.sYXQCD = $("#TB_YXQCD").val();
	Obj.iBJ_XTZK = $("#BJ_XTZK")[0].checked ? 1 : 0;
	Obj.iBJ_QZYK = $("#BJ_QZYK")[0].checked ? 1 : 0;
	Obj.iBJ_CDNRJM = $("#BJ_CDNRJM")[0].checked ? 1 : 0;

	Obj.iBJ_XFSJ = GetSelectValue("S_SJFS")||0;//$("#S_SJFS").val();
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
	$("#TB_HYKKZNAME").val(Obj.sHYKKZNAME);

	$("#DDL_FXFS").val(Obj.iFXFS);
	$("#BJ_GS")[0].checked = Obj.iKXBJ == "1" ? true : false;
	$("#BJ_ZF")[0].checked = Obj.iZFBJ == "1" ? true : false;
	$("#BJ_TK")[0].checked = Obj.iTKBJ == "1" ? true : false;

	$("#DDL_CDJZ").val(Obj.iCDJZ);
	$("#DDL_FS_YXQ").val(Obj.iFS_YXQ);
	$("#TB_HMCD").val(Obj.iHMCD);
	$("#TB_KHQDM").val(Obj.sKHQDM);
	$("#TB_KHHZM").val(Obj.sKHHZM);
	$("#TB_YXQCD").val(Obj.sYXQCD);
	$("#DDL_YHFS").val(Obj.iYHFS);

	$("#BJ_XSJL")[0].checked = Obj.iBJ_XSJL == "1" ? true : false;
	$("#BJ_JF")[0].checked = Obj.iBJ_JF == "1" ? true : false;
	$("#BJ_YHQZH")[0].checked = Obj.iBJ_YHQZH == "1" ? true : false;
	$("#BJ_CZZH")[0].checked = Obj.iBJ_CZZH == "1" ? true : false;
	$("#BJ_CZK")[0].checked = Obj.iBJ_CZK == "1" ? true : false;
	$("#BJ_XTZK")[0].checked = Obj.iBJ_XTZK == "1" ? true : false;
	$("#BJ_QZYK")[0].checked = Obj.iBJ_QZYK == "1" ? true : false;
	$("#BJ_CDNRJM")[0].checked = Obj.iBJ_CDNRJM == "1" ? true : false;

	$("#S_SJFS").val(Obj.iBJ_XFSJ);
	$('#list').datagrid('loadData', Obj.itemTable, "json");
	$('#list').datagrid("loaded");
}
function setDisplay(id, is) {
	if ($("#" + id).css("display") == "none") {
		$("#" + id).css("display", "block");   //  显示
	} else {
		$("#" + id).css("display", "none");   //  隐藏
	}
	$("html,body").animate({ scrollTop: $("#" + is).offset().top }, 1000);
}
