using System.Reflection;
using Icon3DPack.API.DataAccess.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Icon3DPack.API.Core.Common;
using Icon3DPack.API.Core.Entities;
using Icon3DPack.API.Shared.Services;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace Icon3DPack.API.DataAccess.Persistence;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    private readonly IClaimService _claimService;

    public DatabaseContext(DbContextOptions options, IClaimService claimService) : base(options)
    {
        _claimService = claimService;
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    public DbSet<TodoList> TodoLists { get; set; }
    public DbSet<FileExtension>  FileExtensions { get; set; }
    public DbSet<FileEntity> FileEntities { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }
    public DbSet<CategoryTag> CategoryTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity => { entity.ToTable(name: "Users"); });
        modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
        modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
        modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
        modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
        modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });

        modelBuilder.Entity<ProductTag>()
            .HasKey(pt => new { pt.ProductId, pt.TagId });

        modelBuilder.Entity<ProductTag>()
            .HasOne(pt => pt.Product)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(pt => pt.ProductId);

        modelBuilder.Entity<ProductTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.ProductTags)
            .HasForeignKey(pt => pt.TagId);

        modelBuilder.Entity<CategoryTag>()
           .HasKey(pt => new { pt.CategoryId, pt.TagId });

        modelBuilder.Entity<CategoryTag>()
            .HasOne(pt => pt.Category)
            .WithMany(p => p.CategoryTags)
            .HasForeignKey(pt => pt.CategoryId);

        modelBuilder.Entity<CategoryTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.CategoryTags)
            .HasForeignKey(pt => pt.TagId);

        //builder.Entity<ApplicationUser>(entity =>
        //{
        //    entity.ToTable("AppUser");
        //    entity.Property(e => e.Id).HasColumnName("UserId");

        //});

        //builder.Entity<IdentityRole>().ToTable("AppRoles");
        //builder.Entity<IdentityUserRole<Guid>>(entity =>
        //{
        //    entity.ToTable("UserRoles");
        //    entity.HasKey(key => new { key.UserId, key.RoleId });
        //});

        //builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
        //builder.Entity<IdentityUserLogin<Guid>>(entity =>
        //{
        //    entity.ToTable("AppUserLogins");
        //    entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });
        //});
        //builder.Entity<IdentityUserToken<Guid>>(entity =>
        //{
        //    entity.ToTable("UserTokens");
        //    entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

        //});
        //builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");


        //builder.Entity<ApplicationUser>(b =>
        //{
        //    // Each User can have many UserClaims
        //    b.HasMany(e => e.Claims)
        //        .WithOne()
        //        .HasForeignKey(uc => uc.UserId)
        //        .IsRequired();

        //    // Each User can have many UserLogins
        //    b.HasMany(e => e.Logins)
        //        .WithOne()
        //        .HasForeignKey(ul => ul.UserId)
        //        .IsRequired();

        //    // Each User can have many UserTokens
        //    b.HasMany(e => e.Tokens)
        //        .WithOne()
        //        .HasForeignKey(ut => ut.UserId)
        //        .IsRequired();

        //    // Each User can have many entries in the UserRole join table
        //    b.HasMany(e => e.UserRoles)
        //        .WithOne(e => e.User)
        //        .HasForeignKey(ur => ur.UserId)
        //        .IsRequired();
        //});

        //builder.Entity<IdentityRole>(b =>
        //{
        //    // Each Role can have many entries in the UserRole join table
        //    b.HasMany(e => e.UserRoles)
        //        .WithOne(e => e.Role)
        //        .HasForeignKey(ur => ur.RoleId)
        //        .IsRequired();
        //});
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _claimService.GetUserId();
                    entry.Entity.CreatedTime = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = _claimService.GetUserId();
                    entry.Entity.ModifiedTime = DateTime.Now;
                    break;
            }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
