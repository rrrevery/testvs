using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BF.CrmProc
{
    public class CrmLibStrings
    {
        //提示信息尽量放在这里，方便有问题统一修改
        //输入错误
        public const string msgNeedHYKNO = "请输入卡号";
        public const string msgNeedNewHYKNO = "请输入新卡号";
        public const string msgNeedBGDD = "请选择保管地点";
        public const string msgNeedBGDD_BC = "请选择拨出地点";
        public const string msgNeedBGDD_BR = "请选择拨入地点";
        public const string msgNeedHYKTYPE = "请选择卡类型";
        //单据检查
        public const string msgNeedDHJF = "请输入抵用积分";
        public const string msgNeedDHJE = "请输入抵用金额";

        //检查错误
        public const string msgHYXXNotFound = "没有找到此卡数据";
        public const string msgGKDANotFound = "没有找到顾客档案";
        public const string msgKCKNotFound = "没有找到库存卡号";
        public const string msgXFJLNotFound = "没有找到这笔消费";
        public const string msgHYKTYPENotFound = "没有找到卡类型";
        public const string msgXFJLWithWrongHYXX = "消费和卡号不匹配";
        public const string msgXFJLConditionError = "查询消费请传入消费记录ID或者门店ID+收款台号+小票号";
        public const string msgExecExecuted = "单据已审核或不存在";
        public const string msgSaveExecuted = "单据已审核不能保存";
        public const string msgWrongCKJE = "存款金额必须大于等于零";
        public const string msgWrongQKJE = "取款金额必须大于等于零";
        public const string msgChildExist = "数据已经使用，不能删除";
        public const string msgSQLInject = "条件包括数据库敏感词";
        public const string msgUnExecExecuted = "单据已取消审核或不存在";

        public static void GenResultColumns()
        {
            //所有查询结果列名放在这里
            AddResultColumns("iJLBH", "记录编号");
            AddResultColumns("sHYKNAME", "卡类型");
            AddResultColumns("iHYKTYPE", "卡类型代码", 40, true);
            AddResultColumns("sBGDDMC", "保管地点");
            AddResultColumns("sBGDDDM", "保管地点代码", 40, true);
            AddResultColumns("sFXDWMC", "发行单位");
            AddResultColumns("iFXDWID", "发行单位编号", 40, true);
            AddResultColumns("iJKSL", "建卡数量", 60);
            AddResultColumns("iXKSL", "写卡数量", 60);
            AddResultColumns("sCZKHM_BEGIN", "开始卡号");
            AddResultColumns("sCZKHM_END", "结束卡号");
            AddResultColumns("iDJR", "登记人", 40, true);
            AddResultColumns("sDJRMC", "登记人");
            AddResultColumns("dDJSJ", "登记时间", 120);
            AddResultColumns("iZXR", "审核人", 40, true);
            AddResultColumns("sZXRMC", "审核人");
            AddResultColumns("dZXRQ", "审核日期", 120);
            AddResultColumns("sZY", "摘要");
            AddResultColumns("sBZ", "备注");

            //AddResultColumns("iJLBH", "记录编号");
            //AddResultColumns("iJLBH", "记录编号");
            //AddResultColumns("iJLBH", "记录编号");            
        }
        public static void AddResultColumns(string name, string index, string title, int width = 80, bool hidden = false)
        {
            ResultColumn one = new ResultColumn(name, index, title, width, hidden);
            GlobalVariables.SYSConfig.ResultColumns.Add(one);
        }
        public static void AddResultColumns(string name, string title, int width = 80, bool hidden = false)
        {
            ResultColumn one = new ResultColumn(name, name, title, width, hidden);
            GlobalVariables.SYSConfig.ResultColumns.Add(one);
        }

    }
}
