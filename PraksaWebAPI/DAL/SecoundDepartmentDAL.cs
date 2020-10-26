using LoboAspNET1.Helpers;
using Microsoft.Extensions.Configuration;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.DAL
{
    public class SecoundDepartmentDAL : IDepartmentDAL
    {
        private readonly IConfiguration configuration;
        private string ConnectionString;
        public SecoundDepartmentDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = ProtectionHelper.Singletion.GetSectionValue("ConnectionStrings:Connection2");
        }

        public int AddDepartment(Department department)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Department VALUES(NULL,@Name)";

                    SQLiteParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Name";
                    parameter.Value = department.Name;
                    command.Parameters.Add(parameter);


                    command.ExecuteNonQuery();
                    connection.Close();

                    return 0;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public int DeleteDepartment(long id)
        {
            int res = 1;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = $"DELETE FROM Department where ID = {id}";
                    res = command.ExecuteNonQuery();


                    connection.Close();

                    return res;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }

        public List<Department> GetDepartments()
        {

            List<Department> departments = new List<Department>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM Department";

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        departments.Add(new Department()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Name = reader[1].ToString()

                        });

                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return departments;
        }

        public int UpdateDepartment(Department update)
        {
            int res = 1;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {

                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Department
                                            SET Name = @Name
                                            WHERE ID = @ID";

                    SQLiteParameter parameter = command.CreateParameter();
                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.Value = update.ID;
                    parameter.ParameterName = "@ID";
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Name";
                    parameter.Value = update.Name;
                    command.Parameters.Add(parameter);




                    res = command.ExecuteNonQuery();


                    connection.Close();

                    return res;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


            }
        }

        public Department WithMostEmployees()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                Department max = new Department();
                try
                {

                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("BestDepartment", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        max = new Department()
                        {
                            ID = Int32.Parse(reader[1].ToString()),
                            Name = reader[0].ToString(),

                        };

                    }
                    reader.Close();
                    connection.Close();
                    return max;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


            }
        }
    }
}
