vUrl = "../GTPT.ashx";
var sPath1;
var sPath2;
var sPath;
var sPSWD;
var sURL;
var sUSER;
var saveIp;
var imagesFirstName = "";
var vLBColumnNames;
var vLBColumnModel;
function InitGrid() {
    vColumnNames = ['面值金额', '用券须知', '适用范围', '优惠详情', '购买金额', 'iBMQFFGZID', '发放规则', '券大图', '券小图', '品牌名称', '品牌ID', '开抢时间', '用券须知是否添加', '优惠详情是否添加'];
    vColumnModel = [
            { name: 'fMZJE', width: 40, editor: 'text' },
            { name: 'sNAME', hidden: true },
            { name: 'sSYFW', width: 80, },
            { name: 'sSYXQ',  hidden: true },
            { name: 'iGMJE', width: 60, editor: 'text', },
             {
                 name: 'iBMQFFGZID', hidden: true           
             },
             { name: 'sBMQFFGZMC', width: 80, },

          
            { name: 'sIMG', width: 80, },
            { name: 'sLOGO', width: 80, },
            { name: 'sSBMC', width: 80, },
            { name: 'iSBID', hidden: true },
            { name: 'dENDTIME', width: 80 },
             { name: 'sNAMES', width: 30 },
             { name: 'sSYXQS', width: 30 },
    ];
    vLBColumnNames = ['购买金额', '用券须知 ', '适用范围', '优惠详情', 'iBMQFFGZID', '发放规则', '券大图', '券小图', '礼包', '礼包ID', '开抢时间', '分享标题', '分享描述', '分享链接', '分享图片', '用券须知是否添加', '优惠详情是否添加'];
    vLBColumnModel = [
           { name: 'iGMJE', width: 50, editor: 'text' },
           { name: 'sNAME', width: 100, editor: 'text' },
             { name: 'sSYFW', width: 100, },
            { name: 'sSYXQ',  hidden: true },
            { name: 'iBMQFFGZID', hidden: true },
                { name: 'sBMQFFGZMC', width: 80, },

            { name: 'sIMG', width: 80, },
            { name: 'sLOGO', width: 80, },
            { name: 'sLBMC', width: 80, },
            { name: 'iLBID', hidden: true },
           { name: 'dENDTIME_LB', width: 80 },
           { name: 'sTITLE_LB', width: 100, editor: 'text' },
           { name: 'sDESCRIBE_LB', width: 100, editor: 'text' },
           { name: 'sURL_LB', width: 100, editor: 'text' },
           { name: 'sIMG_LB', width: 100, editor: 'text' },
             { name: 'sNAMES', width: 30 },
             { name: 'sSYXQS', width: 30 },

    ];

}



function SetControlState() {
    if ($("#LB_ZZRMC").text() != "") {
        document.getElementById("B_Stop").disabled = true;
    }

    $("#QDR").show();
    $("#QDSJ").show();
    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#B_Start").show();
    $("#B_Stop").show();
    $("#MZ").hide();
    $("#LB").hide();
    $("#aa").hide();
    $("#bb").hide();
    $("#tb").hide();
    $("#tb2").show();
  
}

$(document).ready(function () {
    DrawGrid("list_lb", vLBColumnNames, vLBColumnModel, false);

    BFButtonClick("TB_MDMC", function () {
        SelectWXMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
   
    BFButtonClick("TB_CXZT", function () {
        SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", true);
    });
    BFButtonClick("TB_BMQMC", function () {
        SelectBMQDY("TB_BMQMC", "HF_BMQID", "zHF_BMQID", true);
    });

    BFButtonClick("TB_LBMC", function () {
        SelectBMQLBMC("TB_LBMC", "HF_LBID", "zHF_LBID", true);
    });

    BFButtonClick("TB_SBMC", function () {
        SelectWXSBMC("TB_SBMC", "HF_SBID", "zHF_SBID", true);
    });
    BFButtonClick("TB_BMQFFGZ", function () {
        SelectBMQFFGZ("TB_BMQFFGZ", "HF_BMQFFGZID", "zHF_BMQFFGZID", true);
    });
    $('#upload').click(function () {
        if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files1").val())) {
            ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种")
            return;
        }
        UploadPicture("form1", "TB_IMG", "BMQDT");

    });
    $('#upload_logo').click(function () {
        if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files1").val())) {
            ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种")
            return;
        }
        UploadPicture("form2", "TB_LOGO", "BMQXT");

    });

    $('#upload_FXMZ').click(function () {
        if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files1").val())) {
            ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种")
            return;
        }
        UploadPicture("formMZ", "TB_IMG_MZ", "BMQFXMZ");

    });


    $('#upload_FXLB').click(function () {
        if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files1").val())) {
            ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种")
            return;
        }
        UploadPicture("formLB", "TB_IMG_LB", "BMQFXLB");

    });
    $("#AddItem").click(function () {  
        if ($("#TB_ENDTIME").val() == "") {
            ShowMessage("请选择开抢时间");
            return;
        }
        if (editor1.html() != "") { A = "已添加" }
        else
        { A = "未添加" }
        if (editor2.html() != "") { C = "已添加" }
        else
        { C = "未添加" }

        $('#list').datagrid('appendRow', {
            sIMG: $("#TB_IMG").val(),
            sLOGO: $("#TB_LOGO").val(),
            sSBMC: $("#TB_SBMC").val(),
            iSBID: $("#HF_SBID").val(),
            dENDTIME: $("#TB_ENDTIME").val(),
            sBMQFFGZMC: $("#TB_BMQFFGZ").val(),
            iBMQFFGZID: $("#HF_BMQFFGZID").val(),
            sNAME: editor1.html(),
            sNAMES: A,
            sSYFW: $("#TB_SYFW").val(),
            sSYXQ: editor2.html(),
            sSYXQS: C

        });
    });
    $("#Add").click(function () {
       
        if ($("#HF_LBID").val() == "") {
            ShowMessage("请选择礼包");

            return;
        }
        if ($("#TB_ENDTIME_LB").val() == "") {
            ShowMessage("请选择开抢时间");

            return;
        }

        if (editor3.html() != "") { A = "已添加" }
        else
        { A = "未添加" }
        if (editor4.html() != "") { C = "已添加" }
        else
        { C = "未添加" }

        $('#list_lb').datagrid('appendRow', {
            sIMG: $("#TB_IMG").val(),
            sLOGO: $("#TB_LOGO").val(),
            sLBMC: $("#TB_LBMC").val(),
            iLBID: $("#HF_LBID").val(),
            sBMQFFGZMC: $("#TB_BMQFFGZ").val(),
            iBMQFFGZID: $("#HF_BMQFFGZID").val(),
            dENDTIME_LB: $("#TB_ENDTIME_LB").val(),
            sNAME: editor3.html(),
            sNAMES: A,
            sSYFW: $("#TB_SYFW").val(),
            sSYXQ: editor4.html(),
            sSYXQS: C,
            sTITLE_LB: $("#TB_TITLE_LB").val(),
            sDESCRIBE_LB: $("#TB_DESCRIBE_LB").val(),
            sURL_LB: $("#TB_URL_LB").val(),
            sIMG_LB: $("#TB_IMG_LB").val(),

        }); 
    });
    $("#DelItem").click(function () {
       
        DeleteRows("list");


    });


    $("#Del").click(function () {
        
        DeleteRows("list_lb");


    });

    $("input[type='checkbox'][name='CB_LBBJ']").each(function () {
        $(this).bind("click propertychange", function () {
            var cbinput = $(this);
            if (cbinput.attr("name") == null || cbinput.attr("name") == "") {
                return;
            }
            var hfinput = cbinput.attr("name").replace("CB", "HF");
            if (this.checked) {
                $("#" + hfinput).val(cbinput.val());
                $("input[name=" + cbinput.attr("name") + "]").each(function () {
                    if ($(this).val() != cbinput.val()) {
                        $(this).prop("checked", false);
                    }
                });
            }
            $(this).prop("checked", this.checked);
            if ($("#HF_LBBJ").val() == 1) {

                $("#MZ").show();

                $("#LB").hide();
             
                $("#aa").show();
                $("#tb").show();
                $("#bb").hide();
                $("#tb2").hide();


                KindEditor.ready(function (K) {
                    window.editor = K.create('#TA_YQXZ_MZ');
                });
            }
            if ($("#HF_LBBJ").val() == 2) {
                $("#MZ").hide();
                $("#LB").show();
                $("#aa").hide();
                $("#tb").hide();
                $("#ff").show();
                $("#bb").show();


            }
        });
    });
    $("input[type='checkbox'][name='CB_BUY']").each(function () {
        $(this).bind("click propertychange", function () {
            var cbinput = $(this);
            if (cbinput.attr("name") == null || cbinput.attr("name") == "") {
                return;
            }
            var hfinput = cbinput.attr("name").replace("CB", "HF");
            if (this.checked) {
                $("#" + hfinput).val(cbinput.val());
                $("input[name=" + cbinput.attr("name") + "]").each(function () {
                    if ($(this).val() != cbinput.val()) {
                        $(this).prop("checked", false);
                    }
                });
            }
            $(this).prop("checked", this.checked);
           
            
        });
    });

    
  




});


function IsValidData() {
    if ($("#TB_NAME").val() == "") {
        ShowMessage("请输入活动名称");

        return false;
    }

    if ($("#HF_LBBJ").val() == "") {
        ShowMessage("请选择礼包标记");

        return false;
    }
    if ($("#HF_LBBJ").val() == 1) {
        if ($("#TB_BMQMC").val() == "") {
            ShowMessage("请输入编码券名称");

            return false;
        }
    }
    if ($("#HF_CXID").val() == "") {
        ShowMessage("请选择促销主题");

        return false;
    }
    if ($("#DDL_CHANNELID").val() == "") {
        ShowMessage("请选择渠道");

        return false;
    }
    if ($("#TB_QYXQTS").val() == "") {
        ShowMessage("请输入券有效期天数");

        return false;
    }
    if ($("#TB_KSRQ").val() == "") {
        art.dialog({ lock: true, content: "请输入开始日期" });
        ShowMessage("请选择礼包标记");

        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请输入结束日期");

        return false;
    }
    if ($("#TB_QKSRQ").val() == "") {
        ShowMessage("请输入券开始日期");

        return false;
    }
    if ($("#TB_QJSRQ").val() == "") {
        ShowMessage("请输入券结束日期");

        return false;
    }
    if ($("#list").datagrid("getData").rows.length == 0 && $("#list_lb").datagrid("getData").rows.length) {
        ShowMessage("编码券发放单_面值与编码券发放单_礼包 不能同时为空");

        return false;
        }  

    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sNAME = $("#TB_NAME").val();
    Obj.iBMQID = $("#HF_BMQID").val();
    if (Obj.iBMQID == "")
        Obj.iBMQID = "0";
    Obj.iWX_MDID = $("#HF_MDID").val();

    Obj.iCXID = $("#HF_CXID").val();
    Obj.iFFPT = GetSelectValue("DDL_CHANNELID");
    Obj.iFFPT = 0;
    Obj.iQYXQTS = $("#TB_QYXQTS").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.dQKSRQ = $("#TB_QKSRQ").val();
    Obj.dQJSRQ = $("#TB_QJSRQ").val();
    Obj.iLBBJ = $("#HF_LBBJ").val();
    Obj.iBUY = $("#HF_BUY").val();
    if (Obj.iBUY == "")
        Obj.iBUY = "0";
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.sTITLE_MZ = $("#TB_TITLE_MZ").val();
    Obj.sDESCRIBE_MZ = $("#TB_DESCRIBE_MZ").val();
    Obj.sIMG_MZ = $("#TB_IMG_MZ").val();
    Obj.sURL_MZ = $("#TB_URL_MZ").val();

    var lst_mz = new Array();
    lst_mz = $("#list").datagrid("getRows");
    Obj.MZitemTable = lst_mz;


    var lst_lb = new Array();
    lst_lb = $("#list_lb").datagrid("getRows");
    Obj.LBitemTable = lst_lb; 
    return Obj;

}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_NAME").val(Obj.sNAME);
    $("#HF_BMQID").val(Obj.iBMQID);
    $("#TB_BMQMC").val(Obj.sBMQMC);
    $("#HF_CXID").val(Obj.iCXID);
    $("#TB_CXZT").val(Obj.sCXZT);
    $("#DDL_CHANNELID").val(Obj.iFFPT);
    $("#TB_QYXQTS").val(Obj.iQYXQTS);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_QKSRQ").val(Obj.dQKSRQ);
    $("#TB_QJSRQ").val(Obj.dQJSRQ);

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZSJ);
    $("#HF_MDID").val(Obj.iWX_MDID);
    $("#TB_MDMC").val(Obj.sMDMC);

    $('#list').datagrid('loadData', Obj.MZitemTable, "json");
    $('#list').datagrid("loaded");


    $('#list_lb').datagrid('loadData', Obj.LBitemTable, "json");
    $('#list_lb').datagrid("loaded");


   




    $("input[name='CB_LBBJ']").each(function () {
        if ($(this).val() == Obj.iLBBJ) {
            $(this).prop("checked", "true");
        }
    });

    $("input[name='CB_BUY']").each(function () {
        if ($(this).val() == Obj.iBUY) {
            $(this).prop("checked", "true");
        }
    });

    $("#HF_LBBJ").val(Obj.iLBBJ);
    $("#HF_BUY").val(Obj.iBUY);

    if (Obj.iLBBJ == 1) {
        $("#MZ").show();
        $("#LB").hide();

    }
    if (Obj.iLBBJ == 2) {
        $("#MZ").hide();
        $("#LB").show();

    }





}

function GetBMQFFGZ() {
    var result = "";
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillBMQFFGZ",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'ys' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                result += data[i].iBMQFFGZID + ":" + data[i].sGZMC + ";";
            }
        },
        error: function (data) {
            art.dialog({ content: data.responseText, lock: true, time: 2 });
        }
    });
    return result.substr(0, result.length - 1);
}







