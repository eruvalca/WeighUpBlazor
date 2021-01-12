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
    public partial class CreateWeightLog
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private CompetitionsService CompetitionsService { get; set; }
        [Inject]
        private ContestantsService ContestantsService { get; set; }
        [Inject]
        private WeightLogsService WeightLogsService { get; set; }

        [Parameter]
        public int CompetitionId { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private string UserId { get; set; }
        private string Username { get; set; }
        private Competition Competition { get; set; } = new Competition();
        private Contestant Contestant { get; set; }
        private WeightLog WeightLog { get; set; } = new WeightLog();
        private bool IsFormSubmitting { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                UserId = user.FindFirst(c => c.Type == "sub").Value;
                Username = user.FindFirst(c => c.Type == "name").Value;
            }

            Competition = await CompetitionsService.GetCompetition(CompetitionId);
            Contestant = Competition.Contestants.FirstOrDefault(c => c.UserId == UserId);
            WeightLog.ContestantId = Contestant.ContestantId;
        }

        private async Task HandleSubmit()
        {
            IsFormSubmitting = true;
            WeightLog = await WeightLogsService.PostWeightLog(WeightLog);
            Navigation.NavigateTo($"competition-detail/{Competition.CompetitionId}");
        }
    }
}
