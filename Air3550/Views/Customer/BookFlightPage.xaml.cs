using Air3550.Models;
using Air3550.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Air3550.Views
{
    /// <summary>
    /// Interaction logic for BookFlightPage.xaml
    /// </summary>
    public partial class BookFlightPage : Page
    {
        private readonly FlightService flightService = new();
        private bool _isOneWay = false;

        public BookFlightPage()
        {
            InitializeComponent();
        }

        private void OnWayClick(object sender, RoutedEventArgs e)
        {
            _isOneWay = !_isOneWay;
            if (_isOneWay)
            {
                ArrivalDate.Visibility = Visibility.Hidden;
                OneWayButton.Content = "One Way";
            }
            else
            {
                ArrivalDate.Visibility = Visibility.Visible;
                OneWayButton.Content = "Two Way";
            }
        }

        private void OnSearchFlights(object sender, RoutedEventArgs e)
        {
            var departDate = DepartDate.SelectedDate;
            var returnDate = ReturnDate.SelectedDate;
            Debug.WriteLine(_isOneWay && departDate != null);

            if (_isOneWay && departDate != null)
            {
                FLightSearchError.Visibility = Visibility.Collapsed;
                List<Flight> departFlights = flightService.GetFlights(SourceCity.Text, DestinationCity.Text, departDate.Value);
            }
            else if (!_isOneWay && departDate != null && returnDate != null)
            {
                FLightSearchError.Visibility = Visibility.Collapsed;
                List<Flight> departFlights = flightService.GetFlights(SourceCity.Text, DestinationCity.Text, departDate.Value);
                List<Flight> returnFlights = flightService.GetFlights(DestinationCity.Text, SourceCity.Text, returnDate.Value);
            }
            else { FLightSearchError.Visibility = Visibility.Visible; }

        }
    }
}
