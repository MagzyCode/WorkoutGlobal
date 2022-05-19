namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Base model for product.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Unique identifier of product.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        /// Product cost per item.
        /// </summary>
        public double CostPerProduct { get; set; }

        /// <summary>
        /// Foreign key for product supplier.
        /// </summary>
        public Guid ProductSupplierId { get; set; }

        /// <summary>
        /// Foreign model for product supplier.
        /// </summary>
        public ProductSuppliers Supplier { get; set; }

        /// <summary>
        /// Foreign model for stockroom.
        /// </summary>
        public Stockroom Stockroom { get; set; }

        /// <summary>
        /// Collection of orders in which this product is sold.
        /// </summary>
        public ICollection<Order> Orders { get; set; }
    }
}
