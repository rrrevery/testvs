using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDatabind
{
    public partial class Form1 : Form
    {
        RDataTable table;
        DataObject obj = new DataObject();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table = new RDataTable();
            table.Columns.Add("ID", typeof(double));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Name", typeof(string));

            label1.DataBindings.Add("Text", table.BindSource, "Id");
            textBox1.DataBindings.Add("Text", table.BindSource, "Code");
            textBox2.DataBindings.Add("Text", table.BindSource, "Name");
            dataGridView1.DataSource = table.BindSource;

            //label1.DataBindings.Add("Text", bindingSource1, "Id");
            //textBox1.DataBindings.Add("Text", bindingSource1, "Code");
            //textBox2.DataBindings.Add("Text", bindingSource1, "Name");
            //dataGridView1.DataSource = bindingSource1;

            //label2.DataBindings.Add("Text", obj, "Id");
            //textBox3.DataBindings.Add("Text", obj, "Code");
            //textBox4.DataBindings.Add("Text", obj, "Name");

            bindingSource1.DataSource = obj;
            label2.DataBindings.Add("Text", bindingSource1, "Id");
            textBox3.DataBindings.Add("Text", bindingSource1, "Code");
            textBox4.DataBindings.Add("Text", bindingSource1, "Name");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            table.Rows.Add(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            table.Rows[table.Position]["Name"] = DateTime.Now.Second.ToString() + "." + DateTime.Now.Millisecond.ToString();
        }

        private void btnNewObj_Click(object sender, EventArgs e)
        {
            obj.Id = DateTime.Now.Hour;
            obj.Code = DateTime.Now.Minute.ToString();
            obj.Name = DateTime.Now.Second.ToString();
        }

        private void btnModObj_Click(object sender, EventArgs e)
        {
            obj.Name = DateTime.Now.Second.ToString() + "." + DateTime.Now.Millisecond.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
