using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal class TankHero : Hero, UseSkillTarget, TauntSkill
    {
        private bool isDefenseMode;
        private bool isTaunting;

        public bool IsDefenseMode
        {
            get { 
                return isDefenseMode; 
            }
            private set { 
                isDefenseMode = value; 
            }
        }

        public bool IsTaunting
        {
            get { 
                return isTaunting;
            }
            private set { 
                isTaunting = value; 
            }
        }

        public TankHero(string name)
            : base(name, 2200, 70, 0.3) {}

        public override void ShowStatus()
        {
            Console.WriteLine("탱커 " + Name + " HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature target)
        {
            switch (skillNumber)
            {
                case 1:
                    IronDefense();
                    break;

                case 2:
                    Taunt();
                    break;

                case 3:
                    ShieldThrow(target);
                    break;

                default:
                    throw new InvalidSkillException("잘못된 스킬 번호입니다.");
            }
        }

        private void IronDefense()
        {
            IsDefenseMode = true;

            Console.WriteLine("철벽 방어 사용!");
        }

        public void Taunt()
        {
            IsTaunting = true;

            Console.WriteLine("도발 사용!");
        }

        private void ShieldThrow(Creature target)
        {
            Console.WriteLine("방패 던지기 사용!");
            target.TakeDamage(AttackPower + 90);
        }

        public override void ShowSkills()
        {
            Console.WriteLine("1. 철벽방어 (팀 피해 50% 감소)");
            Console.WriteLine("2. 도발");
            Console.WriteLine("3. 방패던지기 (90 추가피해)");
        }

        public override void TakeDamage(int damage)
        {
            int reducedDamage = (int)(damage * (1 - DefenseRate));

            if (IsDefenseMode)
            {
                reducedDamage /= 2;
                Console.WriteLine("철벽 방어 시전. 피해가 감소합니다.");
            }
        }
    }
}