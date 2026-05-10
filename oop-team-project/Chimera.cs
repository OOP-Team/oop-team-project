using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal class Chimera : Monster
    {
        public Chimera()
            : base("키메라", 1800, 130, 0.1) {}

        public override void ShowStatus()
        {
            Console.WriteLine("키메라 HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature target)
        {
            switch (skillNumber)
            {
                case 1:
                    Console.WriteLine("돌진");
                    target.TakeDamage(AttackPower + 70);
                    break;

                case 2:
                    Console.WriteLine("할퀴기");
                    target.TakeDamage(AttackPower + 100);
                    break;

                case 3:
                    Console.WriteLine("포이즌");
                    target.TakeDamage(AttackPower + 130);
                    break;
            }
        }
        public override void ShowSkills()
        {
            Console.WriteLine("1. 돌진   (70 추가피해)");
            Console.WriteLine("2. 할퀴기 (100 추가피해)");
            Console.WriteLine("3. 포이즌 (130 추가피해)");
        }
    }
}