using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace oop_team_project
{
    internal class BattleGame
    {
        private Team heroTeam;
        private Team monsterTeam;
        private Random random;
        private int round = 1;
        private static int GetSafeInput()
        {
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("숫자만 입력 가능합니다. (0으로 처리)");
                return 0;
            }
            catch (OverflowException)
            {
                Console.WriteLine("너무 큰 숫자입니다. (0으로 처리)");
                return 0;
            }
        }

        public void LogInfo<T>(T data)
        {
            Console.WriteLine("LOG " + data.ToString());
        }
        public void LogInfo(string msg, int level) { Console.WriteLine("LV" + level + " LOG: " + msg); }

        public BattleGame()
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


            foreach (Creature hero in heroTeam.Members)
            {
                hero.OnDead = NotificationDeath;
            }
            foreach (Creature monster in monsterTeam.Members)
            {
                monster.OnDead = NotificationDeath;
            }
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
                Console.WriteLine();
                Console.WriteLine( "<" + round + " 라운드>");
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
            round++;

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

                int currentHp;
                int currentAtk;
                myTeam[0].GetQuickStats(out currentHp, out currentAtk);
                LogInfo("현재 " + myTeam[0].Name + " 상태 - HP: " + currentHp, 1);

                Console.WriteLine("캐릭터를 선택해주세요");
                int attackerIndex = GetSafeInput() - 1; // [GetSafeInput 참조 발생]

                Creature attacker = myTeam[attackerIndex];
                attacker.ShowSkills();

                Console.Write("스킬 입력 : ");
                int skill = GetSafeInput();

                Console.WriteLine("공격 대상 선택");
                int targetIndex = GetSafeInput() - 1;
                Creature target = enemyTeam[targetIndex];

                Random r = new Random();
                if (r.Next(0, 100) < 20)
                {
                    target.TakeDamage(attacker.AttackPower, "20% 확률로 크리티컬");
                }
                else
                {
                    attacker.UseSkill(skill, target);
                }

                attacker.Heal(200, "20% 확률로 히어로팀 200 힐");
            }
            catch (Exception ex)
            {
                Console.WriteLine("에러 발생: " + ex.Message);
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

        private void NotificationDeath(Creature deathCharacter)
        {
            Console.WriteLine("\n<알림>" + deathCharacter.Name + "게임에서 제외\n");
        }
    }
}