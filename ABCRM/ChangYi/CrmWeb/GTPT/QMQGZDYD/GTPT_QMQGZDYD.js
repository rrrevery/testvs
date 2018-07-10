vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ['门店名称', 'MDID', ];
    vColumnModel = [
             { name: 'sMDMC', width: 100, },
             { name: 'iMDID', hidden: true, },

    ];
    vYHQColumnNames = ['优惠券ID','优惠券', '介绍名称', '开抢时间', '支付金额(元)', '购买金额(元)', '限制总张数', '限制总张数提示', '每日限制张数', '每日限制张数提示', '单人限制张数', '单人限制张数提示','优惠券结束日期', '图片', '活动介绍', '规则说明', '分享标题', '分享描述', '分享链接', '分享图片'];
    vYHQColumnModel = [              
          { name: 'iYHQID', hidden: true },
          { name: 'sYHQMC', width: 100 },
          { name: 'sJSMC', editor: 'text', width: 120 },
          { name: 'dENDTIME', width: 100 },
          { name: 'iPAYMONEY', width: 80, editor: 'text' },
          { name: 'fYHQJE', width: 80, editor: 'text' },
          { name: 'iLIMIT', width: 80, editor: 'text' },
          { name: 'sLIMITCONTENT', width: 120, editor: 'text' },
       { name: 'iLIMIT_DAY', editor: 'text', width: 80, },
       { name: 'sLIMITCONTENT_DAY',editor: 'text',  },
       { name: 'iLIMIT_HY', editor: 'text', width: 80, },
       { name: 'sLIMITCONTENT_HY', editor: 'text', },

          {
              name: 'dJSRQ', width: 100, editor: 'text', editoptions: {
                  dataInit: function (el) {
                      $(el).click(function () {
                          WdatePicker();
                      });
                  }
              }
          },

          { name: 'sIMG', width: 120 },
          { name: 'sHDJS', hidden: true },
          { name: 'sGZSM', hidden: true },
         { name: 'sTITLE',  editor: 'text' },
        { name: 'sDESCRIBE', editor: 'text' },
        { name: 'sURL',  editor: 'text' },
         { name: 'sIMG2',  editor: 'text' },
        

    ];
    vKLXColumnNames = ['iHYKTYPE', '卡类型', 'iYHQID', '优惠券名称', '总限制张数', '总限制张数提示'];
    vKLXColumnModel = [
         { name: 'iHYKTYPE', hidden: true },
         { name: 'sHYKNAME', width: 150 },
           { name: 'iYHQID', hidden: true },
          { name: 'sYHQMC', width: 120 },
          { name: 'iLIMIT_HYKTYPE', width: 100, editor: 'text' },
         { name: 'sLIMITCONTENT_HYKTYPE', width: 150, editor: 'text' },

    ];

}



function SetControlState() {
    $("#ZZR").show();//登记人名称
    $("#ZZSJ").show();//登记时间
    $("#QDR").show();//启动人名称
    $("#QDSJ").show();//启动时间
    $("#B_Start").show();//启动
    $("#B_Stop").show();//终止
}

function IsValidData() {


    if ($("#TB_KSRQ").val() == "") {
        ShowMessage("请输入开始日期");
        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请选择结束日期");
        return false;
    }
    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        ShowMessage("开始日期不得大于结束日期");
        return false;
    }
  


     
         

        

       

    return true;
}



$(document).ready(function () {

    DrawGrid("listYHQ", vYHQColumnNames, vYHQColumnModel);
    DrawGrid("listKLX", vKLXColumnNames, vKLXColumnModel);

    BFButtonClick("TB_MDMC", function () {
        SelectWXMD("TB_MDMC", "HF_MDID", "zHF_MDID", true, iWXPID);
    });
  
 
   

    $("#AddItem").click(function () {//添加卡类型  
        var DataArry = new Object();
        var checkRepeatField = ["iYHQID","iHYKTYPE"];

        rowDataYHQ = $("#listYHQ").datagrid("getSelections");
        if (rowDataYHQ.length == 0) {
            ShowMessage("还未选中要添加的优惠券", 3);
            return;
        }


        SelectKLXList('listKLX', DataArry, checkRepeatField, false);
    });


    $("#DelItem").click(function () {
        DeleteRows("listKLX");

    });

    //选择门店
    $("#md_Add").click(function () {
     
        //var rows = $('#list').datagrid('getSelections');
        //if (rows.length <= 0) {
        //    ShowMessage("还未选中要添加礼品卡类型");
        //    return;
        //}
        var DataArry = new Object();
        var checkRepeatField = [ "iMDID"];

        SelectWXMDList('list', DataArry, checkRepeatField, false);


       
    
    });
    $("#md_Del").click(function () {
        DeleteRows("list2");



    });

    //添加优惠券，要先选择门店
    $("#Add_yhq").click(function () {
        //var rows = $('#list2').datagrid('getSelections');
        //if (rows.length <= 0) {
        //    ShowMessage("还未选中要添加门店");
        //    return;
        //}
        var DataArry = new Object();
        var checkRepeatField = ["iYHQID"];

        SelectYHQList('listYHQ', DataArry, checkRepeatField, false);

      

    });


    $("#Del_yhq").click(function () {
        DeleteRows("listYHQ");

    });

  

   


})
function StartClick() {
    art.dialog({
        title: "启动",
        lock: true,
        content: "启动本单执行将会终止正在启动的单据，是否覆盖继续？",
        ok: function () {
            if (posttosever(SaveDataBase(), vUrl, "Start") == true) {
                vProcStatus = cPS_BROWSE;
                ShowDataBase(vJLBH);
                SetControlBaseState();
            }
        },
        okVal: '是',
        cancelVal: '否',
        cancel: true
    });
};
function SaveData() {

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iMDID = $("#HF_MDID").val();



    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;//门店


    var lst1 = new Array();
    lst1 = $("#listKLX").datagrid("getData").rows;
    Obj.itemTable1 = lst1;//卡类型

  

    var lst2 = new Array();
    lst2 = $("#listYHQ").datagrid("getData").rows;
    Obj.itemTable2 = lst2;//优惠券


    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}


function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");


    $('#listKLX').datagrid('loadData', Obj.itemTable1, "json");
    $('#listKLX').datagrid("loaded");


    $('#listYHQ').datagrid('loadData', Obj.itemTable2, "json");
    $('#listYHQ').datagrid("loaded");

    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);

}
function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    if (listName == "listKLX") {//卡类型
    

        var rows = $('#listYHQ').datagrid('getSelections');
        for (var j = 0; j < rows.length; j++) {
         
            for (var i = 0; i < lst.length; i++) {
                lst[i].iYHQID = rows[j].iYHQID;
                lst[i].sYHQMC = rows[j].sYHQMC;
                if (CheckReapet(array, CheckFieldId, lst[i])) {
                    $('#listKLX').datagrid('appendRow', {
                        iHYKTYPE: lst[i].iHYKTYPE,
                        sHYKNAME: lst[i].sHYKNAME,
                        iYHQID: lst[i].iYHQID,
                        sYHQMC: lst[i].sYHQMC,
                    });
                }


            }




        }






     
    }
    if (listName == "list") {//门店
     

        for (var i = 0; i < lst.length; i++) {
          
            
            if (CheckReapet(array, CheckFieldId, lst[i])) {
                $('#list').datagrid('appendRow', {
                    iMDID: lst[i].iMDID,
                    sMDMC: lst[i].sMDMC,
                        
                });
            }
          
        
        }

    
    }
    if (listName == "listYHQ") {//优惠券
        for (var i = 0; i < lst.length; i++) {
                if (CheckReapet(array, CheckFieldId, lst[i])) {
                    $('#listYHQ').datagrid('appendRow', {
                        iYHQID: lst[i].iYHQID,
                        sYHQMC: lst[i].sYHQMC,
                        sIMG: $("#TB_IMG").val(),
                        sHDJS: editor.html(),
                        sGZSM: editor2.html(),
                        sJSMC: $("#TB_JSMC").val(),
                        dENDTIME: $("#TB_ENDTIME").val(),
                        sSHOWNAME: $("#TB_SHOWNAME").val(),
                        iPAYMONEY: $("#TB_PAYMONEY").val(),
                        fYHQJE: $("#TB_YHQJE").val(),
                        iLIMIT: $("#TB_LIMIT").val(),
                        sLIMITCONTENT: $("#TB_LIMITCONTENT").val(),

                        iLIMIT_DAY: $("#TB_LIMIT_DAY").val(),
                        sLIMITCONTENT_DAY: $("#TB_LIMITCONTENT_DAY").val(),

                        iLIMIT_HY: $("#TB_LIMIT_HY").val(),
                        sLIMITCONTENT_HY: $("#TB_LIMITCONTENT_HY").val(),
                        dJSRQ: $("#TB_YHQJSRQ").val(),
                        sTITLE: $("#TB_TITLE_LB").val(),
                        sDESCRIBE: $("#TB_DESCRIBE_LB").val(),
                        sURL: $("#TB_URL_LB").val(),
                        sIMG2: $("#TB_IMG_LB").val(),




                    });
                }


            

        }
    }
}
