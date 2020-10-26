using LoboAspNET1.Helpers;
using Microsoft.Extensions.Configuration;
using PraksaWebAPI.DAL.Interfaces;
using PraksaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.DAL
{
    public class TaskDAL : ITaskDAL
    {
        private readonly IConfiguration configuration;
        private string ConnectionString;
        public TaskDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = ProtectionHelper.Singletion.GetSectionValue("ConnectionStrings:Connection1");
        }

        public int AddTask(TaskModel newTask)
        {
            int res = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();

                    command.CommandText = "INSERT INTO Task VALUES(@Description, @DueDate,@Finished, @CreatedBy); SELECT SCOPE_IDENTITY();";

                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Description";
                    parameter.Value = newTask.Description;
                    command.Parameters.Add(parameter);

                    SqlParameter parameter1 = command.CreateParameter();
                    parameter1.DbType = System.Data.DbType.DateTime;
                    parameter1.ParameterName = "@DueDate";
                    parameter1.Value = newTask.DueDate;
                    command.Parameters.Add(parameter1);


                    SqlParameter parameter3 = command.CreateParameter();
                    parameter3.DbType = System.Data.DbType.Boolean;
                    parameter3.ParameterName = "@Finished";
                    parameter3.Value = newTask.Finished;
                    command.Parameters.Add(parameter3);

                    SqlParameter parameter4 = command.CreateParameter();
                    parameter4.DbType = System.Data.DbType.Boolean;
                    parameter4.ParameterName = "@CreatedBy";
                    parameter4.Value = newTask.CreatedBy;
                    command.Parameters.Add(parameter4);




                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        res = Int32.Parse(reader[0].ToString());

                    }
                    reader.Close();
                    connection.Close();


                    return res;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public int AddTaskUser(TaskUser newTaskUser)
        {
            int res = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();

                    command.CommandText = "INSERT INTO TaskUser VALUES(@TaskID, @EmployeeID)";

                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@TaskID";
                    parameter.Value = newTaskUser.TaskID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@EmployeeID";
                    parameter.Value = newTaskUser.EmployeeID;
                    command.Parameters.Add(parameter);


                    command.ExecuteNonQuery();
                    connection.Close();


                    return res;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public int DeleteTask(long id)
        {
            int res = 1;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = $"DELETE FROM Task where ID = {id}";
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

        public List<int> FinishTask(long id, string role)
        {

            List<int> res = new List<int>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {

                    connection.Open();
                    SqlCommand command = connection.CreateCommand();

                    command.CommandText = $"UPDATE Task SET FINISHED = 1 WHERE ID = @ID;SELECT EmployeeID FROM TaskUser WHERE TaskID=@ID";

                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        res.Add(Int32.Parse(reader[0].ToString()));


                    }
                    reader.Close();
                    connection.Close();


                    return res;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


            }

        }

        public List<Models.Task> GetTasks()
        {
            List<Models.Task> tasks = new List<Models.Task>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM Task";

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tasks.Add(new Models.Task()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Description = reader[1].ToString(),
                            DueDate = DateTime.Parse(reader[2].ToString()),
                            
                            Finished = bool.Parse(reader[3].ToString()),
                            CreatedBy = Int32.Parse(reader[4].ToString())


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
            return tasks;
        }

        public List<Models.Task> TasksForUser(long id)
        {
            List<Models.Task> tasks = new List<Models.Task>();
            Models.Task t = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = @"select t.ID, t.Description, t.DueDate, t.Finished, t.CreatedBy
                                                from task  t join TaskUser tu on t.ID = tu.TaskID
                                                where tu.EmployeeID = @ID";


                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        t = new Models.Task()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Description = reader[1].ToString(),
                            DueDate = DateTime.Parse(reader[2].ToString()),


                            CreatedBy = Int32.Parse(reader[4].ToString())
                        };
                        if (reader[3].ToString() == "0")
                            t.Finished = false;
                        else
                            t.Finished = true;

                        tasks.Add(t);

                    }
                    reader.Close();
                    connection.Close();
                    return tasks;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return tasks;
        }
    }
}
