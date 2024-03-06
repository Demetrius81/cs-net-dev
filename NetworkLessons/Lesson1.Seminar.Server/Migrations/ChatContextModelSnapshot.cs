﻿// <auto-generated />
using Lesson1.Seminar.Server.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lesson1.Seminar.Server.Migrations
{
    [DbContext(typeof(ChatContext))]
    partial class ChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("Lesson1.Seminar.Server.Models.Message", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("AuthorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConsumerId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRecived")
                        .HasColumnType("INTEGER");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ConsumerId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Lesson1.Seminar.Server.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Lesson1.Seminar.Server.Models.Message", b =>
                {
                    b.HasOne("Lesson1.Seminar.Server.Models.User", "Author")
                        .WithMany("SendedMessage")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("messages_from_user_id_fk");

                    b.HasOne("Lesson1.Seminar.Server.Models.User", "Consumer")
                        .WithMany("RecivedMessage")
                        .HasForeignKey("ConsumerId")
                        .HasConstraintName("messages_to_consumer_id_fk");

                    b.Navigation("Author");

                    b.Navigation("Consumer");
                });

            modelBuilder.Entity("Lesson1.Seminar.Server.Models.User", b =>
                {
                    b.Navigation("RecivedMessage");

                    b.Navigation("SendedMessage");
                });
#pragma warning restore 612, 618
        }
    }
}
