using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Household : BaseEntity
    {
        public string Name { get; set; } = "";
        public string Code { get; set; } = "";
        public Profile Profile { get; set; }


        public List<Chore> chores { get; set; }

    }
}
