using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace InContextCaseStudy.Android
{
    [Activity(Label = "InContextCaseStudy", MainLauncher = true, Icon = "@drawable/incontext")]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            Forms.Init(this, bundle);

            LoadApplication(new UI.InContextCaseStudy());
        }
    }
}

