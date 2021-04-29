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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Air3550.Constants;
using Air3550.Services;
using Air3550.Models;
using Air3550.Utils;

namespace Air3550.Views
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        private readonly InputValidationService inputValidationService = new();
        private readonly CustomerService customerService = new();
        private readonly EmployeeService employeeService = new();

        public SignInPage()
        {
            InitializeComponent();
            Title.Text = AppConstants.PROJECT_NAME;

        }

        private void OnSignIn(object sender, RoutedEventArgs e)
        {
            if (!inputValidationService.ValidateInputs(new() { { "string", Id.Text }, { "password", Password.Password } }))
            {
                SignInError.Visibility = Visibility.Visible;
                return;
            }

            var check = Id.Text[0].ToString().ToLower();
            switch (check)
            {
                case "m":
                    GotoDestination(Id.Text[1..], false, new MarketingManagerHomePage());
                    break;
                case "a":
                    GotoDestination(Id.Text[1..], false, new AccountingManagerHomePage());
                    break;
                case "l":
                    GotoDestination(Id.Text[1..], false, new LoadEngineerHomePage());
                    break;
                case "f":
                    GotoDestination(Id.Text[1..], false, new FlightManagerHomePage());
                    break;
                default:
                    GotoDestination(Id.Text, true, new CustomerHomePage());
                    break;
            };
        }

        private void OnForgotCredential(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("please email at asimtut@gmail.com", "Forgot credential");
        }

        private void OnSignUp(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SignUpPage());
        }


        private void GotoDestination(string id, bool isCustomer, Page destinationPage)
        {
            if (inputValidationService.ValidateInputs(new() { { "id", id } }))
            {
                User user = null;

                if (isCustomer)
                {
                    user = customerService.GetCustomerById(long.Parse(id));

                    user = (user != null && user.Password == UserUtil.GenerateSHA512(Password.Password)) ? user : null;
                    Debug.WriteLine("Logged Customer: ");
                }
                else
                {
                    user = employeeService.GetEmployeeById(long.Parse(id));
                    user = (user != null && user.Password == UserUtil.GenerateSHA512(Password.Password)) ? user : null;
                    Debug.WriteLine("Logged Employee: ");
                }

                if (user != null)
                {
                    UserUtil.StoreLoggedUser(user.Id, isCustomer);
                    this.NavigationService.Navigate(destinationPage);
                }
            }
            SignInError.Visibility = Visibility.Visible;
        }
    }
}
