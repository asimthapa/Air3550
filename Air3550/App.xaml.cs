using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Air3550.Data;
using Air3550.Models;
using Air3550.Services;
using Air3550.Utils;
using Air3550.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Air3550
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly DataSeedUtil dataSeedUtil = new();
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using var db = new ApplicationDbContext();
            db.Database.Migrate();
            dataSeedUtil.SeedInitData();
            dataSeedUtil.SeedDefaultActors();
            UserUtil.ClearLoggedUser();
        }
    }
}
