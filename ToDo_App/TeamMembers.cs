using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App
{
    internal class TeamMembers
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }

        public List<TeamMembers> TeamMembersList { get; set; }

        public TeamMembers(string firstName, string lastName, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        public TeamMembers()
        {
            TeamMembersList = new List<TeamMembers>()
        {
            new TeamMembers("Ahmet", "Yılmaz", 1234),
            new TeamMembers("Ayşe", "Kara", 5678),
            new TeamMembers("Mehmet", "Güneş", 9101),
            new TeamMembers("Fatma", "Demir", 2345)
        };
        }
    }

}
