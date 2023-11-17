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
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {

        DataAcess data = new DataAcess();

        List<GamesPlayDetail> gameList = new List<GamesPlayDetail>();

        bool isNewEntry = true;
        public Game()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            gameList = data.GetGame();
            dgvGame.ItemsSource = gameList;
            dgvGame.Items.Refresh();
        }


        private void ClearDataEntryField()
        {
            txtId.Text = string.Empty;
            txtGameName.Text = string.Empty;
            cbo_GameType.SelectedIndex = -1;
            cbo_GameType.Text = string.Empty;
            isNewEntry = true;
        }

        private bool IsFormFilledCorrectly()
        {
            if (String.IsNullOrWhiteSpace(txtGameName.Text))
            {
                return false;
            }
            if (String.IsNullOrWhiteSpace(cbo_GameType.Text))
            {
                return false;
            }
            return true;

        }

        private void dgvGame_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvGame.SelectedIndex < 0)
            {
                return;
            }

            int id = gameList[dgvGame.SelectedIndex].GameID;

            GamesPlayDetail gamesEntry = data.GetGameByID(id);

            txtId.Text = gamesEntry.GameID.ToString();
            txtGameName.Text = gamesEntry.GameName;
            cbo_GameType.Text = gamesEntry.GameType;
            isNewEntry = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsFormFilledCorrectly() == false)
            {
                MessageBox.Show("Please make sure all field is fill");
                return;
            }

            GamesPlayDetail gameSave = new GamesPlayDetail();
            gameSave.GameName = txtGameName.Text;

            ComboBoxItem selectedItem = (ComboBoxItem)cbo_GameType.SelectedItem;
            gameSave.GameType = selectedItem.Content.ToString();

            if (isNewEntry == true)
            {
                data.AddGameDetail(gameSave);
            }
            else
            {
                gameSave.GameID = int.Parse(txtId.Text);
                data.UpdateGame(gameSave);
            }

            ClearDataEntryField();
            UpdateDataGrid();
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearDataEntryField();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgvGame.SelectedIndex < 0)
            {
                return;
            }

            int id = gameList[dgvGame.SelectedIndex].GameID;

            MessageBoxResult result = MessageBox.Show("Delete Entry?", "Delete Confirm?", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                data.DeleteGame(id);

                ClearDataEntryField();
                UpdateDataGrid();

                MessageBox.Show("Delete Complete");

            }
        }
        private void btnUPDATE_Click(object sender, RoutedEventArgs e)
        {
            if (IsFormFilledCorrectly() == false)
            {
                MessageBox.Show("Please Fill Out Correct Before Updating");
                return;
            }
            if (dgvGame.SelectedIndex < 0)
            {
                MessageBox.Show("Select Game to Update");
                return;
            }

            GamesPlayDetail gameUpdate = (GamesPlayDetail)dgvGame.SelectedItem;
            gameUpdate.GameName = txtGameName.Text;

            ComboBoxItem selectedType = (ComboBoxItem)cbo_GameType.SelectedItem;
            gameUpdate.GameType = selectedType.Content.ToString();

            data.UpdateGame(gameUpdate);
            ClearDataEntryField() ;
            UpdateDataGrid();

        }
    }
}
