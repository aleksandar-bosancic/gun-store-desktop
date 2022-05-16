using System;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public static class UtilMySql
{
    private static readonly string connectionString = Util.ConfigUtil.getConnectionString();

    public static MySqlConnection getConnection()
    {
        MySqlConnection connection = new(connectionString);
        connection.Open();
        return connection;
    }

    public static void closeConnection(MySqlConnection? connection)
    {
        try
        {
            connection?.Close();
        }
        catch (Exception e)
        {
            //Logging
            Console.WriteLine(e);
        }
    }

    public static void closeReader(MySqlDataReader? reader)
    {
        try
        {
            reader?.Close();
        }
        catch (Exception e)
        {
            //Logging
            Console.WriteLine(e);
        }
    }

    public static void closeAll(MySqlConnection? connection, MySqlDataReader? reader)
    {
        try
        {
            connection?.Close();
            reader?.Close();
        }
        catch (Exception e)
        {
            //Logging
            Console.WriteLine(e);
        }
    }
}