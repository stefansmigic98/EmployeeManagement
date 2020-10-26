﻿using LoboAspNET1.Helpers;
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
    public class SecoundRoleDAL : IRoleDAL
    {
        private readonly IConfiguration configuration;
        private string ConnectionString;
        public SecoundRoleDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = ProtectionHelper.Singletion.GetSectionValue("ConnectionStrings:Connection2");
        }

        public int AddArole(Role newRole)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Role VALUES(NULL,@RoleName)";

                    SQLiteParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@RoleName";
                    parameter.Value = newRole.RoleName;
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

        public int DeleteRole(long id)
        {
            int res = 1;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = $"DELETE FROM Role where ID = {id}";
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

        public Role GetRoleByID(long id)
        {
            Role role = new Role();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();

                    command.CommandText = $"SELECT * FROM Role WHERE ID LIKE '{id}%'";


                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        role = new Role()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            RoleName = reader[1].ToString(),


                        };

                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            return role;
        }

        public List<Role> GetRoles(string name)
        {
            List<Role> roles = new List<Role>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    if (name != "")
                        command.CommandText = $"SELECT * FROM Role WHERE RoleName LIKE '{name}%'";
                    else
                        command.CommandText = $"SELECT * FROM Role";

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        roles.Add(new Role()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            RoleName = reader[1].ToString(),


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
            return roles;
        }

        public int UpdateRole(Role update)
        {

            int res = 1;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {

                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Role
                                            SET RoleName = @RoleName
                                            WHERE ID = @ID";

                    SQLiteParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.Value = update.ID;
                    parameter.ParameterName = "@ID";
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@RoleName";
                    parameter.Value = update.RoleName;
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
    }
}
