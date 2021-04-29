using Air3550.Constants;
using Air3550.Data;
using Air3550.Services;
using Air3550.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Air3550.Views
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        private readonly InputValidationService inputValidationService = new();
        private readonly CustomerService customerService = new();

        public SignUpPage()
        {
            InitializeComponent();
            Title.Text = $"Welcome to {AppConstants.PROJECT_NAME}";
        }

        private void OnSignUp(object sender, RoutedEventArgs e)
        {
            Dictionary<string, object> inputDict = new() {
                { "firstName", FirstName.Text.Trim() },
                { "lastName", LastName.Text.Trim() },
                { "phoneNumber", PhoneNumber.Text.Trim() },
                { "birthDate", BirthDate.SelectedDate },
                { "email", Email.Text.Trim() },
                { "password", Password.Password.Trim() },
                { "address1", Address1.Text.Trim() },
                { "address2", Address2.Text.Trim() },
                { "state", State.Text.Trim() },
                { "city", City.Text.Trim() },
                { "zipCode", ZipCode.Text.Trim() },
                { "creditCardNumber", CreditCardNumber.Text.Trim()}
            };

            if (
                !(Email.Text == ConfirmEmail.Text &&
                Password.Password == ConfirmPassword.Password &&
                inputValidationService.ValidateInputs(inputDict)
                ))
            {
                SignUpError.Visibility = Visibility.Visible;
                return;
            }
            long id = customerService.AddCustomer(new()
            {
                FirstName = FirstName.Text.Trim(),
                LastName = LastName.Text.Trim(),
                PhoneNumber = long.Parse(PhoneNumber.Text.Trim()),
                BirthDate = BirthDate.SelectedDate.Value,
                Email = Email.Text.Trim(),
                Password = Password.Password.Trim(),
                Address = new()
                {
                    Address1 = Address1.Text.Trim(),
                    Address2 = Address2.Text.Trim(),
                    State = State.Text.Trim(),
                    City = City.Text.Trim(),
                    ZipCode = int.Parse(ZipCode.Text.Trim())
                },
                CreditCardNumber = long.Parse(CreditCardNumber.Text.Trim())
            });
            MessageBox.Show($"your user id is {id}", "User Id");
            this.NavigationService.Navigate(new SignInPage());
        }
    }
}
