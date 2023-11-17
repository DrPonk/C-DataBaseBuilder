using AssignmentOleg.DetailFolder;
using Dapper;
using Data_Management;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentOleg
{
   
    public class DataAcess
    {
        #region Team
        // get all team
        public List<TeamDetail> GetAllTeamDetail()
        {
            string query = "SELECT * FROM TeamName";
            using (var connection = Helper.CreateSQLConnection("Default"))
            { 
                return connection.Query<TeamDetail>(query).ToList();
            }
        }

        public void DeleteTeamDetail(int id)
        {
            string query = "DELETE FROM TeamName " + $"WHERE TeamID = {id}";

            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                connection.Execute(query);
            }
        }

        // get one team
        public TeamDetail GetTeamID (int id) 
        {
            try
            {
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = $"SELECT * FROM TeamName WHERE TeamID = {id}";

                    return connection.QuerySingle<TeamDetail>(query);
                }
            }
            catch (Exception ex)
            {
                return new TeamDetail();
            }
        }

        public void UpdateTeamDetails(TeamDetail teamUpdate)
        {
            string query = "UPDATE TeamName " +
                            "SET TeamName = @TeamName, PrimaryContact = @PrimaryContact, " +
                            "ContactPhone = @ContactPhone, " +
                            "CompetitionPoint = @CompetitionPoint " +
                            "WHERE TeamID = @TeamID ";
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                connection.Execute(query, teamUpdate);
            }
        
        }

        public void AddTeamTable(TeamDetail teamDetail)
        {
            try 
            {
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "INSERT INTO TeamName (TeamName, PrimaryContact,ContactPhone,CompetitionPoint)" +
                                   "VALUES (@TeamName, @PrimaryContact, @ContactPhone, @CompetitionPoint)";


                    connection.Execute(query, teamDetail);
                }
            }
            catch (Exception ex) 
            {
            
            }
        }
        #endregion

        #region Game
        public void AddGameDetail(GamesPlayDetail gamesPlayDetail)
        {
            try
            {
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "INSERT INTO GameDetail (GameName, GameType) " +
                                   "VALUES (@GameName, @GameType)";
                    

                    connection.Execute(query, gamesPlayDetail);

                }
            }
            catch (Exception ex)
            { 
            
            }       
        }

        public List<GamesPlayDetail> GetGame()
        {
            string query = "SELECT * From GameDetail";

            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                return connection.Query<GamesPlayDetail>(query).ToList();
            }
        }

        public GamesPlayDetail GetGameByID(int id)
        {
            string query = $"SELECT * FROM GameDetail WHERE GameID = {id}";
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                return connection.QuerySingle<GamesPlayDetail>(query);            
            }
        }

        public void UpdateGame(GamesPlayDetail gameEntry)
        {
            string query = "UPDATE GameDetail " +
                           "SET GameName = @GameName, GameType = @GameType " +
                           "WHERE GameID = @GameID";
                           
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                connection.Execute(query, gameEntry);
            }
        }

        public void DeleteGame(int id) 
        {
            string query = "DELETE FROM GameDetail " +
                           $"WHERE GameID = {id}";

            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                connection.Execute(query);
            }
        }
        #endregion

        #region Event
        public void AddEventDetail(EventDetail eventDetail)
        {
                
         string query = "INSERT INTO EventDetail (EventName, EventLocation, EventDate) " +
                        "VALUES (@EventName, @EventLocation, @EventDate)";

            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                connection.Execute(query, eventDetail);
            }         
        }
        public List<EventDetail> GetAllEvent()
        {
            string query = "SELECT * From EventDetail";

            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                return connection.Query<EventDetail>(query).ToList();
            }
        }

        public EventDetail GetEventByID(int id)
        {
            string query = $"SELECT * From EventDetail WHERE EventID = {id}";
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                return connection.QuerySingle<EventDetail>(query);
            }
        }

        public void UpdateEvent(EventDetail eventUpdate)
        {
            string query = "UPDATE EventDetail " + 
                           "SET EventName = @EventName, EventLocation = @EventLocation, EventDate = @EventDate " +
                           "WHERE EventID = @EventID";
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                connection.Execute(query, eventUpdate);
            }
        }

        public void DeleteEvent(int id) 
        {
            string query = "DELETE From EventDetail" +
                           $"WHERE EventID = {id}";
            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                connection.Execute(query);
            }       
        }
        #endregion

        #region Result
        public void AddResult(ResultList resultList)
        {
            try 
            {
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "INSERT INTO ResultDetail (teamID, opposingTeamID, eventID, gameID, resultID, FinalResult) " +
                                   "VALUES (@teamID, @teamID, @eventID, @gameID, @resultID, @FinalResult)";
                    connection.Execute(query, resultList);
                }
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public List<ResultDetail> GetAllResult()
        {
            try
            {
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    string query = "SELECT ResultDetail.iD, Team.TeamName AS Team, OpposingTeam.TeamName AS OpposingTeam, " +
                                   "GameDetail.GameName AS GamePlayed, " +
                                   "EventDetail.EventName AS EventHeld, " +
                                   "FinalResult AS FinalResult " +
                                   "FROM ResultDetail " +
                                   "INNER JOIN TeamName Team ON ResultDetail.TeamID = Team.teamID " +
                                   "INNER JOIN TeamName OpposingTeam ON ResultDetail.opposingTeamID = opposingTeam.TeamID " +
                                   "INNER JOIN GameDetail ON ResultDetail.gameID = GameDetail.GameID " +
                                   "INNER JOIN EventDetail ON ResultDetail.eventID = EventDetail.EventID ";

                    return connection.Query<ResultDetail>(query).ToList();
                }
            }

            catch (Exception e)
            {
                return new List<ResultDetail>();
            }
        
        }
        
        public ResultDetail GetResultByID(int id)
        {
            string query = $"SELECT * FROM ResultDetail WHERE ResultID = {id} ";

            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                return connection.QuerySingle<ResultDetail>(query);
            }

        }

        public void DeleteResult(int id)
        {
            string query = $"DELETE FROM ResultDetail " +
                           $"WHERE ResultID = {id}";

            using (var connection = Helper.CreateSQLConnection("Default"))
            {
                connection.Execute(query);
            }
        
        }

        #endregion

    }
}
