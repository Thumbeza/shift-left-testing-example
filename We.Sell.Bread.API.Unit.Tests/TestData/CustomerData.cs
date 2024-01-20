namespace We.Sell.Bread.API.Unit.Tests.TestData
{
    public static class CustomerData
    {
        public static Guid CustomerIdGuid => new("0ff5b45c-bc6e-49bc-a200-1b0ca930386d");
        public static string CustomerIdString => "0ff5b45c-bc6e-49bc-a200-1b0ca930386d";
        public static Guid IncorrectCustomerIdGuid => new("0ff6b45c-bc6e-49bc-a200-1b0ca930386d");
    }
}
