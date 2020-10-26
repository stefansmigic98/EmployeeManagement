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
    public class TaskCommentDAL : ITaskCommentDAL
    {
        private readonly IConfiguration configuration;
        private string ConnectionString;

        public TaskCommentDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = ProtectionHelper.Singletion.GetSectionValue("ConnectionStrings:Connection1");
        }

        public int AddTaskComment(TaskComment taskComment)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO TaskComment VALUES(@Content, @EmployeeID, @TaskID)";

                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Content";
                    parameter.Value = taskComment.Content;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@EmployeeID";
                    parameter.Value = taskComment.EmployeeID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@TaskID";
                    parameter.Value = taskComment.TaskID;
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

        public List<TaskComment> CommentsForTask(int taskid)
        {
            List<TaskComment> comments = new List<TaskComment>();
            TaskComment t = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM TaskComment where TaskID = @TaskID";


                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "TaskID";
                    parameter.Value = taskid;
                    command.Parameters.Add(parameter);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        t = new TaskComment
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Content = reader[1].ToString(),
                            EmployeeID = Int32.Parse(reader[2].ToString()),


                            TaskID = Int32.Parse(reader[3].ToString())
                        };

                        comments.Add(t);

                    }
                    reader.Close();
                    connection.Close();
                    return comments;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return comments;
        }
    }
}
