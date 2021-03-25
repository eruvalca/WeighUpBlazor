using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeighUpBlazor.Shared.Models
{
    public class FinalResultSet
    {
        public Contestant CompetitionWinner { get; set; }
        public Dictionary<decimal, Contestant> PercentWinners { get; set; }
        public Dictionary<decimal, Contestant> TotalWinnings { get; set; }
    }
}
