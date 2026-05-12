using System;
using System.Collections.Generic;
using System.Text;

internal class BattlePowerResult
{
    private int heroToMonsterDamage = 0;
    private int heroDamageTaken = 0;
    private int monsterToHeroDamage = 0;
    private int monsterDamageTaken = 0;

    public void GetHeroStatus(out int attack, out int taken) {
        attack = heroToMonsterDamage;
        taken = heroDamageTaken;
    }

    public void GetMonsterStatus(out int attack, out int taken) {
        attack = monsterToHeroDamage;
        taken = monsterDamageTaken;
    }

    public void AddDamage(bool heroAttack, int damage) {
        if (damage < 0) {
            throw new ArgumentException("데미지는 0 이상이어야 합니다.");
        }

        if (heroAttack) {
            heroToMonsterDamage += damage;
            monsterDamageTaken += damage;
        } else {
            monsterToHeroDamage += damage;
            heroDamageTaken += damage;
        }
    }

    public void PrintResult(){
        int heroAttack;
        int heroTaken;
        int monsterAttack;
        int monsterTaken;
        GetHeroStatus(out heroAttack, out heroTaken);
        GetMonsterStatus(out monsterAttack, out monsterTaken);

        Console.WriteLine("\n<최종 전투 결과>");
        Console.WriteLine("영웅팀 ->  공격한 피해량: " + heroAttack + " & 받은 피해량: " + heroTaken);
        Console.WriteLine("보스팀 ->  공격한 피해량: " + monsterAttack + " & 받은 피해량: " + monsterTaken);
    }
}