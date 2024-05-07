using S7.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S7
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }
        private void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
        { 
            _dbPath = dbPath;
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("NOMBRE ES REQUERIDO");
                Persona person = new() { Name = name };
                result = conn.Insert(person);
                StatusMessage = string.Format("Se inserto una persona", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("ERROR NO SE LOGRO INSERTAR", name, ex.Message);
            }
        }

        public void UpdatePerson(int id, string newName)
        {
            try
            {
                Init();
                if (string.IsNullOrEmpty(newName))
                    throw new Exception("NOMBRE ES REQUERIDO");
                Persona person = conn.Get<Persona>(id);
                if (person == null)
                    throw new Exception("PERSONA NO ENCONTRADA");
                person.Name = newName;
                int result = conn.Update(person);
                StatusMessage = string.Format("{0} registro(s) actualizado(s)", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("ERROR NO SE LOGRO ACTUALIZAR: {0}", ex.Message);
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                Persona person = conn.Get<Persona>(id);
                if (person == null)
                    throw new Exception("PERSONA NO ENCONTRADA");
                int result = conn.Delete<Persona>(id);
                StatusMessage = string.Format("{0} registro(s) eliminado(s)", result);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("ERROR NO SE LOGRO ELIMINAR: {0}", ex.Message);
            }
        }


        public List<Persona> getAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch(Exception ex)
            {
                StatusMessage = string.Format("Error al Listar", ex.Message);
            }
            return new List<Persona>(); 
        }

    }
}
