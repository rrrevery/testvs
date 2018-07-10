vUrl = "../GTPT.ashx";
vCaption = "微信问题定义";
function InitGrid() {
    vColumnNames = ['ID', '问题', '标记', '状态'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'sASK', width: 80, },
               {
                   name: 'iTYPE', width: 80, formatter: function (cellvalues) {
                       if (cellvalues == 0)
                           return "关键词回复";
                       if (cellvalues == 1)
                           return "菜单推送";
                       if (cellvalues == 2)
                           return "关注回复";
                       if (cellvalues == 3)
                           return "默认回复";
                   },
               },

               {
                   name: 'iSTATUS', width: 80, formatter: function (cellvalues) {
                       if (cellvalues == 1)
                           return "正常";
                       if (cellvalues == 2)
                           return "停用";
                   },
               },
    ];
}

$(document).ready(function () {
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    var type = $("[name='TYPE']:checked").val();
    var status = $("[name='status']:checked").val();
    if (status != 'all') {
        MakeSrchCondition2(arrayObj, status, "iSTATUS", "=", true);
    }
    if (type != 'all') {
        MakeSrchCondition2(arrayObj, type, "iTYPE", "=", true);
    }
    MakeSrchCondition(arrayObj, "TB_ASK", "sASK", "like", true);
    return arrayObj;
};
