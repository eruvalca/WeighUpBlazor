﻿using System;
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
        public List<WeighInDeadline> WeighInDeadlines { get; set; }

        public Dictionary<WeighInDeadline, List<ContestantResultSet>> GetResults()
        {
            var results = new Dictionary<WeighInDeadline, List<ContestantResultSet>>();
            var completeDeadlines = new List<WeighInDeadline>();

            var relevantDeadlines = WeighInDeadlines
                .Where(w => w.IsActive && w.DeadlineDate.Date != StartDate.Date && w.DeadlineDate.Date <= DateTime.Today.Date)
                .OrderBy(w => w.DeadlineDate.Date)
                .ToList();

            foreach (var deadline in relevantDeadlines)
            {
                var deadlineComplete = true;

                foreach (var contestant in Contestants)
                {
                    if (!contestant.WeightLogs.Any(w => w.MeasurementDate.ToLocalTime().Date == deadline.DeadlineDate.Date))
                    {
                        deadlineComplete = false;
                    }
                }

                if (deadlineComplete)
                {
                    completeDeadlines.Add(deadline);
                }
            }

            foreach (var deadline in completeDeadlines)
            {
                var deadlineSet = new List<ContestantResultSet>();

                foreach (var contestant in Contestants)
                {
                    var contestantResultSet = new ContestantResultSet
                    {
                        Contestant = contestant,
                        PreviousWeightMeasurement = contestant.WeightLogs
                            .Where(w => w.MeasurementDate.ToLocalTime().Date < deadline.DeadlineDate.Date)
                            .OrderByDescending(w => w.MeasurementDate.ToLocalTime())
                            .FirstOrDefault().WeightMeasurement,
                        DeadlineWeightMeasurement = contestant.WeightLogs
                            .Where(w => w.MeasurementDate.ToLocalTime().Date == deadline.DeadlineDate.Date)
                            .FirstOrDefault().WeightMeasurement
                    };

                    contestantResultSet.CalculatePercentChange();

                    deadlineSet.Add(contestantResultSet);
                }

                deadlineSet.OrderByDescending(d => d.PercentChange).FirstOrDefault().IsDeadlineWinner = true;

                results.Add(deadline, deadlineSet);
            }

            return results;
        }
    }
}
