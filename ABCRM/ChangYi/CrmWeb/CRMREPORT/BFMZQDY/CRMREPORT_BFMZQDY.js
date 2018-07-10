vUrl = "../CRMREPORT.ashx";
$(document).ready(function () {
    FillSH("TB_SHMC");
    $("#TB_SHMC").change(function () {
        var yt = $(this).val();
        $("#HF_SHDM").val($(this).val());
    });

    vProcStatus = cPS_BROWSE;
    SetControlBaseState();

    var arrayObj = new Array();
    var arrayObjMore = new Array();
    $("#list").jqGrid(
	{
	    async: false,
	    datatype: "json",
	    url: vUrl + "?mode=Search&func=" + vPageMsgID,
	    postData: { 'afterFirst': JSON.stringify(arrayObj), 'ZSeldata': JSON.stringify(arrayObjMore) },
	    //mtype:"post",
	    colNames: ['年度','商户名称', 'SHDM','R权重', 'F权重', 'M权重', ],
	    colModel: [
            { name: 'iND', },//sortable默认为true width默认150
            { name: 'sSHMC', },
            { name: 'sSHDM',hidden:true },
            { name: 'fR_QZ', },
             { name: 'fF_QZ', },
              { name: 'fM_QZ', },


	    ],
	    shrinkToFit: false,
	    rownumbers: true,
	    //footerrow: true,
	    altRows: true,
	    width: 800,
	    height: 'auto',
	    rowNum: 10,
	    pager: '#pager',
	    viewrecords: true,
	    onSelectRow: function (rowid, status, e) {
	        if (!$("#B_Save").attr("disabled")) {
	            return false;
	        }
	        var rowData = $("#list").getRowData(rowid);
	        $("#HF_ND").val(rowData.iND);
	        $("#TB_ND").val(rowData.iND);
	        $("#HF_SHDM1").val(rowData.sSHDM);
	        $("#HF_SHDM").text(rowData.sSHDM);
	        $("#TB_SHMC").val(rowData.sSHDM);
	        $("#TB_F_QZ").val(rowData.fF_QZ);
	        $("#TB_R_QZ").val(rowData.fR_QZ);
	        $("#TB_M_QZ").val(rowData.fM_QZ);

	        $("#TB_JLBH").val("1");
	        //SetControlBaseState();
	        $("#B_Update").prop("disabled", false);
	        $("#B_Delete").prop("disabled", false);
	        //$("#list").jqGrid('setRowData', rowid, false, { background: 'silver' });
	    },

	});
});


function IsValidData() {

    if ($("#TB_R_QZ").val() == "") {
        art.dialog({ content: "R权重", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_M_QZ").val() == "") {
        art.dialog({ content: "M权重", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_R_QZ").val() == "") {
        art.dialog({ content: "R权重", lock: true, time: 2 });
        return false; 
    }
    if ($("#TB_SHMC").val() == "") {
        art.dialog({ content: "商户名称不能为空", lock: true, time: 2 });
        return false;
    }
    return true;
}
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
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sOldSHDM = $("#HF_SHDM1").val();

    Obj.iND = $("#TB_ND").val();
    Obj.iOldND = $("#HF_ND").val();
    Obj.sSHDM = $("#TB_SHMC").val();
    Obj.sSHMC = $("#TB_SHMC").val();
    Obj.fR_QZ= $("#TB_R_QZ").val();
    Obj.fF_QZ = $("#TB_F_QZ").val();
    Obj.fM_QZ = $("#TB_M_QZ").val();

    //Obj.iLoginRYID = iDJR;
    //Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_SHDM").text(Obj.sSHDM);
    $("#TB_SHMC").val(Obj.sSHMC);
    $("#TB_R_QZ").val(Obj.fR_ZQ);
    $("#TB_F_QZ").val(Obj.fF_ZQ);
    $("#TB_M_QZ").val(Obj.fM_ZQ);
}

function MakeJLBH(t_jlbh) {
    //生成iJLBH的JSON
    var Obj = new Object();
    Obj.iJLBH = t_jlbh;
    Obj.sSHDM = $("#HF_SHDM").val();
    return Obj;
}