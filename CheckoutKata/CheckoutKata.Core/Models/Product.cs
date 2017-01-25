namespace CheckoutKata.Core.Models
{
    public class Product
    {
        public string Sku { get; set; }

        public decimal UnitPrice { get; set; }

        public int SpecialQty { get; set; }

        public decimal SpecialPrice { get; set; }
    }
}