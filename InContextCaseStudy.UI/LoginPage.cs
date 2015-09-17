using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Xamarin.Forms;

namespace InContextCaseStudy.UI
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            _logoImage = new Image
            {
                Source = ImageSource.FromFile("incontext.png"),
                HorizontalOptions = LayoutOptions.Center,
            };

            _usernameLabel = new Entry
            {
                Placeholder = "Username",
            };
            _usernameLabel.TextChanged += InputChanged;

            _passwordLabel = new Entry
            {
                IsPassword = true, 
                Placeholder = "Password",
            };
            _passwordLabel.TextChanged += InputChanged;

            _loginButton = new Button
            {
                IsEnabled = false,
                Text = "Login",
            };
            _loginButton.Clicked += LoginClicked;

            _activityIndicator = new ActivityIndicator
            {
                IsVisible = false,
                IsRunning = true,
            };

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Start,
                Children =
                {
                    _logoImage,
                    _usernameLabel,
                    _passwordLabel,
                    _loginButton,
                    _activityIndicator
                },
            };
            
            Content = layout;
        }

        private readonly Image _logoImage;
        private readonly Entry _usernameLabel;
        private readonly Entry _passwordLabel;
        private readonly Button _loginButton;
        private readonly ActivityIndicator _activityIndicator;

        private void InputChanged(object sender, EventArgs eventArgs)
        {
            _loginButton.IsEnabled =
                !string.IsNullOrWhiteSpace(_usernameLabel.Text)
                && !string.IsNullOrWhiteSpace(_passwordLabel.Text);
        }

        private bool IsWorking
        {
            set
            {
                _usernameLabel.IsEnabled = !value;
                _passwordLabel.IsEnabled = !value;
                _loginButton.IsVisible = !value;
                _activityIndicator.IsVisible = value;
            }
        }

        private async void LoginClicked(object sender, EventArgs eventArgs)
        {
            IsWorking = true;

            try
            {
                // Simulate authentication API call
                await Task.Delay(3000);

                InContextCaseStudy.Instance.IsUserAuthenticated = true;
                InContextCaseStudy.Instance.MainPage = new MainPage();
            }
            finally
            {
                IsWorking = false;
            }
        }
    }
}
