vUrl = vCZK == "1" ? "../../MZKGL/MZKGL.ashx" : "../HYKGL.ashx";

var rowNumer = 0;
var jkjlbh = GetUrlParam("jkjlbh");
var timeCDNR; //= setTimeout("WriteCard()", 500);// setInterval("WriteCard()", 2000);
var WriteIndex = 0;
var listLength = 0;
var rowData = null;
var LastSucccessDate = null;
vInitHeight = 600;
function IsValidData() {
    return true;
}

function InitGrid() {
    vColumnNames = ['iJLBH', '已制卡标志', "卡号", '磁道内容', "面值金额"];
    vColumnModel = [
          { name: "iJLBH", hidden: true },
          {
              name: "iBJ_ZK", width: 80, formatter: function (cellvalue) {
                  return cellvalue == "0" ? "✘" : "✔";
              }
          },
          { name: "sCZKHM", width: 150, },
          { name: "sCDNR", width: 150, hidden: true },
          { name: "fJE", width: 100, hidden: true }

    ];
};

function WriteCard() {
    var vCurrentDate = new Date();
    if (LastSucccessDate != null) {
        var vDiffDate = vCurrentDate - LastSucccessDate;
        if (parseInt(vDiffDate) > 60000) {
            window.clearTimeout(timeCDNR);
            return;
        }
    }

    var lst = $("#list").datagrid("getData").rows;
    if (lst.length > 0) {
        listLength = lst.length;
        rowData = lst[WriteIndex];
        if (parseInt(WriteIndex) > (parseInt(listLength) - 1)) {
            window.clearTimeout(timeCDNR);
        }
        else {
            //rwcard.WriteCard("2;2;1,9600,n,8,1", rowData.sCDNR);
            var WriteResult = HttpGetWriteCard("http://localhost:22345", "write", "2;2;1,9600,n,8,1", rowData.sCDNR);
            // var Obj = JSON.parse(WriteResult);
            if (WriteResult.indexOf("error") < 0) {//rwcard.LastError
                LastSucccessDate = new Date();
                SaveClick();
            }
            else {
                timeCDNR = setTimeout("WriteCard()", 500);

            }
        }
    }
    else {
        window.clearTimeout(timeCDNR);
    }
}


function CalcCXBFKHBC(sHYK_NO1, sHYK_NO2) {
    var str = GetCXBFKHBC(sHYK_NO1, sHYK_NO2);
    if (str == "null" || str == "") {
        ShowMessage("没有找到卡号", 3);
        return;
    }
    var data = JSON.parse(str);
    for (i = 0; i < data.length; i++) {
        $('#list').datagrid('appendRow', {
            iJLBH: data[i].iJLBH,
            iBJ_ZK: data[i].iBJ_ZK,
            sCZKHM: data[i].sCZKHM,
            sCDNR: data[i].sCDNR,
            fJE: data[i].fJE,
        });
        $("#LB_ZSL").text(data.length);
        //$("#LB_ZCKSL").text(ZSL);

    }
}


$(document).ready(function () {
    document.getElementById("CX").onclick = CX;

    $("#CX").click(function () {

        $('#list').datagrid("loadData", { total: 0, rows: [] });

        if (($("#TB_HYKNO1").val() == "" && $("#TB_HYKNO2").val() == "")) {
            ShowMessage("请输入卡号", 3);
            return;
        }

        CalcCXBFKHBC($("#TB_HYKNO1").val(), $("#TB_HYKNO2").val());


    });


    $("#B_Exec").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Insert").hide();
    $("#B_Save").hide();
    $("#B_Cancel").hide();
    $("#status-bar").hide();
    RefreshButtonSep();
    if (jkjlbh != "") {
        $("#TB_JKJLBH").val(jkjlbh);
        ShowJKXX(jkjlbh);
    }

    $("#TB_JKJLBH").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            ShowJKXX($("#TB_JKJLBH").val());
        }
    });
    $("#Export").click(function () {
        ExportClick();
    });
    $("#Save").click(function () {
        //SaveClick();
        WriteCard();
    });
    //$("#btnWriteCard").click(function () {

    //});

});


function PostToExport(URL, PARAMS) {
    // T 解决导出出URL长度大于限制长度的问题   2016-12-10 
    var temp = document.createElement("form");
    temp.style.display = "none";
    for (var x in PARAMS) {
        var opt = document.createElement("textarea");
        opt.name = x;
        opt.value = PARAMS[x];
        temp.appendChild(opt);
    }
    document.body.appendChild(temp);
    temp.action = URL;
    temp.method = "post";
    temp.submit();

    return temp;
}

//function ShowData(data)
//{
//    ShowJKXX($("#TB_JKJLBH").val());
//}


function ShowJKXX(jkjlbh) {
    var Obj = new Object();
    Obj.SearchConditions = MakeJLBH(jkjlbh);
    Obj.iJLBH = jkjlbh;
    Obj.iBJ_ZK = 0;
    var canBeClose = false;
    var success = false;
    var myDialog = art.dialog({
        lock: true,
        content: "<div class='bfdialog otherlog' style='width:200px'>正在查询数据,请等待……</div>",
        close: function () {
            if (canBeClose) {
                return true;
            }
            return false;
        }
    });
    $.ajax({
        //async:false,
        type: "post",
        url: vUrl + "?mode=View" + "&func=" + vPageMsgID2,
        data: { json: JSON.stringify(Obj).replace(/\\/g, "").replace(/\"{/g, "{").replace(/}\"}/g, "}}"), titles: 'cybillview' },
        success: function (data) {
            canBeClose = true;
            myDialog.close();
            //if (data.indexOf('错误') >= 0) {
            //    //initbuttonStatus(modifyType1);
            //    //changeBtnStatusByModifyType(lastbuttonenum);
            //    art.dialog({ lock: true, content: data })
            //    return;
            //}
            if (data.length > 1) {//&& data.indexOf("错误") < 0) {//判断返回的数据长度 添加时返回billid，其他则返回错误信息
                ShowJKXXData(data);
                //vProcStatus = cPS_BROWSE;
                SetControlBaseState();
            }
            else {
                ShowErrMessage("操作失败");
            }
        },
        error: function (data) {
            canBeClose = true;
            myDialog.close();
            ShowErrMessage("查询失败");
        }
    });



}




function ShowJKXXData(data) {
    var Obj = JSON.parse(data);
    $("#LB_ZSL").text(Obj.itemTable.length);
    $("#LB_ZKSL").text(0);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#TB_JLBH").val(jkjlbh);

    timeCDNR = setTimeout("WriteCard()", 500);

}

function ExportClick() {
    var obj = MakeSearchJSONZK();
    var zUrl = vUrl + "?mode=Export&func=" + vPageMsgID3;
    var array = new Array();
    array["json"] = JSON.stringify(obj);
    PostToExport(zUrl, array);
    SetControlBaseState();
};



function AddCustomerCondition(Obj) {
    Obj.iBJ_ZK = 0;
    Obj.iJLBH = $("#TB_JKJLBH").val();
    Obj.dialogName = "ListZKXX";
    Obj.iBJ_CZK = parseInt(vCZK) || 0;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    if ($("#TB_JKJLBH").val() != "") {
        MakeSrchCondition(arrayObj, "TB_JKJLBH", "iJLBH", "=", false);//iBJ_XTZK
        MakeSrchCondition2(arrayObj, 1, "iBJ_XTZK", "=", false);
    }
    return arrayObj;
}

function MakeSearchJSONZK(listName) {
    if (listName == undefined) { listName = "list"; }
    var cond = MakeSearchCondition();
    if (cond == null)
        return;
    var Obj = new Object();
    Obj.SearchConditions = cond;
    var colModels = "", colNames = "", colWidths = "";
    var cols = $("#" + listName + "").data('datagrid').options.columns[0];
    for (var i = 0; i < cols.length; i++) {
        if (cols[i].field == "sCZKHM" || cols[i].field == "sCDNR") {
            colModels += cols[i].field + "|";
            colNames += cols[i].title + "|";
            colWidths += cols[i].width + "|";
        }
    }
    Obj.sColNames = colNames;
    Obj.sColModels = colModels;
    Obj.sColWidths = colWidths;
    Obj.sSortFiled = $("#" + listName + "").data('datagrid').options.sortName;// cols[0].field;
    Obj.sSortType = $("#" + listName + "").data('datagrid').options.sortOrder;
    Obj.iLoginRYID = iDJR;
    AddCustomerCondition(Obj);

    return Obj;
}

function AddCustomerButton() {
    AddToolButtons("导出", "Export");
    AddToolButtons("保存", "Save");
    AddToolButtons("查部分卡", "CX");

}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = jkjlbh == undefined ? $("#TB_JLBH").val() : jkjlbh;
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYID = 0;
    Obj.iCLLX = "0";
    Obj.iJKJLBH = $("#TB_JKJLBH").val();
    var lstData = new Array();
    lstData.push(rowData);
    Obj.iBJ_CZK = vCZK || 0;
    // lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lstData;
    Obj.sLoginRYMC = sDJRMC;
    Obj.iLoginRYID = iDJR;
    return Obj;
}
function IsValidData() {
    if ($("#TB_JKJLBH").val() == "") {
        art.dialog({ lock: true, content: '请输入正确的建卡记录编号', time: 2 });
        return false;
    }

    return true;
}
function SetControlState() {
    //document.getElementById("Save").disabled = (document.getElementById("B_Save").disabled && !bCanWriteCard);
    document.getElementById("Export").disabled = !bCanWriteCard;
}



function SaveClick() {
    var vMode;
    if (IsValidInputData()) {
        if (vJLBH != "") {
            vMode = "Update";
        }
        else {
            vMode = "Insert";
        }
        if (posttosever(SaveDataBase(), vUrl, vMode) == true) {
            //vProcStatus = cPS_BROWSE;
            //SetControlBaseState();
        }
    }
};

function posttosever(Obj, str_url, str_mode, str_suc, async) {
    PostToServer(Obj, str_url, str_mode, str_suc, async, function (data) {
        SetControlBaseState();
        vJLBH = GetValueRegExp(data, "jlbh");
        var lst = $("#list").datagrid("getData").rows;
        lst[WriteIndex].iBJ_ZK = 1;
        $("#LB_ZKSL").text(parseInt(WriteIndex) + 1);
        $("#list").datagrid("updateRow", lst[WriteIndex]);
        $("#list").datagrid('refreshRow', WriteIndex);
        $('#list').datagrid('selectRow', WriteIndex);//
        WriteIndex = parseInt(WriteIndex) + parseInt(1);
        timeCDNR = setTimeout("WriteCard()", 500);
    });

}


