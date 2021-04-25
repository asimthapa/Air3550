/*using System.Windows;
using System.Diagnostics;
using Air3550.Data;
using Microsoft.EntityFrameworkCore;

using Air3550.Models;
using static Air3550.Constants.AppConstants;
using static Air3550.Utils.FlightUtils;

namespace Air3550.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using var db = new AppDBContext();
            db.Database.Migrate();
            DataSeed.SeedInitData();

            /*if (true)
            {
                //Debug.WriteLine("ASIM");
                //MainFrame.Navigate(new SignInPage());
            }

        }
    }
}
*/