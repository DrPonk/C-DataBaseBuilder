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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTeam_Click(object sender, RoutedEventArgs e)
        {
            Team teamWindow = new Team();
            teamWindow.ShowDialog();
        }

        private void btnEvent_Click(object sender, RoutedEventArgs e)
        {
            Event eventWindow = new Event();
            eventWindow.ShowDialog();
        }

        private void btnGame_Click(object sender, RoutedEventArgs e)
        {
            Game gameWindow = new Game();
            gameWindow.ShowDialog();
        }

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            ResultView gameResult = new ResultView();
            gameResult.ShowDialog();

            ResultView resultView = new ResultView();
            resultView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            resultView.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
