using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using App1.Negocio;
using System;
using Android.Util;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public Button btnIrRegistrar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btnIrRegistrar = FindViewById<Button>(Resource.Id.btnRegistrar);
            btnIrRegistrar.Click += delegate
            {
                irRegistro();
            };
        }
        public void irRegistro()
        {
            var i = new Intent(this, typeof(Registrar));
            StartActivity(i);
        }
    }
}