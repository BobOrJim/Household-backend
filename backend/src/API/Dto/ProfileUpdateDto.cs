using System.ComponentModel.DataAnnotations;


namespace Core.Entities
{
    public class ProfileUpdateDto
    {
        [MaxLength(20)]
        [MinLength(2)]
        public string? Alias { get; set; } = "";
        public bool? IsAdmin { get; set; }
        public string? Avatar { get; set; }
        [MaxLength(7)]
        [MinLength(4)]
        public bool? PendingRequest { get; set; }

    }
}


