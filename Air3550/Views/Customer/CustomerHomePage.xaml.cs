using System;
using System.Windows.Controls;

namespace Air3550.Views
{
    /// <summary>
    /// Interaction logic for CustomerHomePage.xml
    /// </summary>
    public partial class CustomerHomePage : Page
    {
        public CustomerHomePage()
        {
            InitializeComponent();
            //Title.Text = $"Hello {App.LoggedUser.FirstName}";
        }

        private void OnBookFlight(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new BookFlightPage());
        }

        private void OnUpcomingFlights(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void OnFlightHistory(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}