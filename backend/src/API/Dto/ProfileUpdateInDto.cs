using System.ComponentModel.DataAnnotations;


namespace Core.Entities
{
    public class ProfileUpdateInDto
    {
        public string? Alias { get; set; } = "";
        public bool? IsAdmin { get; set; }
        public string? Avatar { get; set; }
        public bool? PendingRequest { get; set; }

    }
}


