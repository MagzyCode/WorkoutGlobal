namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Base model for product suppliers.
    /// </summary>
    public class ProductSupplier
    {
        /// <summary>
        /// Unique identifier of id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Supplier name.
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Supplier residence place.
        /// </summary>
        public string ResidencePlace { get; set; }

        /// <summary>
        /// Collection of supplier products.
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}
