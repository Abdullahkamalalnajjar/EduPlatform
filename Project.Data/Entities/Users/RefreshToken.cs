namespace Project.Data.Entities.Users
{

    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedOn { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
        public bool IsActive => RevokedOn is null && !IsExpired;
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        // لتتبع جهاز المستخدم وضمان جلسة واحدة فقط
        public string? DeviceId { get; set; } // معرّف فريد للجهاز (UUID أو معرّف من التطبيق)
        public string? DeviceName { get; set; } // اسم الجهاز (Chrome على Windows، Safari على iPhone إلخ)
        public string? IpAddress { get; set; } // عنوان IP للجهاز
        public DateTime? LastActivityAt { get; set; } // آخر وقت نشاط على الجهاز
    }
}
