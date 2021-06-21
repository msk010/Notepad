using Microsoft.EntityFrameworkCore;
using Notepad.Domain.Entities;
using System.Threading.Tasks;

namespace Notepad.Application.Interfaces
{
    public interface INotepadDbContext
    {
        DbSet<Note> Notes { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}