namespace We.Sell.Bread.Infrastructure.Exceptions;

public class DatabaseException : Exception
{
    public DatabaseException(string message) : base(message) { }

    public DatabaseException(string message, Exception innerException) : base(message, innerException) { }
}
