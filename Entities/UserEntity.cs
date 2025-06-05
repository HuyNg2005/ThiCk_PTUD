using System;

namespace QuanLyNhaHang.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public UserRole Role { get; set; }

        // Navigation
        public ICollection<BookingEntity> CheckInBookings { get; set; }
        public ICollection<BookingEntity> CheckOutBookings { get; set; }

        public enum UserRole
        {
            Admin,
            Staff,
            Manager
        }
    }
}
