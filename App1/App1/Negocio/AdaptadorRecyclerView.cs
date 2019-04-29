using System;
using System.Collections.Generic;
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
        public RecyclerViewHolder(View itemView): base(itemView)
        {
            txtNombre = itemView.FindViewById<TextView>(Resource.Id.txtNombre);
            txtEdad = itemView.FindViewById<TextView>(Resource.Id.txtEdad);
            txtOcupacion = itemView.FindViewById<TextView>(Resource.Id.txtOcupacion);
            txtSexo = itemView.FindViewById<TextView>(Resource.Id.txtSexo);
        }
    }
    public class AdaptadorRecyclerView : RecyclerView.Adapter
    {
        private List<Persona> lstPersona = new List<Persona>();

        public AdaptadorRecyclerView(List<Persona> lstPersona)
        {
            this.lstPersona = lstPersona;
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
            viewHolder.txtEdad.Text   = ""+lstPersona[position].edad+" años";
            viewHolder.txtOcupacion.Text = lstPersona[position].ocupacion;
            viewHolder.txtSexo.Text = lstPersona[position].sexo;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View itemView = inflater.Inflate(Resource.Layout.item, parent, false);
            return new RecyclerViewHolder(itemView);
        }
    }
}