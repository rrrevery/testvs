<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="APP_BMQFFD.aspx.cs" Inherits="BF.CrmWeb.APP.BMQFFD.APP_BMQFFD" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>

    <script type="text/javascript">
        vPageMsgID = '<%=CM_LMSHGL_BMQFFD%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_LMSHGL_BMQFFD_LR%>');
        bCanStart = CheckMenuPermit(iDJR, '<%=CM_LMSHGL_BMQFFD_QD%>');
        bCanStop = CheckMenuPermit(iDJR, '<%=CM_LMSHGL_BMQFFD_ZZ%>');
    </script>
    <script src="APP_BMQFFD.js"></script>
    <script src='../../../Js/KindEditor/kindeditor.js'></script>
    <script src="../../../Js/KindEditor/lang/zh-CN.js"></script>

</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">门店</div>
            <div class="bffld_right">
                <input type="text" id="TB_MDMC" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />

            </div>
        </div>
    </div>




    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">礼包标记</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="CB_LBBJ" id="C_Q" value="1" />
                <label for="C_Q">单券</label>
                <input class="magic-checkbox" type="checkbox" name="CB_LBBJ" id="C_B" value="2" />
                <label for="C_B">礼包</label>
                <input id="HF_LBBJ" type="hidden" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">编码券规则</div>
            <div class="bffld_right">
                <input id="TB_BMQFFGZ" type="text" />
                <input id="HF_BMQFFGZID" type="hidden" />
                <input id="zHF_BMQFFGZID" type="hidden" />
            </div>
        </div>



    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">活动名称</div>
            <div class="bffld_right">
                <input id="TB_NAME" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">编码券名称</div>
            <div class="bffld_right">
                <input id="TB_BMQMC" type="text" />
                <input id="HF_BMQID" type="hidden" />
                <input id="zHF_BMQID" type="hidden" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">券有效期天数</div>
            <div class="bffld_right">
                <input id="TB_QYXQTS" type="text" tabindex="1" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">促销主题</div>
            <div class="bffld_right">
                <input id="TB_CXZT" type="text" tabindex="1" />
                <input id="HF_CXID" type="hidden" />
                <input id="zHF_CXID" type="hidden" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">开始日期</div>
            <div class="bffld_right">
                <input id="TB_KSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">结束日期</div>
            <div class="bffld_right">
                <input id="TB_JSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">券开始日期</div>
            <div class="bffld_right">
                <input id="TB_QKSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">券结束日期</div>
            <div class="bffld_right">
                <input id="TB_QJSRQ" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" />
            </div>
        </div>
    </div>




    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传券大图 </div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" />
            </div>
        </div>
        <div class="bffld">
            <form id="form1" method="post" name="form1" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file" id="files" />
                </span>
                <input type="button" value="点击上传" id="upload" />
            </form>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传券小图 </div>
            <div class="bffld_right">
                <input id="TB_LOGO" type="text" />
            </div>
        </div>
        <div class="bffld">
            <form id="form2" method="post" name="form1" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file" id="files_logo" />
                </span>
                <input type="button" value="点击上传" id="upload_logo" />
            </form>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">适用范围</div>
            <div class="bffld_right">
                <input id="TB_SYFW" type="text" />
            </div>
        </div>
    </div>
    <div id="MZ">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">分享标题</div>
                <div class="bffld_right">
                    <input id="TB_TITLE_MZ" type="text" tabindex="1" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">分享描述</div>
                <div class="bffld_right">
                    <input id="TB_DESCRIBE_MZ" type="text" tabindex="1" />

                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">分享图片 </div>
                <div class="bffld_right">
                    <input id="TB_IMG_MZ" type="text" />
                </div>
            </div>
            <div class="bffld">
                <form id="formMZ" method="post" name="form1" enctype="multipart/form-data">
                    <span class="img_span">
                        <input type="file" name="file" id="fileMZ" />
                    </span>
                    <input type="button" value="点击上传" id="upload_FXMZ" />
                </form>
            </div>
        </div>
        <div class="bfrow" style="margin-top: 10px;">
            <div class="bffld">
                <div class="bffld_left">链接地址</div>
                <div class="bffld_right">
                    <input id="TB_URL_MZ" type="text" class="form_input" style="width: 400px;" />
                </div>

            </div>
        </div>

        <div class="bffld">
            <div class="bffld_left">选择品牌</div>
            <div class="bffld_right">
                <input id="TB_SBMC" type="text" />
                <input id="HF_SBID" type="hidden" />
                <input id="zHF_SBID" type="hidden" />

            </div>

        </div>

        <div class="bffld">
            <div class="bffld_left">开抢时间</div>
            <div class="bffld_right">
                <input id="TB_ENDTIME" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyy-MM-dd HH:mm:ss'})" />


            </div>

        </div>
        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">用券须知</div>
                <div class="bffld_right">
                    <textarea id="TA_YQXZ_MZ"></textarea>
                </div>
            </div>
        </div>


        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">优惠详情</div>
                <div class="bffld_right">
                    <textarea id="TA_YHXQ_MZ"></textarea>
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left"></div>
                <div class="bffld_right">
                    <button id="AddItem" type='button' class="item_addtoolbar">添加面值</button>
                    <button id="DelItem" type='button' class="item_deltoolbar">删除</button>

                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld" style="width: 1000px">
                <div class="bffld_left"></div>
                <div class="bffld_right" style="width: 1000px">
                    <table id="list"></table>
                </div>
            </div>

        </div>
    </div>








    <div id="LB">
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">分享标题</div>
                <div class="bffld_right">
                    <input id="TB_TITLE_LB" type="text" />
                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">分享描述</div>
                <div class="bffld_right">
                    <input id="TB_DESCRIBE_LB" type="text" />

                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">分享图片 </div>
                <div class="bffld_right">
                    <input id="TB_IMG_LB" type="text" />
                </div>
            </div>
            <div class="bffld">
                <form id="formLB" method="post" name="form1" enctype="multipart/form-data">
                    <span class="img_span">
                        <input type="file" name="file" id="fileLB" />
                    </span>
                    <input type="button" value="点击上传" id="upload_FXLB" />
                </form>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">链接地址</div>
                <div class="bffld_right">
                    <input id="TB_URL_LB" type="text" />
                </div>

            </div>
        </div>




        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left">选择礼包</div>
                <div class="bffld_right">
                    <input id="TB_LBMC" type="text" />
                    <input id="HF_LBID" type="hidden" />
                    <input id="zHF_LBID" type="hidden" />

                </div>
            </div>
            <div class="bffld">
                <div class="bffld_left">开抢时间</div>
                <div class="bffld_right">
                    <input id="TB_ENDTIME_LB" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true,dateFmt:'yyyy-MM-dd HH:mm:ss'})" />


                </div>

            </div>
        </div>

        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">用券须知</div>
                <div class="bffld_right">
                    <textarea id="TA_YQXZ_LB"></textarea>
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld_l">
                <div class="bffld_left">优惠详情</div>
                <div class="bffld_right">
                    <textarea id="TA_YHXQ_LB"></textarea>
                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld">
                <div class="bffld_left"></div>
                <div class="bffld_right">
                    <button id="Add" type='button' class="item_addtoolbar">添加礼包</button>
                    <button id="Del" type='button' class="item_deltoolbar">删除</button>

                </div>
            </div>
        </div>
        <div class="bfrow">
            <div class="bffld" style="width: 1000px">
                <div class="bffld_left"></div>
                <div class="bffld_right" style="width: 1000px">
                    <table id="list_lb"></table>
                </div>
            </div>

        </div>
    </div>








    <%=V_InputBodyEnd %>
    <script>
        KindEditor.ready(function (K) {
            window.editor1 = K.create('#TA_YQXZ_MZ', { width: '100%' });
            window.editor2 = K.create('#TA_YHXQ_MZ', { width: '100%' });
            window.editor3 = K.create('#TA_YQXZ_LB', { width: '100%' });
            window.editor4 = K.create('#TA_YHXQ_LB', { width: '100%' });

        });

    </script>

</body>

</html>
