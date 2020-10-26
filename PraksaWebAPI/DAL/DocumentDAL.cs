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
    public class DocumentDAL : IDocumentDAL
    {
        private readonly IConfiguration configuration;
        private string ConnectionString;
        public DocumentDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = ProtectionHelper.Singletion.GetSectionValue("ConnectionStrings:Connection1");
        }

        public List<Document> MyDocuments(long EmployeeID)
        {
            List<Document> documents = new List<Document>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {

                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT *  FROM Document where EmployeeID = @id";



                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@id";
                    parameter.Value = EmployeeID;
                    command.Parameters.Add(parameter);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        documents.Add(new Document()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            DrivePAth = reader[2].ToString(),
                            CreationDate = DateTime.Parse(reader[3].ToString()),
                            DriveDocumentID = reader[4].ToString(),
                            EmployeeID = Int32.Parse( reader[5].ToString())

                        });
                    }


                    reader.Close();
                    connection.Close();

                    return documents;



                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Document> SharedWithUser(long id)
        {
            List<Document> documents = new List<Document>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {

                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = @"select d.ID, d.Name, d.DrivePath, d.CreationDate, d.DriveDocumentID, d.EmployeeID
                                          from Document as d
                                          join Share as s on d.ID = s.DocumentID
                                          where s.EmployeeID = @id";



                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@id";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        documents.Add(new Document()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            DrivePAth = reader[2].ToString(),
                            CreationDate = DateTime.Parse(reader[3].ToString()),
                            DriveDocumentID = reader[4].ToString(),
                            EmployeeID = Int32.Parse(reader[5].ToString())

                        });
                    }


                    reader.Close();
                    connection.Close();

                    return documents;



                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void UploadDocument(FileModel model, long employeeID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Document VALUES(@Name, @DrivePath,@CreationDate, @DriveDocumentID, @EmployeeID)";

                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Name";
                    parameter.Value = model.Name;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@DrivePath";
                    parameter.Value = model.DrivePath;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.DateTime;
                    parameter.ParameterName = "@CreationDate";
                    parameter.Value = DateTime.Now;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@DriveDocumentID";
                    parameter.Value = model.Id;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@EmployeeID";
                    parameter.Value = employeeID;
                    command.Parameters.Add(parameter);



                    command.ExecuteNonQuery();
                    connection.Close();

                    

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
