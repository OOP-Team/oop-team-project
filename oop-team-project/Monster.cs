using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal abstract class Monster : Creature {
        protected Monster(string name, int hp, int attack, double defense)
            : base(name, hp, attack, defense) {}
    }
}
