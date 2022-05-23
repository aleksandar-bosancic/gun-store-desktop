using System;
using System.Collections.Generic;

namespace GunStoreDesktop.Data.Model;

public class Employee
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public EmployeeSettings Settings { get; set; }


    public Employee()
    {
        Username = "";
        Password = "";
        IsAdmin = false;
        Settings = new EmployeeSettings();
    }

    public override bool Equals(object? obj)
    {
        return obj is Employee employee && 
               Id == employee.Id;
    }

    public override int GetHashCode()
    {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return HashCode.Combine(Id.GetHashCode(), Username?.GetHashCode());
    }
}