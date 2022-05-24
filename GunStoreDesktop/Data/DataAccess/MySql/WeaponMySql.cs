using System;
using System.Collections.Generic;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class WeaponMySql : IWeapon
{
    private const string SELECT_ALL = "select item_id, caliber, magazine_capacity, is_firearm from weapon";
    
    public List<Weapon> getWeapons()
    {
        List<Weapon> weapons = new List<Weapon>();
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
                weapons.Add(new Weapon()
                {
                    ItemId = reader.GetInt32("item_id"),
                    Caliber = reader.GetString("caliber"),
                    MagazineCapacity = reader.GetInt32("magazine_capacity"),
                    IsFirearm = reader.GetBoolean("is_firearm")
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
        return weapons;
    }
}