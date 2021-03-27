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

        public decimal GetInitialWeight()
        {
            return WeightLogs.OrderBy(w => w.MeasurementDate.Date)
                .ThenBy(w => w.WeightLogId)
                .FirstOrDefault().WeightMeasurement;
        }

        public decimal GetFinalWeight()
        {
            return WeightLogs.OrderByDescending(w => w.MeasurementDate.Date)
                .ThenByDescending(w => w.WeightLogId)
                .FirstOrDefault().WeightMeasurement;
        }

        public decimal GetTotalWeightLost()
        {
            return GetInitialWeight() - GetFinalWeight();
        }

        public decimal GetTotalPctLost()
        {
            var initialWeight = GetInitialWeight();

            return (initialWeight - GetFinalWeight()) / initialWeight;
        }
    }
}
