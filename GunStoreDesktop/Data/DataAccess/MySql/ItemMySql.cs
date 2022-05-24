﻿using System;
using System.Collections.Generic;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using GunStoreDesktop.Util;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class ItemMySql : IItem
{
    private const string SELECT_ALL = "select id, manufacturer, model, price, in_stock from item";
    private const string UPDATE_IN_STOCK = "update item i set i.in_stock = @in_stock where i.id = @id";
    
    public List<Item> getItems()
    {
        List<Item> items = new List<Item>();
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
                items.Add(new Item()
                {
                    Id = reader.GetInt32("id"),
                    Manufacturer = reader.GetString("manufacturer"),
                    Model = reader.GetString("model"),
                    Price = reader.GetDouble("price"),
                    InStock = reader.GetInt32("in_stock")
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
        return items;
    }

    public void updateInStockById(int id, int inStock)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = UPDATE_IN_STOCK;
            command.Parameters.AddWithValue("@in_stock", inStock);
            command.Parameters.AddWithValue("@id", id);
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