vUrl = "../MZKGL.ashx";

function InitGrid() {
    vColumnNames = ['卡号','HYID', '售卡编号','面值','BGDDDM'];
    vColumnModel = [
              { name: 'sHYK_NO', hidden: false, },
              { name: 'iHYID', hidden: true, },
               { name: 'iSKJLBH',  },
               { name: 'fCZJE', width: 100, },
               //{
               //    name: "iFP_FLAG", width: 80, formatter: function (cellvalue) {
               //        return cellvalue == "0" ? "✘" : "✔";
               //    }
               //},
              { name: 'sBGDDDM', hidden: true, },



    ];
}
$(document).ready(function () {
 
    $("#jlbh").hide();

    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#TB_SKJLBH").click(function () {    
        var DataArry = new Object();
        DataArry["BJ_TS"] = 1;

        DataArry["iSTATUS"] = 2;
        SelectMZKSKD('TB_SKJLBH', 'HF_SKJLBH', 'zHF_SKJLBH', false, DataArry);
    });



    $("#Dosearch").click(function () {
      
        $('#list').datagrid("loadData", { total: 0, rows: [] });

        if (($("#HF_SKJLBH").val() == "") && ($("#TB_HYKNO1").val()=="" && $("#TB_HYKNO2").val()=="")) {
            ShowMessage("请选择售卡记录编号或输入卡号", 3);
            return;
        }
        if ($("[name='CLLX']:checked").val() == 2) {

            document.getElementById("B_Save").disabled = true;


        }
        if ($("[name='CLLX']:checked").val() == 3) {

            document.getElementById("B_Save").disabled = false;


        }
       CalcKH($("[name='CLLX']:checked").val() ,$("#HF_SKJLBH").val(), $("#TB_HYKNO1").val(), $("#TB_HYKNO2").val(), $("#TB_DJSJ1").val(), $("#TB_DJSJ2").val());


    });


    FillBGDDTree("TreeBGDD", "TB_BGDDMC");



})
function CalcKH(iFP_FLAG,iSKJLBH,sHYK_NO1,sHYK_NO2,dDJSJ1,dDJSJ2) {

   

    var str = GetMZKFP(iFP_FLAG,iSKJLBH, sHYK_NO1, sHYK_NO2, dDJSJ1, dDJSJ2);
    if (str == "null" || str == "") {
        ShowMessage("没有找到卡号", 3);
        return;
    }
    var data = JSON.parse(str);
    for (i = 0; i < data.length; i++) {
        $('#list').datagrid('appendRow', {
            sHYK_NO: data[i].sHYK_NO,
            iHYID: data[i].iHYID,
            iSKJLBH: data[i].iJLBH,
            fCZJE: data[i].fCZJE,
            iFP_FLAG: data[i].iFP_FLAG,
            sBGDDDM: $("#HF_BGDDDM").val(),
        });
      
       
    }





    
}


function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
        case "TreeFXDW": $("#HF_FXDWID").val(treeNode.iFXDWID); break;
        case "TreeHYKTYPE":
            var ids = $("#list").datagrid("getData").rows;
            if (ids.length > 0) {
                var rowdata = $('#list').datagrid('getData').rows[0];
                if (rowdata.iHYKTYPE != treeNode.id) {
                    ShowYesNoMessage("卡类型不一致，是否清空卡号列表？", function () {
                        $('#list').datagrid('loadData', { total: 0, rows: [] });
                        $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
                        hideMenu("menuContentHYKTYPE");
                    });
                }
            }
            else {
                $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
                hideMenu("menuContentHYKTYPE");
            }
            break;
    }
}

function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();

    if ($("[name='CLLX']:checked").val() == 2)
    {

        document.getElementById("B_Save").disabled = true;


    }
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    return Obj;
}


function ShowData(data) {
    //var Obj = JSON.parse(data);
    //$("#TB_JLBH").val(Obj.iJLBH);
  
    //$('#list').datagrid('loadData', Obj.itemTable, "json");
    //$('#list').datagrid("loaded");
}



function IsValidData() {

    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请选择操作地点");
        return;
    }

    return true;
}