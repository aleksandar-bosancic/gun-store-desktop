using System;
using System.Collections.Generic;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class EmployeeMySql : IEmployee
{
    private const string SELECT_ALL = "select id, username, password, is_admin from employee";

    public List<Employee> getEmployees()
    {
        List<Employee> employees = new List<Employee>();
        MySqlConnection? connection = null;
        MySqlDataReader? reader = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = SELECT_ALL;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(new Employee()
                {
                    Id = reader.GetInt32("id"),
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password"),
                    IsAdmin = reader.GetBoolean("is_admin")
                });
            }
        }
        catch (Exception exception)
        {
            throw new DataAccessException("MySQL data access exception", exception);
        }
        finally
        {
            UtilMySql.closeAll(connection, reader);
        }
        return employees;
    }

    public void saveEmployee(Employee employee)
    {
        throw new System.NotImplementedException();
    }

    public void deleteEmployeeById(int employeeId)
    {
        throw new System.NotImplementedException();
    }
}