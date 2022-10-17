using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    //the optional properties are becouse we need to break the circular reference
    public class ChoreCompleted : BaseEntity
    {
        public DateTime CompletedAt { get; set; }
        // public Profile? Profile { get; set; }
        public Guid? ProfileIdQol { get; set; }
        public Chore? Chore { get; set; }
        public Guid? ChoreId { get; set; }
        public Household? Household { get; set; }
        public Guid? HouseholdId { get; set; }
    }
}
