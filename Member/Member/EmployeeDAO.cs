using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Member
{
        public class EmployeeDAO
        {
            private readonly string connectionString;

            //DataAccess Object
            public EmployeeDAO(string connectionString)
            {
                this.connectionString = connectionString;
            }

            
      
            public void Create(Employee employee)
            {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Check if worker ID already exists
                using (var command = new SQLiteCommand("SELECT COUNT(*) from User_Table where workers_id = @WorkersID", connection))
                {
                    command.Parameters.AddWithValue("@WorkersID", employee.WorkersID);
                    var result = (long)command.ExecuteScalar();
                    if (result > 0)
                    {
                        MessageBox.Show("WorkersID already exists");
                        return;
                    }
                }

                // Insert new record
                using (var command = new SQLiteCommand("INSERT INTO User_Table (workers_id, FirstName, MiddleName, LastName, Birthday) VALUES (@WorkersID, @FirstName, @MiddleName, @LastName, @Birthday)", connection))
                {
                    command.Parameters.AddWithValue("@WorkersID", employee.WorkersID);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@Birthday", employee.Birthday);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Successfully saved!");
                }
            }
        }


      



        public Employee Read(int employeeId)
            {
                const string sql = "SELECT EmployeeId, FirstName, MiddleName, LastName, Birthday FROM User_Table WHERE EmployeeId = @EmployeeId";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Employee
                                {
                                    EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    MiddleName = reader.GetString(reader.GetOrdinal("MiddleName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"))
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }

            public void Update(Employee employee)
            {
                const string sql = "UPDATE User_Table SET FirstName = @FirstName, MiddleName = @MiddleName,  LastName = @LastName, Birthday = @Birthday WHERE EmployeeId = @EmployeeId";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        command.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                        command.Parameters.AddWithValue("@LastName", employee.LastName);
                        command.Parameters.AddWithValue("@Birthday", employee.Birthday);
                        command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                     
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }

            public void Delete(int employeeId)
            {
                const string sql = "DELETE FROM User_Table WHERE EmployeeId = @EmployeeId";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }

            public List<Employee> GetAll()
            {
                var employees = new List<Employee>();

                const string sql = "SELECT EmployeeId, FirstName, MiddleName, LastName, Birthday FROM User_Table";

                using (var connection = new SQLiteConnection(connectionString))
                {
                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                employees.Add(new Employee
                                {
                                    EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    MiddleName = reader.GetString(reader.GetOrdinal("MiddleName")),
                                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                    Birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"))
                                });
                            }
                        }
                    }
                }
                return employees;
            }
        }
    }
