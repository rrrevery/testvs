vUrl = "../GTPT.ashx";
var wxusers = new Array();

function InitGrid() {
    vColumnNames = ['OPENID', '卡类型', "卡号", '性别', '原分组', 'groupid'];
    vColumnModel = [
          { name: "sOPENID", hidden: true },
          { name: "sHYKNAME", width: 150, },
          { name: "sHYKNO", width: 150, },
          {
              name: "iSEX", width: 150, formatter: function (cellvalue, icol) {
                  switch (cellvalue) {
                      case "1":
                          return "男";
                          break;
                      case "2":
                          return "女";
                          break;
                      default:
                          return "未知"
                          break;
                  }
              }
          },
          { name: "sGROUP_NAME", width: 150, },
          { name: "iGROUPID", width: 150, hidden: true }
    ];
}

$(document).ready(function () {

    BFButtonClick("TB_GRPMC", function () {
        SelectWXGroup("TB_GRPMC", "HF_GRPID", "zHF_GRPID", true);
    });
    //添加微信用户
    $("#AddItem").click(function () {
        var DataArry = new Object();
        SelectWXUSER('list', DataArry, 'sOPENID');       
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });
});

function SetControlState() {

}


function IsValidData() {

    return true;
}



function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    //主表数据
    Obj.sGROUPJL_NAME = $("#TB_GROUPJL_NAME").val();
    Obj.iGROUPID = $("#HF_GROUPID").val();;//解决disable状态下，值不可取
    Obj.sZY = $("#TB_ZY").val();

    Obj.sDJRMC = $("#LB_DJRMC").text();
    Obj.iDJR = $("#HF_DJR").val();
    Obj.dDJRQ = $("#LB_DJSJ").text();

    //Obj.sZXRMC = $("#LB_ZXRMC").text();
    //Obj.iZXR = $("#HF_ZXR").val();
    //Obj.dZXRQ = $("#LB_ZXRQ").text();
    ////子表数据
    //var ItemTable = new Array();

    //var ids = $("#list").getDataIDs();

    ////卡数据（同样在子表当中）
    //for (i = 0; i < ids.length ; i++) {
    //    var rowData = $("#list").getRowData(ids[i]);
    //    var Item_lst = new Object();
    //    Item_lst = rowData;
    //    Item_lst.sOPENID = rowData.sOPENID;
    //    Item_lst.sINFO = rowData.sOPENID;
    //    ItemTable.push(Item_lst);
    //}

    //设置子表
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
    $("#TB_GROUPJL_NAME").val(Obj.sGROUPJL_NAME);
    $("#HF_GROUPID").val(Obj.iGROUPID);
    $("#S_GROUP_NAME").val(Obj.iGROUPID);
    $("#TB_ZY").val(Obj.sZY);


    ////修改人
    //$("#LB_XGRMC").text(Obj.sXGRMC);
    //$("#LB_XGRQ").text(Obj.dXGRQ);
    ////停用人
    //$("#LB_ZZRMC").text(Obj.sZZRMC);
    //$("#HF_ZZR").val(Obj.iZZR);
    //$("#LB_ZZRQ").text(Obj.dZZRQ);
    //登记人
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);

    //审核人
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

    //子表数据绑定


    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");


}

//jQgird初始化
//function InitGrid(tablename, colnames, colmodels, pager) {
//    jQuery("#" + tablename).jqGrid({
//        async: false,
//        datatype: "local",
//        colNames: colnames,
//        colModel: colmodels,
//        cellsubmit: "clientArray",
//        rownumbers: true,
//        width: 800,
//        pager: "#" + pager,
//        viewrecords: true,
//        rowNum: 100,
//        afterSaveCell: function (rowid, name, val, iRow, iCol) {
//        }
//    });
//}

//打开弹出窗口，并绑定返回的数据
//function WUCDialogToTable(url, widthpx, heightpx, returndata, tablename) {
//    art.dialog.open(url, {
//        lock: true,
//        width: widthpx,
//        height: heightpx,
//        close: function () {
//            //当关闭窗口时，接收返回的数据
//            var returnValues = $.dialog.data(returndata);//接收的应该是转换成对象，或者数组
//            var array = new Array();

//            //有数据返回
//            if (returnValues != "" && returnValues != undefined) {
//                array = JSON.parse(returnValues);//返回的数据需要符合JSON字符串格式，才能进行转换成数组，或者对象        
//                wxusers = array;
//            }
//            $("#" + tablename).clearGridData();
//            //添加到表中,暂时只显示50条
//            for (var i = 0; i <= ((wxusers.length > 50) ? 50 : wxusers.length) ; i++) {
//                $("#" + tablename).addRowData(i, wxusers[i]);
//            }
//            $("#pager_right>div").html("最多只显示50位用户    总共" + wxusers.length + " 位")
//            $.dialog.data(returndata, "");//清空数据

//        }
//    });
//}

//function posttosever(str_data, str_url, str_mode) {
//    var result = "";
//    if (str_mode == "Exec") {
//        //首先反写这些数据到微信  {"openid":"oDF3iYx0ro3_7jD4HFRDfrjdCM58","to_groupid":108}
//        for (var i = 0; i < wxusers.length; i++) {
//            var obj = new Object();
//            obj.openid = wxusers[i].sOPENID;
//            obj.to_groupid = $("#HF_GROUPID").val();
//            obj.mode = "update";
//            obj.type = "post";
//            $.ajax({
//                url: "../GTPT_WX.ashx?requestType=usergroup",
//                data: { postData: JSON.stringify(obj) },
//                async: false,
//                error: function (data) {
//                    result = "fail";
//                    art.dialog({ content: data, lock: true });

//                }
//            });
//        }

//    }
//    if (result == "fail") {
//        return;
//    }


//    var canBeClose = false;
//    var myDialog = art.dialog({
//        lock: true, content: '正在提交数据,请等待......'
//        , close: function () {
//            if (canBeClose) {
//                return true;
//            }
//            return false;
//        }
//    });

//    $.ajax({
//        type: "post",
//        url: str_url + "?mode=" + str_mode + "&func=" + vPageMsgID + "&old=" + vOLDDB,
//        async: false,
//        data: { json: JSON.stringify(str_data), titles: 'cecece' },
//        success: function (data) {
//            canBeClose = true;
//            myDialog.close();
//            if (data.indexOf("错误") >= 0 || data.indexOf("error") >= 0) {
//                art.dialog({ lock: true, content: data });
//                canBeClose = false;
//            }
//            else {
//                art.dialog({ lock: true, content: "操作成功(2秒后自动关闭)", time: 2 });
//                canBeClose = true;
//                vJLBH = GetValueRegExp(data, "jlbh")
//                if (vJLBH != "")
//                    ShowDataBase(vJLBH);
//            }
//        },
//        error: function (data) {
//            canBeClose = false;
//            myDialog.close();
//            art.dialog({ lock: true, content: "保存失败：" + data });
//        }
//    });
//    return canBeClose;
//}





