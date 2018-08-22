
vUrl = "../HYKGL.ashx";
var vCaption = "会员卡退卡";
var HYKNO = GetUrlParam("HYKNO");
function InitGrid() {
    vColumnNames = ["HYID", "卡号", "姓名", "办卡日期", "iHYKTYPE", "卡类型", "主卡标记", "主卡标记", "账户余额", "卡费金额", "已用卡费金额", "退回卡费金额", "已用天数"],
    vColumnModel = [
          { name: "iHYID", hidden: true },
          { name: "sHYK_NO", width: 150 },
          { name: "sHY_NAME", width: 60 },
          { name: "dBKRQ", width: 150 },
          { name: "iHYKTYPE", hidden: true },
          { name: "sHYKNAME", width: 60 },
          { name: "iBJ_CHILD", hidden: true },
          { name: "sBJ_CHILD", width: 60 },
          { name: "fCZJE", width: 60 },
          { name: "fKFJE", width: 60 },
          { name: "fKFJE_Y", width: 60 },
          { name: "fKFJE_T", width: 60 },
          { name: "iYYTS", width: 60 },
    ];
};

function getHYXXList(sHYK_NO) {

    if (sHYK_NO != "") {
        var str = GetHYXXData(0, sHYK_NO);
        if (str == null) {
            showMessage("没有找到卡号", 3);
        }
        else {
            var Obj = JSON.parse(str);
            if (Obj.sHYK_NO == "") {
                showMessage("没有找到卡号", 3);
                return;
            }
            if (Obj.iBJ_TK != 1) {
                showMessage("该卡类型不允许退卡", 3);
                return;
            }

            if (Obj.iSTATUS < 0) {
                ShowMessage("不是有有效卡，不可办理退卡", 3);
                PageDate_Clear()
                return;
            }

            if (dateDiff(Obj.dDJSJ) == 0) {
                ShowMessage("办卡当天，不可办理退卡", 3);
                $("#TB_HYKNO").val("");
                PageDate_Clear()
                return;
            }
            ShowHYData(Obj);
        }
    }
    else {
        showMessage("会员卡号不能为空", 3);
    }


}

function ShowHYData(Obj) {
    var listData = new Array();
    var tp_yyts = 0;
    var tp_zkfje = 0;
    var tp_zyykfje = 0;
    var tp_zthkfje = 0;
    var tp_zye = 0;
    $("#TB_HYKNO").val(Obj.sHYK_NO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#HF_YHQYE").val(Obj.fYHQJE);
    $("#HF_JF").val(Obj.fWCLJF);
    var newData = new Object();
    newData.iHYID = Obj.iHYID;
    newData.sHYK_NO = Obj.sHYK_NO;
    newData.sHY_NAME = Obj.sHY_NAME;
    newData.dBKRQ = Obj.dDJSJ;
    newData.iHYKTYPE = Obj.iHYKTYPE;
    newData.sHYKNAME = Obj.sHYKNAME;
    newData.fKFJE = Obj.fKFJE;
    newData.iBJ_CHILD = Obj.iBJ_CHILD;
    newData.fCZJE = Obj.fCZJE;
    newData.sBJ_CHILD = Obj.iBJ_CHILD == 0 ? "主卡" : "从卡";
    newData.iYYTS = dateDiff(Obj.dXKFRQ);
    newData.fKFJE_Y = parseFloat(((parseFloat(newData.fKFJE) / 365) * parseInt(newData.iYYTS)).toFixed(2));
    newData.fKFJE_T = parseFloat(newData.fKFJE) - Math.floor(parseFloat(newData.fKFJE_Y), 0);
    tp_yyts = parseInt(tp_yyts) + parseInt(newData.iYYTS);
    tp_zkfje = parseInt(tp_zkfje) + parseInt(newData.fKFJE);
    tp_zyykfje = parseInt(tp_zyykfje) + parseInt(newData.fKFJE_Y);
    tp_zthkfje = parseInt(tp_zthkfje) + parseInt(newData.fKFJE_T);
    tp_zye = parseFloat(tp_zye) + parseFloat(newData.fCZJE);
    listData.push(newData);
    if (Obj.SubCardTable.length > 0) {
        for (var i = 0; i < Obj.SubCardTable.length; i++) {
            var subcardItem = new Object();
            subcardItem = Obj.SubCardTable[i];
            if (subcardItem.iBJ_CHILD == 1 && subcardItem.iSTATUS >= 0) {
                subcardItem.dBKRQ = subcardItem.dDJSJ;
                subcardItem.iYYTS = dateDiff(subcardItem.dXKFRQ);
                subcardItem.fKFJE_Y = parseFloat(((parseFloat(subcardItem.fKFJE) / 365) * parseInt(subcardItem.iYYTS)).toFixed(2));
                subcardItem.fKFJE_T = parseFloat(subcardItem.fKFJE) - Math.floor(parseFloat(subcardItem.fKFJE_Y), 0);
                subcardItem.sBJ_CHILD = subcardItem.iBJ_CHILD == 0 ? "主卡" : "从卡";
                tp_zye = parseFloat(tp_zye) + parseFloat(subcardItem.fCZJE);
                tp_yyts = parseInt(tp_yyts) + parseInt(subcardItem.iYYTS);
                tp_zkfje = parseInt(tp_zkfje) + parseInt(subcardItem.fKFJE);
                tp_zyykfje = parseInt(tp_zyykfje) + parseInt(subcardItem.fKFJE_Y);
                tp_zthkfje = parseInt(tp_zthkfje) + parseInt(subcardItem.fKFJE_T);
                listData.push(subcardItem);
            }
        }
    }
    $('#list').datagrid('loadData', listData, "json");
    $('#list').datagrid("loaded");
    $("#TB_YYTS").val(tp_yyts);
    $("#TB_YYKFJE").val(tp_zyykfje);
    $("#TB_THKFJE").val(tp_zthkfje);
    $("#TB_KFJE").val(tp_zkfje);
    $("#TB_THYE").val(tp_zye);
}


$(document).ready(function () {

    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    $("#TB_HYKNO").bind('keypress', function (event) {
        if (event.keyCode == 13) {
            getHYXXList($("#TB_HYKNO").val());
        }
    });
    if (HYKNO != "") {
        getHYXXList(HYKNO);
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }
});


function dateDiff(sDate2) {
    var currentTime = Date.now();
    var targetTime = new Date(sDate2);
    targetTime.setDate(targetTime.getDate());
    targetTime = targetTime.getTime();
    var offsetTime = targetTime - currentTime;
    offsetTime = Math.abs(offsetTime);
    var offsetDays = Math.floor(offsetTime / (3600 * 24 * 1e3));
    return offsetDays;
}


function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
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
        if (parseFloat($("#HF_JF").val()) > 0 || parseFloat($("#HF_YHQYE").val()) > 0) {
            ShowYesNoMessage("有未使用的优惠券和积分，是否退卡？", function () {
                if (posttosever(SaveDataBase(), vUrl, vMode) == true) {
                    vProcStatus = cPS_BROWSE;
                    SetControlBaseState();
                }
            });
        }
        else {
            if (posttosever(SaveDataBase(), vUrl, vMode) == true) {
                vProcStatus = cPS_BROWSE;
                SetControlBaseState();
            }
        }
    }
};




function IsValidData() {
    var rows = $("#list").datagrid("getData").rows.length;
    if (rows < 1) {
        ShowMessage("没有选择可操作的卡", 3);
        return false;
    }
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请填写操作地点!", 3);
        return false;
    }
    if ($("#HF_HYID").val() == "") {
        ShowMessage("请输入会员卡号!", 3);
        return false;
    }
    return true;

}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.fKFJE = $("#TB_KFJE").val(),
    Obj.fKFJE_Y = $("#TB_YYKFJE").val(),
    Obj.fKFJE_T = $("#TB_THKFJE").val();
    Obj.iYYTS = $("#TB_YYTS").val();
    Obj.fCZJE = $("#TB_THYE").val();
    Obj.sZY = $("#TB_ZY").val();
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
    $("#TB_KFJE").val(Obj.fKFJE),
    $("#TB_YYKFJE").val(Obj.fKFJE_Y),
    $("#TB_THKFJE").val(Obj.fKFJE_T);
    $("#TB_YYTS").val(Obj.iYYTS);
    $("#TB_THYE").val(Obj.fCZJE);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#TB_HYKNO").val(Obj.itemTable[0].sHYK_NO);
    $("#HF_HYID").val(Obj.itemTable[0].iHYID);
    $("#HF_HYKTYPE").val(Obj.itemTable[0].iHYKTYPE);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
}

function clearDataGrid() {
    $('#list').datagrid('loadData', { total: 0, rows: [] });
}

function InsertClickCustom() {
    window.setTimeout(function () {
        clearDataGrid();
    }, 1);

};
