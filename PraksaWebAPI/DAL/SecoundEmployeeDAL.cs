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
    public class SecoundEmployeeDAL : IEmployeeDAL
    {
        private readonly IConfiguration configuration;
        private string ConnectionString;

        public SecoundEmployeeDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = ProtectionHelper.Singletion.GetSectionValue("ConnectionStrings:Connection2");
        }

        public int AddEmployee(Employee newEmployee)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Employee VALUES(NULL,@Name, @BirthDate, @Salary, @roleID, @DepartmentID, @FinishedTasks, @Email, @Password)";

                    SQLiteParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Name";
                    parameter.Value = newEmployee.Name;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@BirthDate";
                    parameter.Value = newEmployee.BirthDate.ToString();
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Double;
                    parameter.ParameterName = "@Salary";
                    parameter.Value = newEmployee.Salary;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@roleID";
                    parameter.Value = newEmployee.RoleID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@DepartmentID";
                    parameter.Value = newEmployee.DepartmentID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@FinishedTasks";
                    parameter.Value = newEmployee.FinishedTasks;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Email";
                    parameter.Value = newEmployee.Email;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Password";
                    parameter.Value = newEmployee.Password;
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

        public EmployeeInfo BestSalary()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                EmployeeInfo max = new EmployeeInfo();
                try
                {

                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = @"SELECT  *
                                            FROM Employee
                                            WHERE Salary = (select Max(Salary)
                                            from Employee
                                            )
                                            LIMIT 1";

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        max = new EmployeeInfo()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            BirthDate = DateTime.Parse(reader[2].ToString()),
                            Salary = float.Parse(reader[3].ToString()),
                            RoleID = Int32.Parse(reader[4].ToString()),
                            DepartmentID = Int32.Parse(reader[5].ToString()),
                            FinishedTasks = Int32.Parse(reader[6].ToString()),
                            Email = reader[7].ToString(),

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

        public int DeleteEmployee(long id)
        {
            int res = 1;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = $"DELETE FROM Employee where ID = {id}";
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



        public List<EmployeeInfo> GetEmployeesForDep(long depID)
        {
            List<EmployeeInfo> employees = new List<EmployeeInfo>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM Employee where DepartmentID = {depID}";

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeInfo()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            BirthDate = DateTime.Parse(reader[2].ToString()),
                            Salary = float.Parse(reader[3].ToString()),
                            RoleID = Int32.Parse(reader[4].ToString()),
                            DepartmentID = Int32.Parse(reader[5].ToString()),
                            FinishedTasks = Int32.Parse(reader[6].ToString()),
                            Email = reader[7].ToString()

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
            return employees;
        }



        public List<EmployeeInfo> GetEmployeesForRole(long roleID)
        {
            List<EmployeeInfo> employees = new List<EmployeeInfo>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM Employee where RoleID = {roleID}";

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeInfo()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            BirthDate = DateTime.Parse(reader[2].ToString()),
                            Salary = float.Parse(reader[3].ToString()),
                            RoleID = Int32.Parse(reader[4].ToString()),
                            DepartmentID = Int32.Parse(reader[5].ToString()),
                            FinishedTasks = Int32.Parse(reader[6].ToString()),
                            Email = reader[7].ToString()


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
            return employees;
        }



        public List<EmployeeInfo> GetEmployees(string name)
        {
            List<EmployeeInfo> employees = new List<EmployeeInfo>();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    if (name != "")
                        command.CommandText = $"SELECT * FROM Employee WHERE Name LIKE '{name}%'";
                    else
                        command.CommandText = $"SELECT * FROM Employee";


                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeInfo()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            BirthDate = DateTime.Parse(reader[2].ToString()),
                            Salary = float.Parse(reader[3].ToString()),
                            RoleID = Int32.Parse(reader[4].ToString()),
                            DepartmentID = Int32.Parse(reader[5].ToString()),
                            FinishedTasks = Int32.Parse(reader[6].ToString()),
                            Email = reader[7].ToString()

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
            return employees;
        }

        public Employee GetEmployeeById(long id)
        {
            Employee employee = new Employee();

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM Employee where ID = {id}";

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        employee = new Employee()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            BirthDate = DateTime.Parse(reader[2].ToString()),
                            Salary = float.Parse(reader[3].ToString()),
                            RoleID = Int32.Parse(reader[4].ToString()),
                            DepartmentID = Int32.Parse(reader[5].ToString()),
                            FinishedTasks = Int32.Parse(reader[6].ToString()),
                            Email = reader[7].ToString(),
                            Password = reader[8].ToString()

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

            return employee;
        }

        public int UpdateEmploye(Employee update)
        {
            int res = 1;
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {

                    connection.Open();
                   
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Employee
                                        SET Name = @Name, BirthDate = @BirthDate, Salary = @Salary, RoleID = @RoleID, DepartmentID = @DepartmentID, FinishedTasks = @FinishedTasks, email=@email, password=@password
                                        WHERE ID = @ID
                                        ";

                    SQLiteParameter parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.Value = update.ID;
                    parameter.ParameterName = "@ID";
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@Name";
                    parameter.Value = update.Name;
                    command.Parameters.Add(parameter);


                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@BirthDate";
                    parameter.Value = update.BirthDate.ToString();
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Double;
                    parameter.ParameterName = "@Salary";
                    parameter.Value = update.Salary;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@roleID";
                    parameter.Value = update.RoleID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@DepartmentID";
                    parameter.Value = update.DepartmentID;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.Int32;
                    parameter.ParameterName = "@FinishedTasks";
                    parameter.Value = update.FinishedTasks;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@email";
                    parameter.Value = update.Email;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.DbType = System.Data.DbType.String;
                    parameter.ParameterName = "@password";
                    parameter.Value = update.Password;
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

        public Employee Login(LoginModel loginModel)
        {
            Employee employee = null;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM Employee where email = '{loginModel.Email}' AND password = '{loginModel.Password}'";

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        employee = new Employee()
                        {
                            ID = Int32.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            BirthDate = DateTime.Parse(reader[2].ToString()),
                            Salary = float.Parse(reader[3].ToString()),
                            RoleID = Int32.Parse(reader[4].ToString()),
                            DepartmentID = Int32.Parse(reader[5].ToString()),
                            FinishedTasks = Int32.Parse(reader[6].ToString()),
                            Email = reader[1].ToString(),
                            Password = reader[1].ToString()

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

            return employee;
        }
    }
}
