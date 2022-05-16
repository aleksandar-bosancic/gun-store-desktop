using Org.BouncyCastle.Asn1.X509;

namespace GunStoreDesktop.Data.DataAccess;

public abstract class DataFactory
{
    public abstract IEmployee Employee { get; }

    public static DataFactory GetMySqlDataFactory()
    {
        return new MySql.DataFactoryMySql();
    }
}