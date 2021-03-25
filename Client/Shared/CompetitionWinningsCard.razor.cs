using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeighUpBlazor.Client.Services;
using WeighUpBlazor.Shared.Models;

namespace WeighUpBlazor.Client.Shared
{
    public partial class CompetitionWinningsCard
    {
        [Inject]
        private CompetitionsService CompetitionsService { get; set; }

        [Parameter]
        public Competition Competition { get; set; }

        private Dictionary<WeighInDeadline, List<ContestantResultSet>> CompetitionResults = new Dictionary<WeighInDeadline, List<ContestantResultSet>>();
        private FinalResultSet FinalResults;

        protected override void OnInitialized()
        {
            CompetitionResults = Competition.GetWeeklyResults();

            if (DateTime.Today.ToUniversalTime().Date >= Competition.EndDate.Date)
            {
                FinalResults = Competition.GetFinalResult();
            }
        }
    }
}
