vUrl = "../KFPT.ashx";
function InitGrid() {
    vColumnNames = ["序号", "会员卡号", "iHYID", "会员姓名", "iSEX", "性别", "联系方式", "iKFRYID"];
    vColumnModel = [
          { name: "iXH", width: 70, },
          { name: "sHYK_NO", width: 70, },
          { name: "iHYID", hidden: true, },
          { name: "sHY_NAME", width: 70, },
          { name: "iSEX", hidden: true, },
          { name: "sSEX", width: 70, },
          { name: "sSJHM", width: 70, },  
          { name: "iKFRYID", hidden: true, },
    ];

    vColumnNames_RW = ["RYID", "客服经理", "iJLBH_RW"];
    vColumnModel_RW = [
          { name: "iRWDX", hidden: true, },
          { name: "sPERSON_NAME", width: 70, },
          { name: "iJLBH_RW", hidden: true, },
    ];
}

$(document).ready(function () {
    DrawGrid("list_rw", vColumnNames_RW, vColumnModel_RW);
    $("#B_Exec").hide();
    $("#AddKFZ").hide();
    $("#ZXR").hide();
    $("#djr1").html("发布人");
    $("#TB_PERSON_NAME").click(function () {
        SelectKFJL("TB_PERSON_NAME","HF_RYID","zHF_RYID",false);  //客服经理选择   
    });

    $("#AddKFJL").click(function () {
        if ($("#TB_PERSON_NAME").val() == "") {
            ShowMessage("请选择客服经理");
            return;
        }
        var rows = $("#list_rw").datagrid("getRows");
        for (i = 0; i < rows.length ; i++) {
            if (rows[i].iRWDX == $("#HF_RYID").val()) {
                ShowMessage("不能重复添加");
                return;
            }
        }
        $('#list_rw').datagrid('appendRow', {
            iRWDX: $("#HF_RYID").val(),
            sPERSON_NAME: $("#TB_PERSON_NAME").val(),
            iJLBH_RW: 0,
        });

    });

    $("#DelKFJL").click(function () {
        DeleteRows("list_rw");
    });


    $("#AddItem").click(function () {
        var DataArry = new Object();
        SelectHYK('list', DataArry, 'iHYID');
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });

});

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    for (var i = 0; i < lst.length; i++) {
        var ids = $("#list").datagrid("getRows").length;
        if (CheckReapet(array, CheckFieldId, lst[i])) {
            $('#list').datagrid('appendRow', {
                iXH: ids + 1,    
                sHYK_NO: lst[i].sHYK_NO,
                iHYID: lst[i].iHYID,
                sHY_NAME: lst[i].sHY_NAME,
                sSJHM: lst[i].sSJHM,
                iSEX: lst[i].iSEX,
                sSEX: lst[i].iSEX == 0 ? "男" : "女",
                iKFRYID: lst[i].iKFRYID,
            });
        }
    }

}
function IsValidData() {
    if ($("#TB_RWZT").val() == "") {
        ShowMessage("请输入任务主题");
        return false;
    }
    if ($("#TB_KSRQ").val() == "") {
        ShowMessage("请输入开始日期");
        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请输入结束日期");
        return false;
    }
    if ($("#TB_RW").val() == "") {
        ShowMessage("请输入任务内容");
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sRWZT = $("#TB_RWZT").val();
    Obj.sRW = $("#TB_RW").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();

    Obj.iDJR = iDJR;
    Obj.sDJRMC = sDJRMC;

    var lst_rw = new Array();
    lst_rw = $("#list_rw").datagrid("getRows");
    Obj.rwqfjlItem = lst_rw;

    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.rwjl_hyItem = lst;


    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_RWZT").val(Obj.sRWZT);
    $("#TB_RW").val(Obj.sRW);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#LB_DJSJ").text(Obj.dDJSJ);

    $('#list_rw').datagrid('loadData', Obj.rwqfjlItem, "json");
    $('#list_rw').datagrid("loaded");
    $('#list').datagrid('loadData', Obj.rwjl_hyItem, "json");
    $('#list').datagrid("loaded");

}

function IsValidInputData() {
    try {
        if (typeof (eval("InitGrid")) == "function") {
            var rows = $("#list_rw").datagrid("getData").rows;
            if (bNeedItemData && rows.length <= 0) {
                ShowMessage("子表没有数据，请添加!", 3);
                return false;
            }
            for (var i = 0; i < rows.length; i++) {
                $("#list_rw").datagrid("endEdit", i);
            }
            if ($(".datagrid-editable-input").length > 0) {
                ShowMessage("子表数据正在编辑中，请保存!", 3);
                return false;
            }

        }

    }
    catch (ex) { };
    if (!IsValidData())
        return false;
    return true;
}


