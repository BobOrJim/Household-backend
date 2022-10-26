using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ChoreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public int Points { get; set; } = 0;
        public string Description { get; set; } = "";
        public string PictureUrl { get; set; } = "";
        public string AudioUrl { get; set; } = "";
        public int Frequency { get; set; } = 0;
        public bool IsArchived { get; set; } = false;
        public Guid HouseholdId { get; set; }
    }
}


