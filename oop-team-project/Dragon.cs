using System;
using System.Collections.Generic;
using System.Text;


namespace oop_team_project
{
    internal class Dragon : Monster
    {
        private int BreathPower = 150;
        private int WrathPower = 200;
        private int DashPower = 100;

        public Dragon(string name)
            : base(name, 2500, 170, 0.15) { }

        public override void ShowStatus()
        {
            Console.WriteLine("3. " + Name + " HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature hero)
        {
            switch (skillNumber)
            {
                case 1:
                    BreathSkill(hero);
                    break;
                case 2:
                    WrathSkill(hero);
                    break;
                case 3:
                    DashSkill(hero);
                    break;
                default:
                    throw new SkillException("잘못된 스킬 번호입니다.");
            }
        }

        public override void ShowSkills()
        {
            Console.WriteLine("1. 브레스 (" + BreathPower + " 추가피해)");
            Console.WriteLine("2. 역린   (" + WrathPower + " 추가피해)");
            Console.WriteLine("3. 돌진   (" + DashPower + " 추가피해)");
        }

        private void BreathSkill(Creature hero)
        {
            Console.WriteLine(Name + "의 브레스 공격");
            hero.TakeDamage(AttackPower + BreathPower);
        }

        private void WrathSkill(Creature hero)
        {
            Console.WriteLine(Name + "의 역린");
            hero.TakeDamage(AttackPower + WrathPower);
        }

        private void DashSkill(Creature hero)
        {
            Console.WriteLine(Name + "의 돌진 공격");
            hero.TakeDamage(AttackPower + DashPower);
        }
    }
}