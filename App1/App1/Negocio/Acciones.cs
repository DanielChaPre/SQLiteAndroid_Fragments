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
    public class Acciones : Activity
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
        public EditText edtNombre,edtOcupacion;
        public RadioButton rdbMasculino, rdbFemenino;
        public Spinner sprEdad;
        List<int> edades = new List<int>();
        int edad;
        public int id;
        public string sexo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_movimientos);
            inicializar();
            inicializarSpinner();
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
            rdbMasculino = FindViewById<RadioButton>(Resource.Id.rdbMasculinoA);
            rdbFemenino = FindViewById<RadioButton>(Resource.Id.rdbFemeninoA);
            btnActualizar = FindViewById<Button>(Resource.Id.btnActualizarRegistro);
            btnEliminar = FindViewById<Button>(Resource.Id.btnEliminarRegistro);
            recycler = FindViewById<RecyclerView>(Resource.Id.rcvLista);
            sprEdad = FindViewById<Spinner>(Resource.Id.sprEdad);
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
            sprEdad.ItemSelected += (sender, e) =>
            {
                var edadSpinner = sender as Spinner;
                edad = int.Parse(Convert.ToString(edadSpinner.GetItemAtPosition(e.Position)));
            };

        }
        public void agarrarDatosLista()
        {
            edtNombre.Text = Intent.Extras.GetString(NOMBRE);
            seleccionarSpinner(Intent.Extras.GetString(EDAD));
            edtOcupacion.Text = Intent.Extras.GetString(OCUPACION);
            seleccionarRadioButton(Intent.Extras.GetString(SEXO));
            id = Intent.Extras.GetInt(ID);
        }
        public void seleccionarSpinner(string edadExtraida)
        {
            int indice = int.Parse(edadExtraida)-18;
            sprEdad.SetSelection(indice);
        }
        public void llenarArray()
        {

            for (int i = 18; i <= 60; i++)
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
            try
            {
                Persona persona = new Persona()
                {
                    Id = id,
                    nombre = edtNombre.Text,
                    edad = edad,
                    ocupacion = edtOcupacion.Text,
                    sexo = checarRadioButton(),
                    idImagen = guardarImagen()
                };
                baseDatos.actualizarPersona(persona);
                limpiarCampos();
                cargarDatos();
                regresarPantallaAnterior();
                Toast.MakeText(this, "Los datos de la persona se actualizaron correctamente", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
        }
        public void eliminarPersona()
        {
            try
            {
                Persona persona = new Persona()
                {
                    Id = id,
                    nombre = edtNombre.Text,
                    ocupacion = edtOcupacion.Text,
                    edad = edad,
                    sexo = checarRadioButton(),
                    idImagen = guardarImagen()
                };
                baseDatos.eliminarPersona(persona);
                limpiarCampos();
                cargarDatos();
                regresarPantallaAnterior();
                Toast.MakeText(this, "La persona se elimino correctamente", ToastLength.Long).Show();
            }
            catch (Exception ex)
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
                idImagen = 0;
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
            if (edtNombre.Text.Equals("") || edtOcupacion.Text.Equals(""))
            {
                Toast.MakeText(this, "Existen Campos Vacios", ToastLength.Long).Show();
                return false;
            }
            else
            {
                return true;
            }
        }
        public Boolean validarBotonEliminar()
        {
            if (edtNombre.Text.Equals("") || edtOcupacion.Text.Equals(""))
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
            this.Finish();
        }
    }
}