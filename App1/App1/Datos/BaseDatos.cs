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
using App1.Modelo;
using SQLite;

namespace App1.Datos
{
    public class BaseDatos
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool crearBaseDatos()
        {
            try
            {
                using (var coneccion = new SQLiteConnection(System.IO.Path.Combine(folder, "Personas.db")))
                {
                    coneccion.CreateTable<Persona>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool insertarPersona(Persona persona)
        {
            try
            {
                using (var coneccion = new SQLiteConnection(System.IO.Path.Combine(folder, "Personas.db")))
                {
                    coneccion.Insert(persona);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<Persona> mostrarPersona()
        {
            try
            {
                using (var coneccion = new SQLiteConnection(System.IO.Path.Combine(folder, "Personas.db")))
                {
                    return coneccion.Table<Persona>().ToList();

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool actualizarPersona(Persona persona)
        {
            try
            {
                using (var coneccion = new SQLiteConnection(System.IO.Path.Combine(folder, "Personas.db")))
                {
                    coneccion.Query<Persona>("UPDATE Persona set nombre=?,edad=?,ocupacion=?, sexo=? Where Id=?", persona.nombre, persona.edad, persona.ocupacion, persona.sexo, persona.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool eliminarPersona(Persona persona)
        {
            try
            {
                using (var coneccion = new SQLiteConnection(System.IO.Path.Combine(folder, "Personas.db")))
                {
                    coneccion.Delete(persona);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool seleccionarPersona(int Id)
        {
            try
            {
                using (var coneccion = new SQLiteConnection(System.IO.Path.Combine(folder, "Personas.db")))
                {
                    coneccion.Query<Persona>("SELECT * FROM Personas Where Id=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
    }
}