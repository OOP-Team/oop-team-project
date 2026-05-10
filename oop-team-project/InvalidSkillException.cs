using System;
using System.Collections.Generic;
using System.Text;

namespace oop_team_project
{
    internal class InvalidSkillException : Exception
    {
        public InvalidSkillException(string message)
            : base(message) {}
    }
}
