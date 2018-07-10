vUrl = "../../GTPT/GTPT.ashx";
vCaption = "微信标签";
var vDialogName = "ListWXBQ";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);

var iWXPID = GetUrlParam("wxpid");
var sWXPIF = GetUrlParam("wxpif");


function InitGrid() {
    vColumns = [
        { field: 'iTAGID', title: '标签ID', width: 60 },
        { field: "sBQMC", title: '标签名称', width: 100 },
       { field: "iCOUNT", title: '组内人数', width: 60 },

    ];
    vIdField = "iTAGID";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_BQMC", "sBQMC", "like", true);
    return arrayObj;
};


function SearchData() {

    $('#list').datagrid("loadData", { total: 0, rows: [] });



    var sjson = { }
    $.ajax({
        type: "post",
        url: "../../GTPT/GTPT_WXBQ.ashx?requestType=groups&mode=GetTagList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
        data: { json: JSON.stringify(sjson) },
        success: function (data) {
            var dataArray = new Array();
            dataArray = JSON.parse(data);

            for (var j = 0; j < dataArray.length; j++) {


                $('#list').datagrid('appendRow', {
                    iTAGID: dataArray[j].id,
                    sBQMC: dataArray[j].name,
                    iCOUNT: dataArray[j].count,
                });

            }




        },
        error: function (data) {
            ShowMessage(data)
        }
    });


}
