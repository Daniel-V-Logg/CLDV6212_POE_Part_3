namespace ST10310998_CLDV6212_POE__Part_1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
    }
}
