using System;
using Xamarin.Forms;

namespace InContextCaseStudy.UI
{
    public class InContextCaseStudy : Application
    {
        public InContextCaseStudy()
        {
            Instance = this;

            if (IsUserAuthenticated)
                MainPage = new MainPage();
            else
                MainPage = new LoginPage();
        }

        public bool IsUserAuthenticated { get; set; }

        public static InContextCaseStudy Instance { get; private set; }
    }
}
