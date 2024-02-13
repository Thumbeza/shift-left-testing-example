namespace We.Sell.Bread.Core.DTOs.Customer
{
    public class CustomerCommand
    {
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string PhysicalAddress { get; set; }

        public CustomerCommand(string customerName, string contactNo, string emailAddress, string physicalAddress)
        {
            CustomerName = customerName;
            ContactNo = contactNo;
            EmailAddress = emailAddress;
            PhysicalAddress = physicalAddress;
        }
    }
}
