using AssignmentOleg.DetailFolder;
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
    /// Interaction logic for Event.xaml
    /// </summary>
    public partial class Event : Window
    {
        DataAcess data = new DataAcess();

        List<EventDetail> eventList = new List<EventDetail>();

        bool isNewEntry = true;
        
        public Event()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            eventList = data.GetAllEvent();
            dgvEvent.ItemsSource = eventList;
            dgvEvent.Items.Refresh();
        }

        private void ClearDataEntryField()
        {
            txtId.Text = string.Empty;
            txtEventName.Text = string.Empty;
            txtEventLocation.Text = string.Empty;
            pkrEventDate.SelectedDate = DateTime.Today;

            isNewEntry = true;
        }

        private bool IsFormFilledCorrectly()
        {
            if (String.IsNullOrWhiteSpace(txtEventName.Text))
            { 
                return false;
            }
            if (String.IsNullOrWhiteSpace(txtEventLocation.Text))
            {
                return false;
            }
            if (pkrEventDate.SelectedDate == null || pkrEventDate.SelectedDate > DateTime.Today)
            {
                return false;
            }
            return true;
        }

        private void dgvEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvEvent.SelectedIndex == -1)
            {
                return;
            }

            int id = eventList[dgvEvent.SelectedIndex].EventID;

            EventDetail eventGrid = data.GetEventByID(id);
            txtId.Text = eventGrid.EventID.ToString();
            txtEventName.Text = eventGrid.EventName.ToString();
            txtEventLocation.Text= eventGrid.EventLocation.ToString();
            pkrEventDate.SelectedDate = eventGrid.EventDate;

            isNewEntry = false;
        
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsFormFilledCorrectly() == false)
            {
                MessageBox.Show("Make sure all form are filled");
                return;
            }

            EventDetail eventSave = new EventDetail();
            eventSave.EventName = txtEventName.Text;
            eventSave.EventLocation = txtEventLocation.Text;
            eventSave.EventDate = pkrEventDate.SelectedDate.Value;

            if (isNewEntry)
            {
                data.AddEventDetail(eventSave);
            }

            else
            {
                eventSave.EventID = int.Parse(txtId.Text);

                data.UpdateEvent(eventSave);
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
            if (dgvEvent.SelectedIndex < 0)
            {
                return;
            }

            int Id = eventList[dgvEvent.SelectedIndex].EventID;
            MessageBoxResult response = MessageBox.Show("Delete this event?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (response == MessageBoxResult.Yes)
            {
                data.DeleteEvent(Id);
                ClearDataEntryField();
                UpdateDataGrid();
                
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsFormFilledCorrectly() == false)
            {
                MessageBox.Show("Please fill out all form");
                return;
            }
            if (dgvEvent.SelectedItem == null)
            {
                MessageBox.Show("Please Select Event to Update");
                return;
            }

            EventDetail eventUpdate = (EventDetail)dgvEvent.SelectedItem;

            eventUpdate.EventName = txtEventName.Text;
            eventUpdate.EventLocation = txtEventLocation.Text;
            eventUpdate.EventDate = pkrEventDate.SelectedDate.Value;

            data.UpdateEvent(eventUpdate);
            ClearDataEntryField() ;
            UpdateDataGrid();

        }
    }
}
