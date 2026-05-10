using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace oop_team_project
{
    internal class Team
    {
        private List<Creature> members;

        public List<Creature> Members
        {
            get { return members; }
            private set { members = value; }
        }

        public Team()
        {
            Members = new List<Creature>();
        }

        // [인덱서] 이제 team[0] 이렇게 접근 가능
        public Creature this[int index] => members[index];

        // [람다 활용 추가]
        public void ShowAllStatus() => members.ForEach(m => m.ShowStatus());

        // [out 키워드 2번째]
        public void GetTeamInfo(out int alive, out int total)
        {
            alive = members.Count(m => !m.IsDead);
            total = members.Count;
        }

        public void AddMember(Creature creature)
        {
            Members.Add(creature);
        }

        public bool IsAllDead()
        {
            return Members.All(m => m.IsDead);
        }
    }
}