namespace GunStoreDesktop.Exceptions;
using System;

public class DataAccessException : Exception
{
    public DataAccessException(string message, Exception inner) : base(message, inner) { }
}