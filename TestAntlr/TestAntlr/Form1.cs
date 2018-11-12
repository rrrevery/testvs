using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Antlr4.Runtime;
using Z.Expressions;

namespace TestAntlr
{
    public partial class Form1 : Form
    {
        private void Log(string s)
        {
            textBox2.Text += s + "\r\n";
        }

        public DataTableExpr table;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string input = textBox1.Text;

            //var stream = new AntlrInputStream(input);
            //var lexer = new MyGrammarLexer(stream);
            //var tokens = new CommonTokenStream(lexer);
            //var parser = new MyGrammarParser(tokens);
            //var tree = parser.program();

            //var visitor = new MyGrammarVisitor();
            //var result = visitor.Visit(tree);

            ////textBox2.Text += tree.ToStringTree(parser)+ "\r\n";
            //textBox2.Text += result + "\r\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            double result = 0;
            string output = string.Empty;
            DateTime ks = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                //output = calculator.Calc2(input);
                result = calculator.Calc2(input);
            }
            DateTime js = DateTime.Now;
            Log(input + "=" + result);
            Log((js - ks).TotalMilliseconds.ToString());
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            double result = 0;
            DateTime ks = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                result = calculator.Calc3(input);// textBox1.Text.Execute<double>();
            }
            DateTime js = DateTime.Now;
            Log(textBox1.Text + "=" + result.ToString());
            Log((js - ks).TotalMilliseconds.ToString());
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
            table.Columns.Add("语文平均分", typeof(double));
            table.Columns.Add("偏科程度", typeof(string));
            //table.Columns["文科"].Expression = "政治+历史+地理";
            //table.Columns["理科"].Expression = "物理+化学+生物";
            //table.Columns["总分"].Expression = "语文+数学+英语+文科+理科";
            //table.Columns["偏科程度"].Expression = "100*abs(文科-理科)/(文科+理科)";
            table.ColumnExpression["文科"] = "{{=[政治]+[历史]+[地理]}}";
            table.ColumnExpression["理科"] = "{{=[物理]+[化学]+[生物]}}";
            table.ColumnExpression["总分"] = "{{=[语文]+[数学]+[英语]+[政治]+[历史]+[地理]+[物理]+[化学]+[生物]}}";
            table.ColumnExpression["偏科程度"] = "{{=round(100*abs([文科]-[理科])/([文科]+[理科]),2)}}%";
            table.ColumnExpression["语文平均分"] = "{{=avg([语文])}}";
            dataGridView1.DataSource = table;
        }
    }
}
