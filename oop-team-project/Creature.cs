using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal abstract class Creature
    {
        private string name;
        private int currentHp;
        private int maxHp;
        private int attackPower;
        private double defenseRate;

        public Creature(string name, int maxHp, int attackPower, double defenseRate)
        {
            this.name = name;
            this.maxHp = maxHp;
            this.currentHp = maxHp;
            this.attackPower = attackPower;
            this.defenseRate = defenseRate;
        }
        public string GetName()
        {
            return name;
        }

        public int GetCurrentHp()
        {
            return currentHp;
        }

        public int GetMaxHp()
        {
            return maxHp;
        }

        public int GetAttackPower()
        {
            return attackPower;
        }

        public double GetDefenseRate()
        {
            return defenseRate;
        }

        public void SetName(string name) { 
            this.name = name; 
        }
        public void SetCurrentHp(int hp)
        {
            currentHp += hp;

            if (currentHp < 0)
            {
                currentHp = 0;
            }
            else if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }
        }

        public void SetMaxHp(int maxHp) {
            this.maxHp = maxHp; 
        }
        public void SetAttackPower(int attackPower) { 
            this.attackPower = attackPower; 
        }
        public void SetDefenseRate(double defenseRate) { 
            this.defenseRate = defenseRate; 
        }

        public abstract void ShowStatus();
        public abstract void UseSkill(Creature target);
        public abstract void Attack(Creature target);
    }
}
