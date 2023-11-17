using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentOleg.DetailFolder
{
    public class TeamDetail
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string PrimaryContact { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string CompetitionPoint { get; set; } = string.Empty; // int this when all panel are working 

        public TeamDetail()
        {

        }

        public TeamDetail(int id, string name, string contact, string phone, string point)
        {
            TeamID = id;
            TeamName = name;
            PrimaryContact = contact;
            ContactPhone = phone;
            CompetitionPoint = point;

        }
    }
}
