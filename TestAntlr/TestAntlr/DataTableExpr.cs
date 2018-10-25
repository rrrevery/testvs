using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace TestAntlr
{
    public class DataTableExpr : DataTable
    {
        public Dictionary<string, string> ColumnExpression = new Dictionary<string, string>();
        public DataTableExpr()
        {
            this.RowChanged += DataTableExpr_RowChanged;
        }

        private void DataTableExpr_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            this.RowChanged -= DataTableExpr_RowChanged;
            foreach (var column in ColumnExpression)
                CalcField(e.Row, column.Key, column.Value);
            this.RowChanged += DataTableExpr_RowChanged;
        }

        public void CalcField(DataRow row, string column, string expression)
        {
            string res = string.Empty;
            //计算表达式，可能一个expression里存在多个表达式，按每个{{~}}之间为一个表达式来计算，表达式外的保持原状
            int inx = expression.IndexOf("{{");
            int inx_end = expression.IndexOf("}}");
            while (inx >= 0)
            {
                res += expression.Substring(0, inx);
                string exp = expression.Substring(inx + 3, inx_end - inx - 3);
                //替换字段
                int inx2 = exp.IndexOf("[");
                int inx2_end = exp.IndexOf("]");
                while (inx2 >= 0)
                {
                    string fld = exp.Substring(inx2 + 1, inx2_end - inx2 - 1);
                    double val = 0;
                    double.TryParse(row[fld].ToString(), out val);
                    exp = exp.Replace("[" + fld + "]", val.ToString());
                    inx2 = exp.IndexOf("[");
                    inx2_end = exp.IndexOf("]");
                }
                expression = expression.Substring(inx_end + 2);
                res += calculator.Calc(exp); //"计算表达式《" + exp + "》";
                inx = expression.IndexOf("{{");
                inx_end = expression.IndexOf("}}");
            }
            if (expression != string.Empty)
                res += expression;
            row[column] = res;
        }
    }
}
