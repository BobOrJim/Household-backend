namespace Core.Entities
{
    public class ChoreCompletedInDto
    {
        public DateTime CompletedAt { get; set; } = new();
        public Guid ProfileIdQol { get; set; }
        public Guid ChoreId { get; set; }
        public Guid HouseholdId { get; set; }
    }
}


