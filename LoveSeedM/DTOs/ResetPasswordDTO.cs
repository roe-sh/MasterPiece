namespace LoveSeedM.DTOs
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; } = null!;        // User's email for identification
        public string NewPassword { get; set; } = null!;  // New password to be set
    }
}
