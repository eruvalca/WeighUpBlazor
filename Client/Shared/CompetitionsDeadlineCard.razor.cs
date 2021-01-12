using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeighUpBlazor.Client.Services;
using WeighUpBlazor.Shared.Models;

namespace WeighUpBlazor.Client.Shared
{
    [Authorize]
    public partial class CompetitionsDeadlineCard
    {
        [Inject]
        private NavigationManager Navigation { get; set; }

        [Parameter]
        public Competition Competition { get; set; }
        [Parameter]
        public string UserId { get; set; }
        [Parameter]
        public EventCallback HandleContestantJoin { get; set; }
    }
}
