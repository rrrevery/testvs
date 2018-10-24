using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime;

namespace TestAntlr
{
    public partial class Form1 : Form
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
        public DataTableExpr table;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;

            var stream = new AntlrInputStream(input);
            var lexer = new MyGrammarLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MyGrammarParser(tokens);
            var tree = parser.program();

            var visitor = new MyGrammarVisitor();
            var result = visitor.Visit(tree);

            //textBox2.Text += tree.ToStringTree(parser)+ "\r\n";
            textBox2.Text += result + "\r\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string output = calculator.Calc(input);

            //var stream = new AntlrInputStream(input);
            //var lexer = new calculatorLexer(stream);
            //var tokens = new CommonTokenStream(lexer);
            //var parser = new calculatorParser(tokens);
            //var tree = parser.expression();

            //var visitor = new calculatorVisitor();
            //var result = visitor.Visit(tree);

            textBox2.Text += input + "=" + output + "\r\n";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table = new DataTableExpr();
            table.Columns.Add("姓名", typeof(string));
            table.Columns.Add("语文", typeof(double));
            table.Columns.Add("数学", typeof(double));
            table.Columns.Add("英语", typeof(double));
            table.Columns.Add("政治", typeof(double));
            table.Columns.Add("历史", typeof(double));
            table.Columns.Add("地理", typeof(double));
            table.Columns.Add("物理", typeof(double));
            table.Columns.Add("化学", typeof(double));
            table.Columns.Add("生物", typeof(double));
            table.Columns.Add("总分", typeof(double));
            table.Columns.Add("文科", typeof(double));
            table.Columns.Add("理科", typeof(double));
            table.Columns.Add("偏科程度", typeof(string));
            //table.Columns["文科"].Expression = "政治+历史+地理";
            //table.Columns["理科"].Expression = "物理+化学+生物";
            //table.Columns["总分"].Expression = "语文+数学+英语+文科+理科";
            //table.Columns["偏科程度"].Expression = "100*abs(文科-理科)/(文科+理科)";
            table.ColumnExpression["文科"] = "{{=[政治]+[历史]+[地理]}}";
            table.ColumnExpression["理科"] = "{{=[物理]+[化学]+[生物]}}";
            table.ColumnExpression["总分"] = "{{=[语文]+[数学]+[英语]+[政治]+[历史]+[地理]+[物理]+[化学]+[生物]}}";
            table.ColumnExpression["偏科程度"] = "{{=round(100*abs([文科]-[理科])/([文科]+[理科]),2)}}%";
            table.TableNewRow += Table_TableNewRow;
            table.RowChanged += Table_RowChanged;
            dataGridView1.DataSource = table;
        }

        private void Table_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            //Console.WriteLine("2");
            //table.RowChanged -= Table_RowChanged;
            //foreach (var column in table.ColumnExpression)
            //    CalcField(e.Row, column.Key, column.Value);
            //table.RowChanged += Table_RowChanged;
        }

        private void Table_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            /*e.Row["总分"] = "{{=[语文]+[数学]+[英语]+[文科]+[理科]}}";
            e.Row["文科"] = "{{=[政治]+[历史]+[地理]}}";
            e.Row["理科"] = "{{=[物理]+[化学]+[生物}]}";
            e.Row["偏科程度"] = "{{=100*abs([文科]-[理科])/([文科]+[理科])}}%";*/
            /*CalcField(e.Row, "总分", "{{=[语文]+[数学]+[英语]+[文科]+[理科]}}");
            CalcField(e.Row, "文科", "{{=[政治]+[历史]+[地理]}}");
            CalcField(e.Row, "理科", "{{=[物理]+[化学]+[生物]}}");
            CalcField(e.Row, "偏科程度", "{{=100*abs([文科]-[理科])/([文科]+[理科])}}%");*/
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (((DataTable)dataGridView1.DataSource).Rows.Count == 0)
            //    return;
            //CalcField(((DataTable)dataGridView1.DataSource).Rows[e.RowIndex], "文科", "{{=[政治]+[历史]+[地理]}}");
            //CalcField(((DataTable)dataGridView1.DataSource).Rows[e.RowIndex], "理科", "{{=[物理]+[化学]+[生物]}}");
            //CalcField(((DataTable)dataGridView1.DataSource).Rows[e.RowIndex], "总分", "{{=[语文]+[数学]+[英语]+[文科]+[理科]}}");
            //CalcField(((DataTable)dataGridView1.DataSource).Rows[e.RowIndex], "偏科程度", "{{=round(100*abs([文科]-[理科])/([文科]+[理科]),2)}}%");
        }
    }
}
