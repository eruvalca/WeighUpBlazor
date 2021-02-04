using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeighUpBlazor.Shared.Models
{
    public class ContestantResultSet
    {
        public Contestant Contestant { get; set; }
        public decimal PreviousWeightMeasurement { get; set; }
        public decimal DeadlineWeightMeasurement { get; set; }
        public decimal PercentChange { get; set; }
        public bool IsDeadlineWinner { get; set; }

        public void CalculatePercentChange()
        {
            PercentChange = (PreviousWeightMeasurement - DeadlineWeightMeasurement) / PreviousWeightMeasurement;
        }
    }
}
