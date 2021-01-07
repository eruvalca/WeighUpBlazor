using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeighUpBlazor.Shared.Models
{
    public class WeighInDeadline
    {
        public int WeighInDeadlineId { get; set; }
        public DateTime DeadlineDate { get; set; }

        public int CompetitionId { get; set; }
    }
}
