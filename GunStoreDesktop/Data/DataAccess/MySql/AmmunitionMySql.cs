using System;
using System.Collections.Generic;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class AmmunitionMySql : IAmmunition
{
    private const string INSERT =
        "insert into ammunition(item_id, munition_type, package_size, caliber) VALUES (@item_id, @munition_type, @package_size, @caliber)";
    public List<Ammunition> getAmmunition()
    {
        throw new System.NotImplementedException();
    }

    public void saveAmmunition(Ammunition ammunition)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = INSERT;
            command.Parameters.AddWithValue("@item_id", ammunition.Id);
            command.Parameters.AddWithValue("@caliber", ammunition.Caliber);
            command.Parameters.AddWithValue("@package_size", ammunition.PackageSize);
            command.Parameters.AddWithValue("@munition_type", ammunition.MunitionType);
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

    public void deleteAmmunitionById()
    {
        throw new System.NotImplementedException();
    }
}