using System;
using Android.Graphics;
using Android.Widget;
using Android.Content.Res;
using Android.Content;
using Android.Views.InputMethods;
using Android.Support.V7.App;
using Android.Support.V4.App;
using Java.Util;
using System.Collections.Generic;
using System.Collections.Specialized;
using Android.Views;
using Android.Util;
using Android.Provider;
using Android.Content.PM;
using Android.App;

namespace MySchoolApp
{
    public class AppManager
    {
        private static AppManager instance { get; set; } = null;
        public static AppManager getInstance()
        {
            if (instance == null)
            {
                instance = new AppManager();
            }
            return instance;
        }

        public void hideKeyBoard(FragmentActivity activity)
        {
            InputMethodManager inputManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            var currentFocus = activity.CurrentFocus;
            if (currentFocus != null)
            {
                inputManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }

        public void hideKeyBoard(AppCompatActivity activity)
        {
            InputMethodManager inputManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            var currentFocus = activity.CurrentFocus;
            if (currentFocus != null)
            {
                inputManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }

        public void showKeyBoard(FragmentActivity activity)
        {
            InputMethodManager inputManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            var currentFocus = activity.CurrentFocus;
            if (currentFocus != null)
            {
                inputManager.ShowSoftInput(currentFocus, ShowFlags.Forced);
            }
        }

        public int GetHeight()
        {
            var x = Resources.System.DisplayMetrics.WidthPixels;
            var dp = (int)((x) / Resources.System.DisplayMetrics.Density);
            return x;
        }
        public int convertDpToPixel(Context context, int v)
        {
            return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, v, context.Resources.DisplayMetrics);
        }
    }
}
