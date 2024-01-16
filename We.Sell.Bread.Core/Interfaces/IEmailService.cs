namespace We.Sell.Bread.Core.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string emailAddress, string subject, object body);
    }
}
