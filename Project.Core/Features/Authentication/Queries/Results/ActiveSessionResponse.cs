namespace Project.Core.Features.Authentication.Queries.Results
{
    public class ActiveSessionResponse
    {
        public int SessionId { get; set; } // ????? ?????? (RefreshToken ID)
        public string DeviceId { get; set; } = null!; // ????? ??????
        public string DeviceName { get; set; } = null!; // ??? ??????
        public string IpAddress { get; set; } = null!; // ????? IP
        public DateTime CreatedAt { get; set; } // ??? ????? ??????
        public DateTime? LastActivityAt { get; set; } // ??? ??? ????
        public DateTime ExpiresAt { get; set; } // ??? ????? ??????
        public bool IsCurrentSession { get; set; } // ?? ??? ?????? ???????
    }

    public class UserSessionsResponse
    {
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public List<ActiveSessionResponse> ActiveSessions { get; set; } = new();
        public int TotalActiveSessions { get; set; }
    }
}
