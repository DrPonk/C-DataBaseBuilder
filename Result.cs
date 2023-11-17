using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentOleg.DetailFolder
    // create a seperate ID class for result
{
    public class ResultList
    {
        public int iD { get; set; }

        public int teamID { get; set; }

        public int opposingTeamID { get; set; }

        public int eventID { get; set; }

        public int gameID { get; set; }

        public int resultID { get; set; }

        public string FinalResult { get; set; } = string.Empty; 

        public ResultList()
        { 
        
        }

        public ResultList(int id, string finalResult)
        {
            iD = id;
            FinalResult = finalResult;  
        }

        public ResultList(int TeamID, int OpposingTeamID, int EventID, int GameID, int ResultID)
        { 
            teamID = TeamID;
            opposingTeamID = OpposingTeamID;
            eventID = EventID;
            gameID = GameID;    
            resultID = ResultID;
        }

    }
}
