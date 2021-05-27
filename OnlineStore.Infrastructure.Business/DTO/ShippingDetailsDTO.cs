
namespace OnlineStore.Infrastructure.Business.DTO
{
    public class ShippingDetailsDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
    }
    public enum Country
    {
        US,
        CA,
        FR,
        DE,
        AU,
        GB
    }
}
