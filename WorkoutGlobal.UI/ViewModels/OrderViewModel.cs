namespace WorkoutGlobal.UI.ViewModels
{
    public class OrderViewModel
    {
        /// <summary>
        /// Unique identifier for order.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Foreign key for customer.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Foreign key for ordered product.
        /// </summary>
        public Guid OrderedProductId { get; set; }

        /// <summary>
        /// Product count in order.
        /// </summary>
        public int OrderSize { get; set; }

        /// <summary>
        /// Time of order.
        /// </summary>
        public DateTime OrderTime { get; set; }
    }
}
