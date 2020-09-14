using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;
using MySchool;
using Android.Preferences;
using Android.Support.V4.App;
using Android.Views;

namespace MySchoolApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity, View.IOnClickListener
    {
        private Android.Support.V7.App.AlertDialog.Builder alert = null;
        private Android.Support.V4.App.Fragment _currentFragment = null;
        private ISharedPreferences _preferences;
        private bool IsAdmin;
        Android.Support.V7.Widget.Toolbar _toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            UIRefrences();
            UIClickEvent();
            CheckIsAdmin();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void UIRefrences()
        {
            _toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(_toolbar);
        }

        private void UIClickEvent()
        {
            _toolbar.SetNavigationOnClickListener(this);
        }

        public void OnClick(View v)
        {
            ReplaceToStudentList();
        }
        public void DisplayAndHideBackButton(bool IsDisplay)
        {
            SupportActionBar.SetDisplayHomeAsUpEnabled(IsDisplay);
            SupportActionBar.SetDisplayShowHomeEnabled(IsDisplay);
        }


        private void CheckIsAdmin()
        {
            _preferences = PreferenceManager.GetDefaultSharedPreferences(this);
            IsAdmin = _preferences.GetBoolean(AppConstant.PrefIsAdmin, false);
            if (IsAdmin)
            {
                ReplaceToStudentList();
            }
            else
            {
                ReplaceToStudentDetails();
            }
        }

        public void ReplaceToStudentDetails()
        {
            //if(IsAdmin)
            //{
            //    DisplayAndHideBackButton(true);
            //}
            var bundle = new Bundle();
            bundle.PutString("ARG1", "camera");
            NavigateTo(new FragmentStudentDetails() { Arguments = bundle });
        }

        public void ReplaceToStudentList()
        {
            DisplayAndHideBackButton(false);
            var bundle = new Bundle();
            bundle.PutString("ARG1", "camera");
            NavigateTo(new FragmentStudentList() { Arguments = bundle });
        }

        public void NavigateTo(Android.Support.V4.App.Fragment newFragment)
        {
            try
            {

                Android.Support.V4.App.FragmentManager mFragmentManager = null;
                Android.Support.V4.App.FragmentTransaction mFragmentTransaction = null;

                _currentFragment = newFragment;
                mFragmentManager = SupportFragmentManager;
                mFragmentTransaction = mFragmentManager.BeginTransaction();
                mFragmentTransaction.Replace(Resource.Id.frame, _currentFragment);
                mFragmentTransaction.SetTransition((int)FragmentTransit.FragmentFade);
                mFragmentTransaction.CommitAllowingStateLoss();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void OnBackPressed()
        {
            var fargFragmentStudentDetails = _currentFragment is FragmentStudentDetails;
            var fargFragmentStudentList = _currentFragment is FragmentStudentList;

            if (fargFragmentStudentList)
            {
                SetBackAlert();
            }
            else if (fargFragmentStudentDetails)
            {
                if (IsAdmin)
                {
                    ReplaceToStudentList();
                }
                else
                {
                    SetBackAlert();
                }
            }

        }

        private void SetBackAlert()
        {
            try
            {
                //set alert for executing the task
                if (alert == null)
                    alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                alert.SetMessage("Are you sure you want to exit?");
                alert.SetCancelable(false);

                alert.SetPositiveButton("Exit", (senderAlert, args) =>
                {
                    ActivityCompat.FinishAffinity(this);

                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                        //perform your own task for this conditional button click
                    });
                //run the alert in UI thread to display in the screen
                RunOnUiThread(() =>
                {
                    alert.Show();
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Alert dialog for logout
        /// </summary>
        public void ShowLogoutAlert()
        {
            try
            {
                alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                alert.SetMessage("Are you sure you want to Log out?");
                alert.SetCancelable(false);

                alert.SetPositiveButton("Log out", (senderAlert, args) =>
                {
                    GotoLogin();
                });

                alert.SetNegativeButton("Cancel", (senderAlert, args) =>
                {
                    //perform your own task for this conditional button click
                });
                //run the alert in UI thread to display in the screen
                RunOnUiThread(() =>
                {
                    alert.Show();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void GotoLogin()
        {
            SetPrefrenceValue();
            Intent intent = new Intent(this, typeof(LoginActivity));// New activity
            intent.SetFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
            Finish();
        }

        private void SetPrefrenceValue()
        {
            _preferences.Edit().PutBoolean(AppConstant.PrefIsAdmin, false).Apply();
            _preferences.Edit().PutInt(AppConstant.PrefStudentID, -1).Apply();
        }
    }
}