using System;
using System.Collections.Generic;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class EquipmentMySql : IEquipment
{
    private const string INSERT =
        "insert into equipment(item_id, type, colour, name) VALUES (@item_id, @type, @colour, @name)";
    public List<Equipment> getEquipment()
    {
        throw new System.NotImplementedException();
    }

    public void saveEquipment(Equipment equipment)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = INSERT;
            command.Parameters.AddWithValue("@item_id", equipment.Id);
            command.Parameters.AddWithValue("@type", equipment.Type);
            command.Parameters.AddWithValue("@colour", equipment.Colour);
            command.Parameters.AddWithValue("@name", equipment.Name);
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