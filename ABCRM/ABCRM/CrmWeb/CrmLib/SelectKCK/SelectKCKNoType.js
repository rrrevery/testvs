var morePageArray = new Array();
var vCZK = GetUrlParam("czk");

function checkReapet(sCZKHM) {
    var boolInsert = true;
    if (morePageArray.length != 0) {
        for (i = 0; i < morePageArray.length; i++) {
            if (sCZKHM == morePageArray[i].sCZKHM) {
                boolInsert = false
            }
        }
    }

    return boolInsert;
}


function delSelectLP(sCZKHM) {
    if (morePageArray.length != 0) {
        for (var i = 0; i < morePageArray.length; i++) {
            if (sCZKHM == morePageArray[i].sCZKHM) {
                // morePageArray.remove(i);
                morePageArray.splice(i, 1);
            }
        }

    }
}

function setSelectLP() {
    var currentPage = $("#list").getGridParam("page");
    for (var i = 0; i < morePageArray.length; i++) {
        if (morePageArray[i].iPAGEID == currentPage) {
            $("#list").jqGrid("setSelection", morePageArray[i].iROWID);
        }
    }
}

$(document).ready(function () {
    vBGDDDM = GetUrlParam("sBGDDDM");
    vHYKTYPE = GetUrlParam("iHYKTYPE");
    var HidHykname = false;
    if (vHYKTYPE != "") {
        HidHykname = true;
    }
    vSTATUS = GetUrlParam("iSTATUS");
    vHF = GetUrlParam("vHF");
    sDBConnName = GetUrlParam("sDBConnName");

    $("#list").datagrid(
	{
	    datatype: "json",
	    //mtype:"get",//默认GET
	    colNames: ['卡号', 'iHYKTYPE', '卡类型', '面值金额', '有效期', ],
	    colModel: [
            { name: 'sCZKHM', index: 'sCZKHM', width: 80, },//sortable默认为true width默认150
            { name: 'iHYKTYPE', hidden: true, },
            { name: 'sHYKNAME', hidden: HidHykname },
            { name: 'fQCYE', },
            { name: 'dYXQ', },

	    ],
	    //loadonce: true,
	    sortable: true,
	    sortorder: "desc",
	    sortname: 'sCZKHM',
	    shrinkToFit: false,
	    rownumbers: true,
	    //footerrow: true,
	    altRows: true,
	    width: 500,
	    height: 300,
	    rowNum: 10,
	    pager: '#pager',
	    viewrecords: true,
	    multiselect: true,


	    onSelectRow: function (rowid, status) {
	        var selectID = $("#list").jqGrid('getGridParam', 'selrow');
	        var selectedLP = new Object();
	        var rowData = $("#list").jqGrid("getRowData", selectID);
	        selectedLP = rowData;
	        selectedLP.iROWID = rowid;
	        selectedLP.iPAGEID = $("#list").getGridParam("page");

	        if (status == true) {
	            if (checkReapet(selectedLP.sCZKHM) == true) {
	                morePageArray.push(selectedLP);
	            }
	        }
	        if (status == false) {
	            var delData = $("#list").jqGrid("getRowData", rowid);
	            delSelectLP(delData.sCZKHM);
	        }
	    },

	    onSelectAll: function (aRowids, status) {
	        if (status == true) {
	            for (var q = 0; q < aRowids.length; q++) {
	                var rowData = $("#list").getRowData(aRowids[q]);
	                var mulSelectLP = new Object();
	                mulSelectLP = rowData;
	                mulSelectLP.iROWID = aRowids[q];
	                mulSelectLP.iPAGEID = $("#list").getGridParam("page");
	                delSelectLP(mulSelectLP.sCZKHM);
	                morePageArray.push(mulSelectLP);
	            }
	        }
	        if (status == false) {
	            for (var j = 0; j < aRowids.length; j++) {
	                var rowData = $("#list").getRowData(aRowids[j]);
	                delSelectLP(rowData.sCZKHM);
	            }
	        }
	    },

	    gridComplete: function () {
	        setSelectLP();
	    }


	});
    $("#B_Search").click(function () {
        $("#list").jqGrid('setGridParam', {
            url: "../CrmLib.ashx?func=" + (vCZK == 1 ? "GetMZKKCKList" : "GetKCKList"),
            postData: { sBGDDDM: vBGDDDM, iSTATUS: vSTATUS, iHYKTYPE: vHYKTYPE, sCZKHM_Begin: $("#TB_KSKH").val(), sCZKHM_End: $("#TB_JSKH").val(), iSL: $("#TB_SL").val(), iHF: vHF, sDBConnName: sDBConnName },
            page: 1
        }).trigger("reloadGrid");
    });
    $("#B_Confirm").click(function () {
        //var selectedIds;
        //var lst = new Array();
        //selectedIds = $("#list").jqGrid('getGridParam', 'selarrrow');
        //for (i = 0; i < selectedIds.length; i++) {
        //    var lst1 = new Object();
        //    var rowData = $("#list").jqGrid("getRowData", selectedIds[i]);//$("#list").getRowData(i);
        //    lst1 = rowData;
        //    lst.push(lst1);
        //}
        //window.parent.LoadData(lst);
        window.parent.LoadData(morePageArray);
        art.dialog.close();
    });
    $("#B_Cancel").click(function () {
        //art.dialog.close();
    });
});
