using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace oop_team_project
{
    internal class MageHero : Hero, UseSkillTarget, MageHeroHeal
    {
        public MageHero(string name)
            : base(name, 900, 180, 0.05) {}

        public override void ShowStatus()
        {
            Console.WriteLine("마법사 " + Name + " HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature target)
        {
            switch (skillNumber)
            {
                case 1:
                    FireBall(target);
                    break;

                case 2:
                    Lightning(target);
                    break;

                case 3:
                    break;

                default:
                    throw new InvalidSkillException("잘못된 스킬 번호입니다.");
            }
        }

        public void HealTeam(Team team)
        {
            Console.WriteLine("팀원 모두에게 회복 스킬을 사용한다.");

            foreach (Creature member in team.Members)
            {
                if (!member.IsDead)
                {
                    member.Heal(300);
                }
            }
        }

        private void FireBall(Creature target)
        {
            Console.WriteLine(Name + " 가 파이어볼 공격");
            target.TakeDamage(AttackPower + 120);
        }

        private void Lightning(Creature target)
        {
            Console.WriteLine(Name + " 가 라이트닝 공격");
            target.TakeDamage(AttackPower + 180);
        }

        public override void ShowSkills()
        {
            Console.WriteLine("1. 파이어볼  (120 추가피해)");
            Console.WriteLine("2. 라이트닝  (180 추가피해)");
            Console.WriteLine("3. 전체회복  (팀 전체 300 회복)");
        }
    }
}
