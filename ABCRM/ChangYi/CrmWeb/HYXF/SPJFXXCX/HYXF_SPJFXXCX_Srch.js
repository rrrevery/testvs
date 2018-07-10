vUrl = "../HYXF.ashx";

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID");
    })

    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });


    $("#TB_SHBMMC").click(function () {
       if ($("#HF_SHDM").val() == "") {
           art.dialog({ lock: true, content: "请选择商户！" });
            return;
       }
       else {
          SelectSHBM("TB_SHBMMC", "HF_SHBMDM", "zHF_SHBMDM", $("#HF_SHDM").val(), "");
      }
   });



    $("#TB_HYKNAME").click(function () {

        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", true);
    });

  
  


   // ZSel_MoreCondition_Load(v_ZSel_rownum);
});

function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function getSPXX() {
    sjson = "{'sSHDM':'" + $("#HF_SHDM").val() + "','sSPDM':'" + $("#TB_SPDM").val() + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: vUrl + "?mode=Search&SearchMode=1&func=" + vPageMsgID,
        dataType: "json",
        async: false,
        data: { conditionData: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
            var obj = JSON.parse(result);
            if (obj.sSPMC == "") {
                art.dialog({ lock: true, content: "没有符合的商品信息" });
                return;
            }
            else {
                $("#TB_SPMC").val(obj.sSPMC);
                $("#TB_SPPP").val(obj.sSPSBMC);
                $("#TB_SPFL").val(obj.sSPFLMC);
                $("#TB_HTH").val(obj.sHTH);
                $("#TB_GHSMC").val(obj.sGHSMC);
            }
        },
        error: function (data) {
            result = "";
        }
    }
    )
}




function DoSearch() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    if ($("#HF_SHBMDM").val = ""||$("#HF_HYKTYPE").val() == "") {
        art.dialog({ lock: true, content: "请选择内容" });
        return;
    }
    else {
        MakeSrchCondition(arrayObj, "HF_SHBMDM", "sSHBMDM", "=", true);
        MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "=", false);
        getJFXX(arrayObj);
    }
 
   //MakeMoreSrchCondition(arrayObj);
   
};

function getJFXX(arrayObj)
{
    $.ajax({
        type: 'post',
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        dataType: "json",
        async: false,
        data: { afterFirst: JSON.stringify(arrayObj), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
            var Obj = JSON.parse(result);
            if (Obj.iZXJLBH_PT != 0) {
                $("#TB_FZ").val(Obj.fFZ);
                $("#TB_BS").val(Obj.fBS);
                $("#TB_DJBH").val(Obj.iZXJLBH_PT);
                $("#TB_FDH").val("分单" + Obj.iINX);
                $("#TB_MZTJ").val("规则" + Obj.iGZBH);
                if (Obj.iBJ_CJ == 1) {
                    $("#TB_DQJFTJ").val("参加活动");
                }
            }
            else {
                art.dialog({ lock: true, content: "没有符合条件的单据" });
                return;
            }
        },
        error: function (data) {
            result = "";
        }
    });
}