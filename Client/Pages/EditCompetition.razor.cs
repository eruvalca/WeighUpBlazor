﻿using Microsoft.AspNetCore.Authorization;
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
    public partial class EditCompetition
    {
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private CompetitionsService CompetitionsService { get; set; }
        [Inject]
        private WeighInDeadlinesService WeighInDeadlinesService { get; set; }

        [Parameter]
        public int CompetitionId { get; set; }

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
        private int _numberOfWeeks;
        private int NumberOfWeeks
        {
            get { return _numberOfWeeks; }
            set
            {
                _numberOfWeeks = value;
                HandleWeekChange();
            }
        }
        private string UserId { get; set; }
        private string Username { get; set; }
        private Competition Competition { get; set; }
        private bool IsFormSubmitting { get; set; } = false;
        private bool IsDeleting { get; set; } = false;

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

            _startDate = Competition.StartDate;
            _numberOfWeeks = Competition.NumberOfWeeks;
        }

        private void HandleStartDateChange()
        {
            Competition.StartDate = StartDate;
            Competition.EndDate = Competition.StartDate.AddDays(Competition.NumberOfWeeks * 7);

            Competition.WeighInDeadlines = new List<WeighInDeadline>
            {
                new WeighInDeadline()
                {
                    DeadlineDate = Competition.StartDate,
                    IsActive = true,
                    CompetitionId = Competition.CompetitionId
                },
                new WeighInDeadline()
                {
                    DeadlineDate = Competition.EndDate,
                    IsActive = true,
                    CompetitionId = Competition.CompetitionId
                }
            };

            for (int i = 1; i < Competition.NumberOfWeeks; i++)
            {
                Competition.WeighInDeadlines.Add(new WeighInDeadline()
                {
                    DeadlineDate = Competition.StartDate.AddDays(i * 7),
                    IsActive = true,
                    CompetitionId = Competition.CompetitionId
                });
            }
        }

        private void HandleWeekChange()
        {
            Competition.NumberOfWeeks = NumberOfWeeks;
            Competition.EndDate = Competition.StartDate.AddDays(NumberOfWeeks * 7);

            Competition.WeighInDeadlines = new List<WeighInDeadline>
            {
                new WeighInDeadline()
                {
                    DeadlineDate = Competition.StartDate,
                    IsActive = true,
                    CompetitionId = Competition.CompetitionId
                },
                new WeighInDeadline()
                {
                    DeadlineDate = Competition.EndDate,
                    IsActive = true,
                    CompetitionId = Competition.CompetitionId
                }
            };

            for (int i = 1; i < NumberOfWeeks; i++)
            {
                Competition.WeighInDeadlines.Add(new WeighInDeadline()
                {
                    DeadlineDate = Competition.StartDate.AddDays(i * 7),
                    IsActive = true,
                    CompetitionId = Competition.CompetitionId
                });
            }
        }

        private async Task HandleSubmit()
        {
            IsFormSubmitting = true;

            await WeighInDeadlinesService.DeleteWeighInDeadlineRangeByCompetition(CompetitionId);
            await CompetitionsService.PutCompetition(Competition.CompetitionId, Competition);
            Navigation.NavigateTo($"/competition-detail/{Competition.CompetitionId}");
        }

        private async Task HandleDelete()
        {
            IsDeleting = true;

            await CompetitionsService.DeleteCompetition(CompetitionId);
            Navigation.NavigateTo("/");
        }
    }
}
