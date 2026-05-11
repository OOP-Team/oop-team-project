using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal class SkillException : Exception
    {
        public SkillException(string message)
            : base(message) {}
    }
}
