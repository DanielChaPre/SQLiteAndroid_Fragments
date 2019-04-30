using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App1.Modelo;

namespace App1.Negocio
{
    class MyAdapter : RecyclerView.Adapter
    {
        private List<Persona> lstPersona = new List<Persona>();
        public event EventHandler<int> itemClick;
        private RecyclerView mRecyclerView;

        public override int ItemCount => lstPersona.Count;

        public MyAdapter(List<Persona> lstpPersona)
        {
            this.lstPersona = lstPersona;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item, parent, false);
            return new MyViewHolder(v,OnClick);
        }

        private void OnClick(int obj)
        {
            itemClick?.Invoke(this, obj);
        }
    }
    class MyViewHolder : RecyclerView.ViewHolder
    {
        public TextView txtNombre;
        private readonly Action<int> listener;
        public MyViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            txtNombre = itemView.FindViewById<TextView>(Resource.Id.txtNombre);
            this.listener = listener;

            itemView.Click += ItemView_Click;
        }

        private void ItemView_Click(object sender, EventArgs e)
        {
            listener(LayoutPosition);
        }
    }
}