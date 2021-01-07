using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeighUpBlazor.Shared.Models
{
    public class Competition
    {
        public int CompetitionId { get; set; }
        public string Name { get; set; }
        public decimal PlayInAmount { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(7);
        public int NumberOfWeeks { get; set; } = 1;
        public string CreatedBy { get; set; }
        public string CreatedByUserId { get; set; }

        public List<Contestant> Contestants { get; set; }
    }
}
