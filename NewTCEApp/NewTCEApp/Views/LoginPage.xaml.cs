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
    public partial class LoginPage : ContentPage
    {  
        UserRepo userRepository = new UserRepo();
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string password = passwordEntry.Text;
            string token = await userRepository.SignIn(email, password);

            if (!string.IsNullOrEmpty(token))
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            else
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Error", "Invalid Email or Password", "Ok");
                });
        }
        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }


    }
}