using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProfileCreateInDto
    {
        [MaxLength(20)]
        public string Alias { get; set; } = "";
        public Guid? HouseholdId { get; set; }
        public bool IsAdmin { get; set; }
    }
}


