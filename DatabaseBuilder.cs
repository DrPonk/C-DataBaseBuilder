using Data_Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using AssignmentOleg.DetailFolder;

namespace AssignmentOleg
{
    public class DatabaseBuilder: DataAcess
    {
        public void DataBaseBuilder()
        {
            SqlConnection connection = Helper.CreateSQLConnection("Default");
            try
            {
                string connectionString = $"Data Source = {connection.DataSource}; Integrated Security = true;";

                string query = $"IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name ='{connection.Database}')" + $"CREATE DATABASE {connection.Database}";

                using (connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public bool DoTableExist()
        {
            using (var connection = Helper.CreateSQLConnection("Default")) 
            {
                string query = $"SELECT COUNT(*) FROM {connection.Database}.INFORMATION_SCHEMA.TABLES " +
                $"WHERE TABLE_TYPE = 'BASE TABLE'";

                int count = connection.QuerySingle<int>(query);

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        private void CreateTable(string name, string structure)
        {
            try
            {
                string query = $"CREATE TABLE {name} ({structure})";
                using (var connection = Helper.CreateSQLConnection("Default"))
                {
                    connection.Execute(query);
                }

            }
            catch(Exception e)
            {
            
            }
        }

        public void BuildDataBaseTable()
        { 
            BuildTeamTables();
            BuildGameDetailTable();
            BuildEventTable();
            BuildResultTable();
        }

        public void SeedDataBaseTable()
        {
            SeedTeamTable();
            SeedGameDetail();
            SeedEventTable();
            SeedResultTable();
        }
        private void BuildTeamTables()
        {
            string tableName = "TeamName";

            string tableStructured =
            "TeamID int IDENTITY (1,1) PRIMARY KEY, " +
            "TeamName VARCHAR(50) NOT NULL, " +
            "PrimaryContact VARCHAR(50) NOT NULL, " +
            "ContactPhone VARCHAR(50) NOT NULL, " +
            "CompetitionPoint VARCHAR(50) NOT NULL,";

            CreateTable("TeamName", tableStructured);
        }
        private void BuildGameDetailTable()
        {
            string tableName = "GameDetail";

            string tableStructured =
            "GameID int IDENTITY (1,1) PRIMARY KEY, " +
            "GameName VARCHAR(50) NOT NULL, " +
            "GameType VARCHAR(50) NOT NULL ";

            CreateTable("GameDetail", tableStructured);
        }
        private void BuildEventTable()
        {
            string tableName = "EventDetail";
            string tableStructured =
            "EventID int IDENTITY (1,1) PRIMARY KEY, " +
            "EventName VARCHAR(50) NOT NULL, " +
            "EventLocation VARCHAR(50) NOT NULL, " +
            "EventDate DateTime NOT NULL ";

            CreateTable("EventDetail", tableStructured);
        }
        private void BuildResultTable()
        {
            string tableName = "ResultDetail";
            string tableStructured =
            "iD int IDENTITY (1,1) PRIMARY KEY, " +
            "teamID int NOT NULL, " +
            "opposingTeamID int NOT NULL, " +
            "eventID int NOT NULL, " +
            "gameID int NOT NULL, " +
            "resultID int NOT NULL, " +
            "FinalResult VARCHAR(5) NOT NULL, " +
            "FOREIGN KEY (teamID) REFERENCES TeamName(TeamID), " +
            "FOREIGN KEY (opposingTeamID) REFERENCES TeamName(TeamID), " +
            "FOREIGN KEY (eventID) REFERENCES EventDetail(EventID), " +
            "FOREIGN KEY (gameID) REFERENCES GameDetail(GameID), ";

            CreateTable("ResultDetail", tableStructured);   
        }


        private void SeedTeamTable()
        {
            List<TeamDetail> teamDetails = new List<TeamDetail>
            {
                new TeamDetail
                {
                    TeamName = "TSM",
                    PrimaryContact = "Andy Dinh",
                    ContactPhone = "0423661734",
                    CompetitionPoint = "20"
                },
                new TeamDetail
                {
                  TeamName = "100 Thieves",
                  PrimaryContact = "Nadeshot",
                  ContactPhone = "0421669043",
                  CompetitionPoint = "20"
                }
            };

            foreach (var team in teamDetails)
            {
                AddTeamTable(team);
            }             
        }

        private void SeedGameDetail()
        {
            List<GamesPlayDetail> gamesDetail = new List<GamesPlayDetail>
            {
                new GamesPlayDetail
                {
                    GameName = "League of Legend",
                    GameType = "Mutiplayer"
                },

                new GamesPlayDetail
                { 
                    GameName = "CSGO",
                    GameType = "Multiplayer"
                },

                new GamesPlayDetail
                { 
                    GameName = "Apex Legend",
                    GameType = "Multiplayer"
                }
            };

            foreach (var game in gamesDetail)
            {
                AddGameDetail(game);
            }
        }

        private void SeedEventTable()
        {
            List<EventDetail> eventDetails = new List<EventDetail>
            {
                new EventDetail
                {
                    EventName = "World 2023",
                    EventDate = new DateTime(2023, 08, 12),
                    EventLocation = "Busan"
                },
                new EventDetail
                {
                    EventName = "CSGO Major",
                    EventDate = new DateTime(2023, 07, 11),
                    EventLocation = "Dallas"
                },
                 new EventDetail
                {
                    EventName = "ALGS",
                    EventDate = new DateTime(2023, 06, 21),
                    EventLocation = "Rio"
                }
            };

            foreach (var Event in eventDetails)
            {
                AddEventDetail(Event);
            }
        }

        private void SeedResultTable()
        {
            List<ResultList> resultList = new List<ResultList>();
         
                resultList.Add( new ResultList {teamID = 1, opposingTeamID = 2, eventID = 1, gameID = 2, FinalResult = "WIN" });
                resultList.Add(new ResultList  {teamID = 2, opposingTeamID = 1, eventID = 2, gameID = 2, FinalResult = "LOSE" });
                

            foreach (var result in resultList)
            {
                AddResult(result);
            }
        }

    }
}
