
namespace Core.Entities
{
    public class PauseOutDto
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } = new DateTime();

        public DateTime EndDate { get; set; } = new DateTime();

        public Guid ProfileIdQol { get; set; }

        public Guid HouseholdId { get; set; }
    }
}


