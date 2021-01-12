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
        [Inject]
        private ContestantsService ContestantsService { get; set; }

        [Parameter]
        public int CompetitionId { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private string UserId { get; set; }
        private string Username { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private bool IsContestantJoining { get; set; } = false;
        private Competition Competition { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateTask;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                UserId = user.FindFirst(c => c.Type == "sub").Value;
                Username = user.FindFirst(c => c.Type == "name").Value;
                FirstName = user.FindFirst(c => c.Type == "given_name").Value;
                LastName = user.FindFirst(c => c.Type == "family_name").Value;
            }

            Competition = await CompetitionsService.GetCompetition(CompetitionId);
        }

        public async Task HandleContestantJoin()
        {
            IsContestantJoining = true;
            var contestant = new Contestant()
            {
                UserId = UserId,
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                CompetitionId = Competition.CompetitionId
            };

            contestant = await ContestantsService.PostContestant(contestant);
            Competition = await CompetitionsService.GetCompetition(CompetitionId);
            IsContestantJoining = false;
        }
    }
}
