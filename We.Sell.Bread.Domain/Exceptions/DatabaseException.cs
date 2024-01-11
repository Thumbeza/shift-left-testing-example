namespace We.Sell.Bread.Domain.Exceptions;

public class DatabaseException : Exception
{
    public DatabaseException(string message) : base(message) { }

    public DatabaseException(string message, Exception innerException) : base(message, innerException) { }

    //public DatabaseException(string parameterName, string message, Exception innerException) : base(parameterName, message, innerException) { }
}
