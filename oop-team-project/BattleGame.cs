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

        public BattleGame()
        {
            random = new Random();
            heroTeam = new Team();
            monsterTeam = new Team();

            heroTeam.AddMember(new SwordHero("검사"));
            heroTeam.AddMember(new MageHero("마법사"));
            heroTeam.AddMember(new TankHero("탱커"));
                
            monsterTeam.AddMember(new Chimera("키메라"));
            monsterTeam.AddMember(new BossMonster("보스"));
            monsterTeam.AddMember(new Dragon("드래곤"));


            foreach (Creature hero in heroTeam.Members) {
                hero.OnDead = NotificationDeath;
            }
            foreach (Creature monster in monsterTeam.Members) {
                monster.OnDead = NotificationDeath;
            }
        }

        public void StartGame()
        {
            try {
                Random luckyHeal = new Random();

                Console.WriteLine("무슨 팀을 선택하시겠습니까?");
                Console.WriteLine("1. 용사팀");
                Console.WriteLine("2. 보스팀");
                Console.Write("팀 선택 (숫자입력) : ");

                int select = int.Parse(Console.ReadLine());

                while (!heroTeam.IsAllDead() && !monsterTeam.IsAllDead()) {
                    Console.WriteLine();
                    Console.WriteLine("<" + round + " 라운드>");

                    if (select == 1) {
                        PlayerTurn(heroTeam, monsterTeam);
                        EnemyTurn(monsterTeam, heroTeam);
                    } else  {
                        PlayerTurn(monsterTeam, heroTeam);
                        EnemyTurn(heroTeam, monsterTeam);
                    }

                    if (luckyHeal.Next(0, 100) < 20) {
                        Console.WriteLine("\n<마법사의 패시브 발동! 용사팀 전체 200 회복>");

                        foreach (Creature hero in heroTeam.Members) {
                            if (!hero.IsDead) {
                                hero.Heal(200, "마법사의 패시브");
                            }
                        }
                    }

                    heroTeam.Members.ForEach(m => m.UpdatePoisonStatus());

                    foreach (Creature member in heroTeam.Members) {
                        if (member is TankHero tank) {
                            tank.ResetTaunt();
                            tank.IsDefenseMode = false;
                        }
                    }

                    round++;
                }
                if (heroTeam.IsAllDead()) {
                    Console.WriteLine("몬스터팀 승리!");
                } else {
                    Console.WriteLine("용사팀 승리!");
                }
            }
            catch (SkillException ex) {
                Console.WriteLine("스킬 오류 발생: " + ex.Message);
            }
            catch (Exception ex) {
                Console.WriteLine("게임 진행 중 오류 발생: " + ex.Message);
            }
            finally {
                Console.WriteLine("\n게임 시스템 종료");
            }
        }

        public void PlayerTurn(Team userTeam, Team enemyTeam) {
            try {
                ShowStatus();

                while (true) {
                    Console.WriteLine("\n[1] 공격하기  [2] 전장 전체 체력 순위 보기  [3] 상세 상태 보기");
                    Console.Write("메뉴 선택 : ");
                    int menuInput = GetSafeInput();

                    if (menuInput == 2) {
                        List<Creature> allCharacters = heroTeam.Members
                            .Concat(monsterTeam.Members)
                            .ToList();

                        allCharacters.Sort();

                        Console.WriteLine("\n<전체 캐릭터 체력 순위>");
                        allCharacters.ForEach(c => {
                            string teamName = (c is Hero) ? "용사팀" : "몬스터팀";
                            string lifeStatus = c.IsDead ? "사망" : c.CurrentHp + " / " + c.MaxHp;
                            Console.WriteLine("[" + teamName + "] " + c.Name + " : " + lifeStatus);
                        });
                        Console.WriteLine();
                        continue;
                    } else if (menuInput == 3) {
                        Console.WriteLine("\n<HERO TEAM 상세 상태>");

                        foreach (Creature hero in heroTeam.Members) {
                            hero.ShowStatus(true);
                            Console.WriteLine();
                        }

                        Console.WriteLine("\n<MONSTER TEAM 상세 상태>");

                        foreach (Creature monster in monsterTeam.Members) {
                            monster.ShowStatus(true);
                            Console.WriteLine();
                        }

                        continue;
                    }
                    else if (menuInput == 1) {
                        break;
                    }
                    else {
                        Console.WriteLine("\n잘못된 번호입니다. 다시 입력해주세요.");
                    }
                }

                int currentHp;
                int currentAttack;
                userTeam[0].GetQuickStats(out currentHp, out currentAttack);

                Console.WriteLine("\n공격할 캐릭터를 선택해주세요 (1, 2, 3)");
                Console.Write("입력 : ");
                int attackerIndex = GetSafeInput() - 1;

                if (attackerIndex < 0 || attackerIndex >= userTeam.Members.Count) {
                    Console.WriteLine("\n범위를 벗어난 입력입니다. 첫 번째 캐릭터로 진행합니다.");
                    attackerIndex = 0;
                }

                Creature attacker = userTeam[attackerIndex];

                if (attacker.IsDead) {
                    Console.WriteLine(attacker.Name + " 이미 사망하여 공격할 수 없습니다.");
                    return;
                }

                Console.WriteLine();
                attacker.ShowSkills();

                Console.Write("사용할 스킬 입력 : ");
                Random r = new Random();
                int skill = GetSafeInput();
                int enemyIdx = GetSafeInput() - 1;

                if (attacker is TankHero && (skill == 1 || skill == 2)){
                    attacker.UseSkill(skill, attacker);
                }
                else if (attacker is MageHero mage && skill == 3){
                    mage.HealTeam(userTeam);
                }
                else {
                    Console.Write("\n공격 대상 선택: ");
                    
                    if (enemyIdx < 0 || enemyIdx >= enemyTeam.Members.Count){
                        Console.WriteLine("잘못된 대상 선택입니다.");
                        return;
                    }

                    Creature target = enemyTeam[enemyIdx];

                    if (r.Next(0, 100) < 20){
                        target.TakeDamage(attacker.AttackPower, "\n20% 확률 크리티컬 공격!\n");
                    }
                    else{
                        attacker.UseSkill(skill, target);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine("플레이어 턴 실행 중 오류 발생: " + ex.Message);
            } finally {
                Console.WriteLine("\n플레이어 턴 종료");
            }
        }

        private void EnemyTurn(Team enemyTeam, Team userTeam)
        {
            try
            {
                if (enemyTeam.IsAllDead()) {
                    return;
                }

                Console.WriteLine("\n적 차례입니다.");

                Creature enemyAttacker = enemyTeam.Members
                    .Where(m => !m.IsDead)
                    .OrderBy(x => random.Next())
                    .First();

                Creature target = null;

                foreach (Creature member in userTeam.Members) {
                    if (!member.IsDead && member is TankHero tank) {
                        if (tank.IsTaunting) {
                            target = tank;
                            break;
                        }
                    }
                }

                if (target != null) {
                    Console.WriteLine("\n<도발 효과!> "
                        + enemyAttacker.Name
                        + "이(가) 도발 중인 "
                        + target.Name
                        + "를 공격합니다!");
                } else {
                    target = userTeam.Members
                        .Where(m => !m.IsDead)
                        .OrderBy(x => random.Next())
                        .First();
                }

                enemyAttacker.UseSkill(random.Next(1, 4), target);
            }
            catch (InvalidOperationException) {
                Console.WriteLine("공격 가능한 대상이 없습니다.");
            }
            catch (SkillException ex) {
                Console.WriteLine("몬스터 스킬 오류: " + ex.Message);
            }
            catch (Exception ex) {
                Console.WriteLine("EnemyTurn 오류 발생: " + ex.Message);
            }
            finally {
                Console.WriteLine("적 턴 종료");
            }
        }

        private static int GetSafeInput()
        {
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("숫자만 입력 가능합니다.");
                return 0;
            }
            catch (OverflowException)
            {
                Console.WriteLine("너무 큰 숫자입니다.");
                return 0;
            }
        }

        private void ShowStatus() {
            Console.WriteLine("<HERO TEAM>");
            foreach (Creature hero in heroTeam.Members) {
                hero.ShowStatus();
            }

            Console.WriteLine();

            Console.WriteLine("<MONSTER TEAM>");
            foreach (Creature monster in monsterTeam.Members) {
                monster.ShowStatus();
            }
             
            Console.WriteLine();
        }

        private void NotificationDeath(Creature deathCharacter) {
            Console.WriteLine("\n<알림>" + deathCharacter.Name + "게임에서 제외\n");
        }
    }
}