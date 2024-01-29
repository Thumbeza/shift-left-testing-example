namespace We.Sell.Bread.API.Unit.Tests.TestData
{
    public static class CustomerData
    {
        public static Guid CustomerIdGuid => new("0ff5b45c-bc6e-49bc-a200-1b0ca930386d");
        public static string CustomerIdString => "0ff5b45c-bc6e-49bc-a200-1b0ca930386d";
        public static Guid IncorrectCustomerIdGuid => new("0ff6b45c-bc6e-49bc-a200-1b0ca930386d");
        public static string IncorrectCustomerIdString => new("0ff6b45c-bc6e-49bc-a200-1b0ca930386d");
        public static Guid DeleteCustomerIdGuid => new("f4057bd7-e40d-4cf9-b3ad-e5b65146e37c");
        public static string DeleteCustomerIdString => new("69a63e4e-c48c-43e1-8ccc-c1b336230e57");
    }
}
