using System;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;


namespace MySchoolApp
{ 

    public class StartDateDatePickerFragment : Android.Support.V4.App.DialogFragment, DatePickerDialog.IOnDateSetListener

    {
        public static readonly string TAG = "X:" + typeof(StartDateDatePickerFragment).Name.ToUpper();

        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> _dateSelectedHandler = delegate { };
        static DateTime _minDateTime;
        public static StartDateDatePickerFragment NewInstance(Action<DateTime> onDateSelected, DateTime mindateTime)
        {
            StartDateDatePickerFragment frag = new StartDateDatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            _minDateTime = mindateTime;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = _minDateTime;
            DateTime minDate = DateTime.Now;

            DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                           this,
                                                           currently.Year,
                                                           currently.Month-1,
                                                           currently.Day);

            //Java.Util.GregorianCalendar date = new Java.Util.GregorianCalendar();
            //date.Set((minDate.Year), (minDate.Month-1), minDate.Day, minDate.Hour, minDate.Minute, minDate.Second);

            //dialog.DatePicker.MinDate = date.TimeInMillis;
            dialog.DatePicker.MaxDate = new Java.Util.GregorianCalendar().TimeInMillis;
            return dialog;
        }
       
        public void OnDateSet(Android.Widget.DatePicker view, int year, int month, int dayOfMonth)
        {
            // Note: month is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, month+1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }
}