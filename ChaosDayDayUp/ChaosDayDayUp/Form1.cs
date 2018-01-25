using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChaosDayDayUp
{
    public partial class Form1 : Form
    {
        public DateTime dRQ;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dRQ = new DateTime(2017, 9, 1);
            PersonInfo.HP = 100;
            PersonInfo.MaxHP = 100;
            PersonInfo.MP = 100;
            PersonInfo.MaxMP = 100;
            PersonInfo.Food = 100;
            PersonInfo.MaxFood = 100;
            //Stat.HP = 100;
            //Stat.HP = 100;
            //Stat.HP = 100;
            timer1.Enabled = true;
        }
        private void ShowInfo()
        {
            pbHP.Value= PersonInfo.HP;
            pbHP.Value = PersonInfo.HP;
            pbHP.Value = PersonInfo.HP;
        }
        private void Day()
        {
            Text = "DayDayUp!  " + dRQ.ToShortDateString();
            Show();
            Morning();
            Afternoon();
            Evening();
            dRQ = dRQ.AddDays(1);
        }
        //日程
        private void Morning()
        {
            Breakfast();
            Work(4);
        }
        private void Afternoon()
        {
            Lunch();
            NoonBreak(1);
            Work(2);
            Tea();
            Work(2);
        }
        private void Evening()
        {
            Supper();
            Play(4);
            NighSleep(1);
        }
        //吃饭
        private void Breakfast()
        {
            PersonInfo.Eat(10, 5, 1);
        }
        private void Lunch()
        {
            PersonInfo.Eat(80, 5, 1);
        }
        private void Tea()
        {
            PersonInfo.Eat(10, 10, 0);
        }
        private void Supper()
        {
            PersonInfo.Eat(60, 5, 1);
        }
        private void NightSnack()
        {
            PersonInfo.Eat(10, 10, -1);
        }
        //休息
        private void NoonBreak(int hour)
        {
            PersonInfo.Sleep(hour * 8, Math.Min(hour * 2, 5), 1);
        }
        private void NighSleep(int hour)
        {
            PersonInfo.Sleep(hour * 10, Math.Min(hour * 2, 10), 2);
        }
        private void Work(int hour)
        {

        }
        private void Play(int hour)
        {

        }
        private void Study(int hour)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Day();
        }
    }
}
