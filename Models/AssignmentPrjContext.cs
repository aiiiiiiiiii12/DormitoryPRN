using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectPrn211.Models
{
    public partial class AssignmentPrjContext : DbContext
    {
        public AssignmentPrjContext()
        {
        }

        public AssignmentPrjContext(DbContextOptions<AssignmentPrjContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Roomtype> Roomtypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=AssignmentPrj;User ID=sa;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__admin__F3DBC5733B88E678");

                entity.ToTable("admin");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Unknown')");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("booking");

                entity.HasIndex(e => e.InDate, "unique_in_date")
                    .IsUnique();

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account");

                entity.Property(e => e.Confirmroom).HasColumnName("confirmroom");

                entity.Property(e => e.InDate)
                    .HasColumnType("date")
                    .HasColumnName("in_date");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.Account)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_room_id");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("buildings");

                entity.Property(e => e.BuildingId)
                    .ValueGeneratedNever()
                    .HasColumnName("building_id");

                entity.Property(e => e.Buildingname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("buildingname");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Feedback1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("feedback");

                entity.Property(e => e.Felling)
                    .HasMaxLength(10)
                    .HasColumnName("felling")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.AccountNavigation)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.Account)
                    .HasConstraintName("fk_feedback_users");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("rooms");

                entity.Property(e => e.Roomid).HasColumnName("roomid");

                entity.Property(e => e.BuildingId).HasColumnName("building_id");

                entity.Property(e => e.Member)
                    .HasColumnName("member")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RoomImg)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("room_img");

                entity.Property(e => e.Roomname)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("roomname");

                entity.Property(e => e.RtypeId).HasColumnName("rtype_id");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rooms__building___2B3F6F97");

                entity.HasOne(d => d.Rtype)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RtypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rooms__rtype_id__2C3393D0");
            });

            modelBuilder.Entity<Roomtype>(entity =>
            {
                entity.HasKey(e => e.RtypeId)
                    .HasName("PK__roomtype__7E61DA819643C428");

                entity.ToTable("roomtypes");

                entity.Property(e => e.RtypeId).HasColumnName("rtype_id");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Account)
                    .HasName("PK__Users__EA162E10A640852E");

                entity.Property(e => e.Account)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Dateofbirth)
                    .HasColumnType("date")
                    .HasColumnName("dateofbirth");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Inroom)
                    .HasColumnName("inroom")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Money)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Roles)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
