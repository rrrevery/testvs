vUrl = "../CRMREPORT.ashx";
var STATUS;
$(document).ready(function () {
    $("#A").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#jlbh .dv_sub_left").html("节假日编号");
    FillJJR($("#S_JJRMC"));

    var arrayObj = new Array();
    var arrayObjMore = new Array();
    ND();

    $("#list").jqGrid(
	{
	    async: false,
	    datatype: "json",
	    url: vUrl + "?mode=Search&SearchMode=2&func=" + vPageMsgID,
	    postData: { 'afterFirst': JSON.stringify(arrayObj), 'ZSeldata': JSON.stringify(arrayObjMore) },
	    //mtype:"get",//默认GET
	    colNames: ["节假日代码", "节假日名称"],
	    colModel: [
			{ name: "iJLBH", width: 60, },
			{ name: "sJJRMC", width: 250, },


	    ],
	    sortable: true,
	    sortorder: "asc",
	    sortname: "iJLBH",
	    shrinkToFit: false,
	    rownumbers: true,
	    //footerrow: true,
	    altRows: true,
	    width: 350,
	    height: 'auto',
	    rowNum: 10,
	    pager: '#pager',
	    viewrecords: true,

	    onSelectRow: function (rowid, status, e) {
	        if (!$("#B_Save").attr("disabled")) {
	            return false;
	        }
	        var rowData = $("#list").getRowData(rowid);
	        $("#S_JJRMC").val(rowData.iJLBH);
	        //$("#S_JJRMC").val(rowData.sJJRMC);


	        $("#list2").jqGrid("clearGridData");
	        ND(rowData.iJLBH);


	      

	        SetControlBaseState();
	        $("#B_Update").prop("disabled", false);
	        $("#B_Delete").prop("disabled", false);



	    },

	});

    $("#list2").jqGrid(
   {
       async: false,
       datatype: "json",

       //mtype:"get",//默认GET
       colNames: ["年度", "开始日期", "结束日期"],
       colModel: [
           { name: "iND", width: 80, },
           { name: "dKSRQ", width: 150, },
           { name: "dJSRQ", width: 150, },

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
       pager: '#pager2',
       viewrecords: true,

       onSelectRow: function (rowid, status, e) {
           var rowData = $("#list2").getRowData(rowid);

           $("#TB_ND").val(rowData.iND);

           $("#TB_DJSJ1").val(rowData.dKSRQ);
           $("#TB_DJSJ2").val(rowData.dJSRQ);
       },

   });

    //if (vJLBH != "")
    //    ShowDataBase(vJLBH);



});

function change() { }
function ND(iJLBH) {
    var arrayObj = new Array();
    //MakeSrchCondition(arrayObj, "TB_CLSJ1", "dRQ", ">=", true);
    //MakeSrchCondition(arrayObj, "TB_CLSJ2", "dRQ", "<=", true);
    sjson = '{"iJLBH":' + iJLBH + '}';
    $("#list2").jqGrid('setGridParam', {
        url: vUrl + "?mode=Search&SearchMode=1&func=" + vPageMsgID,
        postData: { 'afterFirst': JSON.stringify(arrayObj), 'conditionData': JSON.stringify(sjson) },
        page: 1
    }).trigger("reloadGrid");



}
function SetControlState() {
    $("#B_Insert").show();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#list").trigger("reloadGrid");
}

function IsValidData() {
    if ($("#TB_JJRMC").val() == "") {
        art.dialog({ lock: true, content: "请输入名称" });
        return false;
    }
    if ($("#TB_ND").val() == "") {
        art.dialog({ lock: true, content: "请输入年度" });
        return false;
    }
    if ($("#TB_DJSJ1").val() == "") {
        art.dialog({ lock: true, content: "请输入开始日期" });
        return false;
    }
    if ($("#TB_DJSJ2").val() == "") {
        art.dialog({ lock: true, content: "请输入结束日期" });
        return false;
    }
    return true;
}
//按钮事件单独写
function InsertClick() {
    PageDate_Clear();
    vProcStatus = cPS_ADD;
    $("#LB_DJRMC").text(sDJRMC);
    $("#HF_DJR").val(iDJR);
    SetControlBaseState();
    InsertClickCustom();
    STATUS = 1;
};

function UpdateClick() {
    if ($("#TB_ND").val() == "") {

        art.dialog({ lock: true, content: "请选择年度" });
        return 

    }
    vProcStatus = cPS_MODIFY;
    SetControlBaseState();
    UpdateClickCustom();
    STATUS = 2;
    document.getElementById("TB_ND").disabled = true;




};
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JJRID").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";


    var myselect = document.getElementById("S_JJRMC");
    var index = myselect.selectedIndex;
    Obj.sJJRMC = myselect.options[index].text;
    //Obj.iJLBH = $("#S_MDMC").va();
    Obj.iJLBH = myselect.options[index].value;
    Obj.dKSRQ = $("#TB_DJSJ1").val();
    Obj.dJSRQ = $("#TB_DJSJ2").val();
    Obj.iND = $("#TB_ND").val();
    Obj.iSTATUS = STATUS;



    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#S_JJRMC").val(Obj.iJLBH);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_ND").val(Obj.iND);
}
