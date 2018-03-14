using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ChaosDayDayUp
{
    public partial class Form1 : Form
    {
        public PersonInfo User = new PersonInfo();
        public string[] WeekDay = new string[] { "日", "一", "二", "三", "四", "五", "六" };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            User.dRQ = new DateTime(2017, 9, 1);
            User.Days = 0;
            User.HP = 100;
            User.MaxHP = 100;
            User.MP = 100;
            User.MaxMP = 100;
            User.Food = 100;
            User.MaxFood = 100;
            User.MyWork = new Work();
            User.MyWork.WorkHP = 6;
            User.MyWork.WorkMP = 1;
            User.MyWork.WorkMoney = 100;
            //Stat.HP = 100;
            //Stat.HP = 100;
            //Stat.HP = 100;
            timer1.Enabled = true;
        }
        public void ShowInfo()
        {
            pbHP.Value = User.HP;
            pbHP.Maximum = User.MaxHP;
            pbMP.Value = User.MP;
            pbMP.Maximum = User.MaxMP;
            pbFood.Value = User.Food;
            pbFood.Maximum = User.MaxFood;
            lbMoney.Text = "$" + User.Money.ToString();
            Text = "DayDayUp!  " + User.dRQ.ToShortDateString() + "[" + WeekDay[(int)User.dRQ.DayOfWeek] + "]";
        }
        public void Day()
        {
            ShowInfo();
            User.Morning();
            Thread.Sleep(100);
            ShowInfo();
            User.Afternoon();
            Thread.Sleep(100);
            ShowInfo();
            User.Evening();
            Thread.Sleep(100);
            ShowInfo();
            User.dRQ = User.dRQ.AddDays(1);
            User.Days++;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Day();
        }
    }
}
