using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Chat_Application.Models
{
    public partial class Chat_ApplicationContext : DbContext
    {
        public Chat_ApplicationContext()
        {
        }

        public Chat_ApplicationContext(DbContextOptions<Chat_ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<Relation> Relations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public virtual DbSet<UserListModel> UserListModels { get; set; } = null!;
        public virtual DbSet<ChatModel> ChatModels { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-3GDVGG9;Database=Chat_Application;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

                entity.Property(e => e.Chat1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Chat");

                entity.Property(e => e.Timestap)
                    .HasColumnType("datetime")
                    .HasColumnName("timestap");

                entity.HasOne(d => d.ReceiveUser)
                    .WithMany(p => p.ChatReceiveUsers)
                    .HasForeignKey(d => d.ReceiveUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chat_User");

                entity.HasOne(d => d.SendUser)
                    .WithMany(p => p.ChatSendUsers)
                    .HasForeignKey(d => d.SendUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chat_User1");
            });

            modelBuilder.Entity<Relation>(entity =>
            {
                entity.ToTable("Relation");

                entity.HasOne(d => d.Friend)
                    .WithMany(p => p.RelationFriends)
                    .HasForeignKey(d => d.FriendId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Relation_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RelationUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Relation_User1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
