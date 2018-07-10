vUrl = "../YHQGL.ashx";

var LX = GetUrlParam("lx");

function InitGrid()
{
    vColumnNames = ['销售金额', "抽奖次数", "礼品选择(代码)", '礼品名称'];
    vColumnModel = [
          { name: "fXSJE", width: 100, editor: 'text' },
          { name: "fFQJE", width: 100, editor: 'text' },
          { name: "sLPDM", width: 100, hidden: true },
          { name: "sLPMC", width: 100, hidden: true }
    ];
};

$(document).ready(function ()
{
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#AddItem").click(function ()
    {
        $("#list").datagrid("appendRow", {});
    });

    $("#DelItem").click(function ()
    {
        DeleteRows("list");
    });

});
function SetControlState()
{

}

function IsValidData()
{

    if ($("#TB_FFGZMC").val() == "")
    {
        ShowMessage("请输入规则名称" );
        return false;
    }
    if ($("#TB_CJCSSX").val() == "")
    {
        ShowMessage( "请输入抽奖次数上限" );
        return false;
    }
    if ($("#TB_QDJE").val() == "")
    {
        ShowMessage("请输入起点金额" );
        return false;
    } 
    return true;
}


function SaveData()
{
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    //主表数据
    Obj.sYHQFFGZMC = $("#TB_FFGZMC").val();
    Obj.fFFXE = $("#TB_CJCSSX").val();
    Obj.fFFQDJE = $("#TB_QDJE").val();
    Obj.iLX = 2;
    Obj.iBJ_TY = $("#CB_BJ_TY")[0].checked ? "1" : "0";
    //子表数据  
    //设置子表  
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}
function ShowData(data)
{
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    //主表数据
    $("#TB_FFGZDM").val(Obj.iJLBH);
    $("#TB_FFGZMC").val(Obj.sYHQFFGZMC);
    $("#TB_CJCSSX").val(Obj.fFFXE);
    $("#TB_QDJE").val(Obj.fFFQDJE);
    $("#HF_BJ_TY").val(Obj.iBJ_TY);
    //bj_sf=Obj.iBJ_SF
    $("#CB_BJ_TY").prop("checked", Obj.iBJ_TY == "1" ? true : false);
  
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    //登记人
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);

}

function CheckReapet(value)
{
    var boolReapet = true;
    for (i = 0; i < $("#list").getGridParam("reccount") - 1; i++)
    {
        var RowDate = $("#list").getRowData(i);
        if (RowDate.fXSJE == value)
        {
            boolReapet = false;
        }

    }

    return boolReapet;

}