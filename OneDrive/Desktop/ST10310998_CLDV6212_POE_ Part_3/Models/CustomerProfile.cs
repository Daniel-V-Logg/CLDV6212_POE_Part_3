using Azure;
using Azure.Data.Tables;

namespace ST10310998_CLDV6212_POE__Part_1.Models
{
    public class CustomerProfile : ITableEntity
    {
        // Azure Table Storage required properties
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        // Custom properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Default constructor
        public CustomerProfile() { }

        // Constructor to easily create a profile
        public CustomerProfile(string partitionKey, string rowKey, string firstName, string lastName, string email, string phoneNumber)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
