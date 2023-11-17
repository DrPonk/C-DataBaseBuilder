using AssignmentOleg.DetailFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssignmentOleg
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : Window
    {
        // Creating and declaring List and DataAcess Class to hold and store data
        DataAcess data = new DataAcess();
        // Declaring a List for each Combo Boxes
        List<TeamDetail> teamList1;
        List<TeamDetail> teamList2;
        List<GamesPlayDetail> gameList;
        List<EventDetail> eventList;
        // make new List to store the data from the query
        List<ResultDetail> resultList = new List<ResultDetail>();

        //Constructor for the Class ResultView
        public ResultView()
        {
            InitializeComponent();
            UpdateDataGrid();

            // Retrieving all TeamDetail and put them into teamList
            teamList1 = data.GetAllTeamDetail();
            // Setting the item source for cboTeam1
            cboTeam1.ItemsSource = teamList1;
            // Selected display (what is shown) in cboTeam1
            cboTeam1.DisplayMemberPath = "TeamName";
            // where to get the value for cboTeam1
            cboTeam1.SelectedValuePath = "TeamID";

            teamList2 = data.GetAllTeamDetail();
            cboTeam2.ItemsSource = teamList2;
            cboTeam2.DisplayMemberPath = "TeamName";
            cboTeam2.SelectedValuePath = "TeamID";

            gameList = data.GetGame();
            cboGame.ItemsSource = gameList;
            cboGame.DisplayMemberPath = "GameName";
            cboGame.SelectedValuePath = "GameID";

            eventList = data.GetAllEvent();
            cboEvent.ItemsSource = eventList;
            cboEvent.DisplayMemberPath = "EventName";
            cboEvent.SelectedValuePath = "EventID";

        }
        private void UpdateDataGrid()
        {
            resultList = data.GetAllResult();
            dgvResult.ItemsSource = resultList;
            dgvResult.Items.Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // checking if there are any item selected within the data grid 
            if (dgvResult.SelectedIndex < 0)
            {
                // if no item are selected return the method
                return;
            }
            // get the ID from the selected Index
            int id = resultList[dgvResult.SelectedIndex].ResultID;
            // confirmation for deletion of Index/Item
            MessageBoxResult reponse = MessageBox.Show("Are you sure you want to delete?", "Delete Confirmation", MessageBoxButton.YesNo);
            // if user response is yes, delete the result record with along with the ID
            if (reponse == MessageBoxResult.Yes)
            {
                data.DeleteResult(id);
                UpdateDataGrid();
            }
        }

        private void SAVE_Click(object sender, RoutedEventArgs e)
        {
            if (cboTeam1.SelectedItem != cboTeam2.SelectedItem)
            {
                if (AreFieldFilledCorrectly() == false)
                {
                    MessageBox.Show("All option must be selected before saving.");
                    return;
                }

                ResultList currentResult = new ResultList();
                int teamName = ((TeamDetail)cboTeam1.SelectedItem).TeamID;
                int opposingTeamName = ((TeamDetail)cboTeam2.SelectedItem).TeamID;
                int eventHeld = ((EventDetail)cboEvent.SelectedItem).EventID;
                int gamePlayed = ((GamesPlayDetail)cboGame.SelectedItem).GameID;

                currentResult.teamID = teamName;
                currentResult.opposingTeamID = opposingTeamName;
                currentResult.eventID = eventHeld;
                currentResult.gameID = gamePlayed;

                if (rbtnWinner.IsChecked == true)
                {
                    currentResult.FinalResult = "Win";
                    TeamDetail team1 = (TeamDetail)cboTeam1.SelectedItem;
                    team1.CompetitionPoint += 2;
                    data.UpdateTeamDetails(team1);
                }
                if (rbtnWinner2.IsChecked == true)
                {
                    currentResult.FinalResult = "Loss";
                    TeamDetail team2 = (TeamDetail)cboTeam2.SelectedItem;
                    team2.CompetitionPoint += 2;
                    data.UpdateTeamDetails(team2);
                }
                if (rbtnDraw.IsChecked == true)
                {
                    currentResult.FinalResult = "Draw";
                    TeamDetail team1 = (TeamDetail)cboTeam1.SelectedItem;
                    TeamDetail team2 = (TeamDetail)cboTeam2.SelectedItem;
                    team1.CompetitionPoint += 1;
                    team2.CompetitionPoint += 1;
                    data.UpdateTeamDetails(team1);
                    data.UpdateTeamDetails(team1);
                }
                data.AddResult(currentResult);
                UpdateDataGrid();
            }
            else
            {
                MessageBox.Show("Team 1 and Team 2 can't be the same or null.");
            }
        }

        private bool AreFieldFilledCorrectly()
        {
            if (cboTeam1.SelectedIndex == -1)
            {
                return false;
            }
            if (cboTeam2.SelectedIndex == -1)
            {
                return false;
            }
            if (cboGame.SelectedIndex == -1)
            {
                return false;
            }
            if (cboEvent.SelectedIndex == -1)
            {
                return false;
            }
            if (rbtnWinner.IsChecked == false && rbtnDraw.IsChecked == false && rbtnWinner2.IsChecked == false)
            {
                return false;
            }

            return true;
        }

        // datagrid select for update data
        private void dgvResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvResult.SelectedIndex < 0)
            {
                return;
            }

        }
    }
}
