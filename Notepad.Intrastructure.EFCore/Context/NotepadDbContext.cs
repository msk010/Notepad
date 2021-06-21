using Microsoft.EntityFrameworkCore;
using Notepad.Application.Interfaces;
using Notepad.Domain.Entities;
using Notepad.Intrastructure.EFCore.Configurations;
using System.Threading.Tasks;

namespace Notepad.Intrastructure.EFCore.Context
{
    public class NotepadDbContext : DbContext, INotepadDbContext
    {
        public NotepadDbContext(DbContextOptions<NotepadDbContext> options) : base(options) { }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new NoteTagConfiguration());
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
        }
    }
}
