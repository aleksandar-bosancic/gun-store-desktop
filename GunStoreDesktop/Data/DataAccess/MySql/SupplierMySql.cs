using System;
using System.Collections.Generic;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class SupplierMySql : ISupplier
{
    private const string SELECT_ALL = "select id, name, address, email_address, phone_number from supplier";
    
    public List<Supplier> getSuppliers()
    {
        List<Supplier> suppliers = new List<Supplier>();
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
                suppliers.Add(new Supplier()
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Address = reader.GetString("address"),
                    Email = reader.GetString("email_address"),
                    PhoneNumber = reader.GetString("phone_number")
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
        return suppliers;
    }
}