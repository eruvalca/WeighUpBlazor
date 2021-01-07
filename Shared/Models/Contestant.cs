using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeighUpBlazor.Shared.Models
{
    public class Contestant
    {
        public int ContestantId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public int CompetitionId { get; set; }
        public List<WeightLog> WeightLogs { get; set; }
    }
}
