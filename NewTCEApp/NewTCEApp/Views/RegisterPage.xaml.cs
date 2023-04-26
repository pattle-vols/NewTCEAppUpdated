using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NewTCEApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        UserRepo userRepository = new UserRepo();
        public RegisterPage()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            string name = registerName.Text;
            string email = registerEmail.Text;
            string password = registerPassword.Text;
            string confirmPass = confirmPassword.Text;
            
            if(String.IsNullOrEmpty(name))
            {
                await DisplayAlert("Warning", "Please enter your name", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(email))
            {
                await DisplayAlert("Warning", "Please enter your email", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(password))
            {
                await DisplayAlert ("Warning", "Please enter your password", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(confirmPass))
            {
                await DisplayAlert ("Warning", "Please confirm your password", "Ok");
                return;
            }
            if(password != confirmPass)
            {
                await DisplayAlert ("Warning", "Passwords do not match", "Ok");
                return;
            }

            bool isSaved = await userRepository.Register(email, name, password);

            if (isSaved)
            {
                await DisplayAlert("Register user", "Registration complete", "Ok");
            }
            else
            {
                await DisplayAlert("Register user", "Registration failed", "Ok");
            }
        }
    }
}