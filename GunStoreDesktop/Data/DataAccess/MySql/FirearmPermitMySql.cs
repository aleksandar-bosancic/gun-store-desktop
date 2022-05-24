using System;
using System.Collections.Generic;
using System.Linq;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class FirearmPermitMySql : IFirearmPermit
{
    private const string SELECT_ALL = "select uid, issue_date, expiration_date from firearm_permit";
    
    public List<FirearmPermit> getFirearmPermits()
    {
        List<FirearmPermit> permits = new List<FirearmPermit>();
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
                permits.Add(new FirearmPermit()
                {
                    Uid = reader.GetString("uid"),
                    IssueDate = DateOnly.FromDateTime(reader.GetDateTime("issue_date")),
                    ExpirationDate = DateOnly.FromDateTime(reader.GetDateTime("expiration_date"))
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
        return permits;
    }

    public FirearmPermit? findByUid(string uid)
    {
        List<FirearmPermit> permits = getFirearmPermits();
        return permits.Find(permit => permit.Uid.Equals(uid));
    }
}