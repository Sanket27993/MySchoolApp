using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySchool;

namespace MySchoolApp
{
    [Application]
    public class App : Application
    {
        public static App Instance { get; set; }
        public App()
        {
            //
        }

        protected App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            //
        }

        public override void OnCreate()
        {
            Instance = this;
            base.OnCreate();
            SharedAppDataConfig.LocalDatabasePath = new FileHelper().GetLocalFilePath("MySchool.db");
        }
    }
}