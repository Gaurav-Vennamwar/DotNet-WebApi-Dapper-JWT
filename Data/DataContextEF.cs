    // using DotnetAPI.Models;
    // using Microsoft.EntityFrameworkCore;

    // namespace DotnetAPI.Data
    // {
    //     public class DataConntextEF : DbContext
    //     {
    //         private readonly IConfiguration _config;

    //         public DataConntextEF(IConfiguration config)
    //         {
    //             _config = config;
    //         }


    //         public virtual DbSet<User> Users { get; set; }
    //         public virtual DbSet<UserJobInfo> UserJobInfo { get; set; }
    //         public virtual DbSet<UserSalary> UserSalary { get; set; }

    //         //now overriding onconfiguring method to provide connection string
    //         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //         {
    //             if (!optionsBuilder.IsConfigured)
    //             {
    //                 optionsBuilder
    //                 .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
    //                 optionsBuilder => optionsBuilder.EnableRetryOnFailure());
    //             }
    //         }

    //         //telling ef where those models are and map it to our schema
    //         //model banake abh ef ko bolre hai i yeh schema meh map kar de
    //         protected override void OnModelCreating(ModelBuilder modelBuilder)
    //         {
    //             modelBuilder.HasDefaultSchema("TutorialAppSchema");

    //             //we are telling ef ki User hie Users hai
    //             modelBuilder.Entity<User>()//its a metghod jisme hum apne model ko define karte hain
    //                 .ToTable("Users", "TutorialAppSchema")
    //                 .HasKey(u => u.UserId); //primary key

    //             modelBuilder.Entity<UserJobInfo>()//its a metghod jisme hum apne model ko define karte hain
    //             .HasKey(u => u.UserId); //primary key

    //             modelBuilder.Entity<UserSalary>()//its a metghod jisme hum apne model ko define karte hain
    //             .HasKey(u => u.UserId); //primary key
    //         }

    //     }


    // }