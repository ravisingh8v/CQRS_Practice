using ET_Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ET_Infrastructure;

public class AppDbContext : DbContext
{
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
public DbSet<ExpenseModel> Expenses {get; set;}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<ExpenseModel>().Property(e => e.Amount).HasPrecision(18,2);
}
}
