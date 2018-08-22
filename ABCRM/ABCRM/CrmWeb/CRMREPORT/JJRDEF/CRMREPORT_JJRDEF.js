vUrl = "../CRMREPORT.ashx";
//var STATUS;
$(document).ready(function () {

    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#jlbh .dv_sub_left").html("节假日编号");

    var arrayObj = new Array();
    var arrayObjMore = new Array();

    $("#list").jqGrid(
	{
	    async: false,
	    datatype: "json",
	    url: vUrl + "?mode=Search&func=" + vPageMsgID,
	    postData: { 'afterFirst': JSON.stringify(arrayObj), 'ZSeldata': JSON.stringify(arrayObjMore) },
	    //mtype:"get",//默认GET
	    colNames: ["节假日代码", "节假日名称"],
	    colModel: [
			{ name: "iJLBH", width: 120, },
			{ name: "sJJRMC", width: 250, },


	    ],
	    sortable: true,
	    sortorder: "asc",
	    sortname: "iJLBH",
	    shrinkToFit: false,
	    rownumbers: true,
	    //footerrow: true,
	    altRows: true,
	    width: 400,
	    height: 'auto',
	    rowNum: 10,
	    pager: '#pager',
	    viewrecords: true,

	    onSelectRow: function (rowid, status, e) {
	        if (!$("#B_Save").attr("disabled")) {
	            return false;
	        }
	        var rowData = $("#list").getRowData(rowid);
	        $("#TB_JLBH").val(rowData.iJLBH);
	        $("#TB_MC").val(rowData.sJJRMC);

	        //SetControlBaseState();
	        $("#B_Update").prop("disabled", false);
	        $("#B_Delete").prop("disabled", false);



	    },

	});

  


});

function SetControlState() {
    $("#B_Insert").show();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#list").trigger("reloadGrid");
}

function IsValidData() {
    if ($("#TB_MC").val() == "") {
        art.dialog({ lock: true, content: "请输入名称" });
        return false;
    }
 
    return true;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sJJRMC = $("#TB_MC").val();
 



    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_MC").val(Obj.sJJRMC);
   
}