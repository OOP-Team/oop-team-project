using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace oop_team_project
{
    internal class Chimera : Monster
    {
        private int DashPower = 70;
        private int ClawPower = 100;
        private int PoisonPower = 130;

        public Chimera(string name)
            : base(name, 1800, 130, 0.1) { }

        public override void ShowStatus()
        {
            Console.WriteLine("1. " + Name + " HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature hero)
        {
            switch (skillNumber)
            {
                case 1:
                    DashSkill(hero);
                    break;
                case 2:
                    ClawSkill(hero);
                    break;
                case 3:
                    PoisonSkill(hero);
                    break;
                default:
                    throw new SkillException("잘못된 스킬 번호입니다.");
            }
        }

        public override void ShowSkills()
        {
            Console.WriteLine("1. 돌진   (" + DashPower + " 추가피해)");
            Console.WriteLine("2. 할퀴기 (" + ClawPower + " 추가피해)");
            Console.WriteLine("3. 포이즌 (" + PoisonPower + " 추가피해)");
        }

        private void DashSkill(Creature hero)
        {
            Console.WriteLine(Name + "의 돌진!");
            hero.TakeDamage(AttackPower + DashPower);
        }

        private void ClawSkill(Creature hero)
        {
            Console.WriteLine(Name + "의 할퀴기!");
            hero.TakeDamage(AttackPower + ClawPower);
        }

        private void PoisonSkill(Creature hero)
        {
            Console.WriteLine(Name + "의 포이즌!");
            hero.TakeDamage(AttackPower + PoisonPower);
            hero.TakeDamage(50, 3);
        }
    }
}