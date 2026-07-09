using Microsoft.EntityFrameworkCore;
using Whatsapp.Infrastructure.Entities;
using Whatsapp.Infrastructure.Persistence;

public class ContactRepository
{
  private readonly AppDbContext _context;

  public ContactRepository (AppDbContext dbContext)
  {
    _context = dbContext;
  }

  public async Task<List<Contact>> GetAllAsync ()
  {
    return await _context.Contacts.ToListAsync();
  }

  public async Task<Contact?> GetByIdAsync(Guid id)
  {
    return await _context.Contacts.FindAsync(id);
  }

  public async Task<Contact> AddAsync(Contact contact)
  {
    _context.Contacts.Add(contact);
    await _context.SaveChangesAsync();
    return contact;
  }

  public async Task<Contact> UpdateAsync(Contact contact)
  {
    _context.Contacts.Update(contact);
    await _context.SaveChangesAsync();
    return contact;
  }

  public async Task DeleteAsync(Contact contact)
  {
    _context.Contacts.Remove(contact);
    await _context.SaveChangesAsync();
  }
}