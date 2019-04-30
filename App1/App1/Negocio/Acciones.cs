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
    [Activity(Label = "Modificacion")]
    public class Acciones:Activity
    {
        private RecyclerView recycler;
        List<Persona> listaPersonas = new List<Persona>();
        public static readonly string NOMBRE = "nombre";
        public static readonly string EDAD = "edad";
        public static readonly string OCUPACION = "ocupacion";
        public static readonly string SEXO = "sexo";
        public static readonly string ID = "id";
        public Button btnActualizar, btnEliminar;
        BaseDatos baseDatos;
        public EditText edtNombre, edtEdad, edtOcupacion;
        public RadioButton rdbMasculino, rdbFemenino;
        public int id;
        public string sexo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_movimientos);
            inicializar();
            agarrarDatosLista();
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
            rdbMasculino = FindViewById<RadioButton>(Resource.Id.rdbMasculinoA);
            rdbFemenino = FindViewById<RadioButton>(Resource.Id.rdbFemeninoA);
            btnActualizar = FindViewById<Button>(Resource.Id.btnActualizarRegistro);
            btnEliminar = FindViewById<Button>(Resource.Id.btnEliminarRegistro);
        }
        public void accionarBotones()
        {
            btnActualizar.Click += delegate
            {
                if (validarCamposVacios())
                {
                    actualizarPersona();
                }
            };
            btnEliminar.Click += delegate
            {
                if (validarBotonEliminar())
                {
                    eliminarPersona();
                }
            };
            edtEdad.TextChanged += delegate
            {
                validarEdad();
            };
        }
        public void agarrarDatosLista()
        {
            edtNombre.Text = Intent.Extras.GetString(NOMBRE);
            edtEdad.Text = Intent.Extras.GetString(EDAD);
            edtOcupacion.Text = Intent.Extras.GetString(OCUPACION);
            seleccionarRadioButton(Intent.Extras.GetString(SEXO));
            id = Intent.Extras.GetInt(ID);
        }
        public void seleccionarRadioButton(string sexo)
        {
            if (sexo.Equals("M"))
            {
                rdbMasculino.Checked = true;
                rdbFemenino.Checked = false;
            }
            else if (sexo.Equals("F"))
            {
                rdbFemenino.Checked = true;
                rdbMasculino.Checked = false;
            }
        }
        public void actualizarPersona()
        {
            Persona persona = new Persona()
            {
                Id = id,
                nombre = edtNombre.Text,
                edad = int.Parse(edtEdad.Text),
                ocupacion = edtOcupacion.Text,
                sexo = checarRadioButton()
            };
            baseDatos.actualizarPersona(persona);
            limpiarCampos();
            cargarDatos();
            regresarPantallaAnterior();
        }
        public void eliminarPersona()
        {
            Persona persona = new Persona()
            {
                Id = id,
                nombre = edtNombre.Text,
                ocupacion = edtOcupacion.Text,
                edad = int.Parse(edtEdad.Text),
                sexo = checarRadioButton()
            };
            baseDatos.eliminarPersona(persona);
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
            var adaptador = new MyAdapter(listaPersonas);
        }

        public void limpiarCampos()
        {
            edtNombre.Text = "";
            edtEdad.Text = "";
            edtOcupacion.Text = "";
            rdbMasculino.Checked = false;
            rdbFemenino.Checked = false;
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
        public Boolean validarBotonEliminar()
        {
            if (edtNombre.Text.Equals("") || edtEdad.Text.Equals("") || edtOcupacion.Text.Equals(""))
            {
                Toast.MakeText(this, "No se puede eliminar", ToastLength.Long).Show();
                Toast.MakeText(this, "No se a seleccionado a una persona", ToastLength.Long).Show();
                return false;
            }
            else
            {
                return true;
            }
        }
        public override void OnBackPressed()
        {
            regresarPantallaAnterior();
        }
    }
}