using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using MySchool;

namespace MySchoolApp
{
    public class FragmentStudentList : Android.Support.V4.App.Fragment
    {
        private Android.Support.V7.App.AlertDialog.Builder alert = null;
        StudentDetailsAdapter _studentDetailsAdapter;
        private RecyclerView _recyclerViewStudentDetails;
        private View _view;
        List<StudentDetailsModel> _studentList;
        List<UserOAuthenticationModel> _userList;
        StudentDetailsViewModel _studentDetailsViewModel;
        private ISharedPreferences _preferences;
        LinearLayout _linearLayoutAdd;
        private int itemPosition = -1;
        TextView _textViewAdminSignOut;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            _view = inflater.Inflate(Resource.Layout.fragment_studentlist, container, false);
            UIRefrences();
            UIClickEvent();
            ClassIniti();
            BindAdapter();
            return _view;
        }

        private void UIRefrences()
        {
            _linearLayoutAdd = _view.FindViewById<LinearLayout>(Resource.Id.linearLayoutAdd);
            _recyclerViewStudentDetails = _view.FindViewById<RecyclerView>(Resource.Id.recyclerViewStudentDetails);
            _textViewAdminSignOut = _view.FindViewById<TextView>(Resource.Id.textViewAdminSignOut);
        }

        private void UIClickEvent()
        {
            _linearLayoutAdd.Click += LinearLayoutAdd_Click;
            _textViewAdminSignOut.Click += TextViewAdminSignOut_Click;
        }

        private void TextViewAdminSignOut_Click(object sender, EventArgs e)
        {
            ((MainActivity)Activity).ShowLogoutAlert();
        }

        private void LinearLayoutAdd_Click(object sender, EventArgs e)
        {
            _preferences.Edit().PutInt(AppConstant.PrefStudentID, 0).Apply();
            ((MainActivity)Activity).ReplaceToStudentDetails();
        }

        private void ClassIniti()
        {
            _preferences = PreferenceManager.GetDefaultSharedPreferences(Activity);
            _studentDetailsViewModel = new StudentDetailsViewModel();
            _studentDetailsViewModel.OpeartaionEvent += StudentDetailsViewModel_OpeartaionEvent;
            _studentList = new List<StudentDetailsModel>();
            GetStudentList();
        }

        private void GetStudentList()
        {
            _studentList = _studentDetailsViewModel.AllStudentList;
            _userList = _studentDetailsViewModel.UserList;
        }

        private void BindAdapter()
        {
            _recyclerViewStudentDetails.SetLayoutManager(new LinearLayoutManager(Activity));
            _recyclerViewStudentDetails.SetItemAnimator(new DefaultItemAnimator());
            _studentDetailsAdapter = new StudentDetailsAdapter(_studentList, _userList);
            _recyclerViewStudentDetails.SetAdapter(_studentDetailsAdapter);

            _studentDetailsAdapter.EditItemClick += StudentDetailsAdapter_EditItemClick;
            _studentDetailsAdapter.DeleteItemClick += StudentDetailsAdapter_DeleteItemClick;
        }

        private void StudentDetailsAdapter_EditItemClick(object sender, AdapterClickEventArgs e)
        {
            var studentID = _studentList[e.Position].StudentID;
            _preferences.Edit().PutInt(AppConstant.PrefStudentID, studentID).Apply();
            ((MainActivity)Activity).ReplaceToStudentDetails();
        }

        
        private void StudentDetailsAdapter_DeleteItemClick(object sender, AdapterClickEventArgs e)
        {
            itemPosition = e.Position;
            var studentData = _studentList[e.Position];
            ShowDeleteAlert(studentData);
        }

        private void StudentDetailsViewModel_OpeartaionEvent(object sender, OperationEventArgs e)
        {
            if (e.Result == (int)EnumResult.success)
            {
                ShowAlert(e.Message);
            }
        }

        private void ShowDeleteAlert(StudentDetailsModel studentDetailsModel)
        {
            try
            {

                //set alert for executing the task
                if (alert == null)
                    alert = new Android.Support.V7.App.AlertDialog.Builder(Activity);

                alert.SetMessage("Are you sure you want to Delete Student Details?");
                alert.SetCancelable(false);

                alert.SetPositiveButton("Yes", (senderAlert, args) =>
                {
                    _studentDetailsViewModel.DeleteStudentData(studentDetailsModel);
                });

                alert.SetNegativeButton("No", (senderAlert, args) =>
                {
                    //perform your own task for this conditional button click
                });
                //run the alert in UI thread to display in the screen

                alert.Show();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void ShowAlert(string errorMessage)
        {
            try
            {

                //set alert for executing the task
                
                    alert = new Android.Support.V7.App.AlertDialog.Builder(Activity);

                alert.SetMessage(errorMessage);
                alert.SetCancelable(false);

                alert.SetPositiveButton("Ok", (senderAlert, args) =>
                {
                   
                    _studentDetailsAdapter._studentList.RemoveAt(itemPosition);
                    _studentDetailsAdapter.NotifyItemRemoved(itemPosition);
                });

                alert.Show();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}