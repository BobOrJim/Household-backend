
namespace Core.Entities
{
    public class Pause : BaseEntity
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Household Household { get; set; }
        public Guid HouseholdId { get; set; }
        public Guid ProfileIdQol { get; set; } // Ingen relation och inget som EF använder, men en QoL för oss!

    }
}
