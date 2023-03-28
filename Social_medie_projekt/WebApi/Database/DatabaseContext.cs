namespace WebApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Login> Login { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<PostLikes> PostLikes { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<PostTag> PostTag { get; set; }

        public override int SaveChanges()
        {
            HandleDelete();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            HandleDelete();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void HandleDelete()
        {
            foreach (var entity in ChangeTracker.Entries<ISoftDelete>()
                .Where(x => x.State == EntityState.Deleted))
            {
                entity.State = EntityState.Modified;
                entity.CurrentValues["IsDeleted"] = true;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Post>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<PostLikes>().HasQueryFilter(x => !x.Post.IsDeleted);

            modelBuilder.Entity<Like>().HasQueryFilter(x => !x.Post.IsDeleted);

            modelBuilder.Entity<PostTag>().HasQueryFilter(x => !x.Post.IsDeleted);

            modelBuilder.Entity<Follow>().HasQueryFilter(x => !x.User.IsDeleted);

            modelBuilder.Entity<Tag>(e =>
            {
                e.HasIndex(t => t.Name).IsUnique();
            });

            modelBuilder.Entity<Like>().HasIndex(x => new { x.UserId })
                .IsUnique(false);

            modelBuilder.Entity<Like>()
                .HasOne(x => x.User)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Login>().HasData(
                new Login
                {
                    LoginId = 1,
                    Email = "Test1@mail.dk",
                    Password = "password",
                    Role = 0
                },
                new Login
                {
                    LoginId = 2,
                    Email = "Test2@mail.dk",
                    Password = "password",
                    Role = (Role)1
                },
                new Login
                {
                    LoginId = 3,
                    Email = "Test3@mail.dk",
                    Password = "password",
                    Role = (Role)1
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    LoginId = 1,
                    UserName = "tester 1",
                    Created = DateTime.Now
                },
                new User
                {
                    UserId = 2,
                    LoginId = 2,
                    UserName = "222test222",
                    Created = DateTime.Now
                },
                new User
                {
                    UserId = 3,
                    LoginId = 3,
                    UserName = "user 3",
                    Created = DateTime.Now
                });

            modelBuilder.Entity<Follow>().HasData(
                new Follow
                {
                    UserId = 1,
                    FollowingId = 2
                },
                new Follow
                {
                    UserId = 2,
                    FollowingId = 1
                }
                );

            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    PostId = 1,
                    Title = "testestestest",
                    Desc = "tadnawdnada",
                    Date = DateTime.Now,
                    UserId = 1,
                },
                new Post
                {
                    PostId = 2,
                    Title = "Test!",
                    Desc = "Woooooo!",
                    Date = DateTime.Now,
                    UserId = 2,
                });

            modelBuilder.Entity<PostLikes>().HasData(
                new PostLikes
                {
                    PostId = 1,
                    Likes = 2
                },
                new PostLikes
                {
                    PostId = 2,
                    Likes = 2
                });

            modelBuilder.Entity<Like>().HasData(
                new Like
                {
                    UserId = 1,
                    PostId = 1,
                },
                new Like
                {
                    UserId = 1,
                    PostId = 2,
                },
                new Like
                {
                    UserId = 2,
                    PostId = 1,
                },
                new Like
                {
                    UserId = 2,
                    PostId = 2,
                });

            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Name = "sax",
                    TagId = 1,
                },
                new Tag
                {
                    Name = "fax",
                    TagId = 2,
                },
                new Tag
                {
                    Name = "howdy",
                    TagId = 3,
                });

            modelBuilder.Entity<PostTag>().HasData(
                new PostTag
                {
                    PostId = 1,
                    TagId = 1,
                },
                new PostTag
                {
                    PostId = 1,
                    TagId = 2,
                },
                new PostTag
                {
                    PostId = 1,
                    TagId = 3,
                },
                new PostTag
                {
                    PostId = 2,
                    TagId = 3,
                });
        }
    }
}