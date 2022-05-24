using System;
using System.Collections.Generic;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class ReceiptMySql : IReceipt
{
    private const string INSERT =
        "insert into receipt(employee_id, buyer_id, date_time, total_price) value (@employee_id, @buyer_id, @date_time, @total_price)";

    public List<Receipt> getReceipts()
    {
        throw new System.NotImplementedException();
    }

    public Receipt saveReceipt(Receipt receipt)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = INSERT;
            command.Parameters.AddWithValue("@employee_id", receipt.Employee?.Id);
            command.Parameters.AddWithValue("@buyer_id", receipt.Buyer.FirearmPermit == null ? null : receipt.Buyer.Id);
            command.Parameters.AddWithValue("@date_time", receipt.DateTime);
            command.Parameters.AddWithValue("@total_price", receipt.TotalPrice);
            command.ExecuteNonQuery();
            receipt.Id = (int)command.LastInsertedId;
        }
        catch (Exception exception)
        {
            throw new DataAccessException("MySQL data access exception", exception);
        }
        finally
        {
            UtilMySql.closeConnection(connection);
        }
        return receipt;
    }
}