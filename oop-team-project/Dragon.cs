using System;
using System.Collections.Generic;
using System.Text;


namespace oop_team_project
{
    internal class Dragon : Monster
    {
        public Dragon()
            : base("드래곤", 2500, 170, 0.15) {}

        public override void ShowStatus()
        {
            Console.WriteLine("드래곤 HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature target)
        {
            switch (skillNumber)
            {
                case 1:
                    Console.WriteLine("브레스");
                    target.TakeDamage(AttackPower + 150);
                    break;

                case 2:
                    Console.WriteLine("역린");
                    target.TakeDamage(AttackPower + 200);
                    break;

                case 3:
                    Console.WriteLine("돌진");
                    target.TakeDamage(AttackPower + 100);
                    break;
            }
        }

        public override void ShowSkills()
        {
            Console.WriteLine("1. 브레스 (150 추가피해)");
            Console.WriteLine("2. 역린   (200 추가피해)");
            Console.WriteLine("3. 돌진   (100 추가피해)");
        }
    }
}