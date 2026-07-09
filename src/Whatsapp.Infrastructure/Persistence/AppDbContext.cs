using Microsoft.EntityFrameworkCore;
using Whatsapp.Infrastructure.Entities;

namespace Whatsapp.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
  {
  }

  public DbSet<Contact> Contacts => Set<Contact>();
}