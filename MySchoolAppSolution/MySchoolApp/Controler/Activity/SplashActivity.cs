using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MySchool;

namespace MySchoolApp.Controler
{
    [Activity(Label = "My School",NoHistory =true, MainLauncher = true, Theme = "@style/AppTheme.Splash")]
    public class SplashActivity : AppCompatActivity
    {
        private ISharedPreferences _preferences;
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_spalsh);
            _preferences = PreferenceManager.GetDefaultSharedPreferences(this);
        }

        protected override void OnResume()
        {
            Task startup = new Task(() => { SimulateStartup(); });
            startup.Start();
            base.OnResume();
        }

        private async void SimulateStartup()
        {
            await Task.Delay(4000); // Simulate a bit of startup work.
            RedirectToAfterLoginScreen();
            //StartActivity(new Intent(this, typeof(LoginActivity)));
        }

        private void RedirectToAfterLoginScreen()
        {
            string userName = _preferences.GetString(AppConstant.PrefUserName, string.Empty);
            string password = _preferences.GetString(AppConstant.PrefPassword, string.Empty);
           
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
            else
            {
                StartActivity(new Intent(this, typeof(LoginActivity)));
            }
        }
    }
}