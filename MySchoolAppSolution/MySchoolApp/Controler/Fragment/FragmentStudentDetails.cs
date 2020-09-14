using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using MySchool;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;

namespace MySchoolApp
{
    public class FragmentStudentDetails : Android.Support.V4.App.Fragment
    {
        View _view;
        private Android.Support.V7.App.AlertDialog.Builder alert = null;
        RelativeLayout relativeLayoutEdit;
        ImageView imageViewNameEdit;
        TextView textViewDisplayName;
        TextView textViewDisplayGender;
        TextView textViewDisplayClass;
        LinearLayout linearLayoutDisplayName;
        ViewSwitcher viewSwitcherName;

        EditText editTextEditName;

        EditText editTextClass;

        RelativeLayout relativeLayoutInfoEdit;
        ImageView imageViewInfoEdit;
        ViewSwitcher viewSwitcherInfo;
        TextView textViewDisplayContactNo;
        TextView textViewDisplayAddress;
        TextView textViewDisplayEmail;
        TextView textViewDisplayBirthDate;
        LinearLayout linearLayoutDisplayInfo;


        TextView textViewEditBirthDate;
        EditText editTextEditAddress;
        EditText editTextEditStreet;
        EditText editTextEditCity;
        EditText editTextEditState;
        EditText editTextEditCountry;
        EditText editTextEditPostalCode;
        EditText editTextEditEmail;
        EditText editTextEditContactNo;

        Button btnCancel;
        Button btnSave;
        Button btnAudited;

        TextView textViewTitle;
        RadioGroup radioGroup;
        RadioButton radiobuttonMale;
        RadioButton radiobuttonFemale;
        // ImageView imageViewAdd;

        TextView textViewchooseFile;
        TextView textViewFileName;
        Button buttonUpload;
        LinearLayout linearLayoutFileUpload;

        TextView textViewSignOut;

        private bool IsAdmin;
        private ISharedPreferences _preferences;
        private StudentDetailsViewModel _studentDetailsViewModel;
        private StudentDetailsModel _studentDetails;
        private int StudentID;
        private bool IsNewStudent = false;
        private bool IsAudited = false;
        private bool IsDocumentUploaded = false;
        private byte[] bytesToUpload;
        public static int REQUEST_STORAGE = 121;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            _view = inflater.Inflate(Resource.Layout.fragment_student_details, container, false);
            UIControlRefrence();
            UIClickEvent();
            ClassInit();
            BindStudentData();
            return _view;
        }

        private void UIControlRefrence()
        {
            relativeLayoutEdit = _view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutEdit);
            imageViewNameEdit = _view.FindViewById<ImageView>(Resource.Id.imageViewNameEdit);
            textViewDisplayName = _view.FindViewById<TextView>(Resource.Id.textViewDisplayName);
            textViewDisplayGender = _view.FindViewById<TextView>(Resource.Id.textViewDisplayGender);
            textViewDisplayClass = _view.FindViewById<TextView>(Resource.Id.textViewDisplayClass);
            linearLayoutDisplayName = _view.FindViewById<LinearLayout>(Resource.Id.linearLayoutDisplayName);
            viewSwitcherName = _view.FindViewById<ViewSwitcher>(Resource.Id.viewSwitcherName);

            editTextEditName = _view.FindViewById<EditText>(Resource.Id.editTextEditName);
            editTextClass = _view.FindViewById<EditText>(Resource.Id.editTextClass);


            relativeLayoutInfoEdit = _view.FindViewById<RelativeLayout>(Resource.Id.relativeLayoutInfoEdit);
            imageViewInfoEdit = _view.FindViewById<ImageView>(Resource.Id.imageViewInfoEdit);
            viewSwitcherInfo = _view.FindViewById<ViewSwitcher>(Resource.Id.viewSwitcherInfo);

            textViewDisplayContactNo = _view.FindViewById<TextView>(Resource.Id.textViewDisplayContactNo);
            textViewDisplayAddress = _view.FindViewById<TextView>(Resource.Id.textViewDisplayAddress);
            textViewDisplayEmail = _view.FindViewById<TextView>(Resource.Id.textViewDisplayEmail);
            textViewDisplayBirthDate = _view.FindViewById<TextView>(Resource.Id.textViewDisplayBirthDate);
            linearLayoutDisplayInfo = _view.FindViewById<LinearLayout>(Resource.Id.linearLayoutDisplayInfo);

            textViewEditBirthDate = _view.FindViewById<TextView>(Resource.Id.textViewEditBirthDate);

            editTextEditAddress = _view.FindViewById<EditText>(Resource.Id.editTextEditAddress);
            editTextEditStreet = _view.FindViewById<EditText>(Resource.Id.editTextEditStreet);
            editTextEditCity = _view.FindViewById<EditText>(Resource.Id.editTextEditCity);
            editTextEditState = _view.FindViewById<EditText>(Resource.Id.editTextEditState);
            editTextEditCountry = _view.FindViewById<EditText>(Resource.Id.editTextEditCountry);

            editTextEditPostalCode = _view.FindViewById<EditText>(Resource.Id.editTextEditPostalCode);
            editTextEditEmail = _view.FindViewById<EditText>(Resource.Id.editTextEditEmail);
            editTextEditContactNo = _view.FindViewById<EditText>(Resource.Id.editTextEditContactNo);

            btnCancel = _view.FindViewById<Button>(Resource.Id.btnCancel);
            btnSave = _view.FindViewById<Button>(Resource.Id.btnSave);
            btnAudited = _view.FindViewById<Button>(Resource.Id.btnAudited);
            textViewTitle = _view.FindViewById<TextView>(Resource.Id.textViewTitle);

            radioGroup = _view.FindViewById<RadioGroup>(Resource.Id.radioGroup);
            radiobuttonMale = _view.FindViewById<RadioButton>(Resource.Id.radiobuttonMale);
            radiobuttonFemale = _view.FindViewById<RadioButton>(Resource.Id.radiobuttonFemale);

            textViewchooseFile = _view.FindViewById<TextView>(Resource.Id.textViewchooseFile);
            textViewFileName = _view.FindViewById<TextView>(Resource.Id.textViewFileName);
            buttonUpload = _view.FindViewById<Button>(Resource.Id.buttonUpload);
            linearLayoutFileUpload = _view.FindViewById<LinearLayout>(Resource.Id.linearLayoutFileUpload);
            textViewSignOut= _view.FindViewById<TextView>(Resource.Id.textViewSignOut);
        }

        private void UIClickEvent()
        {
            imageViewNameEdit.Click += ImageViewNameEdit_Click;
            imageViewInfoEdit.Click += ImageViewInfoEdit_Click;

            textViewEditBirthDate.Click += TextViewEditBirthDate_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnAudited.Click += BtnAudited_Click;

            textViewchooseFile.Click += TextViewchooseFile_Click;
            buttonUpload.Click += ButtonUpload_Click;

            textViewSignOut.Click += TextViewSignOut_Click;
        }

        private void TextViewSignOut_Click(object sender, EventArgs e)
        {
            ((MainActivity)Activity).ShowLogoutAlert();
        }

        private void BtnAudited_Click(object sender, EventArgs e)
        {
            if (_studentDetails != null && !_studentDetails.IsAudited)
            {
                IsAudited = true;
                _studentDetails.IsAudited = true;
                _studentDetailsViewModel.UpdateAudited(_studentDetails);
            }
        }


        private void ClassInit()
        {
            _preferences = PreferenceManager.GetDefaultSharedPreferences(Activity);
            IsAdmin = _preferences.GetBoolean(AppConstant.PrefIsAdmin, false);
            StudentID = _preferences.GetInt(AppConstant.PrefStudentID, -1);

            _studentDetailsViewModel = new StudentDetailsViewModel();
            _studentDetailsViewModel.OpeartaionEvent += StudentDetailsViewModel_OpeartaionEvent;
            GetStudentData();
        }


        private void GetStudentData()
        {
            _studentDetailsViewModel.GetStudentData(StudentID);
            _studentDetails = _studentDetailsViewModel.studentDetails;
        }

        private void ImageViewInfoEdit_Click(object sender, EventArgs e)
        {
            if (viewSwitcherInfo.CurrentView == linearLayoutDisplayInfo)
            {
                viewSwitcherInfo.ShowNext();
            }
            else
            {

                viewSwitcherInfo.ShowPrevious();
            }
        }

        private void ImageViewNameEdit_Click(object sender, EventArgs e)
        {
            if (viewSwitcherName.CurrentView == linearLayoutDisplayName)
            {
                if (IsAdmin)
                {
                    btnAudited.Visibility = ViewStates.Gone;
                    viewSwitcherName.ShowNext();
                    viewSwitcherInfo.ShowNext();
                }
                else
                {

                    viewSwitcherName.ShowNext();
                }

            }
            else
            {
                if (IsAdmin)
                {
                    btnAudited.Visibility = ViewStates.Visible;
                    viewSwitcherName.ShowPrevious();
                    viewSwitcherInfo.ShowPrevious();
                }
                else
                {
                    viewSwitcherName.ShowPrevious();
                }

            }
        }


        private void TextViewEditBirthDate_Click(object sender, EventArgs e)
        {
            DateTime startdate = DateTime.MinValue;
            if (!string.IsNullOrEmpty(textViewEditBirthDate.Text))
            {
                startdate = GetDateFormatInDateTime(textViewEditBirthDate.Text);
            }
            else
            {
                startdate = DateTime.Now;
            }
            var frag = StartDateDatePickerFragment.NewInstance(delegate (DateTime time)
            {
                textViewEditBirthDate.Text = time.ToString(AppConstant.DATEFORMAT_MMDDYYYY);

            }, startdate);
            frag.Show(ChildFragmentManager, StartDateDatePickerFragment.TAG);
        }

        public DateTime GetDateFormatInDateTime(string date)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dateTime16 = DateTime.ParseExact(date, new string[] { "MM.dd.yyyy", "MM-dd-yyyy", "MM/dd/yyyy", "MMM dd, yyyy", "MM/d/yyyy hh:mm:ss tt", "MM/dd/yyyy hh:mm:ss tt", "MM/dd/yyyy hh:mm:ss", "MM-dd-yyyy hh:mm:ss", "M/d/yyyy hh:mm:ss tt", "M/dd/yyyy hh:mm:ss tt" }, provider, DateTimeStyles.None);
            return dateTime16;
        }


        private void BindStudentData()
        {
            try
            {

                SetStudentData();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SetStudentData()
        {
            if (IsAdmin)
            {
                textViewSignOut.Visibility = ViewStates.Gone;
                relativeLayoutEdit.Visibility = ViewStates.Visible;
                relativeLayoutInfoEdit.Visibility = ViewStates.Gone;
                btnAudited.Visibility = ViewStates.Visible;
                linearLayoutFileUpload.Visibility = ViewStates.Gone;
            }
            else
            {
                textViewSignOut.Visibility = ViewStates.Visible;
                linearLayoutFileUpload.Visibility = ViewStates.Visible;
                btnAudited.Visibility = ViewStates.Gone;
                relativeLayoutEdit.Visibility = ViewStates.Gone;
                relativeLayoutInfoEdit.Visibility = ViewStates.Visible;
            }
            if (_studentDetails != null)
            {
                if (_studentDetails.IsAudited)
                {
                    IsAudited = false;
                    btnAudited.Enabled = false;
                    btnAudited.Alpha = 0.7f;
                }
                else
                {
                    btnAudited.Enabled = true;
                }
                ///Edit Student
                IsNewStudent = false;
                textViewDisplayName.Text = _studentDetails.Name;
                editTextEditName.Text = _studentDetails.Name;

                textViewDisplayGender.Text = _studentDetails.Gender;
                SetGenderValue();

                textViewDisplayClass.Text = _studentDetails.StudentClass;
                editTextClass.Text = _studentDetails.StudentClass;

                textViewDisplayBirthDate.Text = _studentDetails.Birthdate;
                textViewEditBirthDate.Text = _studentDetails.Birthdate;

                textViewDisplayContactNo.Text = _studentDetails.ContactNo;
                editTextEditContactNo.Text = _studentDetails.ContactNo;

                textViewDisplayEmail.Text = _studentDetails.EmailID;
                editTextEditEmail.Text = _studentDetails.EmailID;

                textViewDisplayAddress.Text = BindAddress(_studentDetails);

                if (!string.IsNullOrEmpty(_studentDetails.FileName))
                {
                    textViewFileName.Visibility = ViewStates.Visible;
                    textViewFileName.Text = _studentDetails.FileName;
                }

            }
            else
            {
                ///New Student
                IsNewStudent = true;
                btnAudited.Visibility = ViewStates.Gone;
                relativeLayoutEdit.Visibility = ViewStates.Gone;
                viewSwitcherName.ShowNext();
                viewSwitcherInfo.ShowNext();
            }
        }

        private void SetGenderValue()
        {
            if (_studentDetails != null)
            {
                if (_studentDetails.Gender == "Male")
                {
                    radiobuttonMale.Checked = true;
                    radiobuttonFemale.Checked = false;
                }
                else
                {
                    radiobuttonMale.Checked = false;
                    radiobuttonFemale.Checked = true;
                }
            }
        }
        private string BindAddress(StudentDetailsModel studentDetails)
        {
            StringBuilder strAddress = new StringBuilder();
            if (!string.IsNullOrEmpty(studentDetails.Address))
            {
                strAddress.AppendLine(studentDetails.Address);
                editTextEditAddress.Text = studentDetails.Address;
            }
            else
            {
                editTextEditAddress.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(studentDetails.Street))
            {
                strAddress.Append(studentDetails.Street);
                editTextEditStreet.Text = studentDetails.Street;
            }
            else
            {
                editTextEditStreet.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(studentDetails.City))
            {
                strAddress.Append("," + studentDetails.City);
                editTextEditCity.Text = studentDetails.City;
            }
            else
            {
                editTextEditCity.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(studentDetails.State))
            {
                strAddress.Append("," + studentDetails.State);
                editTextEditState.Text = studentDetails.State;
            }
            else
            {
                editTextEditState.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(studentDetails.Country))
            {
                strAddress.Append("," + studentDetails.Country);
                editTextEditCountry.Text = studentDetails.Country;
            }
            else
            {
                editTextEditCountry.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(studentDetails.PostalCode))
            {
                strAddress.Append("," + studentDetails.PostalCode);
                editTextEditPostalCode.Text = studentDetails.PostalCode;
            }
            else
            {
                editTextEditPostalCode.Text = string.Empty;
            }
            return strAddress.ToString();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            AppManager.getInstance().hideKeyBoard(Activity);
            if (!IsAdmin)
            {

                viewSwitcherInfo.ShowPrevious();
            }
            else
            {
                if (IsNewStudent)
                {
                    ((MainActivity)Activity).ReplaceToStudentList();
                }
                else
                {
                    btnAudited.Visibility = ViewStates.Visible;
                    viewSwitcherName.ShowPrevious();
                    viewSwitcherInfo.ShowPrevious();
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            AppManager.getInstance().hideKeyBoard(Activity);
            StudentDataInsertAndUpdate();
        }

        private void StudentDataInsertAndUpdate()
        {
            try
            {

                if (IsNewStudent)
                {
                    _studentDetails = new StudentDetailsModel();
                    _studentDetails.StudentID = GetLastStudentID();
                }
                if (IsAdmin)
                {
                    _studentDetails.Name = editTextEditName.Text.Trim();
                    _studentDetails.Gender = GetRadiobuttonValue();
                    _studentDetails.StudentClass = editTextClass.Text.Trim();
                }
                _studentDetails.Birthdate = textViewEditBirthDate.Text.Trim();
                _studentDetails.Address = editTextEditAddress.Text.Trim();
                _studentDetails.Street = editTextEditStreet.Text.Trim();
                _studentDetails.State = editTextEditState.Text.Trim();
                _studentDetails.City = editTextEditCity.Text.Trim();
                _studentDetails.PostalCode = editTextEditPostalCode.Text.Trim();
                _studentDetails.Country = editTextEditCountry.Text.Trim();
                _studentDetails.ContactNo = editTextEditContactNo.Text.Trim();
                _studentDetails.EmailID = editTextEditEmail.Text.Trim();

                if (IsNewStudent)
                {
                    //New insert student
                    _studentDetailsViewModel.SaveStudentData(_studentDetails, IsAdmin);
                }
                else
                {
                    //Update Student
                    _studentDetailsViewModel.UpdateStudentData(_studentDetails, IsAdmin);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }

        private int GetLastStudentID()
        {
            if (_studentDetailsViewModel.AllStudentList != null && _studentDetailsViewModel.AllStudentList.Count > 0)
            {
                var stuID = _studentDetailsViewModel.AllStudentList.Last().StudentID;
                return stuID + 1;
            }

            return -1;
        }
        private string GetRadiobuttonValue()
        {
            int genid = radioGroup.CheckedRadioButtonId;
            RadioButton radioButton = (RadioButton)_view.FindViewById(genid);
            String gender = radioButton.Text;
            return gender;
        }
        private void StudentDetailsViewModel_OpeartaionEvent(object sender, OperationEventArgs e)
        {
            if (e.Result == (int)EnumResult.success)
            {
                UpdateShowAlert(e.Message);
            }
            else
            {
                if (!string.IsNullOrEmpty(e.Message))
                {
                    ShowAlert(e.Message);
                }
            }
        }
        private void ShowAlert(string errorMessage)
        {
            try
            {

                //set alert for executing the task
                if (alert == null)
                    alert = new Android.Support.V7.App.AlertDialog.Builder(Activity);

                alert.SetMessage(errorMessage);
                alert.SetCancelable(false);

                alert.SetPositiveButton("Ok", (senderAlert, args) =>
                {
                    //alert.Dispose();
                });

                alert.Show();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateShowAlert(string errorMessage)
        {
            try
            {

                //set alert for executing the task
                if (alert == null)
                    alert = new Android.Support.V7.App.AlertDialog.Builder(Activity);

                alert.SetMessage(errorMessage);
                alert.SetCancelable(false);

                alert.SetPositiveButton("Ok", (senderAlert, args) =>
                {
                    if (IsAdmin)
                    {
                        if (IsNewStudent)
                        {
                            IsNewStudent = false;
                            ((MainActivity)Activity).ReplaceToStudentList();
                        }
                        else
                        {
                            if (IsAudited)
                            {
                                ((MainActivity)Activity).ReplaceToStudentList();
                            }
                            else
                            {
                                if (IsDocumentUploaded)
                                {
                                    IsDocumentUploaded = false;
                                }
                                else
                                {
                                    GetStudentData();
                                    BindStudentData();
                                    viewSwitcherName.ShowPrevious();
                                    viewSwitcherInfo.ShowPrevious();
                                }
                            }
                        }
                    }
                    else
                    {
                        GetStudentData();
                        BindStudentData();
                        viewSwitcherInfo.ShowPrevious();
                    }

                });

                alert.Show();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void TextViewchooseFile_Click(object sender, EventArgs e)
        {
            OpenFilePickerAsync();
        }

        FileData fileData;
        /// <summary>
        /// Open file picker
        /// </summary>
        private async void OpenFilePickerAsync()
        {
            try
            {
                fileData = await CrossFilePicker.Current.PickFile(new string[] { ".png", ".jpeg", ".jpg", ".pdf", ".doc", ".docx", ".xlsx", ".txt" });
                if (fileData == null)
                {
                    return;
                }

                var fileName = fileData.FileName.ToLower();
                if (fileName != null)
                {

                    if (!string.IsNullOrEmpty(fileName) &&
                                        Path.GetExtension(fileName).Contains(".png")
                                     || Path.GetExtension(fileName).Contains(".jpeg")
                                     || Path.GetExtension(fileName).Contains(".jpg")
                                     || Path.GetExtension(fileName).Contains(".pdf")
                                     || Path.GetExtension(fileName).Contains(".doc")
                                     || Path.GetExtension(fileName).Contains(".docx")
                                     || Path.GetExtension(fileName).Contains(".xlsx")
                                     || Path.GetExtension(fileName).Contains(".txt"))
                    {
                        CheckStoragePermission();
                    }
                    else
                    {

                        Toast.MakeText(Activity, "Only Image or Doc file type is allowed.", ToastLength.Long).Show();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CheckStoragePermission()
        {
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                {

                    if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.ReadExternalStorage) == (int)PermissionChecker.PermissionGranted)
                    {
                        ConvertFileToBytes();
                    }
                    else
                    {
                        // Storage permission is not granted. If necessary display rationale & request.
                        if (ActivityCompat.ShouldShowRequestPermissionRationale(Activity, Manifest.Permission.ReadExternalStorage))
                        {


                            var requiredPermissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
                            Snackbar.Make(_view,
                                           "Storage permission is required to upload the document.",
                                           Snackbar.LengthIndefinite)
                                    .SetAction("Ok",
                                               new Action<View>(delegate (View obj)
                                               {
                                                   ActivityCompat.RequestPermissions(Activity, requiredPermissions, REQUEST_STORAGE);
                                               }
                                    )
                            ).Show();
                        }
                        else
                        {
                            ActivityCompat.RequestPermissions(Activity, new String[] { Manifest.Permission.ReadExternalStorage }, REQUEST_STORAGE);
                        }
                    }
                }
                else
                {
                    ConvertFileToBytes();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void ConvertFileToBytes()
        {
            try
            {
                var bytesofFile = fileData.DataArray.LongLength;
                var mbFile = (bytesofFile / 1024f) / 1024f;
                if (mbFile < 3)
                {
                    ////Here store file uplode data in DB
                    textViewFileName.Text = Path.GetFileName(fileData.FileName);
                    textViewFileName.Visibility = ViewStates.Visible;
                    bytesToUpload = fileData.DataArray;
                }
                else
                {
                    Toast.MakeText(Activity, "File size should not be greater than 3MB.", ToastLength.Long).Show(); ;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ButtonUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileData != null && _studentDetails != null)
                {
                    IsDocumentUploaded = true;
                    _studentDetails.FileName = Path.GetFileName(fileData.FileName);
                    _studentDetails.Type = Path.GetExtension(fileData.FileName);
                    _studentDetails.bytesToUpload = bytesToUpload;
                    _studentDetailsViewModel.UploadDocument(_studentDetails);

                }
                else
                {
                    Toast.MakeText(Activity, "Please choose file to upload.", ToastLength.Long).Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}