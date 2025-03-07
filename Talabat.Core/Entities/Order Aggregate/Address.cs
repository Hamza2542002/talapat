namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string? fName, string? lName, string? street, string? country, string? depNumber)
        {
            FName = fName;
            LName = lName;
            Street = street;
            Country = country;
            DepNumber = depNumber;
        }

        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Street { get; set; }
        public string? Country { get; set; }
        public string? DepNumber { get; set; }
    }
}
