using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosDayDayUp
{
    public class PersonInfo
    {
        public DateTime dRQ;
        public int Days, HP, MaxHP, MP, MaxMP, Food, MaxFood, Money;
        public int WorkDays;//每月工作天数
        public Work MyWork;
        public void Morning()
        {
            Breakfast();
            Work(4);
        }
        public void Afternoon()
        {
            Lunch();
            NoonBreak(1);
            Work(2);
            Tea();
            Work(2);
        }
        public void Evening()
        {
            Supper();
            Play(4);
            NighSleep(8);
        }
        private void Breakfast()
        {
            Eat(10, 5, 1);
        }
        private void Lunch()
        {
            Eat(80, 5, 1);
            Live(1);
        }
        private void Tea()
        {
            Eat(10, 10, 0);
        }
        private void Supper()
        {
            Eat(60, 5, 1);
            Live(1);
        }
        private void NightSnack()
        {
            Eat(10, 10, -1);
        }
        //休息
        private void NoonBreak(int hour)
        {
            Sleep(hour * 8, Math.Min(hour * 2, 5), 1);
            Live(hour);
        }
        private void NighSleep(int hour)
        {
            Sleep(hour * 10, Math.Min(hour * 2, 10), 2);
            Live(hour);
        }
        private void Work(int hour)
        {
            HP -= hour * MyWork.WorkHP;
            MP -= hour * MyWork.WorkMP;
            Money += MyWork.WorkMoney;//暂时日薪制
            Live(hour);
        }
        private void Play(int hour)
        {

        }
        private void Study(int hour)
        {

        }
        public void Eat(int food, int mood, int health)
        {
            Food += food;
            if (Food > MaxFood)
                Food = MaxFood;
            MP += mood;
            if (MP > MaxMP)
                MP = MaxMP;
        }
        public void Sleep(int hp, int mood, int health)
        {
            HP += hp;
            if (HP > MaxMP)
                HP = MaxMP;
            MP += mood;
            if (MP > MaxMP)
                MP = MaxMP;
        }
        public void Live(int hour)
        {
            //食物永远在降低，不管睡着醒着
            Food -= 10;
        }
    }
    public class Work
    {
        public int WorkHP, WorkMP, WorkMoney;
    }
}
