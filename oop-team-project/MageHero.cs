using System;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace oop_team_project
{
    internal class MageHero : Hero, UseSkill, MageHeroHeal
    {
        private int HealTeamPower = 300;
        private int FireBallPower = 120;
        private int LightningPower = 180;
        public MageHero(string name)
            : base(name, 900, 180, 0.05) {}

        public override void ShowStatus() {
            Console.WriteLine("2. " + Name);
        }

        public override void UseSkill(int skillNumber, Creature monster) {
            switch (skillNumber) {
                case 1:
                    FireBallSkill(monster);
                    break;

                case 2:
                    LightningSkill(monster);
                    break;

                default:
                    throw new SkillException("잘못된 스킬 번호입니다.");
            }
        }

        public void HealTeam(Team team) {
            try {
                Console.WriteLine("팀원 모두에게 회복 스킬을 사용한다.");

                foreach (Creature member in team.Members) {
                    if (!member.IsDead) {
                        member.Heal(HealTeamPower);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("회복 스킬 오류: " + ex.Message);
            }
            finally {
                Console.WriteLine("회복 스킬 종료");
            }
        }

        public override void ShowSkills() {
            Console.WriteLine("1. 파이어볼  (" + FireBallPower + " 추가피해)");
            Console.WriteLine("2. 라이트닝  (" + LightningPower + " 추가피해)");
            Console.WriteLine("3. 전체회복  (팀 전체 " + HealTeamPower + " 회복)");
        }

        private void FireBallSkill(Creature monster) {
            Console.WriteLine(Name + " 가 파이어볼 공격");
            BattleGame.battlePowerResult.AddDamage(IsHero, monster.TakeDamage(AttackPower + FireBallPower));
        }

        private void LightningSkill(Creature monster) {
            Console.WriteLine(Name + " 가 라이트닝 공격");
            BattleGame.battlePowerResult.AddDamage(IsHero, monster.TakeDamage(AttackPower + LightningPower));
        }
    }
}
