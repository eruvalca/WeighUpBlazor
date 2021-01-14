using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeighUpBlazor.Client.Services;
using WeighUpBlazor.Shared.Models;

namespace WeighUpBlazor.Client.Shared
{
    public partial class CompetitionResultsCard
    {
        [Inject]
        private CompetitionsService CompetitionsService { get; set; }

        [Parameter]
        public int CompetitionId { get; set; }

        private Competition Competition { get; set; }
        private List<WeighInDeadline> RelevantDeadlines { get; set; }
        private List<WeighInDeadline> CompleteDeadlines { get; set; } = new List<WeighInDeadline>();

        protected async override Task OnInitializedAsync()
        {
            Competition = await CompetitionsService.GetCompetition(CompetitionId);
            SetupPage();
        }

        private void SetupPage()
        {
            RelevantDeadlines = Competition.WeighInDeadlines
                .Where(w => w.IsActive && w.DeadlineDate.Date != Competition.StartDate.Date && w.DeadlineDate <= DateTime.Today)
                .OrderBy(w => w.DeadlineDate)
                .ToList();

            foreach (var deadline in RelevantDeadlines)
            {
                var deadlineComplete = true;

                foreach (var contestant in Competition.Contestants)
                {
                    if (!contestant.WeightLogs.Any(w => w.MeasurementDate.Date == deadline.DeadlineDate.Date))
                    {
                        deadlineComplete = false;
                    }
                }

                if (deadlineComplete)
                {
                    CompleteDeadlines.Add(deadline);
                }
            }
        }
    }
}
