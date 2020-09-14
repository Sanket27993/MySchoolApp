using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySchool;

namespace MySchoolApp
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        private Android.Support.V7.App.AlertDialog.Builder alert = null;
        private EditText _editTextUsername;
        private EditText _editTextPassword;
        private Button _buttonLogin;
        private LoginViewModel _loginViewModel;
        private ISharedPreferences _preferences;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            UIRefrence();
            UIClickEvent();
            ClassInit();
        }

        /// <summary>
        /// Find UI Control
        /// </summary>
        private void UIRefrence()
        {
            _editTextUsername = FindViewById<EditText>(Resource.Id.editTextUsername);
            _editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            _buttonLogin = FindViewById<Button>(Resource.Id.buttonLogin);

            _editTextUsername.Text = "Sanket123";
            _editTextPassword.Text = "1234";
        }

        /// <summary>
        /// UI control click event
        /// </summary>
        private void UIClickEvent()
        {
            _buttonLogin.Click += ButtonLogin_Click;
        }

        private void ClassInit()
        {
            _preferences = PreferenceManager.GetDefaultSharedPreferences(this);
            _loginViewModel = new LoginViewModel();
            _loginViewModel.OpeartaionEvent += LoginViewModel_OpeartaionEvent;
        }

        /// <summary>
        /// Login button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonLogin_Click(object sender, EventArgs e)
        {

            var userOAuthentication = new UserOAuthenticationModel
            {
                UserName = _editTextUsername.Text.Trim(),
                Password = _editTextPassword.Text.Trim()
            };
            _loginViewModel.UserLogin(userOAuthentication);
        }

        private void LoginViewModel_OpeartaionEvent(object sender, OperationEventArgs e)
        {
            if(e.Result==(int)EnumResult.success)
            {
                SetPrefrenceData();
                StartActivity(new Intent(this, typeof(MainActivity)));
            }
            else
            {
                if(!string.IsNullOrEmpty(e.Message))
                {
                    ShowAlert(e.Message);
                }
            }
        }


        private void SetPrefrenceData()
        {
            _preferences.Edit().PutString(AppConstant.PrefUserName, _editTextUsername.Text).Apply();
            _preferences.Edit().PutString(AppConstant.PrefPassword, _editTextPassword.Text).Apply();
            if (_loginViewModel.UserOAuthenticationData != null)
            {
                if (_loginViewModel.UserOAuthenticationData.IsAdmin)
                {
                    _preferences.Edit().PutBoolean(AppConstant.PrefIsAdmin, true).Apply();
                }
                else
                {
                    _preferences.Edit().PutInt(AppConstant.PrefStudentID, _loginViewModel.UserOAuthenticationData.StudentID).Apply();
                }
            }
           
        }


        private void ShowAlert(string errorMessage)
        {
            try
            {
                
                //set alert for executing the task
                if (alert == null)
                    alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                alert.SetMessage(errorMessage);
                alert.SetCancelable(false);

                alert.SetPositiveButton("Ok", (senderAlert, args) =>
                {
                   //alert.Dispose();
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
    }
}