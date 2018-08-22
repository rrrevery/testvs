vUrl = "../GTPT.ashx";
var irow = 0;
vDBName = "CRMDB";
var HYK_NO = GetUrlParam("HYK_NO");
//var HYID = GetUrlParam("HYID");
var YXLX = [{ "value": "0", "text": "促销游戏" }, { "value": "1", "text": "开卡绑卡送礼品" }, { "value": "2", "text": "微调查送礼品" }];
//var iPUBLICID = 1;
function InitGrid() {
    vColumnNames = ["发放记录编号", "级次", "礼品级次", "礼品名称", "LPID", "LBID", "游戏类型"];
    vColumnModel = [
          { name: "iFFJLBH", width: 100, },
          { name: "iJC", hidden: true },
          { name: "sJCMC", width: 100 },
          { name: "sLPMC", width: 100 },
          { name: "iLPID", hidden: true },
          { name: "iLBID", hidden: true },
          {
              name: 'iTYPE', width: 150, formatter: function (value, row) {
                  for (var i = 0; i < YXLX.length; i++) {

                      if (YXLX[i].value == value) {

                          return YXLX[i].text;

                      }

                  }
              },
              editor: {
                   type: 'combobox',
                   options: {
                       data: YXLX,
                       valueField: "value",
                       textField: "text",
                       editable: false,
                       required: true
                   }
               },
          }
    ];
}

$(document).ready(function () {

    if (HYK_NO != "") {
        $("#TB_WX_NO").val(HYK_NO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYK_NO").attr("readonly", "readonly");
    }

    $("#TB_HYK_NO").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            GetHYXX();
        }
    });

    $("#TB_HYK_NO").change(function () {
        GetHYXX();
    });

    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");


    $("#AddItem").click(function () {
        if ($("#TB_HYK_NO").val() == "") {
            ShowMessage("请填写会员卡号");
            return;
        }
        if ($("#TB_BGDDMC").val() == "") {
            ShowMessage("请选择操作地点");
            return false;
        }
        var DataArry = new Object();
        DataArry["iHYID"] = $("#HF_HYID").val();
        DataArry["iPUBLICID"] = iWXPID;
        SelectXZLP('list', DataArry, 'iLPID');
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });
});
function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}



function IsValidData() {
    if ($("#TB_HYK_NO").val() == "") {
        ShowMessage("请先输入会员卡号！");
        return false;
    }
    if ($("#TB_BGDDMC").val() == "") {
        ShowMessage("请选择操作地点！");
        return false;
    }
    if ($("#list").datagrid("getData").rows == 0) {
        ShowMessage('请添加子表数据');
        return false;
    }
    return true;
}

function SaveData() {

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYK_NO = $("#TB_HYK_NO").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sBZ = $("#TB_BZ").val();
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
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_HYK_NO").val(Obj.sHYK_NO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#TB_BZ").val(Obj.sBZ);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)


}
function GetHYXX() {
    if ($("#TB_HYK_NO").val() != "") {
        var str = GetHYKNOData(0, $("#TB_HYK_NO").val());
        $("#HF_HYID").val(0);
        if (str == "null" || str == "") {
            ShowMessage("没有找到会员号");
            ClearData();
            return false;
        }
        if (str.indexOf("错误") >= 0) {
            ShowMessage(str);
            return;
        }
        var Obj = JSON.parse(str);
        $("#HF_HYID").val(Obj.iHYID);
    }
}
function ClearData() {
    $("#TB_HYK_NO").val("");

}
