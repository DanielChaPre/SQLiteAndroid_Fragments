using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using App1.Datos;
using App1.Modelo;

namespace App1.Negocio
{
    [Activity(Label = "Registrar")]
    public class Registrar:Activity
    {
        List<Persona> listaPersonas = new List<Persona>();
        BaseDatos baseDatos;
        private RecyclerView recycler;
        public EditText edtNombre, edtOcupacion;
        public RadioButton rdbMasculino, rdbFemenino;
        public Button btnGuardar, btnCancelar;
        public Spinner sprEdad;
        List<int> edades = new List<int>();
        int edad;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_registro);
            inicializar();
            inicializarSpinner();
            accionarBotones();
            
        }
        public void inicializar()
        {
            baseDatos = new BaseDatos();
            baseDatos.crearBaseDatos();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);
            edtNombre = FindViewById<EditText>(Resource.Id.edtNombre);
            edtOcupacion = FindViewById<EditText>(Resource.Id.edtOcupacion);
            rdbMasculino = FindViewById<RadioButton>(Resource.Id.rdbMasculino);
            rdbFemenino = FindViewById<RadioButton>(Resource.Id.rdbFemenino);
            btnGuardar = FindViewById<Button>(Resource.Id.btnGuardarRegistro);
            btnCancelar = FindViewById<Button>(Resource.Id.btnCancelarRegistro);
            recycler = FindViewById<RecyclerView>(Resource.Id.rcvLista);
            sprEdad = FindViewById<Spinner>(Resource.Id.sprEdad);
        }
        public void llenarArray()
        {
           
            for(int i = 18; i<=60; i++)
            {
                edades.Add(i);
            }
        }
        public void inicializarSpinner()
        {
            llenarArray();
            var adaptadorSpinner = new ArrayAdapter<int>(this, Android.Resource.Layout.SimpleSpinnerItem, edades);
            sprEdad.Adapter = adaptadorSpinner;    
        }
        public void accionarBotones()
        {
            btnGuardar.Click += delegate
            {
                if (validarCamposVacios())
                {
                    guardarPersona();
                }
            };
            btnCancelar.Click += delegate
            {
                this.Finish();
            };
            sprEdad.ItemSelected += (sender, e) =>
            {
                var edadSpinner = sender as Spinner;
                edad = int.Parse(Convert.ToString(edadSpinner.GetItemAtPosition(e.Position)));
            };
        }
        public void guardarPersona()
        {
            try
            {
                Persona persona = new Persona()
                {
                    nombre = edtNombre.Text,
                    ocupacion = edtOcupacion.Text,
                    edad = edad,
                    sexo = checarRadioButton(),
                    idImagen = guardarImagen()
                };
                baseDatos.insertarPersona(persona);
                limpiarCampos();
                cargarDatos();
                regresarPantallaAnterior();
                Toast.MakeText(this, "Los datos de la persona se guardaron correctamente", ToastLength.Long).Show();
            }
            catch(Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }
        public int guardarImagen()
        {
            int idImagen = 0;
            string sexo = checarRadioButton();
            if (sexo.Equals("M"))
            {
                idImagen = Resource.Drawable.icon_man;
            }
            else
            {
                idImagen = Resource.Drawable.icon_woman;
            }
            return idImagen;
        }
        public void regresarPantallaAnterior()
        {
            var i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
            this.Finish();
        }
        public void cargarDatos()
        {
            listaPersonas = baseDatos.mostrarPersona();
            var adaptador = new AdaptadorRecyclerView(this,listaPersonas, recycler);
        }
        public void limpiarCampos()
        {
            edtNombre.Text = "";
            edtOcupacion.Text = "";
            rdbMasculino.Checked = false;
            rdbMasculino.Checked = false;
        }
        public string checarRadioButton()
        {
            string sexo = "";
            if (rdbMasculino.Checked)
            {
                sexo = "M";
            }
            if (rdbFemenino.Checked)
            {
                sexo = "F";
            }
            return sexo;
        }
        public Boolean validarCamposVacios()
        {
            if (edtNombre.Text.Equals("") ||  edtOcupacion.Text.Equals(""))
            {
                Toast.MakeText(this, "Existen Campos Vacios", ToastLength.Long).Show();
                return false;
            }
            else
            {
                return true;
            }
        }
        public override void OnBackPressed()
        {
            this.Finish();
        }
    }
}