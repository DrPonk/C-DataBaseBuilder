using AssignmentOleg.DetailFolder;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Team.xaml
    /// </summary>
    public partial class Team : Window
    {

        DataAcess data = new DataAcess();
        List<TeamDetail> teamList = new List<TeamDetail>();// multiple 
        TeamDetail teamDetail = new TeamDetail();// single
        bool isNewEntry = false;
        SaveMode saveType = SaveMode.NewSave;
        public Team()
        {
            InitializeComponent();
            UpDateDataGrid();
        }

        private void UpDateDataGrid()
        { 
            teamList = data.GetAllTeamDetail();
            dgvTeam.ItemsSource = teamList;
            dgvTeam.Items.Refresh();
        }

        // can save, can delete, cant edit yet for some reason (ask Oleg or others)
        private void dgvTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvTeam.SelectedIndex < 0)
            {
                return;
            }

            int id = teamList[dgvTeam.SelectedIndex].TeamID;
           
            TeamDetail teamDetail = data.GetTeamID(id);

            txtId.Text = teamDetail.TeamID.ToString();
            txtTeamName.Text = teamDetail.TeamName;
            txtPrimaryContact.Text = teamDetail.PrimaryContact;
            txtContactPhone.Text = teamDetail.ContactPhone;
            txtCompetitionPoints.Text = teamDetail.CompetitionPoint;
            bool isNewEntry = true;
        }

        private bool FilledFillOutCorrectly()
        {
            if (String.IsNullOrWhiteSpace(txtTeamName.Text))
            { 
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtPrimaryContact.Text))
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtContactPhone.Text))
            {
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtCompetitionPoints.Text))
            {
                return false;
            }

            return true;
        
        }

        private void ClearDataEntryField()
        {
            txtId.Text = string.Empty;
            txtTeamName.Text = string.Empty;
            txtPrimaryContact.Text = string.Empty;
            txtContactPhone.Text= string.Empty;
            txtCompetitionPoints.Text= string.Empty;

            isNewEntry = true;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearDataEntryField();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvTeam.SelectedIndex < 0)
            {
                MessageBox.Show("No row selected");
            }

            MessageBoxResult result = MessageBox.Show("Delete Entry?", "Delete Comfirm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                int id = teamList[dgvTeam.SelectedIndex].TeamID;

                data.DeleteTeamDetail(id);

                ClearDataEntryField();
                UpDateDataGrid();

                MessageBox.Show("Delete Complete");
            }
        }



        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (FilledFillOutCorrectly() == false)
            {
                MessageBox.Show("please make sure all field is fill");
                return;
            }
            TeamDetail teamDetail = new TeamDetail();
            // save all Textfield Entry to matching collumn values 
            teamDetail.TeamName = txtTeamName.Text;
            teamDetail.PrimaryContact = txtPrimaryContact.Text;
            teamDetail.ContactPhone = txtContactPhone.Text;
            teamDetail.CompetitionPoint = txtCompetitionPoints.Text;

            if (saveType == SaveMode.NewSave)
            {
                data.AddTeamTable(teamDetail);
            }

            else if (saveType == SaveMode.UpdateSave)
            {
                data.UpdateTeamDetails(teamDetail);
            }
            else 
            {
                return;
            }

            ClearDataEntryField() ;
            UpDateDataGrid();
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (FilledFillOutCorrectly() == false)
            {
                MessageBox.Show("Please fill form correctly before updating");
                return;
            }

            if (dgvTeam.SelectedItem == null)
            {
                MessageBox.Show("Select Item Update");
                return;
            }

            TeamDetail teamUpdate = (TeamDetail)dgvTeam.SelectedItem;

            teamUpdate.TeamName = txtTeamName.Text;
            teamUpdate.PrimaryContact = txtPrimaryContact.Text;
            teamUpdate.ContactPhone = txtContactPhone.Text;
            teamUpdate.CompetitionPoint = txtCompetitionPoints.Text;

            data.UpdateTeamDetails(teamUpdate);
            ClearDataEntryField();
            UpDateDataGrid();

        }
    }
}
