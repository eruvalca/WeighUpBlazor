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
    public partial class CreateCompetition
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private CompetitionsService CompetitionsService { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        private DateTime _startDate;
        private DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                HandleStartDateChange();
            }
        }
        private string UserId { get; set; }
        private string Username { get; set; }
        private Competition NewCompetition { get; set; } = new Competition();
        private bool IsFormSubmitting { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            _startDate = DateTime.Today;
            NewCompetition.WeighInDeadlines = new List<WeighInDeadline>
            {
                new WeighInDeadline()
                {
                    DeadlineDate = NewCompetition.StartDate,
                    IsActive = true,
                    CompetitionId = NewCompetition.CompetitionId
                },
                new WeighInDeadline()
                {
                    DeadlineDate = NewCompetition.EndDate,
                    IsActive = true,
                    CompetitionId = NewCompetition.CompetitionId
                }
            };

            var authState = await AuthenticationStateTask;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                UserId = user.FindFirst(c => c.Type == "sub").Value;
                Username = user.FindFirst(c => c.Type == "name").Value;
            }
        }

        private void HandleStartDateChange()
        {
            NewCompetition.StartDate = StartDate;
            NewCompetition.EndDate = NewCompetition.StartDate.AddDays(NewCompetition.NumberOfWeeks * 7);

            NewCompetition.WeighInDeadlines = new List<WeighInDeadline>
            {
                new WeighInDeadline()
                {
                    DeadlineDate = NewCompetition.StartDate,
                    IsActive = true,
                    CompetitionId = NewCompetition.CompetitionId
                },
                new WeighInDeadline()
                {
                    DeadlineDate = NewCompetition.EndDate,
                    IsActive = true,
                    CompetitionId = NewCompetition.CompetitionId
                }
            };

            for (int i = 1; i < NewCompetition.NumberOfWeeks; i++)
            {
                NewCompetition.WeighInDeadlines.Add(new WeighInDeadline()
                {
                    DeadlineDate = NewCompetition.StartDate.AddDays(i * 7),
                    IsActive = true,
                    CompetitionId = NewCompetition.CompetitionId
                });
            }
        }

        private void HandleWeekChange(ChangeEventArgs e)
        {
            var numberOfWeeks = int.Parse(e.Value.ToString());
            NewCompetition.EndDate = NewCompetition.StartDate.AddDays(numberOfWeeks * 7);

            NewCompetition.WeighInDeadlines = new List<WeighInDeadline>
            {
                new WeighInDeadline()
                {
                    DeadlineDate = NewCompetition.StartDate,
                    IsActive = true,
                    CompetitionId = NewCompetition.CompetitionId
                },
                new WeighInDeadline()
                {
                    DeadlineDate = NewCompetition.EndDate,
                    IsActive = true,
                    CompetitionId = NewCompetition.CompetitionId
                }
            };

            for (int i = 1; i < numberOfWeeks; i++)
            {
                NewCompetition.WeighInDeadlines.Add(new WeighInDeadline()
                {
                    DeadlineDate = NewCompetition.StartDate.AddDays(i * 7),
                    IsActive = true,
                    CompetitionId = NewCompetition.CompetitionId
                });
            }
        }

        private async Task HandleSubmit()
        {
            IsFormSubmitting = true;
            NewCompetition.CreatedBy = Username;
            NewCompetition.CreatedByUserId = UserId;

            NewCompetition = await CompetitionsService.PostCompetition(NewCompetition);
            Navigation.NavigateTo($"competition-detail/{NewCompetition.CompetitionId}");
        }
    }
}
