namespace We.Sell.Bread.Core.Validations
{
    public static class Validate
    {
        public static void NullOrEmptyArgument(string parameterName)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException(parameterName, $"{parameterName} cannot be empty or null");
            }
        }
    }
}
