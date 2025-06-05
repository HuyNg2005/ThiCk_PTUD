using Microsoft.EntityFrameworkCore;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Service.BookingDetail;

namespace QuanLyNhaHang.Data
{
    public class AppDbContext : DbContext
    {  
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<BookingDetailEntity> BookingDetails { get; set; }
        public DbSet<ServiceHotelEntity> Services { get; set; }
        public DbSet<BookingServiceDetailEntity> BookingServiceDetails { get; set; }
        public IBookingDetailService BookingDetailservice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             // UserEntity - BookingEntity (CheckIn/CheckOut)
            modelBuilder.Entity<BookingEntity>()
                .HasOne(b => b.CheckInByUser)
                .WithMany(u => u.CheckInBookings)
                .HasForeignKey(b => b.CheckInByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookingEntity>()
                .HasOne(b => b.CheckOutByUser)
                .WithMany(u => u.CheckOutBookings)
                .HasForeignKey(b => b.CheckOutByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // BookingEntity - RoomEntity
            modelBuilder.Entity<BookingEntity>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // BookingDetailEntity - BookingEntity - CustomerEntity
            modelBuilder.Entity<BookingDetailEntity>()
                .HasOne(bd => bd.Booking)
                .WithMany(b => b.BookingDetails)
                .HasForeignKey(bd => bd.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingDetailEntity>()
                .HasOne(bd => bd.Customer)
                .WithMany(c => c.BookingDetails)
                .HasForeignKey(bd => bd.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // BookingServiceDetailEntity - BookingEntity - ServiceHotelEntity
            modelBuilder.Entity<BookingServiceDetailEntity>()
                .HasOne(bsd => bsd.Booking)
                .WithMany(b => b.BookingServices)
                .HasForeignKey(bsd => bsd.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookingServiceDetailEntity>()
                .HasOne(bsd => bsd.Service)
                .WithMany(s => s.BookingServices)
                .HasForeignKey(bsd => bsd.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
        
    

