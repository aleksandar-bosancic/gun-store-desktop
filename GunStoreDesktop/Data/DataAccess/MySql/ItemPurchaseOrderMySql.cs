using System;
using System.Windows.Media;
using GunStoreDesktop.Data.Model;
using GunStoreDesktop.Exceptions;
using MySql.Data.MySqlClient;

namespace GunStoreDesktop.Data.DataAccess.MySql;

public class ItemPurchaseOrderMySql : IItemPurchaseOrder
{
    private const string INSERT =
        "insert into item_purchase_order(item_id, purchase_order_id, amount, supplier_price) VALUES (@item_id, @purchase_order_id, @amount, @supplier_price)";
    public void save(ItemPurchaseOrder itemPurchaseOrder)
    {
        MySqlConnection? connection = null;
        try
        {
            connection = UtilMySql.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = INSERT;
            command.Parameters.AddWithValue("@item_id", itemPurchaseOrder.ItemId);
            command.Parameters.AddWithValue("@purchase_order_id", itemPurchaseOrder.PurchaseOrderId);
            command.Parameters.AddWithValue("@amount", itemPurchaseOrder.Amount);
            command.Parameters.AddWithValue("@supplier_price", itemPurchaseOrder.SupplierPrice);
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