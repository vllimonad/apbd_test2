using Microsoft.EntityFrameworkCore;
using test2.Models;

namespace test2.Data;

public class ApplicationContext: DbContext
{
    protected ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Item>().HasData(new List<Item>()
        {
            new() { Id = 1, Name = "John", Weight = 12},
            new() { Id = 2, Name = "Ann", Weight = 34},
        });
        
        modelBuilder.Entity<Character>().HasData(new List<Character>()
        {
            new() { Id = 1, FirstName = "John", LastName = "Doe", CurrentWeight = 23, MaxWeight = 75},
            new() { Id = 2, FirstName = "Ann", LastName = "Smith", CurrentWeight = 273, MaxWeight = 759},
            new() { Id = 3, FirstName = "Jack", LastName = "Taylor", CurrentWeight = 323, MaxWeight = 375}
        });
        
        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>()
        {
            new() { CharacterId = 1, ItemId = 1, Amount = 2},
        });
        
        modelBuilder.Entity<Title>().HasData(new List<Title>()
        {
            new() { Id = 1, Name = "title1"},
            new() { Id = 2, Name = "title2"},
            new() { Id = 3, Name = "title3"}
        });
        
        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>()
        {
            new() { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Parse("2024-03-01")},
            new() { CharacterId = 2, TitleId = 2, AcquiredAt = DateTime.Parse("2024-03-02")},
        });
        
    }
}