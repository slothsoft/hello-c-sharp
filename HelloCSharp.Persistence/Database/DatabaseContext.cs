using HelloCSharp.Api.Database;
using HelloCSharp.Api.Models;
using HelloCSharp.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloCSharp.Persistence.Database;

public class DatabaseContext : DbContext
{

    public DbSet<RelationshipEntity> Relationship { get; init; }
    public DbSet<CityEntity> Cities { get; init; }
    public DbSet<PersonEntity> Persons { get; init; }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CityEntity>(entity =>
        {
            entity.ToTable("city");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
        });

        var piltover = new CityEntity {Id = 1, Name = "Piltover"};
        var zaun = new CityEntity {Id = 2, Name = "Zaun"};
        modelBuilder.Entity<CityEntity>().HasData(piltover, zaun);

        modelBuilder.Entity<PersonEntity>(entity =>
        {
            entity.ToTable("person");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Age).IsRequired();
            entity.Property(e => e.CityId).IsRequired();

            entity.HasOne(e => e.City).WithMany().IsRequired().HasForeignKey(e => e.CityId);
        });

        var vi = new PersonEntity {Id = 1, Name = "Vi", Age = 22, CityId = zaun.Id};
        var powder = new PersonEntity {Id = 2, Name = "Powder", Age = 17, CityId = zaun.Id};
        var caitlyn = new PersonEntity {Id = 3, Name = "Caitlyn", Age = 23, CityId = piltover.Id};
        var silco = new PersonEntity {Id = 4, Name = "Silco", Age = 48, CityId = zaun.Id};
        var ekko = new PersonEntity {Id = 5, Name = "Ekko", Age = 18, CityId = zaun.Id};
        modelBuilder.Entity<PersonEntity>().HasData(vi, powder, caitlyn, silco, ekko);

        modelBuilder.Entity<RelationshipEntity>(entity =>
        {
            entity.ToTable("relationship");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FromId).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.ToId).IsRequired();

            entity.HasOne(e => e.From).WithMany().IsRequired().HasForeignKey(e => e.FromId);
            entity.HasOne(e => e.To).WithMany().IsRequired().HasForeignKey(e => e.ToId);
        });
        modelBuilder.Entity<RelationshipEntity>().HasData(
            new RelationshipEntity {Id = 1, FromId = vi.Id, ToId = powder.Id, Type = RelationshipType.Siblings},
            new RelationshipEntity {Id = 2, FromId = vi.Id, ToId = caitlyn.Id, Type = RelationshipType.Partners},
            new RelationshipEntity {Id = 3, FromId = powder.Id, ToId = caitlyn.Id, Type = RelationshipType.Hates},
            new RelationshipEntity {Id = 4, FromId = silco.Id, ToId = powder.Id, Type = RelationshipType.ParentOf}
        );

        base.OnModelCreating(modelBuilder);
    }
}