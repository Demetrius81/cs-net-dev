using Lesson1.Seminar.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1.Seminar.Server.Context;
internal class ChatContext : DbContext
{
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public ChatContext():base()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=Chatapp.db").LogTo(Console.WriteLine);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(item => item.Id);
            entity.ToTable("users");
            entity.Property(prop => prop.Username)
                .HasColumnName("name")
                .HasMaxLength(50);

            entity.HasMany(user => user.RecivedMessage)
                .WithOne(msg => msg.Consumer)
                .HasForeignKey(msg => msg.ConsumerId)
                .HasConstraintName("messages_to_consumer_id_fk");
        });
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(msg => msg.Id);
            entity.Property(msg => msg.content)
                .HasColumnName("text");

            entity.HasOne(msg => msg.Author)
                .WithMany(user => user.SendedMessage)
                .HasForeignKey(msg => msg.AuthorId)
                .HasConstraintName("messages_from_user_id_fk");
        });
        base.OnModelCreating(modelBuilder);
    }
}
