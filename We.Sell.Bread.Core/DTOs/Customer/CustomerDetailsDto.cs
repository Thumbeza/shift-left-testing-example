namespace We.Sell.Bread.Core.DTOs.Customer
{
    public class CustomerDetailsDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string PhysicalAddress { get; set; }

        public CustomerDetailsDto(Guid id, string customerName, string contactNo, string emailAddress, string physicalAddress)
        {
            Id = id;
            CustomerName = customerName;
            ContactNo = contactNo;
            EmailAddress = emailAddress;
            PhysicalAddress = physicalAddress;
        }
    }
}
