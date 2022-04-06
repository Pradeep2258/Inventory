namespace Inventory.Model
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int MerchantId { get; set; }
        public string InsertedBy { get; set; }

    }

    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int MerchantId { get; set; }
        public string InsertedBy { get; set; }
    }

    public class ProductFilter
    {
        public int CategoryType { get; set; }
        public string ProductName { get; set; }

    }
}
