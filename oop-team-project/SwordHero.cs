using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal class SwordHero : Hero, UseSkillTarget
    {
        public SwordHero(string name)
            : base(name, 1400, 120, 0.1) {}

        public override void ShowStatus()
        {
            Console.WriteLine("검사 " + Name + " HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature target)
        {
            switch (skillNumber)
            {
                case 1:
                    Charge(target);
                    break;

                case 2:
                    SpinSlash(target);
                    break;

                case 3:
                    Judgment(target);
                    break;

                default:
                    throw new InvalidSkillException("잘못된 스킬 번호입니다.");
            }
        }

        private void Charge(Creature target)
        {
            Console.WriteLine(Name + " 이 돌격 공격!");
            target.TakeDamage(AttackPower + 50);
        }

        private void SpinSlash(Creature target)
        {
            Console.WriteLine(Name + " 이 회전베기 공격");
            target.TakeDamage(AttackPower + 100);
        }

        private void Judgment(Creature target)
        {
            Console.WriteLine(Name + " 이 저지먼트 공격");
            target.TakeDamage(AttackPower + 180);
        }

        public override void ShowSkills()
        {
            Console.WriteLine("1. 돌격  (50 추가피해)");
            Console.WriteLine("2. 회전베기  (100 추가피해)");
            Console.WriteLine("3. 저지먼트  (180 추가피해)");
        }
    }
}
