using System;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class PurchaseOrderMySql : IPurchaseOrder
{
    private const string INSERT =
        "insert into purchase_order(supplier_id, date_time) VALUES (@supplier_id, @date_time)";
    public PurchaseOrder save(PurchaseOrder purchaseOrder)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = INSERT;
            command.Parameters.AddWithValue("@supplier_id", purchaseOrder.SupplierId);
            command.Parameters.AddWithValue("@date_time", purchaseOrder.DateTime);
            command.ExecuteNonQuery();
            purchaseOrder.Id = (int)command.LastInsertedId;
        }
        catch (Exception exception)
        {
            throw new DataAccessException("MySQL data access exception", exception);
        }
        finally
        {
            UtilMySql.closeConnection(connection);
        }

        return purchaseOrder;
    }
}