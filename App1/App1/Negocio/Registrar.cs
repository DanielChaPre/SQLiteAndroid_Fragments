using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
        public EditText edtNombre, edtEdad, edtOcupacion;
        public RadioButton rdbMasculino, rdbFemenino;
        public Button btnGuardar, btnCancelar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_registro);
            inicializar();
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
            edtEdad = FindViewById<EditText>(Resource.Id.edtEdad);
            rdbMasculino = FindViewById<RadioButton>(Resource.Id.rdbMasculino);
            rdbFemenino = FindViewById<RadioButton>(Resource.Id.rdbFemenino);
            btnGuardar = FindViewById<Button>(Resource.Id.btnGuardarRegistro);
            btnCancelar = FindViewById<Button>(Resource.Id.btnCancelarRegistro);
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
                regresarPantallaAnterior();
            };
            edtEdad.TextChanged += delegate
            {
                validarEdad();
            };
        }

        public void guardarPersona()
        {
            Persona persona = new Persona()
            {
                nombre = edtNombre.Text,
                ocupacion = edtOcupacion.Text,
                edad = int.Parse(edtEdad.Text),
                sexo = checarRadioButton()
            };
            baseDatos.insertarPersona(persona);
            limpiarCampos();
            cargarDatos();
            regresarPantallaAnterior();
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
           var adaptador = new AdaptadorRecyclerView(listaPersonas);
        }
        public void limpiarCampos()
        {
            edtNombre.Text = "";
            edtEdad.Text = "";
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
            if (edtNombre.Text.Equals("") || edtEdad.Text.Equals("") || edtOcupacion.Text.Equals(""))
            {
                Toast.MakeText(this, "Existen Campos Vacios", ToastLength.Long).Show();
                return false;
            }
            else
            {
                return true;
            }
        }
        public Boolean validarEdad()
        {
            Boolean validacion = false;
            try
            {
                int edad = int.Parse(edtEdad.Text);
                if (edad >= 100)
                {
                    Toast.MakeText(this, "La edad es mayor a 100 años", ToastLength.Long).Show();
                    Toast.MakeText(this, "No se puede registrar", ToastLength.Long).Show();
                    validacion = false;
                }
                else
                {
                    validacion = true;
                }
            }
            catch (Exception ex)
            {
                Log.Info("Error", ex.Message);
            }
            return validacion;
        }
        public override void OnBackPressed()
        {
            regresarPantallaAnterior();
        }
    }
}