using System.Collections.Generic;
using GunStoreDesktop.Data.Model;

namespace GunStoreDesktop.Data.DataAccess;

public interface IEmployee
{
    List<Employee> getEmployees();
    void saveEmployee(Employee employee);
    void deleteEmployeeById(int employeeId);

    void updateEmployeeSettingsById(Employee employee);
}