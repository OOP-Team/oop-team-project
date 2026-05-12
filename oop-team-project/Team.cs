using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace oop_team_project
{
    internal class Team<T> where T : Creature {
        private List<T> members;

        public List<T> Members {
            get { 
                return members; 
            }
            set { 
                members = value; 
            }
        }

        public Team() {
            Members = new List<T>();
        }

        public T this[int index] => members[index];

        public void AddMember(T creature) {
            Members.Add(creature);
        }

        public bool IsAllDead() {
            return Members.All(m => m.IsDead);
        }
    }
}