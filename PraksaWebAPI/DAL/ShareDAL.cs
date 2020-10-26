using LoboAspNET1.Helpers;
using Microsoft.Extensions.Configuration;
using PraksaWebAPI.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PraksaWebAPI.DAL
{
    public class ShareDAL : IShareDAL
    {
        public string connectionString;
        private readonly IConfiguration _configuration;
        public ShareDAL(IConfiguration configuration)
        {
            this._configuration = configuration;
            connectionString = ProtectionHelper.Singletion.GetSectionValue("ConnectionStrings:Connection1");
        }

        public string CreateShare(string shareId, string fileId, string user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {

                    connection.Open();
                    SqlCommand command = new SqlCommand("create_share", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    SqlParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@shareId";
                    parameter.Value = shareId;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@fileId";
                    parameter.Value = fileId;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@user";
                    parameter.Value = user;
                    command.Parameters.Add(parameter);



                    command.ExecuteNonQuery();
                    connection.Close();


                    return "";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
