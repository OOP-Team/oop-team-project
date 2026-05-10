using System;
using System.Collections.Generic;
using System.Text;


namespace oop_team_project
{
    internal class BossMonster : Monster
    {
        public BossMonster()
            : base("보스", 3000, 180, 0.2) {}

        public override void ShowStatus()
        {
            Console.WriteLine("보스 HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature target)
        {
            switch (skillNumber)
            {
                case 1:
                    Console.WriteLine("영혼약탈");
                    target.TakeDamage(AttackPower + 150);
                    break;

                case 2:
                    Console.WriteLine("카오스");
                    target.TakeDamage(AttackPower + 220);
                    break;

                case 3:
                    Console.WriteLine("심판");
                    target.TakeDamage(AttackPower + 280);
                    break;
            }
        }

        public override void ShowSkills()
        {
            Console.WriteLine("1. 영혼약탈 (150 추가피해)");
            Console.WriteLine("2. 카오스   (220 추가피해)");
            Console.WriteLine("3. 심판     (280 추가피해)");
        }
    }
}