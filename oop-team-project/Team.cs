using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace oop_team_project
{
    internal class Team
    {
        private List<Creature> members;

        public List<Creature> Members {
            get { 
                return members; 
            }
            set { 
                members = value; 
            }
        }

        public Team() {
            Members = new List<Creature>();
        }

        public Creature this[int index] => members[index];

        public void AddMember(Creature creature) {
            Members.Add(creature);
        }

        public bool IsAllDead() {
            return Members.All(m => m.IsDead);
        }
    }
}