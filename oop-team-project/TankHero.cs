using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal class TankHero : Hero, UseSkill, TauntSkill
    {
        private int ShieldThrowPower = 90;
        private bool isDefenseMode;
        private bool isTaunting;

        public bool IsDefenseMode
        {
            get { 
                return isDefenseMode; 
            }
            set { 
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
            Console.WriteLine("3. " + Name + " HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature monster)
        {
            switch (skillNumber)
            {
                case 1:
                    IronDefenseSkill();
                    break;

                case 2:
                    TauntSkill();
                    break;

                case 3:
                    ShieldThrowSkill(monster);
                    break;

                default:
                    throw new SkillException("잘못된 스킬 번호입니다.");
            }
        }

        public void TauntSkill()
        {
            IsTaunting = true;
            Console.WriteLine("도발을 사용합니다.");
        }

        public override void ShowSkills()
        {
            Console.WriteLine("1. 철벽방어 (팀 피해 50% 감소)");
            Console.WriteLine("2. 도발");
            Console.WriteLine("3. 방패던지기 (" + ShieldThrowPower + " 추가피해)");
        }

        public void ResetTaunt()
        {
            if (isTaunting)
            {
                isTaunting = false;
                Console.WriteLine(Name + "의 도발 효과가 해제되었습니다.");
            }
        }

        public override void TakeDamage(int damage)
        {
            int finalDamage = (int)(damage * (1 - DefenseRate));

            if (IsDefenseMode)
            {
                finalDamage /= 2;
                Console.WriteLine("피해가 50% 감소합니다.");
            }

            base.TakeDamage(finalDamage);
        }

        private void IronDefenseSkill()
        {
            IsDefenseMode = true;
            Console.WriteLine("철벽 방어를 사용합니다.");
        }

        private void ShieldThrowSkill(Creature monster)
        {
            Console.WriteLine("방패 던지기를 사용합니다.");
            monster.TakeDamage(AttackPower + ShieldThrowPower);
        }
    }
}