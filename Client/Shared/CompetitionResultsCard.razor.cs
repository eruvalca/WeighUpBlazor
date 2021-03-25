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
        private Dictionary<WeighInDeadline, List<ContestantResultSet>> CompetitionResults = new Dictionary<WeighInDeadline, List<ContestantResultSet>>();

        protected async override Task OnInitializedAsync()
        {
            Competition = await CompetitionsService.GetCompetition(CompetitionId);
            CompetitionResults = Competition.GetWeeklyResults();
        }
    }
}
