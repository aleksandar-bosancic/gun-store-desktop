namespace GunStoreDesktop.Data.DataAccess.MySql;

public class DataFactoryMySql : DataFactory
{
    private EmployeeMySql employeeMySql;

    public override IEmployee Employee => employeeMySql ?? (employeeMySql = new EmployeeMySql());
}