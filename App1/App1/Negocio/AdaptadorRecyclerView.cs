using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
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
        int posicion = 0;
        private List<Persona> lstPersona = new List<Persona>();
        private RecyclerView mRecyclerView;
        public AdaptadorRecyclerView(List<Persona> lstPersona, RecyclerView mRecyclerView)
        {
            this.lstPersona = lstPersona;
            this.mRecyclerView = mRecyclerView;
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
           /* viewHolder.mRecycle.SetOnClickListener(new View.IOnClickListener()
            {

            });   */   
            viewHolder.mRecycle.Click += mMainView_Click;
            
        }

        public void mMainView_Click(object sender, EventArgs e)
        {
            Acciones acciones = new Acciones();
             posicion = mRecyclerView.GetChildPosition((View)sender);
              string nombre = "";
              nombre = lstPersona[posicion].nombre;
              string edad = "";
              edad = Convert.ToString(lstPersona[posicion].edad);
              string ocupacion = "";
              ocupacion = lstPersona[posicion].ocupacion;
              string sexo = "";
              sexo = lstPersona[posicion].sexo;
            acciones.edtNombre.Text = nombre;
            acciones.edtEdad.Text = edad;
            acciones.edtOcupacion.Text = ocupacion;
            acciones.sexo = sexo;
            /*Console.WriteLine("Informacion");
            Console.WriteLine("Nombre: " + nombre);
            Console.WriteLine("Edad: " + edad);
            Console.WriteLine("Ocupacion: " + ocupacion);
            Console.WriteLine("Sexo: " + sexo);*/
            //listado.irAcciones2();
        }

        public string recuperarNombre()
        {
            string nombre = "";
            nombre = lstPersona[posicion].nombre;
            return nombre;
        }
        public string recuperarEdad()
        {
            string edad = "";
            edad =Convert.ToString(lstPersona[posicion].edad);
            return edad;
        }
        public string recuperarOcupacion()
        {
            string ocupacion = "";
            ocupacion = lstPersona[posicion].ocupacion;
            return ocupacion;
        }
        public string recuperarSexo()
        {
            string sexo = "";
            sexo = lstPersona[posicion].sexo;
            return sexo;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.item, parent, false);
            return new RecyclerViewHolder(itemView);
        }
        
    }
}