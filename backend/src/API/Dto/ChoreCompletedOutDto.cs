namespace Core.Entities
{
    public class ChoreCompletedOutDto
    {
        public Guid Id { get; set; }
        public DateTime CompletedAt { get; set; } = new();
        public Guid ProfileIdQol { get; set; }
        public Guid ChoreId { get; set; }
        public Guid HouseholdId { get; set; }
    }
}


