
namespace Core.Entities
{
    //the optional properties are becouse we need to break the circular reference
    public class ChoreCompleted : BaseEntity
    {
        public DateTime CompletedAt { get; set; }
        public Guid? ProfileIdQol { get; set; }
        public Chore? Chore { get; set; }
        public Guid? ChoreId { get; set; }
        public Household? Household { get; set; }
        public Guid? HouseholdId { get; set; }
    }
}
