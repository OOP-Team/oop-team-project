using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal abstract class Hero : Creature {
        public Hero(string name, int addHp, int addAttack, double addDefense)
                : base(name, addHp, addAttack, addDefense) {}
    }
}