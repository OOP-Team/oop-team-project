using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal abstract class Hero : Creature
    {
        protected const int BASE_HP = 1000;
        protected const int BASE_ATTACK = 50;
        protected const double BASE_DEFENSE = 0.1;
        public Hero(string name, int heroHp, int heroAttack, double heroDefense)
                    : base(name, 0, 0, 0)
        {
            SetMaxHp(BASE_HP + heroHp);
            SetCurrentHp(GetMaxHp());
            SetAttackPower(BASE_ATTACK + heroAttack);
            SetDefenseRate(BASE_DEFENSE + heroDefense);
        }
    }
}
