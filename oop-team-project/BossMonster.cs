using System;
using System.Collections.Generic;
using System.Text;


namespace oop_team_project
{
    internal class BossMonster : Monster
    {
        private int SoulStealPower = 150;
        private int ChaosPower = 220;
        private int JudgmentPower = 280;

        public BossMonster(string name)
            : base(name, 3000, 180, 0.2) { }

        public override void ShowStatus() {
            Console.WriteLine("2. " + Name + " HP : " + CurrentHp + "/" + MaxHp);
        }

        public override void UseSkill(int skillNumber, Creature hero) {
            switch (skillNumber) {
                case 1:
                    SoulStealSkill(hero);
                    break;
                case 2:
                    ChaosSkill(hero);
                    break;
                case 3:
                    JudgmentSkill(hero);
                    break;
                default:
                    throw new SkillException("잘못된 스킬 번호입니다.");
            }
        }

        public override void ShowSkills() {
            Console.WriteLine("1. 영혼약탈 (" + SoulStealPower + " 추가피해)");
            Console.WriteLine("2. 카오스   (" + ChaosPower + " 추가피해)");
            Console.WriteLine("3. 심판     (" + JudgmentPower + " 추가피해)");
        }

        private void SoulStealSkill(Creature hero) {
            Console.WriteLine(Name + "의 영혼약탈 공격");
            hero.TakeDamage(AttackPower + SoulStealPower);
        }

        private void ChaosSkill(Creature hero) {
            Console.WriteLine(Name + "의 카오스 에너지 공격");
            hero.TakeDamage(AttackPower + ChaosPower);
        }

        private void JudgmentSkill(Creature hero) {
            Console.WriteLine(Name + "의 최후의 심판");
            hero.TakeDamage(AttackPower + JudgmentPower);
        }
    }
}