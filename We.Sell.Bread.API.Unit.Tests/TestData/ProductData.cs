namespace We.Sell.Bread.API.Unit.Tests.TestData
{
    public static class ProductData
    {
        public static Guid ProductIdGuid => new("411fbad3-925e-4044-9269-f962641f9277");
        public static string ProductIdString => new("411fbad3-925e-4044-9269-f962641f9277");
        public static Guid IncorrectProductIdGuid => new("ae9af5db-d9fd-4b05-abf7-44b2f1bef9ce");
        public static string IncorrectProductIdString => new("ae9af5db-d9fd-4b05-abf7-44b2f1bef9ce");
    }
}