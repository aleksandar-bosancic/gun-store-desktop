using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using GunStoreDesktop.Util;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class EmployeeMySql : IEmployee
{
    private const string SELECT_ALL = "select id, username, password, is_admin, settings from employee";

    private const string INSERT =
        "insert into employee(username, password, is_admin, settings) value (@username, @password, @is_admin, @settings)";
    private const string DELETE_BY_ID = "delete from employee e where e.id = @id";
    private const string UPDATE_SETTINGS_BY_ID = "update employee e set e.settings = @settings where e.id = @id";

    private const string UPDATE_BY_ID =
        "update employee e set e.username = @username, e.password = @password, e.is_admin = @is_admin where e.id = @id";

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
                    IsAdmin = reader.GetBoolean("is_admin"),
                    Settings = SettingsUtil.StringToEmployeeSettings(reader.GetString("settings"))
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

    public Employee saveEmployee(Employee employee)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = INSERT;
            command.Parameters.AddWithValue("@username", employee.Username);
            command.Parameters.AddWithValue("@password", employee.Password);
            command.Parameters.AddWithValue("@is_admin", employee.IsAdmin);
            command.Parameters.AddWithValue("@settings", SettingsUtil.EmployeeSettingsToString(employee.Settings));
            command.ExecuteNonQuery();
            employee.Id = (int)command.LastInsertedId;
        }
        catch (Exception exception)
        {
            throw new DataAccessException("MySQL data access exception", exception);
        }
        finally
        {
            UtilMySql.closeConnection(connection);
        }
        return employee;
    }

    public void deleteEmployeeById(int employeeId)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = DELETE_BY_ID;
            command.Parameters.AddWithValue("@id", employeeId);
            command.ExecuteNonQuery();
        }
        catch (Exception exception)
        {
            throw new DataAccessException("MySQL data access exception", exception);
        }
        finally
        {
            UtilMySql.closeConnection(connection);
        }
    }

    public void updateEmployeeById(Employee employee)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = UPDATE_BY_ID;
            command.Parameters.AddWithValue("@username", employee.Username);
            command.Parameters.AddWithValue("@password", employee.Password);
            command.Parameters.AddWithValue("@is_admin", employee.IsAdmin);
            command.Parameters.AddWithValue("@id", employee.Id);
            command.ExecuteNonQuery();
        }
        catch (Exception exception)
        {
            throw new DataAccessException("MySQL data access exception", exception);
        }
        finally
        {
            UtilMySql.closeConnection(connection);
        }
    }

    public void updateEmployeeSettingsById(Employee employee)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = UPDATE_SETTINGS_BY_ID;
            command.Parameters.AddWithValue("@settings", SettingsUtil.EmployeeSettingsToString(employee.Settings));
            command.Parameters.AddWithValue("@id", employee.Id);
            command.ExecuteNonQuery();
        }
        catch (Exception exception)
        {
            throw new DataAccessException("MySQL data access exception", exception);
        }
        finally
        {
            UtilMySql.closeConnection(connection);
        }
    }
}