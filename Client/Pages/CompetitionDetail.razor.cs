using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeighUpBlazor.Client.Services;
using WeighUpBlazor.Shared.Models;

namespace WeighUpBlazor.Client.Pages
{
    [Authorize]
    public partial class CompetitionDetail
    {
        [Inject]
        private CompetitionsService CompetitionsService { get; set; }

        [Parameter]
        public int CompetitionId { get; set; }

        private Competition Competition { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Competition = await CompetitionsService.GetCompetition(CompetitionId);
        }
    }
}
