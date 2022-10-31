
namespace Core.Entities
{
    public class Household : BaseEntity
    {
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
        public List<Profile> Profiles { get; set; }
        public List<Chore> Chores { get; set; }
        public List<ChoreCompleted> ChoresCompleted { get; set; }
        public List<Pause> Pauses { get; set; }
    }
}
