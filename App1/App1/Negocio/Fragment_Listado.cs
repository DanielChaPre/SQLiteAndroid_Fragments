using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using App1.Datos;
using App1.Modelo;

namespace App1.Negocio
{
    [Activity(Label = "Listado de Personas")]
    public class Fragment_Listado:Fragment
    {
        private SwipeRefreshLayout mSwipeRefreshLayout;
        private RecyclerView recycler;
        private MyAdapter adaptador;
        private RecyclerView.LayoutManager layoutManager;
        private List<Persona> lstPersona = new List<Persona>();
        BaseDatos baseDatos;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_listado, container, false);
            inicializar(view);
            inicializarRecyclerView();
            inicializarSwipeRefreshLayout();
            cargarDatos();
           // accionarLista();
           // recuperarInformacion();
            return view;
        }
        public void inicializar(View view)
        {
            baseDatos = new BaseDatos();
            baseDatos.crearBaseDatos();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);
            recycler = view.FindViewById<RecyclerView>(Resource.Id.rcvLista);
            mSwipeRefreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swlActualizar);
        }
        public void inicializarSwipeRefreshLayout()
        {
            mSwipeRefreshLayout.SetColorScheme(Android.Resource.Color.HoloBlueBright, Android.Resource.Color.HoloBlueDark, Android.Resource.Color.HoloGreenLight, Android.Resource.Color.HoloRedLight);
            mSwipeRefreshLayout.Refresh += mSwipeRefreshLayout_Refresh;
        }

        private void mSwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mSwipeRefreshLayout.Refreshing = false;
        } 

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //Will run on separate thread
            Thread.Sleep(1000);
        }

        public void inicializarRecyclerView()
        {
            recycler.HasFixedSize = true;
            Fragment_Listado fragment_Listado = this;
            Context context = fragment_Listado.Context;
            layoutManager = new LinearLayoutManager(context);
            recycler.SetLayoutManager(layoutManager);
            recycler.SetItemAnimator(new DefaultItemAnimator());
        }
        public void cargarDatos()
        {
            lstPersona = baseDatos.mostrarPersona();
            adaptador = new MyAdapter(lstPersona);
            recycler.SetAdapter(adaptador);
           // validarListaVacia();
        }
        /*public void recuperarInformacion()
        {
            try
            {
                string nombre = "";
                int edad = 0;
                string ocupacion = "";
                string sexo = "";
                nombre = adaptador.recuperarNombre();
                edad = adaptador.recuperarEdad();
                ocupacion = adaptador.recuperarOcupacion();
                sexo = adaptador.recuperarSexo();
                Console.WriteLine("Informacion");
                Console.WriteLine("Nombre: " + nombre);
                Console.WriteLine("Edad: " + edad);
                Console.WriteLine("Ocupacion: " + ocupacion);
                Console.WriteLine("Sexo: " + sexo);

            }
            catch(Exception ex)
            {
                Log.Info("Error", ex.Message);
            }
        }*/
       /* public void irAcciones2()
        {
            Fragment_Listado adaptadorRecyclerView = this;
            Context context = adaptadorRecyclerView.Context;
            Intent intent = new Intent(context, typeof(Acciones));
            StartActivity(intent);
        }
        public void irAcciones(int id, string nombre, string edad, string ocupacion, string sexo)
        {
            Fragment_Listado adaptadorRecyclerView = this;
            Context context = adaptadorRecyclerView.Context;
            Intent intent = new Intent(context, typeof(Acciones));
            intent.PutExtra(Acciones.ID, id);
            intent.PutExtra(Acciones.NOMBRE, nombre);
            intent.PutExtra(Acciones.EDAD, edad);
            intent.PutExtra(Acciones.OCUPACION, ocupacion);
            intent.PutExtra(Acciones.SEXO, sexo);
            StartActivity(intent);
        }
       /* public void seleccionaritemLista()
        {
            adaptador.itemClick += (s, e) => {
                var txtNombre = e.View.FindViewById<TextView>(Resource.Id.textView1);
                var txtEdad = e.View.FindViewById<TextView>(Resource.Id.textView2);
                var txtOcupacion = e.View.FindViewById<TextView>(Resource.Id.textView3);
                var txtSexo = e.View.FindViewById<TextView>(Resource.Id.textView4);
                try
                {
                    txtNombre.Tag = e.Id;
                    irAcciones(int.Parse(txtNombre.Tag.ToString()), txtNombre.Text, txtEdad.Text.Substring(0, 2), txtOcupacion.Text, txtSexo.Text);
                }
                catch (Exception ex)
                {
                    Log.Info("Error", ex.Message);
                }
            };
        }
        /*public void accionarLista()
        {
            recycler.Click += recycler_Cliked;
        }

        private void recycler_Cliked(object sender, EventArgs e)
        {
           int posicion = recycler.GetChildPosition((View)sender);
           Console.WriteLine("Nombre de la persona " + lstPersona[posicion].nombre);
        }*/
        /* public Boolean validarListaVacia()
{
    if (recycler.)
    {
        Toast.MakeText(this, "La lista esta vacia", ToastLength.Long).Show();
        Toast.MakeText(this, "Registre a una persona", ToastLength.Long).Show();
        irRegistro();
        return false;
    }
    else
    {
        return true;
    }
}*/
    }
}