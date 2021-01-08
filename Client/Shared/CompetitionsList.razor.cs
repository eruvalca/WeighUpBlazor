using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeighUpBlazor.Client.Services;
using WeighUpBlazor.Shared.Models;

namespace WeighUpBlazor.Client.Shared
{
    [Authorize]
    public partial class CompetitionsList
    {
        [Inject]
        private CompetitionsService CompetitionsService { get; set; }

        private List<Competition> Competitions { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Competitions = await CompetitionsService.GetCompetitions();
        }
    }
}
