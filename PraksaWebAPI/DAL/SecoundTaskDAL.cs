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
    public class SecoundTaskDAL : ITaskDAL
    {
        private readonly IConfiguration configuration;
        private string ConnectionString;
        public SecoundTaskDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = ProtectionHelper.Singletion.GetSectionValue("ConnectionStrings:Connection2");
        }

        public int AddTask(TaskModel newTask)
        {
            int res = 0;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();

                    command.CommandText = "INSERT INTO Task VALUES(NULL,@Description, @DueDate,@Finished, @CreatedBy); SELECT last_insert_rowid()";

                    SQLiteParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Description";
                    parameter.Value = newTask.Description;
                    command.Parameters.Add(parameter);

                    SQLiteParameter parameter1 = command.CreateParameter();
                    parameter1.DbType = System.Data.DbType.DateTime;
                    parameter1.ParameterName = "@DueDate";
                    parameter1.Value = newTask.DueDate;
                    command.Parameters.Add(parameter1);


                    SQLiteParameter parameter3 = command.CreateParameter();
                    parameter3.DbType = System.Data.DbType.Boolean;
                    parameter3.ParameterName = "@Finished";
                    parameter3.Value = newTask.Finished;
                    command.Parameters.Add(parameter3);

                    SQLiteParameter parameter4 = command.CreateParameter();
                    parameter4.DbType = System.Data.DbType.Boolean;
                    parameter4.ParameterName = "@CreatedBy";
                    parameter4.Value = newTask.CreatedBy;
                    command.Parameters.Add(parameter4);




                    SQLiteDataReader reader = command.ExecuteReader();
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
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();

                    command.CommandText = "INSERT INTO TaskUser VALUES(NULL,@TaskID, @EmployeeID)";

                    SQLiteParameter parameter = command.CreateParameter();
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
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
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
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {

                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();

                    command.CommandText = $"UPDATE Task SET FINISHED = 1 WHERE ID = @ID;SELECT EmployeeID FROM TaskUser WHERE TaskID=@ID" ;

                    SQLiteParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    SQLiteDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        res.Add(Int32.Parse( reader[0].ToString()));
                        

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

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    Models.Task t = null;
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM Task";

                    SQLiteDataReader reader = command.ExecuteReader();
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

        public List<Models.Task> TasksForUser(long id)
        {
            List<Models.Task> tasks = new List<Models.Task>();
            Models.Task t = null;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = @"select t.ID, t.Description, t.DueDate, t.Finished, t.CreatedBy
                                                from task  t join TaskUser tu on t.ID = tu.TaskID
                                                where tu.EmployeeID = @ID";


                    SQLiteParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "ID";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    SQLiteDataReader reader = command.ExecuteReader();
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
