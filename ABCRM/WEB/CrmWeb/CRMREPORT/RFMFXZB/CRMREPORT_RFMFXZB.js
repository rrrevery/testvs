//vPageMsgID = CM_CRMREPORT_RFMFXZB;
vUrl = "../CRMREPORT.ashx";
$(document).ready(function () {

    vProcStatus = cPS_BROWSE;
    SetControlBaseState();
    var ColumnName = "";
    var arrayObj = new Array();
    var arrayObjMore = new Array();
    //FillSH($("#S_SH"))
    if (vLX == "1") {
        FillSH($("#S_SH"));
        ColumnName = "商户名称 ";
        $("#DV_DMLX").html("商户");
    }
    else if (vLX == "2") {
        FillMD($("#S_SH"));
        ColumnName = "门店名称";
        document.getElementById("DV_DMLX").innerHTML = "门店";
        //document.getElementById("DV_DMLX").nodeValue = "门店";
    }


    $("#list").jqGrid(
	{
	    async: false,
	    datatype: "json",
	    url: vUrl + "?mode=Search&func=" + vPageMsgID,
	    postData: { 'afterFirst': JSON.stringify(arrayObj), 'ZSeldata': JSON.stringify(arrayObjMore) },
	    //mtype:"post",
	    colNames: ['年度', '级别', '商户代码', ColumnName, '起始消费金额', '终止消费金额', '起始来店次数', '终止来店次数', '起始来店频率', '终止来店频率', ],
	    colModel: [
            { name: 'iND', },
            { name: 'iJB', },
            { name: 'sSHDM', hidden: true },//sortable默认为true width默认150
            { name: 'sSHMC', },
			{ name: 'iXFJE_BEGIN', },
            { name: 'iXFJE_END', },
            { name: 'iLDCS_BEGIN', },
            { name: 'iLDCS_END', },
            { name: 'iLDPL_BEGIN', },
            { name: 'iLDPL_END', },
	    ],
	    shrinkToFit: false,
	    rownumbers: true,
	    //footerrow: true,
	    altRows: true,
	    width: 800,
	    height: 'auto',
	    rowNum: 10,
	    pager: '#pager',
	    sortable: true,
	    sortorder: "asc",
	    sortname: "iND",
	    viewrecords: true,
	    onSelectRow: function (rowid, status, e) {
	        if (!$("#B_Save").attr("disabled")) {
	            return false;
	        }
	        var rowData = $("#list").getRowData(rowid);
	        //$("#S_SH").val(rowData.sSHDM);
	        $("#S_SH").val(rowData.sSHDM);
	        $("#TB_ND").val(rowData.iND);
	        $("#TB_JB").val(rowData.iJB);
	        $("#TB_XFJE_BEGIN").val(rowData.iXFJE_BEGIN);
	        $("#TB_XFJE_END").val(rowData.iXFJE_END);
	        $("#TB_LDCS_BEGIN").val(rowData.iLDCS_BEGIN);
	        $("#TB_LDCS_END").val(rowData.iLDCS_END);
	        $("#TB_LDPL_BEGIN").val(rowData.iLDPL_BEGIN);
	        $("#TB_LDPL_END").val(rowData.iLDPL_END);
	        $("#TB_JLBH").val("1");
	        //SetControlBaseState();
	        $("#B_Update").prop("disabled", false);
	        $("#B_Delete").prop("disabled", false);
	        //$("#list").jqGrid('setRowData', rowid, false, { background: 'silver' });
	    },

	}
	);
});


function SetControlState() {
    $("#jlbh").hide();
    $("#B_Insert").show();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#list").trigger("reloadGrid");
    //if ($("#HF_SHDM").val() != "") {
    //    $("#list").
    //}
}

function IsValidData() {

    if ($("#TB_ND").val() == "") {
        art.dialog({ content: "ND不能为空", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_JB").val() == "") {
        art.dialog({ content: "级别不能为空", lock: true, time: 2 });
        return false;
    }
    if ($("#S_SH").val() == "") {
        art.dialog({ content: "商户代码称不能为空", lock: true, time: 2 });
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iND = $("#TB_ND").val();
    Obj.iJB = $("#TB_JB").val();

    //Obj.sOldSHDM = $("#HF_SHDM").val();
    Obj.sSHDM = $("#S_SH").val();

    Obj.iXFJE_BEGIN = $("#TB_XFJE_BEGIN").val() || 0;
    Obj.iXFJE_END = $("#TB_XFJE_END").val() || 0;
    Obj.iLDCS_BEGIN = $("#TB_LDCS_BEGIN").val() || 0;
    Obj.iLDCS_END = $("#TB_LDCS_END").val() || 0;
    Obj.iLDPL_BEGIN = $("#TB_LDPL_BEGIN").val() || 0;
    Obj.iLDPL_END = $("#TB_LDPL_END").val() || 0;
    //Obj.iLoginRYID = iDJR;
    //Obj.sLoginRYMC = sDJRMC;
    Obj.iLX = $("#vLX").val() || 0;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_ND").val(Obj.iND);
    $("#TB_JB").val(Obj.iJB);
    $("#S_SH").val(Obj.sSHDM);
    $("#TB_XFJE_BEGIN").val(Obj.iXFJE_BEGIN);
    $("#TB_XFJE_END").val(Obj.iXFJE_END);
    $("#TB_LDCS_BEGIN").val(Obj.iLDCS_BEGIN);
    $("#TB_LDCS_END").val(Obj.iLDCS_END);
    $("#TB_LDPL_BEGIN").val(Obj.iLDPL_BEGIN);
    $("#TB_LDPL_END").val(Obj.iLDPL_END);
}

function MakeJLBH(t_jlbh) {
    //生成iJLBH的JSON
    var Obj = new Object();
    Obj.iJLBH = t_jlbh;
    Obj.sSHDM = $("#S_SH").val();
    Obj.iND = $("#TB_ND").val();
    Obj.iJB = $("#TB_JB").val();
    return Obj;
}