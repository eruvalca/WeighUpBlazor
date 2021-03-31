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
        public List<FinalContestantPercentage> PercentWinners { get; set; }
        public List<FinalContestantWinnings> TotalWinnings { get; set; }
    }

    public class FinalContestantWinnings
    {
        public decimal Winnings { get; set; }
        public Contestant Contestant { get; set; }
    }

    public class FinalContestantPercentage
    {
        public decimal Percent { get; set; }
        public Contestant Contestant { get; set; }
    }
}
