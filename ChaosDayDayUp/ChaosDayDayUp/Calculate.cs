using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosDayDayUp
{
    public class PersonInfo
    {
        //public static PersonInfo PInfo;
        public static int HP, MaxHP, MP, MaxMP, Food, MaxFood;
        public static void Eat(int food, int mood, int health)
        {
            Food += food;
            if (Food > MaxFood)
                Food = MaxFood;
            MP += mood;
            if (MP > MaxMP)
                MP = MaxMP;
        }
        public static void Sleep(int hp, int mood, int health)
        {
            HP += hp;
            if (HP > MaxMP)
                HP = MaxMP;
            MP += mood;
            if (MP > MaxMP)
                MP = MaxMP;

        }
    }
}
