using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Task_4.Models;

namespace Task_4.Models
{
    public class MesContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public MesContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine).UseLazyLoadingProxies().UseNpgsql("Host = localhost; Username = postgres; Password = example; Database = ChatDb;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("messages_pkey");
                entity.ToTable("Messages");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Text).HasColumnName("text");
                entity.Property(e => e.FromUserId).HasColumnName("from_user_id");
                entity.Property(e => e.ToUserId).HasColumnName("to_user_id");

                entity.HasOne(e => e.FromUser).WithMany(p => p.FromMessages).
                HasForeignKey(e => e.FromUserId).HasConstraintName("messages_from_user_id_fkey");

                entity.HasOne(e => e.ToUser).WithMany(p => p.ToMessages).
                HasForeignKey(e => e.ToUserId).HasConstraintName("messages_to_user_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");
                entity.ToTable("Users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasMaxLength(255).HasColumnName("name");
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}

