using Microsoft.EntityFrameworkCore;
using To_Do_List_Web_Api.Models; 
using To_Do_List_Web_Api.Models.To_Do_List_Web_Api.Models;

namespace To_Do_List_Web_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
