using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Chore : BaseEntity
    {
        public string Name { get; set; } = "";
        public int Points { get; set; } = 0;
        public string Description { get; set; } = "";
        public string PictureUrl { get; set; } = "";
        public string? AudioUrl { get; set; } = "";
        public int Frequency { get; set; } = 0;
        public bool IsArchived { get; set; } = false;
        public Household Household { get; set; }
        public Guid HouseholdId { get; set; }
        public List<ChoreCompleted> ChoresCompleted { get; set; }
    }
}
