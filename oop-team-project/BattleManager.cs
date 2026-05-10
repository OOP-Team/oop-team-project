using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace oop_team_project
{
    internal class BattleManager
    {
        private Team heroTeam;
        private Team monsterTeam;
        private Random random;

        public BattleManager()
        {
            random = new Random();
            heroTeam = new Team();
            monsterTeam = new Team();

            heroTeam.AddMember(new SwordHero("검사"));
            heroTeam.AddMember(new MageHero("마법사"));
            heroTeam.AddMember(new TankHero("탱커"));

            monsterTeam.AddMember(new Chimera());
            monsterTeam.AddMember(new BossMonster());
            monsterTeam.AddMember(new Dragon());
        }

        public void StartGame()
        {
            Console.WriteLine("무슨 팀을 선택하시겠습니까?");
            Console.WriteLine("1. 용사팀");
            Console.WriteLine("2. 보스팀");
            Console.Write("팀 선택 (숫자입력) : ");
            int select = int.Parse(Console.ReadLine());

            while (!heroTeam.IsAllDead() && !monsterTeam.IsAllDead())
            {
                if (select == 1)
                {
                    PlayerTurn(heroTeam, monsterTeam);
                    EnemyTurn(monsterTeam, heroTeam);
                }
                else
                {
                    PlayerTurn(monsterTeam, heroTeam);
                    EnemyTurn(heroTeam, monsterTeam);
                }
            }

            if (heroTeam.IsAllDead())
            {
                Console.WriteLine("몬스터팀 승리!");
            }
            else
            {
                Console.WriteLine("용사팀 승리!");
            }
        }

        private void PlayerTurn(Team myTeam, Team enemyTeam)
        {
            try
            {
                ShowStatus();

                Console.WriteLine("공격할 캐릭터를 선택해주세요.");
                Console.WriteLine("캐릭터 목록");

                for (int i = 0; i < myTeam.Members.Count; i++)
                {
                    Creature targets = enemyTeam.Members[i];
                    Console.WriteLine(
                        (i + 1) + ". " + targets.Name + " HP : " + targets.CurrentHp + "/" + targets.MaxHp
                    );
                }

                int attackerIndex = int.Parse(Console.ReadLine()) - 1;

                Creature attacker = myTeam.Members[attackerIndex];

                Console.WriteLine("스킬 선택");

                attacker.ShowSkills();

                int skill = int.Parse(Console.ReadLine());

                Console.WriteLine("공격 대상 선택");

                for (int i = 0; i < enemyTeam.Members.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {enemyTeam.Members[i].Name}");
                }

                int targetIndex = int.Parse(Console.ReadLine()) - 1;

                Creature target = enemyTeam.Members[targetIndex];

                attacker.UseSkill(skill, target);
            }
            catch (FormatException)
            {
                Console.WriteLine("숫자를 입력해주세요.");
            }
            catch (InvalidSkillException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("차례 종료");
            }
        }

        private void EnemyTurn(Team enemyTeam, Team targetTeam)
        {
            Console.WriteLine("적 차례입니다.");

            Creature attacker = enemyTeam.Members
                .Where(m => !m.IsDead)
                .OrderBy(x => random.Next())
                .First();

            Creature target = targetTeam.Members
                .Where(m => !m.IsDead)
                .OrderBy(x => random.Next())
                .First();

            attacker.UseSkill(random.Next(1, 4), target);
        }

        private void ShowStatus()
        {
            Console.WriteLine("<HERO TEAM>");

            foreach (Creature hero in heroTeam.Members)
            {
                hero.ShowStatus();
            }

            Console.WriteLine();

            Console.WriteLine("<MONSTER TEAM>");

            foreach (Creature monster in monsterTeam.Members)
            {
                monster.ShowStatus();
            }

            Console.WriteLine();
        }
    }
}