using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;
using App1.Negocio;
using System;

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
            inicializar();
            accionarBoton();
            inicializarFragmento();
        }
        public void inicializar()
        {
            btnIrRegistrar = FindViewById<Button>(Resource.Id.btnRegistrar);
        }
        public void accionarBoton()
        {
            btnIrRegistrar.Click += delegate
            {
                IrRegistro();
                //  irAcciones2();
            };
        }
        public void IrRegistro()
        {
            var i = new Intent(this, typeof(Registrar));
            Console.WriteLine("Contexto: " + this);
            StartActivity(i);
        }
        public void inicializarFragmento()
        {
            var fragTx = FragmentManager.BeginTransaction();
            var frag = new Fragment_Listado();
            fragTx.Add(Resource.Id.FragmentContainer, frag);
            fragTx.Commit();
        }
    }
}