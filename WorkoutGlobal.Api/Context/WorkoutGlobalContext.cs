using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutGlobal.Api.Models;
using WorkoutGlobal.Api.Models.Configuration;

namespace WorkoutGlobal.Api.Context
{
    /// <summary>
    /// Represents context of WorkoutGlobal project.
    /// </summary>
    public class WorkoutGlobalContext : IdentityDbContext<UserCredentials>
    {
        /// <summary>
        /// Ctor for set context options.
        /// </summary>
        /// <param name="options">Context options.</param>
        public WorkoutGlobalContext(DbContextOptions options)
            : base(options)
        { }

        /// <summary>
        /// Configure the scheme needed for the identity framework.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserCredentialsConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());

            modelBuilder.Entity<UserCredentials>()
                .HasOne(userCredentials => userCredentials.User)
                .WithOne(user => user.UserCredentials)
                .HasForeignKey<User>(userCredentials => userCredentials.UserCredentialsId);

            modelBuilder.Entity<Video>()
                .HasOne(video => video.User)
                .WithMany(user => user.Videos)
                .HasForeignKey(user => user.UserId);

            modelBuilder.Entity<Course>()
                .HasOne(course => course.Creator)
                .WithMany(creator => creator.Courses)
                .HasForeignKey(course => course.CreatorId);

            modelBuilder.Entity<CourseVideos>()
                .HasOne(courseVideos => courseVideos.Course)
                .WithMany(course => course.CourseVideos)
                .HasForeignKey(courseVideos => courseVideos.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseVideos>()
                .HasOne(courseVideos => courseVideos.Video)
                .WithMany(course => course.VideoCourses)
                .HasForeignKey(courseVideos => courseVideos.VideoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne(product => product.Supplier)
                .WithMany(supplier => supplier.Products)
                .HasForeignKey(product => product.ProductSupplierId);

            modelBuilder.Entity<Product>()
                .HasOne(product => product.Stockroom)
                .WithOne(stockRoom => stockRoom.Product)
                .HasForeignKey<Stockroom>(stockroom => stockroom.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders)
                .HasForeignKey(order => order.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.OrderedProduct)
                .WithMany(product => product.Orders)
                .HasForeignKey(order => order.OrderedProductId);

            modelBuilder.Entity<Post>()
                .HasOne(post => post.Creator)
                .WithMany(creator => creator.Posts)
                .HasForeignKey(post => post.CreatorId);
        }

        /// <summary>
        /// Represents table of user credentials.
        /// </summary>
        public DbSet<UserCredentials> UserCredentials { get; set; }

        /// <summary>
        /// Represent table of user account.
        /// </summary>
        public DbSet<User> UserAccounts { get; set; }

        /// <summary>
        /// Repsents table of user videos.
        /// </summary>
        public DbSet<Video> Videos { get; set; }

        /// <summary>
        /// Repsents table of trainer courses.
        /// </summary>
        public DbSet<Course> Courses { get; set; }

        /// <summary>
        /// Represents table of video in courses.
        /// </summary>
        public DbSet<CourseVideos> CourseVideos { get; set; }

        /// <summary>
        /// Represents table of product suppliers.
        /// </summary>
        public DbSet<ProductSuppliers> ProductSuppliers { get; set; }

        /// <summary>
        /// Represents table of product.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Represents table of stockroom.
        /// </summary>
        public DbSet<Stockroom> Stockrooms { get; set; }

        /// <summary>
        /// Represent table of user orders.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Represents table of user posts.
        /// </summary>
        public DbSet<Post> Posts { get; set; }
    }
}
