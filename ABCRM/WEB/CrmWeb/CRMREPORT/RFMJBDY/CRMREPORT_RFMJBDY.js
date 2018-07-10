//vPageMsgID = CM_CRMREPORT_RFMJB;
vUrl = "../CRMREPORT.ashx";

$(document).ready(function () {

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
	    colNames: ['级别', '比例', ],
	    colModel: [
            { name: 'iJB', },//sortable默认为true width默认150
			{ name: 'iBL', },

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
	    sortname: "iJB",
	    viewrecords: true,
	    onSelectRow: function (rowid, status, e) {
	        if (!$("#B_Save").attr("disabled")) {
	            return false;
	        }
	        var rowData = $("#list").getRowData(rowid);
	        $("#HF_JB").val(rowData.iJB);
	        $("#TB_JB").val(rowData.iJB);
	        $("#TB_BL").val(rowData.iBL);
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

    if ($("#TB_JB").val() == "") {
        art.dialog({ content: "级别不能为空", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_BL").val() == "") {
        art.dialog({ content: "比例不能为空", lock: true, time: 2 });
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iOldJB = $("#HF_JB").val()||0;
    Obj.iJB = $("#TB_JB").val();
    Obj.iBL = $("#TB_BL").val();

    //Obj.iLoginRYID = iDJR;
    //Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_JB").val(Obj.iJB);
    $("#TB_BL").val(Obj.iBL);

}

function MakeJLBH(t_jlbh) {
    //生成iJLBH的JSON
    var Obj = new Object();
    Obj.iJLBH = t_jlbh;
    Obj.iJB = $("#TB_JB").val();
    return Obj;
}