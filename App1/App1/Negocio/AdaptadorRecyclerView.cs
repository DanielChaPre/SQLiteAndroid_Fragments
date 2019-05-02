using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using App1.Modelo;


namespace App1.Negocio
{
    public class RecyclerViewHolder : RecyclerView.ViewHolder
    {
        public TextView txtNombre { get; set; }
        public TextView txtEdad { get; set; }
        public TextView txtOcupacion { get; set; }
        public TextView txtSexo { get; set; }
        public View mRecycle { get; set; }
        
        public RecyclerViewHolder(View itemView): base(itemView)
        {
            txtNombre = itemView.FindViewById<TextView>(Resource.Id.txtNombre);
            txtEdad = itemView.FindViewById<TextView>(Resource.Id.txtEdad);
            txtOcupacion = itemView.FindViewById<TextView>(Resource.Id.txtOcupacion);
            txtSexo = itemView.FindViewById<TextView>(Resource.Id.txtSexo);
            mRecycle = itemView;
            
        }
    }
    public class AdaptadorRecyclerView : RecyclerView.Adapter
    {
      
        Fragment_Listado listado;
        MainActivity main;
        //int posicion = 0;
        private List<Persona> lstPersona = new List<Persona>();
        private RecyclerView mRecyclerView;
        public Context cont;
        public AdaptadorRecyclerView(Activity context, List<Persona> lstPersona, RecyclerView mRecyclerView)
        {
            this.lstPersona = lstPersona;
            this.mRecyclerView = mRecyclerView;
            this.cont = context;
        }
        public override int ItemCount
        {
            get
            {
                return lstPersona.Count;
            }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            RecyclerViewHolder viewHolder = holder as RecyclerViewHolder;
            viewHolder.txtNombre.Text = lstPersona[position].nombre;
            viewHolder.txtEdad.Text = "" + lstPersona[position].edad + " años";
            viewHolder.txtOcupacion.Text = lstPersona[position].ocupacion;
            viewHolder.txtSexo.Text = lstPersona[position].sexo;  
            viewHolder.mRecycle.Click += lista_Click;
           
        }
        public void lista_Click(object sender, EventArgs e)
        {
            listado = new Fragment_Listado();
            //posicion = mRecyclerView.GetChildPosition((View)sender);
             listado.pruebaIrAcciones(cont);
           // Console.WriteLine("Contexto: " + cont);
          //  main.irAcciones(posicion,lstPersona[posicion].nombre, lstPersona[posicion].edad, lstPersona[posicion].ocupacion, lstPersona[posicion].sexo);
            //listado.prueba(posicion,lstPersona[posicion].nombre, lstPersona[posicion].edad, lstPersona[posicion].ocupacion, lstPersona[posicion].sexo);
            ///listado.irAcciones(posicion, lstPersona[posicion].nombre, lstPersona[posicion].edad, lstPersona[posicion].ocupacion, lstPersona[posicion].sexo);
        }
       /* public void pruebaIrAcciones()
        {
            try
            {
                var intent = new Intent(cont, typeof(Acciones));
                StartActivity(intent);
            }
            catch (Exception ex)
            {
                Log.Info("Error", ex.Message);
            }
        }*/
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.item, parent, false);
            return new RecyclerViewHolder(itemView);
        }
    }
}