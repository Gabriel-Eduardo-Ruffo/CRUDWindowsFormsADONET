using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TutoCRUD.Models
{
    class MyConnectionDB
    {
        private string connectionString = "Data Source=(local); Initial Catalog=TestCrud;Integrated Security=true";

        public bool ConnectionOK()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    conn.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            
        }

        //Devuelve toda la lista de datos y las muestra en el dataGrid1 en MainForm
        public List<DataDB> GetDataDB()
        {
            List<DataDB> listDataDB = new List<DataDB>();

            string query = "SELECT Id, FirstName, LastName, Age FROM Persons";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();//abrimos conexion
                    SqlDataReader reader = command.ExecuteReader();//ejecuta consulta a DB
                    while (reader.Read())//Mientras haya datos para leer continua leyendo lo que trajo de la DB
                    {
                        DataDB dataDB = new DataDB();
                        dataDB.Id = reader.GetInt32(0);
                        dataDB.FirstName = reader.GetString(1);
                        dataDB.LastName = reader.GetString(2);
                        dataDB.Age = reader.GetInt32(3);
                        listDataDB.Add(dataDB);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch(Exception ex)
                {
                    throw new Exception("Hubo un error en la consulta a la DB:" + ex.Message);
                }
            }
            return listDataDB;
        }

        //Devuelve solo un objeto que busca por el ID para mostrar los datos en el CRUDForm y que se puedan editar
        public DataDB GetIdDataDB( int _id)
        {
            string query = "SELECT Id, FirstName, LastName, Age FROM Persons WHERE Id=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", _id);

                try
                {
                    connection.Open();//abrimos conexion
                    SqlDataReader reader = command.ExecuteReader();//ejecuta consulta a DB
                    reader.Read();//Leemos si encontramos el dato buscado

                    DataDB dataDB = new DataDB();
                    dataDB.Id = reader.GetInt32(0);
                    dataDB.FirstName = reader.GetString(1);
                    dataDB.LastName = reader.GetString(2);
                    dataDB.Age = reader.GetInt32(3);

                    reader.Close();
                    connection.Close();

                    return dataDB;
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error en la consulta a la DB:" + ex.Message);
                }
            }
            
        }

        //Crea un registro nuevo por medio del CRUDForm Accion CREATE
        public void Create(string _firstName, string _lastName, int _age)
        {
            //para evitar el sql inyection (hacking para arruinar la db)
            string query = "INSERT INTO Persons (FirstName, LastName, Age) VALUES (@firstName,@lastName,@age)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", _firstName);
                command.Parameters.AddWithValue("@lastName", _lastName);
                command.Parameters.AddWithValue("@age", _age);

                try
                {
                    connection.Open();//abrimos conexion
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error en la consulta a la DB:" + ex.Message);
                }
            }
        }

        //Actualiza los datos en la DB accion UPDATE
        public void Update(int _id, string _firstName, string _lastName, int _age)
        {
            //para evitar el sql inyection (hacking para arruinar la db)
            string query = "UPDATE Persons SET FirstName=@firstName, LastName=@lastName, Age=@age WHERE Id=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", _id);
                command.Parameters.AddWithValue("@firstName", _firstName);
                command.Parameters.AddWithValue("@lastName", _lastName);
                command.Parameters.AddWithValue("@age", _age);

                try
                {
                    connection.Open();//abrimos conexion
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error en la consulta a la DB:" + ex.Message);
                }
            }
        }

        //Borrado de datos en la DB accion Delete
        public void Delete(int _id)
        {
            //para evitar el sql inyection (hacking para arruinar la db)
            string query = "DELETE FROM Persons WHERE Id=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", _id);

                try
                {
                    connection.Open();//abrimos conexion
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception("Hubo un error en la consulta a la DB:" + ex.Message);
                }
            }
        }
    }
}
