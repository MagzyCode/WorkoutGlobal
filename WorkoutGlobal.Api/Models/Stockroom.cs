namespace WorkoutGlobal.Api.Models
{
    /// <summary>
    /// Base model for products store room.
    /// </summary>
    public class Stockroom
    {
        /// <summary>
        /// Unique identifier for product if stockroom.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Quantity of remaining goods in stock.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Foreign key for product in stock.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Foreign model for product in stock.
        /// </summary>
        public Product Product { get; set; }

    }
}
