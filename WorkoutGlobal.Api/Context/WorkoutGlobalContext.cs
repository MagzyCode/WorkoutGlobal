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
            modelBuilder.ApplyConfiguration(new UserAccountsConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
            
            #region UserCredentials relations with User

            modelBuilder.Entity<UserCredentials>()
                .HasOne(userCredentials => userCredentials.User)
                .WithOne(user => user.UserCredentials)
                .HasForeignKey<User>(userCredentials => userCredentials.UserCredentialsId);

            #endregion

            #region Video relations with User, Category

            modelBuilder.Entity<Video>()
                .HasOne(video => video.User)
                .WithMany(user => user.CreatedVideos)
                .HasForeignKey(user => user.UserId);

            modelBuilder.Entity<Video>()
                .HasOne(video => video.Category)
                .WithMany(category => category.Videos)
                .HasForeignKey(video => video.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Course relations with User, Category

            modelBuilder.Entity<Course>()
                .HasOne(course => course.Creator)
                .WithMany(creator => creator.CreatedCourses)
                .HasForeignKey(course => course.CreatorId);

            modelBuilder.Entity<Course>()
                .HasOne(course => course.Category)
                .WithMany(category => category.Courses)
                .HasForeignKey(course => course.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region SportEvent relations with User, ssCategoty

            modelBuilder.Entity<SportEvent>()
                .HasOne(sportEvent => sportEvent.EventCreator)
                .WithMany(trainer => trainer.SportEvents)
                .HasForeignKey(sportEvent => sportEvent.TrainerId);

            modelBuilder.Entity<SportEvent>()
                .HasOne(sportEvent => sportEvent.Category)
                .WithMany(category => category.SportEvents)
                .HasForeignKey(sportEvent => sportEvent.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region CourseVideos relations with Course and Video

            modelBuilder.Entity<CourseVideo>()
                .HasOne(courseVideos => courseVideos.Course)
                .WithMany(course => course.CourseVideos)
                .HasForeignKey(courseVideos => courseVideos.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseVideo>()
                .HasOne(courseVideos => courseVideos.Video)
                .WithMany(course => course.VideoCourses)
                .HasForeignKey(courseVideos => courseVideos.VideoId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region Product relations with ProductSupplier and Stockroom

            modelBuilder.Entity<Product>()
                .HasOne(product => product.Supplier)
                .WithMany(supplier => supplier.Products)
                .HasForeignKey(product => product.ProductSupplierId);

            modelBuilder.Entity<Product>()
                .HasOne(product => product.Stockroom)
                .WithOne(stockRoom => stockRoom.Product)
                .HasForeignKey<Stockroom>(stockroom => stockroom.ProductId);

            #endregion

            #region Order relations with User and Product

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders)
                .HasForeignKey(order => order.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.OrderedProduct)
                .WithMany(product => product.Orders)
                .HasForeignKey(order => order.OrderedProductId);

            #endregion

            #region Post relations with User

            modelBuilder.Entity<Post>()
                .HasOne(post => post.Creator)
                .WithMany(creator => creator.Posts)
                .HasForeignKey(post => post.CreatorId);

            #endregion

            #region Video relations with CommentsBlock

            modelBuilder.Entity<Video>()
                .HasOne(video => video.CommentsBlock)
                .WithOne(block => block.CommentedVideo)
                .HasForeignKey<CommentsBlock>(block => block.CommentedVideoId);

            #endregion

            #region Comment relations with CommentsBlock and User

            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.CommentsBlock)
                .WithMany(block => block.Comments)
                .HasForeignKey(comment => comment.CommentsBlockId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.Commentator)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.CommentatorId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region SubscribeCourse relations with User and Courses

            modelBuilder.Entity<SubscribeCourse>()
                .HasOne(subscribeCourses => subscribeCourses.Subscriber)
                .WithMany(user => user.SubscribeCourses)
                .HasForeignKey(subscribeCourses => subscribeCourses.SubscriberId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SubscribeCourse>()
                .HasOne(subscribeCourses => subscribeCourses.Course)
                .WithMany(user => user.Subscriptions)
                .HasForeignKey(subscribeCourses => subscribeCourses.SubscribeCourseId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region StoreVideos relations with User and Video

            modelBuilder.Entity<StoreVideo>()
                .HasOne(storeVideos => storeVideos.User)
                .WithMany(user => user.SavedVideos)
                .HasForeignKey(storeVideos => storeVideos.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StoreVideo>()
                .HasOne(storeVideos => storeVideos.SavedVideo)
                .WithMany(user => user.StoreVideos)
                .HasForeignKey(storeVideos => storeVideos.SavedVideoId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region SubscribeEvents relations with User, SportEvent

            modelBuilder.Entity<SubscribeEvent>()
                .HasOne(subscribeEvent => subscribeEvent.User)
                .WithMany(user => user.SubscribeEvents)
                .HasForeignKey(subscribeEvent => subscribeEvent.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SubscribeEvent>()
                .HasOne(subscribeEvent => subscribeEvent.Event)
                .WithMany(sportEvent => sportEvent.ParticipatingUsers)
                .HasForeignKey(subscribeEvent => subscribeEvent.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }

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
        public DbSet<CourseVideo> CourseVideos { get; set; }

        /// <summary>
        /// Represents table of product suppliers.
        /// </summary>
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }

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
        public DbSet<CommentsBlock> CommentsBlocks { get; set; }
        public DbSet<SubscribeCourse> SubscribeCourses { get; set; }
        public DbSet<StoreVideo> StoreVideos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubscribeEvent> SubscribeEvents { get; set; }
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
