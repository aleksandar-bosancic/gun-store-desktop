using System;
using System.Collections.Generic;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class ReceiptItemMySql : IReceiptItem
{
    private const string INSERT =
        "insert into receipt_item(receipt_id, item_id, amount, item_price) value (@receipt_id, @item_id, @amount, @item_price)";

    public List<ReceiptItem> getReceiptItems()
    {
        throw new System.NotImplementedException();
    }

    public void saveReceiptItem(ReceiptItem receiptItem)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = INSERT;
            command.Parameters.AddWithValue("@receipt_id", receiptItem.ReceiptId);
            command.Parameters.AddWithValue("@item_id", receiptItem.ItemId);
            command.Parameters.AddWithValue("@amount", receiptItem.Amount);
            command.Parameters.AddWithValue("@item_price", receiptItem.ItemPrice);
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