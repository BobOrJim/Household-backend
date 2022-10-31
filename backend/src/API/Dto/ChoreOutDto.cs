
namespace Core.Entities
{
    public class ChoreOutDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public int Points { get; set; } = 0;
        public string Description { get; set; } = "";
        public string PictureUrl { get; set; } = "";
        public string AudioUrl { get; set; } = "";
        public int Frequency { get; set; } = 0;
        public bool IsArchived { get; set; } = false;
        public Guid HouseholdId { get; set; }
    }
}


