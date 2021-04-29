using System.Windows;
using System.Diagnostics;
using Air3550.Data;
using Microsoft.EntityFrameworkCore;

using Air3550.Models;
using static Air3550.Constants.AppConstants;
using static Air3550.Utils.FlightUtil;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Sqlite;
using Air3550.Services;
using System;
using Air3550.Utils;

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
            
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            MainFrame.Navigate(new SignInPage());
        }
    }
}
