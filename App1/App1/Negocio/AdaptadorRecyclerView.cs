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
        public ImageView imgSexo { get; set; }
        public View mRecycle { get; set; }
        
        public RecyclerViewHolder(View itemView): base(itemView)
        {
            
            txtNombre = itemView.FindViewById<TextView>(Resource.Id.txtNombre);
            txtEdad = itemView.FindViewById<TextView>(Resource.Id.txtEdad);
            txtOcupacion = itemView.FindViewById<TextView>(Resource.Id.txtOcupacion);
            txtSexo = itemView.FindViewById<TextView>(Resource.Id.txtSexo);
            imgSexo = itemView.FindViewById<ImageView>(Resource.Id.imgSexo);
            mRecycle = itemView;
        }
    }
    public class AdaptadorRecyclerView : RecyclerView.Adapter
    {
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
            viewHolder.txtEdad.Text = "" + lstPersona[position].edad + "  años";
            viewHolder.txtOcupacion.Text = lstPersona[position].ocupacion;
            viewHolder.txtSexo.Text = lstPersona[position].sexo;
            //viewHolder.imgSexo.SetImageResource(lstPersona[position].idImagen);
            checarImagen(viewHolder.txtSexo.Text, viewHolder);
            viewHolder.mRecycle.Click += lista_Click;
        }
        public void lista_Click(object sender, EventArgs e)
        {
             int position = mRecyclerView.GetChildAdapterPosition((View)sender);
             string edad = lstPersona[position].edad.ToString();
             int id = lstPersona[position].Id;
             try
             {
                 irAcciones(cont, id, lstPersona[position].nombre, edad, lstPersona[position].ocupacion, lstPersona[position].sexo);
             }
             catch(Exception ex)
             {
                 Log.Info("Error", ex.Message);
             }
        }
        public void irAcciones(Context context,int id, string nombre, string edad, string ocupacion, string sexo)
        {
            Intent intent = new Intent(context, typeof(Acciones));
            intent.PutExtra(Acciones.ID, id);
            intent.PutExtra(Acciones.NOMBRE, nombre);
            intent.PutExtra(Acciones.EDAD, edad);
            intent.PutExtra(Acciones.OCUPACION, ocupacion);
            intent.PutExtra(Acciones.SEXO, sexo);
            context.StartActivity(intent);
        }
        public void checarImagen(string sexo, RecyclerViewHolder holder)
        {
            if (sexo.Equals("M"))
            {
                holder.imgSexo.SetImageResource(Resource.Drawable.icon_man);
            }else if (sexo.Equals("F"))
            {
                holder.imgSexo.SetImageResource(Resource.Drawable.icon_woman);
            }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.item, parent, false);
            return new RecyclerViewHolder(itemView);
        }
    }
}