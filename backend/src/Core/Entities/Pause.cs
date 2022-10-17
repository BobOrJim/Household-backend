using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Pause : BaseEntity
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid ProfileId { get; set; }
        public Guid HouseholdId { get; set; }

    }
}
