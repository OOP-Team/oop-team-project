using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal class SwordHero : Hero, UseSkill
    {
        private int ChargePower = 50;
        private int SpinSlashPower = 100;
        private int JudgmentPower = 180;
        public SwordHero(string name)
            : base(name, 1400, 120, 0.1) {}

        public override void ShowStatus() {
            Console.WriteLine("1. " + Name);
        }

        public override void UseSkill(int skillNumber, Creature monster) {
            switch (skillNumber) {
                case 1:
                    ChargeSkill(monster);
                    break;

                case 2:
                    SpinSlashSkill(monster);
                    break;

                case 3:
                    JudgmentSkill(monster);
                    break;

                default:
                    throw new SkillException("잘못된 스킬 번호입니다.");
            }
        }

        public override void ShowSkills() {
            Console.WriteLine("1. 돌격  (" + ChargePower + " 추가피해)");
            Console.WriteLine("2. 회전베기  (" + SpinSlashPower + " 추가피해)");
            Console.WriteLine("3. 저지먼트  (" + JudgmentPower + " 추가피해)");
        }

        private void ChargeSkill(Creature monster) {
            Console.WriteLine(Name + " 이 돌격 공격");
            monster.TakeDamage(AttackPower + ChargePower);
        }

        private void SpinSlashSkill(Creature monster) {
            Console.WriteLine(Name + " 이 회전베기 공격");
            monster.TakeDamage(AttackPower + SpinSlashPower);
        }

        private void JudgmentSkill(Creature monster) {
            Console.WriteLine(Name + " 이 저지먼트 공격");
            monster.TakeDamage(AttackPower + JudgmentPower);
        }
    }
}
