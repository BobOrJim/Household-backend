using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PauseDto
    {
        public DateTime StartDate { get; set; } = new DateTime();

        public DateTime EndDate { get; set; } = new DateTime();

        public Guid ProfileId { get; set; }
    }
}


