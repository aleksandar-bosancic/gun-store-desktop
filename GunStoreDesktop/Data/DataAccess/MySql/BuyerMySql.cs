using System;
using System.Collections.Generic;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class BuyerMySql : IBuyer
{
    private const string SELECT_ALL = "select id, name, last_name, date_of_birth, firearm_permit_uid from buyer";
    private const string INSERT =
        "insert into buyer(id, name, last_name, date_of_birth, firearm_permit_uid) value (@id, @name, @last_name, @date_of_birth, @firearm_permit_uid)";

    public List<Buyer> getBuyers()
    {
        List<Buyer> buyers = new List<Buyer>();
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
                buyers.Add(new Buyer()
                {
                    Id = reader.GetString("id"),
                    Name = reader.GetString("name"),
                    LastName = reader.GetString("last_name"),
                    DateOfBirth = DateOnly.FromDateTime(reader.GetDateTime("date_of_birth")),
                    FirearmPermit = DataFactory.GetMySqlDataFactory().FirearmPermit.findByUid(SafeGetString(reader, 4))
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
        return buyers;
    }

    private static string SafeGetString(MySqlDataReader reader, int colIndex)
    {
        if (!reader.IsDBNull(colIndex))
        {
            return reader.GetString(colIndex);
        }
        else
        {
            return string.Empty;
        }
    }

    public void saveBuyer(Buyer buyer)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = INSERT;
            command.Parameters.AddWithValue("@id", buyer.Id);
            command.Parameters.AddWithValue("@name", buyer.Name);
            command.Parameters.AddWithValue("@last_name", buyer.LastName);
            command.Parameters.AddWithValue("@date_of_birth", buyer.DateOfBirth.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@firearm_permit_uid", buyer.FirearmPermit?.Uid);
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